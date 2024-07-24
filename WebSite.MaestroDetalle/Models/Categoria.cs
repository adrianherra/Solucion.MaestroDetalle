using System;
using System.ComponentModel.DataAnnotations;

namespace WebSite.MaestroDetalle.Models
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

    }//fin class
}//fin namespace
