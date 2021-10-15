using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer.Utilities {
    public static class ChassisnummerValidator {
        public static bool BevatChassisnummerSpeciaalKarakter(string chassisNummer) {
            Regex rgx = new("[^A-Za-z0-9]");
            bool containsSpecialCharacter = rgx.IsMatch(chassisNummer);
            return containsSpecialCharacter;
        }
    }
}
