﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MinoProjectWeb.Models
{
    public class IngresoGasto
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [Range(1, 100000)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Valor")]
        public double Valor { get; set; }
    }

}
