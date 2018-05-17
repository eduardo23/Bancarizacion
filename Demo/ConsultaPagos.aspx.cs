using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DAO_Hermes.Repositorios;
using DAO_Hermes.ViewModel;
using System.Text;
using System.IO;
using System.Data.SqlClient;
namespace Demo
{


    public partial class ConsultaPagos : System.Web.UI.Page
    {
        ////Service Autocomplete
        //[System.Web.Script.Services.ScriptMethod()]
        //[System.Web.Services.WebMethod]
        //public static List<string> SearchClientes(string prefixText, int count)
        //{
        //    using (SqlConnection conn = new SqlConnection())
        //    {
        //        //conn.ConnectionString = @"Data Source=191.98.187.194;Initial Catalog=BDHermesBancarizacionTesting2;Persist Security Info=True;User id=bancarizacionuser;password=h3rm3sb4nk42016$#";
        //        conn.ConnectionString = ConexionDAO.cnx;
        //        using (SqlCommand cmd = new SqlCommand())
        //        {
        //            cmd.CommandText = "select ID,RazonSocial from Maestro.InstitucionEducativa where RazonSocial like @SearchText + '%'";
        //            cmd.Parameters.AddWithValue("@SearchText", prefixText.ToUpper());
        //            cmd.Connection = conn;
        //            conn.Open();
        //            List<string> c = new List<string>();
        //            using (SqlDataReader sdr = cmd.ExecuteReader())
        //            {
        //                while (sdr.Read())
        //                {
        //                    c.Add(AjaxControlToolkit.AutoCompleteExtender
        //                       .CreateAutoCompleteItem(sdr["RazonSocial"].ToString(), sdr["ID"].ToString()));
        //                }
        //            }
        //            conn.Close();
        //            return c;
        //        }
        //    }
        //}
        ////Fin Service Autocomplete

        Institucion_Educativa obje_InstitucionEducativa = new Institucion_Educativa();
        TipoInstitucionEducativaDAO objn_InstitucionEducativa = new TipoInstitucionEducativaDAO();

        TipoProducto obje_TipoProducto = new TipoProducto();
        TipoProductoDAO objn_TipoProducto = new TipoProductoDAO();

        Cia_Seguro obje_CiaSeguro = new Cia_Seguro();
        CiaSeguro_DAO objn_CiaSeguro = new CiaSeguro_DAO();

        Tipo_Moneda obje_TipoMoneda = new Tipo_Moneda();
        MonedaDAO objn_TipoMoneda = new MonedaDAO();

