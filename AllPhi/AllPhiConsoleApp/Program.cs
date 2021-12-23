using BusinessLayer.Model;
using FleetDatabase;
using System;
using System.Collections.Generic;

namespace AllPhiConsoleApp {
    class Program {
        static void Main(string[] args) {
            BestuurderRepositoryADO b = new BestuurderRepositoryADO(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Fleet;Integrated Security=True");
            //List<TypeRijbewijs> list = new List<TypeRijbewijs>();
            //list.Add(TypeRijbewijs.A);
            //b.GeefBestuurder(4);
            //b.BestaatBestuurder(2);
            //Bestuurder bestuurder = new Bestuurder("Pol", "Segers", new DateTime(1999,08,04), "99080455307", list);
            //Bestuurder bestuurder = b.GeefBestuurder(4);
            //bestuurder.Voertuig.ZetModel("SportBack f6");
            //bestuurder.TankKaart.ZetPincode("4321");
            //b.WijzigBestuurder(bestuurder);
            //Bestuurder bestuurder = b.GeefBestuurder(4);
            //b.GeefBestuurders("Gertjan", null, null, null);
            //b.VerwijderBestuurder(bestuurder);
            //b.VoegBestuurderToe(bestuurder);
            //bestuurder.VoegRijbewijsToe(TypeRijbewijs.C);
            //Voertuig v = new Voertuig("Toyota", "393", "12345678911234567", "1ABC123", Brandstoftype_voertuig.Diesel, Typewagen.personenwagen);
            //bestuurder.ZetVoertuig(v);
            //b.WijzigBestuurder(bestuurder);
            VoertuigRepositoryADO v = new VoertuigRepositoryADO(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Fleet;Integrated Security=True");
            //v.GeefVoertuigen("Toyota", "Yaris", "Elektrisch","Personenwagen","Rood",5);
            //Voertuig voertuig1 = v.geefVoertuig(1);
            //voertuig1.ZetAantalDeuren(8);
            //v.UpdateVoertuig(voertuig1);
            //Voertuig voertuig = v.geefVoertuig(1);
            //voertuig.ZetKleur("Paars");
            //voertuig.ZetModel("Bandito");
            //v.VoegVoertuigToe(voertuig);
            TankkaartRepositoryADO tk = new(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Fleet;Integrated Security=True");
            //tk.BestaatTankkaart(2);
            tk.GeefTankkaart(2);
        }
    }
}