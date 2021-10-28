using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Managers
{
    public class BestuurderManager
    {
        private IBestuurderRepository repo;

        public BestuurderManager(IBestuurderRepository repo)
        {
            this.repo = repo;
        }

        public void VoegBestuurderToe(Bestuurder bestuurder)
        {
            if (repo.HeeftBestuurder(bestuurder)) throw new BestuurderManagerException("Bestuurder al gekend");
            repo.VoegBestuurderToe(bestuurder);
        }

        public void VerwijderBestuurder(Bestuurder bestuurder)
        {if (!repo.HeeftBestuurder(bestuurder)) throw new BestuurderManagerException("Bestuurder bestaat niet");
            repo.VerwijderBestuurder(bestuurder);
        }

        public void WijzigBestuurder(Bestuurder bestuurder)
        {
            if (!repo.HeeftBestuurder(bestuurder)) throw new BestuurderManagerException("Bestuurder bestaat niet");
            repo.WijzigBestuurder(bestuurder);
        }
    }
}
