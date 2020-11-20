using Backend.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend
{
    public class ApplicationDbContext : DbContext
    {
        // 
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        // con un DbSet le digo a EF cuales van hacer las tablas que quiero tener en mi DB
        // y tambien le vamos a decir en base a que modelos o entidades vamos a basar 
        // las tablas
        public DbSet<Genero> Generos{ get; set; }
        public DbSet<Actor> Actores { get; set; }
    }
}
