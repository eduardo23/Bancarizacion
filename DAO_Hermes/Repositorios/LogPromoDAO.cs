using DAO_Hermes.ViewModel;
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
    public class LogPromoDAO : IDisposable
    {
        private static SqlConnection conexion;
        private static SqlCommand comando;
        private static ClientResponse clientResponse;

        public LogPromoDAO()
        {
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
