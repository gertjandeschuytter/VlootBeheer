using System;
using System.Linq;

namespace BusinessLayer.Utilities {
     public static class NummerplaatValidator {
        public static bool IsLengteCorrect(string nummerplaat) {
            if (nummerplaat.Trim().Length == 7) return true;
            return false;
        }
        public static bool IsEersteCijferCorrect(string nummerplaat) {
            nummerplaat = nummerplaat.Trim();
            bool isDigit = char.IsDigit(nummerplaat[0]);
            string firstChar = nummerplaat.Substring(0, 1);
            if (isDigit & (firstChar == "1" || firstChar == "2")) return true;
            return false;
        }
        public static bool IsTweedeDeelCorrect(string nummerplaat) {
            nummerplaat = nummerplaat.Trim();
            string secondPart = nummerplaat.Substring(1, 3);
            bool BevatTweedeStukEnkelLetters = secondPart.All(char.IsLetter);
            if (BevatTweedeStukEnkelLetters) return true;
            return false;
        }
        public static bool IsDerdeDeelCorrect(string nummerplaat) {
            nummerplaat = nummerplaat.Trim();
            string thirdPart = nummerplaat.Substring(4, 3);
            bool BevatDerdeStukEnkelNummers = thirdPart.All(char.IsDigit);
            if (BevatDerdeStukEnkelNummers) return true;
            return false;
        }
    }
}
