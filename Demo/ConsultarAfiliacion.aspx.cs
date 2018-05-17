using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DAO_Hermes.Repositorios;
using DAO_Hermes.ViewModel;
using System.IO;
using System;

namespace Demo
{
    public partial class ConsultarAfiliacion : System.Web.UI.Page
    {
        Afiliacion obje = new Afiliacion();
        AfiliacionDAO objn = new AfiliacionDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               LimpiarFicheros();
               CargarAsegurados();
            }
        }

        void LimpiarFicheros()
        {
            foreach (string fichero in Directory.GetFiles(Server.MapPath("~/Files\\"), "*.*"))//Eliminando el PDF de la carpeta Temporal, al momento de Carga
            {
              File.Delete(fichero);
            }
              //ListarAfiliacion();
        }

        //private void ListarAfiliacion()
        //{
        //    obje.UsuarioCreacion = Session["Usuario"].ToString();
        //    List<Afiliacion> ListarAfiliacion = objn.ListarAfiliacionPorUsuario(obje);
        //    grvAfiliacion.DataSource = ListarAfiliacion;
        //    grvAfiliacion.DataBind();
        //    //ViewState["Paginar"]=ListarAfiliacion;
        //}

        public string codigoInstitucion;
        protected void grvAfiliacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Image img = (Image)e.Row.FindControl("img");
                GridView grvDetallesAfiliado = (GridView)e.Row.FindControl("grvDetallesAfiliado");
                string codigoInstitucion = grvAfiliacion.DataKeys[e.Row.RowIndex].Values["CodigoAfiliacion"].ToString();
                //string codigo = e.Row.Cells[3].Text.ToString();
                //obje.CodigoInstitucion = codigoInstitucion; 
                obje.UsuarioAfiliado = Session["Usuario"].ToString();
                obje.IdInstitucion= grvAfiliacion.DataKeys[e.Row.RowIndex].Values["IdInstitucion"].ToString(); 
                obje.FechaCreacionAfiliacion= Convert.ToDateTime(grvAfiliacion.DataKeys[e.Row.RowIndex].Values["FechaCreacionAfiliacion"].ToString());
                List<Afiliacion> listar = objn.ListarPagosAfiliados(obje);
                if (listar.Count != 0)
                {
                   grvDetallesAfiliado.DataSource = listar;
                   ////ViewState["PlanPDF"] = listar[0].ImageSeguro;
                   //ViewState["CodIe"] = listar[0].CodigoInstitucion;                
                   //ViewState["CodPago"] = listar[0].Id;
                   ////ViewState["ProductoId"] = listar[0].ProductoId;
                   grvDetallesAfiliado.DataBind();
                }
            }
        }

        protected void btnBuscarAfiliados_Click(object sender, EventArgs e)
        {
            obje.NombreInstitucion = txtBusqueda.Text;
            List<Afiliacion> ListarAfiliacion = objn.ListarAfiliacionInstitucionPorNombre(obje , Session["Usuario"].ToString());
            grvAfiliacion.DataSource = ListarAfiliacion;
            grvAfiliacion.DataBind();
            //ViewState["Paginar"] = ListarAfiliacion;
        }

        void CargarAsegurados()
        {
            obje.NombreInstitucion = null;
            List<Afiliacion> ListarAfiliacion = objn.ListarAfiliacionInstitucionPorNombre(obje, Session["Usuario"].ToString());
            grvAfiliacion.DataSource = ListarAfiliacion;
            grvAfiliacion.DataBind();
        }


        protected void grvAfiliacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
          //grvAfiliacion.PageIndex = e.NewPageIndex;
          //grvAfiliacion.DataSource = ViewState["Paginar"];
          //grvAfiliacion.DataBind();
        }

        decimal TotalSoles = 0;
        decimal TotalDolares = 0;
        protected void grvDetallesAfiliado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {                   
                    int TipoMoneda = Convert.ToInt16(e.Row.Cells[6].Text);

                    GridView myGrid = (GridView)sender;
                    //GridViewRow myRow = (GridViewRow)imgBtn.Parent.Parent;
                    string IsPagado = Convert.ToString(myGrid.DataKeys[e.Row.RowIndex].Values["EstadoPago"]);

                    //string IsPagado= Convert.ToString(e.Row.Cells[15].Text);



                    string space = "&nbsp;";
                    space = Server.HtmlDecode(space);

                    if(TipoMoneda == 1)//SOLES
                    {
                      TotalSoles += Convert.ToDecimal(e.Row.Cells[3].Text); //Convert.ToDecimal(((Label)e.Row.FindControl("lblPrima")).Text);
                                                                              //((Label)e.Row.FindControl("lblPrima")).Text = "S/." + space + ((Label)e.Row.FindControl("lblPrima")).Text;
                      e.Row.Cells[3].Text= "S/." + space + Convert.ToDecimal(e.Row.Cells[3].Text);
                    }
                    else if (TipoMoneda == 2)//DOLARES
                    {
                      TotalDolares += Convert.ToDecimal(e.Row.Cells[3].Text); //Convert.ToDecimal(((Label)e.Row.FindControl("lblPrima")).Text);
                                                                               //((Label)e.Row.FindControl("lblPrima")).Text = "U$" + space + ((Label)e.Row.FindControl("lblPrima")).Text;
                      e.Row.Cells[3].Text = "U$" + space + Convert.ToDecimal(e.Row.Cells[3].Text);
                    }

                    if(IsPagado == "True")
                    {
                       //e.Row.Cells[13].Text = "CANCELADO";
                       ((Label)e.Row.FindControl("lblEstado")).Text = "CANCELADO";
                       ((ImageButton)e.Row.FindControl("ImgEstado")).Visible = false;
                    }
                    else
                    {
                       //e.Row.Cells[13].Text = "PENDIENTE";
                       ((ImageButton)e.Row.FindControl("ImgEstado")).ImageUrl = "~/Images/warning_Red.gif";
                       ((ImageButton)e.Row.FindControl("ImgEstado")).ToolTip = "PENDIENTE";                  
                       ((Label)e.Row.FindControl("lblEstado")).Visible = false;
                    }

                    DateTime FechaPago =Convert.ToDateTime(e.Row.Cells[8].Text);
                    if(FechaPago==Convert.ToDateTime("1/01/0001 00:00:00"))
                    {
                      e.Row.Cells[8].Text = "-";
                    }

                }
                catch (Exception)
                {
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                string space = "&nbsp;";
                space = Server.HtmlDecode(space);
                e.Row.Cells[1].Text = "Total(S/.):" + "<br>" + "Total(U$):";
                e.Row.Cells[1].Font.Bold = true;
                //((Label)e.Row.FindControl("lblSoles")).Text = "S/." + space + TotalSoles;
                //((Label)e.Row.FindControl("lblDolares")).Text = "U$" + space + TotalDolares;
                e.Row.Cells[2].Text = "S/." + space + TotalSoles +"</br>"+  "U$" + space + TotalDolares;
                TotalSoles = 0;
                TotalDolares = 0;
            }
        }

        protected void BtnRecibo_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton img = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)img.NamingContainer;
            //string cod = ViewState["CodIe"].ToString(); //"00832";
            string usuario = Session["Usuario"].ToString();
            string codPago = gvr.Cells[13].Text;
            //string codPago = ViewState["CodPago"].ToString();//"69079";
            string codProdcuto = gvr.Cells[17].Text; // ViewState["ProductoId"].ToString();//"1/2";
            Response.Redirect("Reportes/BoletaPagosAfiliados.aspx?v_x12car1?01=" + usuario + "&v_x1car2?02=" + codPago + "&gv_x2cr2?02=" + codProdcuto, true);
        }


        protected void grvDetallesAfiliado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            if (e.CommandName=="BtnPlan")
            {
                ImageButton imgBtn = (ImageButton)e.CommandSource;
                GridView myGrid = (GridView)sender;
                GridViewRow myRow = (GridViewRow)imgBtn.Parent.Parent;
                int CodAsociacion = Convert.ToInt32(myGrid.DataKeys[myRow.RowIndex].Values["CodAsociacion"]);

                String identificador = grv.Cells[4].Text.Trim() + ".pdf";
                //byte[] Archivo = (byte[])ViewState["PlanPDF"];
                AsociacionDAO db = new AsociacionDAO();
                byte[] data = (byte[])db.ObtenerPlanAsociacion(CodAsociacion);
                if (data != null)
                {
                    try
                    {
                        Response.Clear();
                        MemoryStream ms = new MemoryStream(data);
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=" + identificador);
                        Response.Buffer = true;
                        ms.WriteTo(Response.OutputStream);
                        HttpContext.Current.ApplicationInstance.CompleteRequest();

                        //string sFile = identificador;
                        //FileStream fs = new FileStream(Server.MapPath("~/rptTemp\\") + sFile, FileMode.Create);
                        //fs.Write(Archivo, 0, Convert.ToInt32(Archivo.Length));
                        //fs.Close();
                        //Response.AddHeader("content-disposition", "attachment;filename=" + sFile);
                        //Response.ContentType = "application/pdf";
                        //Response.BinaryWrite(Archivo);
                        //Response.End();
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                }
            }
        }


    }
}