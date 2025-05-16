using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class DTO
    {
        
        public class UsuarioGetAll
        {
            public int IdUsuario { get; set; }
           
            public string UserName { get; set; }
            public string Nombre { get; set; }
            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }
            public DateTime FechaNacimiento { get; set; }
            public string Sexo { get; set; }
            public string Telefono { get; set; }
            public string Celular { get; set; }
            public bool Estatus { get; set; }
            public string CURP { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
