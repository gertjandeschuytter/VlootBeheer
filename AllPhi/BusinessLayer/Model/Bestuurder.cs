using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Exceptions;

namespace BusinessLayer.Model
{
    public class Bestuurder
    {
        #region Constructors
        public Bestuurder(string naam, string voorNaam, string adres, DateTime geboorteDatum, int rijksRegisterNr, TypeRijbewijs type, Voertuig voertuig, TankKaart tankKaart)
        {
            ZetNaam(naam);
            ZetVoorNaam(voorNaam);
            Adres = adres;
            ZetGeboorteDatum(geboorteDatum);
            ZetRijksRegisterNummer(rijksRegisterNr);
            ZetRijbewijs(type);
            Voertuig = voertuig;
            TankKaart = tankKaart;
        }

        public Bestuurder(string naam, string voorNaam, DateTime geboorteDatum, int rijksRegisterNr, TypeRijbewijs type)
        {
            ZetNaam(naam);
            ZetVoorNaam(voorNaam);
            ZetGeboorteDatum(geboorteDatum);
            ZetRijksRegisterNummer(rijksRegisterNr);
            ZetRijbewijs(type);
        }

        public Bestuurder(string naam, string voorNaam, string adres, DateTime geboorteDatum, int rijksRegisterNr, TypeRijbewijs type)
        {
            ZetNaam(naam);
            ZetVoorNaam(voorNaam);
            ZetGeboorteDatum(geboorteDatum);
            ZetRijksRegisterNummer(rijksRegisterNr);
            ZetRijbewijs(type);
            Adres = adres;
        }

        public Bestuurder(string naam, string voorNaam, DateTime geboorteDatum, int rijksRegisterNr, TypeRijbewijs type, Voertuig voertuig)
        {
            ZetNaam(naam);
            ZetVoorNaam(voorNaam);
            ZetGeboorteDatum(geboorteDatum);
            ZetRijksRegisterNummer(rijksRegisterNr);
            ZetRijbewijs(type);
            Voertuig = voertuig;
        }

        public Bestuurder(string naam, string voorNaam, DateTime geboorteDatum, int rijksRegisterNr, TypeRijbewijs type, TankKaart tankKaart)
        {
            ZetNaam(naam);
            ZetVoorNaam(voorNaam);
            ZetGeboorteDatum(geboorteDatum);
            ZetRijksRegisterNummer(rijksRegisterNr);
            ZetRijbewijs(type);
            TankKaart = tankKaart;
        }

        public Bestuurder(string naam, string voorNaam, string adres, DateTime geboorteDatum, int rijksRegisterNr, TypeRijbewijs type, Voertuig voertuig)
        {
            ZetNaam(naam);
            ZetVoorNaam(voorNaam);
            ZetGeboorteDatum(geboorteDatum);
            ZetRijksRegisterNummer(rijksRegisterNr);
            ZetRijbewijs(type);
            Adres = adres;
            Voertuig = voertuig;
        }

        public Bestuurder(string naam, string voorNaam, DateTime geboorteDatum, int rijksRegisterNr, TypeRijbewijs type, Voertuig voertuig, TankKaart tankKaart)
        {
            ZetNaam(naam);
            ZetVoorNaam(voorNaam);
            ZetGeboorteDatum(geboorteDatum);
            ZetRijksRegisterNummer(rijksRegisterNr);
            ZetRijbewijs(type);
            Voertuig = voertuig;
            TankKaart = tankKaart;
        }

        public Bestuurder(string naam, string voorNaam, string adres, DateTime geboorteDatum, int rijksRegisterNr, TypeRijbewijs type, TankKaart tankKaart)
        {
            ZetNaam(naam);
            ZetVoorNaam(voorNaam);
            ZetGeboorteDatum(geboorteDatum);
            ZetRijksRegisterNummer(rijksRegisterNr);
            ZetRijbewijs(type);
            Adres = adres;
            TankKaart = tankKaart;
        }
        #endregion

        #region Properties
        public string Naam { get; private set; }
        public string VoorNaam { get; private set; }
        public string Adres { get; }
        public DateTime GeboorteDatum { get; private set; }
        public int RijksRegisterNr { get; private set; }
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
            if () throw new BestuurderException("Bestuurder: ZetGeboorteDatum - invalid geboortedatum: geboortedatum mag niet null/ongeldig zijn");
            GeboorteDatum = geboorte;
        }

        public void ZetRijksRegisterNummer(int rijksRegisterNr)
        {
            if (rijksRegisterNr == 0) throw new BestuurderException("Bestuurder: ZetRijksRegisterNummer - invalid rijksregisternummer: rijksregisternummer mag niet leeg zijn");
            if (rijksRegisterNr.ToString().Length != 11) throw new BestuurderException("Bestuurder: ZetRijksRegisterNummer - invalid rijksregisternummer: rijksregisternummer moet 11 cijfers lang zijn.");
            if (!Controleer6Cijfers(rijksRegisterNr)) throw new BestuurderException("Bestuurder: ZetRijksRegisterNummer - invalid rijksregisternummer: De eerste 6 cijfers moeten dezelfde zijn als de geboortedatum komen");
            if (!ControleerLaatste2Cijfers(rijksRegisterNr)) throw new BestuurderException("Bestuurder: ZetRijksRegisterNummer - invalid rijksregisternummer: De laatste 2 cijfers zijn een controle voor de andere 9");
            
            RijksRegisterNr = rijksRegisterNr;
        }

        public void ZetRijbewijs(TypeRijbewijs type)
        {
            if () throw new BestuurderException("Bestuurder: ZetRijbewijs - invalid type rijbewijs: rijbewijs moet ingevuld worden");
            Type = type;
        }
        #endregion

        private bool Controleer6Cijfers(int rijksRegisterNr)
        {
            string cijfers6_Str = string.Empty;

            for(int i = 0; i < 6; i++)
            {
                cijfers6_Str += rijksRegisterNr.ToString()[i];
            }
            string geboorte = GeboorteDatum.ToString("yyMMdd");

            if (cijfers6_Str == geboorte) return true;
            return false;
        }

        private bool ControleerLaatste2Cijfers(int rijksRegisterNr)
        {
            int cijfers9;
            string cijfersLaatste2_str = string.Empty ;
            if (int.Parse(GeboorteDatum.ToString("yyyy")) < 2000)
                cijfers9 = rijksRegisterNr.ToString()[0 - 8];
            else
                cijfers9 = int.Parse("2" + rijksRegisterNr.ToString()[0 - 8]);
            for(int i = 9; i <= 10; i++)
            {
                cijfersLaatste2_str += rijksRegisterNr.ToString()[i];
            }
            int cijfersLaatste2 = int.Parse(cijfersLaatste2_str);
            int controleGetal = 97 - (cijfers9 % 97);
            if (cijfersLaatste2 == controleGetal) return true;
            return false;
        }
        #endregion

    }
}
