using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DAO_Hermes;

namespace DAO_Hermes.Repositorios
{
  public  class PadreDAO:IDisposable
    {        

        BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities();
        public int Agregar(Padre padre)
        {
             db.Padre.Add(padre);
             return db.SaveChanges();
        }

        public int Grabar(Padre padre) {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities()) {
                bool existe = (db.Padre.Where(p => p.ID == padre.ID).Count() > 0);
                if (existe == true)
                {
                    Padre oPadre = db.Padre.Where(p => p.ID == padre.ID).FirstOrDefault();
                    oPadre.ID = padre.ID;
                    oPadre.ApellidoPaterno = padre.ApellidoPaterno;
                    oPadre.ApellidoMaterno = padre.ApellidoMaterno;
                    oPadre.Nombre = padre.Nombre;
                    oPadre.TipoDocumentoID = padre.TipoDocumentoID;
                    oPadre.NumeroDocumento = padre.NumeroDocumento;
                    oPadre.FechaNacimiento = padre.FechaNacimiento;
                    oPadre.BeneficiarioID = padre.BeneficiarioID;
                    //oPadre.TipoPadreID = padre.TipoPadreID;
                    //oPadre.Estado = padre.Estado;
                    oPadre.UsuarioActualizacion = padre.UsuarioActualizacion;
                    oPadre.FechaActualizacion = padre.FechaActualizacion;                    
                }
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
        // ~PadreDAO() {
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
