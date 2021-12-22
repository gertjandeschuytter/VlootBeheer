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

namespace WpfFleetManagement {
    /// <summary>
    /// Interaction logic for WijzigenBestuurderWindow.xaml
    /// </summary>
    public partial class WijzigenBestuurderWindow : Window {

        private Bestuurder _bestuurder = (BusinessLayer.Model.Bestuurder)Application.Current.Properties["Bestuurder"];

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

        public WijzigenBestuurderWindow() {
            InitializeComponent();
            Wijzig_VoornaamTextbox.Text = _bestuurder.Voornaam;
            Wijzig_NaamTextbox.Text = _bestuurder.Naam;
            Wijzig_GeboortedatumDatePicker.SelectedDate = _bestuurder.Geboortedatum;
            Wijzig_RijksregisternummerTextbox.Text = _bestuurder.Rijksregisternummer;
            if (_bestuurder.Adres != null) {
                Straat_wijzigscherm.Text = _bestuurder.Adres.Straat;
                Stad_wijzigscherm.Text = _bestuurder.Adres.Stad;
                Postcode_wijzigscherm.Text = _bestuurder.Adres.Postcode.ToString();
                Huisnummer_wijzigscherm.Text = _bestuurder.Adres.Nummer;
            } 
            if (_bestuurder._Types.Contains(TypeRijbewijs.AM))
                Wijzig_RijbewijsCheckbox_AM.IsChecked = true;
            if (_bestuurder._Types.Contains(TypeRijbewijs.A))
                Wijzig_RijbewijsCheckbox_A.IsChecked = true;
            if (_bestuurder._Types.Contains(TypeRijbewijs.A1))
                Wijzig_RijbewijsCheckbox_A1.IsChecked = true;
            if (_bestuurder._Types.Contains(TypeRijbewijs.A2))
                Wijzig_RijbewijsCheckbox_A2.IsChecked = true;
            if (_bestuurder._Types.Contains(TypeRijbewijs.B))
                Wijzig_RijbewijsCheckbox_B.IsChecked = true;
            if (_bestuurder._Types.Contains(TypeRijbewijs.BE))
                Wijzig_RijbewijsCheckbox_BE.IsChecked = true;
            if (_bestuurder._Types.Contains(TypeRijbewijs.C))
                Wijzig_RijbewijsCheckbox_C.IsChecked = true;
            if (_bestuurder._Types.Contains(TypeRijbewijs.CE))
                Wijzig_RijbewijsCheckbox_CE.IsChecked = true;
            if (_bestuurder._Types.Contains(TypeRijbewijs.D))
                Wijzig_RijbewijsCheckbox_D.IsChecked = true;
            if (_bestuurder._Types.Contains(TypeRijbewijs.DE))
                Wijzig_RijbewijsCheckbox_DE.IsChecked = true;
            if (_bestuurder._Types.Contains(TypeRijbewijs.D1))
                Wijzig_RijbewijsCheckbox_D1.IsChecked = true;
            if (_bestuurder._Types.Contains(TypeRijbewijs.D1E))
                Wijzig_RijbewijsCheckbox_D1E.IsChecked = true;
            if (_bestuurder._Types.Contains(TypeRijbewijs.T))
                Wijzig_RijbewijsCheckbox_T.IsChecked = true;
        }

        private void WijzigButton_Click_1(object sender, RoutedEventArgs e) {
            Voornaam = Wijzig_VoornaamTextbox.Text;
            Naam = Wijzig_NaamTextbox.Text;
            GeboorteDatum = Wijzig_GeboortedatumDatePicker.SelectedDate;
            Rijksregisternummer = Wijzig_RijksregisternummerTextbox.Text;
            if ((bool)Wijzig_RijbewijsCheckbox_A.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.A);
            if ((bool)Wijzig_RijbewijsCheckbox_A1.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.A1);
            if ((bool)Wijzig_RijbewijsCheckbox_A2.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.A2);
            if ((bool)Wijzig_RijbewijsCheckbox_AM.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.AM);
            if ((bool)Wijzig_RijbewijsCheckbox_B.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.B);
            if ((bool)Wijzig_RijbewijsCheckbox_BE.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.BE);
            if ((bool)Wijzig_RijbewijsCheckbox_C.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.C);
            if ((bool)Wijzig_RijbewijsCheckbox_CE.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.CE);
            if ((bool)Wijzig_RijbewijsCheckbox_D.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.D);
            if ((bool)Wijzig_RijbewijsCheckbox_DE.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.DE);
            if ((bool)Wijzig_RijbewijsCheckbox_D1.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.D1);
            if ((bool)Wijzig_RijbewijsCheckbox_D1E.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.D1E);
            if ((bool)Wijzig_RijbewijsCheckbox_T.IsChecked)
                Rijbewijzen.Add(TypeRijbewijs.T);
            Bestuurder bestuurder = new(Naam, Voornaam, (DateTime)GeboorteDatum, Rijksregisternummer, Rijbewijzen);
            bestuurder.ZetID(_bestuurder.BestuurderId);
            MainWindow.bestuurderManager.WijzigBestuurder(bestuurder);
            MessageBox.Show("De bestuurder is gewijzigd");
            this.Close();
        }
    }
}
