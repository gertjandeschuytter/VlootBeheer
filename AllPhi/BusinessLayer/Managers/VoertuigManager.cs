using BusinessLayer.Interfaces;
using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Managers {
    public class VoertuigManager {
        private IVoertuigRepository _vrepo;
        private IBestuurderRepository _brepo;
        public VoertuigManager(IVoertuigRepository voertuigrepo, IBestuurderRepository bestuurderrepo) {
            this._vrepo = voertuigrepo;
            this._brepo = bestuurderrepo;
        }
        public void VoegVoertuigToe(Voertuig voertuig)
        {
            if (_vrepo.BestaatVoertuig(voertuig)) throw new VoertuigManagerException("Voertuig al gekend");
            if (_vrepo.BestaatChassisnummer(voertuig.ChassisNummer)) throw new VoertuigManagerException("Er bestaat al een voertuig met dit chassisnummer");
            if (_vrepo.BestaatNummerplaat(voertuig.NummerPlaat)) throw new VoertuigManagerException("Er bestaat al een voertuig met dit nummerplaat");
            if (voertuig.Bestuurder != null)
            {
                if (_brepo.HeeftVoertuig(voertuig.Bestuurder)) throw new VoertuigManagerException("Meegegeven bestuurder heeft al een voertuig");
            }
            _vrepo.VoegVoertuigToe(voertuig);
        }
        public void VerwijderVoertuig(Voertuig voertuig)
        {
            if (!_vrepo.BestaatVoertuig(voertuig.ID)) throw new VoertuigManagerException("Voertuig bestaat niet");
            _vrepo.VerwijderVoertuig(voertuig);
        }
        public void WijzigVoertuig(Voertuig voertuig)
        {
            //checken als chassisnummer en nummerplaat al bestaan
            Voertuig vdb = _vrepo.geefVoertuig(voertuig.ID);
            if (voertuig.NummerPlaat != vdb.NummerPlaat)
            {
                if (_vrepo.BestaatNummerplaat(voertuig.NummerPlaat)) throw new VoertuigManagerException("Nummerplaat bestaat al in het systeem");
            }
            if (voertuig.ChassisNummer != vdb.ChassisNummer)
            {
                if (_vrepo.BestaatChassisnummer(voertuig.ChassisNummer)) throw new VoertuigManagerException("Chassisnummer bestaat al in het systeem");
            }
            if (!_vrepo.BestaatVoertuig(voertuig.ID)) throw new VoertuigManagerException("Voertuig bestaat niet");
            if (_vrepo.BestaatVoertuig(voertuig)) throw new VoertuigManagerException("Voertuig is niet veranderd");
            Bestuurder nieuweBestuurder = null;
            if (voertuig.Bestuurder != null)
            {
                nieuweBestuurder = _brepo.GeefBestuurder(voertuig.Bestuurder.BestuurderId);
                if (nieuweBestuurder.Voertuig != null && nieuweBestuurder.Voertuig != voertuig) throw new VoertuigManagerException("Deze bestuurder heeft al een wagen, gelieve deze eerst te verwijderen vooraleer je een nieuwe wagen geeft");
            }
            _vrepo.UpdateVoertuig(voertuig);
        }

        public IReadOnlyList<Voertuig> GeefVoertuig(int? id, string? merk, string? model, string? chassisnummer, string? nummerplaat, string? kleur, int? aantalDeuren, string? brandstoftype, string? typewagen, string? naamBestuurder)
        {
            List<Voertuig> list = new();
            if (id.HasValue)
                list.Add(_vrepo.geefVoertuig((int)id));
            else
            list = (List<Voertuig>)_vrepo.GeefVoertuigen(merk, model, chassisnummer, nummerplaat, brandstoftype, typewagen, kleur, aantalDeuren, naamBestuurder);
            return list.AsReadOnly();
        }

    }
}
