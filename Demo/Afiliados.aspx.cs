using DAO_Hermes;
using DAO_Hermes.Repositorios;
using DAO_Hermes.ViewModel;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Demo
{
    public partial class Afiliados : System.Web.UI.Page
    {
        string userid = "";
        string email = "";
        int tp = 0;
        //Definir listas de afiliaciones
        List<USP_OBTENER_PRODUCTOS_IE_Result> ConsultaProductoList = new List<USP_OBTENER_PRODUCTOS_IE_Result>();
        Afiliacion obje = new Afiliacion();
        AfiliacionDAO objn = new AfiliacionDAO();
        //List<string> RecuperarIdProducto = new List<string>();
        List<string> Nombres = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {   //Eliminando pdf de la carpeta Temporal(rptTemp)************************************************
                try
                {
                    //             LimpiarDatos();
                    Session["contador"] = 0;
                    foreach (string fichero in Directory.GetFiles(Server.MapPath("~/rptTemp\\"), "*.*"))

                    {
                        File.Delete(fichero);
                    }
                }
                catch (Exception ex)
                {

                }
                ViewState["RegNuevoAseg"] = "NO";

                userid = Request.QueryString["userid"];
                Session["ListaAfiliaAccidentes"] = new List<Util.AfiliaSeguroAccidentes>();
                Session["ListaAfiliaRentas"] = new List<Util.AfiliaSeguroRenta>();
                Session["ListaAfiliaOncologico"] = new List<Util.AfiliaSeguroOncologico>();
                Session["DatosAfiliaProducto"] = cUtil.CrearDetalleAfiliacion();
                Session["DatosAsegurado"] = cUtil.DatosAsegurado();
                Session["DatosPadreAsegurado"] = cUtil.DatosPadreAseguradoRenta();

                Session["Afiliados"] = new List<string>();
                Session["usuAfiliacion"] = email;

                if (Session["RecuperarIdProducto"] == null)
                {
                    Session["RecuperarIdProducto"] = new List<string>();
                }

                //txtmensaje.Text = "Bienvenido al registro de afiliación";
                //string jss = "openModal()";
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);

                CargarGrados();
                CargarGradosBeneficiario();
                CargarTipoDocumentos();
                CargarGradosEdit();
                CargarDpto();
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "desactivarpnlmadre();", true);
                txtmensaje.Text = "Bienvenido al registro de afiliación";
                string jssx = "openModal();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssx, true);
            }
        }

        protected void ddlOncConDirDep_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlOncConDirPrv.Items.Clear();
            cUtil.ListarUbigeo(ddlOncConDirPrv, ddlOncConDirDep.SelectedValue.Substring(0, 2), "01", "00");
            ListItem lstP = new ListItem("Seleccione la Direccion Provincia", "0");
            lstP.Selected = true;
            ddlOncConDirPrv.Items.Add(lstP);

            ddlOncConDirDis.Items.Clear();
            ListItem lstD = new ListItem("Seleccione la Direccion Distrito", "0");
            lstD.Selected = true;
            ddlOncConDirDis.Items.Add(lstD);

            //cUtil.ListarUbigeo(ddlOncConDirDis, ddlOncConDirDep.SelectedValue.Substring(0, 2), "01", "01");
        }

        protected void ddlOncConDirPrv_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlOncConDirDis.Items.Clear();
            cUtil.ListarUbigeo(ddlOncConDirDis, ddlOncConDirDep.SelectedValue.Substring(0, 2), ddlOncConDirPrv.SelectedValue.Substring(2, 2), "01");
            ListItem lstD = new ListItem("Seleccione la Direccion Distrito", "0");
            lstD.Selected = true;
            ddlOncConDirDis.Items.Add(lstD);
        }
        void CargarDpto() {
            //ddlOncConDirDep
            cUtil.ListarUbigeo(ddlOncConDirDep, "01", "00", "00");
        }
        void CargarTipoDocumentos()
        {
            //cUtil.ListarTipoDocumentos(DDLTipoDocumento);
            cUtil.ListarTipoDocumentoRentas(ddlTipoDocuPadre);
            cUtil.ListarTipoDocumentoRentas(DDLTipoDocMadre);
            //cUtil.ListarTipoDocumentos(DDLTipoDocEdit);
            cUtil.ListarTipoDocumentoRentas(DDLPadTipoDocuEdit);
            cUtil.ListarTipoDocumentoRentas(DDLBeneficiarioTipoDocuEdit);
            //cUtil.ListarTipoDocumentoBeneficiario(DDLBeneficiarioTipoDocuEdit);

            cUtil.ListarTipoDocumentos(ddlOncTitTipoDoc);
            cUtil.ListarTipoDocumentos(ddlOncConTipoDoc);
        }

        void CargaAcciREnt()
        {
            if (tp == 1)
            {
                cUtil.ListarTipoDocumentoAccidente(DDLTipoDocumento);
                cUtil.ListarTipoDocumentoAccidente(DDLTipoDocEdit);
            }
            else
            {
                cUtil.ListarTipoDocumentoRentas(DDLTipoDocumento);
                cUtil.ListarTipoDocumentoRentas(DDLTipoDocEdit);
            }

        }

        void inicializar()
        {
            Session["ListaAfiliaAccidentes"] = new List<Util.AfiliaSeguroAccidentes>();
            Session["ListaAfiliaRentas"] = new List<Util.AfiliaSeguroRenta>();
            Session["ListaAfiliaOncologico"] = new List<Util.AfiliaSeguroOncologico>();
            Session["DatosAfiliaProducto"] = cUtil.CrearDetalleAfiliacion();
            Session["DatosAsegurado"] = cUtil.DatosAsegurado();
            Session["DatosPadreAsegurado"] = cUtil.DatosPadreAseguradoRenta();
        }

        void CargarGrados()
        {
            using (GradoDAO db = new GradoDAO())
            {
                DDLGrado.DataSource = db.Listar();
                DDLGrado.DataTextField = "Nombre";
                DDLGrado.DataValueField = "Id";
                DDLGrado.DataBind();

                ddlOncTitGrado.DataSource = db.Listar();
                ddlOncTitGrado.DataTextField = "Nombre";
                ddlOncTitGrado.DataValueField = "Id";
                ddlOncTitGrado.DataBind();
                
            }
        }


        void CargarGradosBeneficiario()
        {
            using (GradoDAO db = new GradoDAO())
            {
                DDLBeneficiarioGradoEdit.DataSource = db.Listar();
                DDLBeneficiarioGradoEdit.DataTextField = "Nombre";
                DDLBeneficiarioGradoEdit.DataValueField = "Id";
                DDLBeneficiarioGradoEdit.DataBind();
            }
        }


        void CargarGradosEdit()
        {
            ddlGradoEdit.Items.Clear();
            using (GradoDAO db = new GradoDAO())
            {
                ddlGradoEdit.DataSource = db.Listar();
                ddlGradoEdit.DataTextField = "Nombre";
                ddlGradoEdit.DataValueField = "Id";
                ddlGradoEdit.DataBind();
            }
        }
        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            btnConfirmar.Enabled = true;
            using (InstitucionEducativaDAO db = new InstitucionEducativaDAO())
            {
                string codigoAfiliacion = txtCodigo.Text;
                if (codigoAfiliacion != "")
                {
                    try
                    {
                        int idIE = 0;
                        string resultado = db.ObtenerCodigoInstitucion(codigoAfiliacion, out idIE);
                        OnlineDAO dbOnline = new OnlineDAO();
                        dbOnline.LimpiarRegistroOnline(Session["Usuario"].ToString());

                        txtmensaje.Text = resultado;
                        string jss = "openModal()";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
                        MVWAfiliacion.ActiveViewIndex = 1;
                        Session["AfiliacionProducto"] = db.ListarProductosIE(idIE);
                        grvSeguros.DataSource = Session["AfiliacionProducto"];
                        //ViewState["AfiliacionProducto"] = grvSeguros.Rows[0].Cells[3].Text;
                        grvSeguros.DataBind();
                        Limpiar();
                        //    LimpiarDatos();
                    }
                    catch (Exception ex)
                    {
                        txtmensaje.Text = ex.Message;
                        string jss = "openModal();";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
                    }
                }
            }
        }

        protected void btnSgtePaso2_Click(object sender, EventArgs e)
        {
            MVWAfiliacion.ActiveViewIndex = 2;
            int ifila = 0;
            string chekTipoProducto = "";
            try
            {
                foreach (GridViewRow grv in grvSeguros.Rows)
                {
                    CheckBox chk = (CheckBox)grv.FindControl("chkElegir");
                    if (chk.Checked == true)
                    {
                        chekTipoProducto += Convert.ToInt32(grvSeguros.Rows[grv.RowIndex].Cells[6].Text);
                        break;
                    }
                    ifila += 1;
                }
                //Recuperar IdProducto********************************
                List<string> RecuperarIdProducto = (List<string>)Session["RecuperarIdProducto"];
                RecuperarIdProducto.Add(chekTipoProducto);
                Session["RecuperarIdProducto"] = RecuperarIdProducto;

                hdnEdad.Value = "0";

                int iProducto = Convert.ToInt32(grvSeguros.Rows[ifila].Cells[6].Text);
                hdnProductoId.Value = Convert.ToString(iProducto);

                hdnAsociacionId.Value = grvSeguros.Rows[ifila].Cells[1].Text;
                ViewState["NombreColegio"] = grvSeguros.Rows[ifila].Cells[2].Text;

                //Recupera el Tipo de Seguro ********************************
                ViewState["Aseguradora"] = grvSeguros.Rows[ifila].Cells[4].Text;

                hdnInstitucionEducativa.Value = grvSeguros.Rows[ifila].Cells[5].Text;
                hdnidAseguradora.Value = grvSeguros.Rows[ifila].Cells[7].Text;
                hdnPrima.Value = grvSeguros.Rows[ifila].Cells[8].Text;
                ViewState["CodigoMoneda"] = grvSeguros.Rows[ifila].Cells[9].Text;//Codigo de Moneda

                //Recuperando las Fechas de Vigencias Y Fecha de Págos*********************************************
                ViewState["FechaVigenciaPolizaInicio"] = grvSeguros.Rows[ifila].Cells[10].Text;
                ViewState["FechaVigenciaPolizaFinal"] = grvSeguros.Rows[ifila].Cells[11].Text;
                ViewState["FechaDepago"] = grvSeguros.Rows[ifila].Cells[12].Text;

                ViewState["TipoAsociacion"] = grvSeguros.Rows[ifila].Cells[14].Text;
                try
                {
                    List<USP_OBTENER_PRODUCTOS_IE_Result> ff = (List<USP_OBTENER_PRODUCTOS_IE_Result>)Session["AfiliacionProducto"];
                    ViewState["FilePlanSeguro"] = ff[ifila].FilePlanSeguro;
                    ViewState["FileNamePlanSeguro"] = ff[ifila].FileNamePlanSeguro;
                }
                catch (Exception)
                {
                    throw;
                }

                int n = 0;
                n = iProducto;
                tp = n;
                if (iProducto == 1)
                {
                    hdnProducto.Value = grvSeguros.Rows[ifila].Cells[3].Text;
                    lblProducto.Text = "DATOS DE ALUMNO - ACCIDENTES";
                    pnlDatosAlumno.Visible = true;
                    pnlDatoMadre.Visible = false;
                    pnlDatosPadre.Visible = false;
                    pnlDatosOncTit.Visible = false;
                    pnlDatosOncCon.Visible = false;
                }
                else if (iProducto == 2)
                {
                    hdnProducto.Value = grvSeguros.Rows[ifila].Cells[3].Text;
                    lblProducto.Text = "DATOS DEL ALUMNO - BENEFICIARIO";

                    pnlDatosAlumno.Visible = true;
                    if (ViewState["TipoAsociacion"].ToString() == "1")
                    {
                        pnlDatosPadre.Visible = true;
                        pnlDatosPadre.Enabled = true;
                        pnlDatoMadre.Visible = false;
                        chkSegundoPadre.Checked = false;
                    }
                    else if (ViewState["TipoAsociacion"].ToString() == "2")
                    {
                        pnlDatosPadre.Visible = true;
                        pnlDatoMadre.Visible = true;
                        chkSegundoPadre.Enabled = true;
                        pnlDatosPadre.Enabled = true;
                    }
                    pnlDatosOncTit.Visible = false;
                    pnlDatosOncCon.Visible = false;
                }
                else if (iProducto == 3 || iProducto==7)
                {
                    hdnProducto.Value = grvSeguros.Rows[ifila].Cells[3].Text;
                    pnlDatosAlumno.Visible = false;
                    pnlDatoMadre.Visible = false;
                    pnlDatosPadre.Visible = false;
                    pnlDatosOncTit.Visible = true;
                    pnlDatosOncCon.Visible = true;

                    //string jssx = string.Format("viewForm({0});",iProducto);
                    //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssx, true);
                }

                //MuestraFormAfiliacion();
                CargaAcciREnt();
            }
            catch (Exception ex)
            {

            }
        }

        //void MuestraFormAfiliacion() {
        //    string jssx = string.Format("viewForm({0});", hdnProductoId.Value);
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssx, true);
        //}
        protected void btnAceptaResPreg_Click(object sender, EventArgs e)
        {
            if (rblDscPrg.SelectedValue.Length > 0 && rblDscPrgFuma.SelectedValue.Length>0)
            {
                btnSgtePaso3_Click(sender, e);
            }
            else
            {
                string jssc = "openModalcombo();onblur_OncTitFecNac();";
                lblcombo.Text = "Ud. debe Contestar las Preguntas Formuladas.";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssc, true);                
                return;
            }
        }

        void lanza_pregunta()
        {
            string jss = "openPreg();onblur_OncTitFecNac();";
            lblTitlePreg.ForeColor = System.Drawing.Color.Gray;
            lblTitlePreg.Text = "Responda la siguiente Pregunta correspondiente al Titular (Alumno asegurado):";
            lblPreg.ForeColor = System.Drawing.Color.Gray;            
            lblPreg.Text = "¿Ha(s) sido diagnosticado y/o recibido tratamiento por: cualquier tipo de cáncer, neoplasias o tumores(pólipos, displasias, condilomas, leucoplasias), Tiroiditis de Hashimoto, Hepatitis B o C, Virus del Papiloma Humano, VIH - sida ? ";
            lblPregFuma.ForeColor = System.Drawing.Color.Gray;
            lblPregFuma.Text = "¿ Fumas o has fumado diariamente cigarrillos en los últimos años?";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
        }
        protected void btnSgtePaso3_Click(object sender, EventArgs e)
        {
            string FileNamePlanSeguro = "";
            try
            {

                if (hdnProductoId.Value == "1" || hdnProductoId.Value == "2")
                {
                    if (DDLTipoDocumento.SelectedValue == "0")
                    {
                        string jssc = "openModalcombo();";
                        lblcombo.Text = "Debe Seleccionar un Tipo Documento Del Asegurado";
                       ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssc, true);
                        return;
                    }

                    if (DDLGrado.SelectedValue == "0")
                    {
                        string jssc = "openModalcombo();";
                        lblcombo.Text = "Debe Seleccionar el Grado del Alumno";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssc, true);
                        return;
                    }
                }

                if (hdnProductoId.Value == "2")
                {
                    int edad = cUtil.Edad(Convert.ToDateTime(txtFechaNacPadre.Text));
                    if (edad > 70)
                    {
                        string jssc = "openModalcombo();";
                        lblcombo.Text = "La edad del asegurado no puede ser mayor a los  70 años";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssc, true);
                        return;
                    }

                    if (txtFechaNacMadre.Text.Trim() != "" && chkSegundoPadre.Checked == true)
                    {
                        int edad2 = cUtil.Edad(Convert.ToDateTime(txtFechaNacMadre.Text));
                        if (edad2 > 70)
                        {
                            string jssc = "openModalcombo();";
                            lblcombo.Text = "La edad del asegurado no puede ser mayor a los  70 años";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssc, true);
                            return;
                        }
                    }
                }

                if (hdnProductoId.Value == "2" && chkSegundoPadre.Checked == true)
                {
                    if (DDLTipoDocMadre.SelectedValue == "0")
                    {
                        string jssc = "openModalcombo();";
                        lblcombo.Text = "Debe seleccionar el tipo de documento del segundo Padre asegurado";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssc, true);
                        return;
                    }
                    if (ddlTipoDocuPadre.SelectedValue == "0")
                    {
                        string jssc = "openModalcombo();";
                        lblcombo.Text = "Debe seleccionar el tipo de documento del primer Padre Asegurado";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssc, true);
                        return;
                    }
                    if (DDLTipoDocumento.SelectedValue == "0")
                    {
                        string jssc = "openModalcombo();";
                        lblcombo.Text = "Debe seleccionar el tipo de documento del beneficiario";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssc, true);
                        return;
                    }
                }

                BtnAsegOtro.Visible = true;
                if (hdnProductoId.Value == "3" || hdnProductoId.Value == "7")
                {
                    BtnAsegOtro.Visible = false;

                    string sfuncion = "openModalcombo();onblur_OncTitFecNac();";
                    if (ddlOncTitTipoDoc.SelectedValue == "0")
                    {
                        lblcombo.Text = "Debe seleccionar el tipo de documento del Titular";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", sfuncion, true);
                        return;
                    }
                    if (Convert.ToInt32(hdnEdad.Value) == 0)
                    {
                        lblcombo.Text = "Ingrese una Fecha de Nacimiento del titular valida.";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", sfuncion, true);
                        return;
                    }
                    if (hdnProductoId.Value == "3")
                    {
                        if (Convert.ToInt32(hdnEdad.Value) > 18)
                        {
                            lblcombo.Text = "La edad máxima de afiliación al seguro Oncológico Escolar es de 18 años.";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", sfuncion, true);
                            return;
                        }
                    }
                    if (hdnProductoId.Value == "7")
                    {
                        if (Convert.ToInt32(hdnEdad.Value) >30)
                        {
                            lblcombo.Text = "La edad máxima de afiliación al seguro Oncológico Superior es de 30 años.";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", sfuncion, true);
                            return;
                        }
                    }
                    if (ddlOncTitGrado.SelectedValue == "0")
                    {
                        lblcombo.Text = "Debe seleccionar el grado alcanzado del Titular";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", sfuncion, true);
                        return;
                    }
                    if (hdnOncConTipoDoc.Value == "0")
                    {
                        lblcombo.Text = "Debe seleccionar el tipo de documento del Contratante";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", sfuncion, true);
                        return;
                    }
                    if (ddlOncConEstcivil.SelectedValue == "0")
                    {
                        lblcombo.Text = "Debe seleccionar el estado civil del Contratante";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", sfuncion, true);
                        return;
                    }
                    //if (ddlOncConPaisNac.SelectedValue == "0")
                    //{
                    //    lblcombo.Text = "Debe seleccionar el Pais de nacimiento del Contratante";
                    //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", sfuncion, true);
                    //    return;
                    //}
                    if (ddlOncConDirPais.SelectedValue == "0")
                    {
                        lblcombo.Text = "Debe seleccionar la direccion Pais del Contratante";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", sfuncion, true);
                        return;
                    }
                    if (ddlOncConDirDep.SelectedValue == "0")
                    {
                        lblcombo.Text = "Debe seleccionar la direccion Departamento del Contratante";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", sfuncion, true);
                        return;
                    }
                    if (ddlOncConDirPrv.SelectedValue == "0")
                    {
                        lblcombo.Text = "Debe seleccionar la direccion Provincia del Contratante";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", sfuncion, true);
                        return;
                    }
                    if (ddlOncConDirDis.SelectedValue == "0")
                    {
                        lblcombo.Text = "Debe seleccionar la direccion Distrito del Contratante";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", sfuncion, true);
                        return;
                    }

                    if (rblDscPrg.SelectedValue.Length <= 0)
                    {
                        lanza_pregunta();
                        return;
                    }
                    else
                    {
                        if (rblDscPrg.SelectedValue == "SI")
                        {
                            string jssc = "openModalcombo();";
                            lblcombo.Text = "Gracias por confiar en nosotros. Estaremos comunicandonos con usted para ofrecerle un tipo de seguro que se adecue a sus necesidades.";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssc, true);

                            LimpiarDatos();
                            OnlineDAO dbOnline = new OnlineDAO();
                            dbOnline.LimpiarRegistroOnline(Session["Usuario"].ToString());
                            LimpiarInfoPadres();
                            inicializar();
                            MVWAfiliacion.ActiveViewIndex = 0;
                            chkAcepto.Checked = false;
                            rblDscPrg.Items[0].Selected=false;
                            rblDscPrg.Items[1].Selected = false;
                            return;
                        }
                    }
                }

                if (ViewState["RegNuevoAseg"].ToString() == "NO")
                {
                     OnlineDAO dbOnline = new OnlineDAO();
                    dbOnline.LimpiarRegistroOnline(Session["Usuario"].ToString());
                }

                DataTable dtDetallePlan = (DataTable)Session["DatosAfiliaProducto"];
                string asegurado = "";
                string asegurado2 = "";
                string beneficiario = "";
                string producto = "";
                decimal prima = 0;
                string seguro = "";
                string plan = "";
                int idMoneda = 0;
                string nombreColegio = "";
                string aseguradora = "";
                int IdAsociacion = 0;
                bool acepto = false;
                string TipoMoneda = "";
                int IdProducto = 0;

                ConsultaProductoList = (List<USP_OBTENER_PRODUCTOS_IE_Result>)Session["AfiliacionProducto"];
                producto = hdnProducto.Value;
                //ACCIDENTES
                if (hdnProductoId.Value == "1")
                {
                    beneficiario = (txtApellidoPaterno.Text + " " + txtApellidoMaterno.Text + ", " + txtNombres.Text.ToUpper()).ToUpper();
                    asegurado = (txtApePadre.Text + " " + txtApeMatPadre.Text + ", " + txtNomPadre.Text.ToUpper()).ToUpper();
                    asegurado2 = (txtApePatMadre.Text + " " + txtApeMatMadre.Text + ", " + txtNombreMadre.Text.ToUpper()).ToUpper();
                    prima = Convert.ToDecimal(hdnPrima.Value);
                    aseguradora = ViewState["Aseguradora"].ToString(); //ConsultaProductoList[0].Aseguradora;
                    TipoMoneda = "";
                    //llenar aca monea e institucionEducativa*****************************
                    idMoneda = Convert.ToInt32(ViewState["CodigoMoneda"]);// Convert.ToInt32(ConsultaProductoList[0].MonedaID);
                    nombreColegio = HttpUtility.HtmlDecode(ConsultaProductoList[0].NombreNatural.ToString());
                    IdProducto = Convert.ToInt32(hdnProductoId.Value);
                    IdAsociacion = Convert.ToInt32(hdnAsociacionId.Value);

                    FileNamePlanSeguro = Convert.ToString(ViewState["FileNamePlanSeguro"]);

                    OnlineDAO dbAfilidos = new OnlineDAO();
                    Reg_Online Reg = new Reg_Online();
                    Reg.Bene_ApePaterno = txtApellidoPaterno.Text.ToUpper();
                    Reg.Bene_ApeMaterno = txtApellidoMaterno.Text.ToUpper();
                    Reg.Bene_ApeNombres = txtNombres.Text.ToUpper();
                    Reg.Bene_TipoDocumento = Convert.ToInt32(DDLTipoDocumento.SelectedValue);
                    Reg.Bene_NumDocumento = txtNumeroDocumento.Text;
                    Reg.Bene_FechaNacimiento = txtFechaNacimiento.Text;
                    Reg.Bene_Sexo = rbtSexo.SelectedIndex + 1;
                    Reg.UsuarioCreacion = Session["Usuario"].ToString();
                    Reg.Bene_Seccion = txtSeccion.Text.ToUpper();
                    Reg.Bene_Grado = DDLGrado.SelectedValue;

                    Reg.TipoSeguro = HttpUtility.HtmlDecode(hdnProducto.Value);
                    Reg.Prima = hdnPrima.Value;
                    Reg.CompañiaSeguro = aseguradora;
                    Reg.MonedaId = idMoneda;
                    Reg.Colegio = hdnInstitucionEducativa.Value;
                    Reg.IdProducto = Convert.ToInt32(hdnProductoId.Value);
                    Reg.IdAsociacion = Convert.ToInt32(hdnAsociacionId.Value);
                    Reg.idInstitucionEducativa = Convert.ToInt32(hdnInstitucionEducativa.Value);

                    dbAfilidos.AgregarOnlineAccidentes(Reg);
                    CargarConfirmarAfiliacionAccidentes(dtDetallePlan, Session["Usuario"].ToString());

                }
                if (hdnProductoId.Value == "2")
                {
                    if (ViewState["TipoAsociacion"].ToString() == "1")
                    {
                        beneficiario = (txtApellidoPaterno.Text + " " + txtApellidoMaterno.Text + ", " + txtNombres.Text.ToUpper()).ToUpper();
                        asegurado = (txtApePadre.Text + " " + txtApeMatPadre.Text + ", " + txtNomPadre.Text.ToUpper()).ToUpper();
                        asegurado2 = (txtApePatMadre.Text + " " + txtApeMatMadre.Text + ", " + txtNombreMadre.Text.ToUpper()).ToUpper();
                        prima = Convert.ToDecimal(hdnPrima.Value);
                        aseguradora = ViewState["Aseguradora"].ToString(); //ConsultaProductoList[0].Aseguradora;
                        TipoMoneda = "";
                        //llenar aca monea e institucionEducativa*****************************
                        idMoneda = Convert.ToInt32(ViewState["CodigoMoneda"]);// Convert.ToInt32(ConsultaProductoList[0].MonedaID);
                        nombreColegio = HttpUtility.HtmlDecode(ConsultaProductoList[0].NombreNatural.ToString());
                        IdProducto = Convert.ToInt32(hdnProductoId.Value);
                        IdAsociacion = Convert.ToInt32(hdnAsociacionId.Value);
                        FileNamePlanSeguro = Convert.ToString(ViewState["FileNamePlanSeguro"]);

                        OnlineDAO dbAfilidos = new OnlineDAO();
                        Reg_Online Reg = new Reg_Online();
                        Reg.Bene_ApePaterno = txtApellidoPaterno.Text.ToUpper();
                        Reg.Bene_ApeMaterno = txtApellidoMaterno.Text.ToUpper();
                        Reg.Bene_ApeNombres = txtNombres.Text.ToUpper();
                        Reg.Bene_TipoDocumento = Convert.ToInt32(DDLTipoDocumento.SelectedValue);
                        Reg.Bene_NumDocumento = txtNumeroDocumento.Text;
                        Reg.Bene_FechaNacimiento = txtFechaNacimiento.Text;
                        Reg.Bene_Sexo = rbtSexo.SelectedIndex + 1;
                        Reg.UsuarioCreacion = Session["Usuario"].ToString();
                        Reg.Bene_Seccion = txtSeccion.Text.ToUpper();
                        Reg.Bene_Grado = DDLGrado.SelectedValue;

                        Reg.Padre_Nombres = txtNomPadre.Text.ToUpper();
                        Reg.Padre_ApePaterno = txtApePadre.Text.ToUpper();
                        Reg.Padre_ApeMaterno = txtApeMatPadre.Text.ToUpper();
                        Reg.Padre_FechaNacimiento = txtFechaNacPadre.Text;
                        Reg.Padre_TipoDocumento = Convert.ToInt32(ddlTipoDocuPadre.SelectedValue);
                        Reg.Padre_NumDocumento = txtNumDocPadre.Text;
                        //Reg.Padre_IdBeneficiario = Convert.ToInt32(alu.ID);                                               

                        if (rbtTipoPadre.SelectedIndex == 0) { Reg.Padre_TipoPadre = 1; }
                        else if (rbtTipoPadre.SelectedIndex == 1) { Reg.Padre_TipoPadre = 2; }

                        Reg.TipoSeguro = HttpUtility.HtmlDecode(hdnProducto.Value);
                        Reg.Prima = hdnPrima.Value;
                        Reg.CompañiaSeguro = HttpUtility.HtmlDecode(aseguradora);
                        Reg.MonedaId = idMoneda;
                        Reg.Colegio = HttpUtility.HtmlDecode(hdnInstitucionEducativa.Value);
                        Reg.IdProducto = Convert.ToInt32(hdnProductoId.Value);
                        Reg.IdAsociacion = Convert.ToInt32(hdnAsociacionId.Value);
                        Reg.idInstitucionEducativa = Convert.ToInt32(hdnInstitucionEducativa.Value);

                        dbAfilidos.AgregarOnlineAsocia1(Reg);
                        CargarConfirmarAfiliacion(dtDetallePlan, Session["Usuario"].ToString());
                    }


                    if (ViewState["TipoAsociacion"].ToString() == "2" && chkSegundoPadre.Checked == false)
                    {
                        beneficiario = (txtApellidoPaterno.Text.ToUpper() + " " + txtApellidoMaterno.Text.ToUpper() + ", " + txtNombres.Text.ToUpper()).ToUpper();
                        asegurado = (txtApePadre.Text.ToUpper() + " " + txtApeMatPadre.Text.ToUpper() + ", " + txtNomPadre.Text.ToUpper()).ToUpper();
                        asegurado2 = (txtApePatMadre.Text + " " + txtApeMatMadre.Text + ", " + txtNombreMadre.Text.ToUpper()).ToUpper();
                        prima = Convert.ToDecimal(hdnPrima.Value);
                        aseguradora = HttpUtility.HtmlDecode(ViewState["Aseguradora"].ToString()); //ConsultaProductoList[0].Aseguradora;
                        TipoMoneda = "";
                        //llenar aca monea e institucionEducativa*****************************
                        idMoneda = Convert.ToInt32(ViewState["CodigoMoneda"]);// Convert.ToInt32(ConsultaProductoList[0].MonedaID);
                        nombreColegio = HttpUtility.HtmlDecode(ConsultaProductoList[0].NombreNatural.ToString());
                        IdProducto = Convert.ToInt32(hdnProductoId.Value);
                        IdAsociacion = Convert.ToInt32(hdnAsociacionId.Value);
                        FileNamePlanSeguro = Convert.ToString(ViewState["FileNamePlanSeguro"]);

                        OnlineDAO dbAfilidos = new OnlineDAO();
                        Reg_Online Reg = new Reg_Online();
                        Reg.Bene_ApePaterno = txtApellidoPaterno.Text.ToUpper();
                        Reg.Bene_ApeMaterno = txtApellidoMaterno.Text.ToUpper();
                        Reg.Bene_ApeNombres = txtNombres.Text.ToUpper();
                        Reg.Bene_TipoDocumento = Convert.ToInt32(DDLTipoDocumento.SelectedValue);
                        Reg.Bene_NumDocumento = txtNumeroDocumento.Text;
                        Reg.Bene_FechaNacimiento = txtFechaNacimiento.Text;
                        Reg.Bene_Sexo = rbtSexo.SelectedIndex + 1;
                        Reg.UsuarioCreacion = Session["Usuario"].ToString();
                        Reg.Bene_Seccion = txtSeccion.Text.ToUpper();
                        Reg.Bene_Grado = DDLGrado.SelectedValue;

                        Reg.Padre_Nombres = txtNomPadre.Text.ToUpper();
                        Reg.Padre_ApePaterno = txtApePadre.Text.ToUpper();
                        Reg.Padre_ApeMaterno = txtApeMatPadre.Text.ToUpper();
                        Reg.Padre_FechaNacimiento = txtFechaNacPadre.Text;
                        Reg.Padre_TipoDocumento = Convert.ToInt32(ddlTipoDocuPadre.SelectedValue);
                        Reg.Padre_NumDocumento = txtNumDocPadre.Text;
                        Reg.TipoRegistro = 1;
                        //Reg.Padre_IdBeneficiario = Convert.ToInt32(alu.ID);                                               

                        if (rbtTipoPadre.SelectedIndex == 0) { Reg.Padre_TipoPadre = 1; }
                        else if (rbtTipoPadre.SelectedIndex == 1) { Reg.Padre_TipoPadre = 2; }

                        Reg.TipoSeguro = HttpUtility.HtmlDecode(hdnProducto.Value);
                        Reg.Prima = hdnPrima.Value;
                        Reg.CompañiaSeguro = aseguradora;
                        Reg.MonedaId = idMoneda;
                        Reg.Colegio = hdnInstitucionEducativa.Value;
                        Reg.IdProducto = Convert.ToInt32(hdnProductoId.Value);
                        Reg.IdAsociacion = Convert.ToInt32(hdnAsociacionId.Value);
                        Reg.idInstitucionEducativa = Convert.ToInt32(hdnInstitucionEducativa.Value);

                        Reg_Online RegRS = new Reg_Online();
                        RegRS.Bene_ApePaterno = txtApellidoPaterno.Text.ToUpper();
                        RegRS.Bene_ApeMaterno = txtApellidoMaterno.Text.ToUpper();
                        RegRS.Bene_ApeNombres = txtNombres.Text.ToUpper();
                        RegRS.Bene_TipoDocumento = Convert.ToInt32(DDLTipoDocumento.SelectedValue);
                        RegRS.Bene_NumDocumento = txtNumeroDocumento.Text;
                        RegRS.Bene_FechaNacimiento = txtFechaNacimiento.Text;
                        RegRS.Bene_Sexo = rbtSexo.SelectedIndex + 1;
                        RegRS.Bene_Seccion = txtSeccion.Text.ToUpper();
                        RegRS.Bene_Grado = DDLGrado.SelectedValue;
                        RegRS.Padre_Nombres = "RESERVADO";
                        RegRS.Padre_ApePaterno = "RESERVADO";
                        RegRS.Padre_ApeMaterno = "RESERVADO";
                        RegRS.UsuarioCreacion = Session["Usuario"].ToString();
                        RegRS.TipoRegistro = 2;
                        //    RegRS.Bene_Seccion = dt.Rows[ind]["Seccion"].ToString();


                        if (rbtTipoPadre.SelectedIndex == 0) { RegRS.Padre_TipoPadre = 1; }
                        else if (rbtTipoPadre.SelectedIndex == 1) { RegRS.Padre_TipoPadre = 2; }

                        RegRS.TipoSeguro = HttpUtility.HtmlDecode(hdnProducto.Value);
                        RegRS.Prima = hdnPrima.Value;
                        RegRS.CompañiaSeguro = HttpUtility.HtmlDecode(aseguradora);
                        RegRS.MonedaId = idMoneda;
                        RegRS.Colegio = hdnInstitucionEducativa.Value;
                        RegRS.IdProducto = Convert.ToInt32(hdnProductoId.Value);
                        RegRS.IdAsociacion = Convert.ToInt32(hdnAsociacionId.Value);
                        RegRS.idInstitucionEducativa = Convert.ToInt32(hdnInstitucionEducativa.Value);

                        dbAfilidos.AgregarOnline(Reg, RegRS);
                        CargarConfirmarAfiliacion(dtDetallePlan, Session["Usuario"].ToString());
                    }

                    else if (ViewState["TipoAsociacion"].ToString() == "2" && chkSegundoPadre.Checked == true)
                    {
                        beneficiario = (txtApellidoPaterno.Text + " " + txtApellidoMaterno.Text + ", " + txtNombres.Text.ToUpper()).ToUpper();
                        asegurado = (txtApePadre.Text + " " + txtApeMatPadre.Text + ", " + txtNomPadre.Text.ToUpper()).ToUpper();
                        asegurado2 = (txtApePatMadre.Text + " " + txtApeMatMadre.Text + ", " + txtNombreMadre.Text.ToUpper()).ToUpper();
                        prima = Convert.ToDecimal(hdnPrima.Value);
                        aseguradora = HttpUtility.HtmlDecode(ViewState["Aseguradora"].ToString()); //ConsultaProductoList[0].Aseguradora;
                        TipoMoneda = "";
                        //llenar aca monea e institucionEducativa*****************************
                        idMoneda = Convert.ToInt32(ViewState["CodigoMoneda"]);// Convert.ToInt32(ConsultaProductoList[0].MonedaID);
                        nombreColegio = HttpUtility.HtmlDecode(ConsultaProductoList[0].NombreNatural.ToString());
                        IdProducto = Convert.ToInt32(hdnProductoId.Value);
                        IdAsociacion = Convert.ToInt32(hdnAsociacionId.Value);
                        FileNamePlanSeguro = Convert.ToString(ViewState["FileNamePlanSeguro"]);

                        OnlineDAO dbAfilidos = new OnlineDAO();
                        Reg_Online Reg = new Reg_Online();
                        Reg.Bene_ApePaterno = txtApellidoPaterno.Text.ToUpper(); ;
                        Reg.Bene_ApeMaterno = txtApellidoMaterno.Text.ToUpper(); ;
                        Reg.Bene_ApeNombres = txtNombres.Text.ToUpper(); ;
                        Reg.Bene_TipoDocumento = Convert.ToInt32(DDLTipoDocumento.SelectedValue);
                        Reg.Bene_NumDocumento = txtNumeroDocumento.Text;
                        Reg.Bene_FechaNacimiento = txtFechaNacimiento.Text;
                        Reg.Bene_Sexo = rbtSexo.SelectedIndex + 1;
                        Reg.UsuarioCreacion = Session["Usuario"].ToString();
                        Reg.Bene_Seccion = txtSeccion.Text;
                        Reg.Bene_Grado = DDLGrado.SelectedValue;

                        Reg.Padre_Nombres = txtNomPadre.Text.ToUpper();
                        Reg.Padre_ApePaterno = txtApePadre.Text.ToUpper();
                        Reg.Padre_ApeMaterno = txtApeMatPadre.Text.ToUpper();
                        Reg.Padre_FechaNacimiento = txtFechaNacPadre.Text.ToUpper();
                        Reg.Padre_TipoDocumento = Convert.ToInt32(ddlTipoDocuPadre.SelectedValue);
                        Reg.Padre_NumDocumento = txtNumDocPadre.Text;
                        //Reg.Padre_IdBeneficiario = Convert.ToInt32(alu.ID);                                               

                        if (rbtTipoPadre.SelectedIndex == 0) { Reg.Padre_TipoPadre = 1; }
                        else if (rbtTipoPadre.SelectedIndex == 1) { Reg.Padre_TipoPadre = 2; }

                        Reg.TipoSeguro = HttpUtility.HtmlDecode(hdnProducto.Value);
                        Reg.Prima = hdnPrima.Value;
                        Reg.CompañiaSeguro = aseguradora;
                        Reg.MonedaId = idMoneda;
                        Reg.Colegio = hdnInstitucionEducativa.Value;
                        Reg.IdProducto = Convert.ToInt32(hdnProductoId.Value);
                        Reg.IdAsociacion = Convert.ToInt32(hdnAsociacionId.Value);
                        Reg.idInstitucionEducativa = Convert.ToInt32(hdnInstitucionEducativa.Value);

                        Reg_Online RegRS = new Reg_Online();
                        RegRS.Bene_ApePaterno = txtApellidoPaterno.Text.ToUpper(); ;
                        RegRS.Bene_ApeMaterno = txtApellidoMaterno.Text.ToUpper(); ;
                        RegRS.Bene_ApeNombres = txtNombres.Text.ToUpper();
                        RegRS.Bene_TipoDocumento = Convert.ToInt32(DDLTipoDocumento.SelectedValue);
                        RegRS.Bene_NumDocumento = txtNumeroDocumento.Text;
                        RegRS.Bene_FechaNacimiento = txtFechaNacimiento.Text;
                        RegRS.Bene_Sexo = rbtSexo.SelectedIndex + 1;
                        RegRS.Bene_Seccion = txtSeccion.Text;
                        RegRS.Bene_Grado = DDLGrado.SelectedValue;

                        RegRS.Padre_Nombres = txtNombreMadre.Text.ToUpper(); ;
                        RegRS.Padre_ApePaterno = txtApePatMadre.Text.ToUpper(); ;
                        RegRS.Padre_ApeMaterno = txtApeMatMadre.Text.ToUpper(); ;

                        RegRS.Padre_TipoDocumento = Convert.ToInt32(DDLTipoDocMadre.SelectedValue);
                        RegRS.Padre_NumDocumento = txtNumDocMadre.Text.ToUpper(); ;
                        RegRS.Padre_FechaNacimiento = txtFechaNacMadre.Text.ToUpper(); ;

                        RegRS.UsuarioCreacion = Session["Usuario"].ToString();
                        //    RegRS.Bene_Seccion = dt.Rows[ind]["Seccion"].ToString();
                        //  RegRS.Bene_Grado = "";

                        if (rbtParentescoMadre.SelectedIndex == 0) { RegRS.Padre_TipoPadre = 1; }
                        else if (rbtParentescoMadre.SelectedIndex == 1) { RegRS.Padre_TipoPadre = 2; }

                        RegRS.TipoSeguro = HttpUtility.HtmlDecode(hdnProducto.Value);
                        RegRS.Prima = hdnPrima.Value;
                        RegRS.CompañiaSeguro = aseguradora;
                        RegRS.MonedaId = idMoneda;
                        RegRS.Colegio = hdnInstitucionEducativa.Value;
                        RegRS.IdProducto = Convert.ToInt32(hdnProductoId.Value);
                        RegRS.IdAsociacion = Convert.ToInt32(hdnAsociacionId.Value);
                        RegRS.idInstitucionEducativa = Convert.ToInt32(hdnInstitucionEducativa.Value);

                        dbAfilidos.AgregarOnline(Reg, RegRS);
                        CargarConfirmarAfiliacionAsocia2(dtDetallePlan);

                    }

                }

                if (hdnProductoId.Value == "3" || hdnProductoId.Value == "7")
                {
                    beneficiario = txtOncTitApePat.Text.ToUpper() + " " + txtOncTitApeMat.Text.ToUpper() + ", " + txtOncTitNom.Text.ToUpper();
                    asegurado = txtOncTitApePat.Text.ToUpper() + " " + txtOncTitApeMat.Text.ToUpper() + ", " + txtOncTitNom.Text.ToUpper();
                    prima = Convert.ToDecimal(hdnPrima.Value);
                    aseguradora = HttpUtility.HtmlDecode(ViewState["Aseguradora"].ToString());
                    TipoMoneda = "";
                    //llenar aca monea e institucionEducativa*****************************
                    idMoneda = Convert.ToInt32(ViewState["CodigoMoneda"]);
                    nombreColegio = HttpUtility.HtmlDecode(ConsultaProductoList[0].NombreNatural.ToString());
                    IdProducto = Convert.ToInt32(hdnProductoId.Value);
                    IdAsociacion = Convert.ToInt32(hdnAsociacionId.Value);
                    FileNamePlanSeguro = Convert.ToString(ViewState["FileNamePlanSeguro"]);

                    OnlineDAO dbAfilidos = new OnlineDAO();
                    Reg_Online Reg = new Reg_Online();
                    Reg.Bene_ApePaterno = txtOncTitApePat.Text.ToUpper();
                    Reg.Bene_ApeMaterno = txtOncTitApeMat.Text.ToUpper();
                    Reg.Bene_ApeNombres = txtOncTitNom.Text.ToUpper();
                    Reg.Bene_TipoDocumento = Convert.ToInt32(ddlOncTitTipoDoc.SelectedValue);
                    Reg.Bene_NumDocumento = txtOncTitNroDoc.Text;
                    Reg.Bene_FechaNacimiento = txtOncTitFecNac.Text;
                    Reg.Bene_Grado = ddlOncTitGrado.SelectedValue;
                    Reg.Bene_Sexo = rblOncTitGenero.SelectedIndex + 1;
                    Reg.FechaCreacion = DateTime.Now;
                    Reg.UsuarioCreacion = Session["Usuario"].ToString();

                    if (rblOncConPar.SelectedIndex == 0) { Reg.Padre_TipoPadre = 1; }
                    else if (rblOncConPar.SelectedIndex == 1) { Reg.Padre_TipoPadre = 2; }
                    Reg.Padre_ApePaterno = hdnOncConApePat.Value.ToUpper();//txtOncConApePat.Text.ToUpper();
                    Reg.Padre_ApeMaterno = hdnOncConApeMat.Value.ToUpper();//txtOncConApeMat.Text.ToUpper();
                    Reg.Padre_Nombres = hdnOncConNom.Value.ToUpper();//txtOncConNom.Text.ToUpper();
                    Reg.Padre_TipoDocumento = Convert.ToInt32(hdnOncConTipoDoc.Value);//Convert.ToInt32(ddlOncConTipoDoc.SelectedValue);
                    Reg.Padre_NumDocumento = hdnOncConNroDoc.Value;//txtOncConNroDoc.Text;
                    Reg.Padre_FechaNacimiento = hdnOncConFecNac.Value;//txtOncConFecNac.Text;
                    Reg.TipoRegistro = 1;
                    Reg.ad1EstCivilId = Convert.ToInt32(ddlOncConEstcivil.SelectedValue);
                    Reg.ad1NacPais = txtOncConPaisNac.Text;
                    Reg.ad1DirPaisId = Convert.ToInt32(ddlOncConDirPais.SelectedValue);
                    Reg.ad1DirDptCod = ddlOncConDirDep.SelectedValue.Substring(0, 2);
                    Reg.ad1DirPrvCod = ddlOncConDirPrv.SelectedValue.Substring(2, 2);
                    Reg.ad1DirDisCod = ddlOncConDirDis.SelectedValue.Substring(4, 2);
                    Reg.ad1DirEnt = txtOncConDirEnt.Text;
                    Reg.ad1TlfnoDom = txtOncConTelDom.Text;
                    Reg.ad1TlfnoTrb = txtOncConTelTrab.Text;
                    Reg.ad1NroCel = txtOncConNroCel.Text;
                    Reg.ad1DirMail = txtOncConEmail.Text;
                    if (rblOncConGen.SelectedIndex == 0) { Reg.ad1Sexo = 1; }
                    else if (rblOncConGen.SelectedIndex == 1) { Reg.ad1Sexo = 2; }
                    
                    Reg.TipoSeguro = HttpUtility.HtmlDecode(hdnProducto.Value);
                    Reg.Prima = hdnPrima.Value;
                    Reg.CompañiaSeguro = aseguradora;
                    Reg.MonedaId = idMoneda;
                    Reg.Colegio = hdnInstitucionEducativa.Value;
                    Reg.IdProducto = Convert.ToInt32(hdnProductoId.Value);
                    Reg.IdAsociacion = Convert.ToInt32(hdnAsociacionId.Value);
                    Reg.idInstitucionEducativa = Convert.ToInt32(hdnInstitucionEducativa.Value);

                    dbAfilidos.AgregarOnlineAsocia1(Reg);
                    CargarConfirmarAfiliacion(dtDetallePlan, Session["Usuario"].ToString());
                }

                MVWAfiliacion.ActiveViewIndex = 3;

            }
            catch (Exception ex)
            {

            }
            finally {
                if (ViewState["RegNuevoAseg"].ToString() == "SI")
                {
                    ViewState["RegNuevoAseg"] = "NO";
                }
            }
        }
        decimal TotalSoles = 0;
        decimal TotalDolares = 0;
        protected void GrvConfirmar_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    int TipoMoneda = Convert.ToInt16(e.Row.Cells[13].Text);
                    string space = "&nbsp;";
                    space = Server.HtmlDecode(space);

                    if (TipoMoneda == 1)//SOLES
                    {
                        e.Row.Cells[5].Text = "S/.";
                        e.Row.Cells[5].Font.Bold = true;
                        TotalSoles += Convert.ToDecimal(e.Row.Cells[6].Text); //Convert.ToDecimal(((Label)e.Row.FindControl("lblPrima")).Text);
                        //((Label)e.Row.FindControl("lblPrima")).Text = ((Label)e.Row.FindControl("lblPrima")).Text;                      
                    }
                    else if (TipoMoneda == 2)//DOLARES
                    {
                        e.Row.Cells[5].Text = "US$";
                        e.Row.Cells[5].Font.Bold = true;
                        TotalDolares += Convert.ToDecimal(e.Row.Cells[6].Text); //Convert.ToDecimal(((Label)e.Row.FindControl("lblPrima")).Text);
                        //((Label)e.Row.FindControl("lblPrima")).Text = ((Label)e.Row.FindControl("lblPrima")).Text;                      
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
                e.Row.Cells[4].Text = "Total Prima(S/.):" + "<br>" + "Total Prima(US$):";
                e.Row.Cells[4].Font.Bold = true;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                //((Label)e.Row.FindControl("lblSoles")).Text = "<b>" + "S/." + "</b>" + space + TotalSoles;
                //((Label)e.Row.FindControl("lblDolares")).Text = "<b>" + "US$" + "</b>" + space + TotalDolares;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[6].Text = "<b>" + "S/." + "</b>" + space + TotalSoles + "</br>" +
                                      "<b>" + "US$" + "</b>" + space + TotalDolares;
            }
        }

        protected void btnVerTerminos_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string rutaArchivo = Server.MapPath(".") + "\\Files\\Terminos\\condiciones.txt";
                lblTerminos.Text = cUtil.LeerArchivoTexto(rutaArchivo);
                string jss = "openTermscondiciones();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnEditar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //CargaAcciREnt();
                ImageButton btn = (ImageButton)sender;
                GridViewRow Row = (GridViewRow)btn.NamingContainer;
                hdnFilaEdit.Value = Convert.ToString(Row.RowIndex);
                CargarGradosEdit();

                OnlineDAO db = new OnlineDAO();

                string tipo = GrvConfirmar.Rows[Row.RowIndex].Cells[15].Text.ToString();
                hdnPadreId.Value = tipo;
                int index = Row.RowIndex;

                if (hdnProductoId.Value == "1")
                {
                    #region editaracc
                    if (DDLTipoDocEdit.SelectedValue == "2")
                    {
                        txtNumDocEdit.MaxLength = 8;
                    }
                    else
                    {
                        txtNumDocEdit.MaxLength = 20;
                    }
                    DataSet ds = db.LISTAR_AFILIADO_ACCIDENTES(Convert.ToInt32(GrvConfirmar.Rows[Row.RowIndex].Cells[20].Text));

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow d = ds.Tables[0].Rows[0];
                        hdnBeneficiario.Value = (d["Bene_ApePaterno"].ToString() + " " + d["Bene_ApeMaterno"].ToString() + ", " + d["Bene_ApeNombres"].ToString()).ToUpper();
                        TxtApePateEdit.Text = d["Bene_ApePaterno"].ToString();
                        TxtApeMateEdit.Text = d["Bene_ApeMaterno"].ToString();
                        TxtNombresEdit.Text = d["Bene_ApeNombres"].ToString();
                        DDLTipoDocEdit.SelectedValue = d["Bene_TipoDocumento"].ToString();
                        txtNumDocEdit.Text = d["Bene_NumDocumento"].ToString();
                        txtFechaNacEdit.Text = d["Bene_FechaNacimiento"].ToString();
                        ddlGradoEdit.SelectedValue = d["Bene_Grado"].ToString();
                        txtSeccionEdit.Text = d["Bene_seccion"].ToString();
                        //   txtBeneficiarioSeccionEdit.Text = d[""].ToString();
                        if (DDLTipoDocEdit.SelectedValue == "2")
                        {
                            txtNumDocEdit.MaxLength = 8;
                            txtPadNumDocEdit.MaxLength = 8;
                        }
                        else
                        {
                            txtNumDocEdit.MaxLength = 20;
                            txtPadNumDocEdit.MaxLength = 20;
                        }
                        string jss3 = "openEditarAlumno();";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss3, true);
                        return;
                    }
                #endregion
                }
                else if (hdnProductoId.Value == "2")
                {
                    //DataTable dt = (DataTable)Session["DatosPadreAsegurado"];
                    //hdnPadreId.Value = tipo;
                    //DataTable dtBeneficiario = (DataTable)Session["DatosAsegurado"];
                    DataSet ds = db.LISTAR_AFILIACION_ONLINE(Convert.ToInt32(GrvConfirmar.Rows[Row.RowIndex].Cells[20].Text));
                    int idx = Convert.ToInt32(hdnFilaEdit.Value);
                    //int idxAlumno = 0;
                    //int idxpadre = 0;

                    if (ViewState["TipoAsociacion"].ToString() == "1")
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow d = ds.Tables[0].Rows[0];
                            txtPadApePaternoEdit.Text = d["Padre_ApePaterno"].ToString();
                            txtPadApeMaternoEdit.Text = d["Padre_ApeMaterno"].ToString();
                            txtPadNombresEdit.Text = d["Padre_Nombres"].ToString();
                            DDLPadTipoDocuEdit.SelectedValue = d["Padre_TipoDocumento"].ToString();
                            txtPadNumDocEdit.Text = d["Padre_NumDocumento"].ToString();
                            txtPadFechaNacEdit.Text = d["Padre_FechaNacimiento"].ToString();
                            //hdnAntiguo.Value = txtPadApePaternoEdit.Text + " " + txtPadApeMaternoEdit.Text + " " + txtPadNombresEdit.Text;
                            hdnBeneficiario.Value = d["Bene_ApePaterno"].ToString() + " " + d["Bene_ApeMaterno"].ToString() + " " + d["Bene_ApeNombres"].ToString();
                            txtApePatBeneficiarioEdit.Text = d["Bene_ApePaterno"].ToString();
                            txtApeMatBeneficiarioEdit.Text = d["Bene_ApeMaterno"].ToString();
                            txtNombreBeneficiarioEdit.Text = d["Bene_ApeNombres"].ToString();
                            DDLBeneficiarioTipoDocuEdit.SelectedValue = d["Bene_TipoDocumento"].ToString();
                            txtBeneficiarioNumeroDocuEdit.Text = d["Bene_NumDocumento"].ToString();
                            txtBeneficiarioFechaNacEdit.Text = d["Bene_FechaNacimiento"].ToString();
                            DDLBeneficiarioGradoEdit.SelectedValue = d["Bene_Grado"].ToString();
                            txtBeneficiarioSeccionEdit.Text = d["Bene_seccion"].ToString();
                            //   txtBeneficiarioSeccionEdit.Text = d[""].ToString();
                            if (DDLBeneficiarioTipoDocuEdit.SelectedValue == "1")
                            {
                                txtBeneficiarioNumeroDocuEdit.MaxLength = 8;
                                txtPadNumDocEdit.MaxLength = 8;
                            }
                            else
                            {
                                txtBeneficiarioNumeroDocuEdit.MaxLength = 20;
                                txtPadNumDocEdit.MaxLength = 20;
                            }
                            string jss3 = "openEditarPadre();";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss3, true);
                            return;
                        }
                    }
                    if (ViewState["TipoAsociacion"].ToString() == "2" && chkSegundoPadre.Checked == true)
                    {
                        OnlineDAO dbO = new OnlineDAO();

                        DataSet dsO = dbO.LISTAR_AFILIACION_ONLINE(Convert.ToInt32(GrvConfirmar.Rows[index].Cells[20].Text));
                        if (dsO.Tables[0].Rows.Count > 0)
                        {
                            DataRow d = ds.Tables[0].Rows[0];
                            txtPadApePaternoEdit.Text = d["Padre_ApePaterno"].ToString();
                            txtPadApeMaternoEdit.Text = d["Padre_ApeMaterno"].ToString();
                            txtPadNombresEdit.Text = d["Padre_Nombres"].ToString();
                            DDLPadTipoDocuEdit.SelectedValue = d["Padre_TipoDocumento"].ToString();
                            txtPadNumDocEdit.Text = d["Padre_NumDocumento"].ToString();
                            txtPadFechaNacEdit.Text = d["Padre_FechaNacimiento"].ToString();
                            //hdnAntiguo.Value = txtPadApePaternoEdit.Text + " " + txtPadApeMaternoEdit.Text + " " + txtPadNombresEdit.Text;
                            hdnBeneficiario.Value = (d["Bene_ApePaterno"].ToString() + " " + d["Bene_ApeMaterno"].ToString() + ", " + d["Bene_ApeNombres"].ToString()).ToUpper();
                            txtApePatBeneficiarioEdit.Text = d["Bene_ApePaterno"].ToString();
                            txtApeMatBeneficiarioEdit.Text = d["Bene_ApeMaterno"].ToString();
                            txtNombreBeneficiarioEdit.Text = d["Bene_ApeNombres"].ToString();
                            DDLBeneficiarioTipoDocuEdit.SelectedValue = d["Bene_TipoDocumento"].ToString();
                            txtBeneficiarioNumeroDocuEdit.Text = d["Bene_NumDocumento"].ToString();
                            txtBeneficiarioFechaNacEdit.Text = d["Bene_FechaNacimiento"].ToString();
                            DDLBeneficiarioGradoEdit.SelectedValue = d["Bene_Grado"].ToString();
                            txtBeneficiarioSeccionEdit.Text = d["Bene_seccion"].ToString();
                            //   txtBeneficiarioSeccionEdit.Text = d[""].ToString();
                            if (DDLBeneficiarioTipoDocuEdit.SelectedValue == "1")
                            {
                                txtBeneficiarioNumeroDocuEdit.MaxLength = 8;
                                txtPadNumDocEdit.MaxLength = 8;
                            }
                            else
                            {
                                txtBeneficiarioNumeroDocuEdit.MaxLength = 20;
                                txtPadNumDocEdit.MaxLength = 20;
                            }
                            string jss3 = "openEditarPadre();";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss3, true);
                            return;
                        }
                    }
                }

                if (ViewState["TipoAsociacion"].ToString() == "2" && chkSegundoPadre.Checked == false)
                {
                    DataSet ds = db.LISTAR_AFILIACION_ONLINE(Convert.ToInt32(GrvConfirmar.Rows[Row.RowIndex].Cells[20].Text));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow d = ds.Tables[0].Rows[0];
                        txtPadApePaternoEdit.Text = d["Padre_ApePaterno"].ToString();
                        txtPadApeMaternoEdit.Text = d["Padre_ApeMaterno"].ToString();
                        txtPadNombresEdit.Text = d["Padre_Nombres"].ToString();
                        DDLPadTipoDocuEdit.SelectedValue = d["Padre_TipoDocumento"].ToString();
                        txtPadNumDocEdit.Text = d["Padre_NumDocumento"].ToString();
                        txtPadFechaNacEdit.Text = d["Padre_FechaNacimiento"].ToString();
                        //hdnAntiguo.Value = txtPadApePaternoEdit.Text + " " + txtPadApeMaternoEdit.Text + " " + txtPadNombresEdit.Text;
                        hdnBeneficiario.Value = d["Bene_ApePaterno"].ToString() + " " + d["Bene_ApeMaterno"].ToString() + " " + d["Bene_ApeNombres"].ToString();
                        txtApePatBeneficiarioEdit.Text = d["Bene_ApePaterno"].ToString();
                        txtApeMatBeneficiarioEdit.Text = d["Bene_ApeMaterno"].ToString();
                        txtNombreBeneficiarioEdit.Text = d["Bene_ApeNombres"].ToString();
                        DDLBeneficiarioTipoDocuEdit.SelectedValue = d["Bene_TipoDocumento"].ToString();
                        txtBeneficiarioNumeroDocuEdit.Text = d["Bene_NumDocumento"].ToString();
                        txtBeneficiarioFechaNacEdit.Text = d["Bene_FechaNacimiento"].ToString();
                        DDLBeneficiarioGradoEdit.SelectedValue = d["Bene_Grado"].ToString();
                        txtBeneficiarioSeccionEdit.Text = d["Bene_seccion"].ToString();
                        //   txtBeneficiarioSeccionEdit.Text = d[""].ToString();
                        if (DDLBeneficiarioTipoDocuEdit.SelectedValue == "1")
                        {
                            txtBeneficiarioNumeroDocuEdit.MaxLength = 8;
                            txtPadNumDocEdit.MaxLength = 8;
                        }
                        else
                        {
                            txtBeneficiarioNumeroDocuEdit.MaxLength = 20;
                            txtPadNumDocEdit.MaxLength = 20;
                        }
                        string jss3 = "openEditarPadre();";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss3, true);
                        return;
                    }
                }
                if (ViewState["TipoAsociacion"].ToString() == "2" && chkSegundoPadre.Checked == true)
                {
                    OnlineDAO dbO = new OnlineDAO();

                    DataSet dsO = dbO.LISTAR_AFILIACION_ONLINE(Convert.ToInt32(GrvConfirmar.Rows[index].Cells[20].Text));
                    if (dsO.Tables[0].Rows.Count > 0)
                    {
                        DataRow d = dsO.Tables[0].Rows[0];
                        txtPadApePaternoEdit.Text = d["Padre_ApePaterno"].ToString();
                        txtPadApeMaternoEdit.Text = d["Padre_ApeMaterno"].ToString();
                        txtPadNombresEdit.Text = d["Padre_Nombres"].ToString();
                        DDLPadTipoDocuEdit.SelectedValue = d["Padre_TipoDocumento"].ToString();
                        txtPadNumDocEdit.Text = d["Padre_NumDocumento"].ToString();
                        txtPadFechaNacEdit.Text = d["Padre_FechaNacimiento"].ToString();
                        //hdnAntiguo.Value = txtPadApePaternoEdit.Text + " " + txtPadApeMaternoEdit.Text + " " + txtPadNombresEdit.Text;
                        hdnBeneficiario.Value = (d["Bene_ApePaterno"].ToString() + " " + d["Bene_ApeMaterno"].ToString() + ", " + d["Bene_ApeNombres"].ToString()).ToUpper();
                        txtApePatBeneficiarioEdit.Text = d["Bene_ApePaterno"].ToString();
                        txtApeMatBeneficiarioEdit.Text = d["Bene_ApeMaterno"].ToString();
                        txtNombreBeneficiarioEdit.Text = d["Bene_ApeNombres"].ToString();
                        DDLBeneficiarioTipoDocuEdit.SelectedValue = d["Bene_TipoDocumento"].ToString();
                        txtBeneficiarioNumeroDocuEdit.Text = d["Bene_NumDocumento"].ToString();
                        txtBeneficiarioFechaNacEdit.Text = d["Bene_FechaNacimiento"].ToString();
                        DDLBeneficiarioGradoEdit.SelectedValue = d["Bene_Grado"].ToString();
                        //   txtBeneficiarioSeccionEdit.Text = d[""].ToString();
                        if (DDLBeneficiarioTipoDocuEdit.SelectedValue == "1")
                        {
                            txtBeneficiarioNumeroDocuEdit.MaxLength = 8;
                            txtPadNumDocEdit.MaxLength = 8;
                        }
                        else
                        {
                            txtBeneficiarioNumeroDocuEdit.MaxLength = 20;
                            txtPadNumDocEdit.MaxLength = 20;
                        }
                        string jss3 = "openEditarPadre();";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss3, true);
                        return;
                    }
                }

                if (hdnProductoId.Value == "3" || hdnProductoId.Value == "7") {
                    cUtil.ListarTipoDocumentos(ddlOncTitEdtTipoDoc);
                    cUtil.ListarTipoDocumentos(ddlOncConEdtTipoDoc);
                    cUtil.ListarUbigeo(ddlOncConEdtDirDep, "01", "00", "00");
                    using (GradoDAO dbGrado = new GradoDAO())
                    {
                        ddlOncTitEdtGrado.DataSource = dbGrado.Listar();
                        ddlOncTitEdtGrado.DataTextField = "Nombre";
                        ddlOncTitEdtGrado.DataValueField = "Id";
                        ddlOncTitEdtGrado.DataBind();
                    }

                    DataSet ds = db.Obtener_AFILIACION_ONCOLOGICO(Convert.ToInt32(GrvConfirmar.Rows[Row.RowIndex].Cells[20].Text));

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow d = ds.Tables[0].Rows[0];

                        txtOncTitEdtApePat.Text = d["Bene_ApePaterno"].ToString();
                        txtOncTitEdtApeMat.Text = d["Bene_ApeMaterno"].ToString();
                        txtOncTitEdtNom.Text = d["Bene_ApeNombres"].ToString();
                        ddlOncTitEdtTipoDoc.SelectedValue = d["Bene_TipoDocumento"].ToString(); 
                        txtOncTitEdtNroDoc.Text = d["Bene_NumDocumento"].ToString();
                        txtOncTitEdtFecNac.Text = d["Bene_FechaNacimiento"].ToString();
                        ddlOncTitEdtGrado.SelectedValue = d["Bene_Grado"].ToString();
                        rblOncTitEdtGenero.SelectedValue = d["Bene_Sexo"].ToString();

                        rblOncConEdtPar.SelectedValue = d["Padre_TipoPadre"].ToString();
                        hdnOncConEdtApePat.Value = d["Padre_ApePaterno"].ToString();
                        txtOncConEdtApePat.Text = d["Padre_ApePaterno"].ToString();
                        hdnOncConEdtApeMat.Value = d["Padre_ApeMaterno"].ToString();
                        txtOncConEdtApeMat.Text = d["Padre_ApeMaterno"].ToString();
                        hdnOncConEdtNom.Value = d["Padre_Nombres"].ToString();
                        txtOncConEdtNom.Text = d["Padre_Nombres"].ToString();
                        ddlOncConEdtTipoDoc.SelectedValue = d["Padre_TipoDocumento"].ToString();  
                        hdnOncConEdtNroDoc.Value = d["Padre_NumDocumento"].ToString();
                        txtOncConEdtNroDoc.Text = d["Padre_NumDocumento"].ToString();
                        hdnOncConEdtFecNac.Value = d["Padre_FechaNacimiento"].ToString();
                        txtOncConEdtFecNac.Text = d["Padre_FechaNacimiento"].ToString();   
                        ddlOncConEdtEstcivil.SelectedValue = d["ad1EstCivilId"].ToString();
                        txtOncConEdtPaisNac.Text = d["ad1NacPais"].ToString();
                        ddlOncConEdtDirPais.SelectedValue = d["ad1DirPaisId"].ToString();
                        ddlOncConEdtDirDep.SelectedValue= d["ad1DirDptCod"].ToString()+"0000";

                        ddlOncConEdtDirPrv.Items.Clear();
                        cUtil.ListarUbigeo(ddlOncConEdtDirPrv, ddlOncConEdtDirDep.SelectedValue.Substring(0, 2), "01", "00");
                        ListItem lstP = new ListItem("Seleccione la Direccion Provincia", "0");
                        lstP.Selected = true;
                        ddlOncConEdtDirPrv.Items.Add(lstP);
                        ddlOncConEdtDirPrv.SelectedValue = d["ad1DirDptCod"].ToString()+d["ad1DirPrvCod"].ToString()+"00";

                        ddlOncConEdtDirDis.Items.Clear();
                        cUtil.ListarUbigeo(ddlOncConEdtDirDis, ddlOncConEdtDirDep.SelectedValue.Substring(0, 2), ddlOncConEdtDirPrv.SelectedValue.Substring(2, 2), "01");
                        ListItem lstD = new ListItem("Seleccione la Direccion Distrito", "0");
                        lstD.Selected = true;
                        ddlOncConEdtDirDis.Items.Add(lstD);
                        ddlOncConEdtDirDis.SelectedValue = d["ad1DirDptCod"].ToString() + d["ad1DirPrvCod"].ToString() +d["ad1DirDisCod"].ToString();

                        txtOncConEdtDirEnt.Text = d["ad1DirEnt"].ToString();
                        txtOncConEdtTelDom.Text = d["ad1TlfnoDom"].ToString();
                        txtOncConEdtTelTrab.Text = d["ad1TlfnoTrb"].ToString();
                        txtOncConEdtNroCel.Text = d["ad1NroCel"].ToString();
                        txtOncConEdtEmail.Text = d["ad1DirMail"].ToString();
                        rblOncConEdtGen.SelectedValue = d["ad1Sexo"].ToString();

                    }
                        string jss3 = "openEditarOnco();";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss3, true);
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlOncConEdtDirDep_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlOncConEdtDirPrv.Items.Clear();
            cUtil.ListarUbigeo(ddlOncConEdtDirPrv, ddlOncConEdtDirDep.SelectedValue.Substring(0, 2), "01", "00");
            ListItem lstP = new ListItem("Seleccione la Direccion Provincia", "0");
            lstP.Selected = true;
            ddlOncConEdtDirPrv.Items.Add(lstP);

            ddlOncConEdtDirDis.Items.Clear();
            ListItem lstD = new ListItem("Seleccione la Direccion Distrito", "0");
            lstD.Selected = true;
            ddlOncConEdtDirDis.Items.Add(lstD);

            //cUtil.ListarUbigeo(ddlOncConDirDis, ddlOncConDirDep.SelectedValue.Substring(0, 2), "01", "01");
        }

        protected void ddlOncConEdtDirPrv_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlOncConEdtDirDis.Items.Clear();
            cUtil.ListarUbigeo(ddlOncConEdtDirDis, ddlOncConEdtDirDep.SelectedValue.Substring(0, 2), ddlOncConEdtDirPrv.SelectedValue.Substring(2, 2), "01");
            ListItem lstD = new ListItem("Seleccione la Direccion Distrito", "0");
            lstD.Selected = true;
            ddlOncConEdtDirDis.Items.Add(lstD);
        }

        protected void btnPlan_Click(object sender, ImageClickEventArgs e)
        {
            //Afiliacion.ImageSeguro = (byte[])(dr["FilePlanSeguro"]);
            ImageButton img = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)img.NamingContainer;

            String identificador = ViewState["FileNamePlanSeguro"].ToString(); //gvr.Cells[17].Text.Trim();
            byte[] Archivo = (byte[])ViewState["FilePlanSeguro"];

            try
            {
                Response.Clear();
                MemoryStream ms = new MemoryStream(Archivo);
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + identificador);
                Response.Buffer = true;
                ms.WriteTo(Response.OutputStream);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception)
            {
                throw;
            }

        }
        protected void btnEditarAlumnos_Click(object sender, EventArgs e)
        {
            try
            {   //ACA SE EDITA *************************************************************************************
                DataTable dt = (DataTable)Session["DatosAsegurado"];
                OnlineDAO dbOnline = new OnlineDAO();
                if (hdnProductoId.Value == "1")
                {
                    Reg_Online Reg = new Reg_Online();
                    Reg.ReservadoId = Convert.ToInt32(GrvConfirmar.Rows[Convert.ToInt32(hdnFilaEdit.Value)].Cells[20].Text);
                    Reg.Bene_ApePaterno = TxtApePateEdit.Text;
                    Reg.Bene_ApeMaterno = TxtApeMateEdit.Text;
                    Reg.Bene_ApeNombres = TxtNombresEdit.Text;
                    Reg.Bene_TipoDocumento = Convert.ToInt32(DDLTipoDocEdit.SelectedValue);
                    Reg.Bene_NumDocumento = txtNumDocEdit.Text;
                    Reg.Bene_FechaNacimiento = txtFechaNacEdit.Text;
                    Reg.Bene_Seccion = txtSeccionEdit.Text;
                    Reg.UsuarioCreacion = Session["Usuario"].ToString();                   
                    Reg.Bene_Grado = ddlGradoEdit.SelectedValue;             

                    dbOnline.EditarOnline(Reg);
                    GrvConfirmar.DataSource = dbOnline.LISTAR_AFILIACION_ACCIDENTES(Session["Usuario"].ToString());
                    GrvConfirmar.DataBind();
                }
                //if (hdnProductoId.Value == "2" && ViewState["TipoAsociacion"].ToString() == "1")
                //{
                //    cUtil.EditarDetalleAseguradoRenta(dt, TxtApePateEdit.Text, TxtApeMateEdit.Text, TxtNombresEdit.Text,
                //    Convert.ToInt32(DDLTipoDocEdit.SelectedValue), txtNumDocEdit.Text, txtFechaNacEdit.Text, ddlGradoEdit.SelectedValue,
                //    txtSeccionEdit.Text, true, Convert.ToInt32(GrvConfirmar.Rows[Convert.ToInt32(hdnFilaEdit.Value)].Cells[20].Text), 1, Convert.ToInt32(hdnFilaEdit.Value));
                //    //string asegurado = txtApellidoPaterno.Text + " " + txtApellidoMaterno.Text + "," + txtNombres.Text;
                //    Session["DatosAsegurado"] = dt;
                //}
                //int index = Convert.ToInt32(hdnFilaEdit.Value);
                //Alumno alu = new Alumno();
                //alu.ApellidoPaternno = dt.Rows[index]["ApePaterno"].ToString().ToUpper();
                //alu.ApellidoMaterno = dt.Rows[index]["ApeMaterno"].ToString().ToUpper();
                //alu.Nombre = dt.Rows[index]["Nombres"].ToString().ToUpper().ToUpper();
                //alu.TipoDocumentoID = Convert.ToInt32(dt.Rows[index]["TipoDoc"]);
                //alu.NumeroDocumento = dt.Rows[index]["NumDoc"].ToString();
                //alu.FechaNacimiento = Convert.ToDateTime(dt.Rows[index]["FecNac"]);
                //alu.Sexo = Convert.ToInt32(dt.Rows[index]["Genero"]);
                //alu.UsuarioCreacion = Session["Usuario"].ToString().ToUpper();
                //alu.FechaCreacion = DateTime.Now.Date;
                //alu.Estado = true;
                //alu.Seccion = dt.Rows[index]["Seccion"].ToString();
                //alu.GradoID = Convert.ToInt32(dt.Rows[index]["Grado"]);

                ////ListaAFiliaAccidentes[index-1].Alumno = alu;
                ////Session["ListaAfiliaAccidentes"] = ListaAFiliaAccidentes;
                ////DataTable dtdA = cUtil.CrearDetalleAfiliacion();

                //DataTable dtAfiProd = (DataTable)Session["DatosAfiliaProducto"];

                //if (hdnProductoId.Value == "1")
                //{
                //    cUtil.EditarDetalleAfiliacion(dtAfiProd, (alu.ApellidoPaternno + " " + alu.ApellidoMaterno + " " + alu.Nombre).ToUpper(), "", txtNumDocEdit.Text, txtFechaNacEdit.Text, 0, Convert.ToInt32(hdnFilaEdit.Value));
                //}
                ////else if (hdnProductoId.Value == "2")
                ////{
                ////    cUtil.EditarDetalleAfiliacionRenta(dtAfiProd, (alu.ApellidoPaternno + " " + alu.ApellidoMaterno + " " + alu.Nombre).ToUpper(), "", txtNumDocEdit.Text,
                ////           txtFechaNacEdit.Text, 0, Convert.ToInt32(hdnFilaEdit.Value),
                ////           Convert.ToInt32(hdnProductoId.Value), Convert.ToInt32(ViewState["TipoAsociacion"]), 0);
                ////}
                //CargarConfirmarAfiliacion(dtAfiProd);
                //Session["DatosAfiliaProducto"] = dtAfiProd;
            }
            catch (Exception ex)
            {

            }
        }

