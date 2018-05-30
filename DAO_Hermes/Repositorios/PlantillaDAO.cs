using DAO_Hermes.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAO_Hermes.Repositorios
{
    public class PlantillaDAO : IDisposable
    {
        private static List<Plantilla> list_plantilla;
        private static Plantilla entidad;
        private static SqlDataReader reader;
        private static SqlConnection conexion;
        private static SqlCommand comando;
        private static ClientResponse clientResponse;
        public PlantillaDAO()
        {
            list_plantilla = new List<Plantilla>();
            entidad = null;
            reader = null;
            clientResponse = new ClientResponse();
            clientResponse.Status = "OK";
        }


        public ClientResponse AnularPlantilla(int id_planilla)
        {
            try
            {
                using (conexion = new SqlConnection(ConexionDAO.cnx))
                {
                    using (comando = new SqlCommand("usp_anular_plantilla", conexion))
                    {
                        comando.Parameters.AddWithValue("@id_plantilla", id_planilla);                       
                        comando.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        comando.ExecuteNonQuery();
                        clientResponse.Mensaje = "Se anulo satisfactoriamente la plantilla";
                        clientResponse.Status = "Ok";

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

        public ClientResponse getPlantillaXId(int Id_Plantilla)
        {
            try
            {
                using (conexion = new SqlConnection(ConexionDAO.cnx))
                {
                    using (comando = new SqlCommand("usp_sel_plantilla_x_id", conexion))
                    {
                        comando.Parameters.AddWithValue("@id_plantilla", Id_Plantilla);
                        comando.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        using (reader = comando.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                entidad = new Plantilla();
                                entidad.id = Convert.ToInt32(reader["id"] == DBNull.Value ? "" : reader["id"]);
                                entidad.descripcion = Convert.ToString(reader["descripcion"] == DBNull.Value ? "" : reader["descripcion"]);
                                entidad.NombreArchivoHtml = Convert.ToString(reader["NombreArchivoHtml"] == DBNull.Value ? "" : reader["NombreArchivoHtml"]);
                                entidad.ruta_plantilla_html = Convert.ToString(reader["ruta_plantilla_html"] == DBNull.Value ? "" : reader["ruta_plantilla_html"]);
                             
                            }
                        }
                        clientResponse.DataJson = JsonConvert.SerializeObject(entidad).ToString();
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

        public ClientResponse getPlantilla()
        {
            try
            {
                using (conexion = new SqlConnection(ConexionDAO.cnx))
                {
                    using (comando = new SqlCommand("usp_sel_planilla", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        using (reader = comando.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                entidad = new Plantilla();
                                entidad.id = Convert.ToInt32(reader["id"] == DBNull.Value ? "" : reader["id"]);
                                entidad.descripcion = Convert.ToString(reader["descripcion"] == DBNull.Value ? "" : reader["descripcion"]);
                                entidad.NombreArchivoHtml = Convert.ToString(reader["NombreArchivoHtml"] == DBNull.Value ? "" : reader["NombreArchivoHtml"]);
                                entidad.ruta_plantilla_html = Convert.ToString(reader["ruta_plantilla_html"] == DBNull.Value ? "" : reader["ruta_plantilla_html"]);
                                entidad.fl_nuevo = 1; 
                                entidad.list_plantilla_detalle = getPlantillaDetalle(entidad.id);
                                list_plantilla.Add(entidad);
                            }
                        }
                        clientResponse.DataJson = JsonConvert.SerializeObject(list_plantilla).ToString();
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

        public List<Plantilla_Detalle> getPlantillaDetalle(int idPlantilla)
        {
            SqlConnection conexions = null;
            SqlCommand comandos = null;
            SqlDataReader readers = null;
            List<Plantilla_Detalle> list = new List<Plantilla_Detalle>();
            try
            {                
                Plantilla_Detalle plantilla_detalle = null;
                using ( conexions = new SqlConnection(ConexionDAO.cnx))
                {
                    using ( comandos = new SqlCommand("usp_sel_plantilladet_x_id", conexions))
                    {
                        comandos.Parameters.AddWithValue("@id_plantilla", idPlantilla);
                        comandos.CommandType = CommandType.StoredProcedure;
                        conexions.Open();
                        using ( readers = comandos.ExecuteReader())
                        {
                            while (readers.Read())
                            {
                                plantilla_detalle = new Plantilla_Detalle();
                                plantilla_detalle.id = Convert.ToInt32(readers["id"] == DBNull.Value ? 0 : readers["id"]);                               
                                plantilla_detalle.NombreArchivoImagen = Convert.ToString(readers["NombreArchivoImagen"] == DBNull.Value ? "" : readers["NombreArchivoImagen"]);
                                plantilla_detalle.ruta_imagen = Convert.ToString(readers["ruta_imagen"] == DBNull.Value ? "" : readers["ruta_imagen"]);
                                plantilla_detalle.ruta_site_imagen = Convert.ToString(readers["ruta_site_imagen"] == DBNull.Value ? "" : readers["ruta_site_imagen"]);
                                plantilla_detalle.id_estado = Convert.ToInt32(readers["id_estado"] == DBNull.Value ? 0 : readers["id_estado"]);
                                plantilla_detalle.fl_nuevo = 1;
                                list.Add(plantilla_detalle);
                            }
                        }                       
                    }
                }

            }
            catch (Exception ex)
            {
               
            }
            finally
            {
                conexions.Close();
                conexions.Dispose();
                comandos.Dispose();
                readers.Dispose();
            }

            return list;
        }
        public ClientResponse InsertPantilla(Plantilla objeto)
        {
            try
            {
                XElement root = new XElement("ROOT");              
                foreach (Plantilla_Detalle detalle in objeto.list_plantilla_detalle)
                {
                    XElement address = new XElement("Detalle",
                    new XElement("NombreArchivoImagen", detalle.NombreArchivoImagen),
                    new XElement("ruta_imagen", detalle.ruta_imagen),
                    new XElement("ruta_site_imagen", detalle.ruta_site_imagen)
                    );
                    root.Add(address);
                }
                string xml = root.ToString();
                using (conexion = new SqlConnection(ConexionDAO.cnx))
                {                  
                    using (comando = new SqlCommand("usp_ins_planilla", conexion))
                    {
                        comando.Parameters.AddWithValue("@xml", xml);
                        comando.Parameters.AddWithValue("@descripcion", objeto.descripcion);
                        comando.Parameters.AddWithValue("@NombreArchivoHtml", objeto.NombreArchivoHtml);
                        comando.Parameters.AddWithValue("@ruta_plantilla_html", objeto.ruta_plantilla_html);
                        comando.Parameters.AddWithValue("@id_estado", objeto.id_estado);
                        comando.Parameters.Add("@Ind", SqlDbType.Int).Direction = ParameterDirection.Output;
                        comando.Parameters.Add("@Mensaje", SqlDbType.VarChar,200).Direction = ParameterDirection.Output;
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
