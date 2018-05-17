using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Hermes.ViewModel
{
 public   class Afiliacion
    {
        public string UsuarioCreacion { get; set; }

        public string NombreInstitucion { get; set; }
        public int CodigoAfiliacion { get; set; }
        public string NombreAfiliacion { get; set; }
        public DateTime FechaCreacionAfiliacion { get; set; }


        #region****Obtener Pagos Afiliados******************
        public string CodigoInstitucion { get; set; }
        public string UsuarioAfiliado { get; set; }

        public string IdInstitucion { get; set; }

        public string Id { get; set; }
        public string Asegurado { get; set; }
        public string Beneficiario { get; set; }
        public Decimal Prima { get; set; }
        public string Seguro { get; set; }
        public string SeguroCia { get; set; }
        public string BeneficiarioId { get; set; }
        public byte[] ImageSeguro { get; set; }
        public int MonedaId { get; set; }
        public string CodInstitucion { get; set; }
        public string IdPago { get; set; }
        public string ProductoId { get; set; }
        
        public string CodigoPago { get; set; }
        public string EstadoPago { get; set; }


        public DateTime FechaDePago { get; set; }
        public string NombreBanco { get; set; }



        public string TipoSeguro { get; set; }

        public string FechaPago { get; set; }
        public string FechaVigenciaPolizaInicio { get; set; }
        public string FechaVigenciaPolizaFin { get; set; }
        public string AfiliacionSeguroId { get; set; }

       
        #endregion
     #region********OBTENER NUMERO DE PRIMA
        public int CodAsociacion { get; set; }
        public string TotalPrima { get; set; }
        #endregion

        public string TipoPadre { get; set; }

    }
}
