using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using adService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace adService
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
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("adDB"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Ads Service API Documentation",
                    Description = "This documentation provide the information about Ads service",
                    TermsOfService = null,
                    Contact = new OpenApiContact()
                    {
                        Name = "Softtek GDC Monterrey",
                        Email = "softtek@contact.com.mx",
                        Url = null
                    }

                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext context)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();


            //Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            //Enable middleware to serve swager-ui (HTML, JS, CSS, etc.),
            //specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ad Service API"));


            //Data de prueba si no hay en la base de datos
            if (!context.Ads.Any())
            {
                context.Ads.AddRange(new List<Ad>()
                {
                    new Ad(){ Description = "¡¡¡Compra Videojuegos YA!!!" },
                    new Ad(){ Description = "¡¡¡Compra Jerseys de Futbol YA!!!" },
                    new Ad(){ Description = "¡¡¡Compra Cámaras de Vídeo YA!!!" },
                    new Ad(){ Description = "¡¡¡Compra Proyectores YA!!!" },
                    new Ad(){ Description = "¡¡¡Compra Pantallas plasma YA!!!" }
                });
                context.SaveChanges();
            }
        }
    }
}
