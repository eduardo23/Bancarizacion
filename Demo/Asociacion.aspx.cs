using DAO_Hermes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAO_Hermes.Repositorios;
using DAO_Hermes;
using System.IO;
using System.Data.SqlClient;

namespace Demo
{
    public partial class Asociacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                CargarAsociacion();
                CargarTipoInstitucionEducativa();
                CargarInstitucionEducativa();
                CargarSeguros();
                CargarTipoSeguros();
                CargarBancos();
                cUtil. ListarRecaudadores(ddlEmpresaRecaudadora);
                ScriptManager.RegisterClientScriptBlock(this.updResumenes,   updResumenes.GetType(),    "formulas",  "formulas",  true);
            }
        }

        void CargarBancos()
        {
            using (BancoDAO dbBanco = new BancoDAO())
                {
                grvBancos.DataSource = dbBanco.ListarBanco();
                grvBancos.DataBind();
                }
        }

        //Service Autocomplete
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchClientes(string prefixText, int count, string contextKey)
     {
            string sSql = "";
            Char delimiter = '|';
            string tipoInstEdu = "";
            int iteracion = 0;
            string[] substrings = contextKey.Split(delimiter);
            foreach (var substring in substrings)
            {
                if (iteracion == 0)
                {
                    tipoInstEdu = substring;
                    iteracion++;
                }
            }

            using (SqlConnection conn = new SqlConnection())
            {
            //conn.ConnectionString = @"Data Source=191.98.187.194;Initial Catalog=BDHermesBancarizacionTesting2;Persist Security Info=True;User id=bancarizacionuser;password=h3rm3sb4nk42016$#";
            conn.ConnectionString = ConexionDAO.cnx; 
                using (SqlCommand cmd = new SqlCommand())
                {

                    if (tipoInstEdu == "0")
                    {
                        sSql = "select ID,NombreNatural from Maestro.InstitucionEducativa where activo=1 AND NombreNatural like @SearchText + '%'";
                        cmd.CommandText = sSql;
                        cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    }
                    else
                    {
                        sSql = "select ID,NombreNatural from Maestro.InstitucionEducativa where activo=1 AND TipoInstitucionEducativaID= @TipoInstEduca AND NombreNatural like @SearchText + '%'"; ;
                        cmd.CommandText = sSql;
                        cmd.Parameters.AddWithValue("@TipoInstEduca", tipoInstEdu);
                        cmd.Parameters.AddWithValue("@SearchText", prefixText);                        
                    }                    
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> c = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            c.Add(AjaxControlToolkit.AutoCompleteExtender
                               .CreateAutoCompleteItem(sdr["NombreNatural"].ToString(), sdr["ID"].ToString()));
                        }
                    }
                    conn.Close();
                    return c;
                }
            }
        }
        //Fin Service Autocomplete

        void CargarAsociacion()
        {
            using (AsociacionDAO db = new AsociacionDAO())
            {
                grvAsociaciones.DataSource = db.ListarAsociaciones();
                grvAsociaciones.DataBind();
            }
        }

        void CargarTipoInstitucionEducativa() {
            using (TipoInstitucionEducativaDAO db = new TipoInstitucionEducativaDAO())
            {
                ddlTipoInstEduca.DataSource = db.Listar();
                ddlTipoInstEduca.DataTextField = "Nombre";
                ddlTipoInstEduca.DataValueField = "ID";
                ddlTipoInstEduca.DataBind();
            }
        }

        void CargarInstitucionEducativa()
        {
            using (InstitucionEducativaDAO db = new InstitucionEducativaDAO())
            {

                txtSearch.Text = Convert.ToString(db.ListarInstituciones(Convert.ToInt32(ddlTipoInstEduca.SelectedItem.Value)));
                //txtSearch.DataTextField = "Nombre";
                //txtSearch.DataValueField = "ID";
                //txtSearch.DataBind();
            }
        }

        void CargarSeguros()
        {
            using (CiaSeguro_DAO db = new CiaSeguro_DAO())
            {
                ddlCiaSeguros.DataSource = db.ListarSeguros();
                ddlCiaSeguros.DataTextField = "Nombre";
                ddlCiaSeguros.DataValueField = "ID";
                ddlCiaSeguros.DataBind();
            }
        }

        protected void grvAsociaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            using (AsociacionDAO db = new AsociacionDAO())
            {
                grvAsociaciones.DataSource = db.ListarAsociacionesNombre(txtBusqueda.Text);
                grvAsociaciones.DataBind();
            }
        }

        //protected override void Render(System.Web.UI.HtmlTextWriter writer)
        //{
        //    foreach (GridViewRow row in grvAsociaciones.Rows)
        //    {
        //        if (row.RowType == DataControlRowType.DataRow)
        //        {
        //            row.Attributes["onmouseover"] = "this.style.backgroundColor='#FF4000';this.style.color='white';";
        //            row.Attributes["onmouseout"] = "this.style.backgroundColor='#FFFFFF';this.style.color='';";
        //            row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(grvAsociaciones, "Select$" + row.DataItemIndex, true);
        //        }
        //    }

        //    base.Render(writer);
        //}

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            string edi2 = "modalkevin";
            Session["ED"] = edi2;
            Limpiar();
            txtSearch.Enabled = true;
            hdnID.Value = "0";
            MVTipoBancos.ActiveViewIndex = 1;
            txtFechaVigenciaInicio.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            txtFFVigenciaBanco.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            txtFIVigenciaPoliza.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            txtFFVigenciaPoliza.Text = DateTime.Now.Date.AddYears(1). ToString("yyyy-MM-dd");
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

        protected void grvAsociaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int fila = Convert.ToInt32(e.CommandArgument);
                string TipoCargaCodigos = "";
                //string jss = "";
                if (e.CommandName == "EditaAsociacion")
                {
                    ddlTipoInstEduca.SelectedValue = grvAsociaciones.Rows[fila].Cells[48].Text;
                    ddlTipoInstEduca.Attributes.Add("disabled", "disabled");
                    // La institución educativa no podra modificar debido al id ...!! 
                    txtSearch.Enabled = false;
                    //ddlinstitucionIE.SelectedValue = grvAsociaciones.Rows[fila].Cells[14].Text;
                    hdnID.Value= grvAsociaciones.Rows[fila].Cells[31].Text;
                    hdnIDCliente1.Value = grvAsociaciones.Rows[fila].Cells[14].Text;
                    txtSearch.Text =HttpUtility.HtmlDecode( grvAsociaciones.Rows[fila].Cells[1].Text);
                    ddlCiaSeguros.SelectedValue = grvAsociaciones.Rows[fila].Cells[15].Text;
                    ddlCiaSeguros_SelectedIndexChanged(null, null);
                    ddlEmpresaRecaudadora.SelectedValue = grvAsociaciones.Rows[fila].Cells[16].Text;
                    ddlTipoSeguro.SelectedValue = grvAsociaciones.Rows[fila].Cells[17].Text;

                    if (ddlTipoSeguro.SelectedValue == "1" || ddlTipoSeguro.SelectedValue == "2")
                    {
                        txtFechaVigenciaInicio.Text = Convert.ToDateTime(grvAsociaciones.Rows[fila].Cells[18].Text).ToString("yyyy-MM-dd");
                        txtFFVigenciaBanco.Text = Convert.ToDateTime(grvAsociaciones.Rows[fila].Cells[19].Text).ToString("yyyy-MM-dd");
                        txtFIVigenciaPoliza.Text = Convert.ToDateTime(grvAsociaciones.Rows[fila].Cells[20].Text).ToString("yyyy-MM-dd");
                        txtFFVigenciaPoliza.Text = Convert.ToDateTime(grvAsociaciones.Rows[fila].Cells[21].Text).ToString("yyyy-MM-dd");
                    }
                    int monedaId = Convert.ToInt32(grvAsociaciones.Rows[fila].Cells[22].Text);
                    if (monedaId == 1)
                    {
                        rbtMoneda.SelectedIndex = 0;
                    }
                    else
                    {
                        rbtMoneda.SelectedIndex = 1;
                    }

                    txtPrima.Text = grvAsociaciones.Rows[fila].Cells[23].Text;
                    txtInvalidezPT.Text = grvAsociaciones.Rows[fila].Cells[24].Text;
                    txtInvalidezParcial.Text = grvAsociaciones.Rows[fila].Cells[25].Text;
                    txtGastosCuracion.Text = grvAsociaciones.Rows[fila].Cells[26].Text;
                    txtMuerteAccidental.Text = grvAsociaciones.Rows[fila].Cells[27].Text;
                    txtGastosSepelio.Text = grvAsociaciones.Rows[fila].Cells[28].Text;
                    txtMuerteAccidental.Text = grvAsociaciones.Rows[fila].Cells[29].Text;
                    lblFilex.Text=HttpUtility.HtmlDecode( grvAsociaciones.Rows[fila].Cells[36].Text);
                    txtPensionMensual.Text = grvAsociaciones.Rows[fila].Cells[41].Text;
                    txtMesesPension.Text = grvAsociaciones.Rows[fila].Cells[42].Text;
                    txtAñosPension.Text = grvAsociaciones.Rows[fila].Cells[43].Text;
                    txtdeducible.Text=grvAsociaciones.Rows[fila].Cells[47].Text;
                    rbtTipoCarga.SelectedIndex=Convert.ToInt32( grvAsociaciones.Rows[fila].Cells[40].Text)-1;
                    formula1.Text = "=" + txtPensionMensual.Text + " x " + txtPensionMensual.Text;
                    formula2.Text = "=(" + txtPensionMensual.Text + " x " + txtPensionMensual.Text + ")"  ;
                    rbtTipoAsociacion.SelectedIndex = Convert.ToInt32(grvAsociaciones.Rows[fila].Cells[46].Text) - 1;
                    ddlTipoSeguro_SelectedIndexChanged(null, null);
                    using (AsociacionDAO db = new AsociacionDAO())
                    {
                        byte[] data = (byte[])db.ObtenerPlanAsociacion(Convert.ToInt32(grvAsociaciones.Rows[fila].Cells[31].Text));
                        if (data != null)
                        {
                            lblFilex.Text = HttpUtility.HtmlDecode(grvAsociaciones.Rows[fila].Cells[37].Text);
                        }
                        else
                        {
                            lblFilex.Text = "SIN-PLAN-ASIGNADO";
                        }

                        Label lbl = (Label)grvAsociaciones.Rows[fila].FindControl("lblids");
                        if (lbl.Text.Trim() != "")
                        {
                            string lblids = lbl.Text.Substring(0, lbl.Text.Length - 1);
                            string[] Abancos = lblids.Split(',');
                            using (BancoDAO dbBanco = new BancoDAO())
                            {
                                grvBancos.DataSource = dbBanco.ListarBanco();
                                grvBancos.DataBind();

                                foreach (string ele in Abancos)
                                {
                                    foreach (GridViewRow fila_i in grvBancos.Rows)
                                    {
                                        if (fila_i.Cells[0].Text == ele)
                                        {
                                            CheckBox chk = (CheckBox)fila_i.FindControl("chkActivo");
                                            chk.Checked = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            using (BancoDAO dbBanco = new BancoDAO())
                            {
                                grvBancos.DataSource = dbBanco.ListarBanco();
                                grvBancos.DataBind();
                            }

                            hdnID.Value = grvAsociaciones.Rows[fila].Cells[31].Text;
                            //pnlEditaAsociacion.Visible = true;
                        }
                        MVTipoBancos.ActiveViewIndex = 1;
                    }
                }
                if (e.CommandName == "AsignarPoliza")
                {
                    int id = Convert.ToInt32(grvAsociaciones.Rows[fila].Cells[31].Text);
                    hdnID.Value = id.ToString().Trim();
                    TxtNroPoliza.Text = grvAsociaciones.Rows[fila].Cells[32].Text.Replace("&nbsp;", "");
                    TxtCodigoContratante.Text = grvAsociaciones.Rows[fila].Cells[33].Text.Replace("&nbsp;", "");
                    txtNombreContrante.Text = grvAsociaciones.Rows[fila].Cells[34].Text.Replace("&nbsp;", "");

                    string jssa = "openAsignarPoliza();";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssa, true);

                }

                if (e.CommandName == "AnulaAsociacion")
                {
                    hdnID.Value = grvAsociaciones.Rows[fila].Cells[31].Text;
                    lblMensaje.Text = "¿Está seguro que desea anular la asociación para: " + grvAsociaciones.Rows[fila].Cells[1].Text + " ?";
                    string jsss = "openAnularAsociacion();";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jsss, true);
                }

                if (e.CommandName == "EliminaAsociacion")
                {
                    hdnID.Value = grvAsociaciones.Rows[fila].Cells[31].Text;
                    lblMensaje.Text = "¿Está seguro que desea Eliminar la asociación para: " + grvAsociaciones.Rows[fila].Cells[1].Text + " ?";
                    string jsss = "openAnularAsociacion();";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jsss, true);
                }

                if (e.CommandName == "CargarDatos")
                {
                    
                    string IE =HttpUtility.HtmlDecode( grvAsociaciones.Rows[fila].Cells[1].Text);
                    string TS = grvAsociaciones.Rows[fila].Cells[3].Text;
                    string SE =HttpUtility.HtmlDecode( grvAsociaciones.Rows[fila].Cells[2].Text);

                    string IEID = grvAsociaciones.Rows[fila].Cells[14].Text;
                    string SEID = grvAsociaciones.Rows[fila].Cells[15].Text;
                    string RZST = grvAsociaciones.Rows[fila].Cells[35].Text;
                    
                    string IDproduct = grvAsociaciones.Rows[fila].Cells[17].Text;
                    string IDASO = grvAsociaciones.Rows[fila].Cells[31].Text;

                    string CANT = grvAsociaciones.Rows[fila].Cells[38].Text;
                    string codgen = grvAsociaciones.Rows[fila].Cells[44].Text== "&nbsp;" ? "0" : grvAsociaciones.Rows[fila].Cells[44].Text;
                    TipoCargaCodigos = grvAsociaciones.Rows[fila].Cells[40].Text;

                    Response.Redirect("CargarCodigos.aspx?IE=" + IE + "&TS=" + TS + "&SE=" + SE + "&IEID=" + IEID + "&SEID=" + SEID + "&IDASO=" + IDASO + "&IDproduct=" + IDproduct+"&CANT=" + CANT  + "&TipoCargaCodigos=" + TipoCargaCodigos +"&codgen="+ codgen);
                    //Response.Redirect("GenerarCodigos.aspx?IEID=" + IEID + "&SEID=" + SEID+ "&IDASO=" + IDASO + "&IDproduct=" + IDproduct);
                }

                if (e.CommandName == "GenerarCodigos")
                {
                    string IEID = grvAsociaciones.Rows[fila].Cells[14].Text;
                    //hdnIDCliente1.Value = IEID;
                    string SEID =HttpUtility.HtmlDecode( grvAsociaciones.Rows[fila].Cells[17].Text);

                    string IEST = HttpUtility.HtmlDecode(grvAsociaciones.Rows[fila].Cells[1].Text);
                    string TSST =HttpUtility.HtmlDecode( grvAsociaciones.Rows[fila].Cells[3].Text);
                    string SEST = HttpUtility.HtmlDecode(grvAsociaciones.Rows[fila].Cells[2].Text);

                    string RZST = HttpUtility.HtmlDecode(grvAsociaciones.Rows[fila].Cells[35].Text);
                    string IDASO = grvAsociaciones.Rows[fila].Cells[31].Text;
                    string CANT = grvAsociaciones.Rows[fila].Cells[38].Text;
                    //TipoCargaCodigos = grvAsociaciones.Rows[fila].Cells[40].Text;

                    string IDproduct = grvAsociaciones.Rows[fila].Cells[17].Text;
                    Label lblPrima = (Label)(grvAsociaciones.Rows[fila].FindControl("lblPrima"));
                    string Prima = lblPrima.Text;

                    TipoCargaCodigos = grvAsociaciones.Rows[fila].Cells[39].Text;
                    Response.Redirect("GenerarCodigos.aspx?IEID=" + IEID + "&SEID=" + SEID + "&IEST=" + IEST + "&TSST=" + TSST + "&SEST=" + SEST + "&RZST=" + RZST + "&Prima=" + Prima + "&IDASO=" + IDASO + "&IDproduct=" + IDproduct + "&CANT="+ CANT + "&TipoCargaCodigos=" + TipoCargaCodigos);
                }           
            }           
            catch (Exception Ex)
            {

            }
        }

        protected void btnVer_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "ver();", true);
        }


        protected void grvAsociaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    Label lblbancos = (Label)e.Row.FindControl("lblBancos");
                    using (AsociacionDAO db = new AsociacionDAO())
                    {
                        lblbancos.Text = db.ObtenerBancosAsociacion(Convert.ToInt32( e.Row.Cells[31].Text ));
                    }

                    Label lblbancosids = (Label)e.Row.FindControl("lblids");

                    using (AsociacionDAO db = new AsociacionDAO())
                    {
                        lblbancosids.Text = db.ObtenerBancosIdAsociacion(Convert.ToInt32( e.Row.Cells[31].Text ));
                    }
                }
                catch (Exception ex)
                {

                }
            }

            //     return;

            // int id =Convert.ToInt32( e.Row.Cells[10].Text);
            // GridView gvDetails = (GridView)e.Row.FindControl("grvContactos");
            //// cargarContactos(gvDetails, id);
        }
        protected void btnEditaContacto_Click(object sender, EventArgs e)
        {
            using (ContactoDAO db = new ContactoDAO())
            {
                Contacto contacto = new Contacto();
                contacto.ID = Convert.ToInt32(Session["idContacto"]);
                //contacto.ApellidoMaterno = TxtContApeMateEdit.Text;
                //contacto.ApellidoPaterno = TxtContApePateEdit.Text;
                //contacto.Nombre = TxtContNombresEdit.Text;
                //contacto.Email = txtContEmailEdit.Text;
                //contacto.Cargo = txtContCargoEdit.Text;
                db.Agregar(contacto);
                //CargarInstituciones();
                //  txtmensaje.Text = "Contacto:" + TxtNombresContact.Text + " " + TxtApePateContact.Text + " " + TxtApeMateContact.Text + " fue actualizado con exito";
                string jss = "openModal();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
            }
        }

        protected void btnRegistrarContact_Click1(object sender, EventArgs e)
        {
            using (TipoProductoDAO db = new TipoProductoDAO())
            {
                //Producto producto = new Producto();
                //producto.ID = Convert.ToInt32(Session["idSeguro"]);
                //producto.Nombre = TxtNombre.Text;
                //producto.Descripcion = TxtDescripcion.Text;
                //producto.FechaCreacion = DateTime.Now.Date;
                //producto.UsuarioCreacion = "";
                //db.Agregar(producto);
                ////CargarTipoBancos();
                //txtmensaje.Text = "Tipo de seguro:" + producto.Nombre + " fue registrado con exito";
                //string jss = "openModal()";
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
            }
        }

        protected void grvAsociaciones_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            grvAsociaciones.PageIndex = e.NewPageIndex;
            CargarAsociacion();
        }


        protected void ddlCiaSeguros_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Recaudadora> Lista = new List<Recaudadora>();

            Recaudadora r = new Recaudadora();
            r.Id = Convert.ToInt32(ddlCiaSeguros.SelectedValue);
            r.Nombre = ddlCiaSeguros.SelectedItem.Text;
            Lista.Add(r);
            Recaudadora r2 = new Recaudadora();
            r2.Id = 7;
            r2.Nombre = "HERMES";
            Lista.Add(r2);
            CargarRecaudador(Lista);
        }

        void CargarRecaudador(List<Recaudadora> lista)
        {
            ddlEmpresaRecaudadora.Items.Clear();
            ddlEmpresaRecaudadora.DataSource = lista;
            ddlEmpresaRecaudadora.DataValueField = "id";
            ddlEmpresaRecaudadora.DataTextField = "Nombre";
            ddlEmpresaRecaudadora.DataBind();
        }
        void CargarTipoSeguros()
        {
            TipoSeguro_DAO db = new TipoSeguro_DAO();
            ddlTipoSeguro.DataSource = db.Listar();
            ddlTipoSeguro.DataTextField = "Nombre";
            ddlTipoSeguro.DataValueField = "id";
            ddlTipoSeguro.DataBind();
        }

        protected void btnAsociar_Click(object sender, EventArgs e)
        {     
            using (AsociacionDAO db = new AsociacionDAO())
            {
                
                if (hdnIDCliente1.Value == "0")
                {
                    txtmensaje.Text = "Debe seleccionar la institucion educativa";
                    string jss2 = "openModal();";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                    return;
                }

                if (ddlEmpresaRecaudadora.SelectedItem.Value == "0")
                {
                    txtmensaje.Text = "Debe seleccionar la empresa recaudadora";
                    string jss2 = "openModal();";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                    return;
                }

                if (ddlTipoSeguro.SelectedItem.Value == "0")
                {
                    txtmensaje.Text = "Debe seleccionar el tipo de seguro";
                    string jss2 = "openModal();";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                    return;
                }

                if (ddlCiaSeguros.SelectedItem.Value == "0")
                {
                    txtmensaje.Text = "Debe seleccionar el seguro";
                    string jss2 = "openModal();";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                    return;
                }

                try
                {

                    if (Convert.ToString(Session["ED"]) == "modalkevin") {
                        CampañasDAO objn = new CampañasDAO();
                        string MensajeValidaCampana = objn.getValAsocEnCampana(Convert.ToInt32(hdnIDCliente1.Value), Convert.ToInt32(ddlTipoSeguro.SelectedValue));
                        if (MensajeValidaCampana.Trim().Length>0)
                        {
                            txtmensaje.Text = MensajeValidaCampana;
                            string js = "openModal();";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", js, true);
                            return;
                        }
                    }

                    DAO_Hermes.Asociacion asociacion = new DAO_Hermes.Asociacion();
                    asociacion.ID = Convert.ToInt32(hdnID.Value);
                    asociacion.InstitucionEducativaID = hdnIDCliente1.Value=="" ? 0 : Convert.ToInt32(hdnIDCliente1.Value);
                    asociacion.CIASeguroID = Convert.ToInt32(ddlCiaSeguros.SelectedValue);
                    asociacion.RecaudadorID = Convert.ToInt32(ddlEmpresaRecaudadora.SelectedValue);
                    asociacion.ProductoID = Convert.ToInt32(ddlTipoSeguro.SelectedValue);

                    asociacion.FechaVigenciaInicio = null;
                    asociacion.FechaVigenciaFin = null;
                    asociacion.FechaVigenciaPolizaInicio = null;
                    asociacion.FechaVigenciaPolizaFin = null;
                    if (ddlTipoSeguro.SelectedValue == "1" || ddlTipoSeguro.SelectedValue == "2") {
                        asociacion.FechaVigenciaInicio = Convert.ToDateTime(txtFechaVigenciaInicio.Text);
                        asociacion.FechaVigenciaFin = Convert.ToDateTime(txtFFVigenciaBanco.Text);
                        asociacion.FechaVigenciaPolizaInicio = Convert.ToDateTime(txtFIVigenciaPoliza.Text);
                        asociacion.FechaVigenciaPolizaFin = Convert.ToDateTime(txtFFVigenciaPoliza.Text);
                    }
                    asociacion.FileNamePlanSeguro =HttpUtility.HtmlDecode( lblFilex.Text);
                    asociacion.MonedaID = Convert.ToInt32(rbtMoneda.SelectedItem.Value);
                    asociacion.Prima = Convert.ToDecimal(txtPrima.Text);
                    asociacion.FilePlanSeguro = GuardarArchivo(FUpPlan);

                    if (ddlTipoSeguro.SelectedValue == "1")
                    {
                        asociacion.InvalidezPermanenteParcial = Convert.ToDecimal(txtInvalidezParcial.Text);
                        asociacion.GastosSepelio = Convert.ToDecimal(txtGastosSepelio.Text);
                        asociacion.GastoCuracion = Convert.ToDecimal(txtGastosCuracion.Text);
                        asociacion.MuerteAccidental = Convert.ToDecimal(txtMuerteAccidental.Text);
                        asociacion.InvalidezPermanenteTotal = Convert.ToDecimal(txtInvalidezPT.Text);
                        asociacion.InvalidezPermanenteParcial = Convert.ToDecimal(txtInvalidezParcial.Text);
                        asociacion.Deducible =Convert.ToDecimal( txtdeducible.Text);
                    }
                    else if (ddlTipoSeguro.SelectedValue == "2")
                    {
                        asociacion.PensionMensual = Convert.ToDecimal(txtPensionMensual.Text);
                        asociacion.MesesPension = Convert.ToInt32(txtMesesPension.Text);
                        asociacion.AnniosPension = Convert.ToInt32(txtAñosPension.Text);
                        
                    }
                    asociacion.TipoCargaCodigos =Convert.ToInt32( rbtTipoCarga.SelectedItem.Value);
                     asociacion.Activo = true;
                    asociacion.UsuarioCreacion = Session["Usuario"].ToString();
                    asociacion.UsuarioActualizacion = Session["Usuario"].ToString();
                    asociacion.FechaCreacion = DateTime.Now.Date;

                    if (FUpPlan.FileName != "")
                    {
                        string arch = Server.MapPath(" ") + "\\Files\\" + FUpPlan.PostedFile.FileName;
                        FUpPlan.PostedFile.SaveAs( arch);

                        byte[] bytes = System.IO.File.ReadAllBytes(arch);
                        asociacion.FilePlanSeguro = bytes;                                             
                    }

                    asociacion.Deducible =Convert.ToDecimal(  txtdeducible.Text == "" ? "0" : txtdeducible.Text);
                     asociacion.TipoAsociacion =Convert.ToInt32( rbtTipoAsociacion.SelectedItem.Value);

                    if (ddlTipoSeguro.SelectedValue == "1" || ddlTipoSeguro.SelectedValue == "2")
                    {
                        //DateTime? fvIni = null;
                        //DateTime? fvFin = null;
                        //DateTime? fvIniPol = null;
                        //DateTime? fvFinPol = null;

                        //if (txtFechaVigenciaInicio.Text.Trim().Length>0) {
                        //    fvIni
                        //}
                        //if (txtFFVigenciaBanco.Text.Trim().Length > 0)
                        //{
                        //    fvFin
                        //}
                        //if (txtFIVigenciaPoliza.Text.Trim().Length > 0)
                        //{
                        //    fvIniPol
                        //}
                        //if (txtFFVigenciaPoliza.Text.Trim().Length > 0)
                        //{
                        //    fvFinPol
                        //}

                        bool aso = db.ValidarAsociacion(Convert.ToInt32(hdnIDCliente1.Value), Convert.ToDateTime(txtFechaVigenciaInicio.Text), Convert.ToDateTime(txtFFVigenciaBanco.Text), Convert.ToDateTime(txtFIVigenciaPoliza.Text), Convert.ToDateTime(txtFFVigenciaPoliza.Text), Convert.ToInt32(ddlCiaSeguros.SelectedValue), Convert.ToInt32(ddlTipoSeguro.SelectedValue));
                        if (Convert.ToString(Session["ED"]) == "modalkevin")
                        {
                            if (aso == true)
                            {
                                txtmensaje.Text = "Ya existe una asociación para la institucion: " + txtSearch.Text + " con las fechas de vigencia y poliza especificadas";
                                string jssAso = "openModal();";
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssAso, true);
                                return;
                            }
                        }
                    }
                    hdnID.Value= db.Agregar(asociacion).ToString();                                     
                    MVTipoBancos.ActiveViewIndex = 0;
                    GuardarCuentas();
                    CargarAsociacion();
                    txtmensaje.Text = "Registro de asociación de:" + txtSearch.Text + " fue registrado con exito ";
                     string jss2 = "openModal();";
                     ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                }
                catch (Exception ex)
                {
                    txtmensaje.Text = ex.Message;
                    string jss2 = "openModal();";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                }
            }
        }
        void GuardarCuentas()
        {
            using (AsociacionDAO db = new AsociacionDAO())
            {
                BancoDAO dbBanco = new BancoDAO();
                db.Limpiar_Cuentas_Asociacion(Convert.ToInt32(hdnID.Value));

                foreach (GridViewRow fila in grvBancos.Rows)
                {
                    CheckBox chk = (CheckBox)fila.FindControl("chkActivo");
                    if (chk.Checked == true)
                    {                                              
                        //if (dbBanco.EXISTE_CTA_BANCO(Convert.ToInt32(fila.Cells[0].Text), Convert.ToInt32(hdnID.Value) ) == false)
                        //{
                            var cuentas = dbBanco.ListaNumeroCuentasBanco(Convert.ToInt32(fila.Cells[0].Text));

                            foreach (System.Data.DataRow cta in cuentas.Tables[0].Rows)
                            {
                                AsociacionDetalle ad = new AsociacionDetalle();
                                ad.AsociacionID = Convert.ToInt32(hdnID.Value);
                                ad.CuentaID = Convert.ToInt32(cta[0]);
                                ad.FechaCreacion = DateTime.Now.Date;
                                ad.UsuarioCreacion = Session["Usuario"].ToString();
                                db.RegistrarAsociacionDetalle(ad);
                            }
                        //}
                    }
                    //else if (chk.Checked == false)
                    //{
                    //    db.EliminarAsociacionDetalle(Convert.ToInt32(fila.Cells[0].Text), Convert.ToInt32(hdnID.Value));
                    //}
                    }
                }
            }       
        public Byte[] GuardarArchivo(FileUpload flp)
        {
            string filePath = flp.PostedFile.FileName;
            string filename = System.IO.Path.GetFileName(filePath);
            string ext = System.IO.Path.GetExtension(filename);
            string contenttype = String.Empty;

            //Set the contenttype based on File Extension
            switch (ext.ToLower())
            {
                case ".pdf":
                    contenttype = "application/pdf";
                    break;
                    //case ".pdf":
                    //    contenttype = "image/png";
                    //    break;
                    //case ".pdf":
                    //    contenttype = "image/gif";
                    //    break;
            }
            Byte[] bytes = null;
            if (contenttype != String.Empty)
            {
                System.IO.Stream fs = flp.PostedFile.InputStream;
                System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                bytes = br.ReadBytes((Int32)fs.Length);
                //insert the file into database            
            }
            return bytes;
        }

        protected void btnCancelar_Click1(object sender, EventArgs e)
        {
            MVTipoBancos.ActiveViewIndex = 0;
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        void Limpiar()
        {
            ddlTipoInstEduca.SelectedIndex = 0;
            txtSearch.Text = "";
            ddlCiaSeguros.SelectedIndex = 0;
            ddlEmpresaRecaudadora.SelectedIndex = 0;
            ddlTipoSeguro.SelectedIndex = 0;
            txtMuerteAccidental.Text = "0";
            txtPrima.Text = "0";
            txtGastosSepelio.Text = "0";
            txtFIVigenciaPoliza.Text = "";
            txtFFVigenciaPoliza.Text = "";
            txtFFVigenciaBanco.Text = "";
            txtFechaVigenciaInicio.Text = "";
            TxtCodigoContratante.Text = "";
            TxtNroPoliza.Text = "";
            txtNombreContrante.Text = "";
            txtInvalidezParcial.Text = "0";
            txtInvalidezPT.Text = "0";
            txtMesesPension.Text = "0";
            txtPensionMensual.Text = "0";
            txtAñosPension.Text = "0";
            //grvBancos.DataSource = null;
            //grvBancos.DataBind();
            txtMuerteAccidental.Text = "0";
            txtGastosCuracion.Text = "0";
            txtGastosSepelio.Text = "0";
            lblFilex.Text = "";
            rbtMoneda.SelectedIndex = -1;
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            using (AsociacionDAO db = new AsociacionDAO())
            {
                db.AsignarPoliza(Convert.ToInt32(hdnID.Value), TxtNroPoliza.Text, TxtCodigoContratante.Text, txtNombreContrante.Text);
                CargarAsociacion();
            }
        }

        protected void btnAnular_Click1(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(hdnID.Value);
                using (AsociacionDAO db = new AsociacionDAO())
                {
                    db.AnularAsociacion(id, false);
                    btnBuscar_Click(null, null);
                    //CargarAsociacion();
                }
            }
            catch (Exception)
            {

            }
        }

        protected void btnPlan_Click(object sender, ImageClickEventArgs e)
        {
            
            ImageButton img = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)img.NamingContainer;
            string nomArch = gvr.Cells[45].Text;
            int identificador =Convert.ToInt32( gvr.Cells[31].Text);
            AsociacionDAO db = new AsociacionDAO();

            byte[] data = (byte[])db.ObtenerPlanAsociacion(identificador);
        
            if (data != null)
            {
                try
                {
                    //string sFile = grvAsociaciones.Rows[gvr.RowIndex].Cells[45].Text ;
                    //FileStream fs = new FileStream(Server.MapPath("~//Files//") + sFile, FileMode.Create);
                    //fs.Write(data, 0, Convert.ToInt32(data.Length));
                    //fs.Close();
                    //Response.AddHeader("content-disposition", "attachment;filename=" + sFile);
                    //Response.ContentType = "application/pdf";
                    //Response.BinaryWrite(data);
                    //Response.End();
                    Response.Clear();
                    MemoryStream ms = new MemoryStream(data);
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + nomArch);
                    Response.Buffer = true;
                    ms.WriteTo(Response.OutputStream);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                catch (Exception ex)
                {
                    //Response.Write(ex.Message);
                }
            }
        }

     
       protected void chkALL_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkALL = (CheckBox)sender;
            bool accion = false;

            if (chkALL.Checked==true)
            {
                accion = true;
            }
            else
            {
                accion = false;
            }

            foreach (GridViewRow fila in grvBancos.Rows)
            {
                CheckBox chk = (CheckBox)fila.FindControl("chkActivo");
                chk.Checked = accion;
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        protected void grvAsociaciones_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlTipoSeguro_SelectedIndexChanged(object sender, EventArgs e)
        {
            PnlSumasRentas.Visible = false;
            PnlSumasAccidentes.Visible = false;
            rbtTipoAsociacion.Items[1].Attributes.Add("style", "display:none");
            if (ddlTipoSeguro.SelectedValue=="1")
            {
                //    rbtTipoAsociacion.Items[1].Enabled = false;
                rbtTipoAsociacion.Items[1].Attributes.Add("style", "display:none");
                //rbtTipoAsociacion.Items[1].Attributes.Add("class", "oculto");
                PnlSumasAccidentes.Visible = true;
                PnlSumasRentas.Visible = false;
                rbtTipoAsociacion.Visible = false;
            }
            else if (ddlTipoSeguro.SelectedValue == "2")
            {
                rbtTipoAsociacion.Visible = true;
                rbtTipoAsociacion.Items[1].Attributes.Add("style", "display:block");
                PnlSumasRentas.Visible = true;
                PnlSumasAccidentes.Visible = false;
              //  rbtTipoAsociacion.Visible = false;
              
            }
        }

        protected void btnEditar_Click1(object sender, ImageClickEventArgs e)
        {
            string edi = "nomodalkevin";
            Session["ED"] = edi;
        }
    }
}
public class Recaudadora
{
    public int Id { get; set; }
    public string Nombre { get; set;}
}









