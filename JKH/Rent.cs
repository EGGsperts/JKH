using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKH
{
    public class Rent
    {
        public float ColdWaterSupply { get; set; }
        public float HotWaterSupply { get; set; }
        public float WaterDisposal { get; set; }
        public float Heating { get; set; }
        public float PowerSupply { get; set; }
        public float SolidMunicipalWasteManagement { get; set; }
        public float GasSupply { get; set; }
        public float PersonalAccountRent { get; set; }
        public bool DeptorType { get; set; }
        public float PaidUp { get; set; } 
        public string NeededMonth{ get; set;}
        public int NeededYear{ get; set;}
    }
}
