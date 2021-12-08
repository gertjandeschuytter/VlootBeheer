using System;
using BusinessLayer.Model;
using BusinessLayer.Exceptions;
using Xunit;
using NPOI.SS.Formula.Functions;
using System.Collections.Generic;

namespace UnitTests
{
    //public class BestuurderTests
    //{
    //    #region Tests_Ctor_Essentials
    //    [Fact]
    //    public void Test_CtorEssential_Valid()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Bestuurder bestuurder = new("De Smet", "Ruben", new DateTime(1999, 08, 04), "99080455307", types);
    //        Assert.Equal("De Smet", bestuurder.Naam);
    //        Assert.Equal("Ruben", bestuurder.VoorNaam);
    //        Assert.Equal(new DateTime(1999, 08, 04), bestuurder.GeboorteDatum);
    //        Assert.Equal("99080455307", bestuurder.RijksRegisterNr);
    //        Assert.Equal(types, bestuurder._Types);
    //    }

    //    [Theory]
    //    [InlineData("")]
    //    [InlineData(" ")]
    //    [InlineData(null)]
    //    public void Test_CtorEssential_NameInValid(string name)
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Assert.Throws<BestuurderException>(() => new Bestuurder(name, "Ruben", new DateTime(1999, 08, 04), "99080455307", types));
    //    }

    //    [Theory]
    //    [InlineData("")]
    //    [InlineData(" ")]
    //    [InlineData(null)]
    //    public void Test_CtorEssential_VoornaamInValid(string voornaam)
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Assert.Throws<BestuurderException>(() => new Bestuurder("Vermeire", voornaam, new DateTime(1999, 08, 04), "99080455307", types));
    //    }

    //    [Theory]
    //    [InlineData("1/1/1")]
    //    public void Test_CtorEssential_DatumInValid(string datum_str)
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        DateTime datum = DateTime.Parse(datum_str);
    //        Assert.Throws<BestuurderException>(() => new Bestuurder("Vermeire", "GertJan", datum, "99080455307", types));
    //    }

    //    [Theory]
    //    [InlineData("1")]
    //    [InlineData("01234567891")]
    //    [InlineData("99080455308")]
    //    public void Test_CtorEssential_RijksRegisterNrInValid(string rijksregister)
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Assert.Throws<BestuurderException>(() => new Bestuurder("Vermeire", "GertJan", DateTime.Today, rijksregister, types));
    //    }
    //    #endregion

    //    #region Tests_Ctor_All
    //    [Fact]
    //    public void Test_Ctor_All_Valid()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Adres a = new("DiepenBroekStraat", "Balegem", "9860", 1);
    //        Voertuig v = new("Toyota", "Fiesta", "01234567891234567", "1ABC123", Brandstoftype_voertuig.Benzine, Typewagen.personenwagen);
    //        TankKaart t = new("0123456789", new DateTime(2022, 08, 01));

    //        Bestuurder bestuurder = new("Ophalvens", "Jarne", a, new DateTime(1999, 08, 04), "99080455307", v, t, types);
    //        Assert.Equal("Ophalvens", bestuurder.Naam);
    //        Assert.Equal("Jarne", bestuurder.VoorNaam);
    //        Assert.Equal(a, bestuurder.Adres);
    //        Assert.Equal(new DateTime(1999, 08, 04), bestuurder.GeboorteDatum);
    //        Assert.Equal("99080455307", bestuurder.RijksRegisterNr);
    //        Assert.Equal(v, bestuurder.Voertuig);
    //        Assert.Equal(t, bestuurder.TankKaart);
    //        Assert.Equal(types, bestuurder._Types);
    //    }

    //    [Theory]
    //    [InlineData("")]
    //    [InlineData(" ")]
    //    [InlineData(null)]
    //    public void Test_Ctor_All_InvalidNaam(string naam)
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Adres a = new("DiepenBroekStraat", "Balegem", "9860", 1);
    //        Voertuig v = new("Toyota", "Fiesta", "01234567891234567", "1ABC123", Brandstoftype_voertuig.Benzine, Typewagen.personenwagen);
    //        TankKaart t = new("0123456789", new DateTime(2022, 08, 01));

    //        Assert.Throws<BestuurderException>(() => new Bestuurder(naam, "Jarne", a, new DateTime(1999, 08, 04), "99080455307", v, t, types));
    //    }

    //    [Theory]
    //    [InlineData("")]
    //    [InlineData(" ")]
    //    [InlineData(null)]
    //    public void Test_Ctor_All_InvalidVoornaam(string voornaam)
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Adres a = new("DiepenBroekStraat", "Balegem", "9860", 1);
    //        Voertuig v = new("Toyota", "Fiesta", "01234567891234567", "1ABC123", Brandstoftype_voertuig.Benzine, Typewagen.personenwagen);
    //        TankKaart t = new("0123456789", new DateTime(2022, 08, 01));

    //        Assert.Throws<BestuurderException>(() => new Bestuurder("Ophalvens", voornaam, a, new DateTime(1999, 08, 04), "99080455307", v, t, types));
    //    }

