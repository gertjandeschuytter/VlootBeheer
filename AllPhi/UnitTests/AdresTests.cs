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
            Adres a = new("Boerestraat", "Aalst", 9860, "1");
            Assert.Equal("Boerestraat", a.Straat);
            Assert.Equal("Aalst", a.Stad);
            Assert.Equal(9860, a.Postcode);
            Assert.Equal("1", a.Nummer);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_Ctor_InvalidStraat(string straat)
        {
            Assert.Throws<AdresException>(() => new Adres(straat, "Aalst", 9860, "1"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_Ctor_InvalidStad(string stad)
        {
            Assert.Throws<AdresException>(() => new Adres("Boerestraat", stad, 9860, "1"));
        }

        [Theory]
        [InlineData(980)]
        [InlineData(98600)]
        public void Test_Ctor_InvalidPostcode(int postcode)
        {
            Assert.Throws<AdresException>(() => new Adres("Boerestraat", "Aalst", postcode, "1"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Test_Ctor_InvalidNummer(string nummer)
        {
            Assert.Throws<AdresException>(() => new Adres("Boerestraat", "Aalst", 9860, nummer));
        }
        #endregion

        #region Methods
        #region ZetStraat
        [Fact]
        public void Test_ZetStraat_Valid()
        {
            Adres a = new("boerestraat", "Balegem", 9860, "1");
            a.ZetStraat("elenestraat");
            Assert.Equal("elenestraat", a.Straat);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_ZetStraat_Invalid(string straat)
        {
            Adres a = new("boerestraat", "Balegem", 9860, "1");
            Assert.Throws<AdresException>(() => a.ZetStraat(straat));
        }
        #endregion

        #region ZetStad
        [Fact]
        public void Test_ZetStad_Valid()
        {
            Adres a = new("boerestraat", "Balegem", 9860, "1");
            a.ZetStad("Zottegem");
            Assert.Equal("Zottegem", a.Stad);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_ZetStad_Invalid(string stad)
        {
            Adres a = new("boerestraat", "Balegem", 9860, "1");
            Assert.Throws<AdresException>(() => a.ZetStad(stad));
        }
        #endregion

        #region ZetPostcode
        [Fact]
        public void Test_ZetPostcode_Valid()
        {
            Adres a = new("boerestraat", "Balegem", 9860, "1");
            a.ZetPostcode(9998);
            Assert.Equal(9998, a.Postcode);
        }

        [Theory]

        [InlineData(999)]
        [InlineData(99999)]
        public void Test_ZetPostcode_Invalid(int postcode)
        {
            Adres a = new("boerestraat", "Balegem", 9860, "1");
            Assert.Throws<AdresException>(() => a.ZetPostcode(postcode));
        }

        #endregion

        #region ZetNummer
        [Fact]
        public void Test_ZetNummer_Valid()
        {
            Adres a = new("boerestraat", "Balegem", 9860, "1");
            a.ZetNummer("2");
            Assert.Equal("2", a.Nummer);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_ZetNummer_Invalid(string nummer)
        {
            Adres a = new("boerestraat", "Balegem", 9860, "1");
            Assert.Throws<AdresException>(() => a.ZetNummer(nummer));
        }
        #endregion
        #endregion
    }
}
