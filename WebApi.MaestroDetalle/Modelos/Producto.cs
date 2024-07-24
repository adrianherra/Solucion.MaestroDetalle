using System;

namespace WebApi.MaestroDetalle.Modelos
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string CategoriaNombre { get; set; } // Para mostrar el nombre de la categoría
    }//fin class

}//fin namespace
