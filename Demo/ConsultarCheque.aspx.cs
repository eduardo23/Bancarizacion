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
        public static String UserId = "";
        public static ClientResponse response;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                UserId= Session["Usuario"].ToString();
            }
        }

        [WebMethod]
        public static List<Campañas> GetFLstCampana()
        {
            CampañasDAO objCampaña = new CampañasDAO();
            List<Campañas> oCampañas = objCampaña.ListarCampañas();
            return oCampañas;
        }

        [WebMethod]
        public static List<TipoProducto> GetFLstProducto()
        {
            TipoProductoDAO ObjTipoProducto = new TipoProductoDAO();
            List<TipoProducto> oTipoProducto = ObjTipoProducto.ListarTipoProductos();
            return oTipoProducto;
        }

        [WebMethod]
        public static List<InstitucionEducativa> GetFLstInstitucion()
        {
            InstitucionEducativaDAO objInstitucion = new InstitucionEducativaDAO();
            List<InstitucionEducativa> oLstInst = objInstitucion.getLst();
            return oLstInst;
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static ClientResponse GrabarCheque(List<string> arr)
        {           
            int txtID = 0;
            int ddlCampana = 0;
            int ddlInstitucion = 0;
            int ddlCia = 0;
            int ddlProducto = 0;
            int ddlBanco = 0;
            int ddlMoneda = 0;
            decimal txtMonto = 0;
            string txtFecha = string.Empty;
            string txtNroCheque = string.Empty;
            try
            {
                int.TryParse(arr[0], out txtID);
                int.TryParse(arr[1], out ddlCampana);
                int.TryParse(arr[2], out ddlInstitucion);
                int.TryParse(arr[3], out ddlCia);
                int.TryParse(arr[4], out ddlProducto);
                int.TryParse(arr[5], out ddlBanco);
                int.TryParse(arr[6], out ddlMoneda);
                decimal.TryParse(arr[9], out txtMonto);
                txtFecha = arr[7];
                txtNroCheque = arr[8];

                DateTime FechaDate = DateTime.Parse(txtFecha);
                Cheque pCheque = new Cheque()
                {
                    ID = txtID,
                    CampaniaID = ddlCampana,
                    InstitucionEducativaID = ddlInstitucion,
                    CIASeguroID = ddlCia,
                    ProductoID = ddlProducto,
                    BancoID = ddlBanco,
                    MonedaID = ddlMoneda,
                    Fecha = FechaDate,
                    NroCheque = txtNroCheque,
                    Monto = txtMonto


                };
         
                using (ChequeDAO dbChq = new ChequeDAO())
                {
                    if (pCheque.ID == 0)
                    {
                        pCheque.FechaCreacion = DateTime.Now.Date;
                        pCheque.Activo = true;
                        pCheque.UsuarioCreacion = UserId;
                        response = dbChq.Agregar(pCheque);

                    }
                    else
                    {
                        pCheque.FechaActualizacion = DateTime.Now.Date;
                        pCheque.Activo = true;
                        pCheque.UsuarioActualizacion = UserId;
                        response = dbChq.Grabar(pCheque);
                        //dbChq.Grabar(pCheque);
                        //sRet = "'El cheque se ha Actualizado Satisfactoriamente.'";
                    }

                }
           
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
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
        public static ClientResponse getChequeXId(int id)
        {
            ClientResponse response;
            try
            {
                using (ChequeDAO dbChq = new ChequeDAO())
                {
                    response = dbChq.getObtenerChequeXId(id);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }

        [WebMethod]
        public static ClientResponse listar_reporte(int CampaniaID, int ProductoID, int InstitucionEducativaID, String FechaInicial, String FechaFinal, int paginaActual, int RegistroXpagina)
        {
            ClientResponse response;
            try
            {
                using (ChequeDAO dbChq = new ChequeDAO())
                {
                    DateTime? dFechaInicial=null;
                    if (FechaInicial!= "")
                    {
                        dFechaInicial = Convert.ToDateTime(FechaInicial);
                    }

                    DateTime? dFechaFinal=null;
                    if (FechaFinal != "")
                    {
                        dFechaFinal = Convert.ToDateTime(FechaFinal);
                    }

                    response = dbChq.listarReporte(CampaniaID, ProductoID, InstitucionEducativaID, dFechaInicial, dFechaFinal, paginaActual, RegistroXpagina);
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