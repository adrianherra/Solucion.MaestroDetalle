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
    public class ProductoController : ControllerBase
    {
        private readonly IGenericoRepositorio<Producto> _producto;

        public ProductoController(IGenericoRepositorio<Producto> producto)
        {
            _producto = producto;
        }// fin constructor

        [HttpGet("Lista")]
        public async Task<IActionResult> Lista()
        {
            List<Producto> respuesta = await _producto.Listar();
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
            Producto respuesta = await _producto.Obtener(Id);
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
        public async Task<IActionResult> Crear([FromBody] Producto modelo)
        {
            bool resultado = await _producto.Crear(modelo);
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
        public async Task<IActionResult> Actualizar([FromBody] Producto modelo)
        {
            bool respuesta = await _producto.Actualizar(modelo);
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
            bool respuesta = await _producto.Borrar(Id);
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
