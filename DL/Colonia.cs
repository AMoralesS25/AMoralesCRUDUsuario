using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class Colonia
    {
        public int IdColonia { get; set; }
        public string Nombre { get; set; }
        public string CodigoPostal { get; set; }
        public DL.Municipio Municipio { get; set; }
    }
}
