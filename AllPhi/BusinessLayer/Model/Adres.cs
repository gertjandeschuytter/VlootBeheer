using BusinessLayer.Exceptions;

namespace BusinessLayer.Model
{
    public class Adres
    {
        public Adres(string straat, string stad, string postcode, int nummer)
        {
            ZetStraat(straat);
            ZetStad(stad);
            ZetPostcode(postcode);
            ZetNummer(nummer);
        }

        public string Straat { get; private set; }
        public string Stad { get; private set; }
        public string Postcode { get; private set; }
        public int Nummer { get; private set; }

        public void ZetStraat(string straat)
        {
            if (string.IsNullOrWhiteSpace(straat)) throw new AdresException("Adres: ZetStraat - straat mag niet leeg zijn.");
            Straat = straat;
        }

        public void ZetStad(string stad)
        {
            if (string.IsNullOrWhiteSpace(stad)) throw new AdresException("Adres: ZetStad - stad mag niet leeg zijn;");
            Stad = stad;
        }

        public void ZetPostcode(string postcode)
        {
            if (string.IsNullOrWhiteSpace(postcode)) throw new AdresException("Adres: ZetPostcode - postcode mag niet leeg zijn.");
            if (postcode.Length != 4) throw new AdresException("Adres: ZetPostcode - postcode moet 4 cijfers lang zijn");
            Postcode = postcode;
        }

        public void ZetNummer(int nummer)
        {
            if (nummer <= 0) throw new AdresException("Adres: ZetNummer - nummer mag niet kleiner zijn dan 1.");
            Nummer = nummer;
        }

        public override string ToString()
        {
            return $"{Straat} {Nummer}, {Postcode} {Stad}";
        }
    }
}
