using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DAO_Hermes.Repositorios;
using DAO_Hermes.ViewModel;
namespace Demo
{
    public partial class GestionarCampaña : System.Web.UI.Page
    {
        Campañas campañasBe = new Campañas();
        CampañasDAO objn = new CampañasDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    ListarGestionarCampañas();
                    CargarColegiosAsociados();
                    CargarColegiosAsociadosxCompañia();
                    cUtil.ListarTipoInstitucion(ddlTipoInstitucion);
                    cUtil.ListarTipoInstitucion(ddlTipoInstitucionAsig);
                }
                catch(Exception ex)
                { }
            }
        }

        private void ListarGestionarCampañas()
        {
            //List<Campañas> ListarCampañas = objn.ListarGestionarCampañas();
            //grvGestionarCampañas.DataSource = ListarCampañas;
            //grvGestionarCampañas.DataBind();
            Buscar();
        }
        private void CargarColegiosAsociados()
        {
            //ddlInstAsociadas.DataSource = objn.USP_OBTENERINSTITUCIONESASOCIADAS();
            //ddlInstAsociadas.DataValueField = "AsociacionID";
            //ddlInstAsociadas.DataTextField = "Filtro";
            //ddlInstAsociadas.DataBind();
            ddlInstAsociadas.Items.Clear();
            DataTable dtColegios = objn.USP_OBTENERINSTITUCIONESASOCIADAS(Convert.ToInt32(ddlTipoInstitucion.SelectedValue)).Tables[0];
            foreach (DataRow fila in dtColegios.Rows)
            {
                ListItem valor = new ListItem(fila[1].ToString(), fila[0].ToString());
                ddlInstAsociadas.Items.Add(valor);
            }
            string Espacio = "&nbsp;";
            Espacio = Server.HtmlDecode(Espacio);
            lblIeAsociados.Text = "Instituciones Educativas" + Espacio + ddlInstAsociadas.Items.Count.ToString();
        }
        private void CargarColegiosAsociadosxCompañia()
        {
            int idCompañia = Convert.ToInt32(HdCodigoCampaña.Value);
            int TipoInstitucionEducativaId = Convert.ToInt32(ddlTipoInstitucionAsig.SelectedValue);

            //ddlInstAsociadasSEL.DataSource = objn.OBTENER_INST_ASOCIADASxCompañia(idCompañia);
            //ddlInstAsociadasSEL.DataValueField = "AsociacionID";
            //ddlInstAsociadasSEL.DataBind();
            ddlInstAsociadasSEL.Items.Clear();
            DataSet dtColegios = objn.OBTENER_INST_ASOCIADASxCompañia(idCompañia, TipoInstitucionEducativaId);
            if (dtColegios.Tables[0].Rows.Count>0)
                {
            foreach (DataRow fila in dtColegios.Tables[0].Rows)
            {
                ListItem valor = new ListItem(fila[1].ToString(), fila[0].ToString());
                ddlInstAsociadasSEL.Items.Add(valor);
            }
                string Espacio = "&nbsp;";
                Espacio = Server.HtmlDecode(Espacio);
                lblie.Text = "IE Dentro del periodo" +Espacio+ ddlInstAsociadasSEL.Items.Count.ToString();
            }
            else
            {
                lblie.Text = "IE Dentro del periodo:";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //List<Campañas> BusquedaDeCampañas = new List<Campañas>();
            //BusquedaDeCampañas = objn.ConsultarCampañasporNombre(txtBusqueda.Text);
            //grvGestionarCampañas.DataSource = BusquedaDeCampañas;
            //grvGestionarCampañas.DataBind();
            Buscar();
        }

        private void Buscar() {
            List<Campañas> BusquedaDeCampañas = new List<Campañas>();
            BusquedaDeCampañas = objn.ConsultarCampañasporNombre(txtBusqueda.Text);
            grvGestionarCampañas.DataSource = BusquedaDeCampañas;
            grvGestionarCampañas.DataBind();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            string jss = "openEditarCampañas();";
            hdnOpcion.Value = "N";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
        }

        private void Limpiar()
        {
            //ddlCiaSeguros.SelectedIndex = 0;
            //ddlEmpresaRecaudadora.SelectedIndex = 0;
            //ddlinstitucionIE.SelectedIndex = 0;
            //ddlTipoSeguro.SelectedIndex = 0;
            //txtMuerteAccidental.Text = "";
        }

        protected void grvGestionarCampañas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Situacion = e.Row.Cells[4].Text;

                if (Situacion == "False")
                {
                    e.Row.Cells[4].Text = "CERRADO";
                }
                else
                {
                    e.Row.Cells[4].Text = "ACTIVO";
                }

                //string Estado = e.Row.Cells[5].Text;
                //if (Estado == "False")
                //{
                //    e.Row.Cells[5].Text = "CERRADO";
                //}
                //else
                //{
                //    e.Row.Cells[5].Text = "ACTIVO";
                //}
            }
        }

        protected void btnEditaContacto_Click(object sender, EventArgs e)
        {
            try
            {

                //campañasBe.ID = "";// HdCodigoCampaña.Value;
                
                campañasBe.Descripcion = "";
                if (hdnOpcion.Value == "N")
                {
                    campañasBe.ID =0;
                    campañasBe.Nombre = TxtNombreCampaña.Text;
                    campañasBe.InicioVigencia = Convert.ToDateTime(TxtFechaIniVigencia.Text);
                    campañasBe.FinVigencia = Convert.ToDateTime(TxtFechaFinalVigencia.Text);
                    campañasBe.UsuarioCreacion = Session["Usuario"].ToString();
                    campañasBe.FechaCreacion = DateTime.Now.Date;
                    campañasBe.UsuarioActualizacion = Session["Usuario"].ToString();
                    campañasBe.UsuarioCreacion = Session["Usuario"].ToString();
                }
                else if (hdnOpcion.Value == "E")
                {
                    campañasBe.ID =Convert.ToInt32( HdCodigoCampaña.Value);
                    campañasBe.Nombre = txtNombreEdita.Text;
                    campañasBe.InicioVigencia = Convert.ToDateTime(txtFechaVigenciaInicioEdita.Text);
                    campañasBe.FinVigencia = Convert.ToDateTime(txtFechaVigenciaFinEdita.Text);
                    campañasBe.UsuarioActualizacion = Session["Usuario"].ToString();
                    campañasBe.UsuarioCreacion = Session["Usuario"].ToString();
                    campañasBe.FechaCreacion = DateTime.Now.Date;
                    campañasBe.FechaActualizacion = DateTime.Now.Date;
                }

                campañasBe.Estado = "1";                               
                objn.RegistrarCampaña(campañasBe);
                ListarGestionarCampañas();
            }
            catch (Exception ex)
            {
                txtmensaje.Text = ex.Message;
                string jss2 = "openModal()";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
            }
        }

        protected void BtnCierre_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton img = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)img.NamingContainer;
            int CodigoCampaña = Convert.ToInt16(grvGestionarCampañas.DataKeys[gvr.RowIndex].Values["ID"].ToString());
            HdCodigoCampaña.Value = gvr.Cells[0].Text;
            campañasBe.ID = CodigoCampaña;
            List<Campañas> ListarCierreInstituciones = objn.ListarCierreInstituciones(campañasBe);
            grvCierreCampañas.DataSource = ListarCierreInstituciones;
            grvCierreCampañas.DataBind();
            Session["PaginarCierreCampañas"] = ListarCierreInstituciones;
            string jssa = "openCierreInstituciones();";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssa, true);
        }

        protected void BtnApertura_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton img = (ImageButton)sender;
                GridViewRow gvr = (GridViewRow)img.NamingContainer;
                HdCodigoCampaña.Value = gvr.Cells[0].Text;
                //string jssa = "openAperturarCampanas();";
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssa, true);
                CargarColegiosAsociadosxCompañia();
                lblCampañaSel.Text = gvr.Cells[1].Text;
                MVTipoCampañas.ActiveViewIndex = 1;

                if (gvr.Cells[4].Text == "CERRADO")
                    {
                    btnAgregarTodos.Enabled = false;
                    btnAgregar.Enabled = false;
                    btnQuitarTodos.Enabled = false;
                    btnQuitar.Enabled = false;
                }
            }
            catch(Exception ex)
            {

            }
        }

        protected void BtnEditarCampañas_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton img = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)img.NamingContainer;
            //String Tipo_Comprobante = grvGestionarCampañas.DataKeys[gvr.RowIndex].Values["TipoComprobante"].ToString();
            HdCodigoCampaña.Value= gvr.Cells[0].Text;
            txtNombreEdita.Text =HttpUtility.HtmlDecode(  gvr.Cells[1].Text);
            txtFechaVigenciaInicioEdita.Text = Convert.ToDateTime(gvr.Cells[2].Text).ToString("yyyy-MM-dd");
            txtFechaVigenciaFinEdita.Text = Convert.ToDateTime(gvr.Cells[3].Text).ToString("yyyy-MM-dd");
            hdnOpcion.Value = "E";
            string jssa = "openActualizarCampaña();";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssa, true);
        }


        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            //HdCodigoCampaña.Value
            string Cierra = objn.getCierraCampana(Convert.ToInt32(HdCodigoCampaña.Value));
            if (Cierra == "SI")
            {
                objn.CerrarCampaña(Convert.ToInt32(HdCodigoCampaña.Value));
                txtmensaje.Text = "La Campaña ha sido Cerrada con exito.";
                string jss2 = "openModal()";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                //btnSeleccionar.Enabled = false;
            }
            else {
                txtmensaje.Text = "No se puede Cerrar la Campaña. Existe saldo pendiente de Pago a la Compañia de Seguros.";
                string jss2 = "openModal()";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
            }
        }

        protected void grvCierreCampañas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Aquí llamar a los Cierres de Campañas*************************************
                //[dbo].[Usp_Sel_ValidarCierreDeInstituciones]'3','106','86','1'

                //@CampaniaID INT,
                //@AsociacionID INT,
                //@InstitucionEducativaID INT,
                //@ProductoID INT

                //campañasBe.Campaña_Id = Convert.ToInt32(grvCierreCampañas.DataKeys[e.Row.RowIndex].Values["CodigoCampaña"]);
                //campañasBe.Asociacion_ID = Convert.ToString(grvCierreCampañas.DataKeys[e.Row.RowIndex].Values["CodAsociacion"].ToString());
                //campañasBe.InstitucionEducativa_ID = Convert.ToString(grvCierreCampañas.DataKeys[e.Row.RowIndex].Values["CodInstitucion"].ToString());
                //campañasBe.Producto_ID = Convert.ToInt32(grvCierreCampañas.DataKeys[e.Row.RowIndex].Values["productId"]);
                //string ValidarCierres = objn.ValidarCierreInstituciones(campañasBe);

                //if ( ValidarCierres=="false")
                //   {
                //    e.Row.Cells[5].Text = "CERRADO";
                //    ((CheckBox)e.Row.FindControl("chkAll")).Enabled = false;
                //}
                //else if (ValidarCierres == "True")
                //   {
                //    e.Row.Cells[5].Text = "ACTIVO";

                //}

                //Int32 EstadoCierre = Convert.ToInt32(grvCierreCampañas.DataKeys[e.Row.RowIndex].Values["EstadoCierre"].ToString());

                //if (EstadoCierre == 1)
                //{
                //   e.Row.Cells[5].Text = "ACTIVO";
                //}
                //else
                //{
                //   e.Row.Cells[5].Text = "CERRADO";
                //   ((CheckBox)e.Row.FindControl("chkAll")).Enabled = false;
                //}

                int CodigoCampaña= Convert.ToInt32(e.Row.Cells[6].Text);
                int CodInstitucion= Convert.ToInt32(e.Row.Cells[7].Text);
                int productId= Convert.ToInt32(e.Row.Cells[8].Text);

                DataSet ds = objn.getDatosdeCobranza(CodigoCampaña,0, CodInstitucion, productId);
                ((CheckBox)e.Row.FindControl("chkSeleccionar")).Checked = false;
                if (ds.Tables.Count > 0){
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count>0) {
                        DataRow dr = dt.Rows[0];
                        if (Convert.ToDouble(dr["MPendMon1"])==0 && Convert.ToDouble(dr["MPendMon2"]) == 0)
                        {
                            ((CheckBox)e.Row.FindControl("chkSeleccionar")).Checked = true;
                        }
                    }
                }

                Int32 EstadoCierre = Convert.ToInt32(grvCierreCampañas.DataKeys[e.Row.RowIndex].Values["EstadoCierre"].ToString());

                if (EstadoCierre == 1)
                {
                    e.Row.Cells[5].Text = "ACTIVO";
                }
                else
                {
                    e.Row.Cells[5].Text = "CERRADO";
                    //((CheckBox)e.Row.FindControl("chkAll")).Enabled = false;
                }
            }
        }


        protected void grvGestionarCampañas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvGestionarCampañas.PageIndex = e.NewPageIndex;
            ListarGestionarCampañas();
        }

        protected void grvCierreCampañas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grvCierreCampañas.PageIndex = e.NewPageIndex;
                grvCierreCampañas.DataSource = (List<Campañas>)Session["PaginarCierreCampañas"];
                grvCierreCampañas.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void grvGestionarCampañas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ddlInstAsociadas.SelectedIndex > -1)
            {

                objn.AsignarAsociadosCampaña(Convert.ToInt32(HdCodigoCampaña.Value), true, Convert.ToInt32(ddlInstAsociadas.SelectedItem.Value), Session["Usuario"].ToString(), true);
                CargarColegiosAsociados();
                CargarColegiosAsociadosxCompañia();
                //ListItem item = new ListItem(ddlInstAsociadas.SelectedItem.Text, ddlInstAsociadas.SelectedItem.Value);
                //ddlInstAsociadasSEL.Items.Add(item);
                //ddlInstAsociadas.Items.RemoveAt(ddlInstAsociadas.SelectedIndex);
            }

            //string Espacio = "&nbsp;";
            //Espacio = Server.HtmlDecode(Espacio);
            //lblIeAsociados.Text = "Instituciones Edtucativas" + Espacio + ddlInstAsociadas.Items.Count.ToString();
        }

        protected void btnAgregarTodos_Click(object sender, EventArgs e)
        {
            foreach( ListItem ele in ddlInstAsociadas.Items )
            {
                objn.AsignarAsociadosCampaña(Convert.ToInt32(HdCodigoCampaña.Value), true, Convert.ToInt32(ele.Value), Session["Usuario"].ToString(), true);
                //ddlInstAsociadasSEL.Items.Add(ele);
            }
            CargarColegiosAsociados();
            CargarColegiosAsociadosxCompañia();
            //ddlInstAsociadas.Items.Clear();

            //string Espacio = "&nbsp;";
            //Espacio = Server.HtmlDecode(Espacio);
            //lblIeAsociados.Text = "Instituciones Edtucativas" + Espacio + ddlInstAsociadas.Items.Count.ToString();
        }

        protected void btnQuitarTodos_Click(object sender, EventArgs e)
        {
            foreach (ListItem ele in ddlInstAsociadasSEL.Items)
            {
                objn.QuitarAsociaciondeCampaña(Convert.ToInt32(HdCodigoCampaña.Value), Convert.ToInt32(ele.Value));
                //ddlInstAsociadas.Items.Add(ele);
            }
            CargarColegiosAsociados();
            CargarColegiosAsociadosxCompañia();
            //ddlInstAsociadasSEL.Items.Clear();

            //string Espacio = "&nbsp;";
            //Espacio = Server.HtmlDecode(Espacio);
            //lblie.Text = "IE Dentro del periodo" + Espacio + ddlInstAsociadasSEL.Items.Count.ToString();
        }

        protected void btnQuitar_Click(object sender, EventArgs e)
        {
            if (ddlInstAsociadasSEL.SelectedIndex > -1)
            {
                objn.QuitarAsociaciondeCampaña(Convert.ToInt32(HdCodigoCampaña.Value), Convert.ToInt32(ddlInstAsociadasSEL.SelectedItem.Value));
                CargarColegiosAsociados();
                CargarColegiosAsociadosxCompañia();
                ////ddlInstAsociadas.Items.Add(ddlInstAsociadasSEL.SelectedItem);
                ////ddlInstAsociadasSEL.Items.RemoveAt(ddlInstAsociadasSEL.SelectedIndex);
                //ListItem item = new ListItem(ddlInstAsociadasSEL.SelectedItem.Text, ddlInstAsociadasSEL.SelectedItem.Value);
                //ddlInstAsociadas.Items.Add(item);
                //ddlInstAsociadasSEL.Items.RemoveAt(ddlInstAsociadasSEL.SelectedIndex);
            }
            ////ddlInstAsociadasSEL.Items.Clear();
            //string Espacio = "&nbsp;";
            //Espacio = Server.HtmlDecode(Espacio);
            //lblie.Text = "IE Dentro del periodo" + Espacio + ddlInstAsociadasSEL.Items.Count.ToString();
        }

        //protected void btnAsociar_Click(object sender, EventArgs e)
        //{
        //    List<string> codigoAsoc = new List<string>();
        //    foreach (ListItem ele in ddlInstAsociadasSEL.Items)
        //    {
        //        if (ele.Text != "")
        //        {
        //            codigoAsoc.Add(ele.Value);
        //        }
        //    }
        //    try
        //    {
        //        objn.RegistrarAsociacionCampaña(Convert.ToInt32(HdCodigoCampaña.Value), true, Session["Usuario"].ToString(), true, codigoAsoc);
        //        txtmensaje.Text = "La asignación de instituciones educativas fue realizada con exito  a la campaña :" + lblCampañaSel.Text;
        //        string jss2 = "openModal()";
        //        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
        //    }
        //    catch(Exception  ex)
        //    {
        //        txtmensaje.Text =  ex.Message;
        //        string jss2 = "openModal()";
        //        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
        //    }
        //}


        //protected void ddlInstAsociadas_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}
        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            MVTipoCampañas.ActiveViewIndex = 0;
        }
        protected void BtnEliminarCampañas_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton img = (ImageButton)sender;
                GridViewRow gvr = (GridViewRow)img.NamingContainer;
                HdCodigoCampaña.Value = gvr.Cells[0].Text;

                lblmsgConfirm.Text = "¿Desea eliminar el registro de la campaña:" + gvr.Cells[1].Text +"?";
                string jssa = "openConfirm();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssa, true);
                
            }
            catch (Exception ex)
            {
                txtmensaje.Text = ex.Message;
                string jss2 = "openModal()";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                objn.AnularCampaña(Convert.ToInt32(HdCodigoCampaña.Value));
                ListarGestionarCampañas();
            }
            catch(Exception ex)
            {
               
            }
        }

        protected void ddlTipoInstitucion_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarColegiosAsociados();
        }

        protected void ddlTipoInstitucionAsig_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarColegiosAsociadosxCompañia();
        }
    }
}