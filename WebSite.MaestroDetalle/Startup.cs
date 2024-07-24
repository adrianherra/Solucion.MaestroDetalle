using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebSite.MaestroDetalle.Models;
using WebSite.MaestroDetalle.Servicios.Contrato;
using WebSite.MaestroDetalle.Servicios.Implementacion;

namespace WebSite.MaestroDetalle
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
            services.AddControllersWithViews();


            // Configurar HttpClient con un nombre específico
            services.AddHttpClient("CategoriaClient", client =>
            {
                client.BaseAddress = new Uri("http://localhost:40438/api");
            });

            // Configurar HttpClient con un nombre específico para Producto
            services.AddHttpClient("ProductoClient", client =>
            {
                client.BaseAddress = new Uri("http://localhost:40438/api");
            });

            // Registrar los servicios específicos y pasar el HttpClient configurado
            services.AddTransient<IGenericoServicio<Categoria>, CategoriaServicio>(provider =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient("CategoriaClient");
                return new CategoriaServicio(httpClient);
            });

            services.AddTransient<IGenericoServicio<Producto>, ProductoServicio>(provider =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient("ProductoClient");
                return new ProductoServicio(httpClient);
            });

            services.AddHttpClient();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
