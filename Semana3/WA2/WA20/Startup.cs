using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WA20
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            /// Vamos a ocultar este código
            /*app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });*/

            ///middleware que permite usar archivos por defecto, en este caso index.html, pero lo podemos reescribir y decir que sea main.html
            DefaultFilesOptions options = new DefaultFilesOptions();
            System.Diagnostics.Debug.WriteLine(options.DefaultFileNames.Aggregate("", (result, next) => { return $"{result}, {next}"; }));
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("main.html");
            ///agregamos el middleware pero le pasamos las options como parámetros.
            app.UseDefaultFiles(options);

            ///vamos a agregar a contenido estático
            ///siempre va a buscar una carpeta específica en el proyecto: wwwroot
            ///
            app.UseStaticFiles();
        }
    }
}
