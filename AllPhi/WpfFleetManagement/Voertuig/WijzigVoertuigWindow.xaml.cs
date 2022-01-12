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

namespace WpfFleetManagement.Voertuig
{
    /// <summary>
    /// Interaction logic for WijzigVoertuigWindow.xaml
    /// </summary>
    public partial class WijzigVoertuigWindow : Window
    {
        private Bestuurder bestuurder = (Bestuurder)Application.Current.Properties["Bestuurder"];
        private BusinessLayer.Model.Voertuig voertuig = (BusinessLayer.Model.Voertuig)Application.Current.Properties["Voertuig"];

        public string Merk;
        public string Model;
        public string ChassisNummer;
        public string NummerPlaat;
        public Brandstoftype_voertuig BrandstofType;
        public Typewagen TypeWagen;
        public string Kleur;
        public int AantalDeuren;
        public Bestuurder Bestuurder;

        public WijzigVoertuigWindow()
        {
            InitializeComponent();

            MerkTextBox.Text = voertuig.Merk;
            ModelTextBox.Text = voertuig.Model;
            ChassisNummerTextBox.Text = voertuig.ChassisNummer;
            NummerplaatTextBox.Text = voertuig.NummerPlaat;
 
            switch (voertuig.BrandstofType)
            {
                case Brandstoftype_voertuig.Benzine:
                    BrandstofTypeComboBox.SelectedIndex = 1;
                    break;

                case Brandstoftype_voertuig.Diesel:
                    BrandstofTypeComboBox.SelectedIndex = 2;
                    break;

                case Brandstoftype_voertuig.Elektrisch:
                    BrandstofTypeComboBox.SelectedIndex = 5;
                    break;

                case Brandstoftype_voertuig.Hybride_Benzine:
                    BrandstofTypeComboBox.SelectedIndex = 3;
                    break;

                case Brandstoftype_voertuig.Hybride_Diesel:
                    BrandstofTypeComboBox.SelectedIndex = 4;
                    break;
            }

            switch (voertuig.TypeWagen)
            {
                case Typewagen.Bestelwagen:
                    WagenTypeCombobox.SelectedIndex = 2;
                    break;

                case Typewagen.Personenwagen:
                    WagenTypeCombobox.SelectedIndex = 1;
                    break;

                case Typewagen.Sportwagen:
                    WagenTypeCombobox.SelectedIndex = 3;
                    break;
            }

            if(!string.IsNullOrWhiteSpace(voertuig.Kleur)) KleurTextBox.Text = voertuig.Kleur;
            if (voertuig.AantalDeuren >= 3) DeurenTextBox.Text = voertuig.AantalDeuren.ToString();
            if (voertuig.Bestuurder != null)
            {
                BestuurderTextBox.Text = voertuig.Bestuurder.ToString();
                bestuurder = voertuig.Bestuurder;
            }
            else if (bestuurder != null)
            {
                BestuurderTextBox.Text = bestuurder.ToString();
            }
        }

        private void WijzigButton_Click(object sender, RoutedEventArgs e)
        {
            Merk = MerkTextBox.Text;
            Model = ModelTextBox.Text;
            ChassisNummer = ChassisNummerTextBox.Text;
            NummerPlaat = NummerplaatTextBox.Text;

            switch (BrandstofTypeComboBox.SelectedIndex)
            {
                case 1:
                    BrandstofType = Brandstoftype_voertuig.Benzine;
                    break;
                case 2:
                    BrandstofType = Brandstoftype_voertuig.Diesel;
                    break;
                case 3:
                    BrandstofType = Brandstoftype_voertuig.Hybride_Benzine;
                    break;
                case 4:
                    BrandstofType = Brandstoftype_voertuig.Hybride_Diesel;
                    break;
                case 5:
                    BrandstofType = Brandstoftype_voertuig.Elektrisch;
                    break;
            }

            switch (WagenTypeCombobox.SelectedIndex)
            {
                case 1:
                    TypeWagen = Typewagen.Personenwagen;
                    break;

                case 2:
                    TypeWagen = Typewagen.Bestelwagen;
                    break;

                case 3:
                    TypeWagen = Typewagen.Sportwagen;
                    break;
            }

            Kleur = KleurTextBox.Text;

            int output = 0;
            if (!string.IsNullOrWhiteSpace(DeurenTextBox.Text))
            {
                if (int.TryParse(DeurenTextBox.Text, out output))
                {
                        AantalDeuren = output;
                } else AantalDeuren = 0;
            }
            else AantalDeuren = 0;

            Bestuurder = bestuurder;

            BusinessLayer.Model.Voertuig v;
            try
            {
                v = new(Merk, Model, ChassisNummer, NummerPlaat, BrandstofType, TypeWagen);
                v.ZetId(voertuig.ID);
                if(Bestuurder != null) v.ZetBestuurder(Bestuurder);
                v.ZetKleur(Kleur);
                v.ZetAantalDeuren((int)AantalDeuren);

                MainWindow.voertuigManager.WijzigVoertuig(v);
                MessageBox.Show("Het voertuig is gewijzigd");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Close();
        }

        private void BrandstoftypeCombobox_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<string> brandstofTypes = new();
            brandstofTypes.Insert(0, "<geen brandstofType>");
            brandstofTypes.Insert(1, "Benzine");
            brandstofTypes.Insert(2, "Diesel");
            brandstofTypes.Insert(3, "Hybride_Benzine");
            brandstofTypes.Insert(4, "Hybride_Diesel");
            brandstofTypes.Insert(5, "Elektrisch");
            BrandstofTypeComboBox.ItemsSource = brandstofTypes;
        }

        private void VoegToe_TypewagenCombobox_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<string> wagentypes = new();
            wagentypes.Insert(0, "<geen wagentype>");
            wagentypes.Insert(1, "Personenwagen");
            wagentypes.Insert(2, "Bestelwagen");
            wagentypes.Insert(3, "Sportwagen");
            WagenTypeCombobox.ItemsSource = wagentypes;
        }

        private void BestuurderButton_Click(object sender, RoutedEventArgs e)
        {
            BestuurderWijzigenVoertuigWindow zb = new();
            zb.Show();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bestuurder = null;
            BestuurderTextBox.Text = "";
        }
    }
}
