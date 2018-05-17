using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace DAO_Hermes.Repositorios
{
 public   class OnlineDAO
    {         
         BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities();
        public int EliminarRegistroOnline(int idregOnlineR)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("[USP_ELIMINAR_AFILIACION]", cn))
                {
                    cmd.Parameters.AddWithValue("@id", idregOnlineR);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    return cmd.ExecuteNonQuery();                
                }
            }
        }

        public int EliminarRegistroOnline2(int idregOnlineR)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("[USP_ELIMINAR_AFILIACION2]", cn))
                {
                    cmd.Parameters.AddWithValue("@id", idregOnlineR);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public int LimpiarRegistroOnline(string UsuarioCreacion)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LIMPIARAFILIACION", cn))
                {
                    //    cmd.Parameters.AddWithValue("@id", idregOnlineR);

                    cmd.Parameters.AddWithValue("@UsuarioCreacion", UsuarioCreacion);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public int AgregarOnlineAccidentes(Reg_Online regOnline)
        {
            db.Reg_Online.Add(regOnline);            
            db.SaveChanges();            
            return regOnline.Id;
        }

        public int AgregarOnline(Reg_Online regOnline, Reg_Online regOnlineR)
        {
            db.Reg_Online.Add(regOnline);
            db.Reg_Online.Add(regOnlineR);
            db.SaveChanges();
            regOnline.ReservadoId = regOnline.Id;
            regOnlineR.ReservadoId = regOnline.Id;
            db.SaveChanges();
            return regOnline.Id;
        }

        public int AgregarOnlineAsocia1(Reg_Online regOnline)
        {
            db.Reg_Online.Add(regOnline);
             db.SaveChanges();            
            return regOnline.Id;
        }

        public void EditarOnline(Reg_Online regOnline)
        {
            
            var buscado = db.Reg_Online.Where(p => p.Id==regOnline.ReservadoId).ToList();
            if (buscado != null)
            {
                foreach (var item in buscado)
                {
                    item.Bene_ApeMaterno = regOnline.Bene_ApeMaterno;
                    item.Bene_ApePaterno = regOnline.Bene_ApePaterno;
                    item.Bene_ApeNombres = regOnline.Bene_ApeNombres;
                    item.Bene_TipoDocumento = regOnline.Bene_TipoDocumento;
                    item.Bene_NumDocumento = regOnline.Bene_NumDocumento;
                    item.Bene_FechaNacimiento = regOnline.Bene_FechaNacimiento;
                    item.Bene_Grado = regOnline.Bene_Grado;                    
                    item.Bene_Seccion = regOnline.Bene_Seccion;
                    db.SaveChanges();
                }
                //buscado.beneSeccion = "c";           
                var buscadoPadre = db.Reg_Online.Where(p => p.UsuarioCreacion == regOnline.UsuarioCreacion).ToList();
                foreach (var item in buscadoPadre)
                {
                    if (item.Padre_ApePaterno != "RESERVADO")
                    {
                        item.Padre_ApePaterno = regOnline.Padre_ApePaterno;
                        item.Padre_ApeMaterno = regOnline.Padre_ApeMaterno;
                        item.Padre_Nombres = regOnline.Padre_Nombres;
                        item.Padre_TipoDocumento = regOnline.Padre_TipoDocumento;
                        item.Padre_NumDocumento = regOnline.Padre_NumDocumento;
                        item.Padre_FechaNacimiento = regOnline.Padre_FechaNacimiento;
                        db.SaveChanges();
                    }
                }
              
            }            
        }

        public void EditarOnlineRenta2(Reg_Online regOnline , string buscado)
        {
            //string padrebuscado = regOnline.Padre_ApePaterno.Trim() + regOnline.Padre_ApeMaterno.Trim() + regOnline.Padre_Nombres.Trim();

            var padre = db.Reg_Online.Where(p => (p.Padre_ApePaterno.Trim() + " " +p.Padre_ApeMaterno.Trim() +", "+ p.Padre_Nombres.Trim()).ToUpper() == buscado.ToUpper()).ToList();
            //var padre = db.Reg_Online.Where(p => p.Id == regOnline.Id).FirstOrDefault();

            var hijo = db.Reg_Online.Where(p => p.ReservadoId == regOnline.ReservadoId).ToList();

            if (hijo != null)
            {
                foreach (var item in hijo)
                {
                    item.Bene_ApeMaterno = regOnline.Bene_ApeMaterno;
                    item.Bene_ApePaterno = regOnline.Bene_ApePaterno;
                    item.Bene_ApeNombres = regOnline.Bene_ApeNombres;
                    item.Bene_TipoDocumento = regOnline.Bene_TipoDocumento;
                    item.Bene_NumDocumento = regOnline.Bene_NumDocumento;
                    item.Bene_FechaNacimiento = regOnline.Bene_FechaNacimiento;
                    item.Bene_Grado = regOnline.Bene_Grado;
                    item.Bene_Seccion = regOnline.Bene_Seccion;
                    db.SaveChanges();
                }
                //buscado.beneSeccion = "c";           
                var itemx = db.Reg_Online.FirstOrDefault();
                foreach (var item in padre)
                {
                    //    if (item.Padre_ApePaterno != "RESERVADO")
                    //    {
                    item.Padre_ApePaterno = regOnline.Padre_ApePaterno;
                    item.Padre_ApeMaterno = regOnline.Padre_ApeMaterno;
                    item.Padre_Nombres = regOnline.Padre_Nombres;
                    item.Padre_TipoDocumento = regOnline.Padre_TipoDocumento;
                    item.Padre_NumDocumento = regOnline.Padre_NumDocumento;
                    item.Padre_FechaNacimiento = regOnline.Padre_FechaNacimiento;
                        db.SaveChanges();
                    }
                }

            }
        

        public DataSet LISTAR_AFILIACION_ONLINE(int id)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_Obtener_AFILIACION", cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    //cmd.Parameters.AddWithValue("@Cant", Cantidad);
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

        

        public DataSet LISTAR_AFILIADOS_ACCIDENTES(string UsuarioCreacion)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("[USP_Listar_AFILIACION_ACCIDENTES]", cn))
                {
                    //cmd.Parameters.AddWithValue("@id", id);
                    //cmd.Parameters.AddWithValue("@Cant", Cantidad);

                    cmd.Parameters.AddWithValue("@UsuarioCreacion", UsuarioCreacion);
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

        public DataSet LISTAR_AFILIADO_ACCIDENTES(int id)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("[USP_Obtener_AFILIACION_ACCIDENTES]", cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    //cmd.Parameters.AddWithValue("@Cant", Cantidad);
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


        public DataSet Obtener_AFILIACION_ONCOLOGICO(int id)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("[USP_Obtener_AFILIACION_ONCOLOGICO]", cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
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

        public DataSet LISTAR_AFILIACION_ONLINE_ASOCIA2(string UsuarioCreacion)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LISTARAFILIACION_ASOCIA2", cn))
                {
                    //       cmd.Parameters.AddWithValue("@id", id);
                    //cmd.Parameters.AddWithValue("@Cant", Cantidad);

                    cmd.Parameters.AddWithValue("@UsuarioCreacion", UsuarioCreacion);
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


        //void CargarConfirmarAfiliacion(DataTable dt)
        //{
        //    OnlineDAO db = new OnlineDAO();
        //    DataSet ds = db.LISTAR_AFILIACION_ACCIDENTES();
        //    GrvConfirmar.DataSource = ds;
        //    GrvConfirmar.DataBind();
        //}

        public DataSet LISTAR_AFILIACION_ACCIDENTES(string UsuarioCreacion)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("[USP_LISTARAFILIACION_ACCIDENTES]", cn))
                {
                    //cmd.Parameters.AddWithValue("@Codigo", Codigo);
                    //cmd.Parameters.AddWithValue("@Cant", Cantidad);

                    cmd.Parameters.AddWithValue("@UsuarioCreacion", UsuarioCreacion);
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
    

       public DataSet LISTAR_AFILIACION_ONLINE(string UsuarioAfiliacion)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LISTARAFILIACION", cn))
                {
                    //cmd.Parameters.AddWithValue("@Codigo", Codigo);
                    //cmd.Parameters.AddWithValue("@Cant", Cantidad);

                    cmd.Parameters.AddWithValue("@UsuarioCreacion", UsuarioAfiliacion);
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

        public DataSet LISTAR_AFILIACION_RENTAS(String UsuarioCreacion)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand(string.Format("select a.*, b.Nombre as NombreTipoPadre from Reg_Online a left join Afiliacion.TipoPadre b on a.Padre_TipoPadre= b.ID where a.UsuarioCreacion='{0}'", UsuarioCreacion), cn))
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

        public DataSet LISTAR_AFILIACION_ONCOLOGICO(String UsuarioCreacion)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand(string.Format("SELECT * FROM Reg_Online where UsuarioCreacion='{0}'", UsuarioCreacion), cn))
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

    }
}
