using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemPrace_BDAS2.Model
{
    internal class Zamestnanec
    {
        public int IdZamestnanec { get; set; }
        public string Email { get; set; }
        public string TelCislo { get; set; }
        public string TypZamestnance { get; set; }
        public int IdPobocka { get; set; }
        public string Heslo { get; set; }
    }

}
