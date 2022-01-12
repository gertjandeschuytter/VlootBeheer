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
        private BusinessLayer.Model.Voertuig voertuig = (BusinessLayer.Model.Voertuig)Application.Current.Properties["Voertuig"];

        private int? VoertuigId;
        private string? Merk;
        private string? Model;
        private string? Chassisnummer;
        private string? Nummerplaat;
        private string? Kleur;
        private int? AantalDeuren;
        private Brandstoftype_voertuig? Brandstoftype;
        private Typewagen? wagenType;

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

        private bool ZijnAlleVeldenIngevuld()
        {
            if (VoegToe_MerkTextbox.Text.Length > 0 && VoegToe_ModelTextbox.Text.Length > 0 && VoegToe_ChassisnummerTextbox.Text.Length > 0 && VoegToe_NummerplaatTextbox.Text.Length > 0 && VoegToe_BrandstoftypeCombobox.SelectedIndex != 0 && VoegToe_TypewagenCombobox.SelectedIndex != 0)
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
                    Merk = VoegToe_MerkTextbox.Text;
                    Model = VoegToe_ModelTextbox.Text;
                    Chassisnummer = VoegToe_ChassisnummerTextbox.Text;
                    Nummerplaat = VoegToe_NummerplaatTextbox.Text;
                    Kleur = VoegToe_KleurTextbox.Text;
                    int output;
                    if(int.TryParse(VoegToe_AantalDeurenTextbox.Text, out output)) AantalDeuren = output;

                    switch (VoegToe_BrandstoftypeCombobox.SelectedIndex)
                    {
                        case 0:
                            Brandstoftype = Brandstoftype_voertuig.Benzine;
                            break;
                        case 1:
                            Brandstoftype = Brandstoftype_voertuig.Diesel;
                            break;
                        case 2:
                            Brandstoftype = Brandstoftype_voertuig.Hybride_Benzine;
                            break;
                        case 3:
                            Brandstoftype = Brandstoftype_voertuig.Hybride_Diesel;
                            break;
                        case 4:
                            Brandstoftype = Brandstoftype_voertuig.Elektrisch;
                            break;
                    }

                    switch (VoegToe_TypewagenCombobox.SelectedIndex)
                    {
                        case 0:
                            wagenType = Typewagen.Personenwagen;
                            break;

                        case 1:
                            wagenType = Typewagen.Bestelwagen;
                            break;

                        case 2:
                            wagenType = Typewagen.Sportwagen;
                            break;
                    }

                    BusinessLayer.Model.Voertuig v = new(Merk, Model, Chassisnummer, Nummerplaat, (Brandstoftype_voertuig)Brandstoftype, (Typewagen)wagenType, bestuurder);
                    v.ZetAantalDeuren((int)AantalDeuren);
                    v.ZetKleur((string)Kleur);
                    MainWindow.voertuigManager.VoegVoertuigToe(v);
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

        private void WijzigButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["Voertuig"] = (BusinessLayer.Model.Voertuig)VoertuigDatagrid.SelectedItem;
            WijzigVoertuigWindow wvw = new();
            wvw.ShowDialog();
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

            if ((string)Aanpassen_BrandstoftypeCombobox.SelectedItem == "<geen brandstofTypes>")
                Brandstoftype = null;
            else
                Brandstoftype = (Brandstoftype_voertuig)Aanpassen_BrandstoftypeCombobox.SelectedItem;

            if ((string)Aanpassen_TypewagenCombobox.SelectedItem == "<geen wagentype>")
                wagenType = null;
            else
                wagenType = (Typewagen)Aanpassen_TypewagenCombobox.SelectedItem;

            IReadOnlyList<BusinessLayer.Model.Voertuig> voertuigen = MainWindow.voertuigManager.GeefVoertuig(VoertuigId, Merk, Model, Chassisnummer, Nummerplaat, Kleur, AantalDeuren, Brandstoftype.ToString(), wagenType.ToString());

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
            wagentypes.Insert(0, "Personenwagen");
            wagentypes.Insert(1, "Bestelwagen");
            wagentypes.Insert(2, "Sportwagen");
            VoegToe_TypewagenCombobox.ItemsSource = wagentypes;
        }

        private void VoegToe_BrandstoftypeCombobox_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<string> brandstofTypes = new();
            brandstofTypes.Insert(0, "Benzine");
            brandstofTypes.Insert(1, "Diesel");
            brandstofTypes.Insert(2, "Hybride_Benzine");
            brandstofTypes.Insert(3, "Hybride_Diesel");
            brandstofTypes.Insert(4, "Elektrisch");
            VoegToe_BrandstoftypeCombobox.ItemsSource = brandstofTypes;
        }

        private void Aanpassen_BrandstoftypeCombobox_Loaded(object sender, RoutedEventArgs e)
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

        private void Aanpassen_TypewagenCombobox_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<string> wagentypes = new();
            wagentypes.Insert(0, "<geen wagentype>");
            wagentypes.Insert(1, "Personenwagen");
            wagentypes.Insert(2, "Bestelwagen");
            wagentypes.Insert(3, "Sportwagen");
            Aanpassen_TypewagenCombobox.ItemsSource = wagentypes;
            Aanpassen_TypewagenCombobox.SelectedIndex = 0;
        }

        private void VerwijderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BusinessLayer.Model.Voertuig voertuig = (BusinessLayer.Model.Voertuig)VoertuigDatagrid.SelectedItem;
                MainWindow.voertuigManager.VerwijderVoertuig(voertuig);
                MessageBox.Show("Voertuig is verwijderd", Title, MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Properties["Voertuig"] = null;
                FilterButton_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
