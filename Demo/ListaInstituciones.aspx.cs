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
    public partial class ListaInstituciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                CargarInstituciones();
                cUtil.ListarTipoDocumentosTodos(DDLTipoDocumento);
                cUtil.ListarUbigeo(DDLDepartamento, "01", "00", "00");
                cUtil.ListarTipoInstitucion(DDLTipoInstitucionEducativa);
            }
        }

        void CargarInstituciones()
        {
            using (InstitucionEducativaDAO db = new InstitucionEducativaDAO())
            {
                grvSeguros.DataSource = db.ListarInstituciones(3);
                grvSeguros.DataBind();
            }
        }

        void CargarInstitucionesFiltro(int id, string nombre)
        {
            using (InstitucionEducativaDAO db = new InstitucionEducativaDAO())
            {
                grvSeguros.DataSource = db.ListarInstitucionesXNombre(nombre, 3);
                grvSeguros.DataBind();
            }
        }
        protected void grvSeguros_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvSeguros.PageIndex = e.NewPageIndex;
            CargarInstituciones();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarInstitucionesFiltro(3, txtBusqueda.Text);
        }

        //protected override void Render(System.Web.UI.HtmlTextWriter writer)
        //{
        //    foreach (GridViewRow row in grvSeguros.Rows)
        //    {
        //        if (row.RowType == DataControlRowType.DataRow)
        //        {
        //            row.Attributes["onmouseover"] = "this.style.backgroundColor='#FF4000';this.style.color='white';";
        //            row.Attributes["onmouseout"] = "this.style.backgroundColor='#FFFFFF';this.style.color='';";
        //            row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(grvSeguros, "Select$" + row.DataItemIndex, true);
        //        }
        //    }

        //    base.Render(writer);
        //}

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            int X = 1;
            Session["idEducativa"] = 0;
            limpiar();
            MVInstituciones.ActiveViewIndex = 1;
            //    cUtil.ListarUbigeo(DDLDepartamento, "01", "00", "00");
            
        }

        protected void DDLProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            cUtil.ListarUbigeo(DDLDistrito, DDLDepartamento.SelectedValue.Substring(0, 2), DDLProvincia.SelectedValue.Substring(2, 2), "01");
        }

        protected void DDLDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            cUtil.ListarUbigeo(DDLProvincia, DDLDepartamento.SelectedValue.Substring(0, 2), "01", "00");
            cUtil.ListarUbigeo(DDLDistrito, DDLDepartamento.SelectedValue.Substring(0, 2), DDLProvincia.SelectedValue.Substring(2, 2), "01");
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            using ( InstitucionEducativaDAO db = new InstitucionEducativaDAO())
            {
                try
                {
                    InstitucionEducativa inst = new InstitucionEducativa();
                    inst.ID = Convert.ToInt32(Session["idEducativa"]);
                    //inst.ID = Convert.ToInt32(2);
                    inst.Activo = true;
                    inst.Codigo = txtCodigoAfiliacion.Text;
                    inst.ApellidoMaterno = txtApellidoMaterno.Text;
                    inst.ApellidoPaternno = txtApellidoPaterno.Text;
                    inst.TipoDocumentoID = Convert.ToInt32(DDLTipoDocumento.SelectedValue);
                    inst.NumeroDocumento = txtNumeroDocumento.Text;
                    inst.FechaCreacion = DateTime.Now;
                    inst.Nombre = txtNombre.Text;
                    inst.RazonSocial = txtRazonSocial.Text;
                    inst.Telefono = txtTelefono.Text;
                    inst.TipoInstitucionEducativaID = Convert.ToInt32(DDLTipoInstitucionEducativa.SelectedValue);
                    if (rbtTipoEmpresa.SelectedIndex == 0)
                    {
                        inst.NombreNatural = txtnombrocorto.Text;
                        inst.TipoEmpresa = 1;
                    }
                    if (rbtTipoEmpresa.SelectedIndex == 1)
                    {
                        inst.NombreNatural = txtApellidoPaterno.Text + " " + txtApellidoMaterno.Text + " " + txtNombre.Text;
                        inst.TipoEmpresa = 2;
                    }
                    inst.Fax = txtFAX.Text;
                    inst.Activo = true;
                    inst.CodigoUbigeo = DDLDepartamento.SelectedValue.Substring(0, 2) + DDLProvincia.SelectedValue.Substring(2, 2) + DDLDistrito.SelectedValue.Substring(4, 2);
                    inst.Direccion = txtDireccion.Text;

                    db.AgregarInstitucionEducativa(inst);
                    CargarInstituciones();
                    txtmensaje.Text = "¡La institución educativa : " + txtnombrocorto.Text + " fue registrada con exito!";
                    string jss = "openModal()";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
                    MVInstituciones.ActiveViewIndex = 0;
                }
                catch (Exception ex)
                {
                    lblIE.Text = ex.Message;
                    string jssIE = "openModalIE()";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssIE, true);
                }

            }
        }
        void limpiar()
        {
            txtCodigoAfiliacion.Text = "";
            txtApellidoMaterno.Text = "";
            txtApellidoPaterno.Text = "";
            DDLTipoDocumento.SelectedIndex = -1;
            txtNumeroDocumento.Text = "";
            txtNombre.Text = "";
            txtnombrocorto.Text = "";
            txtRazonSocial.Text = "";
            txtTelefono.Text = "";
            DDLTipoInstitucionEducativa.SelectedIndex = -1;
            DDLDepartamento.SelectedIndex = -1;
            DDLProvincia.SelectedIndex = -1;
            DDLProvincia.SelectedIndex = -1;
            txtFAX.Text = "";
            txtDireccion.Text = "";
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            MVInstituciones.ActiveViewIndex = 0;
        }

        protected void grvSeguros_RowCommand(object sender, GridViewCommandEventArgs e)
        {            

            if (e.CommandName == "Edita")
            {
                int fila = Convert.ToInt32(e.CommandArgument);
                int id = Convert.ToInt32(grvSeguros.Rows[fila].Cells[10].Text);
                using (InstitucionEducativaDAO db = new InstitucionEducativaDAO())
                {
                    cUtil.ListarUbigeo(DDLDepartamento, "01", "00", "00");
                    InstitucionEducativa ie = db.Buscar(id);

                    Session["idEducativa"] = ie.ID.ToString();
                    txtCodigoAfiliacion.Text = ie.Codigo;
                    txtRazonSocial.Text = ie.RazonSocial;
                    txtNombre.Text = ie.Nombre;
                    DDLTipoInstitucionEducativa.SelectedValue = ie.TipoInstitucionEducativaID.ToString();
                    txtNumeroDocumento.Text = ie.NumeroDocumento;
                    txtDireccion.Text = ie.Direccion;
                    txtApellidoPaterno.Text = ie.ApellidoPaternno;
                    txtApellidoMaterno.Text = ie.ApellidoMaterno;
                    txtTelefono.Text = ie.Telefono;
                    txtFAX.Text = ie.Fax;
                    txtnombrocorto.Text = ie.NombreNatural;
                    DDLDepartamento.SelectedValue = ie.CodigoUbigeo.Substring(0, 2) + "0000";
                    DDLDepartamento_SelectedIndexChanged(null, null);
                    DDLProvincia.SelectedValue = ie.CodigoUbigeo.Substring(0, 2) + ie.CodigoUbigeo.Substring(2, 2) + "00";
                    DDLProvincia_SelectedIndexChanged(null, null);
                    DDLDistrito.SelectedValue = ie.CodigoUbigeo.Substring(0, 2) + ie.CodigoUbigeo.Substring(2, 2) + ie.CodigoUbigeo.Substring(4, 2);
                    DDLTipoDocumento.SelectedValue = ie.TipoDocumentoID.ToString();
                    MVInstituciones.ActiveViewIndex = 1;
                }
            }

            if (e.CommandName == "Anula")
            {
                
                int fila = Convert.ToInt32(e.CommandArgument);
                int id = Convert.ToInt32(grvSeguros.Rows[fila].Cells[10].Text);

                using (InstitucionEducativaDAO db = new InstitucionEducativaDAO())
                {
                    hdnIdAsociacion.Value=id.ToString() ;
                    lblmsgConfirm.Text = "¿Está seguro que desea anular la institucion educativa: "+ grvSeguros.Rows[fila].Cells[3].Text +" ?";
                    lblTitleConfirm.Text = "Eliminar institución educativa";   
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "openConfirm();", true);
                  
                }
            }

            if (e.CommandName == "Activa")
            {
                int fila = Convert.ToInt32(e.CommandArgument);
                int id = Convert.ToInt32(grvSeguros.Rows[fila].Cells[10].Text);
                using (InstitucionEducativaDAO db = new InstitucionEducativaDAO())
                {
                    db.Activar(id);
                    CargarInstituciones();
                }
            }
            if (e.CommandName == "AgregarContacto")
            {
                int fila = Convert.ToInt32(e.CommandArgument);
                int id = Convert.ToInt32(grvSeguros.Rows[fila].Cells[10].Text);
                using (InstitucionEducativaDAO db = new InstitucionEducativaDAO())
                {
                    Session["idEducativa"] = id;
                    string jss = "openEditarAlumno();";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
                }
            }
            if (e.CommandName == "ConsultarContacto")
            {
                
            }
        }
         protected void btnRegistrarContact_Click(object sender, EventArgs e)
        {
          
        }

        
        protected void btnVer_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "ver();", true);
        }

        protected void grvSeguros_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            int id =Convert.ToInt32( e.Row.Cells[10].Text);
            GridView gvDetails = (GridView)e.Row.FindControl("grvContactos");
            cargarContactos(gvDetails, id);
        }

        void cargarContactos(GridView grv, int id)
        {
            ContactoDAO db = new ContactoDAO();
            grv.DataSource = db.ListarContactosxInstitucion(id);
            grv.DataBind();
        }

        protected void grvContactos_RowCommand(object sender, GridViewCommandEventArgs e)
        {                       
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

            //int id =Convert.ToInt32( e.CommandArgument);

            if (e.CommandName== "EditaContacto")
                {
                Session["idContacto"]= row.Cells[0].Text;
                txtContCargoEdit.Text = row.Cells[2].Text;
                txtContEmailEdit.Text = row.Cells[3].Text;
                TxtContNombresEdit.Text = row.Cells[4].Text;
                TxtContApePateEdit.Text = row.Cells[5].Text;
                TxtContApeMateEdit.Text = row.Cells[6].Text;
                txtInstEduID.Text= row.Cells[7].Text;


                string jss = "openEditarContacto();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
               }

            if (e.CommandName == "AnulaContacto")
            {
            
                using (ContactoDAO db = new ContactoDAO())
                {
                    try
                    {
                        int id = Convert.ToInt32(row.Cells[0].Text);
                        if (id > 0)
                        {
                            db.Anular(id);
                            CargarInstituciones();
                            txtmensaje.Text = "¡El contacto : " + row.Cells[1].Text + " fue anulado con exito!";
                            string jss = "openModal()";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
                        }
                    }
                    catch(Exception ex)
                    {
                        txtmensaje.Text = ex.Message;
                        string jss = "openModal()";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
                    }
                }
            }
        }

        protected void btnEditaContacto_Click(object sender, EventArgs e)
        {
            using (ContactoDAO  db = new ContactoDAO())
                {
                Contacto contacto = new Contacto();
                contacto.ID =Convert.ToInt32( Session["idContacto"]);
                contacto.ApellidoMaterno = TxtContApeMateEdit.Text;
                contacto.ApellidoPaterno = TxtContApePateEdit.Text;
                contacto.Nombre = TxtContNombresEdit.Text;
                contacto.Email = txtContEmailEdit.Text;
                contacto.Cargo = txtContCargoEdit.Text;
                contacto.InstitucionEducativaID =Convert.ToInt32( txtInstEduID.Text);
                db.Agregar(contacto);
                CargarInstituciones();
                txtmensaje.Text = "Contacto:" + TxtNombresContact.Text + " " + TxtApePateContact.Text + " " + TxtApeMateContact.Text + " fue actualizado con exito";
                string jss = "openModal()";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
            }
        }

        protected void btnRegistrarContact_Click1(object sender, EventArgs e)
        {
            using (ContactoDAO db = new ContactoDAO())
            {
                Contacto contacto = new Contacto();
                contacto.ApellidoPaterno = TxtApePateContact.Text;
                contacto.ApellidoMaterno = TxtApeMateContact.Text;
                contacto.Nombre = TxtNombresContact.Text;
                contacto.Email = txtEmailContact.Text;
                contacto.Cargo = txtCargoContact.Text;
                contacto.Estado = true;
                contacto.FechaCreacion = DateTime.Now;
                contacto.InstitucionEducativaID = Convert.ToInt32(Session["idEducativa"]);

                db.Agregar(contacto);
                CargarInstituciones();
                txtmensaje.Text = "Contacto:" + TxtNombresContact.Text + " " + TxtApePateContact.Text + " " + TxtApeMateContact.Text + "fue registrado con exito";
                string jss = "openModal()";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            using (InstitucionEducativaDAO db = new InstitucionEducativaDAO())
            {
                
                db.Anular(Convert.ToInt32(  hdnIdAsociacion.Value ));
                CargarInstituciones();
            }
        }

        protected void btnEditar_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void grvSeguros_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}









