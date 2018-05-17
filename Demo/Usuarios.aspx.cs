using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAO_Hermes;
using DAO_Hermes.Repositorios;

namespace Demo
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                CargaListaUsuario();
                cUtil.ListarTipoDocumentos(DDLTipoDocumento);
                cUtil.ListarRoles(DDLRol);
            }
        }


        void CargaListaUsuario()
        {
            using (Usuario_DAO db = new Usuario_DAO())
            {
                grvUsuarios.DataSource = db.ListarUsuarios();
                grvUsuarios.DataBind();
            }
        }

        protected void grvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvUsuarios.PageIndex = e.NewPageIndex;
            CargaListaUsuario();
        }

        protected void grvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int fila = Convert.ToInt32(e.CommandArgument);
            
            if (e.CommandName == "Edita")
            {
                //Session["idUsuario"] = grvUsuarios.Rows[fila].Cells[0].Text;
                
                txtRazonSocial.Text = grvUsuarios.Rows[fila].Cells[8].Text;
                txtApellidoPaterno.Text = grvUsuarios.Rows[fila].Cells[9].Text;
                txtApellidoMaterno.Text = grvUsuarios.Rows[fila].Cells[10].Text;
                txtNroDocumento.Text= grvUsuarios.Rows[fila].Cells[2].Text;
                DDLRol.SelectedValue = grvUsuarios.Rows[fila].Cells[11].Text;
                txtEmail.Text = grvUsuarios.Rows[fila].Cells[3].Text;
                pkUsuario.Value = grvUsuarios.Rows[fila].Cells[12].Text;

                string jss = "openEditarUsuario();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
            }
        }

        protected void btnEditarUsuario_Click(object sender, EventArgs e)
        {
            using (Usuario_DAO db = new Usuario_DAO())
            {
                Users usu = new Users();
                usu.Id = pkUsuario.Value;
                //Convert.ToString(Session["idUsuario"]);
                usu.Nombre = txtRazonSocial.Text;
                usu.ApellidoPaterno = txtApellidoPaterno.Text;
                usu.ApellidoMaterno = txtApellidoMaterno.Text;
                usu.Email = txtEmail.Text;
                usu.NumeroDocumento = txtNroDocumento.Text;

                //pkUsuario.Value 
                db.EditarUser(usu,DDLRol.SelectedValue);
                CargaListaUsuario();
                string jss = "openModal()";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            MVUsuarios.ActiveViewIndex = 0;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            using (Usuario_DAO db = new Usuario_DAO())
            {
                grvUsuarios.DataSource = db.LISTARUSERS_X_NOMBRE(txtBusqueda.Text);
                grvUsuarios.DataBind();
            }
        }
    }
}