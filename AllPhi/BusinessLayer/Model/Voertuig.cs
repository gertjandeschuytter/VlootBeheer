using System;
using BusinessLayer.Exceptions;
using System.Collections.Generic;

namespace BusinessLayer.Model
{
    public class Voertuig
    {
        #region Properties
        public string MerkEnModel { get; private set; }
        public string ChassisNummer { get; private set; }
        public string NummerPlaat { get; private set; }
        public Brandstoftype BrandstofType { get; private set; }
        public Typewagen TypeWagen { get; private set; }
        public string Kleur { get; private set; }
        public int AantalDeuren { get; private set; }
        public Bestuurder Bestuurder { get; private set; }
        #endregion

        #region fields
        private readonly List<string> MerkenEnModellen = new List<string>();
        #endregion

        #region Constructors
        
        #endregion

        #region Methods
        public void ZetMerkEnModel (string merkEnModel) {
            if(string.IsNullOrEmpty(merkEnModel)) throw new VoertuigException("merk mag niet leeg of null zijn");
            this.MerkEnModel = merkEnModel;
            MerkenEnModellen.Add(merkEnModel);
        }
        public void ZetChassisNummer (string chassisNummer) {
            if(string.IsNullOrEmpty(ChassisNummer)) throw new VoertuigException("chassisnummer mag niet leeg of null zijn");
            else if(ChassisNummer.Length < 17) throw new VoertuigException("chassisnummer moet minstens 17 karakters bevatten");
            this.ChassisNummer = chassisNummer;
        }
        //nummerplaat methode nog maken
        public void ZetKleur(string kleur) {
            if(string.IsNullOrEmpty(kleur)) throw new VoertuigException("kleur mag niet leeg of null zijn");
            this.Kleur = kleur;
        }
        public void ZetAantalDeuren(int aantalDeuren) {
            if(aantalDeuren < 3) throw new VoertuigException("een auto heeft minstens 3 deuren");
            this.AantalDeuren = aantalDeuren;
        }

        #endregion
    }
}
