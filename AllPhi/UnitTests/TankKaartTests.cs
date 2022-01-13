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
        public static List<object[]> Data()
        {
            return new List<object[]>
            {
                new object[] { DateTime.Today}
            };
        }

        [Fact]
        public void Test_ctor_Valid()
        {
            List<TypeRijbewijs> types = new List<TypeRijbewijs>();
            types.Add(TypeRijbewijs.C);
            Brandstoftype_tankkaart brandstoftype_Tankkaart = Brandstoftype_tankkaart.Benzine;
            Bestuurder b = new Bestuurder("kjsdhfskj", "Jarne", new DateTime(1999, 8, 4), "99080455307", types);
            TankKaart tankkaart = new("125678934512687620", new DateTime(2022, 12, 6), "2564", b, true, brandstoftype_Tankkaart);
            Assert.Equal("125678934512687620", tankkaart.KaartNr);
            Assert.Equal(new DateTime(2022, 12, 6), tankkaart.Geldigheidsdatum);
            Assert.Equal("2564", tankkaart.Pincode);
            Assert.Equal(b, tankkaart.Bestuurder);
            Assert.Equal(Brandstoftype_tankkaart.Benzine, tankkaart.Brandstoftype);
            Assert.True(tankkaart.Geblokkeerd);
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_ctor_InvalidKaartnr(string kaartnr)
        {
            List<TypeRijbewijs> types = new List<TypeRijbewijs>();
            types.Add(TypeRijbewijs.C);
            Brandstoftype_tankkaart brandstoftype_Tankkaart = Brandstoftype_tankkaart.Benzine;
            Assert.Throws<TankKaartException>(() => new TankKaart(kaartnr, new DateTime(2022, 12, 6), "2564", new Bestuurder("kjsdhfskj", "Jarne", new DateTime(1999, 8, 4), "99080455307", types), true, brandstoftype_Tankkaart));
        }
        [Theory]
        [InlineData("1/1/1")]
        [MemberData(nameof(Data))]
        public void Test_ctor_InvalidGeldigheidsdatum(string geldigheidsdatum_str)
        {
            List<TypeRijbewijs> types = new List<TypeRijbewijs>();
            types.Add(TypeRijbewijs.C);
            Brandstoftype_tankkaart brandstoftype_Tankkaart = Brandstoftype_tankkaart.Benzine;
            DateTime geldigheidsdatum = DateTime.Parse(geldigheidsdatum_str);
            Assert.Throws<TankKaartException>(() => new TankKaart("125678934512687620", geldigheidsdatum, "2564", new Bestuurder("kjsdhfskj", "Jarne", new DateTime(1999, 8, 4), "99080455307", types), true, brandstoftype_Tankkaart));
        }
        [Theory]
        [InlineData("21")]
        public void Test_ctor_InvalidPincode(string pincode)
        {
            List<TypeRijbewijs> types = new List<TypeRijbewijs>();
            types.Add(TypeRijbewijs.C);
            Brandstoftype_tankkaart brandstoftype_Tankkaart = Brandstoftype_tankkaart.Benzine;
            Assert.Throws<TankKaartException>(() => new TankKaart("125678934512687620", new DateTime(2022, 12, 6), pincode, new Bestuurder("kjsdhfskj", "Jarne", new DateTime(1999, 8, 4), "99080455307", types), true, brandstoftype_Tankkaart));
        }
        //[Theory]
        //[InlineData(null)]
        //public void Test_ctor_InvalidBestuurder(Bestuurder bestuurder)
        //{
        //    Brandstoftype_tankkaart brandstoftype_Tankkaart = Brandstoftype_tankkaart.Benzine;
        //    Assert.Throws<TankKaartException>(() => new TankKaart("125678934512687620", new DateTime(2022, 12, 6), "2564", bestuurder, true, brandstoftype_Tankkaart));
        //}
    }
}
