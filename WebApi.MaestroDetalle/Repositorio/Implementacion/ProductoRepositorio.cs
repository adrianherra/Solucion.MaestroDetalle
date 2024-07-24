using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.MaestroDetalle.Modelos;
using WebApi.MaestroDetalle.Repositorio.Contrato;

namespace WebApi.MaestroDetalle.Repositorio.Implementacion
{
    public class ProductoRepositorio : IGenericoRepositorio<Producto>
    {

        private readonly string _cadenaSQl = "";

        public ProductoRepositorio(IConfiguration configuracion)
        {
            _cadenaSQl = configuracion.GetConnectionString("CadenaSQL");
        }// fin constructor

        public async Task<List<Producto>> Listar()
        {
            List<Producto> lista = new List<Producto>();
            string query = @"SELECT p.IdProducto, p.IdCategoria, p.Nombre, p.Descripcion, p.Precio, p.Cantidad, p.FechaCreacion, p.FechaModificacion, 
                             c.Nombre AS CategoriaNombre  FROM PRODUCTOS p JOIN CATEGORIAS c ON p.IdCategoria = c.IdCategoria";
            using (SqlConnection conexion = new SqlConnection(_cadenaSQl))
            {
                await conexion.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new Producto
                            {
                                IdProducto        = Convert.ToInt32(reader["IdProducto"]),
                                IdCategoria       = Convert.ToInt32(reader["IdCategoria"]),
                                CategoriaNombre   = reader["CategoriaNombre"].ToString(),
                                Nombre            = reader["Nombre"].ToString(),
                                Descripcion       = reader["Descripcion"].ToString(),
                                Precio            = Convert.ToDecimal(reader["Precio"]),
                                Cantidad          = Convert.ToInt32(reader["Cantidad"]),
                                FechaCreacion     = reader.GetDateTime(reader.GetOrdinal("FechaCreacion")),
                                FechaModificacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion"))
                            });

                        }
                    };
                };
                await conexion.CloseAsync();
            };
            return lista;
        }//fin

        public async Task<Producto> Obtener(int Id)
        {
            Producto producto = null;
            string query = @"SELECT p.*, c.Nombre AS CategoriaNombre  FROM PRODUCTOS p
                            INNER JOIN CATEGORIAS c ON p.IdCategoria = c.IdCategoria
                            WHERE p.IdProducto = @IdProducto";

            using (SqlConnection conexion = new SqlConnection(_cadenaSQl))
            {
                await conexion.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdProducto", Id);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            producto = new Producto
                            {
                                IdProducto = Convert.ToInt32(reader["IdProducto"]),
                                IdCategoria = Convert.ToInt32(reader["IdCategoria"]),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                Precio = Convert.ToDecimal(reader["Precio"]),
                                Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                FechaCreacion = reader["FechaCreacion"] != DBNull.Value ? (DateTime?)reader["FechaCreacion"] : null,
                                FechaModificacion = reader["FechaModificacion"] != DBNull.Value ? (DateTime?)reader["FechaModificacion"] : null,
                                CategoriaNombre = reader["CategoriaNombre"].ToString()
                            };
                        }
                    }
                }
            }
            return producto;
        }


        public async Task<bool> Actualizar(Producto modelo)
        {
            bool respuesta = true;
            using (var conexion = new SqlConnection(_cadenaSQl))
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarProducto", conexion);
                cmd.Parameters.AddWithValue("@IdProducto", modelo.IdProducto);
                cmd.Parameters.AddWithValue("@IdCategoria", modelo.IdCategoria);
                cmd.Parameters.AddWithValue("@Nombre", modelo.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", modelo.Descripcion);
                cmd.Parameters.AddWithValue("@Precio", modelo.Precio);
                cmd.Parameters.AddWithValue("@Cantidad", modelo.Cantidad);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                try
                {
                    await conexion.OpenAsync();
                    int lineas = await cmd.ExecuteNonQueryAsync();
                    if (lineas > 0) respuesta = true;
                    await conexion.CloseAsync();
                }
                catch
                {
                    respuesta = false;
                }
            };
            return respuesta;
        }// fin

        public async Task<bool> Borrar(int Id)
        {
            bool respuesta = true;
            using (var conexion = new SqlConnection(_cadenaSQl))
            {
                SqlCommand cmd = new SqlCommand("sp_BorrarProducto", conexion);
                cmd.Parameters.AddWithValue("@IdProducto", Id);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                try
                {
                    await conexion.OpenAsync();
                    int lineas = await cmd.ExecuteNonQueryAsync();
                    if (lineas > 0) respuesta = true;
                    await conexion.CloseAsync();
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }// fin

        public async Task<bool> Crear(Producto modelo)
        {
            bool respuesta = true;
            using (var conexion = new SqlConnection(_cadenaSQl))
            {
                SqlCommand cmd = new SqlCommand("sp_CrearProducto", conexion);
                cmd.Parameters.AddWithValue("@IdCategoria", modelo.IdCategoria);
                cmd.Parameters.AddWithValue("@Nombre", modelo.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", modelo.Descripcion);
                cmd.Parameters.AddWithValue("@Precio", modelo.Precio);
                cmd.Parameters.AddWithValue("@Cantidad", modelo.Cantidad);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                try
                {
                    await conexion.OpenAsync();
                    int lineas = await cmd.ExecuteNonQueryAsync();
                    if (lineas > 0) respuesta = true;
                    await conexion.CloseAsync();
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }// fin

    }// fin
}// fin namespace
