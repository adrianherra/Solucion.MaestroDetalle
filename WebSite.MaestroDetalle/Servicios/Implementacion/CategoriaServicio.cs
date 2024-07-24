using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebSite.MaestroDetalle.Models;
using WebSite.MaestroDetalle.Servicios.Contrato;
namespace WebSite.MaestroDetalle.Servicios.Implementacion
{
    public class CategoriaServicio: IGenericoServicio<Categoria>
    {
        private readonly HttpClient _httpClient;

        public CategoriaServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Categoria>> Listar()
        {
            var lista = new List<Categoria>();
            var respuesta = await _httpClient.GetAsync("api/Categoria/Lista");
            if (respuesta.IsSuccessStatusCode)
            {
                string contenido = await respuesta.Content.ReadAsStringAsync();
                lista = JsonConvert.DeserializeObject<List<Categoria>>(contenido) ?? new List<Categoria>();
            }
            return lista;
        }//fin

        public async Task<Categoria> Obtener(int Id)
        {
            var categoria = new Categoria();
            var respuesta = await _httpClient.GetAsync($"api/Categoria/Obtener/{Id}");
            if (respuesta.IsSuccessStatusCode)
            {
                string contenido = await respuesta.Content.ReadAsStringAsync();
                categoria = JsonConvert.DeserializeObject<Categoria>(contenido) ?? new Categoria();
            }
            return categoria;
        }// fin

        public async Task<bool> Actualizar(Categoria modelo)
        {
            var json = JsonConvert.SerializeObject(modelo);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");
            var resultado = await _httpClient.PutAsync("api/Categoria/Actualizar", contenido);
            return resultado.IsSuccessStatusCode;
        }// fin

        public async Task<bool> Borrar(int Id)
        {
            var resultado = await _httpClient.DeleteAsync($"api/Categoria/Borrar/{Id}");
            if (resultado.IsSuccessStatusCode)
                return true;
            else
                return false;
        }//fin

        public async Task<bool> Crear(Categoria modelo)
        {
            var json = JsonConvert.SerializeObject(modelo);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");
            var resultado = await _httpClient.PostAsync("api/Categoria/Crear", contenido);
            return resultado.IsSuccessStatusCode;
        }//fin 

        
    }//fin class
}// fin namespace
