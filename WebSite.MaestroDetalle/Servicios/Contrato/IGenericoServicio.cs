using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebSite.MaestroDetalle.Servicios.Contrato
{
    public interface IGenericoServicio<T> where T: class
    {
        Task<List<T>> Listar();

        Task<bool> Crear(T modelo);

        Task<T> Obtener(int Id);

        Task<bool> Actualizar(T modelo);

        Task<bool> Borrar(int Id);
    }//fin Interface
}//fin namespace
