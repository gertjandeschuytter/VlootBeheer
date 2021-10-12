using System;
using System.Linq;

public class NummerplaatValidator {
    public NummerplaatValidator() {

    }
    public static bool ControleerLengte(string nummerplaat) {
        if (nummerplaat.Trim().Length == 7) {
            return true;
        }
        return false;
    }
    public static bool ControleerEersteCijfer(string nummerplaat) {
        bool isDigit = char.IsDigit(nummerplaat[0]);
        string firstChar = nummerplaat.Substring(0, 1);
        if (isDigit) {
            if (firstChar != "1" || firstChar != "2") {
                return false;
            }
            return true;
        }
        return false;
    }
    public static bool ControleerTweedeStuk(string nummerplaat) {
        string secondPart = nummerplaat.Substring(1, 3);
        bool isIntString = nummerplaat.All(char.IsDigit);
        if (isIntString) {
            return false;
        } else {
            return true;
        }
    }
    public static bool ControleerDerdeStuk(string nummerplaat) {
        string secondPart = nummerplaat.Substring(4, 6);
        bool isIntString = nummerplaat.All(char.IsDigit);
        if (isIntString) {
            return true;
        } else {
            return false;
        }
    }
}
