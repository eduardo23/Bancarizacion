using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Hermes.ViewModel
{
   public class Institucion_Educativa
    {
        /*
         * ID	
 ProductoID	
 Correlativo	
 Codigo	
 Descripcion
 AfiliacionSeguroAlumnoID	
 AfiliacionSeguroPadreID	
 TipoCarga	
 Prima
 GastoCuracion	
 InvalidezPermanenteTotal	
 InvalidezPermanenteParcial	
 MuerteAccidental
 GastosSepelio	
 PensionMensual	
 MesesPension	
 AnniosPension	
 InstitucionEducativaNombre	
 InstitucionEducativaDireccion	
 InstitucionEducativaCodigo	
 MonedaSimbolo	
 AlumnoApellidoPaterno	
 AlumnoApellidoMaterno	
 AlumnoNombre	
 AlumnoFechaNacimiento	
 AlumnoNumeroDocumento	
 AlumnoEstado	
 AlumnoSeccion	
 AlumnoTipoDocumento	
 AlumnoGrado	
 AlumnoSexo	
 PadreApellidoPaterno	
 PadreApellidoMaterno	
 PadreNombre	
 PadreFechaNacimiento	
 PadreNumeroDocumento	
 PadreEstado	
 PadreTipoDocumento	
 PadreTipoPadreNombre	
 PadreTipo	
 FechaPago	

 OperacionBancaria	
 BancoPagoID	
 BancoPagoNombre	
 MonedaPagoID	
 MonedaPagoNombre	
 MonedaPagoSimbolo	
 IsPagado	
 NroPoliza	
 CodigoContratante	
 NombreContratante	
 FechaCreacion
         * */

        //Parametros*********************
        public int cod_IEducativa { get; set; }
        public int Cod_ProductId { get; set; }
        public int Cod_CiaSeguro { get; set; }
        public int EstadoIsPagado { get; set; }
        public int Cod_Banco { get; set; }
        public int Cod_Moneda { get; set; }
        public DateTime? FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
        public string TextoBusqueda { get; set; }
        public int FiltrarFechaVigencia { get; set; }




        public int ID { get; set; }
        public int ProductoID { get; set; }
        public int Correlativo { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string AfiliacionSeguroAlumnoID { get; set; }
        public string AfiliacionSeguroPadreID { get; set; }
        public int TipoCarga { get; set; }
        public decimal Prima { get; set; }
        public decimal GastoCuracion { get; set; }
        public decimal InvalidezPermanenteTotal { get; set; }
        public decimal InvalidezPermanenteParcial { get; set; }
        public decimal MuerteAccidental { get; set; }
        public decimal GastosSepelio { get; set; }
        public decimal PensionMensual { get; set; }
        public string MesesPension { get; set; }
        public string AnniosPension { get; set; }
        public string InstitucionEducativaNombre { get; set; }
        public string InstitucionEducativaDireccion { get; set; }
        public string InstitucionEducativaCodigo { get; set; }
        public string MonedaSimbolo { get; set; }
        public string AlumnoApellidoPaterno { get; set; }
        public string AlumnoApellidoMaterno { get; set; }
        public string AlumnoNombre { get; set; }
        public DateTime AlumnoFechaNacimiento { get; set; }
        public string AlumnoNumeroDocumento { get; set; }
        public int AlumnoEstado { get; set; }
        public string AlumnoSeccion { get; set; }
        public string AlumnoTipoDocumento { get; set; }
        public string AlumnoTipoDocumentoDsc { get; set; }
        public string AlumnoGrado { get; set; }
        public int AlumnoSexo { get; set; }
        public string AlumnoSexoDsc { get; set; }
        public string PadreApellidoPaterno { get; set; }
        public string PadreApellidoMaterno { get; set; }
        public string PadreNombre { get; set; }
        public DateTime? PadreFechaNacimiento { get; set; }
        public string PadreNumeroDocumento { get; set; }
        public string PadreEstado { get; set; }
        public string PadreTipoDocumento { get; set; }
        public string PadreTipoPadreNombre { get; set; }
        public string PadreTipo { get; set; }
        public DateTime? FechaPago { get; set; }
        public string OperacionBancaria { get; set; }
        public int BancoPagoID { get; set; }
        public string BancoPagoNombre { get; set; }
        public int MonedaPagoID { get; set; }
        public string MonedaPagoNombre { get; set; }
        public string MonedaPagoSimbolo { get; set; }
        public int IsPagado { get; set; }
        public string IsPagadoDsc { get; set; }
        public string NroPoliza { get; set; }
        public string CodigoContratante { get; set; }
        public string NombreContratante { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string Situacion { get; set; }

        #region******ListarInstituciones
        public int CodigoInstitucion { get; set; }
        public string NombreInstitucion { get; set; }
        public int Activo { get; set; }




        #endregion


        #region**************MaxTotales_InstitucionEducativa**********

        public int CampaniaID { get; set; }
        public Decimal TipoCambio { get; set; }

        public string Nombre { get; set; }
        public Decimal AseguradosSoles { get; set; }
        public string SimboloSoles { get; set; }
        public Decimal MontoSoles { get; set; }
        public Decimal AseguradosDolares { get; set; }
        public string SimboloDolares { get; set; }
        public Decimal MontoDolares { get; set; }

        public int TotalPagos { get; set; }








        #endregion
    }
}
