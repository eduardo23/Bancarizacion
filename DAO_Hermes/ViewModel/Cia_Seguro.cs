using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Hermes.ViewModel
{
  public  class Cia_Seguro
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string RUC { get; set; }
        public string Direccion { get; set; }

        public int Estado { get; set; }


        #region************MAX TOTAL CIASEGUROS***************
        public int CampaniaID { get; set; }
        public Decimal? TipoCambio { get; set; }
        //****************************************************************
        public string NombreCiaSeguros { get; set; }
        public string SimboloSoles { get; set; }
        public Decimal MontoSoles { get; set; }
        public string SimboloDolares { get; set; }
        public Decimal MontoDolares { get; set; }
        #endregion
    }
}
