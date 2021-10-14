using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Exceptions;
using BusinessLayer.Utilities;

namespace BusinessLayer.Model
{
    public class Bestuurder
    {
        #region Constructors
        public Bestuurder(string naam, string voorNaam, Adres adres, DateTime geboorteDatum, long rijksRegisterNr, Voertuig voertuig, TankKaart tankKaart) : this(naam, voorNaam, geboorteDatum, rijksRegisterNr)
        {
            ZetVoertuig(voertuig);
            ZetTankKaart(tankKaart);
            Adres = adres;
        }

        public Bestuurder(string naam, string voorNaam, Adres adres, DateTime geboorteDatum, long rijksRegisterNr, Voertuig voertuig) : this(naam, voorNaam, geboorteDatum, rijksRegisterNr)
        {
            ZetVoertuig(voertuig);
            Adres = adres;
        }

        public Bestuurder(string naam, string voorNaam, DateTime geboorteDatum, long rijksRegisterNr, Voertuig voertuig, TankKaart tankKaart) : this(naam, voorNaam, geboorteDatum, rijksRegisterNr)
        {
            Voertuig = voertuig;
            TankKaart = tankKaart;
        }

        public Bestuurder(string naam, string voorNaam, Adres adres, DateTime geboorteDatum, long rijksRegisterNr, TankKaart tankKaart) : this(naam, voorNaam, geboorteDatum, rijksRegisterNr)
        {
            ZetTankKaart(tankKaart);
            Adres = adres;
        }

        public Bestuurder(string naam, string voorNaam, Adres adres, DateTime geboorteDatum, long rijksRegisterNr) : this(naam, voorNaam, geboorteDatum, rijksRegisterNr)
        {
            Adres = adres;
        }

        public Bestuurder(string naam, string voorNaam, DateTime geboorteDatum, long rijksRegisterNr, Voertuig voertuig) : this(naam, voorNaam, geboorteDatum, rijksRegisterNr)
        {
            Voertuig = voertuig;
        }

        public Bestuurder(string naam, string voorNaam, DateTime geboorteDatum, long rijksRegisterNr, TankKaart tankKaart) : this(naam, voorNaam, geboorteDatum, rijksRegisterNr)
        {
            TankKaart = tankKaart;
        }

        public Bestuurder(string naam, string voorNaam, DateTime geboorteDatum, long rijksRegisterNr)
        {
            ZetNaam(naam);
            ZetVoorNaam(voorNaam);
            ZetGeboorteDatum(geboorteDatum);
            ZetRijksRegisterNummer(rijksRegisterNr);
            Types = new List<TypeRijbewijs>();
        }
        #endregion

        #region Properties
        public string Naam { get; private set; }
        public string VoorNaam { get; private set; }
        public Adres Adres { get; }
        public DateTime GeboorteDatum { get; private set; }
        public long RijksRegisterNr { get; private set; }
        public List<TypeRijbewijs> Types { get; private set; }
        public Voertuig Voertuig { get; private set; }
        public TankKaart TankKaart { get; private set; }
        #endregion

        #region Methods

        #region Setters
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

        public void ZetRijksRegisterNummer(long rijksRegisterNr)
        {
            if (RijksRegisterNummerValidator.ContorleerEmpty(rijksRegisterNr)) throw new BestuurderException("Bestuurder: ZetRijksRegisterNummer - invalid rijksregisternummer: rijksregisternummer mag niet leeg zijn.");
            if (!RijksRegisterNummerValidator.ControleerLengte(rijksRegisterNr)) throw new BestuurderException("Bestuurder: ZetRijksRegisterNummer - invalid rijksregisternummer: rijksregisternummer moet 11 cijfers lang zijn.");
            if (!RijksRegisterNummerValidator.ControleerEerste6Cijfers(this, rijksRegisterNr)) throw new BestuurderException("Bestuurder: ZetRijksRegisterNummer - invalid rijksregisternummer: De eerste 6 cijfers moeten dezelfde zijn als de geboortedatum");
            if (!RijksRegisterNummerValidator.ControleerLaatste2Cijfers(this, rijksRegisterNr)) throw new BestuurderException("Bestuurder: ZetRijksRegisterNummer - invalid rijksregisternummer: De laatste 2 cijfers zijn een controle voor de andere 9");

            RijksRegisterNr = rijksRegisterNr;
        }
        #endregion

        public void VoegRijbewijsToe(TypeRijbewijs type)
        {
            if (Types.Contains(type)) throw new BestuurderException("Bestuurder: VoegRijbewijsToe - Deze bestuurder heeft dit type rijbewijs al");
            Types.Add(type);
        }

        public void VerwijderRijbewijs(TypeRijbewijs type)
        {
            if (!Types.Contains(type)) throw new BestuurderException("Bestuurder: VoegRijbewijsToe - Deze bestuurder heeft dit rijbewijs niet.");
            Types.Remove(type);
        }

        public void ZetVoertuig(Voertuig voertuig)
        {
            if (Voertuig == voertuig) throw new BestuurderException("Bestuurder: ZetVoertuig - Dit voertuig is al ingesteld voor deze bestuurder.");
            Voertuig = voertuig;
        }

        public void VerwijderVoertuig(Voertuig voertuig)
        {
            if (Voertuig != voertuig) throw new BestuurderException("Bestuurder: VerwijderVoertuig - Dit voertuig is niet het voertuig van deze bestuurder.");
            Voertuig = null;
        }

        public void ZetTankKaart(TankKaart tankKaart)
        {
            if (TankKaart == tankKaart) throw new BestuurderException("Bestuurder: ZetTankKaart - Deze tankkaart is al ingesteld voor deze bestuurder.");
            TankKaart = tankKaart;
            TankKaart.ZetBestuurder(this);
        }
        public void VerwijderTankKaart(TankKaart tankKaart)
        {
            if (TankKaart != tankKaart) throw new BestuurderException("Bestuurder: VerwijderTankKaart - Deze tankkaart is niet van deze bestuurder.");

            TankKaart = null;
        }
        #endregion

    }
}