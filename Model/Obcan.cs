using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemPrace_BDAS2.Model
{
    internal class Obcan
    {
        public int IdObcan { get; set; }
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public string RodneCislo { get; set; }
        public string DatumNarozeni { get; set; }
        public bool JeZamestnan { get; set; }

        public int IdAdresa { get; set; }
        public int IdObec { get; set; }

        public string Ulice { get; set; }

        public int CisloPopisne { get; set; }
        public string Stat { get; set; }
        public string Nazev { get; set; }
        public int IdNarodnost { get; set; }
        public string Narodnost { get; set; }

        public int IdPohlavi { get; set; }
        public string Pohlavi { get; set; }




    }
}
