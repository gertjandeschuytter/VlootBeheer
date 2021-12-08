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
        IEnumerable<Voertuig> GeefVoertuigen(string? merk, string? model, string? brandstoftype, string? wagentype, string? kleur, int? aantaldeuren);
        Voertuig geefVoertuig(int voertuigId);
    }
}
