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

namespace WpfFleetManagement.Tankkaart
{
    /// <summary>
    /// Interaction logic for TankkaartWindow.xaml
    /// </summary>
    public partial class TankkaartWindow : Window
    {
        private Bestuurder bestuurder = (Bestuurder)Application.Current.Properties["bestuurder"];
        public string Kaartnummer;
        public string Pincode;
        public Brandstoftype_tankkaart? Brandstoftype;
        public int? TankkaartId;
        public bool Geblokkeerd;
        public DateTime? Geldigheidsdatum;
        public Bestuurder Bestuurder;
        public TankkaartWindow()
        {
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            VerwijderButton.IsEnabled = false;
        }

        private bool ZijnAlleVeldenIngevuld()
        {
            if (VoegToe_KaartnummerTextbox.Text.Length > 0 && VoegToe_GeldigheidsdatumDatePicker.SelectedDate != DateTime.MinValue)
            {
                return true;
            }
            return false;
        }

        private void VoegToeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ZijnAlleVeldenIngevuld())
                {

                    Kaartnummer = VoegToe_KaartnummerTextbox.Text;
                    if (string.IsNullOrWhiteSpace(VoegToe_PincodeTextbox.Text))
                        Pincode = null;
                    else
                        Pincode = VoegToe_PincodeTextbox.Text;
                    if (VoegToe_BrandstofTypeCombobox.SelectedItem.ToString() == "<geen brandstoftype>")
                        Brandstoftype = null;
                    else
                        Brandstoftype = (Brandstoftype_tankkaart)Enum.Parse(typeof(Brandstoftype_tankkaart), VoegToe_BrandstofTypeCombobox.SelectedItem.ToString());
                    Geblokkeerd = (bool)VoegToe_IsGeblokkeerdCheckbox.IsChecked;

                    Geldigheidsdatum = VoegToe_GeldigheidsdatumDatePicker.SelectedDate;

                    BusinessLayer.Model.TankKaart t = new(Kaartnummer, (DateTime)Geldigheidsdatum, Pincode, Bestuurder, Geblokkeerd, Brandstoftype);
                    MainWindow.tankkaartManager.VoegTankkaartToe(t);
                    MessageBox.Show("Het voertuig werd succesvol toegevoegd!");
                }
                else
                {
                    MessageBox.Show("alle velden met * moeten ingevuld worden");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void KiesBestuurderButton_Click(object sender, RoutedEventArgs e)
        {
            ZoekBestuurderTankkaartWindow zb = new();
            if(zb.ShowDialog() == true)
            {
                Bestuurder = zb.Bestuurder;
                TextBoxGekozenBestuurder.Text = Bestuurder.ToString();
            }
        }

        private void WijzigButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["Tankkaart"] = (BusinessLayer.Model.TankKaart)TankkaartDatagrid.SelectedItem;
            WijzigTankkaartWindow wtw = new();
            wtw.ShowDialog();
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Aanpassen_KaartnummerTextbox.Text))
                Kaartnummer = null;
            else
                Kaartnummer = Aanpassen_KaartnummerTextbox.Text;
            if (string.IsNullOrWhiteSpace(Aanpassen_PincodeTextbox.Text))
                Pincode = null;
            else
                Pincode = Aanpassen_PincodeTextbox.Text;
            if (string.IsNullOrWhiteSpace(Aanpassen_TankkaartIdTextbox.Text))
                TankkaartId = null;
            else
                TankkaartId = int.Parse(Aanpassen_TankkaartIdTextbox.Text);
            if (Aanpassen_IsGeblokkeerdCheckbox.IsChecked == true)
                Geblokkeerd = true;
            else
                Geblokkeerd = false;
            if (Aanpassen_BrandstoftypeCombobox.SelectedItem.ToString() == "<geen brandstoftype>")
                Brandstoftype = null;
            else
                Brandstoftype = (Brandstoftype_tankkaart)Enum.Parse(typeof(Brandstoftype_tankkaart), Aanpassen_BrandstoftypeCombobox.SelectedItem.ToString());
            if (Aanpassen_GeldigheidsdatumDatePicker.SelectedDate == null)
                Geldigheidsdatum = null;
            else
                Geldigheidsdatum = Aanpassen_GeldigheidsdatumDatePicker.SelectedDate;

            IReadOnlyList<TankKaart> tankkaarten = MainWindow.tankkaartManager.ZoekTankkaarten(TankkaartId, Kaartnummer, Geldigheidsdatum, Pincode, Brandstoftype, Geblokkeerd);

            ObservableCollection<TankKaart> t = new();
            foreach (TankKaart tankkaart in tankkaarten)
            {
                t.Add(tankkaart);
            }

            TankkaartDatagrid.ItemsSource = t;
        }
        private void VerwijderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BusinessLayer.Model.TankKaart tankkaart = (BusinessLayer.Model.TankKaart)TankkaartDatagrid.SelectedItem;
                MainWindow.tankkaartManager.VerwijderTankkaart(tankkaart);
                MessageBox.Show("Tankkaart is verwijderd", Title, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Aanpassen_BrandstoftypeCombobox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> brandstofTypes_tankkaart = Enum.GetNames(typeof(Brandstoftype_tankkaart)).ToList();
            brandstofTypes_tankkaart.Insert(0, "<geen brandstoftype>");
            Aanpassen_BrandstoftypeCombobox.ItemsSource = brandstofTypes_tankkaart;
            Aanpassen_BrandstoftypeCombobox.SelectedIndex = 0;
        }

        private void TankkaartDatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VerwijderButton.IsEnabled = true;
            WijzigButton.IsEnabled = true;
        }

        private void VoegToe_BrandstofTypeCombobox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> brandstoftypes = Enum.GetNames(typeof(Brandstoftype_tankkaart)).ToList();
            brandstoftypes.Insert(0, "<geen brandstoftype>");
            VoegToe_BrandstofTypeCombobox.ItemsSource = brandstoftypes;
            VoegToe_BrandstofTypeCombobox.SelectedIndex = 0;
        }
    }
}
