using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAO_Hermes.Repositorios;
using DAO_Hermes;

namespace Demo
{
    public partial class ListaTiposSeguro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                CargarTipoBancos();
            }
        }

        void CargarTipoBancos()
        {
            using (TipoProductoDAO db = new TipoProductoDAO())
            {                
                grvBancos.DataSource = db.ListarTipoProductos();
                grvBancos.DataBind();
            }
        }

        void CargarInstitucionesFiltro(string nombre, int id)
        {
            using (InstitucionEducativaDAO db = new InstitucionEducativaDAO())
            {
                grvBancos.DataSource = db.ListarInstitucionesXNombre(nombre, id);
                grvBancos.DataBind();
            }
        }        

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarInstitucionesFiltro(txtBusqueda.Text, 3);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            string jss = "openEditarProducto();";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
        }

        void Limpiar()
        {
            Session["idSeguro"] = 0;
            TxtNombre.Text = "";
            TxtDescripcion.Text = "";
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            MVTipoBancos.ActiveViewIndex = 0;
        }

        protected void grvBancos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int fila = Convert.ToInt32(e.CommandArgument);
            string jss = "";
            if (e.CommandName == "EditaTipoSeguro")
            {
                Session["idSeguro"] = grvBancos.Rows[fila].Cells[0].Text;
                TxtNombre.Text = grvBancos.Rows[fila].Cells[1].Text;
                TxtDescripcion.Text = grvBancos.Rows[fila].Cells[2].Text;
                
                jss = "openEditarProducto();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
            }

            if (e.CommandName == "AnulaTipoSeguro")
            {
                int id = Convert.ToInt32(grvBancos.Rows[fila].Cells[0].Text);
                hdnidTipoSeguro.Value=Convert.ToString(id);
                Label lblestado = (Label)grvBancos.Rows[fila].FindControl("lblestado");
                hdnEstado.Value = lblestado.Text;
                using (TipoProductoDAO db = new TipoProductoDAO())
                {
                    if (lblestado.Text.ToUpper() == "ACTIVO")
                    {
                        lblTitleConfirm.Text = "Desactivar Compañía de Seguros";
                        lblmsgConfirm.Text = "¿Desea desactivar este tipo de seguro: " + grvBancos.Rows[fila].Cells[1].Text + " ?";
                    }
                    //else if (lblestado.Text == "NO ACTIVO")
                    //{
                    //    lblTitleConfirm.Text = "Activar Compañía de Seguros";
                    //    lblmsgConfirm.Text = "¿Desea activar este tipo de seguro: " + grvBancos.Rows[fila].Cells[1].Text + " ?";
                    //}
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "openConfirm();", true);
                }
            }

            if (e.CommandName == "ActivaTipoSeguro")
            {
                string id = grvBancos.Rows[fila].Cells[0].Text;
                hdnidTipoSeguro.Value = id;
                Label lblestado = (Label)grvBancos.Rows[fila].FindControl("lblestado");
                hdnEstado.Value = lblestado.Text;
                using (TipoProductoDAO db = new TipoProductoDAO())
                {
                       lblTitleConfirm.Text = "Activar Compañía de Seguros";
                       lblmsgConfirm.Text = "¿Desea activar este tipo de seguro: " + grvBancos.Rows[fila].Cells[1].Text + " ?";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "openConfirm();", true);
                }
            }
        }

        protected void btnRegistrarContact_Click1(object sender, EventArgs e)
        {
            using (TipoProductoDAO db = new TipoProductoDAO())
            {
                //Session["idSeguro"] = 0;
                Producto producto = new Producto();
                producto.ID =Convert.ToInt32(Session["idSeguro"]);
                producto.Nombre = TxtNombre.Text;
                producto.Descripcion = TxtDescripcion.Text;
                producto.Estado = true;
                producto.FechaActualizacion = DateTime.Now.Date;
                producto.UsuarioActualizacion = "";                

                db.Agregar(producto);
                CargarTipoBancos();

                txtmensaje.Text = "Tipo de seguro:" + producto.Nombre +  " fue registrado con exito";
                string jss = "openModal()";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
            }
        }

        protected void grvBancos_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            grvBancos.PageIndex = e.NewPageIndex;
            CargarTipoBancos();
        }

        protected void btnCancelar_Click1(object sender, EventArgs e)
        {
            TxtNombre.Text = "";
            TxtDescripcion.Text = "";
            MVTipoBancos.ActiveViewIndex = 0;
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            using (TipoSeguro_DAO db = new TipoSeguro_DAO())
            {
                //if (hdnEstado.Value=="ACTIVO")
                //{
                    int id = Convert.ToInt32(hdnidTipoSeguro.Value);
                    db.Anular(id, hdnEstado.Value.ToUpper());
                    CargarTipoBancos();
           //     }                
            }
        }
    }
    }










