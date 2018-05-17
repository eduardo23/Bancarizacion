using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAO_Hermes.Repositorios;
using System.Data.SqlClient;
using DAO_Hermes;
using System.Data;

namespace Demo
{
    public partial class GenerarCodigos : System.Web.UI.Page
    {
        //Service Autocomplete
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchClientes(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {

                //conn.ConnectionString = @"Data Source=191.98.187.194;Initial Catalog=BDHermesBancarizacionTesting2;Persist Security Info=True;User id=bancarizacionuser;password=h3rm3sb4nk42016$#";
                conn.ConnectionString = ConexionDAO.cnx;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select ID,NombreNatural  from Maestro.InstitucionEducativa where NombreNatural like  '%'+ @txtSearch + '%'";
                    cmd.Parameters.AddWithValue("@txtSearch", prefixText);
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                //cargarCodigos();
                CargarInstitucionEducativa();


            }
            if (Request["IEID"] != null && !Page.IsPostBack)
            {
                //DDLInEducativa.SelectedValue = Request["IEID"];
                //   DDLSeguro.SelectedValue = Request["SEID"];
                txtRznSocial.Text = Request["IEST"];
                txtSearch.Text = Request["IEST"];
                hdnIDCliente1.Value = Request["IEID"];
                txtCiaSeguro.Text = Request["SEST"];
                txtTipoSeguro.Text = Request["TSST"];
                txtNombreCorto.Text = Request["RZST"];
                txtPrima.Text = Request["Prima"];
                hdnAsociacionID.Value = Request["IDASO"];
                hdnCantidad.Value = Request["CANT"];
                hdnProductoID.Value = Request["IDproduct"];
                hdnInstitucionEducativa.Value = Request["IEID"];
                hdnTipoCargaCodigo.Value = Request["TipoCargaCodigos"];
                CargarSeguros(Convert.ToInt32(hdnInstitucionEducativa.Value));
                DDLSeguro.SelectedValue = hdnAsociacionID.Value;
                btnBuscar_Click(null, null);
                btnFiltrarSeguros_Click(null, null);
                DDLSeguro.Enabled = false;
                txtSearch.Enabled = false;
            }
        }

        void cargarCodigos()
        {
            using (CodigoDAO db = new CodigoDAO())
            {
                int idInst = 0;
                int idAsoc = 0;
                try
                {
                    idInst= Convert.ToInt32(hdnIDCliente1.Value);
                    idAsoc = Convert.ToInt32(DDLSeguro.SelectedValue);
                }
                catch (Exception ex)
                {
                    idInst = 0;
                    idAsoc = 0;
                }

                grvBancos.DataSource = db.ListarCodigosAfiliados(idInst, idAsoc);
                grvBancos.DataBind();
            }
        }

        void CargarInstitucionEducativa()
        {
            //using (InstitucionEducativaDAO db = new InstitucionEducativaDAO())
            ////{
            ////    DDLInEducativa.DataSource = db.ListarInstituciones(3);
            ////    DDLInEducativa.DataTextField = "Nombre";
            ////    DDLInEducativa.DataValueField = "ID";
            ////    DDLInEducativa.DataBind();
            //}
        }

        void CargarSeguros(int idinstitucion)
        {
            using (TipoProductoDAO db = new TipoProductoDAO())
            {
                DDLSeguro.Items.Clear();
                DDLSeguro.DataTextField = "Producto";
                DDLSeguro.DataValueField = "ProductoID";                
                DDLSeguro.DataSource = db.ListarTipoProductosxInstitucion(idinstitucion);
                DDLSeguro.DataBind();
            }
        }
        protected void grvBancos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvBancos.PageIndex = e.NewPageIndex;
            cargarCodigos();
        }

