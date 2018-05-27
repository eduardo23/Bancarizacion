using DAO_Hermes.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Hermes.Repositorios
{
   public class EnvioCorreoPlantillaDAO : IDisposable
    {
        private static List<EnvioCorreoPlantilla> list_envioplantilla;
        private static Plantilla entidad;
        private static SqlDataReader reader;
        private static SqlConnection conexion;
        private static SqlCommand comando;
        private static ClientResponse clientResponse;
        public EnvioCorreoPlantillaDAO()
        {
            list_envioplantilla = new List<EnvioCorreoPlantilla>();
            entidad = null;
            reader = null;
            clientResponse = new ClientResponse();
            clientResponse.Status = "OK";
        }
        public ClientResponse InsertEnvioCorreoPlantilla(EnvioCorreoPlantilla objeto)
        {
            //try
            //{
            //    XElement root = new XElement("ROOT");
            //    foreach (Plantilla_Detalle detalle in objeto.list_plantilla_detalle)
            //    {
            //        XElement address = new XElement("Detalle",
            //        new XElement("NombreArchivoImagen", detalle.NombreArchivoImagen),
            //        new XElement("ruta_imagen", detalle.ruta_imagen)
            //        );
            //        root.Add(address);
            //    }
            //    string xml = root.ToString();
            //    using (conexion = new SqlConnection(ConexionDAO.cnx))
            //    {
            //        using (comando = new SqlCommand("usp_ins_planilla", conexion))
            //        {
            //            comando.Parameters.AddWithValue("@xml", xml);
            //            comando.Parameters.AddWithValue("@descripcion", objeto.descripcion);
            //            comando.Parameters.AddWithValue("@NombreArchivoHtml", objeto.NombreArchivoHtml);
            //            comando.Parameters.AddWithValue("@ruta_plantilla_html", objeto.ruta_plantilla_html);
            //            comando.Parameters.AddWithValue("@id_estado", objeto.id_estado);
            //            comando.Parameters.Add("@Ind", SqlDbType.Int).Direction = ParameterDirection.Output;
            //            comando.Parameters.Add("@Mensaje", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
            //            comando.CommandType = CommandType.StoredProcedure;
            //            conexion.Open();
            //            comando.ExecuteNonQuery();
            //            int ind = Convert.ToInt32(comando.Parameters["@Ind"].Value);
            //            string mensaje = Convert.ToString(comando.Parameters["@Mensaje"].Value);

            //            if (ind > 0)
            //            {
            //                clientResponse.Mensaje = mensaje;
            //                clientResponse.Status = "Ok";
            //            }
            //            else
            //            {
            //                clientResponse.Mensaje = mensaje;
            //                clientResponse.Status = "ERROR";
            //            }
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    clientResponse.Mensaje = ex.Message;
            //    clientResponse.Status = "ERROR";
            //}
            //finally
            //{
            //    conexion.Close();
            //    conexion.Dispose();
            //    comando.Dispose();
            //}
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
