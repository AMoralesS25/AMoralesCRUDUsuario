using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string ApellidoPaterno { get; set; }

        [Required]
        [MaxLength(50)]
        public string ApellidoMaterno { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [MaxLength(10)]
        public string Sexo { get; set; }

        [Required]
        [MaxLength(20)]
        public string Telefono { get; set; }

        [Required]
        [MaxLength(20)]
        public string Celular { get; set; }

        [Required]
        [MaxLength(18)]
        public string CURP { get; set; }

        public bool Estatus { get; set; }

        public int IdLogin { get; set; }
        [ForeignKey("IdLogin")]
        public DL.Login Login { get; set; }

    }
}
