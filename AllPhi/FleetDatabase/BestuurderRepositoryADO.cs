using System;
using System.Collections.Generic;
using BusinessLayer.Interfaces;
using BusinessLayer.Model;

namespace FleetDatabase
{
    public class BestuurderRepositoryADO : IBestuurderRepository
    {
        public Bestuurder GeefBestuurder(int id)
        {
            string query = "SELECT * FROM bestuurder WHERE ID = @id;";

            throw new NotImplementedException();
        }

        public IEnumerable<Bestuurder> GeefBestuurders(string naam, string voornaam, string adres, DateTime datum, string rijksregister, List<TypeRijbewijs> types, Voertuig v, TankKaart t)
        {
            string query = "SELECT * FROM bestuurder WHERE voornaam = @voornaam, naam = @naam,geboortedatum = @datum, adres = @adres, rijksregister = @rijksregister, rijbewijs = @types, Voertuig = @v, tankkaart = @t;";

            throw new NotImplementedException();
        }

        public bool HeeftBestuurder(Bestuurder bestuurder)
        {
            string query = "";

            throw new NotImplementedException();
        }

        public void VerwijderBestuurder(Bestuurder bestuurder)
        {
            string query = "";

            throw new NotImplementedException();
        }

        public void VoegBestuurderToe(Bestuurder bestuurder)
        {
            string query = "INSERT INTO bestuurder(ID, voornaam, naam, geboortedatum, adres, rijksregister, rijbewijs, Voertuig, tankkaart) VALUES (@id, @voornaam, @naam, @datum, @adres, @rijksregister, @types, @v, @t);";

            throw new NotImplementedException();
        }

        public void WijzigBestuurder(Bestuurder bestuurder)
        {
            string query = "";

            throw new NotImplementedException();
        }
    }
}
