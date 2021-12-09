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

namespace WpfFleetManagement
{
    /// <summary>
    /// Interaction logic for VoertuigWindow.xaml
    /// </summary>
    public partial class VoertuigWindow : Window
    {
        public VoertuigWindow()
        {
            InitializeComponent();
        }

        private void VoertuigWindow_Closing(object sender, EventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}
