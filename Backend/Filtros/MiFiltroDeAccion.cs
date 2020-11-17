using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Filtros
{
    public class MiFiltroDeAccion : IActionFilter
    {
        public ILogger<MiFiltroDeAccion> Logger { get; }

        public MiFiltroDeAccion(ILogger<MiFiltroDeAccion> logger)
        {
            Logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Logger.LogInformation("Antes de ejecutar la accion");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Logger.LogInformation("despues de ejecutar la accion");
        }
    }
}