    //    [Fact]
    //    public void Test_Ctor_All_InvalidAdres()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Voertuig v = new("Toyota", "Fiesta", "01234567891234567", "1ABC123", Brandstoftype_voertuig.Benzine, Typewagen.personenwagen);
    //        TankKaart t = new("0123456789", new DateTime(2022, 08, 01));
    //        Assert.Throws<BestuurderException>(() => new Bestuurder("Ophalvens", "Harry", null, new DateTime(1999, 08, 04), "99080455307", v, t, types));
    //    }

    //    [Fact]
    //    public void Test_Ctor_All_InvalidDatum()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Adres a = new("DiepenBroekStraat", "Balegem", "9860", 1);
    //        Voertuig v = new("Toyota", "Fiesta", "01234567891234567", "1ABC123", Brandstoftype_voertuig.Benzine, Typewagen.personenwagen);
    //        TankKaart t = new("0123456789", new DateTime(2022, 08, 01));

    //        Assert.Throws<BestuurderException>(() => new Bestuurder("Ophalvens", "Jarne", a, new DateTime(1, 1, 1), "99080555307", v, t, types));
    //    }

    //    [Theory]
    //    [InlineData("")]
    //    [InlineData(" ")]
    //    [InlineData(null)]
    //    [InlineData("9908045530")]
    //    [InlineData("20073155307")]
    //    [InlineData("99080455309")]
    //    public void Test_Ctor_All_InvalidRijksRegisterNummer(string rijks)
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Adres a = new("DiepenBroekStraat", "Balegem", "9860", 1);
    //        Voertuig v = new("Toyota", "Fiesta", "01234567891234567", "1ABC123", Brandstoftype_voertuig.Benzine, Typewagen.personenwagen);
    //        TankKaart t = new("0123456789", new DateTime(2022, 08, 01));

    //        Assert.Throws<BestuurderException>(() => new Bestuurder("Ophalvens", "Jarne", a, new DateTime(1999,8,4), rijks, v, t, types));
    //    }

    //    [Fact]
    //    public void Test_Ctor_All_InvalidVoertuig()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Adres a = new("DiepenBroekStraat", "Balegem", "9860", 1);
    //        TankKaart t = new("0123456789", new DateTime(2022, 08, 01));

    //        Assert.Throws<BestuurderException>(() => new Bestuurder("Ophalvens", "Jarne", a, new DateTime(1999, 8, 4), "99080455307", null, t, types));
    //    }

    //    [Fact]
    //    public void Test_Ctor_All_InvalidTankKaart()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Adres a = new("DiepenBroekStraat", "Balegem", "9860", 1);
    //        Voertuig v = new("Toyota", "Fiesta", "01234567891234567", "1ABC123", Brandstoftype_voertuig.Benzine, Typewagen.personenwagen);

    //        Assert.Throws<BestuurderException>(() => new Bestuurder("Ophalvens", "Jarne", a, new DateTime(1999, 8, 4), "99080455307", v, null, types));
    //    }
    //    #endregion

    //    #region Methods

    //    #region Valid
    //    #region Setters
    //    [Fact]
    //    public void Test_ZetNaam_Valid()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307", types);
    //        b.ZetNaam("Souffriau");
    //        Assert.Equal("Souffriau", b.Naam);
    //    }

    //    [Fact]
    //    public void Test_ZetVoorNaam_Valid()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307", types);
    //        b.ZetVoorNaam("Karel");
    //        Assert.Equal("Karel", b.VoorNaam);
    //    }

    //    [Fact]
    //    public void Test_ZetGeboorteDatum_Valid()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307", types);
    //        b.ZetGeboorteDatum(new DateTime(2000, 10, 20));
    //        Assert.Equal(new DateTime(2000, 10, 20), b.GeboorteDatum);
    //    }

    //    [Fact]
    //    public void Test_ZetRijksRegisterNummer_Valid()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307", types);
    //        b.ZetRijksRegisterNummer("99080455307");
    //        Assert.Equal("99080455307", b.RijksRegisterNr);
    //    }

    //    [Fact]
    //    public void Test_ZetAdres_Valid()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307", types);
    //        Adres a = new("Boerestraat", "Balegem", "9860", 1);
    //        b.ZetAdres(a);
    //        Assert.Equal(a, b.Adres);
    //    }

    //    [Fact]
    //    public void Test_ZetVoertuig_Valid()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307", types);
    //        Voertuig v = new("Toyota", "308", "01234567891234567", "1ABC123", Brandstoftype_voertuig.Benzine, Typewagen.personenwagen);
    //        b.ZetVoertuig(v);
    //        Assert.Equal(v, b.Voertuig);
    //        Assert.Equal(b, v.Bestuurder);
    //    }

    //    [Fact]
    //    public void Test_ZetTankKaart_Valid()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307", types);
    //        TankKaart t = new("012345678912345", new DateTime(2022, 10, 30));
    //        b.ZetTankKaart(t);
    //        Assert.Equal(t, b.TankKaart);
    //        Assert.Equal(b, t.Bestuurder);
    //    }
    //    #endregion

