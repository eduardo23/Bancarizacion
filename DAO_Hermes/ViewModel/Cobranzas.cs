using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Hermes.ViewModel
{
 public   class Cobranzas
    {
        public int TipoReporte { get; set; }
        public int CampaniaID { get; set; }
        public int CIASeguroID { get; set; }
        public string FechaInicioCIASeguro { get; set; }
        public string FechaFinCIASeguro { get; set; }
        public int? InstitucionEducativaID { get; set; }
        public string FechaInicioInstitucionEducativa { get; set; }
        public string FechaFinInstitucionEducativa { get; set; }
        public int? ProductoID { get; set; }
        public string FechaInicioProducto { get; set; }
        public string FechaFinProducto { get; set; }

        //*****************************************************************
        public string Nombre { get; set; }
        public int AccidentesExisteProducto { get; set; }
        public string AccidentesNombre { get; set; }
        public string AccidentesPrimaSoles { get; set; }
        public int? AccidentesTotalAseguradosSoles { get; set; }
        public string AccidentesPrimaDolares { get; set; }
        public int? AccidentesTotalAseguradosDolares { get; set; }
        public Decimal _AccidentesTotalRecaudadoSoles { get; set; }
        public Decimal _AccidentesTotalPagadoSoles { get; set; }
        public Decimal _AccidentesTotalPendienteSoles { get; set; }
        public Decimal _AccidentesTotalRecaudadoDolares { get; set; }
        public Decimal _AccidentesTotalPagadoDolares { get; set; }
        public Decimal _AccidentesTotalPendienteDolares { get; set; }
        public int RentaExisteProducto { get; set; }
        public string RentaNombre { get; set; }
        public string RentaPrimaSoles { get; set; }
        public int RentaTotalAseguradosSoles { get; set; }
        public string RentaPrimaDolares { get; set; }
        public int? RentaTotalAseguradosDolares { get; set; }
        public Decimal? _RentaTotalRecaudadoSoles { get; set; }
        public Decimal? _RentaTotalPagadoSoles { get; set; }
        public Decimal? _RentaTotalPendienteSoles { get; set; }
        public Decimal _RentaTotalRecaudadoDolares { get; set; }
        public Decimal? _RentaTotalPagadoDolares { get; set; }
        public Decimal? _RentaTotalPendienteDolares { get; set; }

    }
}
