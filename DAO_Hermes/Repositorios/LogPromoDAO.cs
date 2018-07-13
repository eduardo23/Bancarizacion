using DAO_Hermes.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace DAO_Hermes.Repositorios
{
    public class LogPromoDAO : IDisposable
    {
        private static List<LogPromoDet> listLogPromoDet;
        private static SqlConnection conexion;
        private static SqlCommand comando;
        private static SqlDataReader reader;
        private static ClientResponse clientResponse;
        private static LogPromoDet entidad;

        public LogPromoDAO()
        {
            listLogPromoDet = new List<LogPromoDet>();
            entidad = null;
            reader = null;
            clientResponse = new ClientResponse();
            clientResponse.Status = "OK";
        }

        public ClientResponse InsertLogPromo(LogPromo objeto) {
            try
            {
                XElement root = new XElement("ROOT");
                foreach (LogPromoDet detalle in objeto.LogPromoDet)
                {
                    XElement element = new XElement("Detalle",
                    new XElement("id_grupo_correo", detalle.id_grupo_correo),
                    new XElement("destinatario", detalle.destinatario)
                    );
                    root.Add(element);
                }
                string xml = root.ToString();

                using (conexion = new SqlConnection(ConexionDAO.cnx))
                {
                    using (comando = new SqlCommand("usp_ins_LogPromo", conexion))
                    {
                        comando.Parameters.AddWithValue("@xml", xml);
                        comando.Parameters.AddWithValue("@remitente", objeto.remitente);
                        comando.Parameters.AddWithValue("@asunto", objeto.asunto);
                        comando.Parameters.AddWithValue("@plantillaID", objeto.PlantillaID);
                        comando.Parameters.Add("@Ind", SqlDbType.Int).Direction = ParameterDirection.Output;
                        comando.Parameters.Add("@Mensaje", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                        comando.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        comando.ExecuteNonQuery();
                        int ind = Convert.ToInt32(comando.Parameters["@Ind"].Value);
                        string mensaje = Convert.ToString(comando.Parameters["@Mensaje"].Value);

                        if (ind > 0)
                        {
                            clientResponse.Mensaje = mensaje;
                            clientResponse.Status = "Ok";
                        }
                        else
                        {
                            clientResponse.Mensaje = mensaje;
                            clientResponse.Status = "ERROR";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                clientResponse.Mensaje = ex.Message;
                clientResponse.Status = "ERROR";
            }
            finally {
                conexion.Close();
                conexion.Dispose();
                comando.Dispose();
            }
            return clientResponse;
        }

        public ClientResponse getLstLogPromo(string remitente, DateTime? FechaInicial, DateTime? FechaFinal, int paginaActual, int RegistroXPagina)
        {
            try
            {
                int recordCount = 0;
                using (conexion = new SqlConnection(ConexionDAO.cnx))
                {
                    using (comando = new SqlCommand("usp_lista_logpromo", conexion))
                    {
                        comando.Parameters.AddWithValue("@remitente", remitente);
                        comando.Parameters.AddWithValue("@StartDate", FechaInicial);
                        comando.Parameters.AddWithValue("@EndDate", FechaFinal);
                        comando.Parameters.AddWithValue("@vi_Pagina", paginaActual);
                        comando.Parameters.AddWithValue("@vi_RegistrosporPagina", RegistroXPagina);
                        comando.Parameters.Add("@vi_RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                        comando.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        using (reader = comando.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                entidad = new LogPromoDet();
                                GrupoCorreo grupocorreo = new GrupoCorreo();
                                grupocorreo.id = Convert.ToInt32(reader["id_grupo_correo"] == DBNull.Value ? 0 : reader["id_grupo_correo"]);
                                grupocorreo.descripcion = Convert.ToString(reader["ds_grupo_correo"] == DBNull.Value ? "" : reader["ds_grupo_correo"]);
                                entidad.Cantidad = Convert.ToInt32(reader["cantidad"] == DBNull.Value ? 0 : reader["cantidad"]);

                                entidad.GrupoCorreo = grupocorreo;

                                LogPromo logpromo = new LogPromo();
                                logpromo.ID= Convert.ToInt32(reader["logPromoId"] == DBNull.Value ? 0 : reader["logPromoId"]);
                                logpromo.asunto = Convert.ToString(reader["asunto"] == DBNull.Value ? "" : reader["asunto"]);
                                logpromo.fecha = Convert.ToString(reader["fecha"] == DBNull.Value ? "" : reader["fecha"]);

                                Plantilla plantilla = new Plantilla();
                                plantilla.id = Convert.ToInt32(reader["plantillaID"] == DBNull.Value ? 0 : reader["plantillaID"]);
                                plantilla.descripcion= Convert.ToString(reader["plantillads"] == DBNull.Value ? "" : reader["plantillads"]);
                                logpromo.Plantilla = plantilla;

                                Users users = new Users();
                                users.UserName = Convert.ToString(reader["remitente"] == DBNull.Value ? "" : reader["remitente"]);
                                users.Nombre = Convert.ToString(reader["Nombre"] == DBNull.Value ? "" : reader["Nombre"]);
                                users.ApellidoPaterno = Convert.ToString(reader["apellidopaterno"] == DBNull.Value ? "" : reader["apellidopaterno"]);
                                users.ApellidoMaterno = Convert.ToString(reader["apellidoMaterno"] == DBNull.Value ? "" : reader["apellidoMaterno"]);
                                logpromo.Users = users;

                                entidad.LogPromo = logpromo;

                                listLogPromoDet.Add(entidad);
                            }
                        }

                        recordCount = Convert.ToInt32(comando.Parameters["@vi_RecordCount"].Value);
                        Pagination responsepaginacion = new Pagination()
                        {
                            TotalItems = recordCount,
                            TotalPages = (int)Math.Ceiling((double)recordCount / 10)
                        };
                        clientResponse.paginacion = JsonConvert.SerializeObject(responsepaginacion).ToString();
                        clientResponse.DataJson = JsonConvert.SerializeObject(listLogPromoDet).ToString();
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

        public ClientResponse getLstRemitente()
        {
            try
            {
                using (conexion = new SqlConnection(ConexionDAO.cnx))
                {
                    using (comando = new SqlCommand("usp_lst_remitente_promo", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        using (reader = comando.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                entidad = new LogPromoDet();

                                LogPromo logpromo = new LogPromo();
                                logpromo.remitente= Convert.ToString(reader["remitente"] == DBNull.Value ? "" : reader["remitente"]);
                                entidad.LogPromo = logpromo;

                                listLogPromoDet.Add(entidad);
                            }
                        }
                        clientResponse.DataJson = JsonConvert.SerializeObject(listLogPromoDet).ToString();
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
