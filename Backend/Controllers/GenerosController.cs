using Backend.Entidades;
using Backend.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/generos")]
    public class GenerosController: ControllerBase
    {
        private readonly IRepositorio repositorio;

        public GenerosController(IRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet]        
        [HttpGet("Listado")]        //api/genero/listado
        [HttpGet("/Listadogenero")] // /listadogenero
        public ActionResult<List<Genero>> Get()
        {
            return repositorio.ObtenerTodosLosGeneros();
        }

        [HttpPost]
        public void Post()
        {

        }

        [HttpGet("{Id:int}/{nombre=Geferman}")] // api/generos/3/gefermna
        public async Task<ActionResult<Genero>> Get(int Id, string nombre)
        {
            var genero = await repositorio.ObtenerPorId(Id);

            if (genero == null)
            {
                return NotFound();
            }

            return genero;
        }
        [HttpPut]
        public ActionResult Put()
        {
            return NoContent();
        }
        [HttpDelete]
        public ActionResult Delete()
        {
            return NoContent();
        }


    }
}
