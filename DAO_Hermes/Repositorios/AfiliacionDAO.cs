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
 public   class AfiliacionDAO : IDisposable
    {
        public int AgregarAfiliacionSeguro(AfiliacionSeguro afiliacionSeguro)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                db.AfiliacionSeguro.Add(afiliacionSeguro);
                return db.SaveChanges();
            }
        }

        public List<Afiliacion> ListarAfiliacionPorUsuario(Afiliacion objeAfiliacion)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LISTARINSTITUCIONESPORUSUARIO", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@USUARIO", objeAfiliacion.UsuarioCreacion);
                    cn.Open();                   
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Afiliacion> ListarAfiliacion = new List<Afiliacion>();
                        while (dr.Read())
                        {   Afiliacion Afiliacion = new Afiliacion();
                            Afiliacion.CodigoAfiliacion = Convert.ToInt16(dr["Codigo"]);
                            Afiliacion.NombreAfiliacion = Convert.ToString(dr["Nombre"]);
                            Afiliacion.FechaCreacionAfiliacion = Convert.ToDateTime(dr["FechaCreacion"]);
                            ListarAfiliacion.Add(Afiliacion);
                        }
                            return ListarAfiliacion;
                    }
                }
            }
        }


        public List<Afiliacion> ListarAfiliacionInstitucionPorNombre(Afiliacion objeAfiliacion , String usuario)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LISTAR_AFILIACION_NOMBRE", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NOMBRE", objeAfiliacion.NombreInstitucion);
                    cmd.Parameters.AddWithValue("@USUARIO", usuario);
                    cn.Open();                  
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                       List<Afiliacion> ListarAfiliacion = new List<Afiliacion>();
                        while (dr.Read())
                        {   Afiliacion Afiliacion = new Afiliacion();
                            Afiliacion.CodigoAfiliacion = Convert.ToInt32(dr["Codigo"]);
                            Afiliacion.NombreAfiliacion = Convert.ToString(dr["NombreNatural"]);
                            Afiliacion.FechaCreacionAfiliacion = Convert.ToDateTime(dr["FechaCreacion"]);
                            Afiliacion.IdInstitucion = Convert.ToString(dr["ID"]);
                            ListarAfiliacion.Add(Afiliacion);
                        }
                        return ListarAfiliacion;
                    }
                }
            }
        }




        public List<Afiliacion> ListarPagosAfiliados(Afiliacion objeAfiliacion)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_OBTENER_PAGOS_AFILIADOS", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@CODIGOINSTITUCION", objeAfiliacion.CodigoInstitucion);
                    cmd.Parameters.AddWithValue("@USUARIO", objeAfiliacion.UsuarioAfiliado);
                    cmd.Parameters.AddWithValue("@ID", objeAfiliacion.IdInstitucion);
                    cmd.Parameters.AddWithValue("@FEC", objeAfiliacion.FechaCreacionAfiliacion.ToString("yyyyMMdd"));
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Afiliacion> ListarPagosAfiliados = new List<Afiliacion>();
                        while (dr.Read())
                        {
                            Afiliacion Afiliacion = new Afiliacion();
                         //   Afiliacion.Id = Convert.ToString(dr["Codigo"]);
                            Afiliacion.Asegurado = Convert.ToString(dr["Asegurado"]);
                            Afiliacion.Beneficiario = Convert.ToString(dr["Beneficiario"]);
                            Afiliacion.Prima = Convert.ToDecimal(dr["Prima"]);
                            Afiliacion.Seguro = Convert.ToString(dr["Seguro"]);
                            Afiliacion.SeguroCia = Convert.ToString(dr["CiaSeguros"]);
                            //Afiliacion.BeneficiarioId = Convert.ToInt32(dr["BeneficiarioId"]);
                            ////if (Afiliacion.ImageSeguro != null)
                            ////{
                            ////Afiliacion.ImageSeguro = (byte[])(dr["FilePlanSeguro"]);
                            ////}
                            ////else
                            ////{
                            Afiliacion.ImageSeguro = new byte[Convert.ToInt32(0)];
                            ////}

                            Afiliacion.MonedaId = Convert.ToInt32(dr["MonedaId"]);
                            Afiliacion.CodigoInstitucion = Convert.ToString(dr["CodInstitucion"]);
                            Afiliacion.Id = Convert.ToString(dr["ID"]);
                            Afiliacion.CodigoPago = Convert.ToString(dr["CodigoPago"]);
                            Afiliacion.ProductoId = Convert.ToString(dr["ProductoId"]);
                            Afiliacion.EstadoPago = Convert.ToString(dr["IsPagado"]);

                            //Afiliacion.FechaDePago = Convert.ToDateTime(dr["FechaPago"]);

                            if (dr["FechaPago"] != DBNull.Value)
                                Afiliacion.FechaDePago = Convert.ToDateTime(dr["FechaPago"]);


                            Afiliacion.NombreBanco = Convert.ToString(dr["NombreBanco"]);
                            Afiliacion.CodAsociacion = Convert.ToInt32(dr["CodAsociacion"]); ;

                            ListarPagosAfiliados.Add(Afiliacion);
                        }
                        return ListarPagosAfiliados;
                    }
                }
            }
        }
        //public DataTable ListarReportePagos_Afiliacion(Afiliacion objeAfiliacion)
        //{
        //    using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
        //    {
        //        SqlCommand cmd = new SqlCommand("usp_Reporte_ReciboAfiliados", cn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Codigo", objeAfiliacion.CodigoInstitucion);
        //        cmd.Parameters.AddWithValue("@UsuarioCreacion", objeAfiliacion.UsuarioCreacion);
        //        cmd.Parameters.AddWithValue("@CodigoPago", objeAfiliacion.CodigoPago);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        return dt;
        //    }
        //}

        public List<Afiliacion> ListarReportePagos_Afiliacion(Afiliacion objeAfiliacion)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Reporte_ReciboAfiliados", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@Codigo", objeAfiliacion.CodigoInstitucion);
                    cmd.Parameters.AddWithValue("@UsuarioCreacion", objeAfiliacion.UsuarioCreacion);
                    cmd.Parameters.AddWithValue("@CodigoPago", objeAfiliacion.CodigoPago);
                    //cmd.Parameters.AddWithValue("@codProducto", objeAfiliacion.ProductoId);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Afiliacion> ListarReporteAfiliacion = new List<Afiliacion>();
                        while (dr.Read())
                        {
                            Afiliacion Afiliacion = new Afiliacion();
                            Afiliacion.CodigoPago = Convert.ToString(dr["Codigo"]);
                            Afiliacion.Asegurado = Convert.ToString(dr["Asegurado"]);
                            Afiliacion.Beneficiario = Convert.ToString(dr["Beneficiario"]);
                            Afiliacion.Prima = Convert.ToDecimal(dr["Prima"]);
                            Afiliacion.Seguro = Convert.ToString(dr["Seguro"]);
                            Afiliacion.SeguroCia = Convert.ToString(dr["CiaSeguros"]);
                            Afiliacion.BeneficiarioId = Convert.ToString(dr["BeneficiarioID"]);
                            Afiliacion.MonedaId = Convert.ToInt16(dr["MonedaId"]);
                            Afiliacion.FechaVigenciaPolizaInicio = Convert.ToString(dr["FechaVigenciaPolizaInicio"]);
                            Afiliacion.FechaVigenciaPolizaFin = Convert.ToString(dr["FechaVigenciaPolizaFin"]);
                            Afiliacion.NombreInstitucion = Convert.ToString(dr["NombreNatural"]);
                            Afiliacion.CodigoInstitucion = Convert.ToString(dr["CodigoInstitucion"]);
                            Afiliacion.TipoSeguro = Convert.ToString(dr["TipoDeSeguro"]);
                            Afiliacion.FechaPago = Convert.ToString(dr["FechaVigenciaFin"]);
                            Afiliacion.TipoPadre = Convert.ToString(dr["TipoPadre"] == DBNull.Value ? "" : dr["TipoPadre"]);

                            ListarReporteAfiliacion.Add(Afiliacion);
                        }
                        return ListarReporteAfiliacion;
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
        // ~AfiliacionDAO() {
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
