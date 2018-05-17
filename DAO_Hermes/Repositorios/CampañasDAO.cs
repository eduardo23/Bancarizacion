using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO_Hermes.ViewModel;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;


namespace DAO_Hermes.Repositorios
{
    public class CampañasDAO : IDisposable
    {

        #region*******************CAMPAÑAS***********************
        public List<Campañas> ListarGestionarCampañas()
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LISTARGESTIONARCAMPAÑAS", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Campañas> ListarGestionarCampañas = new List<Campañas>();
                        while (dr.Read())
                        {
                            Campañas Campañas = new Campañas();
                            Campañas.ID = Convert.ToInt32(dr["ID"]);
                            Campañas.NombreCampaña = Convert.ToString(dr["Nombre"]);
                            //Campañas.NombreCampaña = Convert.ToString(dr["Nombre"] == DBNull.Value ? "" : dr["Nombre"]);
                            Campañas.InicioVigencia = Convert.ToDateTime(dr["InicioVigencia"]);
                            Campañas.FinVigencia = Convert.ToDateTime(dr["FinVigencia"]);
                            Campañas.Situacion = Convert.ToString(dr["Situacion"]);
                            Campañas.Estado = Convert.ToString(dr["Estado"]);
                            ListarGestionarCampañas.Add(Campañas);
                        }
                        return ListarGestionarCampañas;
                    }
                }
            }
        }


        public List<Campañas> ListarInstitucionesAperturarCampañas()
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("OBTENERINSTITUCIONESASOCIADAS", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Campañas> ListarAperturarCampañas = new List<Campañas>();
                        while (dr.Read())
                        {
                            Campañas Campañas = new Campañas();
                            Campañas.ID = Convert.ToInt32(dr["AsociacionId"]);
                            Campañas.NombreCampaña = Convert.ToString(dr["Filtro"]);
                            ListarAperturarCampañas.Add(Campañas);
                        }
                        return ListarAperturarCampañas;
                    }
                }
            }
        }





        public List<Campañas> ListarCampañas()
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LISTARCAMPAÑAS", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Campañas> ListarCampañas = new List<Campañas>();
                        while (dr.Read())
                        {
                            Campañas Campañas = new Campañas();
                            Campañas.ID = Convert.ToInt32(dr["ID"]);
                            Campañas.Nombre = Convert.ToString(dr["Nombre"] == DBNull.Value ? "" : dr["Nombre"]);
                            ListarCampañas.Add(Campañas);
                        }
                        return ListarCampañas;
                    }
                }
            }
        }

        public DataSet USP_OBTENERINSTITUCIONESASOCIADAS(Int32 TipoInstitucionEducativa)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("OBTENERINSTITUCIONESASOCIADAS", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TipoInstitucionEducativa", TipoInstitucionEducativa);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
        }

        public DataSet OBTENER_INST_ASOCIADASxCompañia(int idCompañia, int TipoInstitucionEducativaId)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("OBTENER_INST_ASOCIADASxCompañia", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDCAMPAÑA", idCompañia);
                    cmd.Parameters.AddWithValue("@TIPOINSTITUCIONEDUCATIVAID", TipoInstitucionEducativaId);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
        }

        public Int32 AsignarAsociadosCampaña(int idCompañia, bool estado, int idAsociacion, string username, bool activo)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_ASOCIACIAR_CAMPAÑA", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@COMPANIAID", idCompañia);
                    cmd.Parameters.AddWithValue("@ESTADO", estado);
                    cmd.Parameters.AddWithValue("@IDASOCIACION", idAsociacion);
                    cmd.Parameters.AddWithValue("@USERNAME", username);
                    cmd.Parameters.AddWithValue("@ACTIVO", activo);
                    cn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public void QuitarAsociaciondeCampaña(int idCompañia, int AsociacionId)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_QuitarAsocdeCAMPAÑA", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@COMPANIAID", idCompañia);
                    cmd.Parameters.AddWithValue("@AsociacionID", AsociacionId);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void LIMPIARASOCIACIONESCAMPAÑA(int idCompañia)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("LIMPIARASOCIACIONESCAMPAÑA", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@COMPANIAID", idCompañia);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RegistrarAsociacionCampaña(int idCompañia, bool estado, string username, bool activo, List<string> items)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    LIMPIARASOCIACIONESCAMPAÑA(idCompañia);

                    foreach (string item in items)
                    {
                        AsignarAsociadosCampaña(idCompañia, estado, Convert.ToInt32(item), username, activo);
                    }

                    trans.Complete();
                }
                catch (Exception ex)
                {
                    trans.Dispose();
                }
                finally { trans.Dispose(); }
            }
        }


        public List<Campañas> ConsultarCampañas(Campañas objeCampañas)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_CONSULTARCAMPAÑAS", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", objeCampañas._ID);
                    cmd.Parameters.AddWithValue("@TIPO", objeCampañas.TIPO);
                    cn.Open();
                   
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    { List<Campañas> ListarCampañas = new List<Campañas>();
                      
                        while (dr.Read())
                        {  Campañas Campaña = new Campañas();
                            Campaña._ID = Convert.ToInt16(dr["ID"]);
                            Campaña._Nombre = Convert.ToString(dr["Nombre"]);
                            Campaña._InicioVigencia = Convert.ToDateTime(dr["InicioVigencia"]);
                            Campaña._FinVigencia = Convert.ToDateTime(dr["FinVigencia"]);
                            Campaña._Estado = Convert.ToString(dr["Estado"]);
                            ListarCampañas.Add(Campaña);
                        }
                        return ListarCampañas;
                    }
                }
            }
        }

        public List<Campañas> ConsultarCampañasporNombre(string nombre)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LISTARCAMPAÑAS_NOMBRE", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NOMBRE", nombre);
                    cn.Open();
                    List<Campañas> ListarCampañas = new List<Campañas>();
                    using (SqlDataReader dr = cmd.ExecuteReader())                    {
                        
                        while (dr.Read())
                        {
                            Campañas Campaña = new Campañas();

                            Campaña.ID = Convert.ToInt32(dr["ID"]);
                            Campaña.NombreCampaña = Convert.ToString(dr["Nombre"]);
                            Campaña.InicioVigencia = Convert.ToDateTime(dr["InicioVigencia"]);
                            Campaña.FinVigencia = Convert.ToDateTime(dr["FinVigencia"]);
                            //Campaña.Situacion = Convert.ToString(dr["Situacion"]);
                            Campaña.Estado = Convert.ToString(dr["Estado"]);
                            ListarCampañas.Add(Campaña);
                        }
                        return ListarCampañas;
                    }
                }
            }
        }

        public bool ExisteCampañaNombre(string Nombre)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT  NOMBRE FROM operacion.Campania WHERE UPPER( RTRIM(NOMBRE )) = UPPER(RTRIM(@NOMBRE))", cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@NOMBRE", Nombre);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        return dr.HasRows;
                    }
                }
            }
          }

        public bool ExisteCampañaID(int ID)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT  NOMBRE FROM  operacion.Campania WHERE id=@id", cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", ID);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        return dr.HasRows;
                    }
                }
            }
        }
        public int EliminarCampañaID(int ID)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("Delete from operacion.Campania WHERE id=@id", cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", ID);
                    cn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int CerrarCampaña(int ID)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("update operacion.Campania set Estado=0 WHERE id=@id", cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", ID);
                    cn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int RegistrarCampaña(Campañas objeCampañas)
        {
            if (ExisteCampañaID(objeCampañas.ID)==false)
            {
              return  AgregarCampañas(objeCampañas);
            }
            else
            {
               return EditarCampañas(objeCampañas);
            }
        }
        public int AnularCampaña(int id)
        {
            if (ExisteCampañaID(id) == false)
            {
                throw new Exception("No existe la campaña seleccionada");
            }
            else
            {
                return EliminarCampañaID(id);
            }
        }




        public int AgregarCampañas(Campañas objeCampañas)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_CampañaInsert", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", objeCampañas.ID);
                    cmd.Parameters.AddWithValue("@Nombre", objeCampañas.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", objeCampañas.Descripcion);
                    cmd.Parameters.AddWithValue("@InicioVigencia", objeCampañas.InicioVigencia);
                    cmd.Parameters.AddWithValue("@FinVigencia", objeCampañas.FinVigencia);
                    cmd.Parameters.AddWithValue("@Estado", objeCampañas.Estado);
                    cmd.Parameters.AddWithValue("@UsuarioCreacion", objeCampañas.UsuarioCreacion);
                    cmd.Parameters.AddWithValue("@FechaCreacion", objeCampañas.FechaCreacion);
                    cmd.Parameters.AddWithValue("@UsuarioActualizacion", objeCampañas.UsuarioActualizacion);
                    cmd.Parameters.AddWithValue("@FechaActualizacion", objeCampañas.FechaActualizacion);
                    cn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int EditarCampañas(Campañas objeCampañas)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_CampañaUpdate", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", objeCampañas.ID);
                    cmd.Parameters.AddWithValue("@Nombre", objeCampañas.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", objeCampañas.Descripcion);
                    cmd.Parameters.AddWithValue("@InicioVigencia", objeCampañas.InicioVigencia);
                    cmd.Parameters.AddWithValue("@FinVigencia", objeCampañas.FinVigencia);
                    cmd.Parameters.AddWithValue("@Estado", objeCampañas.Estado);
                    cmd.Parameters.AddWithValue("@UsuarioCreacion", objeCampañas.UsuarioCreacion);
                    cmd.Parameters.AddWithValue("@FechaCreacion", objeCampañas.FechaCreacion);
                    cmd.Parameters.AddWithValue("@UsuarioActualizacion", objeCampañas.UsuarioActualizacion);
                    cmd.Parameters.AddWithValue("@FechaActualizacion", objeCampañas.FechaActualizacion);
                    cn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }


        public List<Campañas> ListarCierreInstituciones(Campañas objeCampañas)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_CierresEnInstituciones", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCampaña", objeCampañas.ID);
                    cn.Open();                  
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    { List<Campañas> ListarCierreDeInstituciones = new List<Campañas>();
                       
                        while (dr.Read())
                        { Campañas Campaña = new Campañas();
                            Campaña.NombreInstitucion = Convert.ToString(dr["INSTITUCIONEDUCATIVA"]);
                            Campaña.SEGURO = Convert.ToString(dr["SEGURO"]);
                            Campaña.CIASEGURO = Convert.ToString(dr["CIASEGURO"]);
                            Campaña.Bancos = Convert.ToString(dr["BANCOS"]);
                            Campaña.EstadoCierre = Convert.ToInt32(dr["Estado"]);
                            Campaña.CodAsociacion = Convert.ToString(dr["CodAsociacion"]);
                            Campaña.CodInstitucion = Convert.ToString(dr["CodInstitucion"]);
                            Campaña.productId = Convert.ToInt32(dr["productId"]);
                            Campaña.CodigoCampaña = Convert.ToInt32(dr["IdCampaña"]);
                            ListarCierreDeInstituciones.Add(Campaña);
                        }
                        return ListarCierreDeInstituciones;
                    }
                }
            }
        }



        public List<Campañas> ValidarCierreInstituciones(Campañas objeCampañas)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[Usp_Sel_ValidarCierreDeInstituciones]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CampaniaID", objeCampañas.Campaña_Id);
                    cmd.Parameters.AddWithValue("@AsociacionID", objeCampañas.Asociacion_ID);
                    cmd.Parameters.AddWithValue("@InstitucionEducativaID", objeCampañas.InstitucionEducativa_ID);
                    cmd.Parameters.AddWithValue("@ProductoID", objeCampañas.Producto_ID);

                    cn.Open();
                   
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Campañas> ListarCierreInstituciones = new List<Campañas>();
                       
                        while (dr.Read())
                        { Campañas Campaña = new Campañas();
                            Campaña.CierreDeInstitucion = Convert.ToInt32(dr["CierreDeInstitucion"]);
                            ListarCierreInstituciones.Add(Campaña);
                        }
                        return ListarCierreInstituciones;
                    }



                }
            }
        }

        public string getCierraCampana(int CampaniaID)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_CIERRA_CAMPANA", cn))
                {
                    cmd.Parameters.AddWithValue("@CampaniaID", CampaniaID);
                    cmd.Parameters.Add("@Cierra", SqlDbType.VarChar,2).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return cmd.Parameters["@Cierra"].Value.ToString();
                }
            }
        }

        public string getValAsocEnCampana(int InstitucionEducativaID, int ProductoID)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("VAL_ASOC_EN_CAMPANA", cn))
                {
                    cmd.Parameters.AddWithValue("@pInstitucionEducativaID", InstitucionEducativaID);
                    cmd.Parameters.AddWithValue("@pProductoID", ProductoID);
                    cmd.Parameters.Add("@pMENSAJE", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return cmd.Parameters["@pMENSAJE"].Value.ToString();
                }
            }
        }
        public DataSet getDatosdeCobranza(int CampaniaID, int CIASeguroID, int InstitucionEducativaID, int ProductID)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand(string.Format("SELECT * from FnGetCobranza({0}, {1}, {2}, {3})", CampaniaID, CIASeguroID, InstitucionEducativaID, ProductID), cn))
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

        //public String ValidarCierreInstituciones(Campañas objeCampañas)
        //{
        //    SqlDataReader rdr;
        //    using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
        //    {

        //        using (SqlCommand cmd = new SqlCommand("[dbo].[Usp_Sel_ValidarCierreDeInstituciones]", cn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@CampaniaID", objeCampañas.Campaña_Id);
        //            cmd.Parameters.AddWithValue("@AsociacionID", objeCampañas.Asociacion_ID);
        //            cmd.Parameters.AddWithValue("@InstitucionEducativaID", objeCampañas.InstitucionEducativa_ID);
        //            cmd.Parameters.AddWithValue("@ProductoID", objeCampañas.Producto_ID);

        //            cn.Open();
        //            string Resultado="0";
        //            try
        //            {
        //                using (
        //                   SqlDataReader dr = cmd.ExecuteReader())
        //                {
        //                    Campañas Campaña = new Campañas();
        //                    while (dr.Read())
        //                    {
        //                        Campaña.CierreDeInstitucion = Convert.ToString(dr["CierreDeInstitucion"]);
        //                        Resultado =Convert.ToString(Campaña.CierreDeInstitucion);
        //                    }
        //                }
        //            }
        //            catch (SqlException ex)
        //            {
        //                throw new Exception(ex.Message);
        //            }
        //            return Resultado;
        //        }

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
        // ~CampañasDAO() {
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

        #endregion
    }
}
