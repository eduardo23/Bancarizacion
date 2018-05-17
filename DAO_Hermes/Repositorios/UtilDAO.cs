using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace DAO_Hermes.Repositorios
{
   public class UtilDAO:IDisposable
    {
        public string ObtenerValorParametro(string Categoria, string Nombre)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var valor = db.Parametros.Where(p => p.Categoria == Categoria && p.Nombre == Nombre).Select(p => p.Valor).First();
                return valor.ToString();
            }
        }

        public string ObtenerValorParametroDes(string Categoria, string Nombre, string Descripcion)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var valor = db.Parametros.Where(p => p.Categoria == Categoria && p.Nombre == Nombre && p.Descripcion==Descripcion).Select(p => p.Valor).First();
                return valor.ToString();
            }
        }

        public List<string> ListarCaracteresEspeciales()
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LISTAR_CHARS_SPECIALS", cn))
                {
                    //cmd.Parameters.AddWithValue("@id", CodigoId);
                    //cmd.Parameters.AddWithValue("@productID", productoId);
                    //cmd.Parameters.AddWithValue("@buscar", buscar);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    List<string> Caracteres = new List<string>();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while(rd.Read())
                        {
                            Caracteres.Add(Convert.ToString(rd[0]));
                        }
                    }
                    return Caracteres;
                }
            }
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
        // ~UtilDAO() {
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
