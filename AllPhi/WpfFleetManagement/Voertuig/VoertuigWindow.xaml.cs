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
            this.ResizeMode = ResizeMode.NoResize;
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
        }
    }
}
