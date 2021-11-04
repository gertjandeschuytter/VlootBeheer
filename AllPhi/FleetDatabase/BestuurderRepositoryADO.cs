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
            throw new NotImplementedException();
        }

        public IEnumerable<Bestuurder> GeefBestuurders(string naam, string voornaam, Func<string> toString, DateTime datum, string rijksregister, List<TypeRijbewijs> types, Voertuig v, TankKaart t)
        {
            throw new NotImplementedException();
        }

        public bool HeeftBestuurder(Bestuurder bestuurder)
        {
            throw new NotImplementedException();
        }

        public void VerwijderBestuurder(Bestuurder bestuurder)
        {
            throw new NotImplementedException();
        }

        public void VoegBestuurderToe(Bestuurder bestuurder)
        {
            throw new NotImplementedException();
        }

        public void WijzigBestuurder(Bestuurder bestuurder)
        {
            throw new NotImplementedException();
        }
    }
}
