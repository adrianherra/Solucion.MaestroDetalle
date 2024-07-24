using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Threading.Tasks;
using WebApi.MaestroDetalle.Modelos;
using WebApi.MaestroDetalle.Repositorio.Contrato;

namespace WebApi.MaestroDetalle.Repositorio.Implementacion
{
    public class CategoriaRepositorio : IGenericoRepositorio<Categoria>
    {
        private readonly string _cadenaSQl = "";
       
        public CategoriaRepositorio(IConfiguration configuracion)
        {
            _cadenaSQl = configuracion.GetConnectionString("CadenaSQL");
        }// fin constructor

        public async Task<List<Categoria>> Listar()
        {
            List<Categoria> lista = new List<Categoria>();
            string query = "SELECT * FROM CATEGORIAS";
            using (SqlConnection conexion = new SqlConnection(_cadenaSQl))
            {
                await conexion.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new Categoria
                            {
                                IdCategoria = Convert.ToInt32(reader["IdCategoria"]),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                FechaCreacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion")),
                                FechaModificacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion"))
                            });

                        }
                    };
                };
                await conexion.CloseAsync();
            };
            return lista;
        }//fin 

        public async Task<Categoria> Obtener(int Id)
        {
            Categoria categoria = null; // Inicialmente, no hay categoría
            string query = "SELECT * FROM CATEGORIAS WHERE IdCategoria = @IdCategoria";

            using (SqlConnection conexion = new SqlConnection(_cadenaSQl))
            {
                await conexion.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdCategoria", Id);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            categoria = new Categoria
                            {
                                IdCategoria = Convert.ToInt32(reader["IdCategoria"]),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                FechaCreacion = reader["FechaCreacion"] != DBNull.Value ? (DateTime)reader["FechaCreacion"] : DateTime.MinValue,
                                FechaModificacion = reader["FechaModificacion"] != DBNull.Value ? (DateTime)reader["FechaModificacion"] : DateTime.MinValue
                            };
                        }
                    }
                }
            }
            return categoria;
        }

        public async Task<bool> Actualizar(Categoria modelo)
        {
            bool respuesta = true;
            using (var conexion = new SqlConnection(_cadenaSQl))
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarCategoria", conexion);
                cmd.Parameters.AddWithValue("@IdCategoria", modelo.IdCategoria);
                cmd.Parameters.AddWithValue("@Nombre", modelo.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", modelo.Descripcion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                try
                {
                    await conexion.OpenAsync();
                    int lineas = await cmd.ExecuteNonQueryAsync();
                    if (lineas > 0)  respuesta = true;
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
                SqlCommand cmd = new SqlCommand("sp_BorrarCategoria", conexion);
                cmd.Parameters.AddWithValue("@IdCategoria", Id);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                try
                {
                    await conexion.OpenAsync();
                    int lineas = await cmd.ExecuteNonQueryAsync() ;
                    if (lineas > 0) respuesta = true;
                    await conexion.CloseAsync();
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }//fin

        public async Task<bool> Crear(Categoria modelo)
        {
            bool respuesta = true;
            using (var conexion = new SqlConnection(_cadenaSQl))
            {
                SqlCommand cmd = new SqlCommand("sp_CrearCategoria", conexion);
                cmd.Parameters.AddWithValue("@Nombre", modelo.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", modelo.Descripcion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                try
                {
                    await conexion.OpenAsync();
                    int lineas = await cmd.ExecuteNonQueryAsync();
                    if (lineas > 0) respuesta = true;
                }
                catch
                {
                    respuesta = false;
                }
                finally
                {
                    await conexion.CloseAsync();
                }
            }
            return respuesta;
        }//fin

        
    }// fin
}// fin namespace
