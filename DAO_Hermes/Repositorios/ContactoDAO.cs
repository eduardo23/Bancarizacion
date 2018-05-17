using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Hermes.Repositorios
{
    public class ContactoDAO : IDisposable
    {
        public void Activar(int id)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var Ocontacto = db.Contacto.Find(id);
                if (Ocontacto != null)
                {
                    Ocontacto.Estado = true;
                    db.SaveChanges();
                }
            }
        }
        public int Agregar(Contacto contacto)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var Ocontacto = db.Contacto.Find(contacto.ID);
                if (Ocontacto == null)
                {
                    db.Contacto.Add(contacto);
                    db.SaveChanges();
                    return 1;
                }

                else
                {
                    Ocontacto.ApellidoPaterno = contacto.ApellidoPaterno;
                    Ocontacto.ApellidoMaterno = contacto.ApellidoMaterno;
                    Ocontacto.Nombre = contacto.Nombre;
                    Ocontacto.Email = contacto.Email;
                    Ocontacto.Cargo = contacto.Cargo;
                    db.SaveChanges();
                    return 2;
                }
            }
        }

        public int Anular(int idcontacto)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                int res = 0;
                var Ocontacto = db.Contacto.Find(idcontacto);
                if (Ocontacto != null)
                {
                    Ocontacto.Estado = false;
                    db.SaveChanges();
                   
                    res= 1;
                }
                else
                    {
                    throw new Exception("No existe el contacto con el id:" +idcontacto);
                    }
                    return res;
          }
       }
              
        public List<USP_LISTARCONTACTOS_INST_Result> ListarContactosxInstitucion(int institucionId)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                return db.USP_LISTARCONTACTOS_INST(institucionId).ToList();
            }
        }

        public List<USP_LISTARCONTACTOS_BANCO_Result> ListarContactosxBanco(int BancoId) 
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                return db.USP_LISTARCONTACTOS_BANCO (BancoId).ToList();
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
        // ~ContactoDAO() {
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

        //public int Agregar(Contacto contacto)
        //{
        //    using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
        //    {
        //        return db.Contacto.Add(contacto);
        //    }
        //}

    }
}