﻿using Backend.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Repositorios
{
    public interface IRepositorio
    {
        Genero ObtenerPorId(int Id);
        List<Genero> ObtenerTodosLosGeneros();
    }
}
