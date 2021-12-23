using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfFleetManagement.Voertuig;

namespace WpfFleetManagement.Tankkaart
{
    /// <summary>
    /// Interaction logic for ZoekBestuurderTankkaartWindow.xaml
    /// </summary>
    public partial class ZoekBestuurderTankkaartWindow : Window
    {
        public string Voornaam;
        public string Naam;
        public DateTime? GeboorteDatum;
        public string Rijksregisternummer;
        public string Straat;
        public string Stad;
        public int Postcode;
        public string? Huisnummer;
        public int? BestuurderID;
        public List<TypeRijbewijs> Rijbewijzen = new();

        public ZoekBestuurderTankkaartWindow()
        {
            InitializeComponent();
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Filter_VoornaamTextbox.Text))
                Voornaam = null;
            else
                Voornaam = Filter_VoornaamTextbox.Text;
            if (string.IsNullOrWhiteSpace(Filter_NaamTextbox.Text))
                Naam = null;
            else
                Naam = Filter_NaamTextbox.Text;
            if (Filter_GeboortedatumDatePicker.SelectedDate == null)
                GeboorteDatum = null;
            else
                GeboorteDatum = Filter_GeboortedatumDatePicker.SelectedDate;
            if (string.IsNullOrWhiteSpace(Filter_RijksregisternummerTextbox.Text))
                Rijksregisternummer = null;
            else
                Rijksregisternummer = Filter_RijksregisternummerTextbox.Text;
            //if (string.IsNullOrWhiteSpace(Filter_StraatTextbox.Text))
            //    Straat = null;
            //else
            //    Straat = Filter_StraatTextbox.Text;
            //if (string.IsNullOrWhiteSpace(Filter_StadTextbox.Text))
            //    Stad = null;
            //else
            //    Stad = Filter_StadTextbox.Text;

            //int output;
            //if (!int.TryParse(Filter_PostcodeTextbox.Text, out output))
            //    Postcode = null;
            //else
            //    Postcode = output;

            //if (!int.TryParse(Filter_HuisnummerTextbox.Text, out output))
            //    Huisnummer = null;
            //else
            //    Huisnummer = output;
            int output;
            if (!int.TryParse(Filter_BestuurderIdTextbox.Text, out output))
                BestuurderID = null;
            else
                BestuurderID = output;

            IReadOnlyList<Bestuurder> bestuurders = MainWindow.bestuurderManager.GeefBestuurder(BestuurderID, Naam, Voornaam, GeboorteDatum, Rijksregisternummer);

            ObservableCollection<Bestuurder> b = new();
            foreach (Bestuurder bestuurder in bestuurders)
            {
                b.Add(bestuurder);
            }

            DatagridBestuurder.ItemsSource = b;

        }

        private void DatagridBestuurder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KiesBestuurderButton.IsEnabled = true;

        }

        private void KiesBestuurderButton_Click(object sender, RoutedEventArgs e)
        {
            if (DatagridBestuurder.SelectedItem != null)
            {
                Application.Current.Properties["Bestuurder"] = (Bestuurder)DatagridBestuurder.SelectedItem;
                VoertuigWindow vw = new();
                vw.Show();
                vw.TabVoegToe.Focus();
                this.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TankkaartWindow tw = new();
            tw.Show();
        }
    }
}
