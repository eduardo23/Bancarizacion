using DAO_Hermes.Repositorios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Demo
{
    public partial class GenerarTramaOnco : System.Web.UI.Page
    {

        TipoProductoDAO objn_TipoProducto = new TipoProductoDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                ListarTipoSeguro();
            }
            
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            //if (DDLTipoSeguro.SelectedValue== "0")
            //{
            //    txtmensaje.Text = "Debe de seleccionar el tipo de seguro.";
            //    string jss = "openModal()";
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
            //    return;
            //}

            string RUTA = cUtil.ObtenerValorParametroDes("FILE", "RUTA", "GENERAR");
            string NombreArchivo = cUtil.ObtenerValorParametroDes("FILE", "ONCOLOGICO", "GENERAR");
            NombreArchivo = RUTA + NombreArchivo;

            System.Text.StringBuilder detalle = new System.Text.StringBuilder();

            DateTime ini = Convert.ToDateTime(txtFecPagoDesde.Text);
            DateTime fin = Convert.ToDateTime(txtFecPagoHasta.Text);
            string inidate = ini.ToString("yyyyMMdd");
            string findate = fin.ToString("yyyyMMdd");
            detalle.Append(GenerarTrama(Convert.ToInt32(DDLTipoSeguro.SelectedValue), inidate, findate));

            cUtil.EscribirArchivo(NombreArchivo, detalle.ToString());
            System.IO.FileInfo toDownload = new System.IO.FileInfo(NombreArchivo);
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
            Response.AddHeader("Content-Length", toDownload.Length.ToString());
            Response.ContentType = "text/plain";
            Response.WriteFile(NombreArchivo);
            Response.End();

        }

        private string GenerarTrama(int TipoSeguro, string inidate, string findate) {
            DataTable dt = new DataTable();
            DataTable dtc = new DataTable();
            ConfiguracionArchivo_DAO db = new ConfiguracionArchivo_DAO();
            System.Text.StringBuilder detalle = new System.Text.StringBuilder();
            DataSet ds = db.LIST_Datos_Trama_Onco(TipoSeguro, inidate, findate);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in ds.Tables[0].Rows)
                {
                    detalle.Append(fila[0].ToString() +
                                   fila[1].ToString() +
                                   fila[2].ToString() +
                                   QuitAccents(fila[3].ToString()) +
                                   QuitAccents(fila[4].ToString()) +
                                   QuitAccents(fila[5].ToString()) +
                                   QuitAccents(fila[6].ToString()) +
                                   fila[7].ToString() +
                                   fila[8].ToString() +
                                   fila[9].ToString() +
                                   fila[10].ToString() +
                                   fila[11].ToString() +
                                   fila[12].ToString() +
                                   fila[13].ToString() +
                                   fila[14].ToString() +
                                   fila[15].ToString() +
                                   fila[16].ToString() +
                                   fila[17].ToString() +
                                   fila[18].ToString() +
                                   QuitAccents(fila[19].ToString()) +
                                   fila[20].ToString() +
                                   fila[21].ToString() +
                                   fila[22].ToString() +
                                   fila[23].ToString() +
                                   fila[24].ToString() +
                                   fila[25].ToString() +
                                   QuitAccents(fila[26].ToString()) +
                                   QuitAccents(fila[27].ToString()) +
                                   QuitAccents(fila[28].ToString()) +
                                   QuitAccents(fila[29].ToString()) +
                                   fila[30].ToString() +
                                   fila[31].ToString() +
                                   fila[32].ToString() +
                                   fila[33].ToString() +
                                   fila[34].ToString() +
                                   fila[35].ToString() +
                                   QuitAccents(fila[36].ToString()) +
                                   fila[37].ToString() +
                                   fila[38].ToString() +
                                   fila[39].ToString() +
                                   fila[40].ToString() +
                                   QuitAccents(fila[41].ToString()) +
                                   fila[42].ToString() +
                                   fila[43].ToString() +
                                   fila[44].ToString() +
                                   fila[45].ToString() +
                                   fila[46].ToString() +
                                   QuitAccents(fila[47].ToString()) +
                                   fila[48].ToString() +
                                   fila[49].ToString() +
                                   fila[50].ToString() +
                                   fila[51].ToString());
                    detalle.Append("\r\n");
                }
            }

            return detalle.ToString();
        }

        private string QuitAccents(string texto)
        {
            string con = "áàäéèëíìïóòöúùuñÁÀÄÂÉÈËÍÌÏÓÒÖÚÙÜÑçÇ";
            string sin = "aaaeeeiiiooouuunAAA EEEIIIOOOUUUNcC";
            for (int i = 0; i < con.Length; i++)
            {
                texto = texto.Replace(con[i], sin[i]);
            }
            return texto;
        }

        protected void DDLTipoSeguro_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void ListarTipoSeguro()
        {
            DDLTipoSeguro.DataSource = objn_TipoProducto.ListarTipoProductosOnco();
            DDLTipoSeguro.DataTextField = "Nombre";
            DDLTipoSeguro.DataValueField = "ID";
            DDLTipoSeguro.DataBind();
        }
    }
}