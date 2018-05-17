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
    
    public partial class CIASeguroProducto
    {
        public int ID { get; set; }
        public int CIASeguroID { get; set; }
        public int ProductoID { get; set; }
        public Nullable<int> ConfiguracionArchivoID { get; set; }
        public string UsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        public Nullable<System.DateTime> FechaActualizacion { get; set; }
        public Nullable<bool> Activo { get; set; }
    
        public virtual CIASeguro CIASeguro { get; set; }
        public virtual ConfiguracionArchivo ConfiguracionArchivo { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
