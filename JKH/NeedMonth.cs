using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKH
{
    public class NeedMonth
    {
        public string SelectedMonth { get; set; }
        public List<string> AllMonth { get; set; }
        public NeedMonth()
        {
            AllMonth = new List<string>();
        }
    }
}
