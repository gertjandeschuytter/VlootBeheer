using System;
using BusinessLayer.Exceptions;
using System.Collections.Generic;
using BusinessLayer.Utilities;

namespace BusinessLayer.Model
{
    public class Voertuig
    {
        #region Properties
        public string Merk { get; private set; }
        public string Model { get; private set; }
        public string ChassisNummer { get; private set; }
        public string NummerPlaat { get; private set; }
        public Brandstoftype BrandstofType { get; private set; }
        public Typewagen TypeWagen { get; private set; }
        public string Kleur { get; private set; }
        public int AantalDeuren { get; private set; }
        public Bestuurder Bestuurder { get; private set; }
        #endregion

        #region Constructors
        public Voertuig(string merk, string model, string chassisNummer, string nummerPlaat, Brandstoftype brandstofType, Typewagen typeWagen, string kleur) {
            ZetMerk(merk);
            ZetModel(model);
            ZetChassisNummer(chassisNummer);
            ZetNummerPlaat(nummerPlaat);
            BrandstofType = brandstofType;
            TypeWagen = typeWagen;
            ZetKleur(kleur);
        }
        public Voertuig(string merk, string model, string chassisNummer, string nummerPlaat, Brandstoftype brandstofType, Typewagen typeWagen, int aantalDeuren) {
            ZetMerk(merk);
            ZetModel(model);
            ZetChassisNummer(chassisNummer);
            ZetNummerPlaat(nummerPlaat);
            BrandstofType = brandstofType;
            TypeWagen = typeWagen;
            ZetAantalDeuren(aantalDeuren);
        }
        public Voertuig(string merk, string model, string chassisNummer, string nummerPlaat, Brandstoftype brandstofType, Typewagen typeWagen, Bestuurder bestuurder) {
            ZetMerk(merk);
            ZetModel(model);
            ZetChassisNummer(chassisNummer);
            ZetNummerPlaat(nummerPlaat);
            BrandstofType = brandstofType;
            TypeWagen = typeWagen;
            Bestuurder = bestuurder;
        }
        public Voertuig(string merk, string model, string chassisNummer, string nummerPlaat, Brandstoftype brandstofType, Typewagen typeWagen, string kleur, int aantalDeuren) : this(merk, model, chassisNummer, nummerPlaat, brandstofType, typeWagen, kleur) {
            ZetAantalDeuren(aantalDeuren);
        }
        public Voertuig(string merk, string model, string chassisNummer, string nummerPlaat, Brandstoftype brandstofType, Typewagen typeWagen, string kleur, Bestuurder bestuurder) : this(merk, model, chassisNummer, nummerPlaat, brandstofType, typeWagen, kleur) {
            Bestuurder = bestuurder;
        }
        public Voertuig(string merk, string model, string chassisNummer, string nummerPlaat, Brandstoftype brandstofType, Typewagen typeWagen, int aantalDeuren, Bestuurder bestuurder) : this(merk, model, chassisNummer, nummerPlaat, brandstofType, typeWagen, aantalDeuren) {
            Bestuurder = bestuurder;
        }
        public Voertuig(string merk, string model, string chassisNummer, string nummerPlaat, Brandstoftype brandstofType, Typewagen typeWagen)
        {
            Merk = merk;
            Model = model;
            ChassisNummer = chassisNummer;
            NummerPlaat = nummerPlaat;
            BrandstofType = brandstofType;
            TypeWagen = typeWagen;
        }

        #endregion

        #region Methods
        public void ZetBestuurder(Bestuurder bestuurder) {
            Bestuurder = bestuurder;
            Bestuurder.ZetVoertuig(this);
        }
        public void ZetKleur(string kleur) {
            if(string.IsNullOrEmpty(kleur)) throw new VoertuigException("kleur mag niet leeg zijn");
            this.Kleur = kleur;
        }
        public void ZetMerk (string merk) {
            if(string.IsNullOrEmpty(merk)) throw new VoertuigException("merk mag niet leeg of null zijn");
            this.Merk = merk;
        }
        public void ZetModel(string model) {
            if (string.IsNullOrEmpty(model)) throw new VoertuigException("model mag niet leeg of null zijn");
            this.Model = model;
        }
        public void ZetChassisNummer (string chassisNummer) {
            if(string.IsNullOrEmpty(ChassisNummer)) throw new VoertuigException("chassisnummer mag niet leeg of null zijn");
            if(ChassisNummer.Length < 17) throw new VoertuigException("chassisnummer moet minstens 17 karakters bevatten");
            if (ChassisnummerValidator.BevatChassisnummerSpeciaalKarakter(chassisNummer)) throw new VoertuigException("voertuig mag geen speciale karakters bevatten");
            this.ChassisNummer = chassisNummer;
        }
        public void ZetAantalDeuren(int aantalDeuren) {
            if(aantalDeuren < 3) throw new VoertuigException("een auto heeft minstens 3 deuren");
            this.AantalDeuren = aantalDeuren;
        }
        public void ZetNummerPlaat(string nummerplaat) {
            if (!NummerplaatValidator.ControleerLengte(nummerplaat)) throw new VoertuigException("een nummerplaat mag maximum uit 7 karakters bestaan");
            if (!NummerplaatValidator.ControleerEersteCijfer(nummerplaat)) throw new VoertuigException("Eerste nummer moet lager zijn dan 3");
            if (!NummerplaatValidator.ControleerTweedeStuk(nummerplaat)) throw new VoertuigException("2de deel van het nummerplaat moeten letters zijn");
            if (!NummerplaatValidator.ControleerDerdeStuk(nummerplaat)) throw new VoertuigException("3de deel van het nummerplaat moeten cijfers zijn");
            this.NummerPlaat = nummerplaat;
        }
        #endregion
    }
}
