using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ITankkaartRepository
    {
        void VoegTankkaartToe(TankKaart tankkaart);
        bool BestaatTankkaart(int tankkaartId);
        void VerwijderTankkaart(TankKaart tankkaart);
        void UpdateTankkaart(TankKaart tankkaart);
        TankKaart GeefTankkaart(int tankkaartId);
        IReadOnlyList<TankKaart> GeefTankkaarten(string? kaartnr, DateTime? geldigheidsdatum, string? pincode, Bestuurder? bestuurder, bool? geblokkeerd);
    }
}
