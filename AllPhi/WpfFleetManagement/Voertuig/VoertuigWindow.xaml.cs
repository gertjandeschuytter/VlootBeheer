using BusinessLayer.Model;
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

namespace WpfFleetManagement.Voertuig {
    /// <summary>
    /// Interaction logic for VoertuigWindow.xaml
    /// </summary>
    public partial class VoertuigWindow : Window {
        private Bestuurder bestuurder = (Bestuurder)Application.Current.Properties["Bestuurder"];

        public VoertuigWindow()
        {
            InitializeComponent();
            if (bestuurder != null)
            {
                TextBoxGekozenBestuurder.Text = bestuurder.ToString();
            }
        }

        private void KiesBestuurderButton_Click(object sender, RoutedEventArgs e)
        {
            ZoekBestuurderVoertuigWindow zb = new();
            zb.Show();
            this.Close();
        }

        private void VoegToeButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
