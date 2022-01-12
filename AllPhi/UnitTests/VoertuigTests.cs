using System;
using BusinessLayer.Model;
using BusinessLayer.Exceptions;
using Xunit;
using System.Collections.Generic;

namespace UnitTests
{
    public class VoertuigTests
    {
        //eigen tests
        [Fact]
        public void Test_CtorEssential_Valid()
        {
            Voertuig v = new("peugot", "308", "A15KSLSK124ND523A", "1ABC325", Brandstoftype_voertuig.Benzine, Typewagen.Personenwagen);
            Assert.Equal("peugot", v.Merk);
            Assert.Equal("308", v.Model);
            Assert.Equal("A15KSLSK124ND523A", v.ChassisNummer);
            Assert.Equal("1ABC325", v.NummerPlaat);
            Assert.Equal(Brandstoftype_voertuig.Benzine, v.BrandstofType);
            Assert.Equal(Typewagen.Personenwagen, v.TypeWagen);
        }
        [Theory]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_CtorEssential_MerkInValid(string merk)
        {
            Assert.Throws<VoertuigException>(() => new Voertuig(merk, "308", "A15KSLSK124ND523", "1ABC3253", Brandstoftype_voertuig.Benzine, Typewagen.Personenwagen));
        }
        [Theory]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_CtorEssential_ModelInvalid(string model)
        {
            Assert.Throws<VoertuigException>(() => new Voertuig("peugot", model, "A15KSLSK124ND523", "1ABC3253", Brandstoftype_voertuig.Benzine, Typewagen.Personenwagen));
        }
        [Theory]
        [InlineData(" ")]
        [InlineData("A15KSLSK12!ND523")]
        [InlineData("A15KSLSK123ND5235")]
        [InlineData("A15KSLSK123ND52")]
        [InlineData(null)]
        public void Test_CtorEssential_ChassisnummerInValid(string chassisNumer)
        {
            Assert.Throws<VoertuigException>(() => new Voertuig("peugot", "308", chassisNumer, "1ABC3253", Brandstoftype_voertuig.Benzine, Typewagen.Personenwagen));
        }
        [Theory]
        [InlineData(" ")]
        [InlineData("3ABC123")]
        [InlineData("1AB2123")]
        [InlineData("1ABCA23")]
        [InlineData("1ABC1234")]
        [InlineData("AABC123")]
        [InlineData(null)]
        public void Test_CtorEssential_nummerplaatInValid(string nummerplaat)
        {
            Assert.Throws<VoertuigException>(() => new Voertuig("peugot", "308", "A15KSLSK124ND523", nummerplaat, Brandstoftype_voertuig.Benzine, Typewagen.Personenwagen));
        }

        [Fact]
        public void Test_Ctor_All_Valid()
        {
            List<TypeRijbewijs> types = new List<TypeRijbewijs>();
            types.Add(TypeRijbewijs.C);
            Adres adres = new("DiepenBroekStraat", "Balegem", 9860, "1");
            TankKaart tankkaart = new("0123456789", new DateTime(2022, 08, 01));
            Bestuurder bestuurder = new("Ophalvens", "Jarne", adres, new DateTime(1999, 08, 04), "99080455307", tankkaart, types);

            Voertuig voertuig = new("Toyota", "Fiesta", "01234567891234567", "1ABC123", Brandstoftype_voertuig.Benzine, Typewagen.Personenwagen, bestuurder);
            Assert.Equal("Toyota", voertuig.Merk);
            Assert.Equal("Fiesta", voertuig.Model);
            Assert.Equal("01234567891234567", voertuig.ChassisNummer);
            Assert.Equal("1ABC123", voertuig.NummerPlaat);
            Assert.Equal(bestuurder, voertuig.Bestuurder);
            Assert.Equal(tankkaart, voertuig.Bestuurder.TankKaart);
            Assert.Equal(adres, voertuig.Bestuurder.Adres);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_Ctor_All_InvalidMerk(string merk)
        {
            List<TypeRijbewijs> types = new List<TypeRijbewijs>();
            types.Add(TypeRijbewijs.C);
            Adres adres = new("DiepenBroekStraat", "Balegem", 9860, "1");
            TankKaart tankkaart = new("0123456789", new DateTime(2022, 08, 01));
            Bestuurder bestuurder = new("Ophalvens", "Jarne", adres, new DateTime(1999, 08, 04), "99080455307", tankkaart, types);

            Assert.Throws<VoertuigException>(() => new Voertuig(merk, "Fiesta", "01234567891234567", "1ABC123", Brandstoftype_voertuig.Benzine, Typewagen.Personenwagen, bestuurder));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_Ctor_All_InvalidModel(string model)
        {
            List<TypeRijbewijs> types = new List<TypeRijbewijs>();
            types.Add(TypeRijbewijs.C);
            Adres adres = new("DiepenBroekStraat", "Balegem", 9860, "1");
            TankKaart tankkaart = new("0123456789", new DateTime(2022, 08, 01));
            Bestuurder bestuurder = new("Ophalvens", "Jarne", adres, new DateTime(1999, 08, 04), "99080455307", tankkaart, types);

            Assert.Throws<VoertuigException>(() => new Voertuig("Toyota", model, "01234567891234567", "1ABC123", Brandstoftype_voertuig.Benzine, Typewagen.Personenwagen, bestuurder));
        }

        [Fact]
        public void Test_Ctor_All_InvalidChassisnummer()
        {
            List<TypeRijbewijs> types = new List<TypeRijbewijs>();
            types.Add(TypeRijbewijs.C);
            Adres adres = new("DiepenBroekStraat", "Balegem", 9860, "1");
            TankKaart tankkaart = new("0123456789", new DateTime(2022, 08, 01));
            Bestuurder bestuurder = new("Ophalvens", "Jarne", adres, new DateTime(1999, 08, 04), "99080455307", tankkaart, types);

            Assert.Throws<VoertuigException>(() => new Voertuig("Toyota", "Fiesta", null, "1ABC123", Brandstoftype_voertuig.Benzine, Typewagen.Personenwagen, bestuurder));
        }

        [Fact]
        public void Test_Ctor_All_InvalidNummerplaat()
        {
            List<TypeRijbewijs> types = new List<TypeRijbewijs>();
            types.Add(TypeRijbewijs.C);
            Adres adres = new("DiepenBroekStraat", "Balegem", 9860, "1");
            TankKaart tankkaart = new("0123456789", new DateTime(2022, 08, 01));
            Bestuurder bestuurder = new("Ophalvens", "Jarne", adres, new DateTime(1999, 08, 04), "99080455307", tankkaart, types);

            Assert.Throws<VoertuigException>(() => new Voertuig("Toyota", "Fiesta", "01234567891234567", "5ABC123", Brandstoftype_voertuig.Benzine, Typewagen.Personenwagen, bestuurder));
        }
    }
}

