using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSite.MaestroDetalle.Models;
using WebSite.MaestroDetalle.Servicios.Contrato;

namespace WebSite.MaestroDetalle.Controllers
{
    public class CategoriaController : Controller
    {
       
        private readonly IGenericoServicio<Categoria> _serviciosAPI;

        public CategoriaController(IGenericoServicio<Categoria> serviciosAPI)
        {
            _serviciosAPI = serviciosAPI;
        }//fin constructor

        public async Task<IActionResult> Index()
        {
            List<Categoria> categorias = await _serviciosAPI.Listar();
            return View("Index", categorias);
        }// fin

                
        public async Task<IActionResult> ActualizarCategoria(int IdCategoria)
        {
            Categoria modelo = await _serviciosAPI.Obtener(IdCategoria);
            if (modelo == null)
            {
                return NotFound();
            }

            ViewBag.Accion = IdCategoria > 0 ? "Actualizar Categoria" : "Nueva Categoria";
            return View(modelo);
        }// fin

        [HttpPost]
        public async Task<IActionResult> BorrarCategoria(int IdCategoria)
        {
            bool respuesta = await _serviciosAPI.Borrar(IdCategoria);
            if (respuesta)
            {
                TempData["Message"] = "La categoria se borro exitosamente.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "No se pudo borrar la categoria.";
                return RedirectToAction("Index");
            }
        }// fin

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(Categoria modelo)
        {
            bool respuesta;
            if (modelo.IdCategoria == 0)
            {
                respuesta = await _serviciosAPI.Crear(modelo);
            }
            else
            {
                respuesta = await _serviciosAPI.Actualizar(modelo);
            }
            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
        }//fin

    }// fin class
}// fin namespace
