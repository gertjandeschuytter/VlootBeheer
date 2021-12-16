using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Exceptions;

namespace BusinessLayer.Model
{
    public class TankKaart {
        public TankKaart(string kaartNr, DateTime geldigheidsdatum)
        {
            ZetKaartNr(kaartNr);
            ZetGeldigheidsdatum(geldigheidsdatum);
        }

        public TankKaart(int tankkaartId, string kaartNr, DateTime geldigheidsdatum, string pincode, Bestuurder bestuurder, bool geblokkeerd, Brandstoftype_tankkaart? brandstoftype)
        {
            ZetTankkaartId(tankkaartId);
            ZetKaartNr(kaartNr);
            ZetGeldigheidsdatum(geldigheidsdatum);
            ZetPincode(pincode);
            ZetBestuurder(bestuurder);
            Geblokkeerd = geblokkeerd;
            Brandstoftype = brandstoftype;
        }

        #region Constructors

        #endregion

        #region Properties
        public int TankkaartId { get; private set; }
        public string KaartNr { get; private set; }

        public DateTime Geldigheidsdatum { get; private set; }

        public string Pincode { get; private set; }

        public Bestuurder Bestuurder { get; private set; }

        public bool Geblokkeerd { get; private set; }
        public Brandstoftype_tankkaart? Brandstoftype { get; private set; }
        #endregion

        #region Methods
        #region Setters
        public void ZetTankkaartId(int TankkaartId) {
            if (TankkaartId <= 0) throw new TankKaartException("TankkaartId mag niet kleiner of gelijk zijn aan 0");
            this.TankkaartId = TankkaartId;
        }
        public void ZetBrandstoftype(Brandstoftype_tankkaart? brandstoftype)
        {
            if (brandstoftype == null) throw new TankKaartException("Tankkaart: ZetBrandstoftype - Brandstoftype mag niet null zijn");
            Brandstoftype = brandstoftype;
        }
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