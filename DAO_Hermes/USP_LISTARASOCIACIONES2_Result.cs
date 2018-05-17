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
    
    public partial class USP_LISTARASOCIACIONES2_Result
    {
        public string Codigo { get; set; }
        public string RazonSocial { get; set; }
        public string NombreNatural { get; set; }
        public string CIASEGURO { get; set; }
        public string TIPOSEGURO { get; set; }
        public decimal PRIMA { get; set; }
        public string RECAUDADOR { get; set; }
        public string BANCOS { get; set; }
        public Nullable<int> MonedaID { get; set; }
        public int InstitucionEducativaID { get; set; }
        public int RecaudadorID { get; set; }
        public int CIASeguroID { get; set; }
        public int ProductoID { get; set; }
        public Nullable<System.DateTime> FechaVigenciaInicio { get; set; }
        public Nullable<System.DateTime> FechaVigenciaFin { get; set; }
        public Nullable<System.DateTime> FechaVigenciaPolizaInicio { get; set; }
        public Nullable<System.DateTime> FechaVigenciaPolizaFin { get; set; }
        public Nullable<decimal> MuerteAccidental { get; set; }
        public Nullable<int> AnniosPension { get; set; }
        public string CodigoContratante { get; set; }
        public Nullable<decimal> GastoCuracion { get; set; }
        public Nullable<decimal> GastosSepelio { get; set; }
        public Nullable<decimal> InvalidezPermanenteParcial { get; set; }
        public string NombreContratante { get; set; }
        public Nullable<decimal> InvalidezPermanenteTotal { get; set; }
        public string FilePlanSeguro { get; set; }
        public int ID { get; set; }
        public string NroPoliza { get; set; }
        public string fileNamePlanSeguro { get; set; }
        public int Cantidad { get; set; }
        public Nullable<int> codGen { get; set; }
        public int CodigoGen { get; set; }
        public Nullable<int> TipoCargaCodigos { get; set; }
        public Nullable<int> MesesPension { get; set; }
        public Nullable<decimal> PensionMensual { get; set; }
        public Nullable<int> TipoAsociacion { get; set; }
        public Nullable<decimal> Deducible { get; set; }
        public Nullable<int> TipoInstitucionEducativaID { get; set; }
    }
}
