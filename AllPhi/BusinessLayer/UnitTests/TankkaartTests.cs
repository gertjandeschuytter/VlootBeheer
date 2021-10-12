using System;
using Xunit;
using BusinessLayer.Model;

namespace BusinessLayer.UnitTests
{
    public class TankkaartTests
    {
        [Fact]
        public void Test_ctor_InValid()
        {
            TankKaart tankaart = new TankKaart(long kaartnr, DateTime geldigheidsdatum);
        }
    }
}

