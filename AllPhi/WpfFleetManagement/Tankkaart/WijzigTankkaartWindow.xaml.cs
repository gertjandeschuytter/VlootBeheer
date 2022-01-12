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

namespace WpfFleetManagement.Tankkaart
{
    /// <summary>
    /// Interaction logic for WijzigTankkaartWindow.xaml
    /// </summary>
    public partial class WijzigTankkaartWindow : Window
    {

        private BusinessLayer.Model.TankKaart _tankkaart = (BusinessLayer.Model.TankKaart)Application.Current.Properties["Tankkaart"];
        private Bestuurder bestuurder = (Bestuurder)Application.Current.Properties["Bestuurder"];

        public string Kaartnummer;
        public string Pincode;
        public Brandstoftype_tankkaart? Brandstoftype;
        public int? TankkaartId;
        public bool Geblokkeerd;
        public DateTime? Geldigheidsdatum;
        public Bestuurder Bestuurder;
        public WijzigTankkaartWindow()
        {
            InitializeComponent();
            TextBoxKaartnummer.Text = _tankkaart.KaartNr;
            TextBoxPincode.Text = _tankkaart.Pincode;
            DatePickerGeldigheidsDatum.SelectedDate = _tankkaart.Geldigheidsdatum;
            if (_tankkaart.Geblokkeerd) CheckBoxGeblokkeerd.IsChecked = true;
            if (_tankkaart.Bestuurder != null) TextBoxBestuurder.Text = _tankkaart.Bestuurder.ToString();
            if (bestuurder != null)
            {
                TextBoxBestuurder.Text = bestuurder.ToString();
            }

            switch (_tankkaart.Brandstoftype)
            {
                case null:
                ComboBoxBrandstof.SelectedIndex = 0;
                break;
                case Brandstoftype_tankkaart.Benzine:
                ComboBoxBrandstof.SelectedIndex = 1;
                break;
                case Brandstoftype_tankkaart.Diesel:
                ComboBoxBrandstof.SelectedIndex = 2;
                break;
                case Brandstoftype_tankkaart.Elektrisch:
                ComboBoxBrandstof.SelectedIndex = 3;
                break;
                case Brandstoftype_tankkaart.Benzine_Elektrisch:
                ComboBoxBrandstof.SelectedIndex = 4;
                break;
                case Brandstoftype_tankkaart.Diesel_Elektrisch:
                ComboBoxBrandstof.SelectedIndex = 5;
                break;
            }

        }

        private void WijzigButton_Click(object sender, RoutedEventArgs e)
        {
            Kaartnummer = TextBoxKaartnummer.Text;
            Pincode = TextBoxPincode.Text;
            Geblokkeerd = (bool)CheckBoxGeblokkeerd.IsChecked;
            Geldigheidsdatum = (DateTime)DatePickerGeldigheidsDatum.SelectedDate;
            TankkaartId = _tankkaart.TankkaartId;

            switch (ComboBoxBrandstof.SelectedIndex)
            {
                case 0:
                Brandstoftype = null;
                break;
                case 1:
                Brandstoftype = Brandstoftype_tankkaart.Benzine;
                break;
                case 2:
                Brandstoftype = Brandstoftype_tankkaart.Diesel;
                break;
                case 3:
                Brandstoftype = Brandstoftype_tankkaart.Elektrisch;
                break;
                case 4:
                Brandstoftype = Brandstoftype_tankkaart.Benzine_Elektrisch;
                break;
                case 5:
                Brandstoftype = Brandstoftype_tankkaart.Diesel_Elektrisch;
                break;
            }

            if (_tankkaart.Bestuurder != null && bestuurder == null)
            {
                Bestuurder = _tankkaart.Bestuurder;
            }
            else
            {
                Bestuurder = bestuurder;
            }

            BusinessLayer.Model.TankKaart tk;
            try
            {
                tk = new(Kaartnummer,(DateTime)Geldigheidsdatum, Pincode,Bestuurder,Geblokkeerd,Brandstoftype);
                tk.ZetTankkaartId((int)TankkaartId);
                MainWindow.tankkaartManager.UpdateTankkaart(tk);
                MessageBox.Show("De tankkaart is gewijzigd");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Close();
        }

        private void BestuurderButton_Click(object sender, RoutedEventArgs e)
        {
            BestuurderWijzigenTankkaartWindow zb = new();
            zb.Show();
            Close();
        }

        private void ComboBoxBrandstof_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> brandstofTypes_tankkaart = Enum.GetNames(typeof(Brandstoftype_tankkaart)).ToList();
            brandstofTypes_tankkaart.Insert(0, "<geen brandstoftype>");
            ComboBoxBrandstof.ItemsSource = brandstofTypes_tankkaart;
        }
    }
}
