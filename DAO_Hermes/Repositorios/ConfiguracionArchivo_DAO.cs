using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAO_Hermes.Repositorios
{
    public class ConfiguracionArchivo_DAO: IDisposable
    {
        public List<ConfiguracionArchivo> Listar()
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                return db.ConfiguracionArchivo.ToList();
            }
        }

        public List<CIASeguro> ListarRecaudador()
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                return db.CIASeguro.Where(p => p.Nombre.Contains("HERMES")).ToList();
            }
        }


        public DataSet LISTAR_DET_SCOTIANK_CARGA(int BANCOID, int Monedaid, string CUENTA, int RecaudadorID)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LIST_DET_SCOTIANK_CARGA", cn))
                {
                    cmd.Parameters.AddWithValue("@BANCOID", BANCOID);
                    cmd.Parameters.AddWithValue("@MONEDAID", Monedaid);
                   cmd.Parameters.AddWithValue("@CUENTA", CUENTA);
                    cmd.Parameters.AddWithValue("@RecaudadorID", RecaudadorID);
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

        public DataSet LISTAR_DET_BBVA_CARGA(int BANCOID, int Monedaid, string RUC, int RecaudadorID)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LIST_DET_BBVA_CARGA", cn))
                {
                    cmd.Parameters.AddWithValue("@BANCOID", BANCOID);
                    cmd.Parameters.AddWithValue("@MONEDAID", Monedaid);
                    cmd.Parameters.AddWithValue("@RUC", RUC);
                    cmd.Parameters.AddWithValue("@RecaudadorID", RecaudadorID);
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

        public DataSet LISTAR_DET_Interbank_CARGA(int BANCOID, int Monedaid, string Cuenta , int RecaudadorID)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LIST_DET_Interbank_CARGA", cn))
                {
                    cmd.Parameters.AddWithValue("@BANCOID", BANCOID);
                    cmd.Parameters.AddWithValue("@MONEDAID", Monedaid);
                    cmd.Parameters.AddWithValue("@CUENTA", Cuenta);
                    cmd.Parameters.AddWithValue("@RecaudadorID", RecaudadorID);
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
         public DataSet  LIST_PARAM_BANCO_CFG(int BANCOID, int  BancoParametroTipoID )
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LIST_PARAM_BANCO_CFG", cn))
                {
                    cmd.Parameters.AddWithValue("@BANCOID", BANCOID);
                    cmd.Parameters.AddWithValue("@BancoParametroTipoID", BancoParametroTipoID);
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
        public DataSet ObtenerConfiguracionFileCarga( int CiaFileID)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_ObtenerConfiguracionArchivo", cn))
                {
                    cmd.Parameters.AddWithValue("@Tipo", CiaFileID);                 
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
        public int OBTENER_CFG_CARGA(int? CIASeguroID, int ProductoID, int ConfiguracionArchivoTipoID)
        {
            string cnx = "";
            int i = 0;
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_OBTENER_CFG_CARGA", cn))
                {
                    cmd.Parameters.AddWithValue("@CIASeguroID", CIASeguroID);
                    cmd.Parameters.AddWithValue("@ProductoID", ProductoID);
                    cmd.Parameters.AddWithValue("@ConfiguracionArchivoTipoID", ConfiguracionArchivoTipoID);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    i = Convert.ToInt32(cmd.ExecuteScalar());
                    return i;
                }
            }
        }
        public DataSet OBTENER_CAMPOS_CARGA(int ConfiguracionArchivoID)
        {
            string cnx = "";      
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_OBTENER_CAMPOS_CARGA", cn))
                {
                    cmd.Parameters.AddWithValue("@ConfiguracionArchivoID", ConfiguracionArchivoID);                    
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

        public DataSet LIST_Datos_Trama_Onco(string inidate, string findate)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("sp_altasOncosalud", cn))
                {

                    cmd.Parameters.AddWithValue("@StartDate", inidate);
                    cmd.Parameters.AddWithValue("@EndDate", findate);
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
        // ~ConfiguracionArchivo_DAO() {
        //   // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
        //   Dispose(false);
        // }

        // Este código se agrega para implementar correctamente el patrón descartable.
        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
            Dispose(true);
            // TODO: quite la marca de comentario de la siguiente línea si el finalizador se ha reemplazado antes.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
