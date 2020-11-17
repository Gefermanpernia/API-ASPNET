using Backend.Entidades;
using Backend.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        public List<Genero> Get()
        {
            return repositorio.ObtenerTodosLosGeneros();
        }

        [HttpPost]
        public void Post()
        {

        }

        [HttpGet("{Id:int}/{nombre=Geferman}")] // api/generos/3/gefermna
        public Genero Get(int Id, string nombre)
        {
            var genero = repositorio.ObtenerPorId(Id);

            if (genero == null)
            {
                //return NotFound();
            }

            return genero;
        }
        [HttpPut]
        public void Put()
        {
        }
        [HttpDelete]
        public void Delete()
        {

        }


    }
}
