using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DAO_Hermes.ViewModel;

namespace DAO_Hermes.Repositorios
{
    public class InstitucionEducativaDAO:IDisposable
    {
        public DataSet ListarInstitucionesResumen(int InstitucionEducativaID)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LISTAR_IE_RES", cn))
                {
                    cmd.Parameters.AddWithValue("@TIPOIE", InstitucionEducativaID);
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

       
       

        public int AgregarInstitucionEducativa(InstitucionEducativa institutiva)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var ed = db.InstitucionEducativa.Find(institutiva.ID);               
                if (ed != null)
                {
                    db.USP_InstitucionEducativaUpdate(institutiva.ID,institutiva.TipoInstitucionEducativaID,institutiva.TipoDocumentoID, institutiva.Codigo, institutiva.Nombre,
                                                      institutiva.RazonSocial, institutiva.TipoEmpresa, institutiva.NumeroDocumento, institutiva.CodigoUbigeo,institutiva.Direccion,
                                                      institutiva.Telefono, institutiva.Fax,institutiva.ApellidoPaternno,institutiva.ApellidoMaterno, institutiva.NombreNatural,
                                                      institutiva.UsuarioCreacion, institutiva.FechaCreacion,null,null,true);
                    return db.SaveChanges();
                }
                else
                {
                    if (ExisteRZ(institutiva.RazonSocial) == false)
                    {

                        db.InstitucionEducativa.Add(institutiva);
                        return db.SaveChanges();

                    }
                    else
                    {

                        throw new Exception("La Institucion : " + institutiva.RazonSocial + ", Ya se encuentra registrada!");
                    }

                }
            }
        }

