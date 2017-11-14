using WebApi.BarRaters;

namespace WebApi
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.BarDataGridView = new System.Windows.Forms.DataGridView();
            this.titleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ratingColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Distance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.LatitudeLabel = new System.Windows.Forms.Label();
            this.LatitudeTextBox = new System.Windows.Forms.TextBox();
            this.LongitudeTextBox = new System.Windows.Forms.TextBox();
            this.LongitudeLabel = new System.Windows.Forms.Label();
            this.RadiusLabel = new System.Windows.Forms.Label();
            this.GoButton = new System.Windows.Forms.Button();
            this.RadiusTextBox = new System.Windows.Forms.TextBox();
            this.MapButton = new System.Windows.Forms.Button();
            this.manualBarRating = new WebApi.BarRaters.ManualBarRating();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BarDataGridView)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.BarDataGridView, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.manualBarRating, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.progressBar, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(956, 505);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // BarDataGridView
            // 
            this.BarDataGridView.AllowUserToAddRows = false;
            this.BarDataGridView.AllowUserToDeleteRows = false;
            this.BarDataGridView.AllowUserToResizeColumns = false;
            this.BarDataGridView.AllowUserToResizeRows = false;
            this.BarDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BarDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.titleColumn,
            this.ratingColumn,
            this.Distance});
            this.BarDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BarDataGridView.Location = new System.Drawing.Point(275, 4);
            this.BarDataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.BarDataGridView.MultiSelect = false;
            this.BarDataGridView.Name = "BarDataGridView";
            this.BarDataGridView.ReadOnly = true;
            this.BarDataGridView.RowHeadersVisible = false;
            this.BarDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.BarDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.BarDataGridView.Size = new System.Drawing.Size(677, 326);
            this.BarDataGridView.TabIndex = 15;
            this.BarDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.BarDataGridView_ColumnHeaderMouseClick);
            this.BarDataGridView.SelectionChanged += new System.EventHandler(this.BarDataGridView_SelectionChanged);
            // 
            // titleColumn
            // 
            this.titleColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.titleColumn.FillWeight = 5F;
            this.titleColumn.HeaderText = "Title";
            this.titleColumn.Name = "titleColumn";
            this.titleColumn.ReadOnly = true;
            this.titleColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.titleColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // ratingColumn
            // 
            this.ratingColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ratingColumn.FillWeight = 2F;
            this.ratingColumn.HeaderText = "Rating";
            this.ratingColumn.Name = "ratingColumn";
            this.ratingColumn.ReadOnly = true;
            this.ratingColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ratingColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Distance
            // 
            this.Distance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Distance.FillWeight = 2F;
            this.Distance.HeaderText = "Distance, (m)";
            this.Distance.Name = "Distance";
            this.Distance.ReadOnly = true;
            this.Distance.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            this.tableLayoutPanel3.Controls.Add(this.LatitudeLabel, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.LatitudeTextBox, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.LongitudeTextBox, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.LongitudeLabel, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.RadiusLabel, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.GoButton, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.RadiusTextBox, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.MapButton, 1, 4);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 6;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(263, 326);
            this.tableLayoutPanel3.TabIndex = 13;
            // 
            // LatitudeLabel
            // 
            this.LatitudeLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LatitudeLabel.AutoSize = true;
            this.LatitudeLabel.Location = new System.Drawing.Point(4, 6);
            this.LatitudeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LatitudeLabel.Name = "LatitudeLabel";
            this.LatitudeLabel.Size = new System.Drawing.Size(59, 17);
            this.LatitudeLabel.TabIndex = 1;
            this.LatitudeLabel.Text = "Latitude";
            // 
            // LatitudeTextBox
            // 
            this.LatitudeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LatitudeTextBox.Location = new System.Drawing.Point(83, 4);
            this.LatitudeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.LatitudeTextBox.Name = "LatitudeTextBox";
            this.LatitudeTextBox.Size = new System.Drawing.Size(176, 22);
            this.LatitudeTextBox.TabIndex = 4;
            this.LatitudeTextBox.TextChanged += new System.EventHandler(this.LatitudeTextBox_TextChanged);
            // 
            // LongitudeTextBox
            // 
            this.LongitudeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LongitudeTextBox.Location = new System.Drawing.Point(83, 34);
            this.LongitudeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.LongitudeTextBox.Name = "LongitudeTextBox";
            this.LongitudeTextBox.Size = new System.Drawing.Size(176, 22);
            this.LongitudeTextBox.TabIndex = 3;
            this.LongitudeTextBox.TextChanged += new System.EventHandler(this.LongitudeTextBox_TextChanged);
            // 
            // LongitudeLabel
            // 
            this.LongitudeLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LongitudeLabel.AutoSize = true;
            this.LongitudeLabel.Location = new System.Drawing.Point(4, 36);
            this.LongitudeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LongitudeLabel.Name = "LongitudeLabel";
            this.LongitudeLabel.Size = new System.Drawing.Size(71, 17);
            this.LongitudeLabel.TabIndex = 0;
            this.LongitudeLabel.Text = "Longitude";
            // 
            // RadiusLabel
            // 
            this.RadiusLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RadiusLabel.AutoSize = true;
            this.RadiusLabel.Location = new System.Drawing.Point(4, 66);
            this.RadiusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.RadiusLabel.Name = "RadiusLabel";
            this.RadiusLabel.Size = new System.Drawing.Size(52, 17);
            this.RadiusLabel.TabIndex = 2;
            this.RadiusLabel.Text = "Radius";
            // 
            // GoButton
            // 
            this.GoButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GoButton.Location = new System.Drawing.Point(83, 94);
            this.GoButton.Margin = new System.Windows.Forms.Padding(4);
            this.GoButton.Name = "GoButton";
            this.GoButton.Size = new System.Drawing.Size(176, 28);
            this.GoButton.TabIndex = 6;
            this.GoButton.Text = "Go";
            this.GoButton.UseVisualStyleBackColor = true;
            this.GoButton.Click += new System.EventHandler(this.GoButton_Click);
            // 
            // RadiusTextBox
            // 
            this.RadiusTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RadiusTextBox.Location = new System.Drawing.Point(83, 64);
            this.RadiusTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.RadiusTextBox.Name = "RadiusTextBox";
            this.RadiusTextBox.Size = new System.Drawing.Size(176, 22);
            this.RadiusTextBox.TabIndex = 5;
            this.RadiusTextBox.TextChanged += new System.EventHandler(this.RadiusTextBox_TextChanged);
            // 
            // MapButton
            // 
            this.MapButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MapButton.Location = new System.Drawing.Point(83, 130);
            this.MapButton.Margin = new System.Windows.Forms.Padding(4);
            this.MapButton.Name = "MapButton";
            this.MapButton.Size = new System.Drawing.Size(176, 28);
            this.MapButton.TabIndex = 6;
            this.MapButton.Text = "Show Map";
            this.MapButton.UseVisualStyleBackColor = true;
            this.MapButton.Click += new System.EventHandler(this.MapButton_Click);
            // 
            // manualBarRating
            // 
            this.manualBarRating.ImageSize = 100;
            this.manualBarRating.Location = new System.Drawing.Point(275, 374);
            this.manualBarRating.Margin = new System.Windows.Forms.Padding(4);
            this.manualBarRating.MaximumSize = new System.Drawing.Size(667, 123);
            this.manualBarRating.MinimumSize = new System.Drawing.Size(667, 123);
            this.manualBarRating.Name = "manualBarRating";
            this.manualBarRating.Size = new System.Drawing.Size(667, 123);
            this.manualBarRating.TabIndex = 12;
            this.manualBarRating.Click += new System.EventHandler(this.ManualBarRating_Click);
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar.Location = new System.Drawing.Point(275, 338);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(677, 28);
            this.progressBar.TabIndex = 14;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(956, 505);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainWindow";
            this.Text = "BeerOverflow";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BarDataGridView)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        /*private System.Windows.Forms.Button ButtonSortByTitle;
        private System.Windows.Forms.Button ButtonSortByRating;
        private System.Windows.Forms.Button ButtonSortByDistance;*/
        private ManualBarRating manualBarRating;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView BarDataGridView;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ratingColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Distance;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label LatitudeLabel;
        private System.Windows.Forms.TextBox LatitudeTextBox;
        private System.Windows.Forms.TextBox LongitudeTextBox;
        private System.Windows.Forms.Label LongitudeLabel;
        private System.Windows.Forms.Label RadiusLabel;
        private System.Windows.Forms.Button GoButton;
        private System.Windows.Forms.TextBox RadiusTextBox;
        private System.Windows.Forms.Button MapButton;
    }
}