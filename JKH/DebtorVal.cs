using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKH
{
    public class DebtorVal
    {
        public List<Rent> DebtorRent { get; set; }
        public DebtorVal()
        {
            DebtorRent = new List<Rent>();
        }
    }
}
