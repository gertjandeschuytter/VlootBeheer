using System;
using BusinessLayer.Model;
using BusinessLayer.Exceptions;
using Xunit;

namespace UnitTests
{
    public class TankkaartTests
    {
        [Fact]
        public void Test_ctor_InValidKaartNr()
        {
            Assert.Throws<TankKaartException>(() => new TankKaart(0, new DateTime(2022, 08, 01)));
        }

        [Fact]
        public void Test_ctor_InValidGeldigheidsDatum()
        {
            Assert.Throws<TankKaartException>(() => new TankKaart(1, DateTime.Today));
        }
    }
}

