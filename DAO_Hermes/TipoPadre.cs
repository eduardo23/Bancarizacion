//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAO_Hermes
{
    using System;
    using System.Collections.Generic;
    
    public partial class TipoPadre
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoPadre()
        {
            this.Padre = new HashSet<Padre>();
        }
    
        public int ID { get; set; }
        public string Nombre { get; set; }
        public Nullable<bool> Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        public Nullable<System.DateTime> FechaActualizacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Padre> Padre { get; set; }
    }
}
