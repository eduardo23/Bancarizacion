using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Hermes.Repositorios
{
    public class UserSeguridad_DAO:IDisposable
    {
        public string Agregar(Users usuario)
        {
          
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                if (ExisteEmail(usuario.Email) == false)
                {                   

                    db.Users.Add(usuario);
                    db.SaveChanges();
                    return usuario.Id;                   
                    
                }
                else
                {

                    throw new Exception("El usuario: " + usuario.Email + ", Ya se encuentra registrado!");
                }
            }
        }

        public bool Existe(string idusuario)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                Users ousuario = db.Users.Find(idusuario);
                if (ousuario == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
        }
        public bool ExisteEmail(string emailuser)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var oemail = db.Users.Where(p => p.Email == emailuser);
                if (oemail.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public int AgregarRol(UserRoles userrol)
        {

            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {               
                    db.UserRoles.Add(userrol);
                    return db.SaveChanges();
               
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
        // ~UserSeguridad_DAO() {
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
