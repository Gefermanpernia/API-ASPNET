using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Entidades
{
    public class Cine
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 75)]
        public string Nombre { get; set; }

        // Ubicacion del cine, libreria NetTopologySuite para usar querys espaciales
        public Point Ubicacion { get; set; }
    }
}
