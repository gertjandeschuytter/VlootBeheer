using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Managers {
    public class TankkaartManager {
        private ITankkaartRepository Repo;
        private IBestuurderRepository _brepo;
        public TankkaartManager(ITankkaartRepository repo, IBestuurderRepository brepo)
        {
            Repo = repo;
            _brepo = brepo;
        }
        public void VoegTankkaartToe(TankKaart tankkaart)
        {
            try
            {
                if (Repo.BestaatTankkaartNummer(tankkaart.KaartNr)) throw new TankkaartManagerException("Er bestaat al een tankkaart met dit kaartnummer");
                Repo.VoegTankkaartToe(tankkaart);
            }
            catch (Exception ex)
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
            catch (Exception ex)
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
                    if (dbTankkaart.KaartNr != tankkaart.KaartNr)
                    {
                        if (Repo.BestaatTankkaartNummer(tankkaart.KaartNr)) throw new TankkaartManagerException("Deze tankkaart bestaat al in het systeem");
                    }
                    Bestuurder nieuweBestuurder;
                    if (tankkaart.Bestuurder != null)
                    {
                        nieuweBestuurder = _brepo.GeefBestuurder(tankkaart.Bestuurder.BestuurderId);
                        if (nieuweBestuurder.TankKaart != null && nieuweBestuurder.TankKaart != tankkaart) throw new TankkaartManagerException("Deze bestuurder heeft al een tankkaart, gelieve deze eerst te verwijderen vooraleer je een nieuwe wagen geeft");
                    }
                    if (dbTankkaart == tankkaart) throw new TankkaartManagerException("TankkaartManager: UpdateTankkaart - geen verschillen");
                    Repo.UpdateTankkaart(tankkaart);
                }
                else
                {
                    throw new TankkaartManagerException("TankkaartManager: UpdateTankkaart - tankkaart bestaat niet");
                }
            }
            catch (Exception ex)
            {
                throw new TankkaartManagerException("TankkaartManager: UpdateTankkaart - "+ ex.Message);
            }
        }
        public IReadOnlyList<TankKaart> ZoekTankkaarten(int? tankkaartId, string? kaartNr, DateTime? geldigheidsdatum, string? pincode, string? naamBestuurder, Brandstoftype_tankkaart? brandstoftype, bool? geblokkeerd)
        {
            List<TankKaart> tankkaarten = new List<TankKaart>();

            try
            {
                if (tankkaartId.HasValue)
                {
                    if (Repo.BestaatTankkaart((int)tankkaartId)) tankkaarten.Add(Repo.GeefTankkaart(tankkaartId.Value));
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(kaartNr) || geldigheidsdatum != DateTime.MinValue || !string.IsNullOrWhiteSpace(pincode) || brandstoftype != null || !string.IsNullOrWhiteSpace(naamBestuurder))
                    {
                        tankkaarten.AddRange(Repo.ZoekTankkaarten(kaartNr, geldigheidsdatum, pincode, naamBestuurder, brandstoftype, geblokkeerd));
                    }
                    else
                    {
                        throw new TankkaartManagerException("TankkaartManager: ZoekTankkaarten - geen zoekcriteria");
                    }
                }
                return tankkaarten;
            }
            catch (Exception ex)
            {
                throw new TankkaartManagerException("TankkaartManager: ZoekTankkaarten", ex);
            }
        }
    }
}
