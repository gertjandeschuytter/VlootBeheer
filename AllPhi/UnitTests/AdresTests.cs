using System;
using BusinessLayer.Model;
using BusinessLayer.Exceptions;
using Xunit;

namespace UnitTests
{
    public class AdresTests
    {
        #region Ctor
        [Fact]
        public void Test_Ctor_Valid()
        {
            Adres a = new("Boerestraat", "Aalst", "9860", 1);
            Assert.Equal("Boerestraat", a.Straat);
            Assert.Equal("Aalst", a.Stad);
            Assert.Equal("9860", a.Postcode);
            Assert.Equal(1, a.Nummer);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_Ctor_InvalidStraat(string straat)
        {
            Assert.Throws<AdresException>(() => new Adres(straat, "Aalst", "9860", 1));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_Ctor_InvalidStad(string stad)
        {
            Assert.Throws<AdresException>(() => new Adres("Boerestraat", stad, "9860", 1));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("980")]
        [InlineData("98600")]
        public void Test_Ctor_InvalidPostcode(string postcode)
        {
            Assert.Throws<AdresException>(() => new Adres("Boerestraat", "Aalst", postcode, 1));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Test_Ctor_InvalidNummer(int nummer)
        {
            Assert.Throws<AdresException>(() => new Adres("Boerestraat", "Aalst", "9860", nummer));
        }
        #endregion

        #region Methods

        #endregion
    }
}
