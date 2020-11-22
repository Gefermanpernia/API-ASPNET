using AutoMapper;
using Backend.DTOs;
using Backend.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Utilidades
{
    public class AutoMapperProfile: Profile
    {
        // hay que crear el mapeo para cada entidad que se requiera, sea en get, post, put 
        public AutoMapperProfile()
        {
            // reverseMap me permite hacer un mapeo en las dos direcciones GET
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            // POST
            CreateMap<GeneroCreacionDTO, Genero>();
            CreateMap<Actor, ActorDTO>().ReverseMap();
            // POST
            CreateMap<ActorCreacionDTO, Actor>()
                    .ForMember(x => x.Foto, options => options.Ignore());
        }
    }
}
