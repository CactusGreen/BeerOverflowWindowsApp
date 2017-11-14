using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using BeerOverflowWindowsApp.BarComparers;
using BeerOverflowWindowsApp.DataModels;
using System.Device.Location;
using System.Configuration;
using System.Threading.Tasks;
using BeerOverflowWindowsApp.BarProviders;
using BeerOverflowWindowsApp.Utilities;
using System.Net;
using System.Net.Http;
using BeerOverflowWindowsApp.Exceptions;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace BeerOverflowWindowsApp
{
    public partial class MainWindow : Form
    {
        private readonly string _defaultLatitude = ConfigurationManager.AppSettings["defaultLatitude"];
        private readonly string _defaultLongitude = ConfigurationManager.AppSettings["defaultLongitude"];
        private readonly string _defaultRadius = ConfigurationManager.AppSettings["defaultRadius"];
        private readonly BarRating _barRating = null;
        private BarData _selectedBar;
        private MapWindow _mapForm;
        private readonly List<object> _providerList = new List<object>
        {
            new GetBarListGoogle(),
            new GetBarListFourSquare(),
            new GetBarListFacebook(),
            new GetBarListTripAdvisor()
        };

        public MainWindow()
        {
            InitializeComponent();
            _barRating = new BarRating();
            LatitudeTextBox.Text = _defaultLatitude;
            LongitudeTextBox.Text = _defaultLongitude;
            var latitude = Convert.ToDouble(_defaultLatitude, CultureInfo.InvariantCulture);
            var longitude = Convert.ToDouble(_defaultLongitude, CultureInfo.InvariantCulture);
            CurrentLocation.currentLocation = new GeoCoordinate(latitude, longitude);
            RadiusTextBox.Text = _defaultRadius;
        }

        public void ReloadDataGrid(bool completely = false)
        {
            if (completely)
            {
                BarDataGridView_ClearHeaderSortGlyphs();
            }
            var barData = _barRating.BarsData;
            BarDataGridView.Rows.Clear();
            foreach (var bar in barData)
            {
                string rating;
                if (bar.Ratings != null && bar.Ratings.Any())
                    rating = bar.Ratings?.Average().ToString("0.00");
                else
                    rating = "0";
                var distance = bar.DistanceToCurrentLocation.ToString("0");
                BarDataGridView.Rows.Add(bar.Title, rating, distance);               
            }
            if (BarDataGridView.Rows.Count > 0 && _selectedBar != null)
            {
                var indexOfBar = _barRating.BarsData.FindIndex(x => x.Title == _selectedBar.Title);
                BarDataGridView.Rows[indexOfBar].Selected = true;
            }
            ClearSelection();
        }

        private void ClearSelection()
        {
            BarDataGridView.ClearSelection();
            _selectedBar = null;
        }

        private async void GoButton_Click(object sender, EventArgs e)
        {
            var latitude = GetLatitude();
            var longitude = GetLongitude();
            var radius = GetRadius();
            var failedToConnectCounter = 0;
            var providerCount = _providerList.Count;

            try
            {
                var barsFromProvider = new List<BarData>();
                var progressStep = 100 / providerCount;
                var result = new BarDataModel();

                var currentProgressValue = 0;
                GoButton.Enabled = false;
                InitiateProgressBars();
                UpdateProgressBars(currentProgressValue);
                foreach (IBeerable provider in _providerList)
                {
                    try
                    {
                        barsFromProvider = await CollectBarsFromProvider(provider, latitude, longitude, radius);
                    }
                    catch (HttpRequestException)
                    {
                        if (++failedToConnectCounter == providerCount)
                        {
                            MessageBox.Show("Check your internet connection, it seems to be down.");
                        }
                    }
                    catch (WebException)
                    {
                        MessageBox.Show("Failed connecting to: " + provider.ProviderName);
                    }

                    result.AddRange(barsFromProvider);
                    currentProgressValue += progressStep;
                    UpdateProgressBars(currentProgressValue);
                }
                result.RemoveDuplicates();
                result.RemoveBarsOutsideRadius(radius);
                await Task.Run(() => result.GetRatings());
                HideProgressBars();
                
                // Display
                _barRating.BarsData = result;
                var currentLocation = GetCurrentLocation();
                foreach (var bar in _barRating.BarsData)
                {
                    bar.DistanceToCurrentLocation =
                        currentLocation.GetDistanceTo(new GeoCoordinate(bar.Latitude, bar.Longitude));
                }
                SortList(CompareType.Distance);
            }
            catch (ArgumentsForProvidersException)
            {
                MessageBox.Show("Please enter the required data correctly. Erroneus data is painted red.");
            }
            finally
            {
                HideProgressBars();
                GoButton.Enabled = true;
            }
        }

        private void InitiateProgressBars()
        {
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
            progressBar.Value = 0;
        }

        private void HideProgressBars()
        {
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
            progressBar.Value = 0;
        }

        private void UpdateProgressBars(int currentValue)
        {
            TaskbarManager.Instance.SetProgressValue(currentValue, 100);
            // this is a big workaround for progressbar not updating as fast as we need it. 
            // TODO: Fix this when we do threads
            // UPDATE, NO IDEA HOW TO FIX YET
            progressBar.Maximum = 101;
            progressBar.Value = currentValue + 1;
            progressBar.Value = currentValue;
            progressBar.Maximum = 100;
        }

        private async Task<List<BarData>> CollectBarsFromProvider(IBeerable provider,
            string latitude, string longitude, string radius)
        {
            List<BarData> response;
            try
            {
                response = await provider.GetBarsAroundAsync(latitude, longitude, radius);
            }
            catch (NotImplementedException)
            {
                response = provider.GetBarsAround(latitude, longitude, radius);
            }
            return response;
        }

        private string GetLatitude()
        {
            CurrentLocation.currentLocation.Latitude = Convert.ToDouble(LatitudeTextBox.Text, CultureInfo.InvariantCulture);
            return LatitudeTextBox.Text;
        }

        private string GetLongitude()
        {
            CurrentLocation.currentLocation.Longitude = Convert.ToDouble(LongitudeTextBox.Text, CultureInfo.InvariantCulture);
            return LongitudeTextBox.Text;
        }

        private string GetRadius()
        {
            return RadiusTextBox.Text;
        }

        private void ManualBarRating_Click(object sender, EventArgs e)
        {
            var rating = manualBarRating.Rating;
            int ratingNumber;
            if (_selectedBar != null && rating != "" && int.TryParse(rating, out ratingNumber))
            {
                _barRating.AddRating(_selectedBar, ratingNumber);
                _selectedBar.Ratings = WebApiAccess.GetBarRatings(_selectedBar);
                Resort();
            }
        }

        private void BarDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (BarDataGridView.CurrentRow != null)
            {
                var selectedBarName = (string) BarDataGridView.CurrentRow.Cells["titleColumn"].Value;
                _selectedBar = _barRating.BarsData.Find(bar => bar.Title == selectedBarName);
            }
        }

        private void DataTextBoxChanged(TextBox textbox, Func<string, bool> textValidate)
        {
            var textIsCorrect = false;
            try
            {
                textIsCorrect = textValidate(textbox.Text);
            }
            catch (ArgumentsForProvidersException)
            {
                PaintTextBoxIncorrect(textbox);
            }
            if (textIsCorrect)
            { ResetTextBoxColor(textbox); }
        }

        private void LatitudeTextBox_TextChanged(object sender, EventArgs e)
        {
            DataTextBoxChanged(LatitudeTextBox, RegexTools.LatitudeTextIsCorrect);
        }

        private void LongitudeTextBox_TextChanged(object sender, EventArgs e)
        {
            DataTextBoxChanged(LongitudeTextBox, RegexTools.LongitudeTextIsCorrect);
        }

        private void RadiusTextBox_TextChanged(object sender, EventArgs e)
        {
            DataTextBoxChanged(RadiusTextBox, RegexTools.RadiusTextIsCorrect);
        }

        private void PaintTextBoxIncorrect(TextBox textBox)
        {
            textBox.ForeColor = Color.White;
            textBox.BackColor = Color.Red;
        }

        private void ResetTextBoxColor(TextBox textBox)
        {
            textBox.ResetForeColor();
            textBox.ResetBackColor();
        }

        private void SortList(CompareType compareType, SortOrder sortOrder = SortOrder.Ascending)
        {
            var columnIndex = (int)compareType - 1;
            var isAscending = sortOrder == SortOrder.Ascending;
            if (sortOrder != SortOrder.None)
            {
                _barRating.Sort(compareType, isAscending);
                BarDataGridView_ClearHeaderSortGlyphs();
                BarDataGridView.Columns[columnIndex].HeaderCell.SortGlyphDirection = sortOrder;
                ReloadDataGrid();
            }
        }

        private void Resort()
        {
            var currentSortOrder = SortOrder.None;
            var currentSortColumn = CompareType.None;
            foreach (DataGridViewColumn column in BarDataGridView.Columns)
            {
                if (column.HeaderCell.SortGlyphDirection == SortOrder.None) continue;
                currentSortOrder = column.HeaderCell.SortGlyphDirection;
                currentSortColumn = (CompareType) column.Index + 1;
                break;
            }
            if (currentSortOrder != SortOrder.None && currentSortColumn != CompareType.None)
            {
                SortList(currentSortColumn, currentSortOrder);
            }
        }

        private void BarDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var currentSortOrder = BarDataGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection;

            var toBeSortOrder = currentSortOrder != SortOrder.None
                ? (currentSortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending)
                : SortOrder.Ascending;
            SortList((CompareType) e.ColumnIndex + 1, toBeSortOrder);
        }

        private void BarDataGridView_ClearHeaderSortGlyphs()
        {
            foreach (DataGridViewColumn column in BarDataGridView.Columns)
            {
                column.HeaderCell.SortGlyphDirection = SortOrder.None;
            }
        }

        private GeoCoordinate GetCurrentLocation()
        {
            var currentLatitude = Convert.ToDouble(GetLatitude(), CultureInfo.InvariantCulture);
            var currentLongitude = Convert.ToDouble(GetLongitude(), CultureInfo.InvariantCulture);
            var currentLocation = new GeoCoordinate(currentLatitude, currentLongitude);
            return currentLocation;
        }

        private void MapButton_Click(object sender, EventArgs e)
        {
            var latitude = GetLatitude();
            var longitude = GetLongitude();
            if (_mapForm == null)
            {
                _mapForm = new MapWindow();
                _mapForm.FormClosed += MapForm_FormClosed;
            }
            _mapForm.Show(this);
            Hide();
        }

        void MapForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mapForm = null;
            Show();
        }
    }
}
