using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAO_Hermes.Repositorios
{
    public class AsociacionDAO : IDisposable
    {
        public List<USP_OBTENER_PRODUCTOS_IE_Result> ListarProductosIE(int idIE)
        {
            
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {      
                return db.USP_OBTENER_PRODUCTOS_IE(idIE).ToList();
            }
        }

        public List<USP_LISTARASOCIACIONES2_Result> ListarAsociaciones()
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                List<USP_LISTARASOCIACIONES2_Result> obj= db.USP_LISTARASOCIACIONES2().ToList();
                return obj;
            }
        }
        public List<USP_LISTARASOCIACIONES_NOMBRE_Result> ListarAsociacionesNombre(string nombre)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                return db.USP_LISTARASOCIACIONES_NOMBRE(nombre).ToList();
            }
        }

        public int AsignarPoliza(int id, string numero, string codigo, string nombre)
        {
            
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                return db.USP_ASIGNAPOLIZA(id, numero, codigo, nombre);
            }
        }
        //kevin
        public bool ValidarAsociacion(int IEID, DateTime FVI, DateTime FVF, DateTime FVPI, DateTime FVPF, int CIAID, int ProductoID)
        {

            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var asoc = db.USP_VALIDASOC(IEID, FVI.Date, FVF.Date, FVPI.Date, FVPF.Date, CIAID, ProductoID).ToList();
                if (asoc.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        //finkevin
        public int Agregar(Asociacion asociacion)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                Asociacion oAsociacion = db.Asociacion.Find(asociacion.ID);
                if (oAsociacion == null)
                {
                 
                    
                        db.Asociacion.Add(asociacion);
                        db.SaveChanges();
                        return asociacion.ID;
                   

                }
                else
                {
                    oAsociacion.ID = asociacion.ID;
                    oAsociacion.CIASeguroID = asociacion.CIASeguroID;
                    oAsociacion.FilePlanSeguro = asociacion.FilePlanSeguro;
                    oAsociacion.GastoCuracion = asociacion.GastoCuracion;
                    oAsociacion.GastosSepelio = asociacion.GastosSepelio;
                    oAsociacion.FechaVigenciaInicio = asociacion.FechaVigenciaInicio;
                    oAsociacion.FechaVigenciaFin = asociacion.FechaVigenciaFin;
                    oAsociacion.FechaVigenciaPolizaInicio = asociacion.FechaVigenciaPolizaInicio;
                    oAsociacion.FechaVigenciaPolizaFin = asociacion.FechaVigenciaPolizaFin;
                    oAsociacion.InstitucionEducativaID = asociacion.InstitucionEducativaID;
                    oAsociacion.InvalidezPermanenteParcial = asociacion.InvalidezPermanenteParcial;
                    oAsociacion.InvalidezPermanenteTotal = asociacion.InvalidezPermanenteTotal;
                    oAsociacion.MesesPension = asociacion.MesesPension;
                    oAsociacion.MonedaID = asociacion.MonedaID;
                    oAsociacion.MuerteAccidental = asociacion.MuerteAccidental;
                    oAsociacion.NombreContratante = asociacion.NombreContratante;
                    oAsociacion.CodigoContratante = asociacion.CodigoContratante;
                    oAsociacion.NroPoliza = asociacion.NroPoliza;
                    oAsociacion.Prima = asociacion.Prima;
                    oAsociacion.ProductoID = asociacion.ProductoID;
                    oAsociacion.RecaudadorID = asociacion.RecaudadorID;
                    oAsociacion.TipoPrima = asociacion.TipoPrima;
                    oAsociacion.PensionMensual = asociacion.PensionMensual;
                    oAsociacion.Activo = asociacion.Activo;
                    oAsociacion.AnniosPension = asociacion.AnniosPension;
                    oAsociacion.UsuarioActualizacion = asociacion.UsuarioActualizacion ;
                    oAsociacion.FechaActualizacion = DateTime.Now.Date;
                    oAsociacion.FileNamePlanSeguro = asociacion.FileNamePlanSeguro;
                    oAsociacion.TipoCargaCodigos = asociacion.TipoCargaCodigos;
                    oAsociacion.Deducible = asociacion.Deducible;
                    oAsociacion.TipoAsociacion = asociacion.TipoAsociacion;
                    oAsociacion.Activo = true;
                    db.SaveChanges();
                    return oAsociacion.ID;
                }
            }
        }

        public bool AnularAsociacion(int id, bool estado)
        {            
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                Asociacion asociacion = db.Asociacion.Find(id);
                asociacion.Activo = estado;
                db.SaveChanges();
                return true;
            }
        }

        public DataSet ListarBancosIds(string ids)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {

                string Query = "SELECT id as Item,NombreCorto Nombre FROM MAESTRO.BANCO WHERE ID IN([ids])";
                Query = Query.Replace("[ids]", ids);
                using (SqlCommand cmd = new SqlCommand(Query, cn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
        }

        public string ObtenerBancosAsociacion(int idasociacion)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_ObtenerBancosAsociacion", cn))
                {
                    cmd.Parameters.AddWithValue("@AsociacionId", idasociacion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        System.Text.StringBuilder bancos = new StringBuilder();
                        while (dr.Read())
                        {
                            bancos.Append(dr.GetString(0) + " , ");
                        }
                        return bancos.ToString();
                    }
                }
            }
        }

        public int Limpiar_Cuentas_Asociacion(int idasociacion)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LIMPIAR_CUENTAS_ASOC", cn))
                {
                    cmd.Parameters.AddWithValue("@ASOCIACIONID", idasociacion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    return cmd.ExecuteNonQuery();
                }
          }
      }
         
        public string ObtenerBancosIdAsociacion(int idasociacion)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_ObtenerBancosidAsociacion", cn))
                {
                    cmd.Parameters.AddWithValue("@AsociacionId", idasociacion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        System.Text.StringBuilder bancos = new StringBuilder();
                        while (dr.Read())
                        {
                            bancos.Append(dr.GetInt32(0) + ",");
                        }
                        return bancos.ToString();
                    }
                }
            }
        }

        public byte[] ObtenerPlanAsociacion(int idasociacion)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_OBTENER_Plan_Asociacion", cn))
                {
                    cmd.Parameters.AddWithValue("@Id", idasociacion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        byte[] bancos = null;
                        while (dr.Read())
                        {
                            if ( System.DBNull.Value != dr["FilePlanSeguro"])
                            {
                                bancos = (byte[])(dr["FilePlanSeguro"]);
                            }
                        }
                        return bancos;
                    }
                }
            }
        }
               
             
        public void RegistrarAsociacionDetalle( AsociacionDetalle ad )
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {                
                using (BancoDAO dbBanco = new BancoDAO())
                {             
                        db.AsociacionDetalle.Add(ad);
                        db.SaveChanges();
                }            
            }
        }

        public void EliminarAsociacionDetalle(int idbanco, int idasociacion)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var cuentas = db.Cuenta.Where(p=>p.BancoID==idbanco);

                foreach( Cuenta cta in cuentas )
                {
                    var ad = db.AsociacionDetalle.Where(p => p.CuentaID == cta.ID && p.AsociacionID==idasociacion);
                    if (ad.Count()>0)
                    {
                        AsociacionDetalle adc = db.AsociacionDetalle.Where(p => p.CuentaID == cta.ID && p.AsociacionID == idasociacion).Single();
                        db.AsociacionDetalle.Remove(adc);                        
                    }
                }
                db.SaveChanges();
            }
        }



        //public decimal ObtenerPrimaAsociacion (int idAsociacion)
        //{
        //    using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
        //    {
        //        Asociacion asociacion = db.Asociacion.Where(p=>p.);
        //        return asociacion.Prima;                
        //    }
        //}

        //public int ObtenerPrimaAsociacion( )
        //{
        //    using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
        //    {
        //        Asociacion asociacion = db.Asociacion.Find(id);
        //        asociacion.Activo = estado;
        //        db.SaveChanges();
        //        return true;
        //    }
        //}

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
        // ~AsociacionDAO() {
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





