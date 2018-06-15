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
    public class GestionCorreoDAO : IDisposable
    {
        private static List<GestionCorreo> listgestioncorreo;
        private static GestionCorreo entidad;
        private static SqlDataReader reader;
        private static SqlConnection conexion;
        private static SqlCommand comando;
        private static ClientResponse clientResponse;

        public GestionCorreoDAO()
        {
            listgestioncorreo = new List<GestionCorreo>();

            entidad = null;
            reader = null;
            clientResponse = new ClientResponse();
            clientResponse.Status = "OK";
        }

        public ClientResponse getLstGestionCorreoXGrupo(string id_grupo_correo)
        {
            try
            {
                
                using (conexion = new SqlConnection(ConexionDAO.cnx))
                {
                    using (comando = new SqlCommand("usp_sel_correos_x_grupo", conexion))
                    {
                        comando.Parameters.AddWithValue("@id_grupo_correo", id_grupo_correo);
                        comando.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        using (reader = comando.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                entidad = new GestionCorreo();
                                entidad.id = Convert.ToInt32(reader["id"] == DBNull.Value ? 0 : reader["id"]);                               
                                GrupoCorreo grupocorreo = new GrupoCorreo();
                                grupocorreo.id = Convert.ToInt32(reader["id_grupo_correo"] == DBNull.Value ? 0 : reader["id_grupo_correo"]);
                                //grupocorreo.descripcion = Convert.ToString(reader["descgrupo_correo"] == DBNull.Value ? "" : reader["descgrupo_correo"]);
                                entidad.Nombre1 = Convert.ToString(reader["Nombre1"] == DBNull.Value ? "" : reader["Nombre1"]);
                                entidad.Nombre2 = Convert.ToString(reader["Nombre2"] == DBNull.Value ? "" : reader["Nombre2"]);
                                entidad.ApePaterno = Convert.ToString(reader["apePaterno"] == DBNull.Value ? "" : reader["apePaterno"]);
                                entidad.ApeMaterno = Convert.ToString(reader["apeMaterno"] == DBNull.Value ? "" : reader["apeMaterno"]);
                                entidad.Email = Convert.ToString(reader["email"] == DBNull.Value ? "" : reader["email"]);
                                entidad.Tokens = Convert.ToString(reader["tokens"] == DBNull.Value ? "" : reader["tokens"]);
                                
                                entidad.grupocorreo = grupocorreo;
                                listgestioncorreo.Add(entidad);
                            }
                        }
                        clientResponse.DataJson = JsonConvert.SerializeObject(listgestioncorreo).ToString();
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
        public ClientResponse getLstGestionCorreo(int paginaActual, int RegistroXPagina, int id_cbo_origenfil, int id_cbo_grupo_consultar)
        {
            try
            {
                int recordCount = 0;
                using (conexion = new SqlConnection(ConexionDAO.cnx))
                {
                    using (comando = new SqlCommand("usp_sel_gestioncorreo", conexion))
                    {
                        comando.Parameters.AddWithValue("@id_cbo_origenfil", id_cbo_origenfil);
                        comando.Parameters.AddWithValue("@id_grupo_correo", id_cbo_grupo_consultar);
                        comando.Parameters.AddWithValue("@vi_Pagina", paginaActual);
                        comando.Parameters.AddWithValue("@vi_RegistrosporPagina", RegistroXPagina);
                        comando.Parameters.Add("@vi_RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                        comando.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        using (reader = comando.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                entidad = new GestionCorreo();
                                entidad.id = Convert.ToInt32(reader["id"] == DBNull.Value ? 0 : reader["id"]);
                                entidad.Nombre1 = Convert.ToString(reader["Nombre1"] == DBNull.Value ? "" : reader["Nombre1"]);
                                entidad.Nombre2 = Convert.ToString(reader["Nombre2"] == DBNull.Value ? "" : reader["Nombre2"]);
                                entidad.ApePaterno = Convert.ToString(reader["apePaterno"] == DBNull.Value ? "" : reader["apePaterno"]);
                                entidad.ApeMaterno = Convert.ToString(reader["apeMaterno"] == DBNull.Value ? "" : reader["apeMaterno"]);
                                entidad.Email = Convert.ToString(reader["email"] == DBNull.Value ? "" : reader["email"]);
                                
                                entidad.id_estado = Convert.ToInt32(reader["id_estado"] == DBNull.Value ? 0 : reader["id_estado"]);
                                entidad.descestado = Convert.ToString(reader["desestado"] == DBNull.Value ? "" : reader["desestado"]);
                                GrupoCorreo grupocorreo = new GrupoCorreo();
                                grupocorreo.id = Convert.ToInt32(reader["id_grupo_correo"] == DBNull.Value ? 0 : reader["id_grupo_correo"]);
                                grupocorreo.descripcion = Convert.ToString(reader["descgrupo_correo"] == DBNull.Value ? "" : reader["descgrupo_correo"]);
                                entidad.grupocorreo = grupocorreo;
                                entidad.fechabaja = Convert.ToString(reader["fechabaja"] == DBNull.Value ? "" : reader["fechabaja"]);
                                listgestioncorreo.Add(entidad);
                            }
                        }

                        recordCount = Convert.ToInt32(comando.Parameters["@vi_RecordCount"].Value);
                        Pagination responsepaginacion = new Pagination()
                        {
                            TotalItems = recordCount,
                            TotalPages = (int)Math.Ceiling((double)recordCount / 10)
                        };
                        clientResponse.paginacion = JsonConvert.SerializeObject(responsepaginacion).ToString();
                        clientResponse.DataJson = JsonConvert.SerializeObject(listgestioncorreo).ToString();
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

        public ClientResponse InsertGestionCorreoAutomatico(List<GestionCorreo> list)
        {
            try
            {
                List<GestionCorreo> GrupoCorreosNuevos = list.Where(i => i.grupocorreo.id == 0).ToList();
                List<GestionCorreo> GrupoCorreosExistentes = list.Where(i => i.grupocorreo.id > 0).ToList();
                List<GestionCorreo> DistinctList = GrupoCorreosNuevos.GroupBy(a => a.grupocorreo.descripcion).Select(g => g.First()).ToList();
                List<GestionCorreo> ListInsert = new List<GestionCorreo>();                
                foreach (GestionCorreo item in DistinctList)
                {
                    int id_grupo = 0;
                    for (int e = 0; e < GrupoCorreosNuevos.Count(); e++)
                    {
                        if (item.grupocorreo.descripcion.Equals(GrupoCorreosNuevos[e].grupocorreo.descripcion))
                        {
                            if (id_grupo == 0) {
                                ClientResponse response = null;
                                using (GrupoCorreoDAO grupocorreo = new GrupoCorreoDAO()) {
                                    response = grupocorreo.InsertGrupoCorreo(item.grupocorreo);
                                }
                                id_grupo = response.Id;
                                GrupoCorreosNuevos[e].grupocorreo.id = id_grupo;
                            } else {
                                GrupoCorreosNuevos[e].grupocorreo.id = id_grupo;
                            }
                        }
                    }
                }
                ListInsert = GrupoCorreosNuevos.Concat(GrupoCorreosExistentes).ToList();

                foreach (GestionCorreo item in ListInsert)
                {
                    ClientResponse response = InsertGestionCorreo(item);                  
                }
                clientResponse.Mensaje = "Se registro correo satisfactoriamente";
                clientResponse.Status = "OK";

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

        public ClientResponse InsertGestionCorreo(GestionCorreo objeto)
        {
            try
            {
                using (conexion = new SqlConnection(ConexionDAO.cnx))
                {
                    using (comando = new SqlCommand("usp_ins_gestioncorreo", conexion))
                    {
                        comando.Parameters.AddWithValue("@id_grupo_correo", objeto.grupocorreo.id);
                        comando.Parameters.AddWithValue("@Nombre1", objeto.Nombre1);
                        comando.Parameters.AddWithValue("@Nombre2", objeto.Nombre2);
                        comando.Parameters.AddWithValue("@apePaterno", objeto.ApePaterno);
                        comando.Parameters.AddWithValue("@apeMaterno", objeto.ApeMaterno);
                        comando.Parameters.AddWithValue("@email", objeto.Email);
                        comando.Parameters.AddWithValue("@id_estado", objeto.id_estado);
                        comando.Parameters.AddWithValue("@UsuarioCreacion", objeto.UsuarioCreacion);
                        comando.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        comando.ExecuteNonQuery();
                        clientResponse.Mensaje = "Se registro correo satisfactoriamente";
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

        public ClientResponse DarDebajaEnviocorreos(GestionCorreo objeto)
        {
            try
            {
                using (conexion = new SqlConnection(ConexionDAO.cnx))
                {
                    using (comando = new SqlCommand("sp_upd_darjar_envio_correo", conexion))
                    {
                        comando.Parameters.AddWithValue("@tokens", objeto.Tokens);
                        comando.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        comando.ExecuteNonQuery();
                        clientResponse.Mensaje = "Se desafilio el envio de correos  satisfactoriamente";
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

        public ClientResponse ImportarCorreos()
        {
            try
            {
                using (conexion = new SqlConnection(ConexionDAO.cnx))
                {
                    using (comando = new SqlCommand("sp_upd_importar_correos", conexion))
                    {  

                        comando.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        comando.ExecuteNonQuery();
                        clientResponse.Mensaje = "Se importo correctamente los correos";
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
        public ClientResponse ActualiarGestionCorreo(GestionCorreo objeto)
        {
            try
            {
                using (conexion = new SqlConnection(ConexionDAO.cnx))
                {
                    using (comando = new SqlCommand("usp_upd_gestioncorreo", conexion))
                    {
                        comando.Parameters.AddWithValue("@id", objeto.id);
                        comando.Parameters.AddWithValue("@id_grupo_correo", objeto.grupocorreo.id);
                        comando.Parameters.AddWithValue("@Nombre1", objeto.Nombre1);
                        comando.Parameters.AddWithValue("@Nombre2", objeto.Nombre2);
                        comando.Parameters.AddWithValue("@apePaterno", objeto.ApePaterno);
                        comando.Parameters.AddWithValue("@apeMaterno", objeto.ApeMaterno);
                        comando.Parameters.AddWithValue("@email", objeto.Email);
                        comando.Parameters.AddWithValue("@id_estado", objeto.id_estado);
                        comando.Parameters.AddWithValue("@UsuarioActualizacion", objeto.UsuarioModificacion);


                        comando.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        comando.ExecuteNonQuery();
                        clientResponse.Mensaje = "Se actualizo correo satisfactoriamente";
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


        public ClientResponse EliminarGestionCorreo(GestionCorreo objeto)
        {
            try
            {
                using (conexion = new SqlConnection(ConexionDAO.cnx))
                {
                    using (comando = new SqlCommand("usp_del_gestioncorreo", conexion))
                    {
                        comando.Parameters.AddWithValue("@id", objeto.id);
                        comando.Parameters.AddWithValue("@estado", objeto.id_estado);
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