        protected void grvBancos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnGenerador_Click(object sender, EventArgs e)
        {
            try
            {
                using (CodigoDAO db = new CodigoDAO())
                {

                    int id = Convert.ToInt32(hdnAsociacionID.Value);
                    if (db.ObtenerTipoCargaCodigos(id) != 1)
                    {
                        txtmensaje.Text = "La generación de códigos para esta asociación  no  es de tipo automatica";
                        string jss0 = "openModal();";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss0, true);
                        return;
                    }

                    if (txtNombreCorto.Text.Trim() != "")
                    {
                        if (txtDescripcion.Text.Trim() == "")
                        {
                            txtmensaje.Text = "!!!La descripcion de los códigos es obligatoria !!!";
                            string jss1 = "openModal();";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss1, true);
                            return;
                        }
                    }

                    string codInsED = "";
                    string codRel = "";
                    string SegProd = "";
                    //Registrar Codigo
                    Codigo codigo = new Codigo();
                    codigo.AsociacionID = Convert.ToInt32(hdnAsociacionID.Value);
                    codigo.InstitucionEducativaID = Convert.ToInt32(hdnIDCliente1.Value);
                    codigo.ProductoID = Convert.ToInt32(hdnProductoID.Value);
                    codigo.CIASeguroID = Convert.ToInt32(hdnCiaSeguro.Value);

                    codigo.ID = Convert.ToInt32(hdnCodigo.Value == "" ? "0" : hdnCodigo.Value);
                    codigo.Descripcion = txtDescripcion.Text;
                    codigo.FechaCreacion = DateTime.Now;
                    codigo.UsuarioCreacion = Session["Usuario"].ToString();
                    int cantidad = 0;
                    int total = 0;
                    if (Request["IEID"] != "" && Request["IEID"] !=null)
                    {
                        codigo.Cantidad = Convert.ToInt32(hdnCantidad.Value) + Convert.ToInt32(txtNumerosCodigos.Text);
                        cantidad = Convert.ToInt32(hdnCantidad.Value) + 1;

                        if (cantidad == 0)
                        {
                            cantidad = 1;
                        }
                        total = Convert.ToInt32(codigo.Cantidad);
                    }
                    else 
                    {
                        cantidad = db.ObtenerCantidadCodigos( Convert.ToInt32( hdnAsociacionID.Value ));
                        codigo.Cantidad = cantidad + Convert.ToInt32(txtNumerosCodigos.Text);
                      //  cantidad = Convert.ToInt32(hdnCantidad.Value) + 1;
                        if (cantidad == 0)
                        {
                            cantidad = 1;
                        }
                        else
                        {
                            cantidad += 1;
                        }
                        total = Convert.ToInt32(codigo.Cantidad);
                    }

                    List<CodigoDetalle> listDetalle = new List<CodigoDetalle>();
                    for (int x = cantidad; x <= total; x++)
                    {
                        CodigoDetalle codDet = new CodigoDetalle();
                        codDet.Activo = true;
                        // codDet.CodigoID = codigo.ID;
                        codDet.ProductoID = Convert.ToInt32(hdnProductoID.Value);
                       ////if (x == 1)
                       ////{
                           codDet.Correlativo = x;
                       //// }
                       //// else
                       //// {
                       ////     codDet.Correlativo = x+1 ;
                       //// }

                     //   codDet.UsuarioCreacion = Session["Usuario"].ToString();
                        codDet.FechaCreacion = DateTime.Now;
                        codInsED = codigo.InstitucionEducativaID.ToString().PadLeft(5, '0');

                        codRel = x.ToString().PadLeft(5, '0');
                        SegProd = codigo.CIASeguroID.ToString().PadLeft(2, '0') + codigo.ProductoID.ToString().PadLeft(2, '0');
                        codDet.Codigo = codInsED + codRel + SegProd;
                        codDet.Activo = true;
                        //codDet.AfiliacionSeguroPadreID = 1;
                        //codDet.BancoPagoID = 1;
                        //codDet.CargaHistorialID = 1;                                       
                        codDet.Descripcion = txtDescripcion.Text + codRel;
                        codDet.FechaCreacion = DateTime.Now;
                        codDet.UsuarioCreacion = Session["Usuario"].ToString();
                        //codDet.FechaPago= DateTime.Now;
                        //codDet.IsPagado = false;
                        //codDet.MonedaPagoID = 1;
                        //codDet.Observacion = "";
                        //codDet.OperacionBancaria = "";                    
                        codDet.RecibidoBanco = false;
                        codDet.TipoCarga = true;
                        codDet.Activo = true;
                        listDetalle.Add(codDet);
                    }
                    codigo.CodigoDetalle = listDetalle;

                    hdnCodigo.Value = db.GenerarCodigos(codigo, Session["Usuario"].ToString()).ToString();
                    cargarCodigos();

                    btnBuscar_Click(null, null);
                    btnFiltrarSeguros_Click(null, null);
                    hdnCantidad.Value = Convert.ToString(total);
                    txtmensaje.Text = "¡Generación de códigos completados con exito!";
                    string jss = "openModal();";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
                }
            }
            catch (Exception ex)
            {
                txtmensaje.Text = "¡" + ex.Message + "!";
                string jss = "openModal();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
            }
    }

        
            

        protected void btnGenerar_Click(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Command(object sender, CommandEventArgs e)
        {
          
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (DDLSeguro.SelectedValue=="0")
            {
                return;
            }


            using (CodigoDAO db = new CodigoDAO())
            {
                System.Data.DataSet ds = null;
                if (Request["IEID"] != null)
                {
                    ds = db.BuscarCodigosAfiliadoAsociados (Convert.ToInt32(hdnAsociacionID.Value));
                    //ds = db.BuscarCodigosAfiliado (Convert.ToInt32(DDLSeguro.SelectedValue),
                    //                                                     Convert.ToInt32(Request["IEID"]) );
                }
               else
               {
                    ds = db.BuscarCodigosAfiliadoAsociados(Convert.ToInt32(DDLSeguro.SelectedValue));
                    //ds = db.BuscarCodigosAfiliado(Convert.ToInt32(DDLSeguro.SelectedValue),
                    //                                                         Convert.ToInt32(hdnIDCliente1.Value));
                }

                if (ds.Tables[0].Rows.Count > 0 && Request["IEID"] != null)
                {
                    txtRznSocial.Text = ds.Tables[0].Rows[0]["InstitucionEducativaRZ"].ToString();
                    txtNombreCorto.Text = ds.Tables[0].Rows[0]["InstitucionEducativa"].ToString();
                    txtCiaSeguro.Text = ds.Tables[0].Rows[0]["CiaSeguros"].ToString();
                    txtTipoSeguro.Text = ds.Tables[0].Rows[0]["TipoSeguro"].ToString();
                    txtPrima.Text = ds.Tables[0].Rows[0]["Prima"].ToString();
                    hdnCantidad.Value = ds.Tables[0].Rows[0]["Cantidad1"].ToString();
                    hdnAsociacionID.Value = ds.Tables[0].Rows[0]["AsociacionID"].ToString();
                    hdnCiaSeguro.Value = ds.Tables[0].Rows[0]["CiaSeguroID"].ToString();
                    hdnProductoID.Value = ds.Tables[0].Rows[0]["ProductID"].ToString();
                    hdnCodigo.Value =  ds.Tables[0].Rows[0]["ID"].ToString()=="" ? "0" : ds.Tables[0].Rows[0]["ID"].ToString();
                }

                else if (ds.Tables[0].Rows.Count> 0 && Request["IEID"] == null)
                {
                    
                    ds = db.BuscarCodigosAfiliadoAsociados(Convert.ToInt32(DDLSeguro.SelectedValue));
                    
                    txtRznSocial.Text = ds.Tables[0].Rows[0]["InstitucionEducativaRZ"].ToString();
                    txtNombreCorto.Text = ds.Tables[0].Rows[0]["InstitucionEducativa"].ToString();
                    txtCiaSeguro.Text = ds.Tables[0].Rows[0]["CiaSeguros"].ToString();
                    txtTipoSeguro.Text = ds.Tables[0].Rows[0]["TipoSeguro"].ToString();
                    txtPrima.Text = ds.Tables[0].Rows[0]["Prima"].ToString();
                    hdnCantidad.Value =   ds.Tables[0].Rows[0]["Cantidad1"].ToString()=="" ? "0" : ds.Tables[0].Rows[0]["Cantidad1"].ToString();
                    hdnAsociacionID.Value = ds.Tables[0].Rows[0]["AsociacionID"].ToString();
                    hdnCiaSeguro.Value = ds.Tables[0].Rows[0]["CiaSeguroID"].ToString();
                    hdnProductoID.Value = ds.Tables[0].Rows[0]["ProductID"].ToString();
                    hdnTipoCargaCodigo.Value= ds.Tables[0].Rows[0]["TipoCargaCodigos"].ToString();
                    hdnCodigo.Value = ds.Tables[0].Rows[0]["ID"].ToString() == "" ? "0" : ds.Tables[0].Rows[0]["ID"].ToString();

                }
                
            }

            cargarCodigos();
        }

        protected void grvBancos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int i = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "VerCodigos")
            {
                using (CodigoDAO db = new CodigoDAO())
                {
                    int Id = Convert.ToInt32(grvBancos.Rows[i].Cells[0].Text);
                    int idproducto = Convert.ToInt32(grvBancos.Rows[i].Cells[1].Text);

                    hdnCodigo.Value = Id.ToString();
                    hdnProductoID.Value = idproducto.ToString();
                    txtBusqueda.Text = "";

                    grvCodigos.DataSource = db.ListarCodigosDetalles(Id, idproducto);
                    grvCodigos.DataBind();
                    ViewState["grvCodigos"] = db.ListarCodigosDetalles(Id, idproducto);
                }
                MVCodigos.ActiveViewIndex = 1;
            }
        }

        protected void grvBancos_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            grvBancos.PageIndex = e.NewPageIndex;
            //   grvBancos.DataBind();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            MVCodigos.ActiveViewIndex = 0;
        }

        protected void grvCodigos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {           
             grvCodigos.PageIndex = e.NewPageIndex;
            //cargarCodigos();
            grvCodigos.DataSource = ViewState["grvCodigos"];
            grvCodigos.DataBind();
        }
        
        void  CargarCodigos()
        {
            using (CodigoDAO db = new CodigoDAO())
            {
            grvCodigos.DataSource = db.ListarCodigosDetalles(Convert.ToInt32(hdnIDCliente1.Value), Convert.ToInt32(hdnProductoID.Value));
            grvCodigos.DataBind();
            }
        }
        protected void BtnBuscarCodigo_Click(object sender, EventArgs e)
        {

            using (CodigoDAO db = new CodigoDAO())
            {
                if (grvCodigos.Rows.Count > 0)
                {
                    grvCodigos.DataSource = db.ListarCodigosDetalles(Convert.ToInt32(grvCodigos.Rows[0].Cells[0].Text),
                                                                                                                            Convert.ToInt32(hdnProductoID.Value),
                                                                                                                            txtBusqueda.Text);
                    grvCodigos.DataBind();
                }                
            }
        }

        protected void btnFiltrarSeguros_Click(object sender, EventArgs e)
        {
            //CargarSeguros(Convert.ToInt32(hdnIDCliente1.Value));
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (hdnIDCliente1.Value!="")
            {
                CargarSeguros(Convert.ToInt32(hdnIDCliente1.Value));
            }            
        }

        protected void grvCodigos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnBorrarCodigos_Click(object sender, EventArgs e)
        {
            if (txtNombreCorto.Text.Trim() != "")
            {
                string jss = "openCargarEliminaRangos();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtNombreCorto.Text.Trim() != "")
            {
                string jss = "openCargarEliminaRangos();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
            }       
        }

        protected void btnEliminarCodigos_Click(object sender, EventArgs e)
        {
            if (TxtDesde.Text=="" || TxtHasta.Text=="")
            {
                txtmensaje.Text = "Debe ingresar el número de inicio y número fin del rango de códigos a eliminar";
                string jss2 = "openModal();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                return;
            }

            if (Convert.ToInt32( TxtDesde.Text)  > Convert.ToInt32( TxtHasta.Text ))
            {
                txtmensaje.Text = "El número de inicio debe ser menor al número de fin";
                string jss2 = "openModal();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                return;
            }

            try
            {
                CodigoDAO db = new CodigoDAO();
                int res = db.EliminarCodigosGenerados(Convert.ToInt32(TxtDesde.Text), Convert.ToInt32(TxtHasta.Text), Convert.ToInt32(hdnCodigo.Value));
                if (res==1)
                    {
                    txtmensaje.Text = "Se eliminarón con exito, los codigos desde: " + TxtDesde.Text + " hasta el: " + TxtHasta.Text;
                    string jss = "openModal();";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
                    return;
                   }
                else if (res== 0)
                {
                    txtmensaje.Text = "No se pudo generar los códigos especificados ";
                    string jss = "openModal();";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
                    return;
                }
                else if (res ==-1)
                {
                    txtmensaje.Text = "Existen códigos afiliados en el intervalo de codigos especificados";
                    string jss = "openModal();";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
                    return;
                }

            }
            catch(Exception ex)
            {
                txtmensaje.Text = ex.Message;
                string jss = "openModal();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
                return;
            }
        }
    }
}

