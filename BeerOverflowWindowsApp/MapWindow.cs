using System;
using BeerOverflowWindowsApp.DataModels;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BeerOverflowWindowsApp
{
    public partial class MapWindow : Form
    {
        public MapWindow(BarDataModel listOfBars)
        {
            InitializeComponent(listOfBars);
        }

        public MapWindow() { }

        public void MapWindowForm()
        {
            ActiveForm.Close();
        }

        //public MapWindow() { }
    }
}
