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
    public class TipoProductoDAO:IDisposable
    {
        //public List<USP_TipoProductoSelectALL_Result> ListarTipoProductos()
        //{
        //    using (BDHermesBancarizacionEntities db=new BDHermesBancarizacionEntities())
        //    {
        //      return db.USP_TipoProductoSelectALL().ToList();
        //    }                
        //}

        public List<TipoProducto> getLstCntPagosByProdInst(int CampanaId, int InstitucionEducativaId, string inidate, string findate)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Institucion_getLstCntPagosByProdInst", cn))
                {

                    cmd.Parameters.AddWithValue("@CampanaId", CampanaId);
                    cmd.Parameters.AddWithValue("@InstitucionEducativaID", InstitucionEducativaId);
                    cmd.Parameters.AddWithValue("@StartDate", inidate);
                    cmd.Parameters.AddWithValue("@EndDate", findate);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<TipoProducto> ListarProductos = new List<TipoProducto>();
                        while (dr.Read())
                        {
                            TipoProducto Tipo_Producto = new TipoProducto();
                            Tipo_Producto.ID = Convert.ToInt32(dr["ProductoId"]);
                            Tipo_Producto.Nombre = Convert.ToString(dr["ProductoNm"] == DBNull.Value ? "" : dr["ProductoNm"]);
                            Tipo_Producto.CantidadPagos = Convert.ToInt32(dr["CantPagos"]);
                            ListarProductos.Add(Tipo_Producto);
                        }
                        return ListarProductos;
                    }
                }
            }
        }

        public List<TipoProducto> ListarTipoProductos()  
        {
            using (SqlConnection cn =   new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd =  new SqlCommand("USP_TipoProductoSelectALL", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<TipoProducto> ListarProductos = new List<TipoProducto>();
                        while (dr.Read())
                        {
                            TipoProducto Tipo_Producto = new TipoProducto();
                            Tipo_Producto.ID = Convert.ToInt32(dr["ID"]);
                            Tipo_Producto.Nombre = Convert.ToString(dr["Nombre"] == DBNull.Value ? "" : dr["Nombre"]);
                            Tipo_Producto.Descripcion = Convert.ToString(dr["Descripcion"]);
                            Tipo_Producto.Estado = Convert.ToInt32(dr["Estado"]);
                            ListarProductos.Add(Tipo_Producto);
                        }
                        return ListarProductos;
                    }
                }
            }
        }

        public DataSet ListarTipoProductosxInstitucion(int Institucionid)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_ListarSegurosxInstitucion", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@institucionId", Institucionid);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
        }

        public DataSet ListarTipoProductosxInstitucionAsociado(int Asociacionid)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_BUSCAR_CODIGOS_AFILIADOS_ASOCIADO", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@asociacioniD", Asociacionid);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
        }



        public List<TipoProducto> ListarMaxTotal_TipoProducto(TipoProducto objeProductos)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("[Operacion].[Usp_Sel_MaxTotales_Producto]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CampaniaID", objeProductos.CampaniaID);
                    cmd.Parameters.AddWithValue("@TipoCambio", objeProductos.TipoCambio);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<TipoProducto> ListarProductos = new List<TipoProducto>();
                        while (dr.Read())
                        {
                            TipoProducto Productos = new TipoProducto();
                            Productos.NombreProducto = Convert.ToString(dr["Nombre"]);
                            Productos.SimboloSoles = Convert.ToString(dr["SimboloSoles"] == DBNull.Value ? "" : dr["SimboloSoles"]);
                            Productos.MontoSoles = Convert.ToDecimal(dr["MontoSoles"]);
                            Productos.SimboloDolares = Convert.ToString(dr["SimboloDolares"]);
                            Productos.MontoDolares = Convert.ToDecimal(dr["MontoDolares"]);
                            ListarProductos.Add(Productos);
                        }
                        return ListarProductos;
                    }
                }
            }
        }

        public List<TipoProducto> Reporte_AccidenteNombre(TipoProducto objeTipoProducto)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("[Operacion].[Usp_Sel_Reporte_Cobranza_ProductoAccidentes]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TipoReporte", objeTipoProducto.TipoReporte);
                    cmd.Parameters.AddWithValue("@CampaniaID", objeTipoProducto.CampaniaCodigo);
                    cmd.Parameters.AddWithValue("@CIASeguroID", objeTipoProducto.CIASeguroID);
                    cmd.Parameters.AddWithValue("@FechaInicioCIASeguro", objeTipoProducto.FechaInicioCIASeguro);
                    cmd.Parameters.AddWithValue("@FechaFinCIASeguro", objeTipoProducto.FechaFinCIASeguro);
                    cmd.Parameters.AddWithValue("@InstitucionEducativaID", objeTipoProducto.InstitucionEducativaID);
                    cmd.Parameters.AddWithValue("@FechaInicioInstitucionEducativa", objeTipoProducto.FechaInicioInstitucionEducativa);
                    cmd.Parameters.AddWithValue("@FechaFinInstitucionEducativa", objeTipoProducto.FechaFinInstitucionEducativa);
                    cmd.Parameters.AddWithValue("@ProductoID", objeTipoProducto.ProductoID);
                    cmd.Parameters.AddWithValue("@FechaInicioProducto", objeTipoProducto.FechaInicioProducto);
                    cmd.Parameters.AddWithValue("@FechaFinProducto", objeTipoProducto.FechaFinProducto);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<TipoProducto> ListarAccidenteNombre = new List<TipoProducto>();
                        while (dr.Read())
                        {
                            TipoProducto TipoReporteCobranzas = new TipoProducto();
                            TipoReporteCobranzas.AccidentesNombre = Convert.ToString(dr["AccidentesNombre"]);
                            ListarAccidenteNombre.Add(TipoReporteCobranzas);
                        }
                        return ListarAccidenteNombre;
                    }
                }
            }
        }


        public List<TipoProducto> Reporte_Cobranza_ProductoAccidentes(TipoProducto objeTipoProducto)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("[Operacion].[Usp_Sel_Reporte_Cobranza_ProductoAccidentesData]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TipoReporte", objeTipoProducto.TipoReporte);
                    cmd.Parameters.AddWithValue("@CampaniaID", objeTipoProducto.CampaniaCodigo);
                    cmd.Parameters.AddWithValue("@CIASeguroID", objeTipoProducto.CIASeguroID);
                    cmd.Parameters.AddWithValue("@FechaInicioCIASeguro", objeTipoProducto.FechaInicioCIASeguro);
                    cmd.Parameters.AddWithValue("@FechaFinCIASeguro", objeTipoProducto.FechaFinCIASeguro);
                    cmd.Parameters.AddWithValue("@InstitucionEducativaID", objeTipoProducto.InstitucionEducativaID);
                    cmd.Parameters.AddWithValue("@FechaInicioInstitucionEducativa", objeTipoProducto.FechaInicioInstitucionEducativa);
                    cmd.Parameters.AddWithValue("@FechaFinInstitucionEducativa", objeTipoProducto.FechaFinInstitucionEducativa);
                    cmd.Parameters.AddWithValue("@ProductoID", objeTipoProducto.ProductoID);
                    cmd.Parameters.AddWithValue("@FechaInicioProducto", objeTipoProducto.FechaInicioProducto);
                    cmd.Parameters.AddWithValue("@FechaFinProducto", objeTipoProducto.FechaFinProducto);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<TipoProducto> ListarReporte_Cobranza = new List<TipoProducto>();
                        while (dr.Read())
                        {
                            TipoProducto TipoReporteCobranzas = new TipoProducto();
                            TipoReporteCobranzas.Nombre = Convert.ToString(dr["Nombre"]);
                            TipoReporteCobranzas.TotalRecaudadoSoles = Convert.ToDecimal(dr["TotalRecaudadoSoles"]);
                            TipoReporteCobranzas.TotalPagadoSoles = Convert.ToDecimal(dr["TotalPagadoSoles"]);
                            TipoReporteCobranzas.TotalPendienteSoles = Convert.ToDecimal(dr["TotalPendienteSoles"]);
                            TipoReporteCobranzas.TotalRecaudadoDolares = Convert.ToDecimal(dr["TotalRecaudadoDolares"]);
                            TipoReporteCobranzas.TotalPagadoDolares = Convert.ToDecimal(dr["TotalPagadoDolares"]);
                            TipoReporteCobranzas.TotalPendienteDolares = Convert.ToDecimal(dr["TotalPendienteDolares"]);
                            ListarReporte_Cobranza.Add(TipoReporteCobranzas);
                        }
                        return ListarReporte_Cobranza;
                    }
                }
            }
        }




        public List<TipoProducto> Reporte_RentaNombre(TipoProducto objeTipoProducto)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("[Operacion].[Usp_Sel_Reporte_Cobranza_ProductoRenta]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TipoReporte", objeTipoProducto.TipoReporte);
                    cmd.Parameters.AddWithValue("@CampaniaID", objeTipoProducto.CampaniaID);
                    cmd.Parameters.AddWithValue("@CIASeguroID", objeTipoProducto.CIASeguroID);
                    cmd.Parameters.AddWithValue("@FechaInicioCIASeguro", objeTipoProducto.FechaInicioCIASeguro);
                    cmd.Parameters.AddWithValue("@FechaFinCIASeguro", objeTipoProducto.FechaFinCIASeguro);
                    cmd.Parameters.AddWithValue("@InstitucionEducativaID", objeTipoProducto.InstitucionEducativaID);
                    cmd.Parameters.AddWithValue("@FechaInicioInstitucionEducativa", objeTipoProducto.FechaInicioInstitucionEducativa);
                    cmd.Parameters.AddWithValue("@FechaFinInstitucionEducativa", objeTipoProducto.FechaFinInstitucionEducativa);
                    cmd.Parameters.AddWithValue("@ProductoID", objeTipoProducto.ProductoID);
                    cmd.Parameters.AddWithValue("@FechaInicioProducto", objeTipoProducto.FechaInicioProducto);
                    cmd.Parameters.AddWithValue("@FechaFinProducto", objeTipoProducto.FechaFinProducto);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<TipoProducto> ListarRentaNombre = new List<TipoProducto>();
                        while (dr.Read())
                        {
                            TipoProducto TipoReporteCobranzas = new TipoProducto();
                            TipoReporteCobranzas.RentaNombre = Convert.ToString(dr["RentaNombre"]);
                            ListarRentaNombre.Add(TipoReporteCobranzas);
                        }
                        return ListarRentaNombre;
                    }
                }
            }
        }


        public List<TipoProducto> Reporte_Cobranza_ProductoRentas(TipoProducto objeTipoProducto)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("[Operacion].[Usp_Sel_Reporte_Cobranza_ProductoRentaData]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TipoReporte", objeTipoProducto.TipoReporte);
                    cmd.Parameters.AddWithValue("@CampaniaID", objeTipoProducto.CampaniaCodigo);
                    cmd.Parameters.AddWithValue("@CIASeguroID", objeTipoProducto.CIASeguroID);
                    cmd.Parameters.AddWithValue("@FechaInicioCIASeguro", objeTipoProducto.FechaInicioCIASeguro);
                    cmd.Parameters.AddWithValue("@FechaFinCIASeguro", objeTipoProducto.FechaFinCIASeguro);
                    cmd.Parameters.AddWithValue("@InstitucionEducativaID", objeTipoProducto.InstitucionEducativaID);
                    cmd.Parameters.AddWithValue("@FechaInicioInstitucionEducativa", objeTipoProducto.FechaInicioInstitucionEducativa);
                    cmd.Parameters.AddWithValue("@FechaFinInstitucionEducativa", objeTipoProducto.FechaFinInstitucionEducativa);
                    cmd.Parameters.AddWithValue("@ProductoID", objeTipoProducto.ProductoID);
                    cmd.Parameters.AddWithValue("@FechaInicioProducto", objeTipoProducto.FechaInicioProducto);
                    cmd.Parameters.AddWithValue("@FechaFinProducto", objeTipoProducto.FechaFinProducto);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<TipoProducto> ListarReporte_Cobranza = new List<TipoProducto>();
                        while (dr.Read())
                        {
                            TipoProducto TipoReporteCobranzas = new TipoProducto();
                            TipoReporteCobranzas.Nombre = Convert.ToString(dr["Nombre"]);
                            TipoReporteCobranzas.TotalRecaudadoSoles = Convert.ToDecimal(dr["TotalRecaudadoSoles"]);
                            TipoReporteCobranzas.TotalPagadoSoles = Convert.ToDecimal(dr["TotalPagadoSoles"]);
                            TipoReporteCobranzas.TotalPendienteSoles = Convert.ToDecimal(dr["TotalPendienteSoles"]);
                            TipoReporteCobranzas.TotalRecaudadoDolares = Convert.ToDecimal(dr["TotalRecaudadoDolares"]);
                            TipoReporteCobranzas.TotalPagadoDolares = Convert.ToDecimal(dr["TotalPagadoDolares"]);
                            TipoReporteCobranzas.TotalPendienteDolares = Convert.ToDecimal(dr["TotalPendienteDolares"]);
                            ListarReporte_Cobranza.Add(TipoReporteCobranzas);
                        }
                        return ListarReporte_Cobranza;
                    }
                }
            }
        }

        public int Agregar(Producto producto)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var Oproducto = db.Producto.Find(producto.ID);
                if (Oproducto == null)
                {
                    db.Producto.Add(producto);
                    db.SaveChanges();
                    return 1;
                }
                else
                {
                    Oproducto.ID = producto.ID;
                    Oproducto.Nombre = producto.Nombre;
                    Oproducto.Descripcion = producto.Descripcion;
                    Oproducto.Estado = true;
                    Oproducto.FechaCreacion = DateTime.Now.Date;
                    Oproducto.UsuarioCreacion = ""; 
                    db.SaveChanges();
                    return 2;
                }
            }
        }

        public int Anular(int idproducto)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                int res = 0;
                var Oproducto = db.Producto.Find(idproducto);
                if (Oproducto != null)
                {
                    Oproducto.Estado = false;
                    db.SaveChanges();
                    res = 1;
                }
                else
                {
                    throw new Exception("No existe un tipo de seguro con el id:" + idproducto);
                }
                return res;
            }
        }

        public int Activar(int idproducto)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                int res = 0;
                var Oproducto = db.Producto.Find(idproducto);
                if (Oproducto != null)
                {
                    Oproducto.Estado = true;
                    db.SaveChanges();
                    res = 1;
                }
                else
                {
                    throw new Exception("No existe un tipo de seguro con el id:" + idproducto);
                }
                return res;
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
