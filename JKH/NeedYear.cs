using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKH
{
    public class NeedYear
    {
        public int SelectedYear { get; set; }
        public List<int> AllYear { get; set; }
        public NeedYear()
        {
            AllYear = new List<int>();
        }
    }
}
