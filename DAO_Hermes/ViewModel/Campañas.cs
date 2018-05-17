using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Hermes.ViewModel
{
   public class Campañas
    {
        public string NombreCampaña { get; set; }
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime InicioVigencia { get; set; }
        public DateTime FinVigencia { get; set; }
        public string Situacion { get; set; }
        public string Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }




        #region*****************CIERRE DE CAMPAÑAS******************


        
                        
                        
                        



        public int Campaña_Id { get; set; }
        public string Asociacion_ID { get; set; }
        public string InstitucionEducativa_ID { get; set; }
        public int Producto_ID { get; set; }






        public string NombreInstitucion { get; set; }
        public string SEGURO { get; set; }
        public string CIASEGURO { get; set; }
        public string Bancos { get; set; }
        public int EstadoCierre { get; set; }


        public string CodAsociacion{ get; set; }
        public string CodInstitucion { get; set; }
        public int productId { get; set; }
        public int CodigoCampaña { get; set; }
        public int CierreDeInstitucion { get; set; }
        


        #endregion


        public string TIPO { get; set; }

        public int _ID { get; set; }
        public string _Nombre { get; set; }

        public DateTime _InicioVigencia { get; set; }
        public DateTime _FinVigencia { get; set; }
        public string _Estado { get; set; }



        #region****APERTURAR CAMPAÑAS**************************
        public int AsiciacionId { get; set; }
        public string Filtro { get; set; }


        #endregion
    }
}
