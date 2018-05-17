using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAO_Hermes.Repositorios;
using DAO_Hermes;

namespace Demo
{
    public partial class ListaCiaSeguros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                CargaListaSeguro();
            }
        }

        void CargaListaSeguro()
        {
            using (CiaSeguro_DAO db = new CiaSeguro_DAO())
            {
                grvCiaSeguros.DataSource = db.ListarSeguros();
                grvCiaSeguros.DataBind();
            }
        }

        protected void grvCiaSeguros_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvCiaSeguros.PageIndex = e.NewPageIndex;
            CargaListaSeguro();
        }

        protected void grvCiaSeguros_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            int id = Convert.ToInt32(e.Row.Cells[9].Text);

            GridView gvTipoSeguro = (GridView)e.Row.FindControl("grvTipoSeguro");
            GridView gvTipoCuenta = (GridView)e.Row.FindControl("grvTipoCuenta");
                        
           cargarTipoSeguro(gvTipoSeguro, id);
           cargarTipoCuenta(gvTipoCuenta, id);
        }

        void cargarTipoSeguro(GridView grv , int id)
        {
            CiaSeguro_DAO db = new CiaSeguro_DAO();
            grv.DataSource = db.ListarTipoSeguro(id);
            grv.DataBind();
        }

        void cargarTipoCuenta(GridView grv, int id)
        {
            CiaSeguro_DAO db = new CiaSeguro_DAO();
            grv.DataSource = db.ListarTipoCuentas(id);
            grv.DataBind();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            MVSeguros.ActiveViewIndex = 1;
            cUtil.ListarUbigeo(DDLDepartamento, "01", "00", "00");
        }

        protected void DDLDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            cUtil.ListarUbigeo(DDLProvincia, DDLDepartamento.SelectedValue.Substring(0, 2), "01", "00");
            cUtil.ListarUbigeo(DDLDistrito, DDLDepartamento.SelectedValue.Substring(0, 2), DDLProvincia.SelectedValue.Substring(2, 2), "01");
        }

        protected void DDLProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            cUtil.ListarUbigeo(DDLDistrito, DDLDepartamento.SelectedValue.Substring(0, 2), DDLProvincia.SelectedValue.Substring(2, 2), "01");
        }

        protected void btnGuardarCiaSeguro_Click(object sender, EventArgs e)
        {
            using (CiaSeguro_DAO db = new CiaSeguro_DAO())
            {
                CIASeguro ciaseg = new CIASeguro();
                ciaseg.ID = Convert.ToInt32(Session["idCiaSeguro"]);
                ciaseg.Nombre = txtRazonSocial.Text;
                ciaseg.RUC = txtRuc.Text;
                ciaseg.Direccion = txtDireccion.Text;
                ciaseg.CodigoUbigeo = DDLDepartamento.SelectedValue.Substring(0, 2) + DDLProvincia.SelectedValue.Substring(2, 2) + DDLDistrito.SelectedValue.Substring(4, 2);
                //ciaseg.NombreAdj = lblArchivo.Text;
                ciaseg.TipoContenidoAdj = "";
                ciaseg.TamanioAdj = 444;
                ciaseg.DatoAdj = null;

                if(chkEmpRecaudadora.Checked ==true)
                {
                    ciaseg.IsEmpresaRecaudadora = true;
                }
                else
                {
                    ciaseg.IsEmpresaRecaudadora = false;
                }

                ciaseg.Estado = true;
                ciaseg.Codigo = null;
                ciaseg.TerminosCondiciones = null;
                ciaseg.UsuarioCreacion = null;
                ciaseg.FechaCreacion = DateTime.Now;
                ciaseg.UsuarioActualizacion = "";
                ciaseg.FechaActualizacion = DateTime.Now;

                db.agregarSeguro(ciaseg);
              //  string jss = "openModal()";
               // ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
            }
        }

        protected void btnCancelarCiaSeguro_Click(object sender, EventArgs e)
        {
            MVSeguros.ActiveViewIndex = 0;
        }
        
        protected void grvTipoSeguro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

            if (e.CommandName == "EditaTipoSeguro")
            {
                Session["idContacto"] = row.Cells[0].Text;
                txtTipoSeguro.Text = row.Cells[1].Text;
                txtTipoSeguro.Enabled = false;
                //DLTipoSeguro.Text = row.Cells[2].Text;
                //DDLTrama.Text = row.Cells[3].Text;
                                
                string jss = "openEditarTipoSeguro();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
            }

            if (e.CommandName == "AnulaTipoSeguro")
            {
            //    using (CiaSeguro_DAO db = new CiaSeguro_DAO())
            //    {
            //        try
            //        {
            //            int id = Convert.ToInt32(row.Cells[0].Text);
            //            if(id>0)
            //            {
            //                db.Anular(id);
            //                CargaListaSeguro();
            //                //txtmensaje.Text = "¡El tipo se seguro: " + row.Cells[1].Text + " fue anulado con exito!";

            //                string jss = "openModal()";
            //                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            //txtmensaje.Text = ex.Message;
            //            string jss = "openModal()";
            //            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
            //        }
            //    }
            }
        }


        protected void grvCiaSeguros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Agregar")
            {
                int fila = Convert.ToInt32(e.CommandArgument);
                txtTipoSeguro.Text =HttpUtility.HtmlDecode(  Convert.ToString(grvCiaSeguros.Rows[fila].Cells[2].Text));
                txtTipoSeguro.Enabled = false;
                
                // LLenar combobox ...!!
                cUtil.ListarTipoSeguro(DDLTipoSeguro);
                cUtil.ListarTrama(DDLTrama);

                string jss = "openEditarTipoSeguro();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
            }

            if(e.CommandName =="Edita")
            {
                int fila = Convert.ToInt32(e.CommandArgument);
                int id = Convert.ToInt32(grvCiaSeguros.Rows[fila].Cells[9].Text);
                using (CiaSeguro_DAO db = new CiaSeguro_DAO())
                {
                    cUtil.ListarUbigeo(DDLDepartamento, "01", "00", "00");
                    CIASeguro ie = db.Buscar(id);
                   
                    Session["idSeguro"] = ie.ID.ToString();
                    txtRazonSocial.Text = HttpUtility.HtmlDecode(  ie.Nombre);
                    txtRuc.Text = ie.RUC;
                    txtDireccion.Text = ie.Direccion;
                    byte[] imagen = db.ObtenerImagenLogo(Convert.ToInt32(grvCiaSeguros.Rows[fila].Cells[1].Text));
                    if(imagen != null)
                    {
                        img_destinoCia.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(imagen);
                    }

                    DDLDepartamento.SelectedValue = ie.CodigoUbigeo.Substring(0, 2) + "0000";
                    DDLDepartamento_SelectedIndexChanged(null, null);
                    DDLProvincia.SelectedValue = ie.CodigoUbigeo.Substring(0, 2) + ie.CodigoUbigeo.Substring(2, 2) + "00";
                    DDLProvincia_SelectedIndexChanged(null, null);
                    DDLDistrito.SelectedValue = ie.CodigoUbigeo.Substring(0, 2) + ie.CodigoUbigeo.Substring(2, 2) + ie.CodigoUbigeo.Substring(4, 2);
                    chkEmpRecaudadora.Checked = ie.IsEmpresaRecaudadora;
                    btnGuardarCiaSeguro.Text = "Editar";
                    MVSeguros.ActiveViewIndex = 1;
                }
            }

            if(e.CommandName =="Anula")
            {
                int fila = Convert.ToInt32(e.CommandArgument);
                Label lblestado =(Label) grvCiaSeguros.Rows[fila].FindControl("lblestado");
                int id = Convert.ToInt32(grvCiaSeguros.Rows[fila].Cells[9].Text);
                hdnIdCompañiaSeguro.Value = id.ToString();
                hdnEstado.Value = lblestado.Text;

                using (CiaSeguro_DAO db = new CiaSeguro_DAO())
                {
                    
                    if (lblestado.Text.ToUpper() == "ACTIVO")
                    {
                        lblTitleConfirm.Text = "Desactivar Compañía de Seguros";
                        lblmsgConfirm.Text = "¿Desea desactivar esta compañia de seguro: " + grvCiaSeguros.Rows[fila].Cells[2].Text + " ?";
                    }
                   else if (lblestado.Text == "NO ACTIVO")
                    {
                        lblTitleConfirm.Text = "Activar Compañía de Seguros";
                        lblmsgConfirm.Text = "¿Desea activar esta compañía de seguro: " + grvCiaSeguros.Rows[fila].Cells[2].Text + " ?";
                    }

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "openConfirm();", true);               
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            MVSeguros.ActiveViewIndex = 0;
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            using (CiaSeguro_DAO db = new CiaSeguro_DAO())
            {
                int id = Convert.ToInt32(hdnIdCompañiaSeguro.Value);
                db.Anular(id , hdnEstado.Value.ToUpper());
                CargaListaSeguro();                
            }
        }
    }
}