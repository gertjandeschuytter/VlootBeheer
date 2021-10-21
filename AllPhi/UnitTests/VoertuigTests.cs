using System;
using BusinessLayer.Model;
using BusinessLayer.Exceptions;
using Xunit;

namespace UnitTests {
    public class VoertuigTests {
        //eigen tests
        [Fact]
        public void Test_CtorEssential_Valid() {
            Voertuig v = new("peugot", "308", "A15KSLSK124ND523A", "1ABC325", Brandstoftype.Benzine, Typewagen.personenwagen);
            Assert.Equal("peugot", v.Merk);
            Assert.Equal("308", v.Model);
            Assert.Equal("A15KSLSK124ND523A", v.ChassisNummer);
            Assert.Equal("1ABC325", v.NummerPlaat);
            Assert.Equal(Brandstoftype.Benzine, v.BrandstofType);
            Assert.Equal(Typewagen.personenwagen, v.TypeWagen);
        }
        [Theory]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_CtorEssential_MerkInValid(string merk) {
            Assert.Throws<VoertuigException>(() => new Voertuig(merk, "308", "A15KSLSK124ND523", "1ABC3253", Brandstoftype.Benzine, Typewagen.personenwagen));
        }
        [Theory]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_CtorEssential_ModelInvalid(string model) {
            Assert.Throws<VoertuigException>(() => new Voertuig("peugot", model, "A15KSLSK124ND523", "1ABC3253", Brandstoftype.Benzine, Typewagen.personenwagen));
        }
        [Theory]
        [InlineData(" ")]
        [InlineData("A15KSLSK12!ND523")]
        [InlineData("A15KSLSK123ND5235")]
        [InlineData("A15KSLSK123ND52")]
        [InlineData(null)]
        public void Test_CtorEssential_ChassisnummerInValid(string chassisNumer) {
            Assert.Throws<VoertuigException>(() => new Voertuig("peugot", "308", chassisNumer, "1ABC3253", Brandstoftype.Benzine, Typewagen.personenwagen));
        }
        [Theory]
        [InlineData(" ")]
        [InlineData("3ABC123")]
        [InlineData("1AB2123")]
        [InlineData("1ABCA23")]
        [InlineData("1ABC1234")]
        [InlineData("AABC123")]
        [InlineData(null)]
        public void Test_CtorEssential_nummerplaatInValid(string nummerplaat) {
            Assert.Throws<VoertuigException>(() => new Voertuig("peugot", "308", "A15KSLSK124ND523", nummerplaat, Brandstoftype.Benzine, Typewagen.personenwagen));
        }
    }
}
