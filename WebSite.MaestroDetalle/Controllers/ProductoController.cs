using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGeneration;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSite.MaestroDetalle.Models;
using WebSite.MaestroDetalle.Servicios.Contrato;

namespace WebSite.MaestroDetalle.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IGenericoServicio<Producto> _serviciosAPI;
        private readonly IGenericoServicio<Categoria> _serviciosCategoria;

        public ProductoController(IGenericoServicio<Producto> serviciosAPI, IGenericoServicio<Categoria> serviciosCategoria)
        {
            _serviciosAPI = serviciosAPI;
            _serviciosCategoria = serviciosCategoria;

        }//fin constructor

        public async Task<IActionResult> Index()
        {
            List<Producto> productos = await _serviciosAPI.Listar();

            return View("Index", productos);
        }// fin 

        public async Task<IActionResult> ActualizarProducto(int IdProducto)
        {
            Producto producto = new Producto();
            List<Categoria> categorias = new List<Categoria>();

            ViewBag.Accion = "Nuevo Producto";

            categorias = await _serviciosCategoria.Listar();
            ViewBag.Items = new SelectList(categorias, "IdCategoria", "Nombre");
            if (IdProducto != 0)
            {
                producto = await _serviciosAPI.Obtener(IdProducto);
                ViewBag.Accion = "Actualizar Producto";
            }
            return View(producto);
        }// fin

        [HttpPost]
        public async Task<IActionResult> BorrarProducto(int IdProducto)
        {
            bool respuesta = await _serviciosAPI.Borrar(IdProducto);
            if (respuesta)
            {
                TempData["Message"] = "El producto se borro exitosamente.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "No se pudo borrar el producto.";
                return RedirectToAction("Index");
            }
        }// fin

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(Producto modelo)
        {
            bool respuesta;
            if (modelo.IdProducto == 0)
            {
                TempData["Message"] = "El producto se creo exitosamente.";
                respuesta = await _serviciosAPI.Crear(modelo);
            }
            else
            {
                TempData["Message"] = "El producto se actualizo exitosamente.";
                respuesta = await _serviciosAPI.Actualizar(modelo);
            }
            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
        }


    }// fin class
}// fin namespace
