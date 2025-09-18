using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeneszamok.Models
{
    public class Lemez
    {
        public int Id { get; set; }
        public string Cim { get; set; }
        public int KiadasEve { get; set; }
        public string Kiado { get; set; }

        public Lemez() { }

        public override string ToString()
        {
            return $"{Id} Cím:{Cim} ({KiadasEve}) Kiadó:{Kiado}";
        }
    }
}
