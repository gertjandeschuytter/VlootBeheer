using System;
using BusinessLayer.Model;
using BusinessLayer.Exceptions;
using Xunit;
using NPOI.SS.Formula.Functions;

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

        #region Methods

        #region Valid
        #region Setters
        [Fact]
        public void Test_ZetNaam_Valid()
        {
            Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307");
            b.ZetNaam("Souffriau");
            Assert.Equal("Souffriau", b.Naam);
        }

        [Fact]
        public void Test_ZetVoorNaam_Valid()
        {
            Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307");
            b.ZetVoorNaam("Karel");
            Assert.Equal("Karel", b.VoorNaam);
        }

        [Fact]
        public void Test_ZetGeboorteDatum_Valid()
        {
            Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307");
            b.ZetGeboorteDatum(new DateTime(2000, 10, 20));
            Assert.Equal(new DateTime(2000, 10, 20), b.GeboorteDatum);
        }

        [Fact]
        public void Test_ZetRijksRegisterNummer_Valid()
        {
            Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307");
            b.ZetRijksRegisterNummer("99080455307");
            Assert.Equal("99080455307", b.RijksRegisterNr);
        }

        [Fact]
        public void Test_ZetAdres_Valid()
        {
            Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307");
            Adres a = new("Boerestraat", "Balegem", "9860", 1);
            b.ZetAdres(a);
            Assert.Equal(a, b.Adres);
        }

        [Fact]
        public void Test_ZetVoertuig_Valid()
        {
            Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307");
            Voertuig v = new("Toyota", "308", "01234567891234567", "1ABC123", Brandstoftype.Benzine, Typewagen.personenwagen);
            b.ZetVoertuig(v);
            Assert.Equal(v, b.Voertuig);
        }

        [Fact]
        public void Test_ZetTankKaart_Valid()
        {
            Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307");
            TankKaart t = new("012345678912345", new DateTime(2022, 10, 30));
            b.ZetTankKaart(t);
            Assert.Equal(t, b.TankKaart);
        }
        #endregion

        #region Others
        [Fact]
        public void Test_VoegRijbewijsToe_Valid()
        {
            Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307");
            b.VoegRijbewijsToe(TypeRijbewijs.C);
            Assert.Contains(TypeRijbewijs.C, b.Types);
        }

        [Fact]
        public void Test_VerwijderRijbewijs_Valid()
        {
            Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307");
            b.VoegRijbewijsToe(TypeRijbewijs.C);
            b.VerwijderRijbewijs(TypeRijbewijs.C);
            Assert.Empty(b.Types);
        }

        [Fact]
        public void Test_VerwijderVoertuig_Valid()
        {
            Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307");
            Voertuig v = new("Toyota", "308", "01234567891234567", "1ABC123", Brandstoftype.Benzine, Typewagen.personenwagen);
            b.ZetVoertuig(v);
            b.VerwijderVoertuig(v);
            Assert.Null(b.Voertuig);
        }

        [Fact]
        public void Test_VerwijderTankKaart_Valid()
        {
            Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307");
            TankKaart t = new("012345678912345", new DateTime(2022, 10, 30));
            b.ZetTankKaart(t);
            b.VerwijderTankKaart(t);
            Assert.Null(b.TankKaart);
        }
        #endregion
        #endregion

        #region Invalid
        #region Setters
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_ZetNaam_Invalid(string naam)
        {
            Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307");
            Assert.Throws<BestuurderException>(() => b.ZetNaam(naam));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_ZetVoorNaam_Invalid(string voornaam)
        {
            Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307");
            Assert.Throws<BestuurderException>(() => b.ZetVoorNaam(voornaam));
        }

        [Fact]
        public void Test_ZetGeboorteDatum_Invalid()
        {
            Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307");
            Assert.Throws<BestuurderException>(() => b.ZetGeboorteDatum(new DateTime(1,1,1)));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("88020155307")]
        [InlineData("99080455371")]
        [InlineData("1")]
        [InlineData("9999999999999")]
        public void Test_ZetRijksRegisterNummer_Invalid(string rijksRegist)
        {
            Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307");
            Assert.Throws<BestuurderException>(() => b.ZetRijksRegisterNummer(rijksRegist));
        }

        [Fact]
        public void Test_ZetAdres_Invalid()
        {
            Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307");
            Assert.Throws<BestuurderException>(() => b.ZetAdres(null));
        }

        [Fact]
        public void Test_ZetVoertuig_Invalid()
        {
            Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307");
            Assert.Throws<BestuurderException>(() => b.ZetVoertuig(null));
        }

        [Fact]
        public void Test_ZetTankKaart_Invalid()
        {
            Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307");
            Assert.Throws<BestuurderException>(() => b.ZetTankKaart(null));
        }
        #endregion

        #region Others
        [Fact]
        public void Test_VoegRijbewijsToe_Invalid()
        {
            Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307");
            b.VoegRijbewijsToe(TypeRijbewijs.C);
            Assert.Throws<BestuurderException>(() => b.VoegRijbewijsToe(TypeRijbewijs.C));
        }

        [Fact]
        public void Test_VerwijderRijbewijs_Invalid()
        {
            Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307");
            Assert.Throws<BestuurderException>(() => b.VerwijderRijbewijs(TypeRijbewijs.C));
        }

        [Fact]
        public void Test_VerwijderVoertuig_Invalid()
        {
            Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307");
            Voertuig v = new("Toyota", "308", "01234567891234567", "1ABC123", Brandstoftype.Benzine, Typewagen.personenwagen);
            Assert.Throws<BestuurderException>(() => b.VerwijderVoertuig(v));
        }

        [Fact]
        public void Test_VerwijderTankKaart_Invalid()
        {
            Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307");
            TankKaart t = new("012345678912345", new DateTime(2022, 10, 30));
            Assert.Throws<BestuurderException>(() => b.VerwijderTankKaart(t));
        }
        #endregion
        #endregion
        #endregion
    }
}
