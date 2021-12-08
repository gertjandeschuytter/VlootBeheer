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
        bool BestaatBestuurder(int id);
        Bestuurder GeefBestuurder(int id);
        IEnumerable<Bestuurder> GeefBestuurders(string? voornaam, string? naam, string? rijksregister, DateTime? geboortedatum);
    }
}
