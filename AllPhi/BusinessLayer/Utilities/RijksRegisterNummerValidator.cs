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

        public static bool ContorleerEmpty(string rijksRegisterNr)
        {
            if (string.IsNullOrWhiteSpace(rijksRegisterNr)) return true;
            return false;
        }

        public static bool ControleerLengte(string rijksRegisterNr)
        {
            if (rijksRegisterNr.Length != 11) return false;
            return true;
        }

        public static bool ControleerEerste6Cijfers(Bestuurder bestuurder, string rijksRegisterNr)
        {
            string cijfers6_Str = string.Empty;

            cijfers6_Str = rijksRegisterNr[0..6];
            string geboorte = bestuurder.Geboortedatum.ToString("yyMMdd");

            if (cijfers6_Str == geboorte) return true;
            return false;
        }

        public static bool ControleerLaatste2Cijfers(Bestuurder bestuurder, string rijksRegisterNr)
        {
            int cijfers9;
            string cijfersLaatste2_str = string.Empty;
            if (int.Parse(bestuurder.Geboortedatum.ToString("yyyy")) < 2000)
                cijfers9 = int.Parse(rijksRegisterNr[0..9]);
            else
                cijfers9 = int.Parse("2" + rijksRegisterNr[0..9]);

            cijfersLaatste2_str = rijksRegisterNr[9..];
            int controleGetal = 97 - (cijfers9 % 97);
            string controleGetal_Str = controleGetal.ToString();
            if(controleGetal_Str.Length < 2)
            {
                controleGetal_Str = "0" + controleGetal;
            }
            if (cijfersLaatste2_str == controleGetal_Str) return true;
            return false;
        }
    }
}
