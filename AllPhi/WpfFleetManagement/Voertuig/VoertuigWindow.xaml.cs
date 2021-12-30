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

namespace WpfFleetManagement.Voertuig {
    /// <summary>
    /// Interaction logic for VoertuigWindow.xaml
    /// </summary>
    public partial class VoertuigWindow : Window {
        private Bestuurder bestuurder = (Bestuurder)Application.Current.Properties["Bestuurder"];

        private int? VoertuigId;
        private string? Merk;
        private string? Model;
        private string? Chassisnummer;
        private string? Nummerplaat;
        private string? Kleur;
        private int? AantalDeuren;
        private string? Brandstoftype;
        private string? Typewagen;

        public VoertuigWindow()
        {
            ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            VerwijderButton.IsEnabled = false;
            WijzigButton.IsEnabled = false;
            if (bestuurder != null)
            {
                TextBoxGekozenBestuurder.Text = bestuurder.ToString();
            }
        }

        private void KiesBestuurderButton_Click(object sender, RoutedEventArgs e)
        {
            ZoekBestuurderVoertuigWindow zb = new();
            zb.Show();
            Close();
        }

        private void VoegToeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WijzigButton_Click(object sender, RoutedEventArgs e)
        {
            WijzigVoertuigWindow wvw = new();
            wvw.Show();
            Close();
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            int output;
            if (!int.TryParse(Aanpassen_VoertuigIdTextbox.Text, out output))
                VoertuigId = null;
            else
                VoertuigId = output;
            if (string.IsNullOrWhiteSpace(Aanpassen_MerkTextbox.Text))
                Merk = null;
            else
                Merk = Aanpassen_MerkTextbox.Text;
            if (string.IsNullOrWhiteSpace(Aanpassen_ModelTextbox.Text))
                Model = null;
            else
                Model = Aanpassen_ModelTextbox.Text;
            if (string.IsNullOrWhiteSpace(Aanpassen_ChassisnummerTextbox.Text))
                Chassisnummer = null;
            else
                Chassisnummer = Aanpassen_ChassisnummerTextbox.Text;
            if (string.IsNullOrWhiteSpace(Aanpassen_NummerplaatTextbox.Text))
                Nummerplaat = null;
            else
                Nummerplaat = Aanpassen_NummerplaatTextbox.Text;
            if (string.IsNullOrWhiteSpace(Aanpassen_KleurTextbox.Text))
                Kleur = null;
            else
                Kleur = Aanpassen_KleurTextbox.Text;

           
            if (!int.TryParse(Aanpassen_AantalDeurenTextbox.Text, out output))
                AantalDeuren = null;
            else
                AantalDeuren = output;

            if (Aanpassen_BrandstoftypeCombobox.SelectedItem == null)
                Brandstoftype = null;
            else
                Brandstoftype = Aanpassen_BrandstoftypeCombobox.SelectedItem.ToString();

            if (Aanpassen_TypewagenCombobox.SelectedItem == null)
                Typewagen = null;
            else
                Typewagen = Aanpassen_TypewagenCombobox.SelectedItem.ToString();

            IReadOnlyList<BusinessLayer.Model.Voertuig> voertuigen = MainWindow.voertuigManager.GeefVoertuig(VoertuigId, Merk, Model, Chassisnummer, Nummerplaat, Kleur, AantalDeuren, Brandstoftype, Typewagen);

            ObservableCollection<BusinessLayer.Model.Voertuig> v = new();
            foreach (BusinessLayer.Model.Voertuig voertuig in voertuigen)
            {
                v.Add(voertuig);
            }

            VoertuigDatagrid.ItemsSource = v;
            VerwijderButton.IsEnabled = false;
            WijzigButton.IsEnabled = false;
        }

        private void VoertuigDatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VerwijderButton.IsEnabled = true;
            WijzigButton.IsEnabled = true;
        }

        private void VoegToe_TypewagenCombobox_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<string> wagentypes = new();
            wagentypes.Insert(0, "<geen wagentype>");
            wagentypes.Insert(1, "Personenwagen");
            wagentypes.Insert(2, "Bestelwagen");
            wagentypes.Insert(3, "Sportwagen");
            Aanpassen_TypewagenCombobox.ItemsSource = wagentypes;
            Aanpassen_TypewagenCombobox.SelectedIndex = 0;
        }

        private void VoegToe_BrandstoftypeCombobox_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<string> brandstofTypes = new();
            brandstofTypes.Insert(0, "<geen brandstofTypes>");
            brandstofTypes.Insert(1, "Benzine");
            brandstofTypes.Insert(2, "Diesel");
            brandstofTypes.Insert(3, "Hybride_Benzine");
            brandstofTypes.Insert(4, "Hybride_Diesel");
            brandstofTypes.Insert(5, "Elektrisch");
            Aanpassen_BrandstoftypeCombobox.ItemsSource = brandstofTypes;
            Aanpassen_BrandstoftypeCombobox.SelectedIndex = 0;
        }
    }
}
