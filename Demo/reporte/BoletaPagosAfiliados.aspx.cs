using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using DAO_Hermes.Repositorios;
using DAO_Hermes.ViewModel;

namespace Demo.Reportes
{
    public partial class BoletaPagosAfiliados : System.Web.UI.Page
    {   Afiliacion obje = new Afiliacion();
        AfiliacionDAO objn = new AfiliacionDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    string IdProducto = Request.QueryString["gv_x2cr2?02"].ToString();
                 
                    if (IdProducto == "1")
                    {                   
                    //obje.CodigoInstitucion = Request.QueryString["v_x12car1?01"].ToString();
                    obje.UsuarioCreacion = Request.QueryString["v_x12car1?01"].ToString();
                    obje.CodigoPago = Request.QueryString["v_x1car2?02"].ToString();
                  
                    List<Afiliacion> Lista = objn.ListarReportePagos_Afiliacion(obje);
                    ////DataTable Lista = objn.ListarReportePagos_Afiliacion(obje);
                    ReportDataSource rptDs = new ReportDataSource("ReciboDePagos", Lista);

                    RPVBoletaAfiliado.LocalReport.DataSources.Add(rptDs);
                    RPVBoletaAfiliado.LocalReport.ReportPath = Server.MapPath("BoletaPagosAfiliados.rdlc");

                    List<ReportParameter> parametros = new List<ReportParameter>();
                    ReportParameter parametro1 = new ReportParameter("Nombre", Lista[0].NombreInstitucion);
                    ReportParameter parametro2 = new ReportParameter("Codigo", Lista[0].CodigoPago.ToString());
                    ReportParameter parametro3 = new ReportParameter("Beneficiario", Lista[0].Beneficiario.ToString());
                    ReportParameter parametro4 = new ReportParameter("FechaVigenciaPolizaInicio", Convert.ToDateTime(Lista[0].FechaVigenciaPolizaInicio).ToString("dd/MM/yyyy"));
                    ReportParameter parametro5 = new ReportParameter("FechaVigenciaPolizaFin", Convert.ToDateTime(Lista[0].FechaVigenciaPolizaFin).ToString("dd/MM/yyyy"));
                    ReportParameter parametro6;
                    if(Lista[0].TipoSeguro == "1")
                    {
                       parametro6 = new ReportParameter("TipoDeSeguro", "SEGURO DE ACCIDENTES ESTUDIANTILES");
                    }
                    else //if(Lista[0].TipoSeguro == "2")
                    {
                       parametro6 = new ReportParameter("TipoDeSeguro", "SEGURO DE RENTA ESTUDIANTIL");
                    }

                    ReportParameter parametro7;
                    string Espacio = "&nbsp;";
                    Espacio = Server.HtmlDecode(Espacio);
                    if(Lista[0].MonedaId == 1)//Soles
                    {
                      parametro7 = new ReportParameter("Prima", "S/." + Espacio + Lista[0].Prima.ToString());
                    }
                    else//Dolares
                    {
                      parametro7 = new ReportParameter("Prima", "US$" + Espacio + Lista[0].Prima.ToString());
                    }

                    ReportParameter parametro8;

                    if (Lista[0].TipoSeguro == "1")
                    {
                       parametro8 = new ReportParameter("Asegurado", Lista[0].Asegurado.ToString());
                    }
                    else //if(Lista[0].TipoSeguro == "2")
                    {
                       parametro3 = new ReportParameter("Beneficiario", Lista[0].Beneficiario.ToString());
                       parametro8 = new ReportParameter("Asegurado", Lista[0].Asegurado.ToString());
                    }

                    ReportParameter parametro9 = new ReportParameter("FechaDePago", Convert.ToDateTime(Lista[0].FechaPago).ToString("dd/MM/yyyy"));
                    parametros.Add(parametro1);
                    parametros.Add(parametro2);
                    parametros.Add(parametro3);
                    parametros.Add(parametro4);
                    parametros.Add(parametro5);
                    parametros.Add(parametro6);
                    parametros.Add(parametro7);
                    parametros.Add(parametro8);
                    parametros.Add(parametro9);
                    RPVBoletaAfiliado.LocalReport.SetParameters(parametros);
                    //RPVBoletaAfiliado.LocalReport.Refresh();

                    string FilePDF = "BoletaPagosAfiliados_" + DateTime.Now.Date.ToString("ddMMyyyyhhmm") + ".PDF";
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;

                    byte[] bytes = RPVBoletaAfiliado.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    //FilePDF = "01-" + serie + "-" + numero + ".PDF";
                    string ruta = Server.MapPath("/rptTemp/");
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment;filename=" + FilePDF);
                    Response.OutputStream.Write(bytes, 0, bytes.Length);
                    Response.Flush();
                    Response.End();
                    }
                    else if (IdProducto == "2")
                    {
                       
                        obje.UsuarioCreacion = Request.QueryString["v_x12car1?01"].ToString();
                        obje.CodigoPago = Request.QueryString["v_x1car2?02"].ToString();

                        List<Afiliacion> Lista = objn.ListarReportePagos_Afiliacion(obje);
                        ReportDataSource rptDs = new ReportDataSource("ReciboDePagos", Lista);

                        RPVBoletaAfiliado.LocalReport.DataSources.Add(rptDs);
                        RPVBoletaAfiliado.LocalReport.ReportPath = Server.MapPath("BoletaDePagosRentaAfiliados.rdlc");

                        List<ReportParameter> parametros = new List<ReportParameter>();
                        ReportParameter Nombre = new ReportParameter("Nombre", Lista[0].NombreInstitucion);
                        ReportParameter Codigo = new ReportParameter("Codigo", Lista[0].CodigoPago.ToString());
                        ReportParameter Beneficiario = new ReportParameter("Beneficiario", Lista[0].Beneficiario.ToString());

                        ReportParameter Prima;
                        string Espacio = "&nbsp;";
                        Espacio = Server.HtmlDecode(Espacio);
                        if (Lista[0].MonedaId == 1)//Soles
                        {
                            Prima = new ReportParameter("Prima", "S/." + Espacio + Lista[0].Prima.ToString());
                        }
                        else//Dolares
                        {
                            Prima = new ReportParameter("Prima", "US$" + Espacio + Lista[0].Prima.ToString());
                        }

                        ReportParameter TipoDeSeguro;
                        if (Lista[0].ProductoId == "2")// hdnProductoId.Value == "2"
                        {
                            TipoDeSeguro = new ReportParameter("TipoDeSeguro", "SEGURO DE RENTA ESTUDIANTIL");
                        }
                        else //if(Lista[0].TipoSeguro == "1")
                        {
                            TipoDeSeguro = new ReportParameter("TipoDeSeguro", "SEGURO DE ACCIDENTES ESTUDIANTILES");
                        }


                        ReportParameter Asegurado=new ReportParameter();
                        if (Lista[0].ProductoId == "2")// hdnProductoId.Value == "2"
                        {
                            Beneficiario = new ReportParameter("Beneficiario", Lista[0].Asegurado);
                            Asegurado = new ReportParameter("Asegurado", Lista[0].Beneficiario);
                        }
                        //else //if(Lista[0].TipoSeguro == "1")
                        //{
                        //   Asegurado = new ReportParameter("Asegurado", rows.Alumno.ApellidoPaternno + Espacio + rows.Alumno.ApellidoMaterno + Espacio + rows.Alumno.Nombre.ToString());
                        //}

                        ReportParameter FechaVigenciaPolizaInicio = new ReportParameter("FechaVigenciaPolizaInicio", Convert.ToDateTime(Lista[0].FechaVigenciaPolizaInicio).ToString("dd/MM/yyyy"));
                        ReportParameter FechaVigenciaPolizaFin = new ReportParameter("FechaVigenciaPolizaFin", Convert.ToDateTime(Lista[0].FechaVigenciaPolizaFin).ToString("dd/MM/yyyy"));
                        ReportParameter FechaDePago = new ReportParameter("FechaDePago", Convert.ToDateTime(Lista[0].FechaPago).ToString("dd/MM/yyyy"));

                        parametros.Add(Nombre);
                        parametros.Add(Codigo);
                        parametros.Add(Beneficiario);
                        parametros.Add(Prima);
                        parametros.Add(TipoDeSeguro);
                        parametros.Add(Asegurado);
                        parametros.Add(FechaVigenciaPolizaInicio);
                        parametros.Add(FechaVigenciaPolizaFin);
                        parametros.Add(FechaDePago);

                        RPVBoletaAfiliado.LocalReport.SetParameters(parametros);
                        RPVBoletaAfiliado.LocalReport.Refresh();
                        string FilePDF = "BoletaPagosAfiliados_" + DateTime.Now.Date.ToString("ddMMyyyyhhmm") + ".PDF";
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;

                    byte[] bytes = RPVBoletaAfiliado.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    //FilePDF = "01-" + serie + "-" + numero + ".PDF";
                    string ruta = Server.MapPath("/rptTemp/");
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment;filename=" + FilePDF);
                    Response.OutputStream.Write(bytes, 0, bytes.Length);
                    Response.Flush();
                    Response.End();
                    }
                }
                catch (Exception ex)
                {

                }

            }
        }

        
    }
}