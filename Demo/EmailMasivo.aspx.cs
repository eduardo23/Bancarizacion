using DAO_Hermes;
using DAO_Hermes.Repositorios;
using DAO_Hermes.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Demo
{
    public partial class EmailMasivo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetFLstCampana();
            }
        }

        public void GetFLstCampana()
        {
            CampañasDAO objCampaña = new CampañasDAO();
            List<Campañas> oCampañas = objCampaña.ListarCampañas();
            ddlFCampana.DataSource = oCampañas;
            ddlFCampana.DataTextField = "Nombre";
            ddlFCampana.DataValueField = "ID";
            ddlFCampana.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Institucion_Educativa obje_InstitucionEducativa = new Institucion_Educativa();
            InstitucionEducativaDAO objn_InstitucionEducativa = new InstitucionEducativaDAO();

            DateTime ini = Convert.ToDateTime(txtFecPagoDesde.Text);
            DateTime fin = Convert.ToDateTime(txtFecPagoHasta.Text);
            string inidate = ini.ToString("yyyyMMdd");
            string findate = fin.ToString("yyyyMMdd");

            grvInst.DataSource = objn_InstitucionEducativa.getLstCntPagosByInstCamp(Convert.ToInt32(ddlFCampana.SelectedValue), inidate, findate);
            grvInst.DataBind();
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvInst.Rows) {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox check = (row.Cells[0].FindControl("chkSel") as CheckBox);
                    if (check != null)
                    {
                        if (check.Checked)
                        {
                            int InstitucionEducativaId = Convert.ToInt32(row.Cells[4].Text);
                            string InstitucionEducativaCod = row.Cells[1].Text;
                            string InstitucionEducativaNom = row.Cells[2].Text;
                            PreparaEnvioCorreo(Convert.ToInt32(ddlFCampana.SelectedValue), InstitucionEducativaId, InstitucionEducativaCod, InstitucionEducativaNom);
                        }
                    }
                }
            }
            txtmensaje.Text = "Los Notificaciones via E-mail han sido enviadas satisfactoriamente.";
            string jss = "openModal()";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
        }

        private void PreparaEnvioCorreo(int CampanaId, int InstitucionEducativaId, string InstitucionEducativaCod, string InstitucionEducativaNom) {
            string usuario = cUtil.ObtenerValorParametro("CORREO", "USER");
            string clave = cUtil.ObtenerValorParametro("CORREO", "CLAVE");
            string smtp = cUtil.ObtenerValorParametro("CORREO", "SMTP");
            int puerto = Convert.ToInt32(cUtil.ObtenerValorParametro("CORREO", "PUERTO"));            
            string mensaje = "";

            mensaje = mensaje + "<br/><span>Estimados Señores,</span><br/><br/>";
            mensaje = mensaje + "<span>A continuacion, se adjunta la relación de afiliados a los seguros estudiantiles en el rango de la fecha indicado en el asunto.</span><br/>";
            mensaje = mensaje + "<br/>";
            mensaje = mensaje + "<span>RESUMEN:<br/><br/>";
            
            Institucion_Educativa oInstitucionEducativa = new Institucion_Educativa();
            DateTime ini = Convert.ToDateTime(txtFecPagoDesde.Text);
            DateTime fin = Convert.ToDateTime(txtFecPagoHasta.Text);
            string inidate = ini.ToString("yyyyMMdd");
            string findate = fin.ToString("yyyyMMdd");

            string Asunto = Path.Combine(InstitucionEducativaNom + " Reporte de afiliación de asegurados del " + ini.ToString("dd/MM/yyyy") + " al " + fin.ToString("dd/MM/yyyy"));

            oInstitucionEducativa.cod_IEducativa = InstitucionEducativaId;            
            oInstitucionEducativa.Cod_CiaSeguro = 0;
            oInstitucionEducativa.EstadoIsPagado = 1;
            oInstitucionEducativa.Cod_Banco = 0;
            oInstitucionEducativa.Cod_Moneda = 0;
            oInstitucionEducativa.FechaInicial = Convert.ToDateTime(txtFecPagoDesde.Text);
            oInstitucionEducativa.FechaFinal = Convert.ToDateTime(txtFecPagoHasta.Text);
            oInstitucionEducativa.TextoBusqueda = "";

            TipoProductoDAO ObjTipoProducto = new TipoProductoDAO();
            List<TipoProducto> olstTipoProducto = ObjTipoProducto.getLstCntPagosByProdInst(CampanaId, InstitucionEducativaId, inidate, findate);

            List<MemoryStream> adjuntos = new List<MemoryStream>();
            List<string> NameAdjuntos = new List<string>();

            foreach (TipoProducto oProd in olstTipoProducto) {
                oInstitucionEducativa.Cod_ProductId = Convert.ToInt32(oProd.ID);

                TipoInstitucionEducativaDAO objn_InstitucionEducativa = new TipoInstitucionEducativaDAO();
                List<Institucion_Educativa> ListarReporteExcel = objn_InstitucionEducativa.ListarCodigosDetallesPagos(oInstitucionEducativa);
                StringBuilder sb = cUtil.sbDatosConsultaPagos(oInstitucionEducativa.Cod_ProductId, ListarReporteExcel);


                string nameFile = InstitucionEducativaCod + oProd.Nombre.Replace(" ", "") + ".xls";
                //string path = Server.MapPath("~/rptTemp/" + InstitucionEducativaCod + oProd.Nombre.Replace(" ","") + ".xls");

                //System.IO.StreamWriter file;// = new System.IO.StreamWriter(path);
                //file= File.AppendText(path);
                //file.WriteLine(sb.ToString());
                //file.Flush();
                //file.Close();
                //file.Dispose();

                mensaje = mensaje + " " + oProd.Nombre + ": " + ListarReporteExcel.Count.ToString() + " pago(s)." + "<br/>";

                //
                //ExcelModel excelModel= new ExcelModel();
                //excelModel.Data= sb.ToString();
                //ExcelHelper excelHelper = new ExcelHelper();
                //ExcelMeta meta = excelHelper.GetExcelMeta(excelModel.Data);
                //var myByteArray = excelHelper.GetExcelDocument(meta);
                //
                var myString = sb.ToString();
                var myByteArray = System.Text.Encoding.UTF8.GetBytes(myString);
                var ms = new MemoryStream(myByteArray);

                adjuntos.Add(ms);
                NameAdjuntos.Add(nameFile);
            }

            mensaje = mensaje + "<br/>";
            mensaje = mensaje + "<br/>";
            mensaje = mensaje + "<span>Atentamente.</span>";
            mensaje = mensaje + "<br/>";
            mensaje = mensaje + "<br/>";
            mensaje = mensaje + "<span>MIGUEL ESPINOZA GARCIA</span><br/>";
            mensaje = mensaje + "<span>EJECUTIVO DE RIESGOS ESTUDIANTILES</span><br/>";
            mensaje = mensaje + "<span>HERMES ASESORES Y CORREDORES DE SEGUROS</span><br/>";
            mensaje = mensaje + "<span>Dirección: Calle Manco Segundo 2699 - Lima 14</span><br/>";
            mensaje = mensaje + "<span>Telf.: 421-4115 Anexo 122</span><br/>";
            mensaje = mensaje + "<span>mespinoza@hermes.pe</span><br/>";
            mensaje = mensaje + "<span>www.hermes.pe</span><br/>";

            List<USP_LISTARCONTACTOS_INST_Result> lstContactos = new List<USP_LISTARCONTACTOS_INST_Result>();
            ContactoDAO db = new ContactoDAO();
            lstContactos = db.ListarContactosxInstitucion(InstitucionEducativaId);

            string para = "";
            foreach (USP_LISTARCONTACTOS_INST_Result oContacto in lstContactos)
            {
                if (oContacto.Email.Trim().Length > 0) {
                    para = para + oContacto.Email.Trim()+";";
                }
            }

            if (para != "") {
                string Cc = "mespinoza@hermes.pe";
                cUtil.EnvioMails(para, Cc, usuario, Asunto, mensaje, true, clave, smtp, puerto, adjuntos, NameAdjuntos);
            }            

            //foreach (var item in adjuntos)
            //{
            //    File.Delete(item);
            //}
        }
            //public void EnviarMensajeEmail(string para, string asunto, string userid)
            //{
            //    string usuario = cUtil.ObtenerValorParametro("CORREO", "USER");
            //    string clave = cUtil.ObtenerValorParametro("CORREO", "CLAVE");
            //    string smtp = cUtil.ObtenerValorParametro("CORREO", "SMTP");
            //    int puerto = Convert.ToInt32(cUtil.ObtenerValorParametro("CORREO", "PUERTO"));

        //    string mensaje = "";
        //    string url = Server.MapPath(@"\Templates\TemplateEmailAfiliacion.html");

        //    mensaje = cUtil.LeerTemplateHTML(url);
        //    mensaje = mensaje.Replace("[Nombre]", txtapepaterno.Text + " " + txtapematerno.Text + "," + txtnombres.Text);
        //    mensaje = mensaje.Replace("[usuario]", txtCorreoElectronico.Text);
        //    mensaje = mensaje.Replace("[clave]", txtcontraseña.Text);
        //    mensaje = mensaje.Replace("[userid]", userid);

        //    mensaje = HttpUtility.HtmlDecode(mensaje);
        //    cUtil.EnvioMail(para, usuario, asunto, mensaje, null, true, clave, smtp, puerto);
        //}
    }
}