﻿using System;
using BeerOverflowWindowsApp.DataModels;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BeerOverflowWindowsApp
{
    public partial class MapWindow : Form
    {
        public MapWindow()
        {
            InitializeComponent();
        }

        public void MapWindowForm()
        {
            ActiveForm.Close();
        }
    }
}
