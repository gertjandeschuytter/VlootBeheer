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
        public void VoegVoertuigToe(Voertuig voertuig)
        {
            if (repo.BestaatVoertuig(voertuig)) throw new VoertuigManagerException("Voertuig al gekend");
            repo.VoegVoertuigToe(voertuig);
        }
        public void VerwijderBestuurder(Voertuig voertuig)
        {
            if (!repo.BestaatVoertuig(voertuig)) throw new VoertuigManagerException("Voertuig bestaat niet");
            repo.VerwijderVoertuig(voertuig);
        }
        public void WijzigVoertuig(Voertuig voertuig)
        {
            if (!repo.BestaatVoertuig(voertuig.ID)) throw new VoertuigManagerException("Voertuig bestaat niet");
            if (repo.BestaatVoertuig(voertuig)) throw new VoertuigManagerException("Voertuig is niet veranderd");
            repo.UpdateVoertuig(voertuig);
        }

        public IReadOnlyList<Voertuig> GeefVoertuig(int? id, string? merk, string? model, string? chassisnummer, string? nummerplaat, string? kleur, int? aantalDeuren, string? brandstoftype, string? typewagen)
        {
            List<Voertuig> list = new();
            if (id.HasValue)
                list.Add(repo.geefVoertuig((int)id));
            list = (List<Voertuig>)repo.GeefVoertuigen(merk, model, chassisnummer, nummerplaat, brandstoftype, typewagen, kleur, aantalDeuren);
            return list.AsReadOnly();
        }

    }
}
