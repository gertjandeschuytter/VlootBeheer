using System;
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

namespace WpfFleetManagement.Tankkaart
{
    /// <summary>
    /// Interaction logic for TankkaartWindow.xaml
    /// </summary>
    public partial class TankkaartWindow : Window
    {
        public TankkaartWindow()
        {
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            VoegToeButton.IsEnabled = false;
            VerwijderButton.IsEnabled = false;
        }

        private void VoegToeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void KiesBestuurderButton_Click(object sender, RoutedEventArgs e)
        {
            ZoekBestuurderTankkaartWindow zb = new();
            zb.Show();
            Close();
        }

        private void WijzigButton_Click(object sender, RoutedEventArgs e)
        {
            WijzigTankkaartWindow wtw = new();
            wtw.Show();
            Close();
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void VerwijderButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
