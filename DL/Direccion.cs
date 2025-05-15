using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public  class Direccion
    {
        public int IdDireccion { get; set; }
        [RegularExpression(@"^[A-Za-z0-9\s]{1,50}$", ErrorMessage = "La calle solo debe tener letras y números")]
        public string Calle { get; set; }
        [RegularExpression(@"^[A-Za-z0-9\s]{1,20}$", ErrorMessage = "El número interior solo debe tener letras y números")]
        public string NumeroInterior { get; set; }
        [RegularExpression(@"^[A-Za-z0-9\s]{1,50}$", ErrorMessage = "La calle solo debe tener letras y números")]
        public string NumeroExterior { get; set; }
        public DL.Colonia Colonia { get; set; }
    }
}
