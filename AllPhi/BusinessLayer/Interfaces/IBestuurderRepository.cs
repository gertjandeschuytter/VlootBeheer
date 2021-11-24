using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IBestuurderRepository
    {
        void VoegBestuurderToe(Bestuurder bestuurder);
        void VerwijderBestuurder(Bestuurder bestuurder);
        void WijzigBestuurder(Bestuurder bestuurder);
        bool HeeftBestuurder(Bestuurder bestuurder);
        Bestuurder GeefBestuurder(int id);
        IEnumerable<Bestuurder> GeefBestuurders(string naam, string voornaam, Adres adres, DateTime datum, string rijksregister, List<TypeRijbewijs> types, Voertuig v, TankKaart t);
    }
}
