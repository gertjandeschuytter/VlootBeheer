﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfFleetManagement
{
    /// <summary>
    /// Interaction logic for TankkaartWindow.xaml
    /// </summary>
    public partial class TankkaartWindow : Window
    {
        public TankkaartWindow()
        {
            InitializeComponent();
            FilterButton.IsEnabled = false;
            VoegToeButton.IsEnabled = false;
            WijzigButton.IsEnabled = false;
            VerwijderButton.IsEnabled = false;
        }

        private void TankkaartWindow_Closing(object sender, EventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}