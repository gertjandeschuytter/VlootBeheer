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
        public int Postcode;
        public string? Huisnummer;
        public int? BestuurderID;
        public List<TypeRijbewijs> Rijbewijzen = new();

        public BestuurderWindow()
        {
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            WijzigBestuurderButton.IsEnabled = false;
            VerwijderButton.IsEnabled = false;
            DatagridBestuurder.SelectedItem = null;
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
        private bool zijnAdresVeldenIngevuld() {
            if (VoegToe_StraatTextbox.Text.Length > 0 || VoegToe_PostcodeTextbox.Text.Length > 0 || VoegToe_HuisnummerTextbox.Text.Length > 0 || VoegToe_StadTextbox.Text.Length > 0) {
                if (VoegToe_StraatTextbox.Text.Length > 0 && VoegToe_PostcodeTextbox.Text.Length > 0 && VoegToe_HuisnummerTextbox.Text.Length > 0 && VoegToe_StadTextbox.Text.Length > 0) {
                }
                return true;
            }
            return false;
        }
        private void VoegToeButton_Click(object sender, RoutedEventArgs e)
        {
            Adres a = null;
            try {
                if (ZijnAlleVeldenIngevuld()) {
                    Rijbewijzen.Clear();
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
                    if (zijnAdresVeldenIngevuld()) {
                        if (string.IsNullOrWhiteSpace(VoegToe_StraatTextbox.Text))
                            Straat = null;
                        else
                            Straat = VoegToe_StraatTextbox.Text;
                        if (string.IsNullOrWhiteSpace(VoegToe_StadTextbox.Text))
                            Stad = null;
                        else
                            Stad = VoegToe_StadTextbox.Text;

                        int output;
                        if (int.TryParse(VoegToe_PostcodeTextbox.Text, out output))
                            Postcode = output;
                        else
                            MessageBox.Show("mag niet null zijn");

                        if (string.IsNullOrWhiteSpace(VoegToe_HuisnummerTextbox.Text))
                            Huisnummer = null;
                        else
                            Huisnummer = VoegToe_HuisnummerTextbox.Text;
                        a = new Adres(Straat, Stad, Postcode, Huisnummer);
                    }
                    Voornaam = VoegToe_VoornaamTextbox.Text;
                    Naam = VoegToe_NaamTextbox.Text;
                    GeboorteDatum = VoegToe_GeboortedatumDatePicker.SelectedDate;
                    Rijksregisternummer = VoegToe_RijksregisternummerTextbox.Text;
                    Bestuurder bestuurder = new(Naam, Voornaam, (DateTime)GeboorteDatum, Rijksregisternummer, Rijbewijzen);
                    bestuurder.ZetAdres(a);
                    MainWindow.bestuurderManager.VoegBestuurderToe(bestuurder);
                    MessageBox.Show("De bestuurder werd succesvol toegevoegd!");
                } else {
                    MessageBox.Show("alle velden met * moeten ingevuld worden");
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message); 
            }
        }

        private void DatagridBestuurder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            VerwijderButton.IsEnabled = true;
            WijzigBestuurderButton.IsEnabled = true;
            
        }

        private void VerwijderButton_Click(object sender, RoutedEventArgs e)
        {
            try {
                BusinessLayer.Model.Bestuurder bestuurder = (BusinessLayer.Model.Bestuurder)DatagridBestuurder.SelectedItem;
                if (MessageBox.Show("Ben je zeker dat je deze bestuurder wil verwijderen?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    MainWindow.bestuurderManager.VerwijderBestuurder(bestuurder);
                    Application.Current.Properties["Bestuurder"] = null;
                    FilterButton_Click(sender, e);
                }        
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void GaNaarWijzigScherm(object sender, RoutedEventArgs e)
        {
            if ((BusinessLayer.Model.Bestuurder)DatagridBestuurder.SelectedItem == null)
            {
                MessageBox.Show("U heeft geen bestuurder gekozen");
            }
            else
            {
                Application.Current.Properties["Bestuurder"] = (BusinessLayer.Model.Bestuurder)DatagridBestuurder.SelectedItem;
                WijzigenBestuurderWindow wtw = new();
                wtw.ShowDialog();
                FilterButton_Click(sender, e);
            }
        }
        private bool ZijnAlleVeldenIngevuld() {
            if (VoegToe_VoornaamTextbox.Text.Length > 0 && VoegToe_NaamTextbox.Text.Length > 0 && VoegToe_GeboortedatumDatePicker.SelectedDate.HasValue && VoegToe_RijksregisternummerTextbox.Text.Length > 0) {
                if ((bool)VoegToe_RijbewijsCheckbox_AM.IsChecked || (bool)VoegToe_RijbewijsCheckbox_A.IsChecked || (bool)VoegToe_RijbewijsCheckbox_A1.IsChecked ||
                    (bool)VoegToe_RijbewijsCheckbox_A2.IsChecked || (bool)VoegToe_RijbewijsCheckbox_B.IsChecked || (bool)VoegToe_RijbewijsCheckbox_BE.IsChecked ||
                    (bool)VoegToe_RijbewijsCheckbox_C.IsChecked || (bool)VoegToe_RijbewijsCheckbox_CE.IsChecked || (bool)VoegToe_RijbewijsCheckbox_D.IsChecked ||
                    (bool)VoegToe_RijbewijsCheckbox_DE.IsChecked || (bool)VoegToe_RijbewijsCheckbox_D1.IsChecked || (bool)VoegToe_RijbewijsCheckbox_D1E.IsChecked ||
                    (bool)VoegToe_RijbewijsCheckbox_T.IsChecked) {
                    return true;
                }
                return false;
            }
            return false;
        }

        private void DatagridBestuurder_Loaded(object sender, RoutedEventArgs e)
        {

        }
        //private void VoegToe_VoornaamTextbox_TextChanged(object sender, TextChangedEventArgs e) {
        //    if (ZijnAlleVeldenIngevuld()) VoegToeButton.IsEnabled = true;
        //    else VoegToeButton.IsEnabled = false;
        //}
        //private void VoegToe_NaamTextbox_TextChanged(object sender, TextChangedEventArgs e) {
        //    if (ZijnAlleVeldenIngevuld()) VoegToeButton.IsEnabled = true;
        //    else VoegToeButton.IsEnabled = false;

        //}
        //private void VoegToe_RijksregisternummerTextbox_TextChanged(object sender, TextChangedEventArgs e) {
        //    if (ZijnAlleVeldenIngevuld()) VoegToeButton.IsEnabled = true;
        //    else VoegToeButton.IsEnabled = false;

        //}
        //private void VoegToe_GeboortedatumDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
        //    if (ZijnAlleVeldenIngevuld()) VoegToeButton.IsEnabled = true;
        //    else VoegToeButton.IsEnabled = false;

        //}
        //private void VoegToe_RijbewijsCheckbox_AM_Checked(object sender, RoutedEventArgs e) {
        //    if (ZijnAlleVeldenIngevuld()) VoegToeButton.IsEnabled = true;
        //    else VoegToeButton.IsEnabled = false;

        //}
        //private void VoegToe_RijbewijsCheckbox_A_Checked(object sender, RoutedEventArgs e) {
        //    if (ZijnAlleVeldenIngevuld()) VoegToeButton.IsEnabled = true;
        //    else VoegToeButton.IsEnabled = false;

        //}
        //private void VoegToe_RijbewijsCheckbox_A1_Checked(object sender, RoutedEventArgs e) {
        //    if (ZijnAlleVeldenIngevuld()) VoegToeButton.IsEnabled = true;
        //    else VoegToeButton.IsEnabled = false;

        //}
        //private void VoegToe_RijbewijsCheckbox_A2_Checked(object sender, RoutedEventArgs e) {
        //    if (ZijnAlleVeldenIngevuld()) VoegToeButton.IsEnabled = true;
        //    else VoegToeButton.IsEnabled = false;

        //}
        //private void VoegToe_RijbewijsCheckbox_B_Checked(object sender, RoutedEventArgs e) {
        //    if (ZijnAlleVeldenIngevuld()) VoegToeButton.IsEnabled = true;
        //    else VoegToeButton.IsEnabled = false;

        //}
        //private void VoegToe_RijbewijsCheckbox_BE_Checked(object sender, RoutedEventArgs e) {
        //    if (ZijnAlleVeldenIngevuld()) VoegToeButton.IsEnabled = true;
        //    else VoegToeButton.IsEnabled = false;

        //}
        //private void VoegToe_RijbewijsCheckbox_C_Checked(object sender, RoutedEventArgs e) {
        //    if (ZijnAlleVeldenIngevuld()) VoegToeButton.IsEnabled = true;
        //    else VoegToeButton.IsEnabled = false;

        //}
        //private void VoegToe_RijbewijsCheckbox_CE_Checked(object sender, RoutedEventArgs e) {
        //    if (ZijnAlleVeldenIngevuld()) VoegToeButton.IsEnabled = true;
        //    else VoegToeButton.IsEnabled = false;

        //}
        //private void VoegToe_RijbewijsCheckbox_D_Checked(object sender, RoutedEventArgs e) {
        //    if (ZijnAlleVeldenIngevuld()) VoegToeButton.IsEnabled = true;
        //    else VoegToeButton.IsEnabled = false;

        //}
        //private void VoegToe_RijbewijsCheckbox_DE_Checked(object sender, RoutedEventArgs e) {
        //    if (ZijnAlleVeldenIngevuld()) VoegToeButton.IsEnabled = true;
        //    else VoegToeButton.IsEnabled = false;

        //}
        //private void VoegToe_RijbewijsCheckbox_D1_Checked(object sender, RoutedEventArgs e) {
        //    if (ZijnAlleVeldenIngevuld()) VoegToeButton.IsEnabled = true;
        //    else VoegToeButton.IsEnabled = false;

        //}
        //private void VoegToe_RijbewijsCheckbox_D1E_Checked(object sender, RoutedEventArgs e) {
        //    if (ZijnAlleVeldenIngevuld()) VoegToeButton.IsEnabled = true;
        //    else VoegToeButton.IsEnabled = false;

        //}
        //private void VoegToe_RijbewijsCheckbox_T_Checked(object sender, RoutedEventArgs e) {
        //    if (ZijnAlleVeldenIngevuld()) VoegToeButton.IsEnabled = true;
        //    else VoegToeButton.IsEnabled = false;

        //}
    }
}