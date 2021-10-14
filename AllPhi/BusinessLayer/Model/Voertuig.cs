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
        private readonly List<string> MerkenEnModellen = new();
        #endregion

        #region Constructors
        public Voertuig(string merkEnModel, string chassisNummer, string nummerPlaat, Brandstoftype brandstofType, Typewagen typeWagen, string kleur) {
            ZetMerkEnModel(merkEnModel);
            ZetChassisNummer(chassisNummer);
            ZetNummerPlaat(nummerPlaat);
            BrandstofType = brandstofType;
            TypeWagen = typeWagen;
            ZetKleur(kleur);
        }
        public Voertuig(string merkEnModel, string chassisNummer, string nummerPlaat, Brandstoftype brandstofType, Typewagen typeWagen, int aantalDeuren) {
            ZetMerkEnModel(merkEnModel);
            ZetChassisNummer(chassisNummer);
            ZetNummerPlaat(nummerPlaat);
            BrandstofType = brandstofType;
            TypeWagen = typeWagen;
            ZetAantalDeuren(aantalDeuren);
        }
        public Voertuig(string merkEnModel, string chassisNummer, string nummerPlaat, Brandstoftype brandstofType, Typewagen typeWagen, Bestuurder bestuurder) {
            ZetMerkEnModel(merkEnModel);
            ZetChassisNummer(chassisNummer);
            ZetNummerPlaat(nummerPlaat);
            BrandstofType = brandstofType;
            TypeWagen = typeWagen;
            Bestuurder = bestuurder;
        }
        public Voertuig(string merkEnModel, string chassisNummer, string nummerPlaat, Brandstoftype brandstofType, Typewagen typeWagen, string kleur, int aantalDeuren) : this(merkEnModel, chassisNummer, nummerPlaat, brandstofType, typeWagen, kleur) {
            ZetAantalDeuren(aantalDeuren);
        }
        public Voertuig(string merkEnModel, string chassisNummer, string nummerPlaat, Brandstoftype brandstofType, Typewagen typeWagen, string kleur, Bestuurder bestuurder) : this(merkEnModel, chassisNummer, nummerPlaat, brandstofType, typeWagen, kleur) {
            Bestuurder = bestuurder;
        }
        public Voertuig(string merkEnModel, string chassisNummer, string nummerPlaat, Brandstoftype brandstofType, Typewagen typeWagen, int aantalDeuren, Bestuurder bestuurder) : this(merkEnModel, chassisNummer, nummerPlaat, brandstofType, typeWagen, aantalDeuren) {
            Bestuurder = bestuurder;
        }
        public Voertuig(string merkEnModel, string chassisNummer, string nummerPlaat, Brandstoftype brandstofType, Typewagen typeWagen)
        {
            MerkEnModel = merkEnModel;
            ChassisNummer = chassisNummer;
            NummerPlaat = nummerPlaat;
            BrandstofType = brandstofType;
            TypeWagen = typeWagen;
        }

        #endregion

        #region Methods
        //refactor nodig voor controle op nummerplaat, kleur moet in orde
        public void ZetBestuurder(Bestuurder bestuurder) {
            Bestuurder = bestuurder;
            Bestuurder.ZetVoertuig(this);
        }
        public void ZetKleur(string kleur) {
            if(string.IsNullOrEmpty(kleur)) throw new VoertuigException("kleur mag niet leeg zijn");
            this.Kleur = kleur;
        }
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
        public void ZetAantalDeuren(int aantalDeuren) {
            if(aantalDeuren < 3) throw new VoertuigException("een auto heeft minstens 3 deuren");
            this.AantalDeuren = aantalDeuren;
        }
        public void ZetNummerPlaat(string nummerplaat) {

        }
        #endregion
    }
}
