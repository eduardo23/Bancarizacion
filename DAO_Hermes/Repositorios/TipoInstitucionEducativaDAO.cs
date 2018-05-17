
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
    public class TipoInstitucionEducativaDAO : IDisposable
    {
        
        BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities();

        public List<TipoInstitucionEducativa> Listar()
        {
            return db.TipoInstitucionEducativa.ToList();
        }

        //public List<USP_LISTARINSTITUCIONEDUCATIVA_Result> ListarInstitucionEducativa()
        //{
        //    return db.USP_LISTARINSTITUCIONEDUCATIVA().ToList();
        //}


        public List<Institucion_Educativa> ListarInstitucionEducativa()
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LISTARINSTITUCIONEDUCATIVA", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Institucion_Educativa> ListarInstitucion = new List<Institucion_Educativa>();
                        while (dr.Read())
                        {
                            Institucion_Educativa InstitucionEducativa = new Institucion_Educativa();
                            InstitucionEducativa.CodigoInstitucion = Convert.ToInt32(dr["ID"]);
                            InstitucionEducativa.NombreInstitucion = Convert.ToString(dr["NombreNatural"] == DBNull.Value ? "" : dr["NombreNatural"]);
                            InstitucionEducativa.Activo = Convert.ToInt32(dr["Activo"]);
                            ListarInstitucion.Add(InstitucionEducativa);
                        }
                        return ListarInstitucion;
                    }
                }
            }
        }




        public List<Institucion_Educativa> ListarCodigosDetallesPagos(Institucion_Educativa I_Educativa)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Sel_CodigoDetalle_Pagos", cn))
                {
                    string inidate="";
                    string findate="";
                    DateTime ini = Convert.ToDateTime(I_Educativa.FechaInicial);
                    DateTime fin = Convert.ToDateTime(I_Educativa.FechaFinal);


                    //if (ini != null)
                    //{
                    //    inidate = ini.ToString("yyyyMMdd");
                    //}
                    //if (fin == null)
                    //{
                    //    inidate = fin.ToString("yyyyMMdd");
                    //}
                    cmd.CommandTimeout = 4000;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InstitucionEducativaId", I_Educativa.cod_IEducativa);
                    cmd.Parameters.AddWithValue("@ProductoId", I_Educativa.Cod_ProductId);
                    cmd.Parameters.AddWithValue("@CIASeguroID", I_Educativa.Cod_CiaSeguro);
                    cmd.Parameters.AddWithValue("@IsPagado", I_Educativa.EstadoIsPagado);
                    cmd.Parameters.AddWithValue("@BancoID", I_Educativa.Cod_Banco);
                    cmd.Parameters.AddWithValue("@MonedaID", I_Educativa.Cod_Moneda);
                    cmd.Parameters.AddWithValue("@StartDate", I_Educativa.FechaInicial);
                    cmd.Parameters.AddWithValue("@EndDate", I_Educativa.FechaFinal);
                    cmd.Parameters.AddWithValue("@TextoBusqueda", I_Educativa.TextoBusqueda);
                    //cmd.Parameters.AddWithValue("@FiltrarFechaVigencia", I_Educativa.FiltrarFechaVigencia);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Institucion_Educativa> ListarDetallesPagos = new List<Institucion_Educativa>();
                        while (dr.Read())
                        {
                            Institucion_Educativa DetallesPagos = new Institucion_Educativa();
                            DetallesPagos.ID = Convert.ToInt32(dr["ID"]);
                            DetallesPagos.ProductoID = Convert.ToInt32(dr["ProductoID"] == DBNull.Value ? "" : dr["ProductoID"]);
                            DetallesPagos.Correlativo = Convert.ToInt32(dr["Correlativo"]);
                            DetallesPagos.Codigo = Convert.ToString(dr["Codigo"]);
                            DetallesPagos.Descripcion = Convert.ToString(dr["Descripcion"]);
                            DetallesPagos.AfiliacionSeguroAlumnoID = Convert.ToString(dr["AfiliacionSeguroAlumnoID"]);
                            DetallesPagos.AfiliacionSeguroPadreID = Convert.ToString(dr["AfiliacionSeguroPadreID"] == DBNull.Value ? "" : dr["AfiliacionSeguroPadreID"]);
                            DetallesPagos.TipoCarga = Convert.ToInt32(dr["TipoCarga"]);
                            DetallesPagos.Prima = Convert.ToDecimal(dr["Prima"]);
                            DetallesPagos.GastoCuracion = Convert.ToDecimal(dr["GastoCuracion"] == DBNull.Value ? 0 : dr["GastoCuracion"]);
                            DetallesPagos.InvalidezPermanenteTotal = Convert.ToDecimal(dr["InvalidezPermanenteTotal"] == DBNull.Value ? 0 : dr["InvalidezPermanenteTotal"]);
                            DetallesPagos.InvalidezPermanenteParcial = Convert.ToDecimal(dr["InvalidezPermanenteParcial"] == DBNull.Value ? 0 : dr["InvalidezPermanenteParcial"]);
                            DetallesPagos.MuerteAccidental = Convert.ToDecimal(dr["MuerteAccidental"]== DBNull.Value ? 0 : dr["MuerteAccidental"]);
                            DetallesPagos.GastosSepelio = Convert.ToDecimal(dr["GastosSepelio"]== DBNull.Value ? 0 : dr["GastosSepelio"]);
                            DetallesPagos.PensionMensual = Convert.ToDecimal(dr["PensionMensual"] == DBNull.Value ? null : dr["PensionMensual"]);
                            DetallesPagos.MesesPension = Convert.ToString(dr["MesesPension"] == DBNull.Value ? "" : dr["MesesPension"]);
                            DetallesPagos.AnniosPension = Convert.ToString(dr["AnniosPension"] == DBNull.Value ? "" : dr["AnniosPension"]);
                            DetallesPagos.InstitucionEducativaNombre = Convert.ToString(dr["InstitucionEducativaNombre"]);
                            DetallesPagos.InstitucionEducativaDireccion = Convert.ToString(dr["InstitucionEducativaDireccion"]);
                            DetallesPagos.InstitucionEducativaCodigo = Convert.ToString(dr["InstitucionEducativaCodigo"]);
                            DetallesPagos.MonedaSimbolo = Convert.ToString(dr["MonedaSimbolo"]);
                            DetallesPagos.AlumnoApellidoPaterno = Convert.ToString(dr["AlumnoApellidoPaterno"] == DBNull.Value ? "" : dr["AlumnoApellidoPaterno"]);
                            DetallesPagos.AlumnoApellidoMaterno = Convert.ToString(dr["AlumnoApellidoMaterno"]);
                            DetallesPagos.AlumnoNombre = Convert.ToString(dr["AlumnoNombre"]);
                            DetallesPagos.AlumnoFechaNacimiento = Convert.ToDateTime(dr["AlumnoFechaNacimiento"] == DBNull.Value ? null : dr["AlumnoFechaNacimiento"]);

                            DetallesPagos.AlumnoNumeroDocumento = Convert.ToString(dr["AlumnoNumeroDocumento"]);
                            DetallesPagos.AlumnoEstado = Convert.ToInt32(dr["AlumnoEstado"]== DBNull.Value ? 0 : dr["AlumnoEstado"]);
                            DetallesPagos.AlumnoSeccion = Convert.ToString(dr["AlumnoSeccion"]);
                            DetallesPagos.AlumnoTipoDocumento = Convert.ToString(dr["AlumnoTipoDocumento"]);
                            DetallesPagos.AlumnoTipoDocumentoDsc = Convert.ToString(dr["AlumnoTipoDocumentoDsc"]);
                            DetallesPagos.AlumnoGrado = Convert.ToString(dr["AlumnoGradoDsc"]);
                            DetallesPagos.AlumnoSexoDsc = Convert.ToString(dr["AlumnoSexoDsc"] == DBNull.Value ? 0 : dr["AlumnoSexoDsc"]);
                            DetallesPagos.PadreApellidoPaterno = Convert.ToString(dr["PadreApellidoPaterno"]);
                            DetallesPagos.PadreApellidoMaterno = Convert.ToString(dr["PadreApellidoMaterno"]);
                            DetallesPagos.PadreNombre = Convert.ToString(dr["PadreNombre"]);
                            DetallesPagos.PadreFechaNacimiento = Convert.ToDateTime(dr["PadreFechaNacimiento"] == DBNull.Value ? null : dr["PadreFechaNacimiento"]);

                            DetallesPagos.PadreNumeroDocumento = Convert.ToString(dr["PadreNumeroDocumento"] == DBNull.Value ? "" : dr["PadreNumeroDocumento"]);

                            DetallesPagos.PadreEstado = Convert.ToString(dr["PadreEstado"]);
                            DetallesPagos.PadreTipoDocumento = Convert.ToString(dr["PadreTipoDocumentoDsc"]);
                            DetallesPagos.PadreTipoPadreNombre = Convert.ToString(dr["PadreTipoPadreNombre"]);
                            DetallesPagos.PadreTipo = Convert.ToString(dr["PadreTipo"]);
                            DetallesPagos.FechaPago = Convert.ToDateTime(dr["FechaPago"] ==DBNull.Value ? null : dr["FechaPago"]);
                            DetallesPagos.OperacionBancaria = Convert.ToString(dr["OperacionBancaria"]);
                            DetallesPagos.BancoPagoID = Convert.ToInt32(dr["BancoPagoID"] ==DBNull.Value ? 0 : dr["BancoPagoID"]);
                            DetallesPagos.BancoPagoNombre = Convert.ToString(dr["BancoPagoNombre"]);
                            DetallesPagos.MonedaPagoID = Convert.ToInt32(dr["MonedaPagoID"] == DBNull.Value ? 0 : dr["MonedaPagoID"]);
                            DetallesPagos.MonedaPagoNombre = Convert.ToString(dr["MonedaPagoNombre"] == DBNull.Value ? "" : dr["MonedaPagoNombre"]);
                            DetallesPagos.MonedaPagoSimbolo = Convert.ToString(dr["MonedaPagoSimbolo"]);
                            DetallesPagos.IsPagado = Convert.ToInt32(dr["IsPagado"] == DBNull.Value ? 0 : dr["IsPagado"]);
                            DetallesPagos.IsPagadoDsc = Convert.ToString(dr["IsPagadoDsc"] == DBNull.Value ? "" : dr["IsPagadoDsc"]);
                            DetallesPagos.NroPoliza = Convert.ToString(dr["NroPoliza"] == DBNull.Value ? "" : dr["NroPoliza"]);
                            DetallesPagos.CodigoContratante = Convert.ToString(dr["CodigoContratante"]);
                            DetallesPagos.NombreContratante = Convert.ToString(dr["NombreContratante"] == DBNull.Value ? "" : dr["NombreContratante"]);
                            DetallesPagos.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]);
                            DetallesPagos.Situacion = Convert.ToString(dr["Situacion"]);
                            ListarDetallesPagos.Add(DetallesPagos);
                        }
                        return ListarDetallesPagos;
                    }
                }
            }
        }



        public List<Institucion_Educativa> ListarReporte_CodigosDetalles_Pagos(Institucion_Educativa I_Educativa)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[Usp_Sel_Reporte_CodigoDetalle_Pagos]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InstitucionEducativaId", I_Educativa.cod_IEducativa);
                    cmd.Parameters.AddWithValue("@ProductoId", I_Educativa.Cod_ProductId);
                    cmd.Parameters.AddWithValue("@CIASeguroID", I_Educativa.Cod_CiaSeguro);
                    cmd.Parameters.AddWithValue("@IsPagado", I_Educativa.EstadoIsPagado);
                    cmd.Parameters.AddWithValue("@BancoID", I_Educativa.Cod_Banco);
                    cmd.Parameters.AddWithValue("@MonedaID", I_Educativa.Cod_Moneda);
                    cmd.Parameters.AddWithValue("@StartDate", I_Educativa.FechaInicial);
                    cmd.Parameters.AddWithValue("@EndDate", I_Educativa.FechaFinal);
                    cmd.Parameters.AddWithValue("@TextoBusqueda", I_Educativa.TextoBusqueda);
                    cmd.Parameters.AddWithValue("@FiltrarFechaVigencia", I_Educativa.FiltrarFechaVigencia);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Institucion_Educativa> ListarReporteDetallesPagos = new List<Institucion_Educativa>();
                        while (dr.Read())
                        {
                            Institucion_Educativa DetallesPagos = new Institucion_Educativa();
                            DetallesPagos.InstitucionEducativaNombre = Convert.ToString(dr["InstitucionEducativaNombre"]);
                            DetallesPagos.InstitucionEducativaDireccion = Convert.ToString(dr["InstitucionEducativaDireccion"]);
                            DetallesPagos.AlumnoApellidoPaterno = Convert.ToString(dr["AlumnoApellidoPaterno"] == DBNull.Value ? "" : dr["AlumnoApellidoPaterno"]);
                            DetallesPagos.AlumnoApellidoMaterno = Convert.ToString(dr["AlumnoApellidoMaterno"]);
                            DetallesPagos.AlumnoNombre = Convert.ToString(dr["AlumnoNombre"]);
                            DetallesPagos.AlumnoFechaNacimiento = Convert.ToDateTime(dr["AlumnoFechaNacimiento"]);
                            DetallesPagos.AlumnoNumeroDocumento = Convert.ToString(dr["AlumnoNumeroDocumento"]);
                            DetallesPagos.AlumnoEstado = Convert.ToInt32(dr["AlumnoEstado"]);
                            DetallesPagos.AlumnoSeccion = Convert.ToString(dr["AlumnoSeccion"]);
                            DetallesPagos.AlumnoTipoDocumento = Convert.ToString(dr["AlumnoTipoDocumento"]);
                            DetallesPagos.AlumnoGrado = Convert.ToString(dr["AlumnoGrado"]);
                            DetallesPagos.AlumnoSexo = Convert.ToInt32(dr["AlumnoSexo"] == DBNull.Value ? "" : dr["AlumnoSexo"]);
                            DetallesPagos.FechaPago = Convert.ToDateTime(dr["FechaPago"]);
                            DetallesPagos.OperacionBancaria = Convert.ToString(dr["OperacionBancaria"]);
                            DetallesPagos.BancoPagoNombre = Convert.ToString(dr["BancoPagoNombre"]);
                            DetallesPagos.IsPagado = Convert.ToInt32(dr["IsPagado"]);
                            DetallesPagos.NroPoliza = Convert.ToString(dr["NroPoliza"] == DBNull.Value ? "" : dr["NroPoliza"]);
                            DetallesPagos.CodigoContratante = Convert.ToString(dr["CodigoContratante"]);
                            DetallesPagos.NombreContratante = Convert.ToString(dr["NombreContratante"] == DBNull.Value ? "" : dr["NombreContratante"]);
                            ListarReporteDetallesPagos.Add(DetallesPagos);
                        }
                        return ListarReporteDetallesPagos;
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
        // ~TipoInstitucionEducativaDAO() {
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
