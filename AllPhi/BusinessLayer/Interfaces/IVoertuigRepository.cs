using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces {
    public interface IVoertuigRepository {
        void VoegVoertuigToe(Voertuig voertuig);
        void UpdateVoertuig(Voertuig voertuig);
        void VerwijderVoertuig(Voertuig voertuig);
        bool BestaatVoertuig(string chassisNummer);
        Voertuig Geefvoertuig(int? chassisNummer, string Model, string Merk);
    }
}
