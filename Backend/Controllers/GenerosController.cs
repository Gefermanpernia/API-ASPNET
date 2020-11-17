using Backend.Entidades;
using Backend.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        

        [HttpGet("{Id:int}/{nombre=Geferman}")] // api/generos/3/gefermna
        public async Task<ActionResult<Genero>> Get(int Id, [FromHeader] string nombre) // BindRequired es que sera requerido ese parametro
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var genero = await repositorio.ObtenerPorId(Id);

            if (genero == null)
            {
                return NotFound();
            }

            return genero;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Genero genero)
        {
            return NoContent();
        }

        [HttpPut]
        public ActionResult Put([FromBody] Genero genero)
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
