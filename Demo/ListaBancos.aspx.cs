using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAO_Hermes.Repositorios;
using DAO_Hermes;
using System.IO;


namespace Demo
{
    public partial class ListaBancos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                inicializar();
            }
        }
        
        void inicializar()
        {
            CargarBancos();
            cUtil.ListarUbigeo(DDLDepartamento, "01", "00", "00");
            cUtil.ListarMonedas(ddlMoneda);
            cUtil.ListarCiaSeguros(ddlEmpresaRecaudadora);
        }

        void CargarBancos()
        {
            using (BancoDAO db = new BancoDAO())
            {
                grvBancos.DataSource = db.ListarBancos();
                grvBancos.DataBind();
            }
        }

        void CargarInstitucionesFiltro(int id, string nombre)
        {
            using (InstitucionEducativaDAO db = new InstitucionEducativaDAO())
            {
                grvBancos.DataSource = db.ListarInstitucionesXNombre(nombre, 3);
                grvBancos.DataBind();
            }
        }
        protected void grvBancos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //grvBancos.PageIndex = e.NewPageIndex;
            //CargarInstituciones();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            using (BancoDAO db = new BancoDAO())
            {
                grvBancos.DataSource = db.ListaBancosNombre(txtBusqueda.Text);
                grvBancos.DataBind();
            }
        }

        //protected override void Render(System.Web.UI.HtmlTextWriter writer)
        //{
        //    foreach (GridViewRow row in grvBancos.Rows)
        //    {
        //        if (row.RowType == DataControlRowType.DataRow)
        //        {
        //            row.Attributes["onmouseover"] = "this.style.backgroundColor='#FF4000';this.style.color='white';";
        //            row.Attributes["onmouseout"] = "this.style.backgroundColor='#FFFFFF';this.style.color='';";
        //            row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(grvBancos, "Select$" + row.DataItemIndex, true);
        //        }
        //    }

        //    base.Render(writer);
        //}

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            string jss = "openEditarBanco();";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
        }
        void LimpiarCuentas()
        {
            lblParametro1.Text = "Cod. Concepto S/.";
            txtParametro1.Text = "";
            lblParametro2.Text = "Clave";
            txtParametro2.Text = "";
            lblParametro3.Text = "Nro. Cuenta S/.";
            txtParametro3.Text = "";
            lblParametro4.Text = "Cod Secuencia S/.";
            txtParametro4.Text = "";
            rbtPrederminado.ClearSelection();
            TxtBanco.Text = "";
            hdnId.Value = "";
        }
        void LimpiarContacto()
        {
            hdfidContacto.Value = "0";
            TxtapellidoMaterno.Text = "";
            TxtapellidoPaterno.Text = "";
            TxtNombres.Text = "";
            TxtCorreoElectronico.Text = "";
            TxtCargo.Text = "";
            Session["idContacto"] = null;
            txtmensaje.Text = "";
        }
        void Limpiar()
        {
            TxtCodigoBanco.Text = "0";
            TxtNombreCorto.Text = "";
            TxtRazonSocial.Text = "";
            DDLDepartamento.SelectedIndex = -1;
            DDLProvincia.SelectedIndex = -1;
            DDLDistrito.SelectedIndex = -1;
            txtRUC.Text = "";
            chkUsadoProcAfiliación.Checked = false;
            txtDireccion.Text = "";
            img_destino2.ImageUrl = "";
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            using (InstitucionEducativaDAO db = new InstitucionEducativaDAO())
            {
                InstitucionEducativa inst = new InstitucionEducativa();
                inst.ID = Convert.ToInt32(Session["idEducativa"]);
                inst.Activo = true;
                //    inst.Codigo = txtCodigoAfiliacion.Text;
                //    inst.ApellidoMaterno = txtApellidoMaterno.Text;
                //    inst.ApellidoPaternno = txtApellidoPaterno.Text;
                //    inst.TipoDocumentoID = Convert.ToInt32(DDLTipoDocumento.SelectedValue);
                //    inst.NumeroDocumento = txtNumeroDocumento.Text;
                //    inst.FechaCreacion = DateTime.Now;
                //    inst.Nombre = txtNombre.Text;
                //    inst.NombreNatural = txtnombrocorto.Text;
                //    inst.RazonSocial = txtRazonSocial.Text;
                //    inst.Telefono = txtTelefono.Text;
                //    inst.TipoInstitucionEducativaID = Convert.ToInt32(DDLTipoInstitucionEducativa.SelectedValue);
                //    if (rbtTipoEmpresa.SelectedIndex == 0)
                //    {
                //        inst.TipoEmpresa = 1;
                //    }
                //    if (rbtTipoEmpresa.SelectedIndex == 1)
                //    {
                //        inst.TipoEmpresa = 2;
                //    }
                //    inst.Fax = txtFAX.Text;
                //    inst.Activo = true;
                //    inst.CodigoUbigeo = DDLDepartamento.SelectedValue.Substring(0, 2) + DDLProvincia.SelectedValue.Substring(2, 2) + DDLDistrito.SelectedValue.Substring(4, 2);
                //    inst.Direccion = txtDireccion.Text;

                //    db.AgregarInstitucionEducativa(inst);
                //    txtmensaje.Text = "¡La institución educativa : " + txtnombrocorto.Text + " fue registrada con exito!";
                //    string jss = "openModal()";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            MVTipoBancos.ActiveViewIndex = 0;
        }

        protected void grvBancos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int fila = Convert.ToInt32(e.CommandArgument);
                string jss = "";

                if (e.CommandName == "EditaBanco")
                {
                    BancoDAO db = new BancoDAO();
                    TxtCodigoBanco.Text = grvBancos.Rows[fila].Cells[1].Text;
                    TxtRazonSocial.Text = grvBancos.Rows[fila].Cells[2].Text;
                    txtRUC.Text = grvBancos.Rows[fila].Cells[3].Text;
                    TxtNombreCorto.Text = grvBancos.Rows[fila].Cells[6].Text;
                    txtDireccion.Text = grvBancos.Rows[fila].Cells[8].Text;
                    string subigeo = grvBancos.Rows[fila].Cells[7].Text;
                    chkUsadoProcAfiliación.Checked = Convert.ToBoolean(grvBancos.Rows[fila].Cells[9].Text);
                    DDLDepartamento.SelectedValue = subigeo.Substring(0, 2) + "0000";
                    DDLDepartamento_SelectedIndexChanged(null, null);
                    DDLProvincia.SelectedValue = subigeo.Substring(0, 2) + subigeo.Substring(2, 2) + "00";
                    DDLProvincia_SelectedIndexChanged(null, null);
                    DDLDistrito.SelectedValue = subigeo.Substring(0, 2) + subigeo.Substring(2, 2) + subigeo.Substring(4, 2);
                    byte[] imagen = db.ObtenerImagenLogo(Convert.ToInt32(grvBancos.Rows[fila].Cells[1].Text));
                    if (imagen != null)
                    {
                        img_destino2.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(imagen);
                    }
                    jss = "openEditarBanco();";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
                }

                if (e.CommandName == "AnulaBanco")
                {
                    int id = Convert.ToInt32(grvBancos.Rows[fila].Cells[1].Text);
                    using (BancoDAO db = new BancoDAO())
                    {
                        db.Anular(id);
                        CargarBancos();
                    }
                }

                if (e.CommandName == "ActivaBanco")
                {
                    int id = Convert.ToInt32(grvBancos.Rows[fila].Cells[1].Text);
                    using (BancoDAO db = new BancoDAO())
                    {
                        db.Activar(id);
                        CargarBancos();
                    }
                }

                if (e.CommandName == "AgregarCuentas")
                {
                    LimpiarCuentas();
                    TxtBanco.Text = grvBancos.Rows[fila].Cells[2].Text;
                    hdnId.Value = grvBancos.Rows[fila].Cells[1].Text;

                    //System.Data.DataSet ds = new System.Data.DataSet();
                    //using (BancoDAO db = new BancoDAO())
                    //{
                    //    ds = db.ListaPropiedadesBanco(Convert.ToInt32(hdnId.Value), 1);
                    //    if (ds.Tables[0].Rows.Count > 0)
                    //    {

                    //        lblParametro1.Text = Convert.ToString(ds.Tables[0].Rows[0][1]);
                    //        txtParametro1.Text = Convert.ToString(ds.Tables[0].Rows[0][2]);

                    //        lblParametro2.Text = Convert.ToString(ds.Tables[0].Rows[1][1]);
                    //        txtParametro2.Text = Convert.ToString(ds.Tables[0].Rows[1][2]);

                    //        lblParametro3.Text = Convert.ToString(ds.Tables[0].Rows[2][1]);
                    //        txtParametro3.Text = Convert.ToString(ds.Tables[0].Rows[2][2]);

                    //        lblParametro4.Text = Convert.ToString(ds.Tables[0].Rows[3][1]);
                    //        txtParametro4.Text = Convert.ToString(ds.Tables[0].Rows[3][2]);

                    //    }
                    //}

                    string jss2 = "openModalCuentas()";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                }

                if (e.CommandName == "AgregarContacto")
                {
                    LimpiarContacto();
                    fila = Convert.ToInt32(e.CommandArgument);
                    int id = Convert.ToInt32(grvBancos.Rows[fila].Cells[1].Text);

                    Session["idBanco"] = id;
                    hdfidContacto.Value = "0";
                    jss = "openEditarContacto();";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);

                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void grvContactos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                Session["idContacto"] = null;
                //int id =Convert.ToInt32( e.CommandArgument);

                if (e.CommandName == "EditaContacto")
                {
                    LimpiarContacto();
                    Session["idBanco"] = row.Cells[7].Text;
                    hdfidContacto.Value = row.Cells[0].Text;
                    TxtCargo.Text = row.Cells[2].Text;
                    TxtCorreoElectronico.Text = row.Cells[3].Text;
                    TxtNombres.Text = row.Cells[4].Text;
                    TxtapellidoPaterno.Text = row.Cells[5].Text;
                    TxtapellidoMaterno.Text = row.Cells[6].Text;

                    string jss = "openEditarContacto();";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
                }

                if (e.CommandName == "AnulaContacto")
                {
                    using (ContactoDAO db = new ContactoDAO())
                    {
                        int id = Convert.ToInt32(row.Cells[0].Text);
                        if (id > 0)
                        {
                            db.Anular(id);
                            CargarBancos();
                            txtmensaje.Text = "¡El contacto : " + row.Cells[1].Text + " fue anulado con exito!";
                            string jss = "openModal()";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
                        }


                    }
                }
                if (e.CommandName == "ActivaContacto")
                {
                    int id = Convert.ToInt32(row.Cells[0].Text);
                    using (ContactoDAO db = new ContactoDAO())
                    {
                        if (id > 0)
                        {
                            db.Activar(id);
                            CargarBancos();
                            txtmensaje.Text = "¡El contacto : " + row.Cells[1].Text + " fue activado con exito!";
                            string jss = "openModal()";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                txtmensaje.Text = ex.Message;
                string jss = "openModal()";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
            }
        }


        protected void btnVer_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "ver();", true);
        }

        protected void grvBancos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            int id = Convert.ToInt32(e.Row.Cells[1].Text);
            GridView gvDetails = (GridView)e.Row.FindControl("grvContactos");
            cargarContactosBanco(gvDetails, id);

            GridView gvCuentas = (GridView)e.Row.FindControl("grvCuentas");
            cargarCuentasBanco(gvCuentas, id);

        }

        protected void btnEditaContacto_Click(object sender, EventArgs e)
        {

            using (ContactoDAO db = new ContactoDAO())
            {
                Contacto contacto = new Contacto();

                contacto.BancoID = Convert.ToInt32(Session["idBanco"]);
                contacto.ApellidoMaterno = TxtapellidoMaterno.Text;
                contacto.ApellidoPaterno = TxtapellidoPaterno.Text;
                contacto.Nombre = TxtNombres.Text;
                contacto.Email = TxtCorreoElectronico.Text;
                contacto.Cargo = TxtCargo.Text;
                contacto.Estado = true;
                if (hdfidContacto.Value != "0")
                {
                    contacto.ID = Convert.ToInt32(Session["idContacto"]);
                    txtmensaje.Text = "Contacto:" + contacto.Nombre + " " + contacto.ApellidoPaterno + " " + contacto.ApellidoMaterno + " fue actualizado con exito";
                }
                else
                {
                    txtmensaje.Text = "Contacto:" + contacto.Nombre + " " + contacto.ApellidoPaterno + " " + contacto.ApellidoMaterno + " fue registrado con exito";
                }
                db.Agregar(contacto);

                CargarBancos();


                string jss = "openModal()";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);

            }
        }



        protected void btnRegistrarBanco_Click1(object sender, EventArgs e)
        {
            try
            {
                using (BancoDAO db = new BancoDAO())
                {
                    Banco banco = new Banco();
                    banco.ID = Convert.ToInt32(TxtCodigoBanco.Text);
                    banco.Nombre = TxtRazonSocial.Text;
                    banco.NombreCorto = TxtNombreCorto.Text;
                    banco.RUC = txtRUC.Text;
                    banco.Direccion = txtDireccion.Text;
                    banco.ProcesoAfiliacion = chkUsadoProcAfiliación.Checked;
                    banco.CodigoUbigeo = DDLDepartamento.SelectedValue.Substring(0, 2) + DDLProvincia.SelectedValue.Substring(2, 2) + DDLDistrito.SelectedValue.Substring(4, 2);

                    ////////////////////////////////////////

                    string filePath = file_url.PostedFile.FileName;
                    string filename = Path.GetFileName(filePath);
                    string ext = Path.GetExtension(filename);
                    string contenttype = String.Empty;

                    //Set the contenttype based on File Extension
                    switch (ext.ToLower())
                    {
                        case ".jpg":
                            contenttype = "image/jpg";
                            break;
                        case ".png":
                            contenttype = "image/png";
                            break;
                        case ".gif":
                            contenttype = "image/gif";
                            break;
                    }
                    if (contenttype != String.Empty)
                    {
                        Stream fs = file_url.PostedFile.InputStream;
                        BinaryReader br = new BinaryReader(fs);
                        Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        //insert the file into database
                        banco.DatoAdj = bytes;
                    }
                    else
                    {
                        txtmensaje.Text = "File format not recognised. Upload Image formats";
                        string jss2 = "openModal()";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                    }

                    banco.TipoContenidoAdj = "";
                    banco.NombreAdj = "";
                    banco.Estado = true;
                    banco.FechaCreacion = DateTime.Now.Date;
                    banco.UsuarioCreacion = "";

                    db.Agregar(banco);
                    CargarBancos();
                    txtmensaje.Text = "El banco: " + banco.Nombre + " fue registrado con exito";
                    string jss = "openModal()";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void DDLDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            cUtil.ListarUbigeo(DDLProvincia, DDLDepartamento.SelectedValue.Substring(0, 2), "01", "00");
            cUtil.ListarUbigeo(DDLDistrito, DDLDepartamento.SelectedValue.Substring(0, 2), DDLProvincia.SelectedValue.Substring(2, 2), "01");
            //string jss = "openEditarBanco();";
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
        }

        protected void DDLProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            cUtil.ListarUbigeo(DDLDistrito, DDLDepartamento.SelectedValue.Substring(0, 2), DDLProvincia.SelectedValue.Substring(2, 2), "01");
            //string jss = "openEditarBanco();";
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
        }
        void cargarContactosBanco(GridView grv, int id)
        {
            ContactoDAO db = new ContactoDAO();
            grv.DataSource = db.ListarContactosxBanco(id);
            grv.DataBind();
        }

        void cargarCuentasBanco(GridView grv, int id)
        {
            BancoDAO db = new BancoDAO();
            grv.DataSource = db.ListarCuentasBanco(id);
            grv.DataBind();
        }

        protected void btnGuardar_Command(object sender, CommandEventArgs e)
        {
            using (CuentaDAO db = new CuentaDAO())
            {
                Cuenta cuenta = new Cuenta();
                cuenta.ID = Convert.ToInt32(hdnId.Value);
                cuenta.BancoID = Convert.ToInt32(hdnIdBanco.Value);
                cuenta.CodigoCIASeguro = Convert.ToInt32(ddlEmpresaRecaudadora.SelectedValue);
                cuenta.FechaCreacion = DateTime.Now;
                cuenta.Numero = txtNumero.Text;
                cuenta.TipoMonedaID = Convert.ToInt32(ddlMoneda.SelectedValue);
                cuenta.UsuarioCreacion = "";
                cuenta.Estado = true;
                cuenta.Predeterminado = rbtPrederminado.SelectedItem.Value == "1" ? true : false;

                db.AgregarCuenta(cuenta);
            }
        }

        protected void grvCuentas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //int fila =Convert.ToInt32( e.CommandArgument);
            //GridView gridPadre = (GridView)sender;                        
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            if (e.CommandName == "EditarCuenta")
            {
                hdnId.Value = row.Cells[0].Text;
                TxtBanco.Text = row.Cells[5].Text;

                System.Data.DataSet ds = new System.Data.DataSet();
                using (BancoDAO db = new BancoDAO())
                {
                    CuentaDAO dtadb = new CuentaDAO();
                    Cuenta cta = dtadb.BuscarCuenta(Convert.ToInt32(hdnId.Value));
                    hdnIdBanco.Value = row.Cells[4].Text;
                    ddlMoneda.SelectedValue = Convert.ToString(cta.TipoMonedaID);
                    ddlEmpresaRecaudadora.SelectedValue = Convert.ToString(cta.CodigoCIASeguro);
                    txtNumero.Text = cta.Numero;
                    rbtPrederminado.SelectedItem.Value = Convert.ToString(cta.Predeterminado == true ? 1 : 0);

                    ds = db.ListaPropiedadesBanco(Convert.ToInt32(cta.BancoID), Convert.ToInt32(cta.TipoMonedaID));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblParametro1.Text = Convert.ToString(ds.Tables[0].Rows[0][1]);
                        txtParametro1.Text = Convert.ToString(ds.Tables[0].Rows[0][2]);

                        lblParametro2.Text = Convert.ToString(ds.Tables[0].Rows[1][1]);
                        txtParametro2.Text = Convert.ToString(ds.Tables[0].Rows[1][2]);

                        lblParametro3.Text = Convert.ToString(ds.Tables[0].Rows[2][1]);
                        txtParametro3.Text = Convert.ToString(ds.Tables[0].Rows[2][2]);

                        lblParametro4.Text = Convert.ToString(ds.Tables[0].Rows[3][1]);
                        txtParametro4.Text = Convert.ToString(ds.Tables[0].Rows[3][2]);

                        string jss2 = "openModalCuentas()";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                    }
                }
            }
            if (e.CommandName == "AnularCuenta")
            {
                using (CuentaDAO db = new CuentaDAO())
                {
                    try
                    {
                        hdnId.Value = row.Cells[0].Text;
                        db.AnularCuenta(Convert.ToInt32(hdnId.Value));
                        CargarBancos();
                        txtmensaje.Text = "Cuenta eliminada con exito";
                        string jss2 = "openModal()";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                    }
                    catch (Exception ex)
                    {
                        txtmensaje.Text = ex.Message;
                        string jss2 = "openModal()";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);

                    }
                }
            }

        }

        protected void btnEditar_Click(object sender, ImageClickEventArgs e)
        {

        }
        
    }
}












