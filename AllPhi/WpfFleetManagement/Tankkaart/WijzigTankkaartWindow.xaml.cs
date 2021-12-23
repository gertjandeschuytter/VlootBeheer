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
    /// Interaction logic for WijzigTankkaartWindow.xaml
    /// </summary>
    public partial class WijzigTankkaartWindow : Window
    {
        public WijzigTankkaartWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TankkaartWindow tw = new();
            tw.Show();
        }

        private void WijzigButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
