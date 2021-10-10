using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Model;

namespace BusinessLayer.Utilities
{
    class RijksRegisterNummerValidator
    {
        public RijksRegisterNummerValidator()
        {
        }

        public static bool ControleerEerste6Cijfers(Bestuurder bestuurder, long rijksRegisterNr)
        {
            string cijfers6_Str = string.Empty;

            cijfers6_Str = rijksRegisterNr.ToString()[0..5];
            string geboorte = bestuurder.GeboorteDatum.ToString("yyMMdd");

            if (cijfers6_Str == geboorte) return true;
            return false;
        }

        public static bool ControleerLaatste2Cijfers(Bestuurder bestuurder, long rijksRegisterNr)
        {
            int cijfers9;
            string cijfersLaatste2_str = string.Empty;
            if (int.Parse(bestuurder.GeboorteDatum.ToString("yyyy")) < 2000)
                cijfers9 = int.Parse(rijksRegisterNr.ToString()[0..8]);
            else
                cijfers9 = int.Parse("2" + rijksRegisterNr.ToString()[0..8]);

            cijfersLaatste2_str = rijksRegisterNr.ToString()[9..];
            int cijfersLaatste2 = int.Parse(cijfersLaatste2_str);
            int controleGetal = 97 - (cijfers9 % 97);
            if (cijfersLaatste2 == controleGetal) return true;
            return false;
        }
    }
}
