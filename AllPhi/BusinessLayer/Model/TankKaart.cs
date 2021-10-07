using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model
{
    public class TankKaart
    {
        #region Constructors
        public TankKaart(long kaartnr, DateTime geldigheidsdatum, int pincode, List<string> mogelijkeBrandstoffen, Bestuurder bestuurder, bool geblokkeerd)
        {
            Kaartnr = kaartnr;
            Geldigheidsdatum = geldigheidsdatum;
            Pincode = pincode;
            MogelijkeBrandstoffen = mogelijkeBrandstoffen;
            Bestuurder = bestuurder;
            Geblokkeerd = geblokkeerd;
        }
        #endregion

        #region Properties
        public long Kaartnr { get; set; }
        public DateTime Geldigheidsdatum { get; set; }
        public int Pincode { get; set; }
        public List<string> MogelijkeBrandstoffen { get; set; } = new List<string>();
        public Bestuurder Bestuurder { get; set; }
        public bool Geblokkeerd { get; set; }
        #endregion

        #region Methods
        #endregion
    }
}
