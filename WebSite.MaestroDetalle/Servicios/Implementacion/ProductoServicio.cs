using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebSite.MaestroDetalle.Models;
using WebSite.MaestroDetalle.Servicios.Contrato;

namespace WebSite.MaestroDetalle.Servicios.Implementacion
{
    public class ProductoServicio : IGenericoServicio<Producto>
    {
        private readonly HttpClient _httpClient;

        public ProductoServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Producto>> Listar()
        {
            List<Producto> lista = new List<Producto>();
            var respuesta = await _httpClient.GetAsync("/api/Producto/Lista");
            if (respuesta.IsSuccessStatusCode)
            {
                string contenido = await respuesta.Content.ReadAsStringAsync();
                lista = JsonConvert.DeserializeObject<List<Producto>>(contenido) ?? new List<Producto>();
            }
            return lista;
        }
        public async Task<Producto> Obtener(int Id)
        {
            Producto producto = null;
            var respuesta = await _httpClient.GetAsync($"/api/Producto/Obtener/{Id}");
            if (respuesta.IsSuccessStatusCode)
            {
                string contenido = await respuesta.Content.ReadAsStringAsync();
                producto = JsonConvert.DeserializeObject<Producto>(contenido) ?? new Producto();
            }
            return producto ?? new Producto();
        }

        public async Task<bool> Actualizar(Producto modelo)
        {
            var json = JsonConvert.SerializeObject(modelo);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");
            var resultado = await _httpClient.PutAsync("/api/Producto/Actualizar", contenido);
            return resultado.IsSuccessStatusCode;
        }

        public async Task<bool> Borrar(int Id)
        {
            var resultado = await _httpClient.DeleteAsync($"/api/Producto/Borrar/{Id}");
            return resultado.IsSuccessStatusCode;
        }

        public async Task<bool> Crear(Producto modelo)
        {
            var json = JsonConvert.SerializeObject(modelo);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");
            var resultado = await _httpClient.PostAsync("/api/Producto/Crear", contenido);
            return resultado.IsSuccessStatusCode;
        }

    }//fin
}//fin namespace
