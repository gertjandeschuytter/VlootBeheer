using System;
using System.Collections.Generic;

namespace BusinessLayer.Model
{
    public class Voertuig
    {
        //properties
        public string Merk { get; private set; }
        public string Model { get; private set; }
        public string ChassisNummer { get; private set; }
        public string NummerPlaat { get; private set; }
        public Brandstoftype BrandstofType { get; private set; }
        public Typewagen TypeWagen { get; private set; }

        //fields
        private List<string> Merken = new List<string>();

        #region Constructors
        public Voertuig()
        {

        }
        #endregion

        #region Properties
        #endregion

        #region Methods
        #endregion
    }
}
