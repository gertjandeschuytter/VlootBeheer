using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BusinessLayer.Model;
using BusinessLayer.Exceptions;

namespace UnitTests
{
    public class TankKaartTests
    {
        [Fact]
        public void Test_ctor_Valid()
        {
            TankKaart tankkaart = new TankKaart("125678934512687620", new DateTime(2021, 12, 6), "2564", new Bestuurder("kjsdhfskj", "Jarne", new DateTime(1999, 8, 4), 99080455307), true);
            Assert.Equal("125678934512687620", tankkaart.KaartNr);
            Assert.Equal(new DateTime(2021, 12, 6), tankkaart.Geldigheidsdatum);
            Assert.Equal("2564", tankkaart.Pincode);
            Assert.Equal(new Bestuurder("kjsdhfskj", "Jarne", new DateTime(1999, 8, 4), 99080455307), tankkaart.Bestuurder);
            Assert.True(tankkaart.Geblokkeerd);
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_ctor_InvalidKaartnr(string kaartnr)
        {
            Assert.Throws<TankKaartException>(() => new TankKaart(kaartnr, new DateTime(2021, 12, 6), "2564", new Bestuurder("kjsdhfskj", "Jarne", new DateTime(1999, 8, 4), 99080455307), true));
        }
        public void Test_ZetBestuurder_Valid()
        {

        }
    }
}
