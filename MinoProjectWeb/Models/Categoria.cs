using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MinoProjectWeb.Models
{
    public class Categoria
    {   [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(120)]
        [Display(Name = "Nombre categoria")]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(2)]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }//IN Ingreso GA Gasto
        [Required]
        public bool Estado { get; set; } //True or false

    }
}
