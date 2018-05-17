using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Hermes.Repositorios
{
   public class CuentaDAO:IDisposable
    {
        
        BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities();
        public int AgregarCuenta(Cuenta cuenta)
        {
            Cuenta cta = db.Cuenta.Find(cuenta.ID);
            if (cta==null)
            {
                db.Cuenta.Add(cuenta);
            }
            else
            {
                cta.BancoID = cuenta.BancoID;
                cta.CodigoCIASeguro = cuenta.CodigoCIASeguro;
                cta.FechaActualizacion = DateTime.Now;
                cta.Numero = cuenta.Numero;
                cta.Predeterminado = cuenta.Predeterminado;
                cta.TipoMonedaID = cuenta.TipoMonedaID;                
                cta.UsuarioCreacion = "";
            }

            return   db.SaveChanges();
        }

        public Cuenta BuscarCuenta(int id)
        {
            Cuenta  cuenta= db.Cuenta.Find(id);
            if (cuenta!=null)
            {
                return cuenta;
            }
            else
                {
                throw new Exception("No existe la cuenta especificada");
            }
        }

        public bool AnularCuenta(int id)
        {
            bool res = false;
            Cuenta cuenta = db.Cuenta.Find(id);
            if (cuenta != null)
            {
                cuenta.Estado = false;
                db.SaveChanges();
                res= true;
            }
            else
            {
                throw new Exception("No existe la cuenta especificada");                
            }
            return res;
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
        // ~CuentaDAO() {
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
