using Backend.Entidades;
using Backend.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/generos")]
    [ApiController]
    public class GenerosController: ControllerBase
    {
        private readonly IRepositorio repositorio;
        private readonly WeatherForecastController weatherForecastController;

        public GenerosController(IRepositorio repositorio, WeatherForecastController weatherForecastController)
        {
            this.repositorio = repositorio;
            this.weatherForecastController = weatherForecastController;
        }

        [HttpGet]        
        [HttpGet("Listado")]        //api/genero/listado
        [HttpGet("/Listadogenero")] // /listadogenero
        public ActionResult<List<Genero>> Get()
        {
            return repositorio.ObtenerTodosLosGeneros();
        }

        [HttpGet("guid")] // api/generos/guid
        public ActionResult<Guid> GetGUID()
        {
            return Ok(new
            {
                GUID_GenerosController = repositorio.ObtenerGUID(),
                GUID_weatherForecastController = weatherForecastController.ObtenerGUIDweatherForecastController()
            });
        }

        

        [HttpGet("{Id:int}/{nombre=Geferman}")] // api/generos/3/gefermna
        public async Task<ActionResult<Genero>> Get(int Id, [FromHeader] string nombre) // BindRequired es que sera requerido ese parametro
        {

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
            repositorio.CrearGenero(genero);
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
