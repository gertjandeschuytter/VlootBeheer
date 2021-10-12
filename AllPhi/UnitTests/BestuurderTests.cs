using System;
using BusinessLayer.Model;
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
    }
}
