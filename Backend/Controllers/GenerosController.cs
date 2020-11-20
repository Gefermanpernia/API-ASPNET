﻿using AutoMapper;
using Backend.DTOs;
using Backend.Entidades;
using Backend.Filtros;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
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
        private readonly ILogger<GenerosController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GenerosController(ILogger<GenerosController> logger,
            ApplicationDbContext context,
            IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet] // api/generos  
        public async Task<ActionResult<List<GeneroDTO>>> Get()
        {
            // asi se hace el mapeo de genero a GeneroDTO 
            var generos = await context.Generos.ToListAsync();
            return mapper.Map<List<GeneroDTO>>(generos);


        }



        [HttpGet("{Id:int}")] // api/generos/3
        public ActionResult<Genero> Get(int Id)
        {

            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            var genero = mapper.Map<Genero>(generoCreacionDTO);
            context.Add(genero);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut]
        public ActionResult Put([FromBody] Genero genero)
        {
            throw new NotImplementedException();

        }
        [HttpDelete]
        public ActionResult Delete()
        {
            throw new NotImplementedException();

        }


    }
}
