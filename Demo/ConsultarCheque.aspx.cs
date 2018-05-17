using DAO_Hermes;
using DAO_Hermes.Repositorios;
using DAO_Hermes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Demo
{
    public partial class ConsultarCheque : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {

            }
        }

        [WebMethod]
        public static  List<Campañas> GetFLstCampana() {
            CampañasDAO objCampaña = new CampañasDAO();
            List<Campañas> oCampañas = objCampaña.ListarCampañas();
            return oCampañas;
        }

        [WebMethod]
        public static List<TipoProducto> GetFLstProducto() {
            TipoProductoDAO ObjTipoProducto = new TipoProductoDAO();
            List<TipoProducto> oTipoProducto = ObjTipoProducto.ListarTipoProductos();
            return oTipoProducto;
        }

        [WebMethod]
        public static List<InstitucionEducativa> GetFLstInstitucion() {
            InstitucionEducativaDAO objInstitucion= new InstitucionEducativaDAO();
            List<InstitucionEducativa> oLstInst= objInstitucion.getLst();
            return oLstInst;
        }
 
        protected void btnExportar_Click(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GrabarCheque(Cheque pCheque) {
            string sRet = "";
            try
            {
                pCheque.FechaCreacion = DateTime.Now.Date;
                pCheque.Activo = true;

                using (ChequeDAO dbChq = new ChequeDAO())
                {
                    if (pCheque.ID == 0)
                    {
                        dbChq.Agregar(pCheque);
                        sRet = "'El cheque se ha Emitido Satisfactoriamente.'";
                    }
                    else
                    {
                        dbChq.Grabar(pCheque);
                        sRet = "'El cheque se ha Actualizado Satisfactoriamente.'";
                    }

                }
            }
            catch (Exception)
            {
                sRet = "Ocurrio un error al intentar grabar el Cheque";
            }            
            return sRet;
        }

        [WebMethod]
        public static string EliminarCheque(Int32 Id)
        {
            string sRet = "";
            try
            {

                using (ChequeDAO dbChq = new ChequeDAO())
                {
                    dbChq.Eliminar(Id);
                    sRet = "El Cheque ha sido anulado satisfactoriamente";
                }
            }
            catch (Exception ex)
            {
                sRet = ex.Message.ToString();
            }
            return sRet;
        }

        [WebMethod]
        public static ClientResponse listar_reporte(int CampaniaID, int ProductoID, int InstitucionEducativaID, int paginaActual,int RegistroXpagina)
        {
            ClientResponse response;
            try
            {
                using (ChequeDAO dbChq = new ChequeDAO())
                {
                    response = dbChq.listarReporte(CampaniaID, ProductoID, InstitucionEducativaID,  paginaActual, RegistroXpagina);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }
    }
}