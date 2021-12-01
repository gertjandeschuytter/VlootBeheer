using BusinessLayer.Model;
using FleetDatabase;
using System;
using System.Collections.Generic;

namespace AllPhiConsoleApp {
    class Program {
        static void Main(string[] args) {
            BestuurderRepositoryADO b = new BestuurderRepositoryADO(@"Data Source=LAPTOP-JARNE\SQLEXPRESS;Initial Catalog=fleet;Integrated Security=True");
            List<TypeRijbewijs> list = new List<TypeRijbewijs>();
            list.Add(TypeRijbewijs.A);
            Bestuurder bestuurder = new Bestuurder("Ophalvens", "Jarne", new DateTime(1999,08,04), "99080455307", list);

            b.VoegBestuurderToe(bestuurder);

            bestuurder.VoegRijbewijsToe(TypeRijbewijs.C);
            Voertuig v = new Voertuig("Toyota", "393", "12345678911234567", "1ABC123", Brandstoftype_voertuig.Diesel, Typewagen.personenwagen);
            bestuurder.ZetVoertuig(v);

            b.WijzigBestuurder(bestuurder);
        }
    }
}