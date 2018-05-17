using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAO_Hermes.Repositorios;
using DAO_Hermes.ViewModel;
namespace Demo.Cobranza
{
    public partial class Reportes : System.Web.UI.Page
    {
        Campañas obje = new Campañas();
        CampañasDAO objn = new CampañasDAO();

        Cia_Seguro obje_CiaSeguros = new Cia_Seguro();
        CiaSeguro_DAO objn_CiaSeguros = new CiaSeguro_DAO();

        Institucion_Educativa obje_IE = new Institucion_Educativa();
        InstitucionEducativaDAO objn_IE = new InstitucionEducativaDAO();
        TipoInstitucionEducativaDAO objn_TIE = new TipoInstitucionEducativaDAO();

        TipoProducto obje_TipoProducto = new TipoProducto();
        TipoProductoDAO objn_TipoProducto = new TipoProductoDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
              ListarCampañas();
              ListarMaxCiaSeguros();
              ListarMaxInstitucionEducativa();
              ListarMaxTipoSeguro();
              ListarTipoProductos();
              ListarInstitucionEducativa();
              ocultar1.Visible = false;
              ocultar2.Visible = false;
              ocultar3.Visible = false;
            }
        }

        private void ListarCampañas()
        {
           List<Campañas> ListarCampañas = objn.ListarCampañas();
           DDLCampaña.DataSource = ListarCampañas;
           DDLCampaña.DataValueField ="ID";
           DDLCampaña.DataTextField ="Nombre";
           DDLCampaña.DataBind();
           //DDLCampaña.Items.Insert(0, "---SELECCIONE---");
        }

        private void ListarMaxCiaSeguros()
        {
            obje_CiaSeguros.CampaniaID =Convert.ToInt32(DDLCampaña.SelectedValue);
            obje_CiaSeguros.TipoCambio =1;
            List<Cia_Seguro> ListarMaxTotalCiaSeguro = objn_CiaSeguros.ListarMaxTotal_CiaSeguros(obje_CiaSeguros);
            lblSoles.Text = ListarMaxTotalCiaSeguro[0].SimboloSoles.ToString();
            lblTotalSoles.Text=Convert.ToDecimal(ListarMaxTotalCiaSeguro[0].MontoSoles).ToString("##,##.00");

            lblDolares.Text = ListarMaxTotalCiaSeguro[0].SimboloDolares.ToString();
            lblTotalDolares.Text = Convert.ToDecimal(ListarMaxTotalCiaSeguro[0].MontoDolares).ToString("##,##.00");
        }

        private void ListarMaxInstitucionEducativa()
        {
            obje_IE.CampaniaID = Convert.ToInt32(DDLCampaña.SelectedValue);
            obje_IE.TipoCambio = 1;
            List<Institucion_Educativa> ListarMaxTotalInstitucionEducativa =objn_IE.ListarMaxTotal_InstitucionEducativa(obje_IE);

            string Espacio = "&nbsp;&nbsp;";
            Espacio = Server.HtmlDecode(Espacio);

            lblSolesIE.Text = ListarMaxTotalInstitucionEducativa[0].SimboloSoles.ToString();
            lblTotalSolesAseguradosIE.Text = "Total Aseg:"+ Espacio + ListarMaxTotalInstitucionEducativa[0].AseguradosSoles.ToString();
            lblTotalSolesIE.Text = Convert.ToDecimal(ListarMaxTotalInstitucionEducativa[0].MontoSoles).ToString("##,##.00");

            lblDolatresIE.Text = ListarMaxTotalInstitucionEducativa[0].SimboloDolares.ToString();
            lblTotalDolaresAsegurasIE.Text = "Total Aseg:" + Espacio + ListarMaxTotalInstitucionEducativa[0].AseguradosDolares.ToString();
            lblTotalDolaresIE.Text = Convert.ToDecimal(ListarMaxTotalInstitucionEducativa[0].MontoDolares).ToString("##,##.00");
        }

        private  void ListarMaxTipoSeguro()
        {
            obje_TipoProducto.CampaniaID = Convert.ToInt32(DDLCampaña.SelectedValue);
            obje_TipoProducto.TipoCambio = 1;
            List<TipoProducto> ListarMaxTotalTipoProducto = objn_TipoProducto.ListarMaxTotal_TipoProducto(obje_TipoProducto);
            lblSeguro.Text = ListarMaxTotalTipoProducto[0].NombreProducto;
            lblSolesTipoSeguro.Text = ListarMaxTotalTipoProducto[0].SimboloSoles.ToString();
            lblTotalSolesTipoSeguro.Text = Convert.ToDecimal(ListarMaxTotalTipoProducto[0].MontoSoles).ToString("##,##.00");

            lblDolaresTipoSeguro.Text = ListarMaxTotalTipoProducto[0].SimboloDolares.ToString();
            lblTotalDolaresTipoSeguro.Text = Convert.ToDecimal(ListarMaxTotalTipoProducto[0].MontoDolares).ToString("##,##.00");
        }

        private void ListarTipoProductos()
        {
            ddlTipoProducto.DataSource = objn_TipoProducto.ListarTipoProductos();
            ddlTipoProducto.DataTextField = "Nombre";
            ddlTipoProducto.DataValueField = "ID";
            ddlTipoProducto.DataBind();
        }

        private void ListarInstitucionEducativa()
        {
            ddlInstitucionEducativa.DataSource = objn_TIE.ListarInstitucionEducativa();
            ddlInstitucionEducativa.DataValueField = "ID";
            ddlInstitucionEducativa.DataTextField = "Nombre";           
            ddlInstitucionEducativa.DataBind();
        }
        protected void DDLCampaña_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListarMaxCiaSeguros();
            ListarMaxInstitucionEducativa();
            ListarMaxTipoSeguro();

            int TipoReporte = 1;
            int CodCampaña = Convert.ToInt32(DDLCampaña.SelectedValue);
            Response.Redirect("CompañiaDeSeguros.aspx?TipoReporte=" + TipoReporte + "&CodCampaña=" + CodCampaña);
        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {
          //int TipoReporte = 1;
          //int CodCampaña = 2;// Convert.ToInt32(DDLCampaña.SelectedValue);
          //Response.Redirect("CobranzaProducto.aspx?TipoReporte=" + TipoReporte + "&CodCampaña=" + CodCampaña);
        }

        protected void btn_Limpiar_Click(object sender, EventArgs e)
        {

        }

        protected void btn_ReporteCompañiaSeguros_Click(object sender, ImageClickEventArgs e)
        {
          ocultar1.Visible = true; ocultar2.Visible = false; ocultar3.Visible = false;
        }

        protected void btn_ReporteInstituciones_Click(object sender, ImageClickEventArgs e)
        {
          ocultar1.Visible = false; ocultar2.Visible = true; ocultar3.Visible = false;
        }

        protected void btn_ReporteProductos_Click(object sender, ImageClickEventArgs e)
        { 
          int TipoReporte = 1;
          int CodCampaña = 2;// Convert.ToInt32(DDLCampaña.SelectedValue);
          Response.Redirect("CobranzaProducto.aspx?TipoReporte=" + TipoReporte + "&CodCampaña=" + CodCampaña);
          ocultar1.Visible = false; ocultar2.Visible = false; ocultar3.Visible = true;
        }

        protected void btn_VerReporte_Click(object sender, EventArgs e)
        {
          int TipoReporte = 1;
          int CodCampaña = 2;//Convert.ToInt32(DDLCampaña.SelectedValue);
          int CodProducto = Convert.ToInt32(ddlTipoProducto.SelectedValue);
          DateTime FechaInicio =Convert.ToDateTime(txtFechaInicio.Text);
          DateTime FechaFin = Convert.ToDateTime(txtFechaFinal.Text);
          Response.Redirect("ReporteBusquedaProductos.aspx?TipoReporte=" + TipoReporte + "&CodCampaña=" + CodCampaña +"&CodProducto=" +CodProducto +
                                                                                            "&FechaInicio=" +FechaInicio + "&FechaFin=" +FechaFin);
          ocultar1.Visible = false; ocultar2.Visible = false; ocultar3.Visible = true;
        }

    }
}