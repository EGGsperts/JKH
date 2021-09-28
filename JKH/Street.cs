using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JKH
{
    public class Street
    {
        public List<string> nameStreet { get; set; }
        public string selectedStreetName { get; set; }
        public Street()
        {
            nameStreet = new List<string>();
        }
    }
}
