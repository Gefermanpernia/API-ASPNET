using AutoMapper;
using Backend.DTOs;
using Backend.Entidades;
using Backend.Utilidades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/actores")]
    [ApiController]
    public class ActoresController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "actores";
        public ActoresController(ApplicationDbContext context,
            IMapper mapper,
            IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpPost]              // con fromForm podremos enviar la foto
        public async Task<ActionResult> Post([FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            var actor = mapper.Map<Actor>(actorCreacionDTO);
                
            if (actorCreacionDTO.Foto != null)
            {
                actor.Foto = await almacenadorArchivos.GuardarArchivo(contenedor, actorCreacionDTO.Foto);
            }
            context.Add(actor);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
