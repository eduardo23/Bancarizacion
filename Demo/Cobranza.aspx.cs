using DAO_Hermes;
using DAO_Hermes.Repositorios;
using DAO_Hermes.ViewModel;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Demo
{
    public partial class Cobranza : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetFLstCampana();
                GetFLstFiltro();
            }
        }

        public void GetFLstCampana()
        {
            CampañasDAO objCampaña = new CampañasDAO();
            List<Campañas> oCampañas = objCampaña.ListarCampañas();
            ddlFCampana.DataSource = oCampañas;
            ddlFCampana.DataTextField = "Nombre";
            ddlFCampana.DataValueField = "ID";
            ddlFCampana.DataBind();
        }

        public void getLstCiabyCamp(Int32 CampId)
        {
            CiaSeguro_DAO objCia = new CiaSeguro_DAO();
            List<Cia_Seguro> LstCia = objCia.getLstbyCamp(CampId);
            ddlFiltro.DataSource = LstCia;
            ddlFiltro.DataTextField = "Nombre";
            ddlFiltro.DataValueField = "ID";
            ddlFiltro.DataBind();

            ListItem lsItm = new ListItem("--TODOS--", "0");
            lsItm.Selected = true;
            ddlFiltro.Items.Add(lsItm);

        }

        public void getLstInsbyCamp(Int32 CampId)
        {
            InstitucionEducativaDAO objInst = new InstitucionEducativaDAO();
            List<Institucion_Educativa> LstIns = objInst.getLstByCampania(CampId);
            ddlFiltro.DataSource = LstIns;
            ddlFiltro.DataTextField = "Nombre";
            ddlFiltro.DataValueField = "ID";
            ddlFiltro.DataBind();

            ListItem lsItm = new ListItem("--TODOS--", "0");
            lsItm.Selected = true;
            ddlFiltro.Items.Add(lsItm);

        }

        public void getLstSegbyCamp(Int32 CampId)
        {
            TipoSeguro_DAO obj = new TipoSeguro_DAO();
            List<Producto> LstPrd = obj.getLstbyCamp(CampId);
            ddlFiltro.DataSource = LstPrd;
            ddlFiltro.DataTextField = "Nombre";
            ddlFiltro.DataValueField = "ID";
            ddlFiltro.DataBind();

            ListItem lsItm = new ListItem("--TODOS--", "0");
            lsItm.Selected = true;
            ddlFiltro.Items.Add(lsItm);
            
        }

        public void GetFLstFiltro()
        {
            ddlFiltro.Items.Clear();
            if (ddlFTipoReporte.SelectedValue == "1") {
                getLstCiabyCamp(Convert.ToInt32(ddlFCampana.SelectedValue));
                }
            if (ddlFTipoReporte.SelectedValue == "2")
            {
                getLstInsbyCamp(Convert.ToInt32(ddlFCampana.SelectedValue));
            }
            if (ddlFTipoReporte.SelectedValue == "3")
            {
                getLstSegbyCamp(Convert.ToInt32(ddlFCampana.SelectedValue));
            }

        }

        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            Buscar(); 
        }

        protected void ddlFCampana_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetFLstFiltro();
            Buscar();
        }
        protected void ddlFTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetFLstFiltro();
            Buscar();
        }

        private void Buscar() {
            ChequeDAO oChqDao = new ChequeDAO();
            DataSet ds = new DataSet();

            Int32 nCam = Convert.ToInt32(ddlFCampana.SelectedValue);
            Int32 nCia = 0;
            Int32 nIns = 0;
            Int32 nPrd = 0;
            string sRdlc = "";
            if (ddlFTipoReporte.SelectedValue == "1")
            {
                nCia = Convert.ToInt32(ddlFiltro.SelectedValue);
                sRdlc = "RepCobbyCia.rdlc";
            }
            if (ddlFTipoReporte.SelectedValue == "2")
            {
                nIns = Convert.ToInt32(ddlFiltro.SelectedValue);
                sRdlc = "RepCobbyIns.rdlc";
            }
            if (ddlFTipoReporte.SelectedValue == "3")
            {
                nPrd = Convert.ToInt32(ddlFiltro.SelectedValue);
                sRdlc = "RepCobbyPrd.rdlc";
            }

            //Int32 nPend = 1;
            //ReportParameter[] parameters = new ReportParameter[1];
            //parameters[0] = new ReportParameter("EstadoPago", "0");

            ds = oChqDao.getLstCobranza(nCam, nCia, nIns, nPrd/*, nPend*/);
            ReportDataSource rptDs = new ReportDataSource();
            rptDs.Value = ds.Tables[0];
            rptDs.Name = "DSReportCobranza";
            RptVCobranza.LocalReport.DataSources.Clear();
            RptVCobranza.LocalReport.DataSources.Add(rptDs);

            RptVCobranza.LocalReport.ReportEmbeddedResource = sRdlc;
            RptVCobranza.LocalReport.ReportPath = Server.MapPath(sRdlc);

            //RptVCobranza.LocalReport.SetParameters(parameters);

            RptVCobranza.LocalReport.Refresh();
        }
    }
}