using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IBestuurderRepository
    {
        void VoegBestuurderToe(Bestuurder bestuurder);
        void VerwijderBestuurder(Bestuurder bestuurder);
        void WijzigBestuurder(Bestuurder bestuurder);
        bool HeeftBestuurder(Bestuurder bestuurder);
    }
}
