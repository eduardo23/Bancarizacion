using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAO_Hermes.Repositorios   
{
    public class CodigoDAO : IDisposable  
    {         
        
        BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities();
        public DataSet ListarCodigosAfiliados(int InstitucionEducativaID=0, int AsociacionID=0)
        {
            string cnx = "";
            
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LISTARCODIGOS_AFILIADOS", cn))
                {
                    cmd.Parameters.AddWithValue("@InstitucionEducativaID", InstitucionEducativaID);
                    cmd.Parameters.AddWithValue("@AsociacionID", AsociacionID);
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

        public int ObtenerTipoCargaCodigos(int id)
        {
            string cnx = "";

            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_OBTENER_TIPO_CARGA", cn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    return Convert.ToInt32( cmd.ExecuteScalar());
                }
            }
        }

        public int ObtenerCantidadCodigos(int Asociacionid)
        {
            string cnx = "";

            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_OBTENER_CANTIDAD_CODIGOS", cn))
                {
                    cmd.Parameters.AddWithValue("@IDASOCIACION", Asociacionid);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }



        public int EliminarCodigosGenerados(int inicio, int fin, int codigoid)
        {
            string cnx = "";

            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("usp_EliminaCodigosAutomaticos", cn))
                {
                    cmd.Parameters.AddWithValue("@INICIO", inicio);
                    cmd.Parameters.AddWithValue("@FIN", fin);
                    cmd.Parameters.AddWithValue("@codigoid", codigoid);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }



        public int? BuscarCodigoGenAsociacion(int idasociacion)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("select dbo.BuscarCodigoGenAsociacion(" + idasociacion +")", cn))
                {
                    //cmd.Parameters.AddWithValue("@idasociacion", idasociacion);               
                    cmd.CommandType = CommandType.Text;
                    cn.Open();
                    return (int?) cmd.ExecuteScalar();
                }
            }
        }

        public int ObtenerCodigoAsociacion(int idasociacion)
        {
            BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities();

            int codigo = 0;
            var qry = db.Codigo.Where(p => p.AsociacionID == idasociacion).Select(p => p.ID);
            if (qry.Count() >= 0)
            {
                codigo = db.Codigo.Where(p => p.AsociacionID == idasociacion).Select(p => p.ID).First();
            }
            else
            {
                codigo = 0;
            }
            return codigo;
        }
        
            public DataSet BuscarCodigosAfiliadoAsociados( int Asociacionid)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_BUSCAR_CODIGOS_AFILIADOS_ASOCIADO", cn))
                {                    
                    cmd.Parameters.AddWithValue("@asociacionID", Asociacionid);
                    //cmd.Parameters.AddWithValue("@ASOCIACIONID", IdAsociacion);
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

        public DataSet BuscarCodigosAfiliado(int PRODUCTOID, int INSTITUCIONEDUCATIVAID)
        {            
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_BUSCAR_CODIGOS_AFILIADOS", cn))
                {
                    cmd.Parameters.AddWithValue("@INSTITUCIONEDUCATIVAID", INSTITUCIONEDUCATIVAID);
                    cmd.Parameters.AddWithValue("@PRODUCTOID", PRODUCTOID);
                    //cmd.Parameters.AddWithValue("@ASOCIACIONID", IdAsociacion);
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

        public int AFILIAR_CODIGO_LIBRE_ACCIDENTES(CodigoDetalle codDetalle)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                CodigoDetalle codigo = db.CodigoDetalle.Where(p => p.ID == codDetalle.ID).Single();
                codigo.AfiliacionSeguroID = codDetalle.AfiliacionSeguroID;
                codigo.AfiliacionSeguroAlumnoID = codDetalle.AfiliacionSeguroAlumnoID;
                return db.SaveChanges();
            }
        }

        public int EliminarCodigosRangoAutomatico(int Inicio , int fin)
        {
            return 1;
        }


        public int AFILIAR_CODIGO_LIBRE_RENTA(List<CodigoDetalle> codDetalles)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                foreach (CodigoDetalle cod in codDetalles)
                {
                    CodigoDetalle codigo = db.CodigoDetalle.Where(p => p.ID == cod.ID).Single();
                    codigo.AfiliacionSeguroID = cod.AfiliacionSeguroID;
                    codigo.AfiliacionSeguroPadreID = cod.AfiliacionSeguroPadreID;
                }
                return db.SaveChanges();
            }
        }

        public string Obtener_CODIGO_LIBRE_ID(int CodigoLibreID)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var select = (from e in db.CodigoDetalle where e.ID == CodigoLibreID select e.Codigo);
                return select.ToString();
            }
        }

        public DataSet OBTENER_CODIGO_LIBRE(Int32 ASOCIACIONID)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_OBTENER_CODIGO_LIBRE", cn))
                {
                    cmd.Parameters.AddWithValue("@ASOCIACIONID", ASOCIACIONID);
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

        public DataSet OBTENER_CODIGO_LIBRE_RENTA(Int32 ASOCIACIONID)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_OBTENER_CODIGO_LIBRE_RENTA", cn))
                {
                    cmd.Parameters.AddWithValue("@ASOCIACIONID", ASOCIACIONID);
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

        public DataSet OBTENER_CODIGO_LIBRE_ONCO(Int32 ASOCIACIONID)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_OBTENER_CODIGO_LIBRE_ONCOLOGICO", cn))
                {
                    cmd.Parameters.AddWithValue("@ASOCIACIONID", ASOCIACIONID);
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

        public DataSet BuscarCodigoAfiliadoCantidad(int PRODUCTOID, int INSTITUCIONEDUCATIVAID)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_BUSCAR_CODIGOS_AFILIADOS", cn))
                {
                    cmd.Parameters.AddWithValue("@INSTITUCIONEDUCATIVAID", INSTITUCIONEDUCATIVAID);
                    cmd.Parameters.AddWithValue("@PRODUCTOID", PRODUCTOID);
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

        public DataSet ListarCodigosDetalles(int CodigoId, int productoId, string buscar = null)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LISTARCODIGOS_DET", cn))
                {
                    cmd.Parameters.AddWithValue("@id", CodigoId);
                    cmd.Parameters.AddWithValue("@productID", productoId);
                    cmd.Parameters.AddWithValue("@buscar", buscar);
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

        public DataSet ListarAsegurados()
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LISTAR_ASEGURADOS", cn))
                {
                    //cmd.Parameters.AddWithValue("@id", CodigoId);
                    //cmd.Parameters.AddWithValue("@productID", productoId);
                    //cmd.Parameters.AddWithValue("@buscar", buscar);
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
        public DataSet ListarAlumnosAsegurados(int InstitucionEducativaID, int ProductoID, int CIASeguroID, int AsociacionID, string Asegurado)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_OBT_ALUMNOS_ASEGURADOS", cn))
                {
                    cmd.Parameters.AddWithValue("@InstitucionEducativaID", InstitucionEducativaID);
                    cmd.Parameters.AddWithValue("@ProductoID", ProductoID);
                    cmd.Parameters.AddWithValue("@CIASeguroID", CIASeguroID);
                    cmd.Parameters.AddWithValue("@AsociacionID", AsociacionID);
                    cmd.Parameters.AddWithValue("@Asegurado", Asegurado);
                    
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

        public int GenerarCodigos(Codigo codigo,string usuario )
        {
            int res = 0;
            Codigo Ocodigo = null;
            var qry = db.Codigo.Where(p => p.ID == codigo.ID);
            if (qry.Count() > 0)
            {
                Ocodigo = db.Codigo.Where(p => p.ID == codigo.ID).Single();              
                Ocodigo.Cantidad = codigo.Cantidad;
                Ocodigo.Descripcion = codigo.Descripcion;
                Ocodigo.FechaActualizacion = DateTime.Now;
                Ocodigo.UsuarioActualizacion = usuario;
                db.SaveChanges();
                List<CodigoDetalle> detallles = new List<CodigoDetalle>();

                foreach (CodigoDetalle Coddetalle in codigo.CodigoDetalle)
                {
                    Coddetalle.CodigoID = Ocodigo.ID;
                    db.CodigoDetalle.Add(Coddetalle);
                }                
                    db.SaveChanges();
            }
            else
            {
                db.Codigo.Add(codigo);
                db.SaveChanges();
                res = codigo.ID;
                //foreach (CodigoDetalle Coddetalle in codigo.CodigoDetalle)
                //{
                //    Coddetalle.CodigoID = codigo.ID;
                //    db.CodigoDetalle.Add(Coddetalle);
                //}
                //res = db.SaveChanges();
            }            
            return res;
        }


        public OCodigo Upd_Cant_CodCargaArch(int InstitucionEducativaID, int TipoProductoId,
                                                                                           int CompañiaSeguroId, int AsociacionId, int cantidad, string Userid)
        {
            OCodigo dCodigo = new OCodigo();
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var codigo = db.Codigo.Where(p => p.AsociacionID == AsociacionId).Single();
                //var codigo = db.Codigo.Where(p => p.AsociacionID == AsociacionId && p.ProductoID == TipoProductoId
                //                                                                   && p.CIASeguroID == CompañiaSeguroId && p.InstitucionEducativaID == InstitucionEducativaID).Single();
                dCodigo.CantidadInicial = Convert.ToInt32(codigo.Cantidad);
                codigo.Cantidad = codigo.Cantidad + cantidad;
                codigo.FechaActualizacion = DateTime.Now.Date;
                codigo.UsuarioActualizacion = Userid;
                db.SaveChanges();
                dCodigo.Codigo = codigo.ID;
                dCodigo.Total = Convert.ToInt32(codigo.Cantidad);
                return dCodigo;
            }
        }
        public int AgregarCodigoDetalle(CodigoDetalle detalle)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                db.CodigoDetalle.Add(detalle);
                return db.SaveChanges();
            }
        }

        public void procesarCodigosPagadosScotiank(DataTable dt)
        {
            string mon = "";
            int cod = 0;
            string obs = "";
            double Imp = 0;

            int len = 0;
            string cad = "";
            string pardec = "";
            string parent = "";
            foreach (DataRow fila in dt.Rows)
            {
                if (fila["Tipo"].ToString() != "H" && fila["Tipo"].ToString() != "T")
                {
                    cad = fila["ImportePagado"].ToString();
                    len = cad.Length - 2;
                    pardec = cad.Substring(len, 2);
                    parent = cad.Substring(1, len - 1);

                    Imp = Convert.ToDouble(parent + "." + pardec);

                    Registrar_Pago_Codigo(
                    Convert.ToString(fila["CodDeudor"]),
                    Convert.ToInt32(fila["Bnkid"]),
                    (fila["Moneda"].ToString() == "0000" ? 1 : fila["Moneda"].ToString() == "0001" ? 2 : 0),
                    Convert.ToString(fila["NumOperacion"]),
                    Convert.ToString(fila["FechaPago"]),
                    Imp,
                    fila["Usr"].ToString(),
                    ref mon, ref cod, ref obs);

                    fila["Mon"] = mon;
                    fila["cod"] = cod;
                    fila["obs"] = obs;
                }
            }
        }

        public void procesarCodigosPagadosInterbank(DataTable dt)
        {
            string mon = "";
            int cod = 0;
            string obs = "";
            double Imp = 0;

            int len = 0;
            string cad = "";
            string pardec = "";
            string parent = "";
            foreach (DataRow fila in dt.Rows)
            {
                cad = fila["ImportePagado"].ToString();
                len = cad.Length - 2;
                pardec = cad.Substring(len, 2);
                parent = cad.Substring(1, len-1);

                Imp = Convert.ToDouble(parent+"."+pardec);

                Registrar_Pago_Codigo(
                Convert.ToString(fila["CodDeudor"]),
                Convert.ToInt32(fila["Bnkid"]),
                (fila["Moneda"].ToString() == "01" ? 1 : fila["Moneda"].ToString() == "10" ? 2 : 0),
                Convert.ToString(fila["NumOperacion"]),
                Convert.ToString(fila["FechaPago"]),
                Imp,
                fila["Usr"].ToString(),
                ref mon,
                ref cod,
                ref obs);

                fila["Mon"] = mon;
                fila["cod"] = cod;
                fila["obs"] = obs;
            }
        }


        public void procesarCodigosPagadosBBVA(DataTable dt)
        {
            string mon = "";
            int cod = 0;
            string obs = "";
            double Imp = 0;

            int len = 0;
            string cad = "";
            string pardec = "";
            string parent = "";

            foreach (DataRow fila in dt.Rows)
            {
                if (fila["Tipo"].ToString() == "02")
                {
                    cad = fila["ImportePagado"].ToString();
                    len = cad.Length - 2;
                    pardec = cad.Substring(len, 2);
                    parent = cad.Substring(1, len - 1);

                    Imp = Convert.ToDouble(parent + "." + pardec);

                    Registrar_Pago_Codigo(
                    Convert.ToString(fila["CodDeudor"]),
                     Convert.ToInt32(fila["Bnkid"]),
                     Convert.ToInt32(fila["TipoMoneda"]),
                     Convert.ToString(fila["NumOperacion"]),
                     Convert.ToString(fila["FechaPago"]),
                     Imp,
                     fila["Usr"].ToString(),
                     ref mon,
                     ref cod,
                     ref obs);

                    fila["Mon"] = mon;
                    fila["cod"] = cod;
                    fila["obs"] = obs;
                }
            }
        }


        public int Registrar_Pago_Codigo(string CODIGO, 
                                         int BANCOID, 
                                         int MONEDAID, 
                                         string numOperacion, 
                                         string FechaPago, 
                                         double MontoPago,
                                         string Usuario,
                                         ref string Mon,
                                         ref int Cod, 
                                         ref string Obs)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                SqlCommand cmd = new SqlCommand("USP_REGISTRO_PAGO_CODIGO", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                    
                SqlParameter Codigo = new SqlParameter("@CODIGO", SqlDbType.VarChar);
                Codigo.Value = CODIGO;
                Codigo.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(Codigo);

                SqlParameter BancoId = new SqlParameter("@BANCOID", SqlDbType.Int);
                BancoId.Value= BANCOID;
                BancoId.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(BancoId);

                SqlParameter MonedaId = new SqlParameter("@MONEDAID", SqlDbType.Int);
                MonedaId.Value = MONEDAID;
                MonedaId.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(MonedaId);

                SqlParameter NumOperacion = new SqlParameter("@numOperacion", SqlDbType.VarChar);
                NumOperacion.Value = numOperacion;
                NumOperacion.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(NumOperacion);

                SqlParameter FecPago = new SqlParameter("@FechaPago", SqlDbType.VarChar);
                FecPago.Value = FechaPago;
                FecPago.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(FecPago);

                SqlParameter MntPago = new SqlParameter("@MontoPago", SqlDbType.Decimal);
                MntPago.Value = MontoPago;
                MntPago.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(MntPago);

                SqlParameter Usr = new SqlParameter("@Usuario", SqlDbType.VarChar);
                Usr.Value = Usuario;
                Usr.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(Usr);

                SqlParameter retMon = new SqlParameter("@RetMon", SqlDbType.VarChar)
                {
                    Size = 5,
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(retMon);

                SqlParameter retCod = new SqlParameter("@RetCod", SqlDbType.Int) {
                    Size = 10,
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(retCod);

                SqlParameter retObs = new SqlParameter("@RetObs", SqlDbType.VarChar)
                {
                    Size=100,
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(retObs);

                cn.Open();
                cmd.ExecuteNonQuery();

                Mon= retMon.Value.ToString();
                Cod = Convert.ToInt32(retCod.Value);
                Obs = retObs.Value.ToString();
                return 1;                
            }
        }

        public DataTable CrearDataTableCodigosPagados()
        {
            DataTable dtDatos = new DataTable();
            dtDatos.Columns.Add(new DataColumn("Tipo", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("CodigoUsuario", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("NombreUsuario", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Prima", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Operacion", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("ProductoId", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("MonedaId", typeof(string)));
            return dtDatos;
        }

        public int AgregarCodigo(Codigo codigo)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                db.Codigo.Add(codigo);
                db.SaveChanges();
                return codigo.ID;
            }
        }


        public bool BuscarCodigo(int codigo)
        {
            
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var qry=db.Codigo.Where(p=>p.ID==codigo);
                if (qry.Count()>0)
                    {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


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
        // ~CodigoDAO() {
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
    }
}
public class OCodigo
{
    public int Codigo { get;set; }
    public int CantidadInicial { get; set; }
    public int Total { get; set; }
}