        Bancos obje_Bancos = new Bancos();
        BancoDAO objn_Bancos = new BancoDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                ListarTipoInstitucion();
                ListarProducto();
                ListarTipoSeguro();
                ListarMoneda();
                ListarBanco();
            }
        }

        void ListarTipoInstitucion()
        {
            ddlInstitucion.DataSource = objn_InstitucionEducativa.ListarInstitucionEducativa();
            ddlInstitucion.DataTextField = "NombreInstitucion";
            ddlInstitucion.DataValueField = "CodigoInstitucion";
            ddlInstitucion.DataBind();
        }

        void ListarProducto()
        {
            DDLTipoSeguro.DataSource = objn_TipoProducto.ListarTipoProductos();
            DDLTipoSeguro.DataTextField = "Nombre";
            DDLTipoSeguro.DataValueField = "ID";
            DDLTipoSeguro.DataBind();

        }
        
        void ListarTipoSeguro()
        {
            DDLCiaSeguro.DataSource = objn_CiaSeguro.ListarSeguros();
            DDLCiaSeguro.DataTextField = "Nombre";
            DDLCiaSeguro.DataValueField = "ID";
            DDLCiaSeguro.DataBind();
        }

        void ListarMoneda()
        {
            DDLMoneda.DataSource = objn_TipoMoneda.ListarMoneda();
            DDLMoneda.DataTextField = "Nombre";
            DDLMoneda.DataValueField = "ID";
            DDLMoneda.DataBind();
        }

        void ListarBanco()
        {
            DDLBanco.DataSource = objn_Bancos.ListarBanco();
            DDLBanco.DataTextField = "Nombre";
            DDLBanco.DataValueField = "ID";
            DDLBanco.DataBind();
        }

        private Institucion_Educativa PreparaFiltros() {
            Institucion_Educativa oInstitucionEducativa = new Institucion_Educativa();

            oInstitucionEducativa.cod_IEducativa = Convert.ToInt32(ddlInstitucion.SelectedValue);
            oInstitucionEducativa.Cod_ProductId = Convert.ToInt32(DDLTipoSeguro.SelectedValue);
            oInstitucionEducativa.Cod_CiaSeguro = Convert.ToInt32(DDLCiaSeguro.SelectedValue);

            if (chkPago.Checked == true)
            {
                oInstitucionEducativa.EstadoIsPagado = 1;
            }
            else { oInstitucionEducativa.EstadoIsPagado = 0; }


            oInstitucionEducativa.Cod_Banco = Convert.ToInt32(DDLBanco.SelectedValue);
            oInstitucionEducativa.Cod_Moneda = Convert.ToInt32(DDLMoneda.SelectedValue);

            if (txtFechaInicio.Text != "")
            {
                oInstitucionEducativa.FechaInicial = Convert.ToDateTime(txtFechaInicio.Text);
            }
            else { oInstitucionEducativa.FechaInicial = null; }

            if (txtFechaFinal.Text != "")
            {
                oInstitucionEducativa.FechaFinal = Convert.ToDateTime(txtFechaFinal.Text);
            }
            else { oInstitucionEducativa.FechaFinal = null; }

            oInstitucionEducativa.TextoBusqueda = txtApenombres.Text;

            return oInstitucionEducativa;
        }

        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            Institucion_Educativa Filtros = PreparaFiltros();
            if (Filtros.Cod_ProductId == 0)
            {
                txtmensaje.Text = "Debe de seleccionar el tipo de seguro.";
                string jss = "openModal()";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
                return;
            }
            grvSeguros.DataSource = objn_InstitucionEducativa.ListarCodigosDetallesPagos(Filtros);
            grvSeguros.DataBind();
            Session["grvSeguros"] = grvSeguros.DataSource;
        }


        private void Exportar_excel()
        {
            Institucion_Educativa Filtros = PreparaFiltros();
            List<Institucion_Educativa> ListarReporteExcel = objn_InstitucionEducativa.ListarCodigosDetallesPagos(Filtros);

            if (ListarReporteExcel.Count != 0) //Preguntamos si esta lleno la Lista
            {
                StringBuilder sb = cUtil.sbDatosConsultaPagos(Convert.ToInt32(DDLTipoSeguro.SelectedValue), ListarReporteExcel);

                string file = String.Format("Cuentas-por-Cobrar_{0}_{1}.xls",
                DateTime.Now.ToString("ddMMyyyy"), DateTime.Now.ToString("HHmmss"));
                Response.Clear();
                Response.Buffer = true;
                Response.Write(sb.ToString());
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;Filename=" + file);
                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
                HttpContext.Current.Response.End();
            }
        }

        protected void btnExportarExcel_Click(object sender, EventArgs e)
        {
            Exportar_excel();
        }

        protected void grvSeguros_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvSeguros.PageIndex = e.NewPageIndex;
            grvSeguros.DataSource = Session["grvSeguros"];
            grvSeguros.DataBind();
        }

        //protected void grvSeguros_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType==DataControlRowType.DataRow)
        //    {
        //        int Estado =Convert.ToInt32(e.Row.Cells[13].Text);
        //        if (Estado==1)
        //        {
        //            e.Row.Cells[13].Text = "PAGADO";
        //        }
        //    }
        //}

        //protected void DDLInstitucionEducativa_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}