using DAO_Hermes.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Hermes.Repositorios
{
    public class GrupoCorreoDAO : IDisposable
    {       
        private static List<GrupoCorreo> list_grupocorreo;
        private static GrupoCorreo entidad;
        private static SqlDataReader reader;
        private static SqlConnection conexion;
        private static SqlCommand comando;
        private static ClientResponse clientResponse;
        public GrupoCorreoDAO()
        {
            list_grupocorreo = new List<GrupoCorreo>();
            entidad = null;
            reader = null;
            clientResponse = new ClientResponse();
            clientResponse.Status = "OK";
        }


        public ClientResponse getGrupoCorreoXOrigen(int origen)
        {
            try
            {
                using (conexion = new SqlConnection(ConexionDAO.cnx))
                {
                    using (comando = new SqlCommand("usp_sel_grupo_x_origen", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@origen", origen);
                        conexion.Open();
                        using (reader = comando.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                entidad = new GrupoCorreo();
                                entidad.id = Convert.ToInt32(reader["id"] == DBNull.Value ? "" : reader["id"]);
                                entidad.descripcion = Convert.ToString(reader["descripcion"] == DBNull.Value ? "" : reader["descripcion"]);
                                list_grupocorreo.Add(entidad);
                            }
                        }
                        clientResponse.DataJson = JsonConvert.SerializeObject(list_grupocorreo).ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                clientResponse.Mensaje = ex.Message;
                clientResponse.Status = "ERROR";
            }
            finally
            {
                conexion.Close();
                conexion.Dispose();
                comando.Dispose();
                reader.Dispose();
            }

            return clientResponse;
        }

        public ClientResponse getGrupoCorreoNxOrigen(int origen)
        {
            try
            {
                using (conexion = new SqlConnection(ConexionDAO.cnx))
                {
                    using (comando = new SqlCommand("usp_sel_grupo_N_x_origen", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@origen", origen);
                        conexion.Open();
                        using (reader = comando.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                entidad = new GrupoCorreo();
                                entidad.id = Convert.ToInt32(reader["id"] == DBNull.Value ? "" : reader["id"]);
                                entidad.descripcion = Convert.ToString(reader["descripcion"] == DBNull.Value ? "" : reader["descripcion"]);
                                list_grupocorreo.Add(entidad);
                            }
                        }
                        clientResponse.DataJson = JsonConvert.SerializeObject(list_grupocorreo).ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                clientResponse.Mensaje = ex.Message;
                clientResponse.Status = "ERROR";
            }
            finally
            {
                conexion.Close();
                conexion.Dispose();
                comando.Dispose();
                reader.Dispose();
            }

            return clientResponse;
        }

        public ClientResponse getGrupoCorreoCombo(int origen)
        {
            try
            {
                using (conexion = new SqlConnection(ConexionDAO.cnx))
                {
                    using (comando = new SqlCommand("usp_sel_grupo_correo_combo", conexion))
                    {                        
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@origen", origen);
                        conexion.Open();
                        using (reader = comando.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                entidad = new GrupoCorreo();
                                entidad.id = Convert.ToInt32(reader["id"] == DBNull.Value ? "" : reader["id"]);
                                entidad.descripcion = Convert.ToString(reader["descripcion"] == DBNull.Value ? "" : reader["descripcion"]);
                                list_grupocorreo.Add(entidad);
                            }
                        }
                        clientResponse.DataJson = JsonConvert.SerializeObject(list_grupocorreo).ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                clientResponse.Mensaje = ex.Message;
                clientResponse.Status = "ERROR";
            }
            finally
            {
                conexion.Close();
                conexion.Dispose();
                comando.Dispose();
                reader.Dispose();
            }

            return clientResponse;
        }
        public ClientResponse getLstGrupoCorreo(int origen, string grupo, int estado, int paginaActual, int RegistroXPagina)
        {
            try
            {
                int recordCount = 0;
                using (conexion = new SqlConnection(ConexionDAO.cnx))
                {
                    using (comando = new SqlCommand("usp_sel_grupocorreo", conexion))
                    {
                        //cmd.Parameters.AddWithValue("@skey", skey);
                        comando.Parameters.AddWithValue("@origen", origen);
                        comando.Parameters.AddWithValue("@grupo", grupo);
                        comando.Parameters.AddWithValue("@estado", estado);
                        comando.Parameters.AddWithValue("@vi_Pagina", paginaActual);
                        comando.Parameters.AddWithValue("@vi_RegistrosporPagina", RegistroXPagina);
                        comando.Parameters.Add("@vi_RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                        comando.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        using (reader = comando.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                entidad = new GrupoCorreo();
                                entidad.id = Convert.ToInt32(reader["id"] == DBNull.Value ? 0 : reader["id"]);
                                entidad.descripcion = Convert.ToString(reader["descripcion"] == DBNull.Value ? "" : reader["descripcion"]);
                                entidad.descestado = Convert.ToString(reader["desestado"] == DBNull.Value ? "" : reader["desestado"]);
                                entidad.descorigen = Convert.ToString(reader["desorigen"] == DBNull.Value ? "" : reader["desorigen"]);
                                entidad.estado = Convert.ToInt32(reader["estado"] == DBNull.Value ? 0 : reader["estado"]);
                                entidad.origen = Convert.ToInt32(reader["origen"] == DBNull.Value ? 0 : reader["origen"]);
                                list_grupocorreo.Add(entidad);
                            }
                        }

                        recordCount = Convert.ToInt32(comando.Parameters["@vi_RecordCount"].Value);
                        Pagination responsepaginacion = new Pagination()
                        {
                            TotalItems = recordCount,
                            TotalPages = (int)Math.Ceiling((double)recordCount / 10)                            
                        };
                        clientResponse.paginacion = JsonConvert.SerializeObject(responsepaginacion).ToString();
                        clientResponse.DataJson = JsonConvert.SerializeObject(list_grupocorreo).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                clientResponse.Mensaje = ex.Message;
                clientResponse.Status = "ERROR";
            }
            finally
            {
                conexion.Close();
                conexion.Dispose();
                comando.Dispose();
                reader.Dispose();
            }
            
            return clientResponse;

        }
        public ClientResponse InsertGrupoCorreo(GrupoCorreo objeto)
        {        
            try
            {
                using (conexion = new SqlConnection(ConexionDAO.cnx))
                {
                    using (comando = new SqlCommand("usp_ins_grupocorreo", conexion))
                    {
                        comando.Parameters.AddWithValue("@descripcion", objeto.descripcion);
                        comando.Parameters.AddWithValue("@estado", objeto.estado);
                        comando.Parameters.AddWithValue("@origen", objeto.origen);
                        comando.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;
                        comando.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        comando.ExecuteNonQuery();
                        int id = Convert.ToInt32(comando.Parameters["@id"].Value);
                        clientResponse.Id = id;
                        clientResponse.Mensaje = "Se registro de grupo de correo satisfactoriamente";
                        clientResponse.Status = "OK";

                    }
                }
            }
            catch (Exception ex)
            {
                clientResponse.Mensaje = ex.Message;
                clientResponse.Status = "ERROR";
            }
            finally
            {
                conexion.Close();
                conexion.Dispose();
                comando.Dispose();                
            }         
            return clientResponse;
        }

        public ClientResponse ActualiarGrupoCorreo(GrupoCorreo objeto)
        {
            try
            {
                using (conexion = new SqlConnection(ConexionDAO.cnx))
                {
                    using (comando = new SqlCommand("usp_upd_grupocorreo", conexion))
                    {
                        comando.Parameters.AddWithValue("@id", objeto.id);
                        comando.Parameters.AddWithValue("@descripcion", objeto.descripcion);
                        comando.Parameters.AddWithValue("@estado", objeto.estado);
                        comando.Parameters.AddWithValue("@origen", objeto.origen);
                        comando.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        comando.ExecuteNonQuery();
                        clientResponse.Mensaje = "Se actualizo de grupo de correo satisfactoriamente";
                        clientResponse.Status = "OK";

                    }
                }
            }
            catch (Exception ex)
            {
                clientResponse.Mensaje = ex.Message;
                clientResponse.Status = "ERROR";
            }
            finally
            {
                conexion.Close();
                conexion.Dispose();
                comando.Dispose();
            }
            return clientResponse;
        }


        public ClientResponse EliminarGrupoCorreo(GrupoCorreo objeto)
        {
            try
            {
                using (conexion = new SqlConnection(ConexionDAO.cnx))
                {
                    using (comando = new SqlCommand("usp_del_grupocorreo", conexion))
                    {
                        comando.Parameters.AddWithValue("@id", objeto.id);                       
                        comando.Parameters.AddWithValue("@estado", objeto.estado);                       
                        comando.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        comando.ExecuteNonQuery();
                        clientResponse.Mensaje = "Se elimino satisfactoriamente el registro";
                        clientResponse.Status = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                clientResponse.Mensaje = ex.Message;
                clientResponse.Status = "ERROR";
            }
            finally
            {
                conexion.Close();
                conexion.Dispose();
                comando.Dispose();
            }
            return clientResponse;
        }

        #region IDisposable Support
        private bool disposedValue = false; // Para detectar llamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: elimine el estado administrado (objetos administrados).
                }

                // TODO: libere los recursos no administrados (objetos no administrados) y reemplace el siguiente finalizador.
                // TODO: configure los campos grandes en nulos.

                disposedValue = true;
            }
        }

        // TODO: reemplace un finalizador solo si el anterior Dispose(bool disposing) tiene código para liberar los recursos no administrados.
        // ~TipoProductoDAO() {
        //   // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
        //   Dispose(false);
        // }

        // Este código se agrega para implementar correctamente el patrón descartable.
        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
            Dispose(true);
            // TODO: quite la marca de comentario de la siguiente línea si el finalizador se ha reemplazado antes.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
