using BusinessLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model
{
    public class RijbewijsLijst
    {
        public RijbewijsLijst( List<TypeRijbewijs> types)
        {
            VoegRijbewijzenToe(types);
        }

        public int ID { get; private set; }
        public Dictionary<TypeRijbewijs, bool> Types { get; private set; }

        public void ZetID(int id)
        {
            if (id <= 0) throw new RijbewijsLijstException("RijbewijsLijst - ZetID -> Id mag niet 0 of kleiner zijn.");
            ID = id;
        }

        public void VoegRijbewijzenToe(List<TypeRijbewijs> types)
        {
            foreach(TypeRijbewijs type in types)
            {
                Types.Add(type, true);
            }
        }

        public void VoegRijbewijsToe(TypeRijbewijs type)
        {
            if (Types.Keys.Contains(type)) throw new RijbewijsLijstException("RijbewijsLijst: VoegRijbewijsToe - Bestuurder heeft dit rijbewijs al.");
            Types.Add(type, true);
        }
        public void VerwijderRijbewijs(TypeRijbewijs type)
        {
            if (!Types.Keys.Contains(type)) throw new RijbewijsLijstException("RijbewijsLijst: VerwijderRijbewijs - Bestuurder heeft dit rijbewijs niet.");
            Types.Remove(type);
        }
    }
}
