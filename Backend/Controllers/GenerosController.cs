using Backend.Entidades;
using Backend.Filtros;
using Backend.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/generos")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GenerosController: ControllerBase
    {
        private readonly IRepositorio repositorio;
        private readonly WeatherForecastController weatherForecastController;
        private readonly ILogger<GenerosController> logger;

        public GenerosController(IRepositorio repositorio, 
            WeatherForecastController weatherForecastController, 
            ILogger<GenerosController> logger)
        {
            this.repositorio = repositorio;
            this.weatherForecastController = weatherForecastController;
            this.logger = logger;
        }

        [HttpGet]        
        [HttpGet("Listado")]        //api/genero/listado
        [HttpGet("/Listadogenero")] // /listadogenero
        //[ResponseCache(Duration = 60)] //filtro de accion
        [ServiceFilter(typeof(MiFiltroDeAccion))]
        public ActionResult<List<Genero>> Get()
        {
            logger.LogInformation("Vamos a mostrar los generos");
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
            logger.LogDebug($"obteniendo un genero por el id {Id}");
            var genero = await repositorio.ObtenerPorId(Id);

            if (genero == null)
            {
                logger.LogWarning($"no pudimos encontrar el genero de id {Id}");
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
