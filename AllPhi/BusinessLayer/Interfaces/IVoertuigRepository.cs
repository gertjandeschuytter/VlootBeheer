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
        bool BestaatVoertuig(string chassisNummer);
        bool BestaatVoertuig(Voertuig voertuig);
        IEnumerable<Voertuig> GeefVoertuigen(Bestuurder bestuurder);
    }
}
