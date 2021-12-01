using System;
using System.Collections.Generic;
using BusinessLayer.Exceptions;
using BusinessLayer.Utilities;

namespace BusinessLayer.Model
{
    public class Bestuurder
    {
        #region Constructors
        public Bestuurder(string naam, string voorNaam, Adres adres, DateTime geboorteDatum, string rijksRegisterNr, Voertuig voertuig, TankKaart tankKaart, List<TypeRijbewijs> types) : this(naam, voorNaam, adres, geboorteDatum, rijksRegisterNr, types)
        {
            ZetVoertuig(voertuig);
            ZetTankKaart(tankKaart);
        }

        public Bestuurder(string naam, string voorNaam, Adres adres, DateTime geboorteDatum, string rijksRegisterNr, Voertuig voertuig, List<TypeRijbewijs> types) : this(naam, voorNaam,adres, geboorteDatum, rijksRegisterNr, types)
        {
            ZetVoertuig(voertuig);
        }

        public Bestuurder(string naam, string voorNaam, DateTime geboorteDatum, string rijksRegisterNr, Voertuig voertuig, TankKaart tankKaart, List<TypeRijbewijs> types) : this(naam, voorNaam, geboorteDatum, rijksRegisterNr, types)
        {
            ZetVoertuig(voertuig);
            ZetTankKaart(tankKaart);
        }

        public Bestuurder(string naam, string voorNaam, Adres adres, DateTime geboorteDatum, string rijksRegisterNr, TankKaart tankKaart, List<TypeRijbewijs> types) : this(naam, voorNaam,adres, geboorteDatum, rijksRegisterNr, types)
        {
            ZetTankKaart(tankKaart);
        }

        public Bestuurder(string naam, string voorNaam, Adres adres, DateTime geboorteDatum, string rijksRegisterNr, List<TypeRijbewijs> types) : this(naam, voorNaam, geboorteDatum, rijksRegisterNr, types)
        {
            ZetAdres(adres);
        }

        public Bestuurder(string naam, string voorNaam, DateTime geboorteDatum, string rijksRegisterNr, List<TypeRijbewijs> types, Voertuig voertuig) : this(naam, voorNaam, geboorteDatum, rijksRegisterNr, types)
        {
            ZetVoertuig(voertuig);
        }

        public Bestuurder(string naam, string voorNaam, DateTime geboorteDatum, string rijksRegisterNr, List<TypeRijbewijs> types, TankKaart tankKaart) : this(naam, voorNaam, geboorteDatum, rijksRegisterNr, types)
        {
            ZetTankKaart(tankKaart);
        }

        public Bestuurder(string naam, string voorNaam, DateTime geboorteDatum, string rijksRegisterNr, List<TypeRijbewijs> types)
        {
            ZetNaam(naam);
            ZetVoorNaam(voorNaam);
            ZetGeboorteDatum(geboorteDatum);
            ZetRijksRegisterNummer(rijksRegisterNr);
            foreach(var type in types)
            {
                VoegRijbewijsToe(type);
            }
        }
        #endregion

        #region Properties
        public int ID { get; private set; }
        public string Naam { get; private set; }
        public string VoorNaam { get; private set; }
        public Adres Adres { get; private set; }
        public DateTime GeboorteDatum { get; private set; }
        public string RijksRegisterNr { get; private set; }
        public Voertuig Voertuig { get; private set; }
        public TankKaart TankKaart { get; private set; }

        public List<TypeRijbewijs> _Types { get; private set; } = new List<TypeRijbewijs>();
        #endregion

        #region Methods

        #region Setters

        public void ZetID(int ID)
        {
            if (ID <= 0) throw new BestuurderException("Bestuurder: ZetID - ID mag niet 0 of kleiner zijn.");
            this.ID = ID;
        }
        public void ZetNaam(string naam)
        {
            if (string.IsNullOrWhiteSpace(naam)) throw new BestuurderException("Bestuurder: ZetNaam - invalid naam: naam mag niet leeg zijn");
            Naam = naam;
        }

        public void ZetVoorNaam(string voorNaam)
        {
            if (string.IsNullOrWhiteSpace(voorNaam)) throw new BestuurderException("Bestuurder: ZetVoorNaam - invalid voornaam: voornaam mag niet leeg zijn");
            VoorNaam = voorNaam;
        }

