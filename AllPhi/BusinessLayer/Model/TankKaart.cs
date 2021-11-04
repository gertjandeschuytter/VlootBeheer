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
        public TankKaart(string kaartnr, DateTime geldigheidsdatum)
        {
            ZetKaartNr(kaartnr);
            ZetGeldigheidsdatum(geldigheidsdatum);
        }

        public TankKaart(string kaartnr, DateTime geldigheidsdatum, string pincode) : this(kaartnr, geldigheidsdatum)
        {
            ZetPincode(pincode);
        }

        public TankKaart(string kaartnr, DateTime geldigheidsdatum, bool geblokkeerd) : this(kaartnr, geldigheidsdatum)
        {
            Geblokkeerd = geblokkeerd;
        }

        public TankKaart(string kaartnr, DateTime geldigheidsdatum, Bestuurder bestuurder) : this(kaartnr, geldigheidsdatum)
        {
            ZetBestuurder(bestuurder);
        }

        public TankKaart(string kaartnr, DateTime geldigheidsdatum, string pincode, bool geblokkeerd) : this(kaartnr, geldigheidsdatum, pincode)
        {
            Geblokkeerd = geblokkeerd;
        }

        public TankKaart(string kaartnr, DateTime geldigheidsdatum, string pincode, Bestuurder bestuurder) : this(kaartnr, geldigheidsdatum, pincode)
        {
            ZetBestuurder(bestuurder);
        }

        public TankKaart(string kaartnr, DateTime geldigheidsdatum, Bestuurder bestuurder, bool geblokkeerd) : this(kaartnr, geldigheidsdatum, bestuurder)
        {
            Geblokkeerd = geblokkeerd;
        }

        public TankKaart(string kaartnr, DateTime geldigheidsdatum, string pincode, Bestuurder bestuurder, bool geblokkeerd) : this(kaartnr, geldigheidsdatum, pincode, bestuurder)
        {
            Geblokkeerd = geblokkeerd;
        }
        #endregion

        #region Properties
        public string KaartNr { get; set; }

        public DateTime Geldigheidsdatum { get; set; }

        public string Pincode { get; set; }

        public Bestuurder Bestuurder { get; set; }

        public bool Geblokkeerd { get; set; }
        #endregion

        #region Methods
        #region Setters
        public void ZetBestuurder(Bestuurder bestuurder)
        {
            if (bestuurder == null) throw new TankKaartException("Tankkaart: ZetBestuurder - bestuurder is null");
            if (Bestuurder == bestuurder) throw new TankKaartException("Tankkaart: ZetBestuurder - bestuurder bestaat al");
            if (Bestuurder != null)
                if (Bestuurder.HeeftTankKaart(this))
                    Bestuurder.VerwijderTankKaart(this);
            Bestuurder = bestuurder;
            if (bestuurder.TankKaart != this)
                bestuurder.ZetTankKaart(this);
        }
        public void ZetKaartNr(string kaartNr)
        {
            if(string.IsNullOrWhiteSpace(kaartNr)) throw new TankKaartException("Tankkaart: ZetKaartNr - kaartNr is null");
            KaartNr = kaartNr;
        }
        public void ZetGeldigheidsdatum(DateTime geldigheidsdatum)
        {
            if(geldigheidsdatum == DateTime.MinValue) throw new TankKaartException("Tankkaart: ZetGeldigheidsdatum - geldigheidsdatum is null");
            if(geldigheidsdatum <= DateTime.Today) throw new TankKaartException("Tankkaart: ZetGeldigheidsDatum - geldigheidsdatum moet later dan vandaag zijn");
            Geldigheidsdatum = geldigheidsdatum;
        }
        public void ZetPincode(string pincode)
        {
            if (string.IsNullOrWhiteSpace(pincode)) throw new TankKaartException("Tankkaart: ZetPincode - pincode is null");
            if (pincode.Length != 4) throw new TankKaartException("Tankkaart: ZetPincode - pincode moet 4 cijfers lang zijn");
            Pincode = pincode;
        }
        #endregion
        internal bool HeeftBestuurder(Bestuurder bestuurder)
        {
            if (Bestuurder == bestuurder) return true;
            return false;
        }

        internal void VerwijderBestuurder(Bestuurder bestuurder)
        {
            if (Bestuurder != bestuurder) throw new VoertuigException("Voertuig: VerwijderBestuurder - deze bestuurder is niet ingesteld voor dit voertuig");
            Bestuurder = null;
        }
        #endregion
    }
}