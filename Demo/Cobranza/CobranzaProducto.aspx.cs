using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAO_Hermes.Repositorios;
using DAO_Hermes.ViewModel;
using Microsoft.Reporting.WebForms;

namespace Demo.Cobranza
{
    public partial class CobranzaProducto : System.Web.UI.Page
    {
        TipoProducto obje_TipoProducto = new TipoProducto();
        TipoProductoDAO objn_TipoProducto = new TipoProductoDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    obje_TipoProducto.TipoReporte = Convert.ToInt32(Request.QueryString["TipoReporte"]);
                    obje_TipoProducto.CampaniaCodigo = Convert.ToInt32(Request.QueryString["CodCampaña"]);
                    obje_TipoProducto.CIASeguroID = 0;
                    obje_TipoProducto.FechaInicioCIASeguro = "";
                    obje_TipoProducto.FechaFinCIASeguro = "";
                    obje_TipoProducto.InstitucionEducativaID = 0;
                    obje_TipoProducto.FechaInicioInstitucionEducativa = "";
                    obje_TipoProducto.FechaFinInstitucionEducativa = "";
                    obje_TipoProducto.ProductoID = 0;
                    obje_TipoProducto.FechaInicioProducto = "";
                    obje_TipoProducto.FechaFinProducto = "";

                    List<TipoProducto> ListaNombreSeguro = objn_TipoProducto.Reporte_AccidenteNombre(obje_TipoProducto);              
                    List<TipoProducto> ListaDataNombreSeguro = objn_TipoProducto.Reporte_Cobranza_ProductoAccidentes(obje_TipoProducto);

                    ReportDataSource rptDs = new ReportDataSource("TituloAccidentes", ListaNombreSeguro);
                    ReportDataSource rptDs1 = new ReportDataSource("DataAccidentes", ListaDataNombreSeguro);

                    RPVCobranzaProductos.LocalReport.DataSources.Add(rptDs);
                    RPVCobranzaProductos.LocalReport.DataSources.Add(rptDs1);

                    List<TipoProducto> ListaNombreRenta = objn_TipoProducto.Reporte_RentaNombre(obje_TipoProducto);
                    List<TipoProducto> ListaDataRentaSeguro = objn_TipoProducto.Reporte_Cobranza_ProductoRentas(obje_TipoProducto);

                    ReportDataSource rptDsTituloRenta = new ReportDataSource("TituloRenta", ListaNombreRenta);
                    ReportDataSource rptDsDataRenta = new ReportDataSource("DataRenta", ListaDataRentaSeguro);

                    RPVCobranzaProductos.LocalReport.DataSources.Add(rptDsTituloRenta);
                    RPVCobranzaProductos.LocalReport.DataSources.Add(rptDsDataRenta);

                    RPVCobranzaProductos.LocalReport.ReportPath = Server.MapPath("ReporteCobranzaPoducto.rdlc");

                    //List<ReportParameter> parametros = new List<ReportParameter>();
                    //ReportParameter NombreAccidente = new ReportParameter("NombreAccidente", ListaDataNombreSeguro[0].NombreCompañiaSeguros.ToString());
                    //ReportParameter NombreRenta = new ReportParameter("NombreRenta", ListaDataRentaSeguro[0].NombreCompañiaSeguros.ToString());
                   
                    //parametros.Add(NombreAccidente);
                    //parametros.Add(NombreRenta);

                    //RPVCobranzaProductos.LocalReport.SetParameters(parametros);

                    //RPVCobranzaProductos.ShowParameterPrompts = false;
                    RPVCobranzaProductos.LocalReport.Refresh();

                    string FilePDF = "CobranzaProducto" + DateTime.Now.Date.ToString("ddMMyyyyhhmm") + ".PDF";
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;

                    byte[] bytes = RPVCobranzaProductos.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
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