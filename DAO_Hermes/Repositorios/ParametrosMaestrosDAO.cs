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
    public class ParametrosMaestrosDAO : IDisposable
    {

        private static List<ParametrosMaestros> list_parametromaestro;
        private static ParametrosMaestros entidad;
        private static SqlDataReader reader;
        private static SqlConnection conexion;
        private static SqlCommand comando;
        private static ClientResponse clientResponse;
        public ParametrosMaestrosDAO()
        {
            list_parametromaestro = new List<ParametrosMaestros>();
            entidad = null;
            reader = null;
            clientResponse = new ClientResponse();
            clientResponse.Status = "OK";
        }


        public ClientResponse getLstParametroMaestro(string skey)
        {
            try
            {
                using (conexion = new SqlConnection(ConexionDAO.cnx))
                {
                    using (comando = new SqlCommand("usp_sel_parametros_maestros", conexion))
                    {
                        comando.Parameters.AddWithValue("@skey", skey);
                        comando.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        using ( reader = comando.ExecuteReader())
                        {
                            
                            while (reader.Read())
                            {
                                entidad = new ParametrosMaestros();
                                entidad.valor = Convert.ToString(reader["valor"] == DBNull.Value ? "" : reader["valor"]);
                                entidad.descripcion = Convert.ToString(reader["descripcion"] == DBNull.Value ? "" : reader["descripcion"]);
                                list_parametromaestro.Add(entidad);
                            }                            
                        }
                        clientResponse.DataJson = JsonConvert.SerializeObject(list_parametromaestro).ToString();
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
