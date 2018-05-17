using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
namespace DAO_Hermes.Repositorios
{
    public class ChequeDAO : IDisposable
    {
        private static List<Cheque> lstCheque;
        private static Cheque entidad;
        private static SqlDataReader reader;
        private static SqlConnection conexion;
        private static SqlCommand comando;
        private static ClientResponse clientResponse;
        public ChequeDAO()
        {
            lstCheque = new List<Cheque>();
            entidad = null;
            reader = null;
            clientResponse = new ClientResponse();
            clientResponse.Status = "OK";
        }
        BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities();
        public int Agregar(Cheque cheque)
        {
            db.Cheque.Add(cheque);
            return db.SaveChanges();
        }

        public int Eliminar(int Id)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                int res = 0;
                var oChq = db.Cheque.Find(Id);
                if (oChq != null)
                {
                    oChq.Activo = false;
                    db.SaveChanges();
                    res = 1;
                }
                else
                {
                    throw new Exception("No existe un Cheque con el id:" + Id);
                }
                return res;
            }
        }

        public ClientResponse listarReporte(int CampaniaID, int ProductoID, int InstitucionEducativaID, int paginaActual, int RegistroXPagina)
        {
            int recordCount = 0;          
            try
            {
                using (conexion = new SqlConnection(ConexionDAO.cnx))
                {
                    using (comando = new SqlCommand("usp_cheque_getLst", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@CampaniaID", CampaniaID);
                        comando.Parameters.AddWithValue("@ProductoID", ProductoID);
                        comando.Parameters.AddWithValue("@InstitucionEducativaID", InstitucionEducativaID);
                        comando.Parameters.AddWithValue("@vi_Pagina", paginaActual);
                        comando.Parameters.AddWithValue("@vi_RegistrosporPagina", RegistroXPagina);
                        comando.Parameters.Add("@vi_RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                        conexion.Open();
                        using (reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                entidad = new Cheque();
                                
                                entidad.ID = (reader["ID"] == DBNull.Value) ? 0 : Convert.ToInt32(reader["ID"].ToString());
                                entidad.CampaniaID = (reader["CampaniaID"] == DBNull.Value) ? 0 : Convert.ToInt32(reader["CampaniaID"]);

                                Campania oCamp = new Campania();
                                oCamp.Nombre = (reader["CampaniaNm"] == DBNull.Value) ? "" : reader["CampaniaNm"].ToString();
                                entidad.Campania = oCamp;
                                entidad.InstitucionEducativaID = (reader["InstitucionEducativaID"] == DBNull.Value) ? 0 : Convert.ToInt32(reader["InstitucionEducativaID"]);

                                InstitucionEducativa oInst = new InstitucionEducativa();
                                oInst.Nombre = (reader["InstitucionEducativaNm"] == DBNull.Value) ? "" : reader["InstitucionEducativaNm"].ToString();
                                entidad.InstitucionEducativa = oInst;

                                entidad.CIASeguroID = (reader["CIASeguroID"] == DBNull.Value) ? 0 : Convert.ToInt32(reader["CIASeguroID"]);

                                CIASeguro oCia = new CIASeguro();
                                oCia.Nombre = (reader["CIASeguroNm"] == DBNull.Value) ? "" : reader["CIASeguroNm"].ToString();
                                entidad.CIASeguro = oCia;

                                entidad.ProductoID = (reader["ProductoID"] == DBNull.Value) ? 0 : Convert.ToInt32(reader["ProductoID"]);

                                Producto oProd = new Producto();
                                oProd.Nombre = (reader["ProductoNm"] == DBNull.Value) ? "" : reader["ProductoNm"].ToString();
                                entidad.Producto = oProd;

                                entidad.BancoID = (reader["BancoID"] == DBNull.Value) ? 0 : Convert.ToInt32(reader["BancoID"]);

                                Banco oBan = new Banco();
                                oBan.Nombre = (reader["BancoNm"] == DBNull.Value) ? "" : reader["BancoNm"].ToString();
                                entidad.Banco = oBan;

                                entidad.MonedaID = (reader["MonedaID"] == DBNull.Value) ? 0 : Convert.ToInt32(reader["MonedaID"]);

                                Moneda oMon = new Moneda();
                                oMon.Nombre = (reader["MonedaNm"] == DBNull.Value) ? "" : reader["MonedaNm"].ToString();
                                entidad.Moneda = oMon;

                                entidad.Fecha= Convert.ToDateTime(reader["Fecha"]);
                                entidad.NroCheque= (reader["NroCheque"] == DBNull.Value) ? "" : reader["NroCheque"].ToString();
                                entidad.Monto = (reader["Monto"] == DBNull.Value) ? 0 : Convert.ToDecimal(reader["Monto"]);
                                entidad.Concepto = (reader["Concepto"] == DBNull.Value) ? "" : reader["Concepto"].ToString();

                                lstCheque.Add(entidad);
                            }
                        }
                        recordCount = Convert.ToInt32(comando.Parameters["@vi_RecordCount"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                clientResponse.Mensaje = ex.Message;
                clientResponse.Status = "ERROR";
            }
            finally
            {
                conexion.Close();
                conexion.Dispose();
                comando.Dispose();
                reader.Dispose();
            }
            Pagination responsepaginacion = new Pagination()
            {
                TotalItems = recordCount,
                TotalPages = (int)Math.Ceiling((double)recordCount / 10)
                //TotalPages = (int)Math.Ceiling((double)recordCount / objeto.ItemsPerPage)
            };
            clientResponse.DataJson = JsonConvert.SerializeObject(lstCheque).ToString();
            clientResponse.paginacion = JsonConvert.SerializeObject(responsepaginacion).ToString();
            return clientResponse;
        }

        public int Grabar(Cheque cheque)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                bool existe = (db.Cheque.Where(p => p.ID == cheque.ID).Count() > 0);
                if (existe == true)
                {
                    Cheque oCheque = db.Cheque.Where(p => p.ID == cheque.ID).FirstOrDefault();
                    oCheque.ID = cheque.ID;
                    oCheque.CampaniaID = cheque.CampaniaID;
                    oCheque.InstitucionEducativaID = cheque.InstitucionEducativaID;
                    oCheque.CIASeguroID = cheque.CIASeguroID;
                    oCheque.ProductoID = cheque.ProductoID;
                    oCheque.BancoID = cheque.BancoID;
                    oCheque.MonedaID = cheque.MonedaID;
                    oCheque.Fecha = cheque.Fecha;
                    oCheque.NroCheque = cheque.NroCheque;
                    oCheque.Monto = cheque.Monto;
                    oCheque.UsuarioActualizacion = cheque.UsuarioActualizacion;
                    oCheque.FechaActualizacion = cheque.FechaActualizacion;
                }
                return db.SaveChanges();
            }
        }

        public DataSet getLstCobranza(int CampaniaID, int CIASeguroID, int InstitucionEducativaID, int ProductID)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_COBRANZA_LISTAR", cn))
                {
                    cmd.Parameters.AddWithValue("@CampaniaID", CampaniaID);
                    cmd.Parameters.AddWithValue("@CIASeguroID", CIASeguroID);
                    cmd.Parameters.AddWithValue("@InstitucionEducativaID", InstitucionEducativaID);
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
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
        // ~ChequeDAO() {
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
