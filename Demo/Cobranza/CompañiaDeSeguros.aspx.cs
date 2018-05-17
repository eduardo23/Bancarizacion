using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAO_Hermes.Repositorios;
using DAO_Hermes.ViewModel;
using Microsoft.Reporting.WebForms;
using AjaxControlToolkit;

namespace Demo.Cobranza
{
    public partial class CompañiaDeSeguros : System.Web.UI.Page
    {     
        Cobranzas obje_Cobranzas = new Cobranzas();
        CobranzasDAO objn_Cobranzas = new CobranzasDAO();      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                    try
                    {
                    obje_Cobranzas.TipoReporte =Convert.ToInt32(Request.QueryString["TipoReporte"]);
                    obje_Cobranzas.CampaniaID =  Convert.ToInt32(Request.QueryString["CodCampaña"]);
                    obje_Cobranzas.CIASeguroID =0;
                    obje_Cobranzas.FechaInicioCIASeguro = "";
                    obje_Cobranzas.FechaFinCIASeguro = "";
                    obje_Cobranzas.InstitucionEducativaID = 0;
                    obje_Cobranzas.FechaInicioInstitucionEducativa = "";
                    obje_Cobranzas.FechaFinInstitucionEducativa = "";
                    obje_Cobranzas.ProductoID = 0;
                    obje_Cobranzas.FechaInicioProducto = "";
                    obje_Cobranzas.FechaFinProducto = "";
                    List<Cobranzas> ListaTipoSeguro = objn_Cobranzas.ListarCobranzasPorTipo_Seguro(obje_Cobranzas);
                    ReportDataSource rptDs = new ReportDataSource("Reporte_Cobranza", ListaTipoSeguro);

                    RPVCompañiaDeSeguros.LocalReport.DataSources.Add(rptDs);
                    RPVCompañiaDeSeguros.LocalReport.ReportPath = Server.MapPath("ReporteCobranza.rdlc");
                    RPVCompañiaDeSeguros.ShowParameterPrompts = false;
                    RPVCompañiaDeSeguros.LocalReport.Refresh();

                    string FilePDF = "CompañiaDeSeguros_" + DateTime.Now.Date.ToString("ddMMyyyyhhmm") + ".PDF";
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;

                    byte[] bytes = RPVCompañiaDeSeguros.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    //FilePDF = "01-" + serie + "-" + numero + ".PDF";
                    string ruta = Server.MapPath("../Cobranza/");
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment;filename=" + FilePDF);
                    Response.OutputStream.Write(bytes, 0, bytes.Length);
                    Response.Flush();
                    Response.End();
                }
                    catch (Exception ex)
                    {
                    }             
            }
        }

 

    }
}