        public bool ExisteRZ(string razon)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var orz = db.InstitucionEducativa.Where(p => p.RazonSocial == razon);
                if (orz.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public List<InstitucionEducativa> Listar() {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                return db.InstitucionEducativa.Where(p => p.Activo==true).OrderBy(p => p.NombreNatural).ToList();
            }
        }
        public List<Institucion_Educativa> ListarMaxTotal_InstitucionEducativa(Institucion_Educativa objeIE)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("[Operacion].[Usp_Sel_MaxTotales_InstitucionEducativa]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CampaniaID", objeIE.CampaniaID);
                    cmd.Parameters.AddWithValue("@TipoCambio", objeIE.TipoCambio);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Institucion_Educativa> ListarIE = new List<Institucion_Educativa>();
                        while (dr.Read())
                        {
                            Institucion_Educativa IE = new Institucion_Educativa();
                            IE.Nombre = Convert.ToString(dr["Nombre"]);
                            IE.AseguradosSoles = Convert.ToDecimal(dr["AseguradosSoles"] == DBNull.Value ? "" : dr["AseguradosSoles"]);
                            IE.SimboloSoles = Convert.ToString(dr["SimboloSoles"]);
                            IE.MontoSoles = Convert.ToDecimal(dr["MontoSoles"]);
                            IE.AseguradosDolares = Convert.ToDecimal(dr["AseguradosDolares"]);
                            IE.SimboloDolares = Convert.ToString(dr["SimboloDolares"]);
                            IE.MontoDolares = Convert.ToDecimal(dr["MontoDolares"]);
                            ListarIE.Add(IE);
                        }
                        return ListarIE;
                    }
                }
            }
        }

        public List<InstitucionEducativa> getLst()
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Institucion_getLst", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<InstitucionEducativa> oLstInstitucion = new List<InstitucionEducativa>();
                        while (dr.Read())
                        {
                            InstitucionEducativa oInst = new InstitucionEducativa();
                            oInst.ID = Convert.ToInt32(dr["ID"] == DBNull.Value ? 0 : dr["ID"]);
                            oInst.Nombre = Convert.ToString(dr["NombreNatural"] == DBNull.Value ? "" : dr["NombreNatural"]);
                            oLstInstitucion.Add(oInst);
                        }
                        return oLstInstitucion;
                    }
                }
            }
        }
        public List<Institucion_Educativa> getLstByCampania(int CampanaId)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Institucion_getLstByCampania", cn))
                {
                    cmd.Parameters.AddWithValue("@CampanaId", CampanaId);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Institucion_Educativa> oLstInstitucion = new List<Institucion_Educativa>();
                        while (dr.Read())
                        {
                            Institucion_Educativa oInst = new Institucion_Educativa();
                            oInst.ID = Convert.ToInt32(dr["ID"]== DBNull.Value ? 0 : dr["ID"]);
                            oInst.Nombre= Convert.ToString(dr["Nombre"] == DBNull.Value ? "" : dr["Nombre"]);
                            oLstInstitucion.Add(oInst);
                        }
                        return oLstInstitucion;
                    }
                }
            }
        }

        public List<Institucion_Educativa> getLstByCampaniaProducto(int CampanaId, int ProductoId)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Institucion_getLstByCampaniaProducto", cn))
                {
                    cmd.Parameters.AddWithValue("@CampanaId", CampanaId);
                    cmd.Parameters.AddWithValue("@ProductoId", ProductoId);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Institucion_Educativa> oLstInstitucion = new List<Institucion_Educativa>();
                        while (dr.Read())
                        {
                            Institucion_Educativa oInst = new Institucion_Educativa();
                            oInst.ID = Convert.ToInt32(dr["ID"] == DBNull.Value ? 0 : dr["ID"]);
                            oInst.Nombre = Convert.ToString(dr["Nombre"] == DBNull.Value ? "" : dr["Nombre"]);
                            oLstInstitucion.Add(oInst);
                        }
                        return oLstInstitucion;
                    }
                }
            }
        }

        public List<Institucion_Educativa> getLstCntPagosByInstCamp(int CampanaId, string inidate, string findate)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Institucion_getLstCntPagosByInstCamp", cn))
                {
                    cmd.Parameters.AddWithValue("@CampanaId", CampanaId);
                    cmd.Parameters.AddWithValue("@StartDate", inidate);
                    cmd.Parameters.AddWithValue("@EndDate", findate);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Institucion_Educativa> oLstInstitucion = new List<Institucion_Educativa>();
                        while (dr.Read())
                        {
                            Institucion_Educativa oInst = new Institucion_Educativa();
                            oInst.ID = Convert.ToInt32(dr["ID"] == DBNull.Value ? 0 : dr["ID"]);
                            oInst.Codigo=Convert.ToString(dr["codigo"] == DBNull.Value ? "" : dr["codigo"]);
                            oInst.Nombre = Convert.ToString(dr["NombreNatural"] == DBNull.Value ? "" : dr["NombreNatural"]);
                            oInst.TotalPagos = Convert.ToInt32(dr["CantPagos"] == DBNull.Value ? 0 : dr["CantPagos"]);
                            oLstInstitucion.Add(oInst);
                        }
                        return oLstInstitucion;
                    }
                }
            }
        }

        public string ObtenerCodigoInstitucion(string codigo, out int idEDucativa)
        {
            string msg = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var id = (from tb in db.InstitucionEducativa where tb.Codigo==codigo select tb.ID);
                
                if (id.Count()>0)
                {
                    DateTime hoy = DateTime.Now.Date;
                    int idEdu = db.InstitucionEducativa.Where(p => p.Codigo == codigo).Select(p => p.ID).First();
                    idEDucativa = idEdu;
                    var query = (from tb in db.Asociacion where tb.FechaVigenciaInicio <= hoy && tb.FechaVigenciaFin >= hoy  && tb.InstitucionEducativaID == idEdu select tb.ID);
                    if (query.Count()>0)
                    {
                        msg = "Bienvenidos al Sistema de Generación de Códigos de Pagos del Instituto: " + ObtenerNombreIE(idEdu);
                    }
                    else
                    {
                        var query2 = (from tb in db.Asociacion where tb.InstitucionEducativaID == idEdu && (tb.ProductoID==3 || tb.ProductoID==7) select tb.ID);
                        if (query2.Count() > 0)
                        {
                            msg = "Bienvenidos al Sistema de Generación de Códigos de Pagos del Instituto: " + ObtenerNombreIE(idEdu);
                        }
                        else
                        {
                            throw new Exception("El(La) Colegio: " + ObtenerNombreIE(idEdu) + " ya no se encuentra disponible para afiliación");
                        }
                    }
                }
                else
                {
                    throw new Exception("No existe el codigo de afiliación especificado:" + codigo);
                }
            }

            return msg;
        }

        public string ObtenerNombreIE(int ID)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var NombreIE = (from tb in db.InstitucionEducativa where tb.ID == ID select tb.NombreNatural).First();
                return NombreIE;
            }
        }

        public int ObtenerIdIExCodigoAfiliacion(string codAfiliacion)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var IDIE = (from tb in db.InstitucionEducativa where tb.Codigo == codAfiliacion select tb.ID).First();
                return IDIE;
            }
        }


        public List<USP_OBTENER_PRODUCTOS_IE_Result> ListarProductosIE(int idIE)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                return db.USP_OBTENER_PRODUCTOS_IE(idIE).OrderBy(p=>p.IdProducto).ToList();
            }
        }

        public List<USP_LISTARINSTITUCIONES_Result> ListarInstituciones(int tipoInstitutiva)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                return db.USP_LISTARINSTITUCIONES(tipoInstitutiva).OrderBy(p=>p.Nombre).ToList();
            }
        }

        public InstitucionEducativa Buscar(int id)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var ed =db.InstitucionEducativa.Find(id);
                if (ed!=null)
                {
                    return ed;
                }
                else
                {
                    throw new Exception("No existe el registro con el id:"+ id);
                }
            }
        }

        public Int32 Anular(int id)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var ed = db.InstitucionEducativa.Find(id);
                if (ed != null)
                {
                    ed.Activo = false;
                    db.SaveChanges();
                    return 1;
                }
                else
                {
                    throw new Exception("No existe el registro con el id:" + id);
                }
            }
        }

        public Int32 Activar(int id)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var ed = db.InstitucionEducativa.Find(id);
                if (ed != null)
                {
                    ed.Activo = true;
                    db.SaveChanges();
                    return 1;
                }
                else
                {
                    throw new Exception("No existe el registro con el id:" + id);
                }
            }
        }

        public List<string> ListarInstitucionesNombre(string nombre)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                List<USP_LISTARINSTITUCIONES_NOMBRE_Result> coleccion = new List<USP_LISTARINSTITUCIONES_NOMBRE_Result>();
                coleccion = db.USP_LISTARINSTITUCIONES_NOMBRE(nombre).ToList();
                List<string> Listado = new List<string>();
                foreach (USP_LISTARINSTITUCIONES_NOMBRE_Result  instituto in  coleccion)
                {                    
                    Listado.Add(string.Format("{0}-{1}", instituto.NombreNatural, instituto.ID));
                }
                return Listado;
            }
        }

        public List<USP_LISTARINSTITUCIONESXNOMBRE_Result> ListarInstitucionesXNombre(string nombre, int tipo)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                return db.USP_LISTARINSTITUCIONESXNOMBRE (nombre,tipo).ToList();
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
        // ~InstitucionEducativaDAO() {
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
