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
        public Bestuurder(string naam, string voorNaam, Adres adres, DateTime geboorteDatum, long rijksRegisterNr, TypeRijbewijs type, Voertuig voertuig, TankKaart tankKaart) : this(naam, voorNaam, geboorteDatum, rijksRegisterNr, type)
        {
            Adres = adres;
            Voertuig = voertuig;
            TankKaart = tankKaart;
        }

        public Bestuurder(string naam, string voorNaam, DateTime geboorteDatum, long rijksRegisterNr, TypeRijbewijs type)
        {
            ZetNaam(naam);
            ZetVoorNaam(voorNaam);
            ZetGeboorteDatum(geboorteDatum);
            ZetRijksRegisterNummer(rijksRegisterNr);
            ZetRijbewijs(type);
        }

        public Bestuurder(string naam, string voorNaam, Adres adres, DateTime geboorteDatum, long rijksRegisterNr, TypeRijbewijs type) : this(naam, voorNaam, geboorteDatum, rijksRegisterNr, type)
        {
            Adres = adres;
        }

        public Bestuurder(string naam, string voorNaam, DateTime geboorteDatum, long rijksRegisterNr, TypeRijbewijs type, Voertuig voertuig) : this(naam, voorNaam, geboorteDatum, rijksRegisterNr, type)
        {

            Voertuig = voertuig;
        }

        public Bestuurder(string naam, string voorNaam, DateTime geboorteDatum, long rijksRegisterNr, TypeRijbewijs type, TankKaart tankKaart) : this(naam, voorNaam, geboorteDatum, rijksRegisterNr, type)
        {

            TankKaart = tankKaart;
        }

        public Bestuurder(string naam, string voorNaam, Adres adres, DateTime geboorteDatum, long rijksRegisterNr, TypeRijbewijs type, Voertuig voertuig) : this(naam, voorNaam, geboorteDatum, rijksRegisterNr, type)
        {

            Adres = adres;
            Voertuig = voertuig;
        }

        public Bestuurder(string naam, string voorNaam, DateTime geboorteDatum, long rijksRegisterNr, TypeRijbewijs type, Voertuig voertuig, TankKaart tankKaart) : this(naam, voorNaam, geboorteDatum, rijksRegisterNr, type)
        {

            Voertuig = voertuig;
            TankKaart = tankKaart;
        }

        public Bestuurder(string naam, string voorNaam, Adres adres, DateTime geboorteDatum, long rijksRegisterNr, TypeRijbewijs type, TankKaart tankKaart) : this(naam, voorNaam, geboorteDatum, rijksRegisterNr, type)
        {

            Adres = adres;
            TankKaart = tankKaart;
        }
        #endregion

        #region Properties
        public string Naam { get; private set; }
        public string VoorNaam { get; private set; }
        public Adres Adres { get; }
        public DateTime GeboorteDatum { get; private set; }
        public long RijksRegisterNr { get; private set; }
        public TypeRijbewijs Type { get; private set; }
        public Voertuig Voertuig { get; }
        public TankKaart TankKaart { get; }
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
            if (!RijksRegisterNummerValidator.ControleerEerste6Cijfers(this, rijksRegisterNr)) throw new BestuurderException("Bestuurder: ZetRijksRegisterNummer - invalid rijksregisternummer: De eerste 6 cijfers moeten dezelfde zijn als de geboortedatum komen");
            if (!RijksRegisterNummerValidator.ControleerLaatste2Cijfers(this, rijksRegisterNr)) throw new BestuurderException("Bestuurder: ZetRijksRegisterNummer - invalid rijksregisternummer: De laatste 2 cijfers zijn een controle voor de andere 9");
            
            RijksRegisterNr = rijksRegisterNr;
        }

        public void ZetRijbewijs(TypeRijbewijs type)
        {
            if (!Enum.IsDefined(type)) throw new BestuurderException("Bestuurder: ZetRijbewijs - invalid type rijbewijs: rijbewijs moet ingevuld worden");
            Type = type;
        }
        #endregion


        #endregion

    }
}
