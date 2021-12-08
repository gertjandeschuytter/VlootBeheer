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

        //public List<Bestuurder> GeefBestuurder(int? id, string naam, string voornaam, Adres adres, DateTime datum, string rijksregister, List<TypeRijbewijs> types, Voertuig v, TankKaart t)
        //{
        //    List<Bestuurder> list = new List<Bestuurder>();
        //    try
        //    {
        //        if(id.HasValue) list.Add(repo.GeefBestuurder((int)id));
        //        if(!string.IsNullOrWhiteSpace(naam) || !string.IsNullOrWhiteSpace(voornaam) || adres != null || datum != new DateTime(1,1,1) || !string.IsNullOrWhiteSpace(rijksregister) || types.Count != 0 || v != null || t != null)
        //        {
        //            list.AddRange(repo.GeefBestuurders(naam, voornaam, adres, datum, rijksregister, types, v, t));
        //        }
        //        else
        //        {
        //            throw new BestuurderManagerException("Geen zoekcriteria gegeven");
        //        }

        //        return list;
        //    }
        //    catch(Exception ex) { throw new BestuurderManagerException("BestuurderManager - Er liep iets mis: ", ex); }
        //}

        public void VoegBestuurderToe(Bestuurder bestuurder)
        {
            try
            {
                if (repo.BestaatBestuurder(bestuurder.ID)) throw new BestuurderManagerException("Bestuurder al gekend");
                repo.VoegBestuurderToe(bestuurder);
            }
            catch(Exception ex) { throw new BestuurderManagerException("BestuurderManager - Er liep iets mis: ", ex); }
        }

        public void VerwijderBestuurder(Bestuurder bestuurder)
        {
            try
            {
                if (!repo.BestaatBestuurder(bestuurder.ID)) throw new BestuurderManagerException("Bestuurder bestaat niet");
                repo.VerwijderBestuurder(bestuurder);
            }
            catch(Exception ex) { throw new BestuurderManagerException("BestuurderManager - Er liep iets mis: ", ex); }
        }

        public void WijzigBestuurder(Bestuurder bestuurder)
        {
            try
            {
                if (bestuurder == null) throw new BestuurderManagerException("bestuurder mag niet null zijn");
                if (!repo.BestaatBestuurder(bestuurder.ID)) throw new BestuurderManagerException("Bestuurder bestaat niet");
                if (bestuurder == repo.GeefBestuurder(bestuurder.ID)) throw new BestuurderManagerException("Ze zijn gelijk");
                repo.WijzigBestuurder(bestuurder);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager - Er liep iets mis: ", ex); }
        }
    }
}
