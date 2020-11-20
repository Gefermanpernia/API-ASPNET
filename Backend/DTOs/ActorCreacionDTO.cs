using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public class ActorCreacionDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string Nombre { get; set; }
        public string Biografia { get; set; }
        public DateTime FechaNacimiento { get; set; }

    }
}
