using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO_Hermes.Repositorios;
using System.Data;
using System.Data.SqlClient;
using DAO_Hermes.ViewModel;

namespace DAO_Hermes.Repositorios
{
    public class Usuario_DAO:IDisposable
    {
        //public List<string> ListarUsuariosNombre(string nombre)
        //{
        //    using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
        //    {
        //        List<Users> coleccion = new List<Users>();
        //        coleccion = db.Users.Contains(nombre);
        //        List<string> Listado = new List<string>();
        //        foreach (USP_LISTARINSTITUCIONES_NOMBRE_Result4 instituto in coleccion)
        //        {
        //            Listado.Add(string.Format("{0}-{1}", instituto.Nombre, instituto.ID));
        //        }
        //        return Listado;
        //    }
        //}

        public int Agregar( Padre padre)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var usud = db.Users.Find(padre.ID);
                if(usud !=null)
                {
                    return 0;
                }
                else
                {
                    db.Padre.Add(padre);
                    return db.SaveChanges();
                }
            }
        }

        public int AgregarUser(Users usu)
        {            
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var usud = db.Users.Find(usu.Id);
                if (usud != null)
                {
                    return 0;
                }
                else
                {
                    db.Users.Add(usu);
                    return db.SaveChanges();
                }
            }
        }

        public int EditarUser(Users usu, string newRole)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                int res = 0;
                var usud = db.Users.Find(usu.Id);
                if (usud != null)
                {
                    usud.ApellidoMaterno = usu.ApellidoMaterno;
                    usud.ApellidoPaterno = usu.ApellidoPaterno;
                    usud.Nombre = usu.Nombre;
                    usud.Email = usu.Email;
                    usud.TipoDocumento= usu.TipoDocumento;
                    usud.NumeroDocumento = usu.NumeroDocumento;
                    db.SaveChanges();
                    ActualizarRolUsuarioId(usu.Id,newRole);
                    return 1;
                }
                return res;
            }
        }


        public bool Existe(int idpadre)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                Padre opadre=db.Padre.Find(idpadre);
                if (opadre ==null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public string BuscarUsuarioId(string userid)
        {
            string email = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var user =db.Users.Find(userid);
                if (user!=null)
                {
                    email= user.Email;
                }
                return email;
            }
        }

        public Users BuscarUsuarioxId(string userid)
        {

            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var user = db.Users.Find(userid);
                if (user != null)
                {
                    return user;
                }
                else
                {
                    throw new Exception("No existe el usuario especificado");
                }
            }
        }


        public Users BuscarUsuarioPorEmail(string email)
        {            
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                Users user = db.Users.Where(p=>p.Email==email).First();
                return user;                               
            }
        }
        //kevin
        public string BuscarUsuarioPorEmailRest(string email)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                
                try
                {
                    var users = db.Users.Where(p => p.Email == email).First();
                    if (users != null)
                    {
                        return users.Password;
                    }
                }   
                catch(Exception ex)
                {
                new Exception("No existe registrado el correo electrónico: " + email);
                }
                return "";
            }
        }
        //fin kevin
        public bool ValidarUsuarioId(string user , string password)
        {            
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var Qry = db.Users.Where(p => p.UserName == user && p.PasswordHash == password);
                if (Qry.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public int ActualizarRolUsuarioId(string id , String rolesID)
        {
            int res = 0;
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                Users usuario = db.Users.Find(id);
                if (usuario!=null)
                {
                        UserRoles rol = db.UserRoles.Where(p=>p.UserId==id).Single();
                        rol.RoleId = rolesID;
                        res= db.SaveChanges();
                }
            }
            return res;
        }

        public string ObtenerRolUsuarioId(string id, String rolesID)
        {
            string  res = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {                
                   var rol = db.UserRoles.Where(p => p.UserId == id).Single();
                    res = rol.RoleId;                
            }
            return res;
        }


        public List<LoguearUsuario> Validar_Usuario(LoguearUsuario usuario)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("LoguearUsuario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);

                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<LoguearUsuario> Logueo = new List<LoguearUsuario>();
                        while (dr.Read())
                        {
                            LoguearUsuario _usuario = new LoguearUsuario();
                            _usuario.IdUsuario = Convert.ToString(dr["ID"]);
                            _usuario.Tipo = Convert.ToString(dr["Name"] == DBNull.Value ? "" : dr["Name"]);
                            _usuario.NombreUsuario = Convert.ToString(dr["UserName"]);
                            Logueo.Add(_usuario);
                        }
                        return Logueo;
                    }
                }
            }
        }
            
        public List<USP_LISTARUSERS_Result> ListarUsuarios()
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                return db.USP_LISTARUSERS().ToList();
            }
        }

        public System.Data.DataSet LISTARUSERS_X_NOMBRE(string nombre)
        {
            string cnx = "";
            
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LISTARUSERSXNOMBRE", cn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);            
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

       
        public Users Buscar(int id)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var ed = db.Users.Find(id);
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

        public List<Roles> CargarRoles()
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                return db.Roles.ToList();
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
        // ~Usuario_DAO() {
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
