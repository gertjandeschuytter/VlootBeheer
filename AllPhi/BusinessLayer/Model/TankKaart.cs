using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Exceptions

namespace BusinessLayer.Model
{
    public class TankKaart
    {

        #region Constructors
        public TankKaart(long kaartnr, DateTime geldigheidsdatum)
        {
            Kaartnr = kaartnr;
            Geldigheidsdatum = geldigheidsdatum;
        }

        public TankKaart(long kaartnr, DateTime geldigheidsdatum, string pincode)
        {
            Kaartnr = kaartnr;
            Geldigheidsdatum = geldigheidsdatum;
            Pincode = pincode;
        }

        public TankKaart(long kaartnr, DateTime geldigheidsdatum, List<string> mogelijkeBrandstoffen)
        {
            Kaartnr = kaartnr;
            Geldigheidsdatum = geldigheidsdatum;
        }

        public TankKaart(long kaartnr, DateTime geldigheidsdatum, bool geblokkeerd)
        {
            Kaartnr = kaartnr;
            Geldigheidsdatum = geldigheidsdatum;
            Geblokkeerd = geblokkeerd;
        }

        public TankKaart(long kaartnr, DateTime geldigheidsdatum, string pincode, List<string> mogelijkeBrandstoffen)
        {
            Kaartnr = kaartnr;
            Geldigheidsdatum = geldigheidsdatum;
            Pincode = pincode;
            MogelijkeBrandstoffen = mogelijkeBrandstoffen;
        }

        public TankKaart(long kaartnr, DateTime geldigheidsdatum, string pincode, bool geblokkeerd)
        {
            Kaartnr = kaartnr;
            Geldigheidsdatum = geldigheidsdatum;
            Pincode = pincode;
            Geblokkeerd = geblokkeerd;
        }

        public TankKaart(long kaartnr, DateTime geldigheidsdatum, List<string> mogelijkeBrandstoffen, bool geblokkeerd)
        {
            Kaartnr = kaartnr;
            Geldigheidsdatum = geldigheidsdatum;
            Geblokkeerd = geblokkeerd;
        }

        public TankKaart(long kaartnr, DateTime geldigheidsdatum, string pincode, List<string> mogelijkeBrandstoffen, bool geblokkeerd)
        {
            Kaartnr = kaartnr;
            Geldigheidsdatum = geldigheidsdatum;
            Pincode = pincode;
            MogelijkeBrandstoffen = mogelijkeBrandstoffen;
            Geblokkeerd = geblokkeerd;
        }
        #endregion

        #region Properties
        public long Kaartnr { get; set; }
        public DateTime Geldigheidsdatum { get; set; }
        public string Pincode { get; set; }
        public List<string> MogelijkeBrandstoffen { get; set; } = new List<string>();
        public Bestuurder Bestuurder { get; set; }
        public bool Geblokkeerd { get; set; }
        #endregion

        #region Methods
        #region Setters
        public void ZetBestuurder(string naam, string voorNaam, DateTime geboorteDatum, long rijksRegisterNr, TypeRijbewijs type)
        {
            if (string.IsNullOrWhiteSpace(naam) && string.IsNullOrWhiteSpace(voorNaam) && string.IsNullOrWhiteSpace(geboorteDatum) && string.IsNullOrWhiteSpace(rijksRegisterNr) && string.IsNullOrWhiteSpace(type)) throw new BestuurderException("Bestuurder: ZetBestuurder - invalid bestuurder: niks mag leeg zijn");
            Bestuurder = new Bestuurder(naam, voorNaam, geboorteDatum, rijksRegisterNr, type);
        }
        #endregion
        #endregion
    }
}
