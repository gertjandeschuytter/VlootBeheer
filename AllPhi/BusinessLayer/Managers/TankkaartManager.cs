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
    public class TankkaartManager
    {
        private ITankkaartRepository Repo;
        public TankkaartManager(ITankkaartRepository repo)
        {
            Repo = repo;
        }
        public void VoegTankkaartToe(TankKaart tankkaart)
        {
            try
            {
                if (Repo.BestaatTankkaart(tankkaart.TankkaartId))
                {
                    throw new TankkaartManagerException("TankkaartManager: VoegTankkaartToe - tankkaart bestaat al");
                }
                else
                {
                    Repo.VoegTankkaartToe(tankkaart);
                }
            }
            catch(Exception ex)
            {
                throw new TankkaartManagerException("TankkaartManager: VoegTankkaartToe", ex);
            }
        }
        public void VerwijderTankkaart(TankKaart tankkaart)
        {
            try
            {
                if (!Repo.BestaatTankkaart(tankkaart.TankkaartId))
                {
                    throw new TankkaartManagerException("TankkaartManager: VerwijderTankkaart - tankkaart bestaat niet");
                }
                else
                {
                    Repo.VerwijderTankkaart(tankkaart);
                }
            }
            catch(Exception ex)
            {
                throw new TankkaartManagerException("TankkaartManager: VerwijderTankkaart", ex);
            }
        }
        public void UpdateTankkaart(TankKaart tankkaart)
        {
            try
            {
                if (Repo.BestaatTankkaart(tankkaart.TankkaartId))
                {
                    TankKaart dbTankkaart = Repo.GeefTankkaart(tankkaart.TankkaartId);
                    if(dbTankkaart == tankkaart)
                    {
                        throw new TankkaartManagerException("TankkaartManager: UpdateTankkaart - geen verschillen");
                    }
                    else
                    {
                        Repo.UpdateTankkaart(tankkaart);
                    }
                }
                else
                {
                    throw new TankkaartManagerException("TankkaartManager: UpdateTankkaart - tankkaart bestaat niet");
                }
            }
            catch(Exception ex)
            {
                throw new TankkaartManagerException("TankkaartManager: UpdateTankkaart", ex);
            }
        }
        public IReadOnlyList<TankKaart> ZoekTankkaarten(string kaartNr, DateTime geldigheidsdatum, string pincode, Bestuurder bestuurder, bool geblokkeerd)
        {
            List<TankKaart> tankkaarten = new List<TankKaart>();

            try
            {
                if(!string.IsNullOrWhiteSpace(kaartNr) || geldigheidsdatum != DateTime.MinValue || !string.IsNullOrWhiteSpace(pincode) || bestuurder != null)
                {
                    tankkaarten.AddRange(Repo.GeefTankkaarten(kaartNr, geldigheidsdatum, pincode, bestuurder, geblokkeerd));
                }
                else
                {
                    throw new TankkaartManagerException("TankkaartManager: ZoekTankkaarten - geen zoekcriteria");
                }
                return tankkaarten;
            }
            catch(Exception ex)
            {
                throw new TankkaartManagerException("TankkaartManager: ZoekTankkaarten", ex);
            }
        }
    }
}