    //    #region Others
    //    [Fact]
    //    public void Test_VoegRijbewijsToe_Valid()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307", types);
    //        b.VoegRijbewijsToe(TypeRijbewijs.A);
    //        Assert.Contains(TypeRijbewijs.A, b._Types);
    //    }

    //    [Fact]
    //    public void Test_VoegRijbewijzenToe_Valid()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        types.Add(TypeRijbewijs.A);
    //        Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307", types);
    //        Assert.Contains(TypeRijbewijs.A, b._Types);
    //    }

    //    [Fact]
    //    public void Test_VerwijderRijbewijs_Valid()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307", types);
    //        b.VerwijderRijbewijs(TypeRijbewijs.C);
    //        Assert.Empty(b._Types);
    //    }

    //    [Fact]
    //    public void Test_VerwijderVoertuig_Valid()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307", types);
    //        Voertuig v = new("Toyota", "308", "01234567891234567", "1ABC123", Brandstoftype_voertuig.Benzine, Typewagen.personenwagen);
    //        b.ZetVoertuig(v);
    //        b.VerwijderVoertuig(v);
    //        Assert.Null(b.Voertuig);
    //        Assert.Null(v.Bestuurder);
    //    }

    //    [Fact]
    //    public void Test_VerwijderTankKaart_Valid()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307", types);
    //        TankKaart t = new("012345678912345", new DateTime(2022, 10, 30));
    //        b.ZetTankKaart(t);
    //        b.VerwijderTankKaart(t);
    //        Assert.Null(b.TankKaart);
    //        Assert.Null(t.Bestuurder);
    //    }
    //    #endregion
    //    #endregion

    //    #region Invalid
    //    #region Setters
    //    [Theory]
    //    [InlineData("")]
    //    [InlineData(" ")]
    //    [InlineData(null)]
    //    public void Test_ZetNaam_Invalid(string naam)
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307", types);
    //        Assert.Throws<BestuurderException>(() => b.ZetNaam(naam));
    //    }

    //    [Theory]
    //    [InlineData("")]
    //    [InlineData(" ")]
    //    [InlineData(null)]
    //    public void Test_ZetVoorNaam_Invalid(string voornaam)
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307", types);
    //        Assert.Throws<BestuurderException>(() => b.ZetVoorNaam(voornaam));
    //    }

    //    [Fact]
    //    public void Test_ZetGeboorteDatum_Invalid()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307", types);
    //        Assert.Throws<BestuurderException>(() => b.ZetGeboorteDatum(new DateTime(1,1,1)));
    //    }

    //    [Theory]
    //    [InlineData("")]
    //    [InlineData(" ")]
    //    [InlineData(null)]
    //    [InlineData("88020155307")]
    //    [InlineData("99080455371")]
    //    [InlineData("1")]
    //    [InlineData("9999999999999")]
    //    public void Test_ZetRijksRegisterNummer_Invalid(string rijksRegist)
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307", types);
    //        Assert.Throws<BestuurderException>(() => b.ZetRijksRegisterNummer(rijksRegist));
    //    }

    //    [Fact]
    //    public void Test_ZetAdres_Invalid()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307", types);
    //        Assert.Throws<BestuurderException>(() => b.ZetAdres(null));
    //    }

    //    [Fact]
    //    public void Test_ZetVoertuig_Invalid()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307", types);
    //        Assert.Throws<BestuurderException>(() => b.ZetVoertuig(null));
    //    }

    //    [Fact]
    //    public void Test_ZetTankKaart_Invalid()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307", types);
    //        Assert.Throws<BestuurderException>(() => b.ZetTankKaart(null));
    //    }
    //    #endregion

    //    #region Others
    //    [Fact]
    //    public void Test_VoegRijbewijsToe_Invalid()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307", types);
    //        b.VoegRijbewijsToe(TypeRijbewijs.C);
    //        Assert.Throws<BestuurderException>(() => b.VoegRijbewijsToe(TypeRijbewijs.C));
    //    }

    //    [Fact]
    //    public void Test_VerwijderRijbewijs_Invalid()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.A);
    //        Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307", types);
    //        Assert.Throws<BestuurderException>(() => b.VerwijderRijbewijs(TypeRijbewijs.C));
    //    }

    //    [Fact]
    //    public void Test_VerwijderVoertuig_Invalid()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307", types);
    //        Voertuig v = new("Toyota", "308", "01234567891234567", "1ABC123", Brandstoftype_voertuig.Benzine, Typewagen.personenwagen);
    //        Assert.Throws<BestuurderException>(() => b.VerwijderVoertuig(v));
    //    }

    //    [Fact]
    //    public void Test_VerwijderTankKaart_Invalid()
    //    {
    //        List<TypeRijbewijs> types = new List<TypeRijbewijs>();
    //        types.Add(TypeRijbewijs.C);
    //        Bestuurder b = new("Ophalvens", "Jarne", new DateTime(1999, 08, 04), "99080455307", types);
    //        TankKaart t = new("012345678912345", new DateTime(2022, 10, 30));
    //        Assert.Throws<BestuurderException>(() => b.VerwijderTankKaart(t));
    //    }
    //    #endregion
    //    #endregion
    //    #endregion
    //}
}
