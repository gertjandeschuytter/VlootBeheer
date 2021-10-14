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
            Bestuurder bestuurder = new("De Smet", "Ruben", new DateTime(1999, 08, 04), 99080455307);
            Assert.Equal("De Smet", bestuurder.Naam);
            Assert.Equal("Ruben", bestuurder.VoorNaam);
            Assert.Equal(new DateTime(1999, 08, 04), bestuurder.GeboorteDatum);
            Assert.Equal(99080455307, bestuurder.RijksRegisterNr);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_CtorEssential_NameInValid(string name)
        {
            Assert.Throws<BestuurderException>(() => new Bestuurder(name, "Ruben", new DateTime(1999, 08, 04), 99080455307));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_CtorEssential_VoornaamInValid(string voornaam)
        {
            Assert.Throws<BestuurderException>(() => new Bestuurder("Vermeire", voornaam, new DateTime(1999, 08, 04), 99080455307));
        }

        [Theory]
        [InlineData("1/1/1")]
        public void Test_CtorEssential_DatumInValid(string datum_str)
        {
            DateTime datum = DateTime.Parse(datum_str);
            Assert.Throws<BestuurderException>(() => new Bestuurder("Vermeire", "GertJan", datum, 99080455307));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(01234567891)]
        [InlineData(99080455308)]
        public void Test_CtorEssential_RijksRegisterNrInValid(long rijksregister)
        {
            Assert.Throws<BestuurderException>(() => new Bestuurder("Vermeire", "GertJan", DateTime.Today, rijksregister));
        }
        #endregion

        #region Tests_Ctor_Ess+1
        [Fact]
        public void Test_Ctor_Adres_Valid()
        {
            Adres a = new("Valhalla", "Asgard", 6996, 1);
            Bestuurder bestuurder = new("De Smet", "Ruben", a, new DateTime(1999, 08, 04), 99080455307);
            Assert.Equal("De Smet", bestuurder.Naam);
            Assert.Equal("Ruben", bestuurder.VoorNaam);
            Assert.Equal(a, bestuurder.Adres);
            Assert.Equal(new DateTime(1999, 08, 04), bestuurder.GeboorteDatum);
            Assert.Equal(99080455307, bestuurder.RijksRegisterNr);
        }

        [Fact]
        public void Test_Ctor_Voertuig_Valid()
        {
            Voertuig v = new(/*help*/);
            Bestuurder bestuurder = new("De Smet", "Ruben", new DateTime(1999, 08, 04), 99080455307, v);
            Assert.Equal("De Smet", bestuurder.Naam);
            Assert.Equal("Ruben", bestuurder.VoorNaam);
            Assert.Equal(new DateTime(1999, 08, 04), bestuurder.GeboorteDatum);
            Assert.Equal(99080455307, bestuurder.RijksRegisterNr);
            Assert.Equal(v, bestuurder.Voertuig);
        }

        [Fact]
        public void Test_Ctor_TankKaart_Valid()
        {
            TankKaart t = new(/*help*/);
            Bestuurder bestuurder = new("De Smet", "Ruben", new DateTime(1999, 08, 04), 99080455307, t);
            Assert.Equal("De Smet", bestuurder.Naam);
            Assert.Equal("Ruben", bestuurder.VoorNaam);
            Assert.Equal(new DateTime(1999, 08, 04), bestuurder.GeboorteDatum);
            Assert.Equal(99080455307, bestuurder.RijksRegisterNr);
            Assert.Equal(t, bestuurder.TankKaart);
        }
        #endregion

        #region Tests_Ctor_Ess+2
        #endregion

        #region Tests_Ctor_All
        #endregion
    }
}
