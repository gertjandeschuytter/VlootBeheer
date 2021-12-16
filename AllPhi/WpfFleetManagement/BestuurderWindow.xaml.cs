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

namespace WpfFleetManagement
{
    /// <summary>
    /// Interaction logic for BestuurderWindow.xaml
    /// </summary>
    public partial class BestuurderWindow : Window
    {
        public string Voornaam;
        public string Naam;
        public DateTime? GeboorteDatum;
        public string Rijksregisternummer;
        public string Straat;
        public string Stad;
        public int? Postcode;
        public int? Huisnummer;
        public int? BestuurderID;
        public List<TypeRijbewijs> Rijbewijzen = new();

        public BestuurderWindow()
        {
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            VoegToeButton.IsEnabled = false;
            WijzigBestuurderButton.IsEnabled = false;
            VerwijderButton.IsEnabled = false;
        }

        private void BestuurderWindow_Closing(object sender, EventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
        }

        private void FilterButton_Click(object sender, EventArgs e)
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
            foreach(Bestuurder bestuurder in bestuurders)
            {
                b.Add(bestuurder);
            }

            DatagridBestuurder.ItemsSource = b;
        }

        private void VoegToeButton_Click(object sender, RoutedEventArgs e)
        {
            Voornaam = VoegToe_VoornaamTextbox.Text;
            Naam = VoegToe_NaamTextbox.Text;
            GeboorteDatum = VoegToe_GeboortedatumDatePicker.SelectedDate;
            Rijksregisternummer = VoegToe_RijksregisternummerTextbox.Text;
            if ((bool)VoegToe_RijbewijsCheckbox_A.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.A);
            if ((bool)VoegToe_RijbewijsCheckbox_A1.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.A1);
            if ((bool)VoegToe_RijbewijsCheckbox_A2.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.A2);
            if ((bool)VoegToe_RijbewijsCheckbox_AM.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.AM);
            if ((bool)VoegToe_RijbewijsCheckbox_B.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.B);
            if ((bool)VoegToe_RijbewijsCheckbox_BE.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.BE);
            if ((bool)VoegToe_RijbewijsCheckbox_C.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.C);
            if ((bool)VoegToe_RijbewijsCheckbox_CE.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.CE);
            if ((bool)VoegToe_RijbewijsCheckbox_D.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.D);
            if ((bool)VoegToe_RijbewijsCheckbox_DE.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.DE);
            if ((bool)VoegToe_RijbewijsCheckbox_D1.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.D1);
            if ((bool)VoegToe_RijbewijsCheckbox_D1E.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.D1E);
            if ((bool)VoegToe_RijbewijsCheckbox_T.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.T);

            Bestuurder bestuurder = new(Voornaam, Naam, (DateTime)GeboorteDatum, Rijksregisternummer, Rijbewijzen);

            MainWindow.bestuurderManager.VoegBestuurderToe(bestuurder);
        }

        private void DatagridBestuurder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            VerwijderButton.IsEnabled = true;
            WijzigBestuurderButton.IsEnabled = true;
            
        }

        private void VerwijderButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["Bestuurder"] = (BusinessLayer.Model.Bestuurder)DatagridBestuurder.CurrentItem;
        }

        private void GaNaarWijzigScherm(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["Bestuurder"] = (BusinessLayer.Model.Bestuurder)DatagridBestuurder.CurrentItem;
            WijzigenBestuurderWindow window = new();
            window.ShowDialog();
        }
    }
}