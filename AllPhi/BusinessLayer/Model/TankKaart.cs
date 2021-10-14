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
            ZetKaartNr(kaartnr);
            ZetGeldigheidsdatum(geldigheidsdatum);
            MogelijkeBrandstoffen = new List<string>();
        }

        public TankKaart(long kaartnr, DateTime geldigheidsdatum, string pincode) : this(kaartnr, geldigheidsdatum)
        {
            ZetPincode(pincode);
        }

        public TankKaart(long kaartnr, DateTime geldigheidsdatum, bool geblokkeerd) : this(kaartnr, geldigheidsdatum)
        {
            Geblokkeerd = geblokkeerd;
        }

        public TankKaart(long kaartnr, DateTime geldigheidsdatum, Bestuurder bestuurder) : this(kaartnr, geldigheidsdatum)
        {
            ZetBestuurder(bestuurder);
        }

        public TankKaart(long kaartnr, DateTime geldigheidsdatum, string pincode, bool geblokkeerd) : this(kaartnr, geldigheidsdatum, pincode)
        {
            Geblokkeerd = geblokkeerd;
        }

        public TankKaart(long kaartnr, DateTime geldigheidsdatum, string pincode, Bestuurder bestuurder) : this(kaartnr, geldigheidsdatum, pincode)
        {
            ZetBestuurder(bestuurder);
        }

        public TankKaart(long kaartnr, DateTime geldigheidsdatum, Bestuurder bestuurder, bool geblokkeerd) : this(kaartnr, geldigheidsdatum, bestuurder)
        {
            Geblokkeerd = geblokkeerd;
        }

        public TankKaart(long kaartnr, DateTime geldigheidsdatum, string pincode, Bestuurder bestuurder, bool geblokkeerd) : this(kaartnr, geldigheidsdatum, pincode, bestuurder)
        {
            Geblokkeerd = geblokkeerd;
        }
        #endregion

        #region Properties
        public long KaartNr { get; set; }

        public DateTime Geldigheidsdatum { get; set; }

        public string Pincode { get; set; }

        public List<string> MogelijkeBrandstoffen { get; set; }

        public Bestuurder Bestuurder { get; set; }

        public bool Geblokkeerd { get; set; }
        #endregion

        #region Methods
        #region Setters
        public void ZetBestuurder(Bestuurder bestuurder)
        {
            Bestuurder = bestuurder ?? throw new TankKaartException("Tankkaart: ZetBestuurder - bestuurder is null");
            Bestuurder.ZetTankKaart(this);
        }
        public void ZetKaartNr(long kaartNr)
        {
            if(kaartNr == 0) throw new TankKaartException("Tankkaart: ZetKaartNr - kaartNr is null");
            KaartNr = kaartNr;
        }
        public void ZetGeldigheidsdatum(DateTime geldigheidsdatum)
        {
            if(geldigheidsdatum == null) throw new TankKaartException("Tankkaart: ZetGeldigheidsdatum - geldigheidsdatum is null");
            if(geldigheidsdatum <= DateTime.Today) throw new TankKaartException("Tankkaart: ZetGeldigheidsDatum - geldigheidsdatum moet later dan vandaag zijn");
            Geldigheidsdatum = geldigheidsdatum;
        }
        public void ZetPincode(string pincode)
        {
            if (pincode.Length != 4) throw new TankKaartException("Tankkaart: ZetPincode - pincode moet 4 cijfers lang zijn");
            Pincode = pincode ?? throw new TankKaartException("Tankkaart: ZetPincode - pincode is null");
        }
        //public void ZetMogelijkeBrandstoffen()
        #endregion
        #endregion
    }
}