protected void btnActualizarPadre_Click(object sender, EventArgs e)
{
    try
    {
        string UsuarioCreacion = Session["Usuario"].ToString();

        int edad = cUtil.Edad(Convert.ToDateTime(txtPadFechaNacEdit.Text));
        if (edad > 70)
        {
            string jssc = "openModalcombo();";
            lblcombo.Text = "La edad del asegurado no puede ser mayor a los  70 años";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssc, true);
            return;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (ViewState["TipoAsociacion"].ToString() == "1")
        {
                    int edadm = cUtil.Edad(Convert.ToDateTime(txtPadFechaNacEdit.Text));
                    if (edadm > 70)
                    {
                        string jssc = "openModalcombo();";
                        lblcombo.Text = "La edad del asegurado no puede ser mayor a los  70 años";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssc, true);
                        return;
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                                                
                    OnlineDAO dbAfilidos = new OnlineDAO();

                    Reg_Online Reg = new Reg_Online();
                    Reg.ReservadoId = Convert.ToInt32(GrvConfirmar.Rows[Convert.ToInt32(hdnFilaEdit.Value)].Cells[20].Text);
                    Reg.Bene_ApePaterno = txtApePatBeneficiarioEdit.Text.ToUpper();
                    Reg.Bene_ApeMaterno = txtApeMatBeneficiarioEdit.Text.ToUpper();
                    Reg.Bene_ApeNombres = txtNombreBeneficiarioEdit.Text.ToUpper();
                    Reg.Bene_TipoDocumento = Convert.ToInt32(DDLBeneficiarioTipoDocuEdit.SelectedValue);
                    Reg.Bene_NumDocumento = txtBeneficiarioNumeroDocuEdit.Text;
                    Reg.Bene_FechaNacimiento = txtBeneficiarioFechaNacEdit.Text;
                    Reg.Bene_Seccion = txtBeneficiarioSeccionEdit.Text.ToUpper();
                    Reg.UsuarioCreacion = Session["Usuario"].ToString();
                    //Reg.Bene_Sexo = rbtSexo.SelectedIndex + 1;
                    //Reg.Bene_Seccion = dt.Rows[ind]["Seccion"].ToString();                        
                    Reg.Bene_Grado = DDLBeneficiarioGradoEdit.SelectedValue;
                    Reg.Padre_Nombres = txtPadNombresEdit.Text.ToUpper();
                    Reg.Padre_ApePaterno = txtPadApePaternoEdit.Text.ToUpper();
                    Reg.Padre_ApeMaterno = txtPadApeMaternoEdit.Text.ToUpper();
                    Reg.Padre_FechaNacimiento = txtPadFechaNacEdit.Text;
                    Reg.Padre_TipoDocumento = Convert.ToInt32(DDLPadTipoDocuEdit.SelectedValue);
                    Reg.Padre_NumDocumento = txtPadNumDocEdit.Text;
                    dbAfilidos.EditarOnline(Reg);
                    GrvConfirmar.DataSource = dbAfilidos.LISTAR_AFILIACION_ONLINE(UsuarioCreacion);
                    GrvConfirmar.DataBind();
                    //DataTable dtAfiProd = (DataTable)Session["DatosAfiliaProducto"];                  
                    //CargarConfirmarAfiliacion(dtAfiProd);
                    //List<Util.AfiliaSeguroRenta> ListaAFiliaRentas = (List<Util.AfiliaSeguroRenta>)Session["ListaAfiliaRentas"];
                    //Session["DatosAfiliaProducto"] = dtAfiProd;           
                }
                    
                if (ViewState["TipoAsociacion"].ToString() == "2" && chkSegundoPadre.Checked == false)
                    {
                    OnlineDAO dbAfilidos = new OnlineDAO();

                    Reg_Online Reg = new Reg_Online();
                    Reg.ReservadoId = Convert.ToInt32(GrvConfirmar.Rows[Convert.ToInt32(hdnFilaEdit.Value)].Cells[20].Text);
                    Reg.Bene_ApePaterno = txtApePatBeneficiarioEdit.Text.ToUpper();
                    Reg.Bene_ApeMaterno = txtApeMatBeneficiarioEdit.Text.ToUpper();
                    Reg.Bene_ApeNombres = txtNombreBeneficiarioEdit.Text.ToUpper();
                    Reg.Bene_TipoDocumento = Convert.ToInt32(DDLBeneficiarioTipoDocuEdit.SelectedValue);
                    Reg.Bene_NumDocumento = txtBeneficiarioNumeroDocuEdit.Text;
                    Reg.Bene_FechaNacimiento = txtBeneficiarioFechaNacEdit.Text;
                    Reg.Bene_Seccion = txtBeneficiarioSeccionEdit.Text.ToUpper();
                    Reg.UsuarioCreacion = Session["Usuario"].ToString();
                    //Reg.Bene_Sexo = rbtSexo.SelectedIndex + 1;
                    //Reg.Bene_Seccion = dt.Rows[ind]["Seccion"].ToString();                        
                    Reg.Bene_Grado = DDLBeneficiarioGradoEdit.SelectedValue;

                    Reg.Padre_Nombres = txtPadNombresEdit.Text.ToUpper();
                    Reg.Padre_ApePaterno = txtPadApePaternoEdit.Text.ToUpper();
                    Reg.Padre_ApeMaterno = txtPadApeMaternoEdit.Text.ToUpper();
                    Reg.Padre_FechaNacimiento = txtPadFechaNacEdit.Text;
                    Reg.Padre_TipoDocumento = Convert.ToInt32(DDLPadTipoDocuEdit.SelectedValue);
                    Reg.Padre_NumDocumento = txtPadNumDocEdit.Text;
            //Reg.Padre_IdBeneficiario = Convert.ToInt32(alu.ID);                                               
            //if (rbtTipoPadre.SelectedIndex == 0) { Reg.Padre_TipoPadre = 1; }
            //else if (rbtTipoPadre.SelectedIndex == 1) { Reg.Padre_TipoPadre = 2; }
                    Reg_Online RegRS = new Reg_Online();
                    RegRS.Bene_ApePaterno = txtApePatBeneficiarioEdit.Text.ToUpper();
                    RegRS.Bene_ApeMaterno = txtApeMatBeneficiarioEdit.Text.ToUpper();
                    RegRS.Bene_ApeNombres = txtNombreBeneficiarioEdit.Text.ToUpper();
                    RegRS.Padre_Nombres = "RESERVADO";
                    RegRS.Padre_ApePaterno = "RESERVADO";
                    RegRS.Padre_ApeMaterno = "RESERVADO";
                    RegRS.UsuarioCreacion = Session["Usuario"].ToString();
                    RegRS.Bene_Seccion = txtBeneficiarioSeccionEdit.Text.ToUpper();
                    RegRS.Bene_FechaNacimiento = txtBeneficiarioFechaNacEdit.Text;
                    RegRS.Bene_Grado = DDLBeneficiarioGradoEdit.SelectedValue;
            
                    dbAfilidos.EditarOnline(Reg);
                    GrvConfirmar.DataSource = dbAfilidos.LISTAR_AFILIACION_ONLINE(UsuarioCreacion);
                    GrvConfirmar.DataBind();           
        }

        else if (ViewState["TipoAsociacion"].ToString() == "2" && chkSegundoPadre.Checked == true)
        {
            int edadm = cUtil.Edad(Convert.ToDateTime(txtPadFechaNacEdit.Text));
            if (edadm > 70)
            {
                string jssc = "openModalcombo();";
                lblcombo.Text = "La edad del asegurado no puede ser mayor a los  70 años";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssc, true);
                return;
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                                                
            OnlineDAO dbAfilidos = new OnlineDAO();

            Reg_Online Reg = new Reg_Online();
            
            Reg.ReservadoId = Convert.ToInt32(GrvConfirmar.Rows[Convert.ToInt32(hdnFilaEdit.Value)].Cells[21].Text);
            Reg.Id = Convert.ToInt32(GrvConfirmar.Rows[Convert.ToInt32(hdnFilaEdit.Value)].Cells[20].Text);
             Reg.Bene_ApePaterno = txtApePatBeneficiarioEdit.Text.ToUpper();
            Reg.Bene_ApeMaterno = txtApeMatBeneficiarioEdit.Text.ToUpper();
            Reg.Bene_ApeNombres = txtNombreBeneficiarioEdit.Text.ToUpper();
            Reg.Bene_TipoDocumento = Convert.ToInt32(DDLBeneficiarioTipoDocuEdit.SelectedValue);
            Reg.Bene_NumDocumento = txtBeneficiarioNumeroDocuEdit.Text;
            Reg.Bene_FechaNacimiento = txtBeneficiarioFechaNacEdit.Text;
            Reg.Bene_Seccion = txtBeneficiarioSeccionEdit.Text.ToUpper();
            Reg.UsuarioCreacion = Session["Usuario"].ToString();
            //Reg.Bene_Sexo = rbtSexo.SelectedIndex + 1;
            //Reg.Bene_Seccion = dt.Rows[ind]["Seccion"].ToString();                        
            Reg.Bene_Grado = DDLBeneficiarioGradoEdit.SelectedValue;

            Reg.Padre_Nombres = txtPadNombresEdit.Text.ToUpper();
            Reg.Padre_ApePaterno = txtPadApePaternoEdit.Text.ToUpper();
            Reg.Padre_ApeMaterno = txtPadApeMaternoEdit.Text.ToUpper();
            Reg.Padre_FechaNacimiento = txtPadFechaNacEdit.Text;
            Reg.Padre_TipoDocumento = Convert.ToInt32(DDLPadTipoDocuEdit.SelectedValue);
            Reg.Padre_NumDocumento = txtPadNumDocEdit.Text;


             string buscado = GrvConfirmar.Rows[Convert.ToInt32(hdnFilaEdit.Value)].Cells[0].Text;
            dbAfilidos.EditarOnlineRenta2(Reg ,buscado);
            GrvConfirmar.DataSource = dbAfilidos.LISTAR_AFILIACION_ONLINE_ASOCIA2(UsuarioCreacion);
            GrvConfirmar.DataBind();
            //DataTable dtAfiProd = (DataTable)Session["DatosAfiliaProducto"];                  
            //CargarConfirmarAfiliacion(dtAfiProd);
            //List<Util.AfiliaSeguroRenta> ListaAFiliaRentas = (List<Util.AfiliaSeguroRenta>)Session["ListaAfiliaRentas"];
            //Session["DatosAfiliaProducto"] = dtAfiProd;           
        }
    }
    catch (Exception ex)
    {

    }
   
}

        protected void btnOncEdtGuarDatosAseg_Click(object sender, EventArgs e) {

        }
        protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                String UsuarioRegistro = Session["Usuario"].ToString();

                ImageButton btn = (ImageButton)sender;
                GridViewRow Row = (GridViewRow)btn.NamingContainer;

                hdnFilaEdit.Value = Convert.ToString(Row.RowIndex);
                int index = Convert.ToInt32(Row.RowIndex);
               
                if (hdnProductoId.Value == "1")
                {
                    OnlineDAO db = new OnlineDAO();
                    db.EliminarRegistroOnline2(Convert.ToInt32(GrvConfirmar.Rows[index].Cells[20].Text));
                    GrvConfirmar.DataSource = db.LISTAR_AFILIACION_ACCIDENTES(Session["Usuario"].ToString());
                    GrvConfirmar.DataBind();
                }                
                else if (hdnProductoId.Value == "2" && ViewState["TipoAsociacion"].ToString() == "1")
                {
                    OnlineDAO db = new OnlineDAO();
                    db.EliminarRegistroOnline2(Convert.ToInt32(GrvConfirmar.Rows[index].Cells[20].Text));
                    GrvConfirmar.DataSource = db.LISTAR_AFILIACION_ONLINE(UsuarioRegistro);
                    GrvConfirmar.DataBind();
                }
                else if (hdnProductoId.Value == "2" && ViewState["TipoAsociacion"].ToString() == "2" && chkSegundoPadre.Checked == false)
                {
                    OnlineDAO db = new OnlineDAO();
                    db.EliminarRegistroOnline(Convert.ToInt32(GrvConfirmar.Rows[index].Cells[20].Text));
                    GrvConfirmar.DataSource = db.LISTAR_AFILIACION_ONLINE(UsuarioRegistro);
                    GrvConfirmar.DataBind();
                }
                else if (hdnProductoId.Value == "2" && ViewState["TipoAsociacion"].ToString() == "2" && chkSegundoPadre.Checked == true)
                {
                    OnlineDAO db = new OnlineDAO();
                    db.EliminarRegistroOnline2(Convert.ToInt32(GrvConfirmar.Rows[index].Cells[20].Text));
                    GrvConfirmar.DataSource = db.LISTAR_AFILIACION_ONLINE_ASOCIA2(UsuarioRegistro);
                    GrvConfirmar.DataBind();
                }
                else if (hdnProductoId.Value == "3" || hdnProductoId.Value == "7")
                {
                    OnlineDAO db = new OnlineDAO();
                    db.EliminarRegistroOnline2(Convert.ToInt32(GrvConfirmar.Rows[index].Cells[20].Text));
                    GrvConfirmar.DataSource = db.LISTAR_AFILIACION_ONLINE_ASOCIA2(UsuarioRegistro);
                    GrvConfirmar.DataBind();
                }
            }
            catch(Exception ex)
            {
            }
        }
    

        void CargarConfirmarAfiliacion(DataTable dt , string UsuarioCreacion)
        {
            OnlineDAO db = new OnlineDAO();
            DataSet ds = db.LISTAR_AFILIACION_ONLINE(UsuarioCreacion);
            GrvConfirmar.DataSource = ds;
            GrvConfirmar.DataBind();
        }

        void CargarConfirmarAfiliacionAccidentes(DataTable dt, string UsuarioRegistro)
        {
            OnlineDAO db = new OnlineDAO();
            DataSet ds = db.LISTAR_AFILIACION_ACCIDENTES(UsuarioRegistro);
            GrvConfirmar.DataSource = ds;
            GrvConfirmar.DataBind();
        }

        void CargarConfirmarAfiliacionAsocia2(DataTable dt)
        {
            OnlineDAO db = new OnlineDAO();
            DataSet ds = db.LISTAR_AFILIACION_ONLINE_ASOCIA2(Session["Usuario"].ToString());
            GrvConfirmar.DataSource = ds;
            GrvConfirmar.DataBind();
        }

        protected void BtnAsegOtro_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["DatosAsegurado"];
            int index = Convert.ToInt32(dt.Rows.Count - 1);
            List<Util.AfiliaSeguroRenta> ListaAFiliaRentas = (List<Util.AfiliaSeguroRenta>)Session["ListaAfiliaRentas"];

            try
            {
                if (hdnProductoId.Value == "1") //Accidentes
                {
                    Alumno alu = new Alumno();
                    //Guardar datos de alumno o beneficiario
                    alu.ApellidoPaternno = dt.Rows[index]["ApePaterno"].ToString();
                    alu.ApellidoMaterno = dt.Rows[index]["ApeMaterno"].ToString();
                    alu.Nombre = dt.Rows[index]["Nombres"].ToString();
                    alu.TipoDocumentoID = Convert.ToInt32(dt.Rows[index]["TipoDoc"]);
                    alu.NumeroDocumento = dt.Rows[index]["NumDoc"].ToString();
                    alu.FechaNacimiento = Convert.ToDateTime(dt.Rows[index]["FecNac"]);
                    alu.Sexo = Convert.ToInt32(dt.Rows[index]["Genero"]);
                    alu.UsuarioCreacion = Session["Usuario"].ToString();
                    alu.FechaCreacion = DateTime.Now.Date;
                    alu.Estado = false;
                    alu.Seccion = dt.Rows[index]["Seccion"].ToString();
                    alu.GradoID = Convert.ToInt32(dt.Rows[index]["Grado"]);
                    alu.Carrera = "";
                    alu.CodigoAlumno = "";

                    //SEGURO DE ACCIDENTES                            
                    using (AfiliacionDAO dbAfiliacion = new AfiliacionDAO())
                    {
                        AfiliacionSeguro afiseguro = new AfiliacionSeguro();
                        afiseguro.InstitucionEducativaID = Convert.ToInt32(hdnInstitucionEducativa.Value);
                        afiseguro.CodigoPago = "";
                        afiseguro.FechaCreacion = DateTime.Now.Date;
                        afiseguro.UsuarioCreacion = Session["Usuario"].ToString();
                        afiseguro.Estado = true;

                        List<Util.AfiliaSeguroAccidentes> ListaAFiliaAccidentes = (List<Util.AfiliaSeguroAccidentes>)Session["ListaAfiliaAccidentes"];

                        Util.AfiliaSeguroAccidentes AfiliaAccidente = new Util.AfiliaSeguroAccidentes();
                        AfiliaAccidente.Alumno = alu;
                        AfiliaAccidente.AfiliacionSeguro = afiseguro;
                        AfiliaAccidente.Producto = HttpUtility.HtmlDecode(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[4].Text);
                        AfiliaAccidente.Prima = GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[6].Text;
                        AfiliaAccidente.Aseguradora = HttpUtility.HtmlDecode(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[7].Text);
                        AfiliaAccidente.MonedaId = Convert.ToInt32(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[13].Text);
                        AfiliaAccidente.NombreColegio = HttpUtility.HtmlDecode(Convert.ToString(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[14].Text));
                        AfiliaAccidente.IdProducto = Convert.ToInt32(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[15].Text);
                        AfiliaAccidente.IdAsociacion = Convert.ToInt32(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[16].Text);

                        ListaAFiliaAccidentes.Add(AfiliaAccidente);
                        Session["ListaAfiliaAccidentes"] = ListaAFiliaAccidentes;
                    }
                }
                //SEGURO DE RENTAS
                else if (hdnProductoId.Value == "2")
                {
                    if (ViewState["TipoAsociacion"].ToString() == "1")
                    {

                        Alumno alu1 = new Alumno();
                        alu1.ApellidoPaternno = dt.Rows[index]["ApePaterno"].ToString();
                        alu1.ApellidoMaterno = dt.Rows[index]["ApeMaterno"].ToString();
                        alu1.Nombre = dt.Rows[index]["Nombres"].ToString();
                        alu1.TipoDocumentoID = Convert.ToInt32(dt.Rows[index]["TipoDoc"]);
                        alu1.NumeroDocumento = dt.Rows[index]["NumDoc"].ToString();
                        alu1.FechaNacimiento = Convert.ToDateTime(dt.Rows[index]["FecNac"]);
                        alu1.Sexo = Convert.ToInt32(dt.Rows[index]["Genero"]);
                        alu1.UsuarioCreacion = Session["Usuario"].ToString();
                        alu1.FechaCreacion = DateTime.Now.Date;
                        alu1.Estado = true;
                        alu1.Seccion = dt.Rows[index]["Seccion"].ToString();
                        alu1.GradoID = Convert.ToInt32(dt.Rows[index]["Grado"]);
                        alu1.Carrera = "";
                        alu1.CodigoAlumno = "";

                        Padre padre = new Padre();
                        //Padre padre2 = null;
                        padre.Nombre = txtNomPadre.Text;
                        padre.ApellidoPaterno = txtApePadre.Text;
                        padre.ApellidoMaterno = txtApeMatPadre.Text;
                        padre.FechaNacimiento = Convert.ToDateTime(txtFechaNacPadre.Text);
                        padre.TipoDocumentoID = Convert.ToInt32(ddlTipoDocuPadre.SelectedValue);
                        padre.NumeroDocumento = txtNumDocPadre.Text;
                        padre.BeneficiarioID = Convert.ToInt32(alu1.ID);
                        padre.UsuarioCreacion = Session["Usuario"].ToString();
                        padre.FechaCreacion = DateTime.Now.Date;
                        padre.Estado = true;

                        if (rbtTipoPadre.SelectedIndex == 0)
                        {
                            padre.TipoPadreID = 1;
                        }
                        else if (rbtTipoPadre.SelectedIndex == 1)
                        {
                            padre.TipoPadreID = 2;
                        }

                        AfiliacionSeguro afiseguro = new AfiliacionSeguro();
                        afiseguro.InstitucionEducativaID = Convert.ToInt32(hdnInstitucionEducativa.Value);
                        afiseguro.CodigoPago = "";
                        afiseguro.FechaCreacion = DateTime.Now.Date;
                        afiseguro.UsuarioCreacion = Session["Usuario"].ToString();
                        afiseguro.Estado = true;

                        Util.AfiliaSeguroRenta AfiliaRenta = new Util.AfiliaSeguroRenta();
                        AfiliaRenta.Id = 1;
                        AfiliaRenta.Alumno = alu1;
                        AfiliaRenta.AfiliacionSeguro = afiseguro;
                        AfiliaRenta.Padre = padre;
                        AfiliaRenta.Producto = HttpUtility.HtmlDecode(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[4].Text);
                        AfiliaRenta.Prima = GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[6].Text;
                        AfiliaRenta.Aseguradora = HttpUtility.HtmlDecode(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[7].Text);
                        AfiliaRenta.MonedaId = Convert.ToInt32(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[13].Text);
                        AfiliaRenta.NombreColegio = HttpUtility.HtmlDecode(Convert.ToString(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[14].Text));
                        AfiliaRenta.IdProducto = Convert.ToInt32(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[15].Text);
                        AfiliaRenta.IdAsociacion = Convert.ToInt32(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[16].Text);
                        ListaAFiliaRentas.Add(AfiliaRenta);

                        Session["ListaAfiliaRentas"] = ListaAFiliaRentas;
                        
                    }
                }
            }
            catch (Exception ex)
            {
            }

            if (ViewState["TipoAsociacion"].ToString() == "2" && chkSegundoPadre.Checked == false)
            {           
                
            }

            if (ViewState["TipoAsociacion"].ToString() == "2" && chkSegundoPadre.Checked == true)
            {
            }

            MVWAfiliacion.ActiveViewIndex = 2;
            Limpiar();
            ViewState["RegNuevoAseg"] = "SI";
            // pnlDatosPadre.Enabled = false;
            //  pnlDatoMadre.Enabled = false;
        }

        void Limpiar()
        {
            txtApellidoMaterno.Text = "";
            txtApellidoPaterno.Text = "";
            txtNombres.Text = "";
            txtFechaNacimiento.Text = "";
            DDLGrado.SelectedValue = "0";
            txtSeccion.Text = "";
            DDLTipoDocumento.SelectedValue = "0";
            txtNumeroDocumento.Text = "";

            //TxtApeMateEdit.Text = "";
            //TxtApePateEdit.Text = "";
            //txtFechaNacEdit.Text = "";
            //DDLTipoDocEdit.SelectedValue = "0";

            //txtSeccionEdit.Text = "";
            //txtNumDocEdit.Text = "";
            //DDLTipoDocEdit.SelectedIndex = 0;
            //txtApeMatMadre.Text = "";
            //txtNumDocPadre.Text = "";
            //txtNumDocMadre.Text = "";
            //rbtParentescoMadre.SelectedIndex = 0;
            ////rbtSexoEdit.SelectedIndex = 0;
            //ddlTipoDocuPadre.SelectedIndex = 0;

        }
        void LimpiarInfoPadres()
        {
            txtNombreMadre.Text = "";
            txtNomPadre.Text = "";
            txtApeMatMadre.Text = "";
            txtApeMatPadre.Text = "";
            txtApePadre.Text = "";
            txtApePatMadre.Text = "";
            ddlTipoDocuPadre.SelectedValue = "0";
            DDLTipoDocMadre.SelectedValue = "0";
            txtNumDocPadre.Text = "";
            txtNumDocMadre.Text = "";
            rbtParentescoMadre.SelectedIndex = 1;
            rbtTipoPadre.SelectedIndex = 0;
            rbtSexo.SelectedIndex = 0;
            //rbtSexoEdit.SelectedIndex = 0;            
            DDLTipoDocEdit.SelectedValue = "0";
            DDLTipoDocEdit.SelectedValue = "0";
            TxtApeMateEdit.Text = "";
            TxtApePateEdit.Text = "";
            txtPadApePaternoEdit.Text = "";
            txtPadApeMaternoEdit.Text = "";
            txtPadNombresEdit.Text = "";
            txtPadNumDocEdit.Text = "";
            txtPadFechaNacEdit.Text = "";

            TxtNombresEdit.Text = "";
            txtBeneficiarioFechaNacEdit.Text = "";
            txtBeneficiarioNumeroDocuEdit.Text = "";
            txtSeccionEdit.Text = "";
            txtBeneficiarioSeccionEdit.Text = "";

            txtSeccionEdit.Text = "";
            txtNumDocEdit.Text = "";
            txtFechaNacEdit.Text = "";

            //Datos Oncologico
            //INPUTS DATOS TITULAR
            txtOncTitApePat.Text = "";
            txtOncTitApeMat.Text = "";
            txtOncTitNom.Text = "";
            ddlOncTitTipoDoc.SelectedValue = "0";
            txtOncTitNroDoc.Text = "";
            txtOncTitFecNac.Text = "";
            ddlOncTitGrado.SelectedValue = "0";

            //INPUTS DATOS CONTRATANTE
            txtOncConApePat.Text = "";
            txtOncConApeMat.Text = "";
            txtOncConNom.Text = "";
            ddlOncConTipoDoc.SelectedValue = "0";
            txtOncConNroDoc.Text = "";
            txtOncConFecNac.Text = "";
            txtOncConPaisNac.Text = "PERU";
            ddlOncConEstcivil.SelectedValue = "0";
            ddlOncConDirPais.SelectedValue = "0";
            ddlOncConDirDep.SelectedValue = "0";
            ddlOncConDirPrv.SelectedValue = "0";
            ddlOncConDirDis.SelectedValue = "0";
            txtOncConDirEnt.Text = "";
            txtOncConTelDom.Text = "";
            txtOncConTelTrab.Text = "";
            txtOncConNroCel.Text = "";
            txtOncConEmail.Text = "";


            rblDscPrg.Items[0].Selected = false;
            rblDscPrg.Items[1].Selected = false;
            rblDscPrgFuma.Items[0].Selected = false;
            rblDscPrgFuma.Items[1].Selected = false;
        }
        protected void BtnFinalizar_Click(object sender, EventArgs e)
        {
            if (GrvConfirmar.Rows.Count == 0)
            {
                string jssMensaje = "openModal();";
                txtmensaje.Text = "¡ No existen registros disponibles ! , !Debe agregar un nuevo asegurado!";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssMensaje, true);
                return;

            }


            if (chkAcepto.Checked == false)
            {
                string msgConfirm = "openConfirm();";
                lblTitleConfirm.Text = "Hermes | Seguros";
                lblmsgConfirm.Text = "Debe aceptar el contrato de afiliación...";
                btnConfirmar.Visible = false;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", msgConfirm, true);
            }
            else
            {
                string jss = "openConfirm();";
                lblTitleConfirm.ForeColor = System.Drawing.Color.Gray;
                lblmsgConfirm.ForeColor = System.Drawing.Color.Gray;
                lblTitleConfirm.Text = "Confirmación";
                lblmsgConfirm.Text = "Verifique que los datos sean correctamente ingresados. ¿Desea confirmar el registro?";
                btnConfirmar.Visible = true;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
            }
        }

        //int ifiIla = 0;
        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            List<Util.AfiliaSeguroRenta> ListaAFiliaRentas = (List<Util.AfiliaSeguroRenta>)Session["ListaAfiliaRentas"];
            List<Util.AfiliaSeguroAccidentes> ListaAFiliaAccidentes = (List<Util.AfiliaSeguroAccidentes>)Session["ListaAfiliaAccidentes"];
            List<Util.AfiliaSeguroOncologico> ListaAFiliaOncologico = (List<Util.AfiliaSeguroOncologico>)Session["ListaAfiliaOncologico"];


            List<EnviaEmailSeguro> ListaAfiliados = new List<EnviaEmailSeguro>();
            try
            {
                string UsuarioCreacion = Session["Usuario"].ToString();
                string NombreUsuario = "";
                AfiliacionDAO dbAfiliacion = new AfiliacionDAO();
                DataTable dt = (DataTable)Session["DatosAsegurado"];

                if (hdnProductoId.Value == "1") //Accidentes
                {
                    #region Accidentes
                    OnlineDAO dbO = new OnlineDAO();
                    DataSet ds = dbO.LISTAR_AFILIADOS_ACCIDENTES(UsuarioCreacion);
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            Alumno alu1 = new Alumno();

                            alu1.ApellidoPaternno = item["bene_ApePaterno"].ToString();
                            alu1.ApellidoMaterno = item["bene_ApeMaterno"].ToString();
                            alu1.Nombre = item["Bene_ApeNombres"].ToString();
                            alu1.TipoDocumentoID = Convert.ToInt32(item["Bene_TipoDocumento"]);
                            alu1.NumeroDocumento = item["Bene_NumDocumento"].ToString();
                            alu1.FechaNacimiento = Convert.ToDateTime(item["Bene_FechaNacimiento"]);
                            alu1.Sexo = Convert.ToInt32(item["Bene_sexo"]);
                            alu1.UsuarioCreacion = Session["Usuario"].ToString();
                            alu1.FechaCreacion = DateTime.Now.Date;
                            alu1.Estado = true;
                            alu1.Seccion = item["Bene_Seccion"].ToString();
                            alu1.GradoID = Convert.ToInt32(item["Bene_Grado"]);
                            alu1.Carrera = "";

                            AfiliacionSeguro afiseguro1 = new AfiliacionSeguro();
                            afiseguro1.InstitucionEducativaID = Convert.ToInt32(hdnInstitucionEducativa.Value);
                            afiseguro1.CodigoPago = "";
                            afiseguro1.FechaCreacion = DateTime.Now.Date;
                            afiseguro1.UsuarioCreacion = Session["Usuario"].ToString();
                            afiseguro1.Estado = true;

                            Util.AfiliaSeguroAccidentes AfiliaAccidente = new Util.AfiliaSeguroAccidentes();
                            AfiliaAccidente.AfiliacionSeguro = afiseguro1;
                            AfiliaAccidente.Producto = HttpUtility.HtmlDecode(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[4].Text);
                            AfiliaAccidente.Prima = GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[6].Text;
                            AfiliaAccidente.Aseguradora = HttpUtility.HtmlDecode(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[7].Text);
                            AfiliaAccidente.MonedaId = Convert.ToInt32(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[13].Text);
                            AfiliaAccidente.NombreColegio = HttpUtility.HtmlDecode(Convert.ToString(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[14].Text));
                            AfiliaAccidente.IdProducto = Convert.ToInt32(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[15].Text);
                            AfiliaAccidente.IdAsociacion = Convert.ToInt32(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[16].Text);
                            AfiliaAccidente.Alumno = alu1;
                            ListaAFiliaAccidentes.Add(AfiliaAccidente);                            
                        }
                        Session["ListaAfiliaAccidentes"] = ListaAFiliaAccidentes;
                    }
                    #endregion
                }
                //RENTAS
                if (hdnProductoId.Value == "2")
                {
                    #region renta_1
                    if (ViewState["TipoAsociacion"].ToString() == "1")
                    {
                        OnlineDAO dbO = new OnlineDAO();
                        DataSet ds = dbO.LISTAR_AFILIACION_RENTAS(UsuarioCreacion);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow item in ds.Tables[0].Rows)
                            {
                                Alumno alu1 = new Alumno();
                                alu1.ApellidoPaternno = item["bene_ApePaterno"].ToString();
                                alu1.ApellidoMaterno = item["bene_ApeMaterno"].ToString();
                                alu1.Nombre = item["Bene_ApeNombres"].ToString();
                                alu1.TipoDocumentoID = Convert.ToInt32(item["Bene_TipoDocumento"]);
                                alu1.NumeroDocumento = item["Bene_NumDocumento"].ToString();
                                alu1.FechaNacimiento = Convert.ToDateTime(item["Bene_FechaNacimiento"]);
                                alu1.Sexo = Convert.ToInt32(item["Bene_sexo"]);
                                alu1.UsuarioCreacion = Session["Usuario"].ToString();
                                alu1.FechaCreacion = DateTime.Now.Date;
                                alu1.Estado = true;
                                alu1.Seccion = item["Bene_Seccion"].ToString();
                                alu1.GradoID = Convert.ToInt32(item["Bene_Grado"]);
                                alu1.Carrera = "";

                                Padre padre1 = new Padre();
                                padre1.Nombre = item["Padre_Nombres"].ToString();
                                padre1.ApellidoPaterno = item["Padre_ApePaterno"].ToString();
                                padre1.ApellidoMaterno = item["Padre_ApeMaterno"].ToString();
                                padre1.FechaNacimiento =Convert.ToDateTime( item["Padre_FechaNacimiento"]);
                                padre1.TipoDocumentoID =Convert.ToInt32( item["Padre_TipoDocumento"]);
                                padre1.NumeroDocumento = item["Padre_NumDocumento"].ToString();
                                padre1.UsuarioCreacion = Session["Usuario"].ToString();
                                padre1.TipoPadreID =Convert.ToInt32( item["Padre_TipoPadre"]);

                                TipoPadre tipopadre = new TipoPadre();
                                tipopadre.Nombre = item["NombreTipoPadre"].ToString();
                                padre1.TipoPadre = tipopadre;

                                padre1.FechaCreacion = DateTime.Now.Date;
                                padre1.Estado = true;                        

                                AfiliacionSeguro afiseguro1 = new AfiliacionSeguro();
                                afiseguro1.InstitucionEducativaID = Convert.ToInt32(hdnInstitucionEducativa.Value);
                                afiseguro1.CodigoPago = "";
                                afiseguro1.FechaCreacion = DateTime.Now.Date;
                                afiseguro1.UsuarioCreacion = Session["Usuario"].ToString();
                                afiseguro1.Estado = true;

                                Util.AfiliaSeguroRenta AfiliaRenta = new Util.AfiliaSeguroRenta();
                                AfiliaRenta.AfiliacionSeguro = afiseguro1;
                                AfiliaRenta.Producto = HttpUtility.HtmlDecode(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[4].Text);
                                AfiliaRenta.Prima = GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[6].Text;
                                AfiliaRenta.Aseguradora = HttpUtility.HtmlDecode(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[7].Text);
                                AfiliaRenta.MonedaId = Convert.ToInt32(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[13].Text);
                                AfiliaRenta.NombreColegio = HttpUtility.HtmlDecode(Convert.ToString(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[14].Text));
                                AfiliaRenta.IdProducto = Convert.ToInt32(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[15].Text);
                                AfiliaRenta.IdAsociacion = Convert.ToInt32(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[16].Text);
                                AfiliaRenta.Alumno = alu1;
                                AfiliaRenta.Padre = padre1;
                                ListaAFiliaRentas.Add(AfiliaRenta);
                            }                            
                        }
                            Session["ListaAfiliaRentas"] = ListaAFiliaRentas;
                        }                    
                    #endregion
                    #region  Asociacion2_1
                    if (ViewState["TipoAsociacion"].ToString() == "2" && chkSegundoPadre.Checked == false)
                    {
                        OnlineDAO dbO = new OnlineDAO();
                        DataSet ds = dbO.LISTAR_AFILIACION_RENTAS(UsuarioCreacion);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow item in ds.Tables[0].Rows)
                            {
                                Alumno alu = new Alumno();
                                Padre padre = new Padre();
                                if (Convert.ToInt32(item["TipoRegistro"]) == 1)
                                {
                                    
                                    alu.ApellidoPaternno = item["bene_ApePaterno"].ToString();
                                    alu.ApellidoMaterno = item["bene_ApeMaterno"].ToString();
                                    alu.Nombre = item["Bene_ApeNombres"].ToString();
                                    alu.TipoDocumentoID = Convert.ToInt32(item["Bene_TipoDocumento"]);
                                    alu.NumeroDocumento = item["Bene_NumDocumento"].ToString();
                                    alu.FechaNacimiento = Convert.ToDateTime(item["Bene_FechaNacimiento"]);
                                    alu.Sexo = Convert.ToInt32(item["Bene_sexo"]);
                                    alu.UsuarioCreacion = Session["Usuario"].ToString();
                                    alu.FechaCreacion = DateTime.Now.Date;
                                    alu.Estado = true;
                                    alu.Seccion = item["Bene_Seccion"].ToString();
                                    alu.GradoID = Convert.ToInt32(item["Bene_Grado"]);
                                    alu.Carrera = "";
                                
                                    padre.Nombre = item["Padre_Nombres"].ToString();
                                    padre.ApellidoPaterno = item["Padre_ApePaterno"].ToString();
                                    padre.ApellidoMaterno = item["Padre_ApeMaterno"].ToString();
                                    padre.FechaNacimiento = Convert.ToDateTime(item["Padre_FechaNacimiento"]);
                                    padre.TipoDocumentoID = Convert.ToInt32(item["Padre_TipoDocumento"]);
                                    padre.NumeroDocumento = item["Padre_NumDocumento"].ToString();
                                    padre.UsuarioCreacion = Session["Usuario"].ToString();
                                    padre.TipoPadreID = Convert.ToInt32(item["Padre_TipoPadre"]);

                                    TipoPadre tipopadre = new TipoPadre();
                                    tipopadre.Nombre = item["NombreTipoPadre"].ToString();
                                    padre.TipoPadre=tipopadre;

                                    padre.FechaCreacion = DateTime.Now.Date;
                                    padre.Estado = true;
                                }
                                else if (Convert.ToInt32(item["TipoRegistro"]) == 2)
                                {
                                    
                                    alu.ApellidoPaternno = item["bene_ApePaterno"].ToString();
                                    alu.ApellidoMaterno = item["bene_ApeMaterno"].ToString();
                                    alu.Nombre = item["Bene_ApeNombres"].ToString();
                                    alu.TipoDocumentoID = Convert.ToInt32(item["Bene_TipoDocumento"]);
                                    alu.NumeroDocumento = item["Bene_NumDocumento"].ToString();
                                    alu.FechaNacimiento = Convert.ToDateTime(item["Bene_FechaNacimiento"]);
                                    alu.Sexo = Convert.ToInt32(item["Bene_sexo"]);
                                    alu.UsuarioCreacion = Session["Usuario"].ToString();
                                    alu.FechaCreacion = DateTime.Now.Date;
                                    alu.Estado = true;
                                    alu.Seccion = item["Bene_Seccion"].ToString();
                                    alu.GradoID = Convert.ToInt32(item["Bene_Grado"]);
                                    alu.Carrera = "";
                                  
                                    padre.Nombre = item["Padre_Nombres"].ToString();
                                    padre.ApellidoPaterno = item["Padre_ApePaterno"].ToString();
                                    padre.ApellidoMaterno = item["Padre_ApeMaterno"].ToString();
                                 //   padre1.FechaNacimiento = Convert.ToDateTime(item["Padre_FechaNacimiento"]);
                                 //   padre1.TipoDocumentoID = Convert.ToInt32(item["Padre_TipoDocumento"]);
                                 //   padre1.NumeroDocumento = item["Padre_NumDocumento"].ToString();
                                    padre.UsuarioCreacion = Session["Usuario"].ToString();
                                   // padre1.TipoPadreID = Convert.ToInt32(item["Padre_TipoPadre"]);
                                    padre.FechaCreacion = DateTime.Now.Date;
                                    padre.Estado = true;
                                }


                                AfiliacionSeguro afiseguro1 = new AfiliacionSeguro();
                                afiseguro1.InstitucionEducativaID = Convert.ToInt32(hdnInstitucionEducativa.Value);
                                afiseguro1.CodigoPago = "";
                                afiseguro1.FechaCreacion = DateTime.Now.Date;
                                afiseguro1.UsuarioCreacion = Session["Usuario"].ToString();
                                afiseguro1.Estado = true;

                                Util.AfiliaSeguroRenta AfiliaRenta = new Util.AfiliaSeguroRenta();
                                AfiliaRenta.AfiliacionSeguro = afiseguro1;
                                AfiliaRenta.Producto = HttpUtility.HtmlDecode(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[4].Text);
                                AfiliaRenta.Prima = GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[6].Text;
                                AfiliaRenta.Aseguradora = HttpUtility.HtmlDecode(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[7].Text);
                                AfiliaRenta.MonedaId = Convert.ToInt32(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[13].Text);
                                AfiliaRenta.NombreColegio = HttpUtility.HtmlDecode(Convert.ToString(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[14].Text));
                                AfiliaRenta.IdProducto = Convert.ToInt32(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[15].Text);
                                AfiliaRenta.IdAsociacion = Convert.ToInt32(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[16].Text);
                                AfiliaRenta.Alumno = alu;
                                AfiliaRenta.Padre = padre;
                                ListaAFiliaRentas.Add(AfiliaRenta);
                            }
                        }
                        Session["ListaAfiliaRentas"] = ListaAFiliaRentas;
                    }
                    #endregion
                    #region  Asociacion2_2
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewState["TipoAsociacion"].ToString() == "2" && chkSegundoPadre.Checked == true)
                    {
                        OnlineDAO dbO = new OnlineDAO();
                        DataSet ds = dbO.LISTAR_AFILIACION_RENTAS(UsuarioCreacion);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow item in ds.Tables[0].Rows)
                            {
                                   Alumno alu = new Alumno();
                                   Padre padre = new Padre();                                
                                    alu.ApellidoPaternno = item["bene_ApePaterno"].ToString();
                                    alu.ApellidoMaterno = item["bene_ApeMaterno"].ToString();
                                    alu.Nombre = item["Bene_ApeNombres"].ToString();
                                    alu.TipoDocumentoID = Convert.ToInt32(item["Bene_TipoDocumento"]);
                                    alu.NumeroDocumento = item["Bene_NumDocumento"].ToString();
                                    alu.FechaNacimiento = Convert.ToDateTime(item["Bene_FechaNacimiento"]);
                                    alu.Sexo = Convert.ToInt32(item["Bene_sexo"]);
                                    alu.UsuarioCreacion = Session["Usuario"].ToString();
                                    alu.FechaCreacion = DateTime.Now.Date;
                                    alu.Estado = true;
                                    alu.Seccion = item["Bene_Seccion"].ToString();
                                    alu.GradoID = Convert.ToInt32(item["Bene_Grado"]);
                                    alu.Carrera = "";

                                    padre.Nombre = item["Padre_Nombres"].ToString();
                                    padre.ApellidoPaterno = item["Padre_ApePaterno"].ToString();
                                    padre.ApellidoMaterno = item["Padre_ApeMaterno"].ToString();
                                    padre.FechaNacimiento = Convert.ToDateTime(item["Padre_FechaNacimiento"]);
                                    padre.TipoDocumentoID = Convert.ToInt32(item["Padre_TipoDocumento"]);
                                    padre.NumeroDocumento = item["Padre_NumDocumento"].ToString();
                                    padre.UsuarioCreacion = Session["Usuario"].ToString();
                                    padre.TipoPadreID = Convert.ToInt32(item["Padre_TipoPadre"]);

                                    TipoPadre tipopadre = new TipoPadre();
                                    tipopadre.Nombre= item["NombreTipoPadre"].ToString();
                                    padre.TipoPadre=tipopadre;

                                    padre.FechaCreacion = DateTime.Now.Date;
                                    padre.Estado = true;
                                                                
                                AfiliacionSeguro afiseguro1 = new AfiliacionSeguro();
                                afiseguro1.InstitucionEducativaID = Convert.ToInt32(hdnInstitucionEducativa.Value);
                                afiseguro1.CodigoPago = "";
                                afiseguro1.FechaCreacion = DateTime.Now.Date;
                                afiseguro1.UsuarioCreacion = Session["Usuario"].ToString();
                                afiseguro1.Estado = true;

                                Util.AfiliaSeguroRenta AfiliaRenta = new Util.AfiliaSeguroRenta();
                                AfiliaRenta.AfiliacionSeguro = afiseguro1;
                                AfiliaRenta.Producto = HttpUtility.HtmlDecode(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[4].Text);
                                AfiliaRenta.Prima = GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[6].Text;
                                AfiliaRenta.Aseguradora = HttpUtility.HtmlDecode(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[7].Text);
                                AfiliaRenta.MonedaId = Convert.ToInt32(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[13].Text);
                                AfiliaRenta.NombreColegio = HttpUtility.HtmlDecode(Convert.ToString(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[14].Text));
                                AfiliaRenta.IdProducto = Convert.ToInt32(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[15].Text);
                                AfiliaRenta.IdAsociacion = Convert.ToInt32(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[16].Text);
                                AfiliaRenta.Alumno = alu;
                                AfiliaRenta.Padre = padre;
                                ListaAFiliaRentas.Add(AfiliaRenta);
                            }
                        }
                        Session["ListaAfiliaRentas"] = ListaAFiliaRentas;
                    }
                    #endregion
                }

                if (hdnProductoId.Value == "3" || hdnProductoId.Value == "7")
                {
                    #region oncologico
                    OnlineDAO dbO = new OnlineDAO();
                    DataSet ds = dbO.LISTAR_AFILIACION_ONCOLOGICO(UsuarioCreacion);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow item = ds.Tables[0].Rows[0];
                        
                        Alumno alu = new Alumno();
                        Padre padre = null;
                        PersonaDatosAdic personadatosadic = new PersonaDatosAdic();
                        PersonaPreg personapreg = new PersonaPreg();

                        alu.ApellidoPaternno = item["bene_ApePaterno"].ToString();
                        alu.ApellidoMaterno = item["bene_ApeMaterno"].ToString();
                        alu.Nombre = item["Bene_ApeNombres"].ToString();
                        alu.TipoDocumentoID = Convert.ToInt32(item["Bene_TipoDocumento"]);
                        alu.NumeroDocumento = item["Bene_NumDocumento"].ToString();
                        alu.FechaNacimiento = Convert.ToDateTime(item["Bene_FechaNacimiento"]);
                        alu.GradoID = Convert.ToInt32(item["Bene_Grado"]);
                        alu.Sexo = Convert.ToInt32(item["Bene_sexo"]);
                        alu.UsuarioCreacion = Session["Usuario"].ToString();
                        alu.FechaCreacion = DateTime.Now.Date;
                        alu.asociacionid=Convert.ToInt32(item["IdAsociacion"]);
                        alu.Estado = true;

                        //string ASegMayorEdad = "SI";
                        if (Convert.ToInt32(hdnOncTitFecNac.Value)<18)
                        {
                            //ASegMayorEdad = "NO";
                            padre = new Padre();

                            padre.Nombre = item["Padre_Nombres"].ToString();
                            padre.ApellidoPaterno = item["Padre_ApePaterno"].ToString();
                            padre.ApellidoMaterno = item["Padre_ApeMaterno"].ToString();
                            padre.TipoDocumentoID = Convert.ToInt32(item["Padre_TipoDocumento"]);
                            padre.NumeroDocumento = item["Padre_NumDocumento"].ToString();
                            padre.FechaNacimiento = Convert.ToDateTime(item["Padre_FechaNacimiento"]);
                            padre.UsuarioCreacion = Session["Usuario"].ToString();
                            padre.TipoPadreID = Convert.ToInt32(item["Padre_TipoPadre"]);
                            padre.FechaCreacion = DateTime.Now.Date;
                            padre.Estado = true;
                        }

                        personadatosadic.EstCivilId= Convert.ToInt32(item["ad1EstCivilId"]);
                        personadatosadic.NacPais = item["ad1NacPais"].ToString();
                        personadatosadic.DirPaisId = Convert.ToInt32(item["ad1DirPaisId"]);
                        personadatosadic.DirDptCod = item["ad1DirDptCod"].ToString();
                        personadatosadic.DirPrvCod = item["ad1DirPrvCod"].ToString();
                        personadatosadic.DirDisCod = item["ad1DirDisCod"].ToString();
                        personadatosadic.DirEnt = item["ad1DirEnt"].ToString();
                        personadatosadic.TlfnoDom = item["ad1TlfnoDom"].ToString();
                        personadatosadic.TlfnoTrb = item["ad1TlfnoTrb"].ToString();
                        personadatosadic.NroCel = item["ad1NroCel"].ToString();
                        personadatosadic.DirMail = item["ad1DirMail"].ToString();
                        personadatosadic.Sexo = Convert.ToInt32(item["ad1Sexo"]);
                        personadatosadic.FechaCreacion= DateTime.Now.Date;
                        personadatosadic.UsuarioCreacion= Session["Usuario"].ToString();

                        personapreg.PreguntaId = 2; //Fuma?
                        personapreg.Rspta = rblDscPrgFuma.SelectedValue.ToString();

                        AfiliacionSeguro afiseguro1 = new AfiliacionSeguro();
                        afiseguro1.InstitucionEducativaID = Convert.ToInt32(hdnInstitucionEducativa.Value);
                        afiseguro1.CodigoPago = "";
                        afiseguro1.FechaCreacion = DateTime.Now.Date;
                        afiseguro1.UsuarioCreacion = Session["Usuario"].ToString();
                        afiseguro1.Estado = true;

                        Util.AfiliaSeguroOncologico AfiliaOncologico = new Util.AfiliaSeguroOncologico();
                        AfiliaOncologico.AfiliacionSeguro = afiseguro1;
                        AfiliaOncologico.Producto = HttpUtility.HtmlDecode(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[4].Text);
                        AfiliaOncologico.Prima = GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[6].Text;
                        AfiliaOncologico.Aseguradora = HttpUtility.HtmlDecode(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[7].Text);
                        AfiliaOncologico.MonedaId = Convert.ToInt32(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[13].Text);
                        AfiliaOncologico.NombreColegio = HttpUtility.HtmlDecode(Convert.ToString(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[14].Text));
                        AfiliaOncologico.IdProducto = Convert.ToInt32(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[15].Text);
                        AfiliaOncologico.IdAsociacion = Convert.ToInt32(GrvConfirmar.Rows[GrvConfirmar.Rows.Count - 1].Cells[16].Text);
                        AfiliaOncologico.Alumno = alu;
                        AfiliaOncologico.Padre = padre;
                        //AfiliaOncologico.AsegMayordeEdad = ASegMayorEdad;
                        AfiliaOncologico.PersonaDatosAdic = personadatosadic;
                        AfiliaOncologico.PersonaPreg = personapreg;
                        ListaAFiliaOncologico.Add(AfiliaOncologico);
                    }
                    #endregion
                }



                #region EnviarCorreoaCliente
                using (Alumno_DAO db = new Alumno_DAO())
                {
                    if (hdnProductoId.Value == "1")
                    {
                        #region procesa_accidentes
                        foreach (Util.AfiliaSeguroAccidentes AfiliaSeguroAccidente in ListaAFiliaAccidentes)
                        {
                            CodigoDAO dbCodigo = new CodigoDAO();
                            DataSet dsCodigos = dbCodigo.OBTENER_CODIGO_LIBRE(Convert.ToInt32(AfiliaSeguroAccidente.IdAsociacion/*  1.Value*/));

                            if (dsCodigos.Tables[0].Rows.Count > 0)
                            {
                                AfiliaSeguroAccidente.CodigoLibreID = Convert.ToInt32(dsCodigos.Tables[0].Rows[0][6]);
                                AfiliaSeguroAccidente.CodigoLibre = dsCodigos.Tables[0].Rows[0][0].ToString();
                                db.AgregarAfiliadoAccidentes(AfiliaSeguroAccidente.Alumno, AfiliaSeguroAccidente.AfiliacionSeguro, AfiliaSeguroAccidente.CodigoLibreID, Session["Usuario"].ToString(), Convert.ToInt32(hdnAsociacionId.Value == "" ? "0" : hdnAsociacionId.Value));

                                EnviaEmailSeguro envio = new EnviaEmailSeguro();
                                envio.Para = Session["Usuario"].ToString();
                                envio.Codigo = AfiliaSeguroAccidente.CodigoLibre;
                                envio.Usuario = Session["Usuario"].ToString();

                                envio.asegurado = HttpUtility.HtmlDecode(AfiliaSeguroAccidente.Alumno.ApellidoPaternno + " " + AfiliaSeguroAccidente.Alumno.ApellidoMaterno
                                                            + ", " + AfiliaSeguroAccidente.Alumno.Nombre).ToUpper();
                                envio.Producto = AfiliaSeguroAccidente.Producto;
                                envio.Prima = AfiliaSeguroAccidente.Prima;
                                envio.aseguradora = HttpUtility.HtmlDecode(AfiliaSeguroAccidente.Aseguradora);
                                envio.beneficiario = "";
                                envio.idAsegurado = 1;
                                envio.codMoneda = AfiliaSeguroAccidente.MonedaId;

                                using (Usuario_DAO dbUsuario = new Usuario_DAO())
                                {
                                    Users dsUsuario = dbUsuario.BuscarUsuarioPorEmail(Session["Usuario"].ToString());
                                    Users DatosUsuario = dbUsuario.BuscarUsuarioPorEmail(Session["Usuario"].ToString());
                                    envio.NombreCompleto = DatosUsuario.ApellidoPaterno.ToUpper() + " " + DatosUsuario.ApellidoMaterno.ToUpper() + " " + DatosUsuario.Nombre.ToUpper();
                                    NombreUsuario = envio.NombreCompleto;
                                    ListaAfiliados.Add(envio);
                                }
                            }
                            else
                            {
                                string jssMensaje = "openModal();";
                                txtmensaje.Text = "¡ No existen Códigos para este Seguro!";
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssMensaje, true);
                                return;
                            }
                        }
                        #endregion
                    }
                    else if (hdnProductoId.Value == "2")
                    {
                        #region procesa_renta
                        if (ViewState["TipoAsociacion"].ToString() == "1")
                        {
                            foreach (Util.AfiliaSeguroRenta AfiliaSeguroRenta in ListaAFiliaRentas)
                            {
                                CodigoDAO dbCodigo = new CodigoDAO();
                                DataSet dsCodigos = dbCodigo.OBTENER_CODIGO_LIBRE_RENTA(Convert.ToInt32(AfiliaSeguroRenta.IdAsociacion));

                                if (dsCodigos.Tables[0].Rows.Count > 0)
                                {
                                    AfiliaSeguroRenta.CodigoLibreID = Convert.ToInt32(dsCodigos.Tables[0].Rows[0][6]);
                                    AfiliaSeguroRenta.CodigoLibre = dsCodigos.Tables[0].Rows[0][0].ToString();
                                    db.AgregarAfiliadoRenta(AfiliaSeguroRenta.Alumno, AfiliaSeguroRenta.Padre, AfiliaSeguroRenta.AfiliacionSeguro,
                                                                               AfiliaSeguroRenta.CodigoLibreID, Session["Usuario"].ToString());

                                    EnviaEmailSeguro envio = new EnviaEmailSeguro();
                                    envio.Para = Session["Usuario"].ToString();
                                    envio.Usuario = Session["Usuario"].ToString();
                                    envio.asegurado = HttpUtility.HtmlDecode(AfiliaSeguroRenta.Padre.ApellidoPaterno.ToUpper() + " " + AfiliaSeguroRenta.Padre.ApellidoMaterno.ToUpper() + " " +
                                                                          AfiliaSeguroRenta.Padre.Nombre.ToUpper());
                                    envio.Producto = HttpUtility.HtmlDecode(AfiliaSeguroRenta.Producto);
                                    envio.Prima = AfiliaSeguroRenta.Prima;
                                    envio.aseguradora = HttpUtility.HtmlDecode(AfiliaSeguroRenta.Aseguradora);

                                    envio.beneficiario = HttpUtility.HtmlDecode(AfiliaSeguroRenta.Alumno.ApellidoPaternno.ToUpper() + " " + AfiliaSeguroRenta.Alumno.ApellidoMaterno.ToUpper() + " , " +
                                                                       AfiliaSeguroRenta.Alumno.Nombre.ToUpper());
                                    envio.Codigo = AfiliaSeguroRenta.CodigoLibre;
                                    envio.idAsegurado = 2;
                                    envio.codMoneda = AfiliaSeguroRenta.MonedaId;

                                    using (Usuario_DAO dbUsuario = new Usuario_DAO())
                                    {
                                        Users dsUsuario = dbUsuario.BuscarUsuarioPorEmail(Session["Usuario"].ToString());
                                        Users DatosUsuario = dbUsuario.BuscarUsuarioPorEmail(Session["Usuario"].ToString());
                                        envio.NombreCompleto = DatosUsuario.ApellidoPaterno.ToUpper() + " " + DatosUsuario.ApellidoMaterno.ToUpper() + " , " + DatosUsuario.Nombre.ToUpper();
                                        envio.Usuario = envio.NombreCompleto;
                                        NombreUsuario = envio.NombreCompleto;
                                        ListaAfiliados.Add(envio);
                                    }
                                }
                                else
                                {
                                    string jssMensaje = "openModal();";
                                    txtmensaje.Text = "¡ No existen códigos de pago disponibles!";
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssMensaje, true);
                                    return;
                                }
                            }
                        }
                        else if (ViewState["TipoAsociacion"].ToString() == "2")
                        {
                            ListaAFiliaRentas = (List<Util.AfiliaSeguroRenta>)Session["ListaAfiliaRentas"];

                            foreach (Util.AfiliaSeguroRenta AfiliaSeguroRenta2 in ListaAFiliaRentas)
                            {
                                CodigoDAO dbCodigo = new CodigoDAO();
                                DataSet dsCodigos = dbCodigo.OBTENER_CODIGO_LIBRE_RENTA(Convert.ToInt32(AfiliaSeguroRenta2.IdAsociacion));
                                if (dsCodigos.Tables[0].Rows.Count > 0)
                                {
                                    AfiliaSeguroRenta2.CodigoLibreID = Convert.ToInt32(dsCodigos.Tables[0].Rows[0][6]);
                                    AfiliaSeguroRenta2.CodigoLibre = dsCodigos.Tables[0].Rows[0][0].ToString();

                                    db.AgregarAfiliadoRentaPadre2(AfiliaSeguroRenta2.Alumno, AfiliaSeguroRenta2.Padre, AfiliaSeguroRenta2.AfiliacionSeguro,
                                                                                   AfiliaSeguroRenta2.CodigoLibreID, Session["Usuario"].ToString());

                                    EnviaEmailSeguro envio = new EnviaEmailSeguro();
                                    envio.Para = Session["Usuario"].ToString();
                                    envio.Usuario = Session["Usuario"].ToString();

                                    envio.asegurado = HttpUtility.HtmlDecode(AfiliaSeguroRenta2.Padre.ApellidoPaterno.ToUpper() + " " + AfiliaSeguroRenta2.Padre.ApellidoMaterno.ToUpper() + ", " +
                                                                          AfiliaSeguroRenta2.Padre.Nombre.ToUpper());

                                    envio.Producto = HttpUtility.HtmlDecode(AfiliaSeguroRenta2.Producto);
                                    envio.Prima = AfiliaSeguroRenta2.Prima;
                                    envio.aseguradora = HttpUtility.HtmlDecode(AfiliaSeguroRenta2.Aseguradora).ToUpper();
                                    //envio.NombreCompleto = "";
                                    envio.beneficiario = HttpUtility.HtmlDecode(AfiliaSeguroRenta2.Alumno.ApellidoPaternno.ToUpper() + " " + AfiliaSeguroRenta2.Alumno.ApellidoMaterno.ToUpper() + ", " +
                                                                            AfiliaSeguroRenta2.Alumno.Nombre.ToUpper());
                                    envio.Codigo = AfiliaSeguroRenta2.CodigoLibre;
                                    envio.idAsegurado = 2;
                                    envio.codMoneda = AfiliaSeguroRenta2.MonedaId;

                                    using (Usuario_DAO dbUsuario = new Usuario_DAO())
                                    {
                                        Users dsUsuario = dbUsuario.BuscarUsuarioPorEmail(Session["Usuario"].ToString());
                                        Users DatosUsuario = dbUsuario.BuscarUsuarioPorEmail(Session["Usuario"].ToString());
                                        envio.NombreCompleto = DatosUsuario.ApellidoPaterno.ToUpper() + " " + DatosUsuario.ApellidoMaterno.ToUpper() + ", " + DatosUsuario.Nombre.ToUpper();
                                        envio.Usuario = envio.NombreCompleto.ToUpper();
                                        NombreUsuario = envio.NombreCompleto.ToUpper();
                                        ListaAfiliados.Add(envio);
                                    }
                                }
                                else
                                {
                                    string jssMensaje = "openModal();";
                                    txtmensaje.Text = "¡ No existen códigos de pago disponibles!";
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssMensaje, true);
                                    return;
                                }
                            }
                        }
                        #endregion
                    }
                    else if (hdnProductoId.Value == "3" || hdnProductoId.Value == "7")
                    {
                        foreach (Util.AfiliaSeguroOncologico AfiliaSeguroOnco in ListaAFiliaOncologico)
                        {
                            CodigoDAO dbCodigo = new CodigoDAO();
                            DataSet dsCodigos = dbCodigo.OBTENER_CODIGO_LIBRE_ONCO(Convert.ToInt32(AfiliaSeguroOnco.IdAsociacion));

                            if (dsCodigos.Tables[0].Rows.Count > 0)
                            {
                                AfiliaSeguroOnco.CodigoLibreID = Convert.ToInt32(dsCodigos.Tables[0].Rows[0][6]);
                                AfiliaSeguroOnco.CodigoLibre = dsCodigos.Tables[0].Rows[0][0].ToString();
                                db.AgregarAfiliadoOnco(AfiliaSeguroOnco.Alumno, AfiliaSeguroOnco.Padre, AfiliaSeguroOnco.PersonaDatosAdic, AfiliaSeguroOnco.PersonaPreg, AfiliaSeguroOnco.AfiliacionSeguro,
                                                                           AfiliaSeguroOnco.CodigoLibreID, Session["Usuario"].ToString());

                                EnviaEmailSeguro envio = new EnviaEmailSeguro();
                                envio.Para = Session["Usuario"].ToString();
                                envio.Usuario = Session["Usuario"].ToString();
                                envio.asegurado = HttpUtility.HtmlDecode(AfiliaSeguroOnco.Alumno.ApellidoPaternno.ToUpper() + " " + AfiliaSeguroOnco.Alumno.ApellidoMaterno.ToUpper() + " " +
                                                                      AfiliaSeguroOnco.Alumno.Nombre.ToUpper());
                                envio.Producto = HttpUtility.HtmlDecode(AfiliaSeguroOnco.Producto);
                                envio.Prima = AfiliaSeguroOnco.Prima;
                                envio.aseguradora = HttpUtility.HtmlDecode(AfiliaSeguroOnco.Aseguradora);

                                envio.beneficiario = HttpUtility.HtmlDecode(AfiliaSeguroOnco.Alumno.ApellidoPaternno.ToUpper() + " " + AfiliaSeguroOnco.Alumno.ApellidoMaterno.ToUpper() + " , " +
                                                                   AfiliaSeguroOnco.Alumno.Nombre.ToUpper());
                                envio.Codigo = AfiliaSeguroOnco.CodigoLibre;
                                //envio.idAsegurado = 2;
                                envio.codMoneda = AfiliaSeguroOnco.MonedaId;

                                using (Usuario_DAO dbUsuario = new Usuario_DAO())
                                {
                                    Users dsUsuario = dbUsuario.BuscarUsuarioPorEmail(Session["Usuario"].ToString());
                                    Users DatosUsuario = dbUsuario.BuscarUsuarioPorEmail(Session["Usuario"].ToString());
                                    envio.NombreCompleto = DatosUsuario.ApellidoPaterno.ToUpper() + " " + DatosUsuario.ApellidoMaterno.ToUpper() + " , " + DatosUsuario.Nombre.ToUpper();
                                    envio.Usuario = envio.NombreCompleto;
                                    NombreUsuario = envio.NombreCompleto;
                                    ListaAfiliados.Add(envio);
                                }
                            }
                            else
                            {
                                string jssMensaje = "openModal();";
                                txtmensaje.Text = "¡ No existen códigos de pago disponibles!";
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssMensaje, true);
                                return;
                            }
                        }
                    }
                #endregion

                EnviarMensajeEmail(ListaAfiliados, NombreUsuario + "/ GENERACIÓN DE CÓDIGOS DE PAGOS HERMES ONLINE");
                LimpiarDatos();
                OnlineDAO dbOnline = new OnlineDAO();
                dbOnline.LimpiarRegistroOnline(Session["Usuario"].ToString());
                LimpiarInfoPadres();
                inicializar();
                //BtnFinalizar.Enabled = false;
                //Guardar afiliacionseguro alumno           
                string jss = "openModalFinalizar();";
                lblFinalizar.Text = "¡ Los códigos de pago se han generado correctamente, por favor verifique su correo electrónico. !";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
                MVWAfiliacion.ActiveViewIndex = 0;
                chkAcepto.Checked = false;
                }
            }
              
            catch (Exception ex)
            {
                string jss = "openModal();";
                lblFinalizar.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
            }
        }

        public void EnviarMensajeEmail(List<EnviaEmailSeguro> mensajes, string Asunto)
        {
            ReportParameter TipoSeguroAccidentes = null;
            ReportParameter AseguradoAccidentes = null;
            ReportParameter TipoSeguroRenta = null;
            ReportParameter AseguradoRenta = null;
            ReportParameter Asegurado = null;
            try
            {

                //List<Util.AfiliaSeguroRenta> Lista_AFiliaRentasx = (List<Util.AfiliaSeguroRenta>)Session["ListaAfiliaRentas"];
                //List<Util.AfiliaSeguroRenta2> Lista_AFiliaRentas2 = (List<Util.AfiliaSeguroRenta2>)Session["ListaAfiliaRentas"];
                List<Util.AfiliaSeguroAccidentes> ListaAFiliaAccidentesx = (List<Util.AfiliaSeguroAccidentes>)Session["ListaAfiliaAccidentes"];
                List<Util.AfiliaSeguroRenta> ListaAFiliaRentas = (List<Util.AfiliaSeguroRenta>)Session["ListaAfiliaRentas"];
                List<Util.AfiliaSeguroAccidentes> ListaAFiliaAccidentes = (List<Util.AfiliaSeguroAccidentes>)Session["ListaAfiliaAccidentes"];
                List<Util.AfiliaSeguroOncologico> ListaAFiliaOnco = (List<Util.AfiliaSeguroOncologico>)Session["ListaAfiliaOncologico"];

                string usuario = cUtil.ObtenerValorParametro("CORREO", "USER");
                string clave = cUtil.ObtenerValorParametro("CORREO", "CLAVE");
                string smtp = cUtil.ObtenerValorParametro("CORREO", "SMTP");
                int puerto = Convert.ToInt32(cUtil.ObtenerValorParametro("CORREO", "PUERTO"));
                string mensaje = "";
                if (hdnProductoId.Value == "1")
                {
                    #region enviomail_accidentes
                    foreach (Util.AfiliaSeguroAccidentes rows in ListaAFiliaAccidentes)
                    {
                        //string url = Server.MapPath(@"\Templates\GenerarcionCodsOnLine.html");          //Ruta Local
                        string url = Server.MapPath(@"~//Templates//GenerarcionCodsOnLine.html");   //->Ruta del SERVIDOR 
                        mensaje = cUtil.LeerTemplateHTML(url);

                        string tabla = @"<table style='width:100 %'><tr style ='background-color:#5D9ACF;color:white'><td>Codigo</ td ><td>"
                                               + "Asegurado </td><td> Beneficiario </td><td>Prima</td><td>Tipo de Seguro</td><td>Cia Seguro </td></tr>";

                        foreach (EnviaEmailSeguro item in mensajes)
                        {
                            tabla += "<tr><td>[CodigoPago]</td><td>[Asegurado]</td><td>[Beneficiario]</td><td>[Prima]</td><td>[Seguro]</td>"
                                          + "<td>[CiaSeguro] </td></tr>";

                            mensaje = mensaje.Replace("[usuario]", item.Usuario);
                            mensaje = mensaje.Replace("[Nombre]", item.NombreCompleto);

                            tabla = tabla.Replace("[CodigoPago]", item.Codigo);

                            if (item.codMoneda == 1)
                            {
                                tabla = tabla.Replace("[Prima]", "S/" + item.Prima);
                            }
                            else if (item.codMoneda == 2)
                            {
                                tabla = tabla.Replace("[Prima]", "$" + item.Prima);
                            }

                            tabla = tabla.Replace("[CiaSeguro]", item.aseguradora);
                            tabla = tabla.Replace("[Seguro]", item.Producto);
                            tabla = tabla.Replace("[Asegurado]", item.asegurado);
                            tabla = tabla.Replace("[Beneficiario]", item.beneficiario);
                            //tabla = tabla.Replace("[Nombre]", item.NombreCompleto);
                        }

                        tabla += "</table>";
                        mensaje = mensaje.Replace("[Datos]", tabla);
                        mensaje = HttpUtility.HtmlDecode(mensaje);

                        //Generación de Reportes y envíos vía Email**************************************************************************************

                        ReportDataSource rptDs = new ReportDataSource("ReciboDePagos", ListaAFiliaAccidentes);

                        RPVBoletaDeAfiliados.LocalReport.DataSources.Add(rptDs);
                        RPVBoletaDeAfiliados.LocalReport.ReportPath = Server.MapPath("BoletaDePagosAfiliados.rdlc");
                        List<ReportParameter> parametros = new List<ReportParameter>();
                        ReportParameter Nombre = new ReportParameter("Nombre", ViewState["NombreColegio"].ToString());
                        ReportParameter Codigo = new ReportParameter("Codigo", rows.CodigoLibre.ToString());
                        //ReportParameter parametro3 = new ReportParameter("Beneficiario", ListaAFiliaAccidentes[0].Alumno.ToString());

                        string Espacio = "&nbsp;";
                        Espacio = Server.HtmlDecode(Espacio);
                        ReportParameter Prima;
                        if (rows.MonedaId == 1)//Soles
                        {
                            Prima = new ReportParameter("Prima", "S/." + Espacio + rows.Prima.ToString());
                        }
                        else//Dolares
                        {
                            Prima = new ReportParameter("Prima", "US$" + Espacio + rows.Prima.ToString());
                        }

                        if (rows.IdProducto == 1)//PRODUCTO
                        {
                            TipoSeguroAccidentes = new ReportParameter("TipoDeSeguro", "SEGURO DE ACCIDENTES ESTUDIANTILES");
                        }

                        if (rows.IdProducto == 1)//hdnProductoId.Value == "1"
                        {
                            AseguradoAccidentes = new ReportParameter("Asegurado", (rows.Alumno.ApellidoPaternno.ToString() + Espacio + rows.Alumno.ApellidoMaterno.ToString() + Espacio + rows.Alumno.Nombre.ToString()).ToUpper());
                        }

                        ReportParameter FechaVigenciaPolizaInicio = new ReportParameter("FechaVigenciaPolizaInicio", Convert.ToDateTime(ViewState["FechaVigenciaPolizaInicio"]).ToString("dd/MM/yyyy"));
                        ReportParameter FechaVigenciaPolizaFin = new ReportParameter("FechaVigenciaPolizaFin", Convert.ToDateTime(ViewState["FechaVigenciaPolizaFinal"]).ToString("dd/MM/yyyy"));
                        ReportParameter FechaDePago = new ReportParameter("FechaDePago", Convert.ToDateTime(ViewState["FechaDepago"]).ToString("dd/MM/yyyy"));
                        parametros.Add(Nombre);
                        parametros.Add(Codigo);
                        parametros.Add(Prima);
                        parametros.Add(TipoSeguroAccidentes);
                        parametros.Add(AseguradoAccidentes);
                        parametros.Add(FechaVigenciaPolizaInicio);
                        parametros.Add(FechaVigenciaPolizaFin);
                        parametros.Add(FechaDePago);

                        RPVBoletaDeAfiliados.LocalReport.SetParameters(parametros);
                        RPVBoletaDeAfiliados.LocalReport.Refresh();
                        Warning[] warnings;
                        string[] streamids;
                        string mimeType;
                        string encoding;
                        string extension;

                        byte[] bytes = RPVBoletaDeAfiliados.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                        string path = Server.MapPath("~/rptTemp/" + "ReciboAccidentesReport" + rows.CodigoLibre + ".pdf");
                        FileStream fs = new FileStream(path, FileMode.Create);
                        fs.Write(bytes, 0, bytes.Length);
                        fs.Close();
                    }
                    string adjunto = "";
                    List<string> adjuntosAccidentes = new List<string>();
                    foreach (var item in ListaAFiliaAccidentes)
                    {
                        adjunto = Path.Combine(Server.MapPath("~/rptTemp/" + "ReciboAccidentesReport" + item.CodigoLibre + ".pdf"));//Ruta del reporte generado
                        adjuntosAccidentes.Add(adjunto);
                    }
                    cUtil.EnvioMails(Session["Usuario"].ToString(), usuario, Asunto, mensaje, true, clave, smtp, puerto, adjuntosAccidentes);
                    #endregion
                }
                else if (hdnProductoId.Value == "2")
                {
                #region enviomail_rentas
                    if (ViewState["TipoAsociacion"].ToString() == "1")
                    {
                    #region enviomail_rentas_asocia1
                        foreach (Util.AfiliaSeguroRenta rows in ListaAFiliaRentas)
                        {
                            //string url = Server.MapPath(@"\Templates\GenerarcionCodsOnLine.html");
                            string url = Server.MapPath(@"~//Templates//GenerarcionCodsOnLine.html");
                            mensaje = cUtil.LeerTemplateHTML(url);
                            string tabla = @"<table style='width:100 %'><tr style ='background-color:#5D9ACF;color:white'><td>Codigo</ td ><td>"
                                   + "Asegurado </td><td> Beneficiario </td><td>Prima</td><td>Tipo de Seguro</td><td>Cia Seguro </td></tr>";

                            foreach (EnviaEmailSeguro item in mensajes)
                            {
                                if (Convert.ToDecimal(item.Prima) > 0)
                                {
                                    tabla += "<tr><td>[CodigoPago]</td><td>[Asegurado]</td><td>[Beneficiario]</td><td>[Prima]</td><td>[Seguro]</td>"
                                    + "<td>[CiaSeguro] </td></tr>";

                                    //tabla = tabla.Replace("[usuario]", item.Usuario);
                                    mensaje = mensaje.Replace("[usuario]", item.Usuario);
                                    mensaje = mensaje.Replace("[Nombre]", item.NombreCompleto);

                                    tabla = tabla.Replace("[CodigoPago]", item.Codigo);

                                    if (item.codMoneda == 1)
                                    {
                                        tabla = tabla.Replace("[Prima]", "S/" + item.Prima);
                                    }
                                    else if (item.codMoneda == 2)
                                    {
                                        tabla = tabla.Replace("[Prima]", "$" + item.Prima);
                                    }

                                    tabla = tabla.Replace("[CiaSeguro]", item.aseguradora);
                                    tabla = tabla.Replace("[Seguro]", item.Producto);
                                    tabla = tabla.Replace("[Asegurado]", item.asegurado);
                                    tabla = tabla.Replace("[Beneficiario]", item.beneficiario);
                                    //tabla = tabla.Replace("[Nombre]", item.NombreCompleto);
                                }
                            }//end foreach

                            tabla += "</table>";
                            mensaje = mensaje.Replace("[Datos]", tabla);
                            mensaje = HttpUtility.HtmlDecode(mensaje);
                            //Generación de Reportes y envíos vía Email*****************************************************************************             
                            //**********************************************************************************************************************
                            ReportDataSource rptDs = new ReportDataSource("ReciboDePagos", ListaAFiliaRentas);

                            RPVBoletaRentaAfiliados.LocalReport.DataSources.Add(rptDs);
                            RPVBoletaRentaAfiliados.LocalReport.ReportPath = Server.MapPath("BoletaDePagosRentaAfiliados.rdlc");

                            List<ReportParameter> parametros = new List<ReportParameter>();
                            ReportParameter Nombre = new ReportParameter("Nombre", ViewState["NombreColegio"].ToString());
                            ReportParameter Codigo = new ReportParameter("Codigo", rows.CodigoLibre.ToString());
                            ReportParameter Beneficiario = new ReportParameter("Beneficiario", rows.Padre.ToString());
                            ReportParameter Prima;
                            string Espacio = "&nbsp;";
                            Espacio = Server.HtmlDecode(Espacio);
                            if (rows.MonedaId == 1)//Soles
                            {
                                Prima = new ReportParameter("Prima", "S/." + Espacio + rows.Prima.ToString());
                            }
                            else//Dolares
                            {
                                Prima = new ReportParameter("Prima", "US$" + Espacio + rows.Prima.ToString());
                            }

                            ReportParameter TipoDeSeguro;
                            ReportParameter TipoAsegurado = new ReportParameter();
                            if (rows.IdProducto == 2)// hdnProductoId.Value == "2"
                            {
                                TipoDeSeguro = new ReportParameter("TipoDeSeguro", "SEGURO DE RENTA ESTUDIANTIL");
                                TipoAsegurado = new ReportParameter("TipoAsegurado", String.Format("ASEGURADO ({0})        :", rows.Padre.TipoPadre.Nombre.ToUpper()));
                            }
                            else //if(Lista[0].TipoSeguro == "1")
                            {
                                TipoDeSeguro = new ReportParameter("TipoDeSeguro", "SEGURO DE ACCIDENTES ESTUDIANTILES");
                            }

                            //ReportParameter Asegurado;

                            Beneficiario = new ReportParameter("Beneficiario", (rows.Alumno.ApellidoPaternno + Espacio +
                                                                                                     rows.Alumno.ApellidoMaterno + " " + rows.Alumno.Nombre.ToString()).ToUpper());
                            Asegurado = new ReportParameter("Asegurado", (rows.Padre.ApellidoPaterno + Espacio + rows.Padre.ApellidoMaterno + Espacio + rows.Padre.Nombre).ToUpper());

                            ReportParameter FechaVigenciaPolizaInicio = new ReportParameter("FechaVigenciaPolizaInicio",
                                                                                                                                      Convert.ToDateTime(ViewState["FechaVigenciaPolizaInicio"]).ToString("dd/MM/yyyy"));
                            ReportParameter FechaVigenciaPolizaFin = new ReportParameter("FechaVigenciaPolizaFin",
                                                                                                                                      Convert.ToDateTime(ViewState["FechaVigenciaPolizaFinal"]).ToString("dd/MM/yyyy"));
                            ReportParameter FechaDePago = new ReportParameter("FechaDePago", Convert.ToDateTime(ViewState["FechaDepago"]).ToString("dd/MM/yyyy"));

                            parametros.Add(Nombre);
                            parametros.Add(Codigo);
                            parametros.Add(Beneficiario);
                            parametros.Add(Prima);
                            parametros.Add(TipoDeSeguro);
                            parametros.Add(Asegurado);
                            parametros.Add(FechaVigenciaPolizaInicio);
                            parametros.Add(FechaVigenciaPolizaFin);
                            parametros.Add(FechaDePago);
                            parametros.Add(TipoAsegurado);

                            RPVBoletaRentaAfiliados.LocalReport.SetParameters(parametros);
                            RPVBoletaRentaAfiliados.LocalReport.Refresh();

                            Warning[] warnings;
                            string[] streamids;
                            string mimeType;
                            string encoding;
                            string extension;

                            byte[] bytes = RPVBoletaRentaAfiliados.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                            string path = Server.MapPath("~/rptTemp/" + "ReciboRentasReport" + rows.CodigoLibre + ".pdf");
                            FileStream fs = new FileStream(path, FileMode.Create);
                            fs.Write(bytes, 0, bytes.Length);
                            fs.Close();
                        }
                        List<string> adjuntosRentas = new List<string>();
                        string adjunto = "";
                        foreach (var item in ListaAFiliaRentas)
                        {
                            adjunto = Path.Combine(Server.MapPath("~/rptTemp/" + "ReciboRentasReport" + item.CodigoLibre + ".pdf"));//Ruta del reporte generado
                            adjuntosRentas.Add(adjunto);
                        }
                        cUtil.EnvioMails(Session["Usuario"].ToString(), usuario, Asunto, mensaje, true, clave, smtp, puerto, adjuntosRentas);
                    #endregion
                    }
                    //Generación de Reportes y envíos vía Email**************************************************************************************
                    else if (ViewState["TipoAsociacion"].ToString() == "2")
                    {
                    #region enviomail_rentas_asocia2
                        //Enviando Reportes vía email*******************************************************************************                                            
                        //List<Util.AfiliaSeguroRenta2> ListaAFiliaRentas = (List<Util.AfiliaSeguroRenta2>)Session["ListaAfiliaRentasx"];
                        foreach (Util.AfiliaSeguroRenta rows in ListaAFiliaRentas)
                        {
                            if (rows.IdProducto == 2 && rows.Padre.ApellidoPaterno.ToUpper() != "RESERVADO")
                            {
                                //string url = Server.MapPath(@"\Templates\GenerarcionCodsOnLine.html");
                                string url = Server.MapPath(@"~//Templates//GenerarcionCodsOnLine.html");
                                mensaje = cUtil.LeerTemplateHTML(url);

                                string tabla = @"<table style='width:100 %'><tr style ='background-color:#5D9ACF;color:white'><td>Codigo</ td ><td>"
                                               + "Asegurado </td><td> Beneficiario </td><td>Prima</td><td>Tipo de Seguro</td><td>Cia Seguro </td></tr>";

                                foreach (EnviaEmailSeguro item in mensajes)
                                {
                                    if (Convert.ToDecimal(item.Prima) > 0)
                                    {
                                        if (item.asegurado.Trim() != "RESERVADO RESERVADO, RESERVADO")
                                        {
                                            tabla += "<tr><td>[CodigoPago]</td><td>[Asegurado]</td><td>[Beneficiario]</td><td>[Prima]</td><td>[Seguro]</td>"
                                                      + "<td>[CiaSeguro] </td></tr>";

                                            //tabla = tabla.Replace("[usuario]", item.Usuario);
                                            mensaje = mensaje.Replace("[usuario]", item.Usuario);
                                            mensaje = mensaje.Replace("[Nombre]", item.NombreCompleto);
                                            tabla = tabla.Replace("[CodigoPago]", item.Codigo);

                                            if (item.codMoneda == 1)
                                            {
                                                tabla = tabla.Replace("[Prima]", "S/" + item.Prima);
                                            }
                                            else if (item.codMoneda == 2)
                                            {
                                                tabla = tabla.Replace("[Prima]", "$" + item.Prima);
                                            }

                                            tabla = tabla.Replace("[CiaSeguro]", item.aseguradora);
                                            tabla = tabla.Replace("[Seguro]", item.Producto);
                                            tabla = tabla.Replace("[Asegurado]", item.asegurado);
                                            tabla = tabla.Replace("[Beneficiario]", item.beneficiario);
                                            //tabla = tabla.Replace("[Nombre]", item.NombreCompleto);
                                        }
                                    }
                                }
                                tabla += "</table>";
                                mensaje = mensaje.Replace("[Datos]", tabla);
                                mensaje = HttpUtility.HtmlDecode(mensaje);

                                //Generación de Reportes y envíos vía Email*****************************************************************************             
                                //**********************************************************************************************************************
                                ReportDataSource rptDs = new ReportDataSource("ReciboDePagos", ListaAFiliaRentas);

                                RPVBoletaRentaAfiliados.LocalReport.DataSources.Add(rptDs);
                                RPVBoletaRentaAfiliados.LocalReport.ReportPath = Server.MapPath("BoletaDePagosRentaAfiliados.rdlc");

                                List<ReportParameter> parametros = new List<ReportParameter>();
                                ReportParameter Nombre = new ReportParameter("Nombre", ViewState["NombreColegio"].ToString());
                                ReportParameter Codigo = new ReportParameter("Codigo", rows.CodigoLibre.ToString());
                                ReportParameter Beneficiario = new ReportParameter("Beneficiario", rows.Padre.ToString());

                                ReportParameter Prima;
                                string Espacio = "&nbsp;";
                                Espacio = Server.HtmlDecode(Espacio);
                                if (rows.MonedaId == 1)//Soles
                                {
                                    Prima = new ReportParameter("Prima", "S/." + Espacio + rows.Prima.ToString());
                                }
                                else//Dolares
                                {
                                    Prima = new ReportParameter("Prima", "US$" + Espacio + rows.Prima.ToString());
                                }

                                ReportParameter TipoDeSeguro;
                                ReportParameter TipoAsegurado = new ReportParameter();
                                if (rows.IdProducto == 2)// hdnProductoId.Value == "2"
                                {
                                    TipoDeSeguro = new ReportParameter("TipoDeSeguro", "SEGURO DE RENTA ESTUDIANTIL");
                                    TipoAsegurado = new ReportParameter("TipoAsegurado", String.Format("ASEGURADO ({0})        :", rows.Padre.TipoPadre.Nombre.ToUpper()));
                                }
                                else //if(Lista[0].TipoSeguro == "1")
                                {
                                    TipoDeSeguro = new ReportParameter("TipoDeSeguro", "SEGURO DE ACCIDENTES ESTUDIANTILES");
                                }

                                Beneficiario = new ReportParameter("Beneficiario", (rows.Alumno.ApellidoPaternno + Espacio + rows.Alumno.ApellidoMaterno + Espacio + rows.Alumno.Nombre.ToString()).ToUpper());
                                Asegurado = new ReportParameter("Asegurado", (rows.Padre.ApellidoPaterno + Espacio + rows.Padre.ApellidoMaterno + Espacio + rows.Padre.Nombre).ToUpper());

                                ReportParameter FechaVigenciaPolizaInicio = new ReportParameter("FechaVigenciaPolizaInicio", Convert.ToDateTime(ViewState["FechaVigenciaPolizaInicio"]).ToString("dd/MM/yyyy"));
                                ReportParameter FechaVigenciaPolizaFin = new ReportParameter("FechaVigenciaPolizaFin", Convert.ToDateTime(ViewState["FechaVigenciaPolizaFinal"]).ToString("dd/MM/yyyy"));
                                ReportParameter FechaDePago = new ReportParameter("FechaDePago", Convert.ToDateTime(ViewState["FechaDepago"]).ToString("dd/MM/yyyy"));

                                parametros.Add(Nombre);
                                parametros.Add(Codigo);
                                parametros.Add(Beneficiario);
                                parametros.Add(Prima);
                                parametros.Add(TipoDeSeguro);
                                parametros.Add(Asegurado);
                                parametros.Add(FechaVigenciaPolizaInicio);
                                parametros.Add(FechaVigenciaPolizaFin);
                                parametros.Add(FechaDePago);
                                parametros.Add(TipoAsegurado);

                                RPVBoletaRentaAfiliados.LocalReport.SetParameters(parametros);
                                RPVBoletaRentaAfiliados.LocalReport.Refresh();

                                Warning[] warnings;
                                string[] streamids;
                                string mimeType;
                                string encoding;
                                string extension;

                                byte[] bytes = RPVBoletaRentaAfiliados.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                                string path = Server.MapPath("~/rptTemp/" + "ReciboRentasReport" + rows.CodigoLibre + ".pdf");
                                FileStream fs = new FileStream(path, FileMode.Create);
                                fs.Write(bytes, 0, bytes.Length);
                                fs.Close();
                            }
                        }

                        List<string> adjuntosRentasx = new List<string>();
                        string adjuntox = "";
                        foreach (var item in ListaAFiliaRentas)
                        {
                            if (item.Padre.ApellidoPaterno.ToUpper().Trim() != "RESERVADO")
                            {
                                adjuntox = Path.Combine(Server.MapPath("~/rptTemp/" + "ReciboRentasReport" + item.CodigoLibre + ".pdf"));//Ruta del reporte generado
                                adjuntosRentasx.Add(adjuntox);
                            }
                        }
                        cUtil.EnvioMails(Session["Usuario"].ToString(), usuario, Asunto, mensaje, true, clave, smtp, puerto, adjuntosRentasx);
                    #endregion
                    }
                #endregion
                }
                else if (hdnProductoId.Value == "3" || hdnProductoId.Value == "7")
                {
                    #region enviomail_oncologico
                    foreach (Util.AfiliaSeguroOncologico rows in ListaAFiliaOnco)
                    {
                        string url = Server.MapPath(@"~//Templates//GenerarcionCodsOnLine.html");
                        mensaje = cUtil.LeerTemplateHTML(url);
                        string tabla = @"<table style='width:100 %'><tr style ='background-color:#5D9ACF;color:white'><td>Codigo</ td ><td>"
                               + "Asegurado </td><td> Beneficiario </td><td>Prima</td><td>Tipo de Seguro</td><td>Cia Seguro </td></tr>";

                        foreach (EnviaEmailSeguro item in mensajes)
                        {
                            if (Convert.ToDecimal(item.Prima) > 0)
                            {
                                tabla += "<tr><td>[CodigoPago]</td><td>[Asegurado]</td><td>[Beneficiario]</td><td>[Prima]</td><td>[Seguro]</td>"
                                + "<td>[CiaSeguro] </td></tr>";

                                mensaje = mensaje.Replace("[usuario]", item.Usuario);
                                mensaje = mensaje.Replace("[Nombre]", item.NombreCompleto);

                                tabla = tabla.Replace("[CodigoPago]", item.Codigo);

                                if (item.codMoneda == 1)
                                {
                                    tabla = tabla.Replace("[Prima]", "S/" + item.Prima);
                                }
                                else if (item.codMoneda == 2)
                                {
                                    tabla = tabla.Replace("[Prima]", "$" + item.Prima);
                                }

                                tabla = tabla.Replace("[CiaSeguro]", item.aseguradora);
                                tabla = tabla.Replace("[Seguro]", item.Producto);
                                tabla = tabla.Replace("[Asegurado]", item.asegurado);
                                tabla = tabla.Replace("[Beneficiario]", item.beneficiario);
                                //tabla = tabla.Replace("[Nombre]", item.NombreCompleto);
                            }
                        }//end foreach

                        tabla += "</table>";
                        mensaje = mensaje.Replace("[Datos]", tabla);
                        mensaje = HttpUtility.HtmlDecode(mensaje);
                        //Generación de Reportes y envíos vía Email*****************************************************************************             
                        //**********************************************************************************************************************
                        ReportDataSource rptDs = new ReportDataSource("ReciboDePagos", ListaAFiliaRentas);

                        RPVBoletaRentaAfiliados.LocalReport.DataSources.Add(rptDs);
                        RPVBoletaRentaAfiliados.LocalReport.ReportPath = Server.MapPath("Reportes/BoletaDePagosOncologico.rdlc");

                        List<ReportParameter> parametros = new List<ReportParameter>();
                        ReportParameter Nombre = new ReportParameter("Nombre", ViewState["NombreColegio"].ToString());
                        ReportParameter Codigo = new ReportParameter("Codigo", rows.CodigoLibre.ToString());
                        ReportParameter Vigencia = new ReportParameter("Vigencia", "VIGENCIA: A partir del primer día del mes siguiente de haber realizado el pago.");
                        ReportParameter Prima;
                        string Espacio = "&nbsp;";
                        Espacio = Server.HtmlDecode(Espacio);
                        if (rows.MonedaId == 1)//Soles
                        {
                            Prima = new ReportParameter("Prima", "S/." + Espacio + rows.Prima.ToString());
                        }
                        else//Dolares
                        {
                            Prima = new ReportParameter("Prima", "US$" + Espacio + rows.Prima.ToString());
                        }

                        ReportParameter TipoDeSeguro= new ReportParameter("TipoDeSeguro", "SEGURO ONCOLOGICO ESTUDIANTIL");

                        //ReportParameter Asegurado;

                        Asegurado = new ReportParameter("Asegurado", (rows.Alumno.ApellidoPaternno + Espacio +
                                                                                                 rows.Alumno.ApellidoMaterno + " " + rows.Alumno.Nombre.ToString()).ToUpper());
                        parametros.Add(Nombre);
                        parametros.Add(Codigo);
                        parametros.Add(Vigencia);
                        parametros.Add(Prima);
                        parametros.Add(TipoDeSeguro);
                        parametros.Add(Asegurado);

                        RPVBoletaRentaAfiliados.LocalReport.SetParameters(parametros);
                        RPVBoletaRentaAfiliados.LocalReport.Refresh();

                        Warning[] warnings;
                        string[] streamids;
                        string mimeType;
                        string encoding;
                        string extension;

                        byte[] bytes = RPVBoletaRentaAfiliados.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                        string path = Server.MapPath("~/rptTemp/" + "ReciboOncologicoReport" + rows.CodigoLibre + ".pdf");
                        FileStream fs = new FileStream(path, FileMode.Create);
                        fs.Write(bytes, 0, bytes.Length);
                        fs.Close();
                    }
                    List<string> adjuntosRentas = new List<string>();
                    string adjunto = "";
                    foreach (var item in ListaAFiliaOnco)
                    {
                        adjunto = Path.Combine(Server.MapPath("~/rptTemp/" + "ReciboOncologicoReport" + item.CodigoLibre + ".pdf"));//Ruta del reporte generado
                        adjuntosRentas.Add(adjunto);
                    }
                    cUtil.EnvioMails(Session["Usuario"].ToString(), usuario, Asunto, mensaje, true, clave, smtp, puerto, adjuntosRentas);
                    #endregion
                }

            }
            catch (Exception ex)
            {

            }
        }


        public string GetUniqueKey(int length)
        {
            string guidResult = string.Empty;

            while (guidResult.Length < length)
            {
                // Get the GUID.
                guidResult += Guid.NewGuid().ToString().GetHashCode().ToString("x");
            }

            // Make sure length is valid.
            if (length <= 0 || length > guidResult.Length)
                throw new ArgumentException("Length must be between 1 and " + guidResult.Length);

            // Return the first length bytes.
            return guidResult.Substring(0, length);
        }



        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grvSeguros_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void chkElegir_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow gvr = (GridViewRow)chk.NamingContainer;
            int iContador = 0;
            if (chk.Checked == true)
            {
                foreach (GridViewRow f in grvSeguros.Rows)
                {
                    if (iContador != gvr.RowIndex)
                    {
                        CheckBox chkItem = (CheckBox)grvSeguros.Rows[iContador].FindControl("chkElegir");
                        chkItem.Checked = false;
                    }
                    iContador += 1;
                }
            }
        }

        protected void GrvConfirmar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        protected void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        //protected void MaxL()
        //{
        //    if (DDLTipoDocumento.SelectedIndex == 1)
        //    {
        //        txtNumeroDocumento.MaxLength = 8;
        //    }
        //    if (DDLTipoDocumento.SelectedIndex == 2)
        //    {
        //        txtNumeroDocumento.Text = "PROBANDO2";
        //    }
        //    if (DDLTipoDocumento.SelectedIndex == 3)
        //    {
        //        txtNumeroDocumento.Text = "PROBANDO3";
        //    }
        //    //string message = DDLTipoDocumento.SelectedItem.Text + " - " + DDLTipoDocumento.SelectedItem.Value;
        //    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
        //}

        protected void grvSeguros_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //byte[] bytes = (byte[])(e.Row.DataItem as DataRowView)["FilePlanSeguro"];
                //string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                //e.Row.Cells[13].Text=base64String;
            }
        }

        protected void btnCerrarModal_Click(object sender, EventArgs e)
        {

        }

        protected void btnFinalizarEstadia_Click(object sender, EventArgs e)
        {
            //Response.Redirect("ConsultarAfiliacion.aspx");
        }

        protected void chkSegundoPadre_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSegundoPadre.Checked == true)
            {
                pnlPadre2.Enabled = true;
            }
            else
            {
                pnlPadre2.Enabled = false;
            }
        }


        //protected void RBT1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (RBT1.SelectedIndex==0)
        //    {
        //        pnlMadre.Enabled = true;
        //    }
        //    else if (RBT1.SelectedIndex == 1)
        //    {
        //        pnlMadre.Enabled = false;
        //    }
        //}

        //protected void DDLTipoDocumento_SelectedIndexChanged1(object sender, EventArgs e)
        //{
        //    if (DDLTipoDocumento.SelectedIndex == 2)
        //    {
        //        txtNumeroDocumento.Attributes.Add("type", "number");
        //    }
        //    else
        //    {
        //        txtNumeroDocumento.Attributes.Add("type", "date");
        //    }
        //}  

        void LimpiarDatos()
        {
            Session["AfiliacionProducto"] = null;
            Session["DatosAsegurado"] = null;
            Session["DatosPadreAsegurado"] = null;
            Session["DatosAfiliaProducto"] = null;
            Session["ListaAfiliaAccidentes"] = null;
            Session["ListaAfiliaRentas"] = null;
            Session["ListaAfiliaOncologico"] = null;
            Session["dtDetallePlan"] = null;
            Session["ListaAfiliaAccidentesx"] = null;
            Session["ListaAfiliaRentasx"] = null;
            Session["CA"] = 0;
            Session["CP"] = 0;
        }
        
        protected void lnkbtnTerminos_Click(object sender, EventArgs e)
        {
            string nombaseg = "";

            nombaseg = hdnidAseguradora.Value;
            if (nombaseg == "2")
            {
                ltlTerminos.Text =HttpUtility.HtmlDecode( cUtil.LeerTemplateHTML(Server.MapPath( "~/files/terminos/RIMAC_SEGUROS_ACCIDENTES.html")));
                string jss27 = "openModalTerminos2();";
                //lblNombreSeg.Text = "Bienvenido a la Familia RIMAC Seguro";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss27, true);
                return;
            }
        }
        
    }
}


