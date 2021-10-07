using System;
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
        private readonly List<string> Merken = new();
        #endregion

        #region Constructors

        public Voertuig(string merkEnModel, string chassisNummer, string nummerPlaat, Brandstoftype brandstofType, Typewagen typeWagen, string kleur, int aantalDeuren, Bestuurder bestuurder) {
            MerkEnModel = merkEnModel;
            ChassisNummer = chassisNummer;
            NummerPlaat = nummerPlaat;
            BrandstofType = brandstofType;
            TypeWagen = typeWagen;
            Kleur = kleur;
            AantalDeuren = aantalDeuren;
            Bestuurder = bestuurder;
        }

        public Voertuig(string merkEnModel, string chassisNummer, string nummerPlaat, Brandstoftype brandstofType, Typewagen typeWagen) {
            MerkEnModel = merkEnModel;
            ChassisNummer = chassisNummer;
            NummerPlaat = nummerPlaat;
            BrandstofType = brandstofType;
            TypeWagen = typeWagen;
        }

        public Voertuig(string merkEnModel, string chassisNummer, string nummerPlaat, Brandstoftype brandstofType, Typewagen typeWagen, string kleur) : this(merkEnModel, chassisNummer, nummerPlaat, brandstofType, typeWagen) {
            Kleur = kleur;
        }

        public Voertuig(string merkEnModel, string chassisNummer, string nummerPlaat, Brandstoftype brandstofType, Typewagen typeWagen, string kleur, int aantalDeuren) : this(merkEnModel, chassisNummer, nummerPlaat, brandstofType, typeWagen, kleur) {
            AantalDeuren = aantalDeuren;
        }

        public Voertuig(string merkEnModel, string chassisNummer, string nummerPlaat, Brandstoftype brandstofType, Typewagen typeWagen, string kleur, Bestuurder bestuurder) : this(merkEnModel, chassisNummer, nummerPlaat, brandstofType, typeWagen, kleur) {
            Bestuurder = bestuurder;
        }

        public Voertuig(string merkEnModel, string chassisNummer, string nummerPlaat, Brandstoftype brandstofType, Typewagen typeWagen, int aantalDeuren, Bestuurder bestuurder) : this(merkEnModel, chassisNummer, nummerPlaat, brandstofType, typeWagen) {
            AantalDeuren = aantalDeuren;
            Bestuurder = bestuurder;
        }




        #endregion

        #region Methods
        #endregion
    }
}