        public void ZetGeboorteDatum(DateTime geboorte)
        {
            DateTime standaard = new();
            if (geboorte == standaard) throw new BestuurderException("Bestuurder: ZetGeboorteDatum - invalid geboortedatum: geboortedatum mag niet null/ongeldig zijn");
            GeboorteDatum = geboorte;
        }

        public void ZetRijksRegisterNummer(string rijksRegisterNr)
        {
            if (RijksRegisterNummerValidator.ContorleerEmpty(rijksRegisterNr)) throw new BestuurderException("Bestuurder: ZetRijksRegisterNummer - invalid rijksregisternummer: rijksregisternummer mag niet leeg zijn.");
            if (!RijksRegisterNummerValidator.ControleerLengte(rijksRegisterNr)) throw new BestuurderException("Bestuurder: ZetRijksRegisterNummer - invalid rijksregisternummer: rijksregisternummer moet 11 cijfers lang zijn.");
            if (!RijksRegisterNummerValidator.ControleerEerste6Cijfers(this, rijksRegisterNr)) throw new BestuurderException("Bestuurder: ZetRijksRegisterNummer - invalid rijksregisternummer: De eerste 6 cijfers moeten dezelfde zijn als de geboortedatum");
            if (!RijksRegisterNummerValidator.ControleerLaatste2Cijfers(this, rijksRegisterNr)) throw new BestuurderException("Bestuurder: ZetRijksRegisterNummer - invalid rijksregisternummer: De laatste 2 cijfers zijn een controle voor de andere 9");

            RijksRegisterNr = rijksRegisterNr;
        }

        public void ZetAdres(Adres adres)
        {
            Adres = adres ?? throw new BestuurderException("Bestuurder: ZetAdres - Adres mag niet null zijn.");
        }

        public void ZetVoertuig(Voertuig voertuig)
        {
            if (voertuig == null) throw new BestuurderException("Bestuurder: ZetVoertuig - voertuig mag niet null zijn");
            if (Voertuig == voertuig) throw new BestuurderException("Bestuurder: ZetVoertuig - Dit voertuig is al ingesteld voor deze bestuurder.");
            if (Voertuig != null)
                if (Voertuig.HeeftBestuurder(this))
                    Voertuig.VerwijderBestuurder(this);
            Voertuig = voertuig;
            if (!voertuig.HeeftBestuurder(this))
                voertuig.ZetBestuurder(this);
        }

        public void ZetTankKaart(TankKaart tankKaart)
        {
            if (tankKaart == null) throw new BestuurderException("Bestuurder: ZetTankKaart - tankkaart mag niet null zijn.");
            if (TankKaart == tankKaart) throw new BestuurderException("Bestuurder: ZetTankKaart - tankkaart is al ingesteld");
            if (TankKaart != null)
                if (TankKaart.HeeftBestuurder(this))
                    TankKaart.VerwijderBestuurder(this);
            TankKaart = tankKaart;
            if (tankKaart.Bestuurder != this)
                tankKaart.ZetBestuurder(this);
        }
        #endregion

        public void VoegRijbewijsToe(TypeRijbewijs type)
        {
            if (_Types.Contains(type)) throw new RijbewijsLijstException("RijbewijsLijst: VoegRijbewijsToe - Bestuurder heeft dit rijbewijs al.");
            _Types.Add(type);
        }

        public void VerwijderRijbewijs(TypeRijbewijs type)
        {
            if (!_Types.Contains(type)) throw new RijbewijsLijstException("RijbewijsLijst: VerwijderRijbewijs - Bestuurder heeft dit rijbewijs niet.");
            _Types.Remove(type);
        }

        public void VerwijderVoertuig(Voertuig voertuig)
        {
            if (Voertuig != voertuig) throw new BestuurderException("Bestuurder: VerwijderVoertuig - Dit voertuig is niet het voertuig van deze bestuurder.");
            Voertuig = null;
        }
        internal bool HeeftVoertuig(Voertuig voertuig)
        {
            if (Voertuig == voertuig) return true;
            return false;
        }

        public void VerwijderTankKaart(TankKaart tankKaart)
        {
            if (TankKaart != tankKaart) throw new BestuurderException("Bestuurder: VerwijderTankKaart - Deze tankkaart is niet van deze bestuurder.");
            TankKaart = null;
        }
        internal bool HeeftTankKaart(TankKaart tankKaart)
        {
            if (TankKaart == tankKaart) return true;
            return false;
        }
        #endregion

    }
}