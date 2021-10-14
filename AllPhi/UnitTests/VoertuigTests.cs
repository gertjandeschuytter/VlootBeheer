using System;
using BusinessLayer.Model;
using BusinessLayer.Exceptions;
using Xunit;

namespace UnitTests {
    public class VoertuigTests {
        [Fact]
        public void Test_Ctor_Valid() {
            Voertuig v = new Voertuig("peugot", "308", "A15KSLSK124ND523", "1ABC3253", Brandstoftype.Benzine, Typewagen.personenwagen);
            Assert.Equal("peugot", v.Merk);
            Assert.Equal("308", v.Model);
            Assert.Equal("A15KSLSK124ND523", v.ChassisNummer);
            Assert.Equal("1ABC3253", v.NummerPlaat);
            Assert.Equal(Brandstoftype.Benzine, v.BrandstofType);
            Assert.Equal(Typewagen.personenwagen, v.TypeWagen);
        }
        [Theory]
        [InlineData("A15KSLSK12!ND523")]
        [InlineData("A15KSLSK12?ND523")]
        public void Test_Ctor_SC_Chassisnummer_InValid(string chassisNumer) {
            Assert.Throws<VoertuigException>(() => new Voertuig("peugot", "308", chassisNumer, "1ABC3253", Brandstoftype.Benzine, Typewagen.personenwagen));
        }
        [Fact]
        public void Test_ctor_noMerk() {
            Assert.Throws<VoertuigException>(() => new Voertuig(null, "308", "A15KSLSK124ND523", "1ABC3253", Brandstoftype.Benzine, Typewagen.personenwagen));
        }
        [Fact]
        public void Test_ctor_noModel() {
            Assert.Throws<VoertuigException>(() => new Voertuig("peugot", null, "A15KSLSK124ND523", "1ABC3253", Brandstoftype.Benzine, Typewagen.personenwagen));
        }
        [Fact]
        public void Test_ctor_noChassisNummer() {
            Assert.Throws<VoertuigException>(() => new Voertuig("peugot", "308", null, "1ABC3253", Brandstoftype.Benzine, Typewagen.personenwagen));
        }
        [Fact]
        public void Test_ctor_noNummerPlaat() {
            Assert.Throws<VoertuigException>(() => new Voertuig("peugot", "308", "A15KSLSK124ND523", null, Brandstoftype.Benzine, Typewagen.personenwagen));
        }
    }
}
