using System;
using BusinessLayer.Model;
using BusinessLayer.Exceptions;
using Xunit;

namespace UnitTests
{
    public class TankkaartTests
    {
        [Fact]
        public void Test_ctor_InValid()
        {
            TankKaart tankaart = new(/*Ge hoort hier mee te geven wa de waarden zijn niet de types*/);
        }
    }
}

