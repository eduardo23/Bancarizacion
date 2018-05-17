using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Hermes.Repositorios
{
    public class FuncionesAuxDAO: IDisposable
    {
        public DataSet ConsultaPago(int institucionid, int productoid, int ciaseguroid, int pagadoid, int bancoid, int monedaid, 
                                             DateTime  fechaInicio, DateTime fechaFin, string texto, int filtrarfecha)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("Operacion.Usp_Sel_CodigoDetalle_By_Pagos", cn))
                {
                    cmd.Parameters.AddWithValue("@InstitucionEducativaId", institucionid);
                    cmd.Parameters.AddWithValue("@ProductoId", productoid);
                    cmd.Parameters.AddWithValue("@CIASeguroID", ciaseguroid);
                    cmd.Parameters.AddWithValue("@IsPagado", pagadoid);
                    cmd.Parameters.AddWithValue("@BancoID", bancoid);
                    cmd.Parameters.AddWithValue("@MonedaID", monedaid);
                    cmd.Parameters.AddWithValue("@StartDate", fechaInicio);
                    cmd.Parameters.AddWithValue("@EndDate", fechaFin);
                    cmd.Parameters.AddWithValue("@TextoBusqueda", texto);
                    cmd.Parameters.AddWithValue("@FiltrarFechaVigencia", filtrarfecha);

                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds;
                    }
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
