using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class Municipio
    {
        public int IdMunicipio { get; set; }
        public string Nombre { get; set; }
        public DL.Estado Estado { get; set; }
    }
}
