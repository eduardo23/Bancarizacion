using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO_Hermes.ViewModel;
using System.Data.SqlClient;
using System.Data;

namespace DAO_Hermes.Repositorios
{
    public class BancoDAO : IDisposable
    {
        public DataSet ListaPropiedadesBanco(int bancoid, int monedaid)
        {
            string cnx = "";//ggfg
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
                {
                cnx = db.Database.Connection.ConnectionString;
            }                
            using (SqlConnection cn = new SqlConnection(cnx))
                {
                using (SqlCommand cmd = new SqlCommand("USP_PARAM_BANCOS",cn))
                    {
                    cmd.Parameters.AddWithValue("@MONEDAID",monedaid);
                    cmd.Parameters.AddWithValue("@BANCOID", bancoid);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds =new DataSet();
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
        }
             

        public DataSet ListaNumeroCuentasBanco(int bancoid)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LIST_Cuenta_BANCO", cn))
                {
                    cmd.Parameters.AddWithValue("@BANCOID", bancoid);
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

        public bool EXISTE_CTA_BANCO(int bancoid , int  asociacionid)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }

            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_EXISTE_CTA_BANCO", cn))
                {
                    cmd.Parameters.AddWithValue("@IDbanco", bancoid);
                    cmd.Parameters.AddWithValue("@IDAsociacion", asociacionid);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        return dr.HasRows;
                    }
                }
            }
        }

        


        public DataSet ListaCuentasBanco(int bancoid)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LIST_CTAS_BANCO", cn))
                {                    
                    cmd.Parameters.AddWithValue("@BANCOID", bancoid);
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

        public DataSet ListaBancosNombre(string Nombre)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LISTARBANCOSXNOMBRE", cn))
                {
                    cmd.Parameters.AddWithValue("@SearchText", Nombre);
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


        public List<USP_BancoselectALL_Result> ListarBancos()
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                return db.USP_BancoselectALL().ToList();
            }
        }

        //public List<USP_LISTARBANCOS_Result> ListarBanco()
        //{
        //    using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
        //    {
        //        return db.USP_LISTARBANCOS().ToList();
        //    }
        //}


        public List<Bancos> ListarBanco()
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LISTADO_DE_BANCOS", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Bancos> ListarBanco = new List<Bancos>();
                        while (dr.Read())
                        {
                            Bancos bancos = new Bancos();
                            bancos.ID = Convert.ToInt32(dr["ID"]);
                            bancos.Nombre = Convert.ToString(dr["Nombre"] == DBNull.Value ? "" : dr["Nombre"]);
                            ListarBanco.Add(bancos);
                        }
                        return ListarBanco;
                    }
                }
            }
        }


        public List<USP_LISTARCTASBANCOS_Result> ListarCuentasBanco(int bancoid)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                return db.USP_LISTARCTASBANCOS(bancoid).ToList();
            }
        }
        
        public int Agregar(Banco banco)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var Obanco = db.Banco.Find(banco.ID);
                if (Obanco == null)
                {
                    db.Banco.Add(banco);
                    db.SaveChanges();
                    return 1;
                }
                else
                {
                    Obanco.ID = banco.ID;
                    Obanco.Nombre = banco.Nombre;
                    Obanco.NombreCorto = banco.NombreCorto;
                    Obanco.CodigoUbigeo = banco.CodigoUbigeo;
                    Obanco.Direccion = banco.Direccion;
                    Obanco.FechaCreacion = banco.FechaCreacion;
                    Obanco.RUC = banco.RUC;
                    Obanco.ProcesoAfiliacion = banco.ProcesoAfiliacion;
                    Obanco.UsuarioCreacion = banco.UsuarioCreacion;
                    Obanco.DatoAdj = banco.DatoAdj;
                    Obanco.NombreAdj = banco.NombreAdj;                                        
                    Obanco.Estado = true;
                    Obanco.FechaCreacion = DateTime.Now.Date;
                    Obanco.UsuarioCreacion = "";
                    db.SaveChanges();
                    return 2;
                }
            }
        }

        public int Anular(int idbanco)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                int res = 0;
                var Obanco = db.Banco.Find(idbanco);
                if (Obanco != null)
                {
                    Obanco.Estado = false;
                    db.SaveChanges();
                    res = 1;
                }
                else
                {
                    throw new Exception("No existe un banco con el id:" + idbanco);
                }
                return res;
            }
        }

        public int Activar(int idbanco)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                int res = 0;
                var Obanco = db.Banco.Find(idbanco);
                if (Obanco != null)
                {
                    Obanco.Estado = true;
                    db.SaveChanges();
                    res = 1;
                }
                else
                {
                    throw new Exception("No existe un banco con el id:" + Obanco);
                }
                return res;
            }
        }
        
        public byte[] ObtenerImagenLogo(int idbanco)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                int res = 0;
                var Obanco = db.Banco.Find(idbanco);
                if (Obanco != null)
                {
                    return Obanco.DatoAdj;
                }
               return null;      
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