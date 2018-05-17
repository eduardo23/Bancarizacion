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
    public class CiaSeguro_DAO : IDisposable
    {
        public CIASeguro Buscar(int id)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var ed = db.CIASeguro.Find(id);
                if (ed != null)
                {
                    return ed;
                }
                else
                {
                    throw new Exception("No existe el registro con el id:" + id);
                }
            }
        }

        public Int32 Anular(int id ,string estado)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var ed = db.CIASeguro.Find(id);
                if (ed != null)
                {
                    if (estado=="ACTIVO")
                    {
                        ed.Estado = false;
                    }
                    else
                    {
                        ed.Estado = true;
                    }                    
                    db.SaveChanges();
                    return 1;
                }
                else
                {
                    throw new Exception("No existe el registro con el id:" + id);
                }
            }
        }

        public int agregarSeguro(CIASeguro seguro)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var ed = db.CIASeguro.Find(seguro.ID);
                if (ed != null)
                {
                    return 0;
                }
                else
                {
                    db.CIASeguro.Add(seguro);
                    return db.SaveChanges();
                }
            }
        }

        //public List<USP_LISTARCIASEGURO_Result> ListarSeguros()
        //{
        //    using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
        //    {
        //        return db.USP_LISTARCIASEGURO().ToList();
        //    }
        //}



        public List<Cia_Seguro> ListarSeguros()
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LISTARCIASEGURO", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Cia_Seguro> ListarCiaSeguros = new List<Cia_Seguro>();
                        while (dr.Read())
                        {
                            Cia_Seguro CiaSeguro = new Cia_Seguro();
                            CiaSeguro.ID = Convert.ToInt32(dr["ID"]);
                            CiaSeguro.Nombre = Convert.ToString(dr["Nombre"] == DBNull.Value ? "" : dr["Nombre"]);
                            CiaSeguro.RUC = Convert.ToString(dr["RUC"]);
                            CiaSeguro.Direccion = Convert.ToString(dr["Direccion"]);
                            CiaSeguro.Estado = Convert.ToInt32(dr["Estado"]);
                            ListarCiaSeguros.Add(CiaSeguro);
                        }
                        return ListarCiaSeguros;
                    }
                }
            }
        }

        public List<Cia_Seguro> getLstbyInst(Int32 InstId)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("usp_CiaSeguro_getLstbyInst", cn))
                {
                    cmd.Parameters.AddWithValue("@InstId", InstId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Cia_Seguro> ListarCiaSeguros = new List<Cia_Seguro>();
                        while (dr.Read())
                        {
                            Cia_Seguro CiaSeguro = new Cia_Seguro();
                            CiaSeguro.ID = Convert.ToInt32(dr["ID"]);
                            CiaSeguro.Nombre = Convert.ToString(dr["Nombre"] == DBNull.Value ? "" : dr["Nombre"]);
                            ListarCiaSeguros.Add(CiaSeguro);
                        }
                        return ListarCiaSeguros;
                    }
                }
            }
        }
        public List<Cia_Seguro> getLstbyCamp(Int32 CampId)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("usp_CiaSeguro_getLstbyCamp", cn))
                {
                    cmd.Parameters.AddWithValue("@CampaniaID", CampId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Cia_Seguro> ListarCiaSeguros = new List<Cia_Seguro>();
                        while (dr.Read())
                        {
                            Cia_Seguro CiaSeguro = new Cia_Seguro();
                            CiaSeguro.ID = Convert.ToInt32(dr["ID"]==DBNull.Value ? 0 : dr["ID"]);
                            CiaSeguro.Nombre = Convert.ToString(dr["Nombre"] == DBNull.Value ? "" : dr["Nombre"]);
                            ListarCiaSeguros.Add(CiaSeguro);
                        }
                        return ListarCiaSeguros;
                    }
                }
            }
        }

        public List<Cia_Seguro> ListarMaxTotal_CiaSeguros(Cia_Seguro objeCiaSeguros)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("[Operacion].[Usp_Sel_MaxTotales_CIASeguro]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CampaniaID", objeCiaSeguros.CampaniaID);
                    cmd.Parameters.AddWithValue("@TipoCambio", objeCiaSeguros.TipoCambio);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Cia_Seguro> ListarCiaSeguros = new List<Cia_Seguro>();
                        while (dr.Read())
                        {
                            Cia_Seguro CiaSeguro = new Cia_Seguro();
                            CiaSeguro.NombreCiaSeguros = Convert.ToString(dr["Nombre"]);
                            CiaSeguro.SimboloSoles = Convert.ToString(dr["SimboloSoles"] == DBNull.Value ? "" : dr["SimboloSoles"]);
                            CiaSeguro.MontoSoles = Convert.ToDecimal(dr["MontoSoles"]);
                            CiaSeguro.SimboloDolares = Convert.ToString(dr["SimboloDolares"]);
                            CiaSeguro.MontoDolares = Convert.ToDecimal(dr["MontoDolares"]);
                            ListarCiaSeguros.Add(CiaSeguro);
                        }
                        return ListarCiaSeguros;
                    }
                }
            }
        }
        public List<USP_LISTATIPOSEGUROS_Result> ListarTipoSeguro(int id)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                return db.USP_LISTATIPOSEGUROS(id).ToList();
            }
        }

        public List<USP_LISTARBANCO_Result> ListarTipoCuentas(int idDetalle)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                return db.USP_LISTARBANCO(idDetalle).ToList();
            }
        }

        public byte[] ObtenerImagenLogo(int idCiaSeguro)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                int res = 0;
                var Ociaseguro = db.CIASeguro.Find(idCiaSeguro);
                if (Ociaseguro != null)
                {
                    return Ociaseguro.DatoAdj;
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
        // ~CiaSeguro_DAO() {
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
