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
    
    public partial class USP_OBTENER_PAGOS_AFILIADOS_Result
    {
        public string Asegurado { get; set; }
        public string Beneficiario { get; set; }
        public decimal Prima { get; set; }
        public string Seguro { get; set; }
        public string CiaSeguros { get; set; }
        public byte[] FilePlanSeguro { get; set; }
        public Nullable<int> MonedaID { get; set; }
        public string CodInstitucion { get; set; }
        public int ID { get; set; }
        public string CodigoPago { get; set; }
        public int ProductoID { get; set; }
        public Nullable<bool> IsPagado { get; set; }
        public Nullable<System.DateTime> FechaPago { get; set; }
        public string NombreBanco { get; set; }
    }
}
