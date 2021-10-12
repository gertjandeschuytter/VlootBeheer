using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Exceptions;

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

        public TankKaart(long kaartnr, DateTime geldigheidsdatum, string pincode) : this(kaartnr, geldigheidsdatum)
        {
            Kaartnr = kaartnr;
            Geldigheidsdatum = geldigheidsdatum;
            Pincode = pincode;
        }

        public TankKaart(long kaartnr, DateTime geldigheidsdatum, List<string> mogelijkeBrandstoffen) : this(kaartnr, geldigheidsdatum)
        {
            Kaartnr = kaartnr;
            Geldigheidsdatum = geldigheidsdatum;
        }

        public TankKaart(long kaartnr, DateTime geldigheidsdatum, bool geblokkeerd) : this(kaartnr, geldigheidsdatum)
        {
            Kaartnr = kaartnr;
            Geldigheidsdatum = geldigheidsdatum;
            Geblokkeerd = geblokkeerd;
        }

        public TankKaart(long kaartnr, DateTime geldigheidsdatum, string pincode, List<string> mogelijkeBrandstoffen) : this(kaartnr, geldigheidsdatum)
        {
            Kaartnr = kaartnr;
            Geldigheidsdatum = geldigheidsdatum;
            Pincode = pincode;
            MogelijkeBrandstoffen = mogelijkeBrandstoffen;
        }

        public TankKaart(long kaartnr, DateTime geldigheidsdatum, string pincode, bool geblokkeerd) : this(kaartnr, geldigheidsdatum)
        {
            Kaartnr = kaartnr;
            Geldigheidsdatum = geldigheidsdatum;
            Pincode = pincode;
            Geblokkeerd = geblokkeerd;
        }

        public TankKaart(long kaartnr, DateTime geldigheidsdatum, List<string> mogelijkeBrandstoffen, bool geblokkeerd) : this(kaartnr, geldigheidsdatum)
        {
            Kaartnr = kaartnr;
            Geldigheidsdatum = geldigheidsdatum;
            Geblokkeerd = geblokkeerd;
        }

        public TankKaartlong kaartnr, DateTime geldigheidsdatum, string pincode, List<string> mogelijkeBrandstoffen, bool geblokkeerd) : this(kaartnr, geldigheidsdatum)
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
        public void ZetBestuurder(Bestuurder bestuurder)
        {
            Bestuurder = bestuurder;
            Bestuurder.ZetTankKaart(this);
        }
        public void ZetKaartNr(long kaartNr)
        {
            if(kaartNr == null) throw new TankKaartException("Tankkaart: ZetKaartNr - kaartNr is null");
            KaartNr = kaartNr;
        }
        public void ZetGeldigheidsdatum(DateTime geldigheidsdatum)
        {
            if(geldigheidsdatum == null) throw new TankKaartException("Tankkaart: ZetGeldigheidsdatum - geldigheidsdatum is null");
            Geldigheidsdatum = geldigheidsdatum;
        }
        public void ZetPincode(string pincode)
        {
            if(pincode == null) throw new TankKaartException("Tankkaart: ZetPincode - pincode is null");
            Pincode = pincode;
        }
        //public void ZetMogelijkeBrandstoffen()
        public void ZetGeblokkeerd(bool geblokkeerd)
        {
            if(geblokkeerd == null) throw new TankKaartException("Tankkaart: ZetKaartNr - geblokkeerd is null");
            Geblokkeerd = geblokkeerd;
        }
        #endregion
        #endregion
    }
}