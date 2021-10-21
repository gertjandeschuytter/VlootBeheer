using System;
using BusinessLayer.Model;
using BusinessLayer.Exceptions;
using Xunit;

namespace UnitTests
{
    public class BestuurderTests
    {
        #region Tests_Ctor_Essentials
        [Fact]
        public void Test_CtorEssential_Valid()
        {
            Bestuurder bestuurder = new("De Smet", "Ruben", new DateTime(1999, 08, 04), "99080455307");
            Assert.Equal("De Smet", bestuurder.Naam);
            Assert.Equal("Ruben", bestuurder.VoorNaam);
            Assert.Equal(new DateTime(1999, 08, 04), bestuurder.GeboorteDatum);
            Assert.Equal("99080455307", bestuurder.RijksRegisterNr);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_CtorEssential_NameInValid(string name)
        {
            Assert.Throws<BestuurderException>(() => new Bestuurder(name, "Ruben", new DateTime(1999, 08, 04), "99080455307"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_CtorEssential_VoornaamInValid(string voornaam)
        {
            Assert.Throws<BestuurderException>(() => new Bestuurder("Vermeire", voornaam, new DateTime(1999, 08, 04), "99080455307"));
        }

        [Theory]
        [InlineData("1/1/1")]
        public void Test_CtorEssential_DatumInValid(string datum_str)
        {
            DateTime datum = DateTime.Parse(datum_str);
            Assert.Throws<BestuurderException>(() => new Bestuurder("Vermeire", "GertJan", datum, "99080455307"));
        }

        [Theory]
        [InlineData("1")]
        [InlineData("01234567891")]
        [InlineData("99080455308")]
        public void Test_CtorEssential_RijksRegisterNrInValid(string rijksregister)
        {
            Assert.Throws<BestuurderException>(() => new Bestuurder("Vermeire", "GertJan", DateTime.Today, rijksregister));
        }
        #endregion

        #region Tests_Ctor_All
        [Fact]
        public void Test_Ctor_All_Valid()
        {
            Adres a = new("DiepenBroekStraat", "Balegem", "9860", 1);
            Voertuig v = new("Toyota", "Fiesta", "01234567891234567", "1ABC123", Brandstoftype.Benzine, Typewagen.personenwagen);
            TankKaart t = new("0123456789", new DateTime(2022, 08, 01));

            Bestuurder bestuurder = new("Ophalvens", "Jarne", a, new DateTime(1999, 08, 04), "99080455307", v, t);
            Assert.Equal("Ophalvens", bestuurder.Naam);
            Assert.Equal("Jarne", bestuurder.VoorNaam);
            Assert.Equal(a, bestuurder.Adres);
            Assert.Equal(new DateTime(1999, 08, 04), bestuurder.GeboorteDatum);
            Assert.Equal("99080455307", bestuurder.RijksRegisterNr);
            Assert.Equal(v, bestuurder.Voertuig);
            Assert.Equal(t, bestuurder.TankKaart);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_Ctor_All_InvalidNaam(string naam)
        {
            Adres a = new("DiepenBroekStraat", "Balegem", "9860", 1);
            Voertuig v = new("Toyota", "Fiesta", "01234567891234567", "1ABC123", Brandstoftype.Benzine, Typewagen.personenwagen);
            TankKaart t = new("0123456789", new DateTime(2022, 08, 01));

            Assert.Throws<BestuurderException>(() => new Bestuurder(naam, "Jarne", a, new DateTime(1999, 08, 04), "99080455307", v, t));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_Ctor_All_InvalidVoornaam(string voornaam)
        {
            Adres a = new("DiepenBroekStraat", "Balegem", "9860", 1);
            Voertuig v = new("Toyota", "Fiesta", "01234567891234567", "1ABC123", Brandstoftype.Benzine, Typewagen.personenwagen);
            TankKaart t = new("0123456789", new DateTime(2022, 08, 01));

            Assert.Throws<BestuurderException>(() => new Bestuurder("Ophalvens", voornaam, a, new DateTime(1999, 08, 04), "99080455307", v, t));
        }

        [Fact]
        public void Test_Ctor_All_InvalidAdres()
        {
            Voertuig v = new("Toyota", "Fiesta", "01234567891234567", "1ABC123", Brandstoftype.Benzine, Typewagen.personenwagen);
            TankKaart t = new("0123456789", new DateTime(2022, 08, 01));
            Assert.Throws<BestuurderException>(() => new Bestuurder("Ophalvens", "Harry", null, new DateTime(1999, 08, 04), "99080455307", v, t));
        }

        [Fact]
        public void Test_Ctor_All_InvalidDatum()
        {
            Adres a = new("DiepenBroekStraat", "Balegem", "9860", 1);
            Voertuig v = new("Toyota", "Fiesta", "01234567891234567", "1ABC123", Brandstoftype.Benzine, Typewagen.personenwagen);
            TankKaart t = new("0123456789", new DateTime(2022, 08, 01));

            Assert.Throws<BestuurderException>(() => new Bestuurder("Ophalvens", "Jarne", a, new DateTime(1, 1, 1), "99080555307", v, t));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("9908045530")]
        [InlineData("20073155307")]
        [InlineData("99080455309")]
        public void Test_Ctor_All_InvalidRijksRegisterNummer(string rijks)
        {
            Adres a = new("DiepenBroekStraat", "Balegem", "9860", 1);
            Voertuig v = new("Toyota", "Fiesta", "01234567891234567", "1ABC123", Brandstoftype.Benzine, Typewagen.personenwagen);
            TankKaart t = new("0123456789", new DateTime(2022, 08, 01));

            Assert.Throws<BestuurderException>(() => new Bestuurder("Ophalvens", "Jarne", a, new DateTime(1999,8,4), rijks, v, t));
        }

        [Fact]
        public void Test_Ctor_All_InvalidVoertuig()
        {
            Adres a = new("DiepenBroekStraat", "Balegem", "9860", 1);
            TankKaart t = new("0123456789", new DateTime(2022, 08, 01));

            Assert.Throws<BestuurderException>(() => new Bestuurder("Ophalvens", "Jarne", a, new DateTime(1999, 8, 4), "99080455307", null, t));
        }

        [Fact]
        public void Test_Ctor_All_InvalidTankKaart()
        {
            Adres a = new("DiepenBroekStraat", "Balegem", "9860", 1);
            Voertuig v = new("Toyota", "Fiesta", "01234567891234567", "1ABC123", Brandstoftype.Benzine, Typewagen.personenwagen);

            Assert.Throws<BestuurderException>(() => new Bestuurder("Ophalvens", "Jarne", a, new DateTime(1999, 8, 4), "99080455307", v, null));
        }
        #endregion


    }
}
