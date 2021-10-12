using System;
using BusinessLayer.Model;
using BusinessLayer.Exceptions;
using Xunit;

namespace UnitTests
{
    public class BestuurderTests
    {
        [Fact]
        public void Test_Ctor_Valid()
        {
            Bestuurder bestuurder = new("De Smet", "Ruben", new DateTime(1999,08,04), 99080455307);
            Assert.Equal("De Smet", bestuurder.Naam);
            Assert.Equal("Ruben", bestuurder.VoorNaam);
            Assert.Equal(new DateTime(1999,08,04), bestuurder.GeboorteDatum);
            Assert.Equal(99080455307, bestuurder.RijksRegisterNr);
        }
        
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_Ctor_NameInValid(string name)
        {
            Assert.Throws<BestuurderException>(() => new Bestuurder(name, "Ruben", new DateTime(1999, 08, 04), 99080455307));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_Ctor_VoornaamInValid(string voornaam)
        {
            Assert.Throws<BestuurderException>(() => new Bestuurder("Vermeire", voornaam, new DateTime(1999, 08, 04), 99080455307));
        }

        [Theory]
        [InlineData(?)]
    }
}
