using BusinessLayer.Interfaces;
using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Managers {
    public class VoertuigManager {
        private IVoertuigRepository repo;
        public VoertuigManager(IVoertuigRepository repo) {
            this.repo = repo;
        }
        public void VoegVoertuigToe(Voertuig voertuig) {
            if (repo.BestaatVoertuig(voertuig.ChassisNummer)) throw new VoertuigManagerException("Voertuig al gekend");
            repo.VoegVoertuigToe(voertuig);
        }
        public void VerwijderBestuurder(Voertuig voertuig) {
            if (!repo.BestaatVoertuig(voertuig.ChassisNummer)) throw new VoertuigManagerException("Voertuig bestaat niet");
            repo.VerwijderVoertuig(voertuig);
        }
        public void WijzigBestuurder(Voertuig voertuig) {
            if (!repo.BestaatVoertuig(voertuig.ChassisNummer)) throw new VoertuigManagerException("Voertuig bestaat niet");
            repo.UpdateVoertuig(voertuig);
        }
        public void ZoekVoertuig(string chassisNummer) {
            if (!repo.BestaatVoertuig(chassisNummer)) throw new VoertuigManagerException("Voertuig bestaat niet");
            repo.Geefvoertuig(chassisNummer);
        }
    }
}
