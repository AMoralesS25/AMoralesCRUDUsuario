using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Login
    {
        public int IdLogin { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "Escribe el email")]
        [RegularExpression(@"^[\w+._-]{4,254}@[a-zA-Z0-9.-]{2,254}\.[a-zA-Z]{2,10}$", ErrorMessage = "El email no es valido")]
        public string Email { get; set; }
        [DisplayName("Password")]
        [Required(ErrorMessage = "Escribe el password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@@$!%*?&])([A-Za-z\d$@@$!%*?&]|[^ ]){8,50}$", ErrorMessage = "El password debe contener al menos un número, una mayuscula y un signo")]
        public string Password { get; set; }
        public List<Object>? Logins { get; set; }

    }
}
