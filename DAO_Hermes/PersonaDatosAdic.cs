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
    
    public partial class PersonaDatosAdic
    {
        public int Id { get; set; }
        public int PersonaTipoId { get; set; }
        public int PersonaId { get; set; }
        public Nullable<int> EstCivilId { get; set; }
        public string NacPais { get; set; }
        public Nullable<int> DirPaisId { get; set; }
        public string DirDptCod { get; set; }
        public string DirPrvCod { get; set; }
        public string DirDisCod { get; set; }
        public string DirEnt { get; set; }
        public string TlfnoDom { get; set; }
        public string TlfnoTrb { get; set; }
        public string NroCel { get; set; }
        public string DirMail { get; set; }
        public Nullable<int> Sexo { get; set; }
        public string UsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        public Nullable<System.DateTime> FechaActualizacion { get; set; }
    }
}
