using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemPrace_BDAS2.Model
{
    public class PrukazPojistovny
    {
        public string DatumVydani { get; set; }
        public string PlatnostDo { get; set; }

        public string CisloPrukazu { get; set; }
        public int IdObcan { get; set; }
        public int IdPojistovna { get; set; }

    }

}



