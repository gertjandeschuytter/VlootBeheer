using System;
using BusinessLayer.Exceptions;
using System.Collections.Generic;
using BusinessLayer.Utilities;
using System.Linq;

namespace BusinessLayer.Model {
    public class Voertuig {
        #region Properties
        public int ID { get; private set; }
        public string Merk { get; private set; }
        public string Model { get; private set; }
        public string ChassisNummer { get; private set; }
        public string NummerPlaat { get; private set; }
        public Brandstoftype_voertuig BrandstofType { get; private set; }
        public Typewagen TypeWagen { get; private set; }
        public string Kleur { get; private set; }
        public int AantalDeuren { get; private set; }
        public Bestuurder Bestuurder { get; private set; }
        #endregion

        #region Constructors
        public Voertuig(string merk, string model, string chassisNummer, string nummerPlaat, Brandstoftype_voertuig brandstofType, Typewagen typeWagen) {
            ZetMerk(merk);
            ZetModel(model);
            ZetChassisNummer(chassisNummer);
            ZetNummerPlaat(nummerPlaat);
            BrandstofType = brandstofType;
            TypeWagen = typeWagen;
        }
        public Voertuig(string merk, string model, string chassisNummer, string nummerPlaat, Brandstoftype_voertuig brandstofType, Typewagen typeWagen, Bestuurder bestuurder) {
            ZetMerk(merk);
            ZetModel(model);
            ZetChassisNummer(chassisNummer);
            ZetNummerPlaat(nummerPlaat);
            BrandstofType = brandstofType;
            TypeWagen = typeWagen;
            Bestuurder = bestuurder;
        }
        public Voertuig(string merk, string model, string chassisNummer, string nummerPlaat, Brandstoftype_voertuig brandstofType, Typewagen typeWagen, string kleur, int aantalDeuren) : this(merk, model, chassisNummer, nummerPlaat, brandstofType, typeWagen) {
            ZetAantalDeuren(aantalDeuren);
            ZetKleur(kleur);
        }
        public Voertuig(string merk, string model, string chassisNummer, string nummerPlaat, Brandstoftype_voertuig brandstofType, Typewagen typeWagen, string kleur, Bestuurder bestuurder) : this(merk, model, chassisNummer, nummerPlaat, brandstofType, typeWagen, bestuurder) {
            ZetKleur(kleur);
        }

        public Voertuig(string merk, string model, string chassisNummer, string nummerPlaat, Brandstoftype_voertuig brandstofType, Typewagen typeWagen, int aantalDeuren, Bestuurder bestuurder) : this(merk, model, chassisNummer, nummerPlaat, brandstofType, typeWagen) {
            Bestuurder = bestuurder;

            ZetAantalDeuren(aantalDeuren);
        }


        #endregion

        #region Methods
        public void ZetId(int id) {
            if (id < 1) throw new VoertuigException("id mag niet kleiner zijn dan 0");
            this.ID = id;
        }
        public void ZetBestuurder(Bestuurder bestuurder) {
            if (bestuurder == null) throw new VoertuigException("Bestuurder mag niet null zijn");
            if (Bestuurder == bestuurder) throw new VoertuigException("Deze bestuurder is al ingesteld voor dit voertuig.");
            if (Bestuurder != null)
                if (Bestuurder.HeeftVoertuig(this))
                    Bestuurder.VerwijderVoertuig(this);
            Bestuurder = bestuurder;
            if (bestuurder.Voertuig != this)
                bestuurder.ZetVoertuig(this);
        }

        public void ZetKleur(string kleur) {
            if (kleur == null) {
                this.Kleur = null;
            } else if (string.IsNullOrEmpty(kleur)) {
                throw new VoertuigException("Kleur mag niet leeg zijn");
            } else {
                this.Kleur = kleur;
            }
        }
        public void ZetMerk(string merk) {
            if (string.IsNullOrWhiteSpace(merk)) throw new VoertuigException("merk mag niet leeg of null zijn");
            this.Merk = merk;
        }
        public void ZetModel(string model) {
            if (string.IsNullOrWhiteSpace(model)) throw new VoertuigException("model mag niet leeg of null zijn");
            this.Model = model;
        }
        public void ZetChassisNummer(string chassisNummer) {
            if (string.IsNullOrEmpty(chassisNummer)) throw new VoertuigException("chassisnummer mag niet leeg of null zijn");
            if (chassisNummer.Length != 17) throw new VoertuigException("chassisnummer moet 17 karakters bevatten");
            if (ChassisnummerValidator.BevatChassisnummerSpeciaalKarakter(chassisNummer)) throw new VoertuigException("voertuig mag geen speciale karakters bevatten");
            this.ChassisNummer = chassisNummer;
        }
        public void ZetAantalDeuren(int aantalDeuren) {
            if (aantalDeuren < 3) throw new VoertuigException("een auto heeft minstens 3 deuren");
            this.AantalDeuren = aantalDeuren;
        }
        public void ZetNummerPlaat(string nummerplaat) {
            if (!NummerplaatValidator.IsLengteCorrect(nummerplaat)) throw new VoertuigException("een nummerplaat mag maximum uit 7 karakters bestaan");
            if (!NummerplaatValidator.IsEersteCijferCorrect(nummerplaat)) throw new VoertuigException("Eerste nummer moet lager zijn dan 3");
            if (!NummerplaatValidator.IsTweedeDeelCorrect(nummerplaat)) throw new VoertuigException("2de deel van het nummerplaat mag enkel uit letters bestaan");
            if (!NummerplaatValidator.IsDerdeDeelCorrect(nummerplaat)) throw new VoertuigException("3de deel van het nummerplaat mag enkel uit cijfers bestaan");
            this.NummerPlaat = nummerplaat;
        }

        internal bool HeeftBestuurder(Bestuurder bestuurder) {
            if (Bestuurder == bestuurder) return true;
            return false;
        }

        internal void VerwijderBestuurder(Bestuurder bestuurder) {
            if (Bestuurder != bestuurder) throw new VoertuigException("Voertuig: VerwijderBestuurder - deze bestuurder is niet ingesteld voor dit voertuig");
            Bestuurder = null;
        }

        #endregion
    }
}
