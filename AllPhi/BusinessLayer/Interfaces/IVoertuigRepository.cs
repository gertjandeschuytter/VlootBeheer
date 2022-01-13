using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces {
    public interface IVoertuigRepository {
        public void VoegVoertuigToe(Voertuig voertuig);
        public void VerwijderVoertuig(Voertuig voertuig);
        public void UpdateVoertuig(Voertuig voertuig);
        bool BestaatVoertuig(int VoertuigId);
        bool BestaatChassisnummer(string chassisnummer);
        bool BestaatVoertuig(Voertuig Voertuig);
        IReadOnlyList<Voertuig> GeefVoertuigen(string? merk, string? model,string? chassisnummer, string? nummerplaat, string? brandstoftype, string? wagentype, string? kleur, int? aantaldeuren, string? naamBestuurder);
        Voertuig geefVoertuig(int voertuigId);
        bool BestaatNummerplaat(string nummerPlaat);
    }
}
