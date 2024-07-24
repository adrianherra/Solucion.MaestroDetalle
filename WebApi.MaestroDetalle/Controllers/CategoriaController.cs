using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using WebApi.MaestroDetalle.Modelos;
using WebApi.MaestroDetalle.Repositorio.Contrato;

namespace WebApi.MaestroDetalle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IGenericoRepositorio<Categoria> _categoria;

        public CategoriaController(IGenericoRepositorio<Categoria> categoria)
        {
            _categoria = categoria;    
        }// fin constructor

        [HttpGet("Lista")]
        public async Task<IActionResult> Lista()
        {
            List<Categoria> respuesta = await _categoria.Listar();
            try
            {
                return StatusCode(StatusCodes.Status200OK, respuesta);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }// fin Lista

        [HttpGet("Obtener/{Id}")]
        public async Task<IActionResult> Obtener(int Id)
        {
            Categoria respuesta = await _categoria.Obtener(Id);
            try
            {
                return StatusCode(StatusCodes.Status200OK, respuesta);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }// fin Obtener

        [HttpPost("Crear")]
        public async Task<IActionResult> Crear([FromBody] Categoria modelo)
        {
            bool resultado = await _categoria.Crear(modelo);
            try
            {
                return StatusCode(StatusCodes.Status200OK, new { estado = resultado });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }// fin Crear

        [HttpPut("Actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] Categoria modelo)
        {
            bool respuesta = await _categoria.Actualizar(modelo);
            try
            {
                return StatusCode(StatusCodes.Status200OK, new { estado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }// fin Actualizar

        [HttpDelete("Borrar/{Id}")]
        public async Task<IActionResult> Borrar(int Id)
        {
            bool respuesta = await _categoria.Borrar(Id);
            try
            {
                return StatusCode(StatusCodes.Status200OK, new { estado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }// fin Borrar

    }// fin class
}// fin namespace
