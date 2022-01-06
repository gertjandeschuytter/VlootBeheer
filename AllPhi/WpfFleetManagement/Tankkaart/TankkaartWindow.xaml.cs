﻿using BusinessLayer.Model;
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
        public string Kaartnummer;
        public string Pincode;
        public Brandstoftype_tankkaart? Brandstoftype;
        public int? TankkaartId;
        public bool Geblokkeerd;
        public DateTime? Geldigheidsdatum;
        public TankkaartWindow()
        {
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            VoegToeButton.IsEnabled = false;
            VerwijderButton.IsEnabled = false;
        }

        private void VoegToeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void KiesBestuurderButton_Click(object sender, RoutedEventArgs e)
        {
            ZoekBestuurderTankkaartWindow zb = new();
            zb.Show();
            Close();
        }

        private void WijzigButton_Click(object sender, RoutedEventArgs e)
        {
            WijzigTankkaartWindow wtw = new();
            wtw.Show();
            Close();
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

        }

        private void Aanpassen_BrandstoftypeCombobox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> maten = Enum.GetNames(typeof(Brandstoftype_tankkaart)).ToList();
            maten.Insert(0, "<geen brandstoftype>");
            Aanpassen_BrandstoftypeCombobox.ItemsSource = maten;
            Aanpassen_BrandstoftypeCombobox.SelectedIndex = 0;
        }
    }
}
