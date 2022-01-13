using BusinessLayer.Exceptions;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Model
{
    public class Adres : IEquatable<Adres> {
        public Adres(string straat, string stad, int postcode, string nummer)
        {
            ZetStraat(straat);
            ZetStad(stad);
            ZetPostcode(postcode);
            ZetNummer(nummer);
        }

        public Adres(int iD, string straat, string stad, int postcode, string nummer) {
            ZetAdresId(iD);
            ZetStraat(straat);
            ZetStad(stad);
            ZetPostcode(postcode);
            ZetNummer(nummer);
        }

        public int ID { get; private set; }
        public string Straat { get; private set; }
        public string Stad { get; private set; }
        public int Postcode { get; private set; }
        public string Nummer { get; private set; }

        public void ZetAdresId(int adresId) {
            if (adresId <= 0) throw new AdresException("Id mag niet lager of gelijk zijn aan 0");
            ID = adresId;
        }
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
            if (postcode < 1000 || postcode > 9999) throw new AdresException("Adres: ZetPostcode - postcode moet minsters 4 cijfers bevatten.");
            Postcode = postcode;
        }

        public void ZetNummer(string nummer)
        {
            if (string.IsNullOrWhiteSpace(nummer)) throw new AdresException("Adres: ZetNummer - nummer mag niet kleiner zijn dan 1.");
            Nummer = nummer;
        }

        public override bool Equals(object obj) {
            return Equals(obj as Adres);
        }

        public bool Equals(Adres other) {
            return other != null &&
                   ID == other.ID &&
                   Straat == other.Straat &&
                   Stad == other.Stad &&
                   Postcode == other.Postcode &&
                   Nummer == other.Nummer;
        }

        public override int GetHashCode() {
            return HashCode.Combine(ID, Straat, Stad, Postcode, Nummer);
        }

        public static bool operator ==(Adres left, Adres right) {
            return EqualityComparer<Adres>.Default.Equals(left, right);
        }

        public static bool operator !=(Adres left, Adres right) {
            return !(left == right);
        }
    }
}
