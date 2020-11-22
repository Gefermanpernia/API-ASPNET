using AutoMapper;
using Backend.Controllers;
using Backend.Filtros;
using Backend.Utilidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // configuracion autoMapper
            services.AddAutoMapper(typeof(Startup));

            // pasandole a mapper el geometry factory
            services.AddSingleton(provider =>
            
                new MapperConfiguration(config =>
                {
                    var geometryfactory = provider.GetRequiredService<GeometryFactory>();
                    config.AddProfile(new AutoMapperProfile(geometryfactory));

                }).CreateMapper());

            // Geometryfactory para obtener querys espaciales
            services.AddSingleton<GeometryFactory>(NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326));


            // configuracion para almacenar las fotos sea en local o azure
            services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();

            //
            services.AddHttpContextAccessor();


            // Base de datos
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("defaultConnection"),
                sqlServe => sqlServe.UseNetTopologySuite()));



            // Configuracion para permitir metodos abrir cors
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    var frontendURL = Configuration.GetValue<string>("frontend_url");
                    builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader()
                    //se expone la cabecera 
                    .WithExposedHeaders(new string[] { "cantidadTotalRegistros" }) ;
                });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            services.AddResponseCaching();


            services.AddControllers(options => 
            {
                options.Filters.Add(typeof(FiltroDeExcepcion));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend v1"));
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
