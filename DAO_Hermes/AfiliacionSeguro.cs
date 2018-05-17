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
    
    public partial class AfiliacionSeguro
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AfiliacionSeguro()
        {
            this.AfiliacionSeguroPadre = new HashSet<AfiliacionSeguroPadre>();
            this.AfiliacionSeguroAlumno = new HashSet<AfiliacionSeguroAlumno>();
            this.CodigoDetalle = new HashSet<CodigoDetalle>();
        }
    
        public int ID { get; set; }
        public Nullable<int> InstitucionEducativaID { get; set; }
        public string CodigoPago { get; set; }
        public Nullable<bool> Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        public Nullable<System.DateTime> FechaActualizacion { get; set; }
        public Nullable<int> asociaciacionId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AfiliacionSeguroPadre> AfiliacionSeguroPadre { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AfiliacionSeguroAlumno> AfiliacionSeguroAlumno { get; set; }
        public virtual InstitucionEducativa InstitucionEducativa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CodigoDetalle> CodigoDetalle { get; set; }
    }
}
