using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Hermes.ViewModel
{
   public class TipoProducto
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Estado { get; set; }


        #region************************MaxTotales_Producto*********************

        public int CampaniaID { get; set; }
        public decimal TipoCambio { get; set; }

        public string NombreProducto { get; set; }
        public string SimboloSoles { get; set; }
        public decimal MontoSoles { get; set; }
        public string SimboloDolares { get; set; }
        public decimal MontoDolares { get; set; }

        #endregion



        #region**************Reporte Por Tipo de Segúro************************

        public int TipoReporte { get; set; }
        public int CampaniaCodigo { get; set; }
        public int CIASeguroID { get; set; }
        public string FechaInicioCIASeguro { get; set; }
        public string FechaFinCIASeguro { get; set; }
        public int? InstitucionEducativaID { get; set; }
        public string FechaInicioInstitucionEducativa { get; set; }
        public string FechaFinInstitucionEducativa { get; set; }
        public int? ProductoID { get; set; }
        public string FechaInicioProducto { get; set; }
        public string FechaFinProducto { get; set; }

        //********************************************************************************
        public string NombreCompañiaSeguros { get; set; }
        public decimal TotalRecaudadoSoles { get; set; }
        public decimal TotalPagadoSoles { get; set; }
        public decimal TotalPendienteSoles { get; set; }
        public decimal TotalRecaudadoDolares { get; set; }
        public decimal TotalPagadoDolares { get; set; }
        public decimal TotalPendienteDolares { get; set; }


        public string AccidentesNombre { get; set; }
        public string RentaNombre { get; set; }

        #endregion

        public int CantidadPagos { get; set; }

    }
}
