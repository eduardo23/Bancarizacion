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
    
    public partial class AfiliacionSeguroAlumno
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AfiliacionSeguroAlumno()
        {
            this.CodigoDetalle = new HashSet<CodigoDetalle>();
        }
    
        public int ID { get; set; }
        public Nullable<int> AfiliacionSeguroID { get; set; }
        public Nullable<int> AlumnoID { get; set; }
        public Nullable<System.DateTime> fechaCreacion { get; set; }
        public Nullable<int> idasociacion { get; set; }
    
        public virtual AfiliacionSeguro AfiliacionSeguro { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CodigoDetalle> CodigoDetalle { get; set; }
        public virtual Alumno Alumno { get; set; }
    }
}