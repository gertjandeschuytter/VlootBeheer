using BusinessLayer.Exceptions;

namespace BusinessLayer.Model
{
    public class Adres
    {
        public Adres(string straat, string stad, int postcode, int nummer)
        {
            ZetStraat(straat);
            ZetStad(stad);
            ZetPostcode(postcode);
            ZetNummer(nummer);
        }

        public string Straat { get; private set; }
        public string Stad { get; private set; }
        public int Postcode { get; private set; }
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

        public void ZetPostcode(int postcode)
        {
            if (postcode == 0) throw new AdresException("Adres: ZetPostcode - postcode mag niet 0 zijn.");
            Postcode = postcode;
        }

        public void ZetNummer(int nummer)
        {
            if (nummer <= 0) throw new AdresException("Adres: ZetNummer - nummer mag niet kleiner zijn dan 1.");
            Nummer = nummer;
        }
    }
}
