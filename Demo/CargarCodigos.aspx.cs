using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAO_Hermes;
using DAO_Hermes.Repositorios;
using System.Data;
using System.Text;


namespace Demo
{
    public partial class CargarCodigos : System.Web.UI.Page
    {
        string remplazar(string texto)
        {
            string con = "áàäéèëíìïóòöúùuñÁÀÄÉÈËÍÌÏÓÒÖÚÙÜÑçÇ";
            string sin = "aaaeeeiiiooouuunAAAEEEIIIOOOUUUNcC";
            for (int i = 0; i < con.Length; i++)
            {
                texto = texto.Replace(con[i], sin[i]);
            }
            return texto;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtIE.Text = Request["IE"];
            txtTipoSeguro.Text = Request["TS"];
            txtCiaSeguro.Text = Request["SE"];

            txtInstitucionIEMn1.Text = Request["IE"];
            txtTipoSegurosMn1.Text = Request["TS"];
            txtCiaSeguroMn1.Text = Request["SE"];
            //txtApeNombresMn1.Text = "";

            txtCiaSeguroMn2.Text = Request["SE"];
            txtTipoSeguroMn2.Text = Request["TS"];
            txtIEMn2.Text = Request["IE"];

            hdnAsociacionID.Value = Request["IDASO"];
            hdnCiaSeguro.Value = Request["SEID"];
            hdnInstitucionEducativa.Value = Request["IEID"];
            hdnProductoID.Value = Request["IDproduct"];
            hdnCodigoGen.Value = Request["codgen"];
            Session["caracteres"] = cargarCaracteresEspeciales();
            Session["errores"] = new List<error>();
            if (Page.IsPostBack == false)
            {
                try
                {
                    cargarAsegurados("");
                    cargarGrados();
                    CargaHistorial();
                    hdnUsuario.Value = Session["Usuario"].ToString();
              //     string script = "$(document).ready(function () { $('[id*=ContentPlaceHolder1_btnCargarValidar]').click(); });";
              //     ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);

                    //        string script2 = "$(document).ready(function () { $('[id*=ContentPlaceHolder1_btnDepurarGenerar]').click(); });";
                    //        ClientScript.RegisterStartupScript(this.GetType(), "load", script2, true);
                }
                catch (Exception ex)
                {

                }
                //  ScriptManager.GetCurrent(this).RegisterPostBackControl(this.FUFile);
            }
        }

        void cargarTipoDocumento()
        {
            //using (TipoDocumento_DAO db = new TipoDocumento_DAO())
            //{
            //    ddlTipoDocumento.DataValueField = "ID";
            //    ddlTipoDocumento.DataTextField = "Nombre";
            //    ddlTipoDocumento.DataSource = db.Listar();
            //    ddlTipoDocumento.DataBind();
            //}

            if (hdnProductoID.Value == "1")
            {
                cUtil.ListarTipoDocumentoAccidenteTodos(ddlTipoDocumento);
            }
            else
            {
                //cUtil.ListarTipoDocumentoAccidente(ddlTipoDocumento);
                //cUtil.ListarTipoDocumentoRentas(ddlTipoDocumento2);
                cUtil.ListarTipoDocumentoRentasTodos(ddlTipoDocumento);
                cUtil.ListarTipoDocumentoRentasTodos(ddlTipoDocumento2);
            }
        }

        void cargarGrados()
        {
            using (GradoDAO db = new GradoDAO())
            {
                ddlGrado.DataSource = db.Listar().OrderBy(p => p.Orden);
                ddlGrado.DataValueField = "Id";
                ddlGrado.DataTextField = "Nombre";
                ddlGrado.DataBind();
                
            }
        }

        void cargarAsegurados(string pAsegurado)
        {
            using (CodigoDAO db = new CodigoDAO())
            {
                DataTable ds = db.ListarAlumnosAsegurados(Convert.ToInt32(hdnInstitucionEducativa.Value),
                                                                                                                                         Convert.ToInt32(hdnProductoID.Value),
                                                                                                                                         Convert.ToInt32(hdnCiaSeguro.Value),
                                                                                                                                         Convert.ToInt32(hdnAsociacionID.Value),pAsegurado).Tables[0];

                grvAsegurados.DataSource = ds;
                grvAsegurados.DataBind();
                lblCantidad.Text = ds.Rows.Count.ToString();
            }
        }

        DataTable cargarAseguradosExportar()
        {
            using (CodigoDAO db = new CodigoDAO())
            {
                DataTable ds = db.ListarAlumnosAsegurados(Convert.ToInt32(hdnInstitucionEducativa.Value),
                                                                                                                                         Convert.ToInt32(hdnProductoID.Value),
                                                                                                                                         Convert.ToInt32(hdnCiaSeguro.Value),
                                                                                                                                         Convert.ToInt32(hdnAsociacionID.Value), txtApeNombresMn1.Text).Tables[0];

                return ds;
            }
        }


        void CargaHistorial()
        {
            //ES 15:01, comentado presenta errores... sin datos
            /*using (HistorialDAO db = new HistorialDAO())
            {
                grvHistorialCargas.DataSource = db.ListarHistorialCarga( Convert.ToInt32(hdnAsociacionID.Value));
                grvHistorialCargas.DataBind();
            }*/
        }

        void cargarAseguradosNombre(string Nombre)
        {
            using (Alumno_DAO db = new Alumno_DAO())
            {
                grvAsegurados.DataSource = db.ListarAlumnosAseguradosNombre(Convert.ToInt32(hdnInstitucionEducativa.Value),
                                                                                                                                        Convert.ToInt32(hdnProductoID.Value),
                                                                                                                                        Convert.ToInt32(hdnCiaSeguro.Value),
                                                                                                                                        Convert.ToInt32(hdnAsociacionID.Value), Nombre).Tables[0];
                grvAsegurados.DataBind();
            }
        }

        protected void btnCargarValidar_Click(object sender, EventArgs e)
        {
            if (FUFile.PostedFile.FileName != "")
            {
                try
                {
                    using (ConfiguracionArchivo_DAO dbArch = new ConfiguracionArchivo_DAO())
                    {
                        DataSet dsFileCargaExcel = null;
                        DataSet dsFileCargaTemplate = null;
                        int idproducto = Convert.ToInt32(hdnProductoID.Value);
                        int idSeguro = Convert.ToInt32(hdnCiaSeguro.Value);
                        int idCfg = 1;
                        string archTemplateAcc = "";
                        //Guardar Libro de excel en carpeta temporal
                        string archExcel = Server.MapPath("files") + "//" + FUFile.PostedFile.FileName;
                        FUFile.PostedFile.SaveAs(archExcel);

                        if (hdnProductoID.Value == "1")
                        {
                            int IdFileCarga = dbArch.OBTENER_CFG_CARGA(idSeguro, idproducto, idCfg);
                            dsFileCargaTemplate = dbArch.OBTENER_CAMPOS_CARGA(IdFileCarga);
                            archTemplateAcc = cUtil.ObtenerValorParametro("ACCIDENTES", hdnCiaSeguro.Value);
                            dsFileCargaExcel = cUtil.ObtenerDatosExcel(archExcel, "[Formato Asegurados$]");
                            //Validar Nombre0000000000 de archivo
                        
                            int filaExcel = 0;
                            foreach (DataRow Fila in dsFileCargaTemplate.Tables[0].Rows)
                            {
                                string FieldExcel = dsFileCargaExcel.Tables[0].Columns[filaExcel].ColumnName.ToLower();
                                //   FieldExcel = FieldExcel.Replace(" ", "");
                                //   FieldExcel = FieldExcel.Replace("/", "");
                                FieldExcel = FieldExcel.Replace("_", " ");
                                FieldExcel = FieldExcel.Replace(".", "");
                                // FieldExcel = FieldExcel.Replace("/", "");
                                FieldExcel = FieldExcel.Replace("#", ".");
                                FieldExcel = FieldExcel.Replace("\r\n", " ");

                                //  FieldExcel = remplazar(FieldExcel);
                                string FieldCFG = Fila[2].ToString().ToLower().Replace("\r\n", " ");
                                if (FieldCFG == FieldExcel.ToLower())
                                {
                                }
                                else
                                {
                                    txtmensaje.Text = "La columna :" + dsFileCargaExcel.Tables[0].Columns[filaExcel].ColumnName.ToLower() + " es invalida";
                                    string jss2 = "openModal();";
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                                    return;
                                }
                                filaExcel += 1;
                            }
                            lblCantidadCargados.Text = dsFileCargaExcel.Tables[0].Rows.Count.ToString() + " registros cargados";
                            switch (hdnCiaSeguro.Value)
                            {
                                case "1":
                                    grvPacificoAccidentes.DataSource = dsFileCargaExcel.Tables[0];
                                    grvPacificoAccidentes.DataBind();
                                    break;
                                case "2":
                                    GrvRimacAccidentes.DataSource = dsFileCargaExcel.Tables[0];
                                    GrvRimacAccidentes.DataBind();
                                    break;
                                case "3":
                                    GrvPositivaAccidentes.DataSource = dsFileCargaExcel.Tables[0];
                                    GrvPositivaAccidentes.DataBind();
                                    break;
                            }
                        }
                        // }

                        else if (hdnProductoID.Value == "2")
                        {
                            int iproducto = Convert.ToInt32(hdnProductoID.Value);
                            int IdFileCarga = dbArch.OBTENER_CFG_CARGA(idSeguro, iproducto, idCfg);
                            dsFileCargaTemplate = dbArch.OBTENER_CAMPOS_CARGA(iproducto);
                            archTemplateAcc = cUtil.ObtenerValorParametro("RENTAS", "RENTA-GENERAL");
                            dsFileCargaExcel = cUtil.ObtenerDatosExcel(archExcel, "[ALUMNOS]");
                            
                            //if (System.IO.Path.GetFileName(archExcel) != archTemplateAcc)
                                if (1>3)
                                {
                                txtmensaje.Text = "El nombre de archivo no es valido para el seguro de Accidentes seleccionado se esperaba: " + archTemplateAcc;
                                string jss2 = "openModal()";
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                                return;
                            }
                            else
                            {
                                int filaExcel = 0;
                                for (int y = 0; y <= dsFileCargaTemplate.Tables[0].Rows.Count - 1; y++)
                                {
                                    string FieldExcel = dsFileCargaExcel.Tables[0].Columns[filaExcel].ColumnName.ToLower();
                                    FieldExcel = FieldExcel.Replace("num# documento", "Num. Documento");
                                    //   FieldExcel = FieldExcel.Replace(" ", "");
                                    //   FieldExcel = FieldExcel.Replace(" ", "");
                                    FieldExcel = FieldExcel.Replace("/", "");
                                    FieldExcel = FieldExcel.Replace("_", " ");
                                    //     FieldExcel = FieldExcel.Replace(".", "");
                                    FieldExcel = FieldExcel.Replace("/", "");
                                    FieldExcel = FieldExcel.Replace("#", "");
                                    //   FieldExcel = FieldExcel.Replace(".", "");
                                    FieldExcel = FieldExcel.Replace("/", "");
                                    FieldExcel = FieldExcel.Replace("#", "");
                                    FieldExcel = FieldExcel.Replace("f7", "");
                                    FieldExcel = FieldExcel.Replace("f16", "");
                                    FieldExcel = remplazar(FieldExcel);

                                    string valor = dsFileCargaTemplate.Tables[0].Rows[y][2].ToString();
                                    if (y > 8 && y < 16)
                                    {
                                        valor = valor + "1";
                                    }
                                    valor = valor.Replace("grado1", "grado");
                                    if (valor.ToLower() == FieldExcel.ToLower())
                                    {
                                    }
                                    else
                                    {
                                        txtmensaje.Text = "La columna :" + dsFileCargaExcel.Tables[0].Columns[y].ColumnName.ToLower() + " es invalida";
                                        string jss2 = "openModal();";
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                                        return;
                                    }
                                    filaExcel += 1;
                                }
                                lblCantidadCargados.Text = dsFileCargaExcel.Tables[0].Rows.Count.ToString() + " registros cargados";
                                grvDatosIE_RENTA.DataSource = dsFileCargaExcel;
                                grvDatosIE_RENTA.DataBind();
                            }
                            lblCantidadCargados.Text = dsFileCargaExcel.Tables[0].Rows.Count.ToString() + " registros cargados";
                        }
                    }
                    btnDepurarGenerar.Enabled =true;
                    Session["Exportado"] = "false";
                }
                catch (Exception ex)
                {
                    txtmensaje.Text = ex.Message;
                    //txtmensaje.Text = "El archivo de trama seleccionado no cumple con la trama requerida";
                    string jss2 = "openModal();";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                    return;
                }
            }
        }
        bool ValidarInfoAlumnos(GridView grv)
        {
            bool res = false;
            foreach (GridViewRow fila in grv.Rows)
            {
                Label lblerror = (Label)fila.FindControl("lblerror");
                if (lblerror.Text == "SI")
                {
                    res = true;
                    break;
                }
            }
            return res;
        }

        protected void btnDepurarGenerar_Click(object sender, EventArgs e)
        {
            int total = 0;
            if (Session["Exportado"].ToString() == "false")
            {
                Session["Exportado"] = "true";
                if (hdnProductoID.Value == "1" && hdnCiaSeguro.Value == "3")
                {
                    if (ValidarInfoAlumnos(GrvPositivaAccidentes) == true)
                    {
                        txtmensaje.Text = "El archivo de trama proporcionado contiene errores: Existen alumnos con seguro vigente ";
                        string jss2 = "openModal();";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                        return;
                    }
                    //Establecer la cantidad de codigos a Generar
                    using (CodigoDAO db = new CodigoDAO())
                    {
                        //Crear un codigo
                        bool tienecodigo = false;
                        Codigo codigo = new Codigo();
                        int cantCodigos = Convert.ToInt32(GrvPositivaAccidentes.Rows.Count);

                        Alumno_DAO dbalu = new Alumno_DAO();
                        int? icod = db.BuscarCodigoGenAsociacion(Convert.ToInt32(hdnAsociacionID.Value));
                        if (icod != null && icod != 0) { hdnCodigoGen.Value = icod.ToString(); tienecodigo = true; } else { hdnCodigoGen.Value = "0"; tienecodigo = false; }
                        int CodGeneradox = dbalu.OBTENER_CANT_CODIGOS(Convert.ToInt32(hdnCodigoGen.Value));

                        if (CodGeneradox != 0)
                        {
                            codigo.Cantidad = cantCodigos + CodGeneradox;
                            tienecodigo = true;
                        }
                        else
                        {
                            codigo.Cantidad = cantCodigos;
                        }

                        codigo.AsociacionID = Convert.ToInt32(hdnAsociacionID.Value);
                        codigo.InstitucionEducativaID = Convert.ToInt32(hdnInstitucionEducativa.Value);
                        codigo.ProductoID = Convert.ToInt32(hdnProductoID.Value);
                        codigo.CIASeguroID = Convert.ToInt32(hdnCiaSeguro.Value);
                        codigo.Descripcion = "";
                        codigo.FechaCreacion = DateTime.Now;
                        codigo.UsuarioCreacion = Session["Usuario"].ToString();
                        total = Convert.ToInt32(codigo.Cantidad);

                        Alumno_DAO dbAlumno = new Alumno_DAO();
                        List<Alumno> alumnos = new List<Alumno>();
                        int fila = 0;

                        foreach (GridViewRow filaRow in GrvPositivaAccidentes.Rows)
                        {
                            Alumno alumno = new Alumno();
                            alumno.ApellidoPaternno = HttpUtility.HtmlDecode(GrvPositivaAccidentes.Rows[fila].Cells[2].Text);
                            alumno.ApellidoMaterno = HttpUtility.HtmlDecode(GrvPositivaAccidentes.Rows[fila].Cells[3].Text);
                            alumno.Nombre = HttpUtility.HtmlDecode(GrvPositivaAccidentes.Rows[fila].Cells[4].Text);
                            alumno.FechaNacimiento = Convert.ToDateTime(GrvPositivaAccidentes.Rows[fila].Cells[6].Text);
                            alumno.TipoDocumentoID = 2;
                            alumno.NumeroDocumento = HttpUtility.HtmlDecode(GrvPositivaAccidentes.Rows[fila].Cells[7].Text);
                            alumno.Sexo = GrvPositivaAccidentes.Rows[fila].Cells[5].Text.ToUpper() == "M" ? 1 : GrvPositivaAccidentes.Rows[fila].Cells[5].Text.ToUpper() == "F" ? 2 : 0;
                            try
                            {
                                //alumno.GradoID = dbAlumno.ObtenerGradoIDXNombre(GrvPositivaAccidentes.Rows[fila].Cells[7].Text.Trim() == "I" ? "I1" : GrvPositivaAccidentes.Rows[fila].Cells[7].Text.Trim());
                                alumno.GradoID = dbAlumno.ObtenerGradoIDXNombre(GrvPositivaAccidentes.Rows[fila].Cells[8].Text.Trim() == "I" ? "I1" : GrvPositivaAccidentes.Rows[fila].Cells[8].Text.ToUpper().Replace("BACHILLERATO", "S6"));
                            }
                            catch (Exception ex)
                            { }

                            alumno.Seccion = GrvPositivaAccidentes.Rows[fila].Cells[9].Text;
                            alumno.FechaCreacion = DateTime.Now.Date;
                            alumno.UsuarioCreacion = Session["Usuario"].ToString();
                            alumno.Estado = true;

                            alumnos.Add(alumno);
                            fila += 1;
                        }

                        int? codGenerado = dbAlumno.AgregarAfiliadoCargaAccidentesPositiva(codigo, alumnos, Session["Usuario"].ToString(),
                                                                                                               Convert.ToInt32(hdnInstitucionEducativa.Value),
                                                                                                              Convert.ToInt32(hdnProductoID.Value), "", codigo.Cantidad, Convert.ToInt32(Request["CANT"]) + 1, tienecodigo, Convert.ToInt32(hdnCodigoGen.Value == "" ? "0" : hdnCodigoGen.Value));

                        Alumno_DAO dbAlumnoExp = new Alumno_DAO();

                        DataSet dt = dbAlumnoExp.GEN_EXPORTARCODIGOS_ACCIDENTES(Convert.ToInt32(codGenerado), Convert.ToInt32(Request["CANT"]));
                        //Request["CANT"] = codigo.Cantidad;
                        if (dt.Tables[0].Rows.Count > 0)
                        {
                            HistorialDAO dbHist = new HistorialDAO();
                            CargaHistorial ch = new CargaHistorial();
                            ch.CodigoID = dt.Tables[0].Rows.Count;
                            ch.AsociacionID = Convert.ToInt32(hdnAsociacionID.Value);
                            ch.FechaCreacion = DateTime.Now;
                            ch.UsuarioCreacion = Session["Usuario"].ToString();
                            ch.Activo = true;
                            dbHist.RegistrarCargaHistorialCarga(ch);
                            //  ExportarCodigosAcccidentes(dt.Tables[0]);
                            StringBuilder sb = new StringBuilder();

                            sb.Append("<table cellpadding='0' cellspacing='0' border='1'>");
                            sb.Append("<tr style='color:White; font-weight:bold; font-family:Arial; font-size:10px;'>");
                            sb.Append("<td style='background-color:#FF0000;'>ITEM</td>");
                            sb.Append("<td style='background-color:#FF0000;'>NombreNatural</td>");
                            sb.Append("<td style='background-color:#FF0000;'>GastoCuracion</td>");
                            sb.Append("<td style='background-color:#FF0000;'>InvalidezPermanenteTotal</td>");
                            sb.Append("<td style='background-color:#FF0000;'>InvalidezPermanenteParcial</td>");
                            sb.Append("<td style='background-color:#FF0000;'>MuerteAccidental</td>");
                            sb.Append("<td style='background-color:#FF0000;'>GastosSepelio</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Moneda</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Prima</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Deducible</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Codigo de Pago</td>");
                            sb.Append("<td style='background-color:#FF0000;'>TipoDoc</td>");
                            sb.Append("<td style='background-color:#FF0000;'>NumeroDocumento</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Apellido Paterno</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Apelllido Materno</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Nombre</td>");
                            sb.Append("<td style='background-color:#FF0000;'>FechaNacimiento</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Grado</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Seccion</td>");
                            sb.Append("</tr>");


                            foreach (DataRow item in dt.Tables[0].Rows)
                            {
                                sb.Append("<tr style='color:Black; font-family:Arial; font-size:10px;'>");
                                sb.Append(String.Format("<td align='left'>{0}</td>", item[0].ToString()));
                                sb.Append(String.Format("<td align='left'>{0}</td>", HttpUtility.HtmlDecode(item[1].ToString())));
                                sb.Append(@"<td  style='mso-number-format:\#\,\#\#0\.00'>" + item[2].ToString() + "</td>");
                                sb.Append(@"<td  style='mso-number-format:\#\,\#\#0\.00'>" + item[3].ToString() + "</td>");
                                sb.Append(@"<td  style='mso-number-format:\#\,\#\#0\.00'>" + item[4].ToString() + "</td>");
                                sb.Append(@"<td  style='mso-number-format:\#\,\#\#0\.00'>" + item[5].ToString() + "</td>");
                                sb.Append(@"<td  style='mso-number-format:\#\,\#\#0\.00'>" + item[6].ToString() + "</td>");
                                sb.Append(String.Format("<td align='center'>{0}</td>", item[7].ToString()));
                                sb.Append(@"<td  style='mso-number-format:\#\,\#\#0\.00'>" + item[8].ToString() + "</td>");
                                sb.Append(@"<td  style='mso-number-format:\#\,\#\#0\.00'>" + item[9].ToString() + "</td>");
                                sb.Append("<td  style='mso-number-format:\\@' >" + item[10].ToString() + "</td>");
                                sb.Append(String.Format("<td align='rigth'>{0}</td>", item[11].ToString()));
                                sb.Append(String.Format("<td  style='mso-number-format:\\@'>{0}</td>", item[12].ToString()));
                                sb.Append(String.Format("<td align='rigth'>{0}</td>", HttpUtility.HtmlDecode(item[13].ToString())));
                                sb.Append(String.Format("<td align='rigth'>{0}</td>", HttpUtility.HtmlDecode(item[14].ToString())));
                                sb.Append(String.Format("<td align='rigth'>{0}</td>", HttpUtility.HtmlDecode(item[15].ToString())));
                                sb.Append(@"<td style='mso-number-format:dd\-mm\-yyyy'>" + item[16].ToString());
                                sb.Append(String.Format("<td align='rigth'>{0}</td>", item[17].ToString()));
                                sb.Append(String.Format("<td align='rigth'>{0}</td>", item[18].ToString()));
                                sb.Append("</tr>");
                            }
                            sb.Append("</table>");
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Write(sb.ToString());
                            Response.ContentType = "application/vnd.ms-excel";
                            Response.AddHeader("Content-Disposition",
                            String.Format("attachment;Filename=CodigosPagosAccidentes_{0}_{1}.xls",
                            DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("HH:mm:ss")));
                            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                            HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
                            HttpContext.Current.Response.End();

                        }

                    }
                }

                //ACCIDENTES - PACIFICO
                else if (hdnProductoID.Value == "1" && hdnCiaSeguro.Value == "1")
                {
                    if (ValidarInfoAlumnos(grvPacificoAccidentes) == true)
                    {
                        txtmensaje.Text = "El archivo de trama proporcionado contiene errores: Existen alumnos con seguro vigente ";
                        string jss2 = "openModal();";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                        return;
                    }

                    //Establecer la cantidad de codigos a Generar
                    using (CodigoDAO db = new CodigoDAO())
                    {
                        //Crear un codigo
                        bool tienecodigo = false;
                        Codigo codigo = new Codigo();
                        int cantCodigos = Convert.ToInt32(grvPacificoAccidentes.Rows.Count);

                        Alumno_DAO dbalu = new Alumno_DAO();
                        int? icod = db.BuscarCodigoGenAsociacion(Convert.ToInt32(hdnAsociacionID.Value));
                        if (icod != null && icod != 0) { hdnCodigoGen.Value = icod.ToString(); tienecodigo = true; } else { hdnCodigoGen.Value = "0"; tienecodigo = false; }
                        int CodGeneradox = dbalu.OBTENER_CANT_CODIGOS(Convert.ToInt32(hdnCodigoGen.Value));

                        if (CodGeneradox != 0)
                        {
                            codigo.Cantidad = cantCodigos + CodGeneradox;
                            tienecodigo = true;
                        }
                        else
                        {
                            codigo.Cantidad = cantCodigos;
                        }

                        codigo.AsociacionID = Convert.ToInt32(hdnAsociacionID.Value);
                        codigo.InstitucionEducativaID = Convert.ToInt32(hdnInstitucionEducativa.Value);
                        codigo.ProductoID = Convert.ToInt32(hdnProductoID.Value);
                        codigo.CIASeguroID = Convert.ToInt32(hdnCiaSeguro.Value);
                        codigo.Descripcion = "";
                        codigo.FechaCreacion = DateTime.Now;
                        codigo.UsuarioCreacion = Session["Usuario"].ToString();
                        total = Convert.ToInt32(codigo.Cantidad);

                        Alumno_DAO dbAlumno = new Alumno_DAO();
                        List<Alumno> alumnos = new List<Alumno>();
                        int fila = 0;

                        foreach (GridViewRow filaRow in grvPacificoAccidentes.Rows)
                        {
                            Alumno alumno = new Alumno();
                            alumno.ApellidoPaternno = HttpUtility.HtmlDecode(grvPacificoAccidentes.Rows[fila].Cells[2].Text);
                            alumno.ApellidoMaterno = HttpUtility.HtmlDecode(grvPacificoAccidentes.Rows[fila].Cells[3].Text);
                            alumno.Nombre = HttpUtility.HtmlDecode(grvPacificoAccidentes.Rows[fila].Cells[4].Text);
                            alumno.FechaNacimiento = Convert.ToDateTime(grvPacificoAccidentes.Rows[fila].Cells[5].Text);
                            alumno.NumeroDocumento = HttpUtility.HtmlDecode(grvPacificoAccidentes.Rows[fila].Cells[7].Text);
                            alumno.TipoDocumentoID = 2;
                            alumno.Sexo = grvPacificoAccidentes.Rows[fila].Cells[6].Text.ToUpper() == "M" ? 1 : grvPacificoAccidentes.Rows[fila].Cells[6].Text.ToUpper() == "F" ? 2 : 0;

                            alumno.GradoID = dbAlumno.ObtenerGradoIDXNombre(grvPacificoAccidentes.Rows[fila].Cells[13].Text.ToUpper() == "I" ? "I1" : grvPacificoAccidentes.Rows[fila].Cells[14].Text.ToUpper().Replace("BACHILLERATO", "S6"));
                            alumno.Seccion = grvPacificoAccidentes.Rows[fila].Cells[14].Text;
                            alumno.FechaCreacion = DateTime.Now.Date;
                            alumno.UsuarioCreacion = Session["Usuario"].ToString();
                            alumno.Estado = true;

                            alumnos.Add(alumno);
                            fila += 1;
                        }
                        int? codGenerado = dbAlumno.AgregarAfiliadoCargaAccidentesPacifico(codigo, alumnos, Session["Usuario"].ToString(),
                                                                                                               Convert.ToInt32(hdnInstitucionEducativa.Value),
                                                                                                              Convert.ToInt32(hdnProductoID.Value), "", codigo.Cantidad, Convert.ToInt32(Request["CANT"]) + 1, tienecodigo, Convert.ToInt32(hdnCodigoGen.Value == "" ? "0" : hdnCodigoGen.Value), Convert.ToInt32(hdnAsociacionID.Value == "" ? "0" : hdnAsociacionID.Value));
                        Alumno_DAO dbAlumnoExp = new Alumno_DAO();
                        DataSet dt = dbAlumnoExp.GEN_EXPORTARCODIGOS_ACCIDENTES(Convert.ToInt32(codGenerado), GrvPositivaAccidentes.Rows.Count);
                        if (dt.Tables[0].Rows.Count > 0)
                        {
                            HistorialDAO dbHist = new HistorialDAO();
                            CargaHistorial ch = new CargaHistorial();
                            ch.CodigoID = dt.Tables[0].Rows.Count;
                            ch.AsociacionID = Convert.ToInt32(hdnAsociacionID.Value);
                            ch.FechaCreacion = DateTime.Now;
                            ch.UsuarioCreacion = Session["Usuario"].ToString();
                            ch.Activo = true;
                            dbHist.RegistrarCargaHistorialCarga(ch);
                            //  ExportarCodigosAcccidentes(dt.Tables[0]);
                            StringBuilder sb = new StringBuilder();

                            sb.Append("<table cellpadding='0' cellspacing='0' border='1'>");
                            sb.Append("<tr style='color:White; font-weight:bold; font-family:Arial; font-size:10px;'>");
                            sb.Append("<td style='background-color:#FF0000;'>ITEM</td>");
                            sb.Append("<td style='background-color:#FF0000;'>NombreNatural</td>");
                            sb.Append("<td style='background-color:#FF0000;'>GastoCuracion</td>");
                            sb.Append("<td style='background-color:#FF0000;'>InvalidezPermanenteTotal</td>");
                            sb.Append("<td style='background-color:#FF0000;'>InvalidezPermanenteParcial</td>");
                            sb.Append("<td style='background-color:#FF0000;'>MuerteAccidental</td>");
                            sb.Append("<td style='background-color:#FF0000;'>GastosSepelio</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Moneda</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Deducible</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Prima</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Codigo de Pago</td>");
                            sb.Append("<td style='background-color:#FF0000;'>TipoDoc</td>");
                            sb.Append("<td style='background-color:#FF0000;'>NumeroDocumento</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Apellido Paterno</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Apelllido Materno</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Nombre</td>");
                            sb.Append("<td style='background-color:#FF0000;'>FechaNacimiento</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Grado</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Seccion</td>");

                            sb.Append("</tr>");


                            foreach (DataRow item in dt.Tables[0].Rows)
                            {
                                sb.Append("<tr style='color:Black; font-family:Arial; font-size:10px;'>");
                                sb.Append(String.Format("<td align='left'>{0}</td>", item[0].ToString()));
                                sb.Append(String.Format("<td align='left'>{0}</td>", HttpUtility.HtmlDecode(item[1].ToString())));
                                sb.Append(@"<td  style='mso-number-format:\#\,\#\#0\.00'>" + item[2].ToString() + "</td>");
                                sb.Append(@"<td  style='mso-number-format:\#\,\#\#0\.00'>" + item[3].ToString() + "</td>");
                                sb.Append(@"<td  style='mso-number-format:\#\,\#\#0\.00'>" + item[4].ToString() + "</td>");
                                sb.Append(@"<td  style='mso-number-format:\#\,\#\#0\.00'>" + item[5].ToString() + "</td>");
                                sb.Append(@"<td  style='mso-number-format:\#\,\#\#0\.00'>" + item[6].ToString() + "</td>");
                                sb.Append(String.Format("<td align='center'>{0}</td>", item[7].ToString()));
                                sb.Append(@"<td  style='mso-number-format:\#\,\#\#0\.00'>" + item[8].ToString() + "</td>");
                                sb.Append(@"<td  style='mso-number-format:\#\,\#\#0\.00'>" + item[9].ToString() + "</td>");
                                sb.Append("<td  style='mso-number-format:\\@' >" + item[10].ToString() + "</td>");
                                sb.Append(String.Format("<td align='rigth'>{0}</td>", item[11].ToString()));
                                sb.Append(String.Format("<td  style='mso-number-format:\\@'>{0}</td>", item[12].ToString()));
                                sb.Append(String.Format("<td align='rigth'>{0}</td>", HttpUtility.HtmlDecode(item[13].ToString())));
                                sb.Append(String.Format("<td align='rigth'>{0}</td>", HttpUtility.HtmlDecode(item[14].ToString())));
                                sb.Append(String.Format("<td align='rigth'>{0}</td>", HttpUtility.HtmlDecode(item[15].ToString())));
                                sb.Append(@"<td style='mso-number-format:dd\-mm\-yyyy'>" + item[16].ToString());
                                sb.Append(String.Format("<td align='rigth'>{0}</td>", item[17].ToString()));
                                sb.Append(String.Format("<td align='rigth'>{0}</td>", item[18].ToString()));
                                //    sb.Append(String.Format("<td align='rigth'>{0}</td>", item[22].ToString()));
                                sb.Append("</tr>");
                            }
                            sb.Append("</table>");
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Write(sb.ToString());
                            Response.ContentType = "application/vnd.ms-excel";
                            Response.AddHeader("Content-Disposition",
                            String.Format("attachment;Filename=CodigosPagosAccidentes_{0}_{1}.xls",
                            DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("HH:mm:ss")));
                            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                            HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
                            HttpContext.Current.Response.End();

                        }

                    }
                }

                //ACCIDENTES - RIMAC
                else
                    if (hdnProductoID.Value == "1" && hdnCiaSeguro.Value == "2")
                {

                    if (ValidarInfoAlumnos(GrvRimacAccidentes) == true)
                    {
                        txtmensaje.Text = "El archivo de trama proporcionado contiene errores: Existen alumnos con seguro vigente ";
                        string jss2 = "openModal();";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);

                        return;
                    }

                    using (CodigoDAO db = new CodigoDAO())
                    {
                        //Crear un codigo
                        bool tieneCodigo = false;
                        Codigo codigo = new Codigo();

                        int cantCodigos = Convert.ToInt32(GrvRimacAccidentes.Rows.Count);

                        Alumno_DAO dbalu = new Alumno_DAO();
                        int? icod = db.BuscarCodigoGenAsociacion(Convert.ToInt32(hdnAsociacionID.Value));
                        if (icod != null && icod != 0) { hdnCodigoGen.Value = icod.ToString(); tieneCodigo = true; } else { hdnCodigoGen.Value = "0"; tieneCodigo = false; }
                        int CodGeneradox = dbalu.OBTENER_CANT_CODIGOS(Convert.ToInt32(hdnCodigoGen.Value));

                        if (CodGeneradox != 0)
                        {
                            codigo.Cantidad = cantCodigos + CodGeneradox;
                            tieneCodigo = true;
                        }
                        else
                        {
                            codigo.Cantidad = cantCodigos;
                        }

                        codigo.AsociacionID = Convert.ToInt32(hdnAsociacionID.Value);
                        codigo.InstitucionEducativaID = Convert.ToInt32(hdnInstitucionEducativa.Value);
                        codigo.ProductoID = Convert.ToInt32(hdnProductoID.Value);
                        codigo.CIASeguroID = Convert.ToInt32(hdnCiaSeguro.Value);
                        codigo.Descripcion = "";
                        codigo.FechaCreacion = DateTime.Now;
                        codigo.UsuarioCreacion = Session["Usuario"].ToString();
                        total = Convert.ToInt32(codigo.Cantidad);

                        Alumno_DAO dbAlumno = new Alumno_DAO();
                        List<Alumno> alumnos = new List<Alumno>();
                        int fila = 0;

                        foreach (GridViewRow filaRow in GrvRimacAccidentes.Rows)
                        {
                            Alumno alumno = new Alumno();
                            alumno.ApellidoPaternno = HttpUtility.HtmlDecode(GrvRimacAccidentes.Rows[fila].Cells[5].Text);
                            alumno.ApellidoMaterno = HttpUtility.HtmlDecode(GrvRimacAccidentes.Rows[fila].Cells[6].Text);
                            alumno.Nombre = HttpUtility.HtmlDecode(GrvRimacAccidentes.Rows[fila].Cells[7].Text);
                            alumno.FechaNacimiento = Convert.ToDateTime(GrvRimacAccidentes.Rows[fila].Cells[8].Text);
                            alumno.TipoDocumentoID = Convert.ToInt32(GrvRimacAccidentes.Rows[fila].Cells[3].Text.Replace("&nbsp;", "0"));
                            alumno.NumeroDocumento = HttpUtility.HtmlDecode(GrvRimacAccidentes.Rows[fila].Cells[4].Text);
                            alumno.Sexo = GrvRimacAccidentes.Rows[fila].Cells[12].Text.ToUpper() == "M" ? 1 : GrvRimacAccidentes.Rows[fila].Cells[12].Text.ToUpper() == "F" ? 2 : 0;
                            try
                            {
                                alumno.GradoID = dbAlumno.ObtenerGradoIDXNombre(GrvRimacAccidentes.Rows[fila].Cells[14].Text.Trim() == "I" ? "I1" : GrvRimacAccidentes.Rows[fila].Cells[14].Text.Trim().ToUpper().Replace("BACHILLERATO", "S6"));
                            }
                            catch (Exception ex)
                            {
                            }

                            alumno.Seccion = GrvRimacAccidentes.Rows[fila].Cells[15].Text.Replace("&nbsp;", "");
                            alumno.FechaCreacion = DateTime.Now.Date;
                            alumno.UsuarioCreacion = Session["Usuario"].ToString();
                            alumno.Estado = true;
                            alumnos.Add(alumno);
                            fila += 1;
                        }

                        //Generar codigo     
                        int? codGenerado = dbAlumno.AgregarAfiliadoCargaAccidentesRimac(codigo, alumnos, Session["Usuario"].ToString(),
                                                                                                             Convert.ToInt32(hdnInstitucionEducativa.Value),
                                                                                                             Convert.ToInt32(hdnProductoID.Value), "", codigo.Cantidad, Convert.ToInt32(Request["CANT"]) + 1, tieneCodigo,
                                                                                                             Convert.ToInt32(hdnCodigoGen.Value == "" ? "0" : hdnCodigoGen.Value), Convert.ToInt32(hdnAsociacionID.Value == "" ? "0" : hdnAsociacionID.Value));
                        Alumno_DAO dbAlumnoExp = new Alumno_DAO();
                        DataSet dt = dbAlumnoExp.GEN_EXPORTARCODIGOS_ACCIDENTES(codGenerado, GrvPositivaAccidentes.Rows.Count);
                        if (dt.Tables[0].Rows.Count > 0)
                        {
                            HistorialDAO dbHist = new HistorialDAO();
                            CargaHistorial ch = new CargaHistorial();
                            ch.CodigoID = dt.Tables[0].Rows.Count;
                            ch.AsociacionID = Convert.ToInt32(hdnAsociacionID.Value);
                            ch.FechaCreacion = DateTime.Now;
                            ch.UsuarioCreacion = Session["Usuario"].ToString();
                            ch.Activo = true;
                            dbHist.RegistrarCargaHistorialCarga(ch);
                            //  ExportarCodigosAcccidentes(dt.Tables[0]);
                            StringBuilder sb = new StringBuilder();

                            sb.Append("<table cellpadding='0' cellspacing='0' border='1'>");
                            sb.Append("<tr style='color:White; font-weight:bold; font-family:Arial; font-size:10px;'>");
                            sb.Append("<td style='background-color:#FF0000;'>ITEM</td>");
                            sb.Append("<td style='background-color:#FF0000;'>NombreNatural</td>");
                            sb.Append("<td style='background-color:#FF0000;'>GastoCuracion</td>");
                            sb.Append("<td style='background-color:#FF0000;'>InvalidezPermanenteTotal</td>");
                            sb.Append("<td style='background-color:#FF0000;'>InvalidezPermanenteParcial</td>");
                            sb.Append("<td style='background-color:#FF0000;'>MuerteAccidental</td>");
                            sb.Append("<td style='background-color:#FF0000;'>GastosSepelio</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Moneda</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Deducible</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Prima</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Codigo de Pago</td>");
                            sb.Append("<td style='background-color:#FF0000;'>TipoDoc</td>");
                            sb.Append("<td style='background-color:#FF0000;'>NumeroDocumento</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Apellido Paterno</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Apelllido Materno</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Nombre</td>");
                            sb.Append("<td style='background-color:#FF0000;'>FechaNacimiento</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Grado</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Seccion</td>");
                            sb.Append("</tr>");


                            foreach (DataRow item in dt.Tables[0].Rows)
                            {
                                sb.Append("<tr style='color:Black; font-family:Arial; font-size:10px;'>");
                                sb.Append(String.Format("<td align='left'>{0}</td>", item[0].ToString()));
                                sb.Append(String.Format("<td align='left'>{0}</td>", HttpUtility.HtmlDecode(item[1].ToString())));
                                sb.Append(@"<td  style='mso-number-format:\#\,\#\#0\.00'>" + item[2].ToString() + "</td>");
                                sb.Append(@"<td  style='mso-number-format:\#\,\#\#0\.00'>" + item[3].ToString() + "</td>");
                                sb.Append(@"<td  style='mso-number-format:\#\,\#\#0\.00'>" + item[4].ToString() + "</td>");
                                sb.Append(@"<td  style='mso-number-format:\#\,\#\#0\.00'>" + item[5].ToString() + "</td>");
                                sb.Append(@"<td  style='mso-number-format:\#\,\#\#0\.00'>" + item[6].ToString() + "</td>");
                                sb.Append(String.Format("<td align='center'>{0}</td>", item[7].ToString()));
                                sb.Append(@"<td  style='mso-number-format:\#\,\#\#0\.00'>" + item[8].ToString() + "</td>");
                                sb.Append(@"<td  style='mso-number-format:\#\,\#\#0\.00'>" + item[9].ToString() + "</td>");
                                sb.Append("<td  style='mso-number-format:\\@' >" + item[10].ToString() + "</td>");
                                sb.Append(String.Format("<td align='rigth'>{0}</td>", item[11].ToString()));
                                sb.Append(String.Format("<td  style='mso-number-format:\\@'>{0}</td>", item[12].ToString()));
                                sb.Append(String.Format("<td align='rigth'>{0}</td>", HttpUtility.HtmlDecode(item[13].ToString())));
                                sb.Append(String.Format("<td align='rigth'>{0}</td>", HttpUtility.HtmlDecode(item[14].ToString())));
                                sb.Append(String.Format("<td align='rigth'>{0}</td>", HttpUtility.HtmlDecode(item[15].ToString())));
                                sb.Append(@"<td style='mso-number-format:dd\-mm\-yyyy'>" + item[16].ToString());
                                sb.Append(String.Format("<td align='rigth'>{0}</td>", item[17].ToString()));
                                sb.Append(String.Format("<td align='rigth'>{0}</td>", item[18].ToString()));
                                sb.Append("</tr>");
                            }
                            sb.Append("</table>");
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Write(sb.ToString());
                            Response.ContentType = "application/vnd.ms-excel";
                            Response.AddHeader("Content-Disposition",
                            String.Format("attachment;Filename=CodigosPagosAccidentes_{0}_{1}.xls",
                            DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("HH:mm:ss")));
                            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                            HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
                            HttpContext.Current.Response.End();

                            //txtmensaje.Text = "Codigos generados con exito.... falta generar el reporte de excel!!";
                            //string jss2 = "openModal();";
                            //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                        }
                    }
                }

                else if (hdnProductoID.Value == "2")
                {

                    if (ValidarInfoAlumnos(grvDatosIE_RENTA) == true)
                    {
                        txtmensaje.Text = "El archivo de trama proporcionado contiene errores: Existen padres con seguro vigente ";
                        string jss2 = "openModal();";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                        return;
                    }

                    //Establecer la cantidad de codigos a Generar
                    int cantCodigos = Convert.ToInt32(grvDatosIE_RENTA.Rows.Count);

                    using (CodigoDAO db = new CodigoDAO())
                    {
                        //Crear un codigo
                        bool tieneCodigoRentas = false;
                        Codigo codigo = new Codigo();
                        int cantCodigosRentas = Convert.ToInt32(grvDatosIE_RENTA.Rows.Count);

                        Alumno_DAO dbalu = new Alumno_DAO();
                        int? icod = db.BuscarCodigoGenAsociacion(Convert.ToInt32(hdnAsociacionID.Value));
                        if (icod != null && icod != 0) { hdnCodigoGen.Value = icod.ToString(); tieneCodigoRentas = true; } else { hdnCodigoGen.Value = "0"; tieneCodigoRentas = false; }
                        int CodGeneradox = dbalu.OBTENER_CANT_CODIGOS(Convert.ToInt32(hdnCodigoGen.Value));

                        if (CodGeneradox != 0)
                        {
                            codigo.Cantidad = cantCodigos + CodGeneradox;
                            tieneCodigoRentas = true;
                        }
                        else
                        {
                            codigo.Cantidad = cantCodigos;
                        }

                        //Crear un codigo                   
                        codigo.AsociacionID = Convert.ToInt32(hdnAsociacionID.Value);
                        codigo.InstitucionEducativaID = Convert.ToInt32(hdnInstitucionEducativa.Value);
                        codigo.ProductoID = Convert.ToInt32(hdnProductoID.Value);
                        codigo.CIASeguroID = Convert.ToInt32(hdnCiaSeguro.Value);
                        //    codigo.Cantidad = cantCodigos;
                        codigo.Descripcion = "";
                        codigo.FechaCreacion = DateTime.Now;
                        codigo.UsuarioCreacion = hdnUsuario.Value;
                        total = Convert.ToInt32(codigo.Cantidad);

                        // int filaContador = 0;
                        List<Padre> padres = new List<Padre>();

                        List<Alumno> beneficiarios = new List<Alumno>();
                        for (int fila = 0; fila < grvDatosIE_RENTA.Rows.Count; fila++)
                        {
                            using (AfiliacionDAO dbAfiliacion = new AfiliacionDAO())
                            {
                                using (Alumno_DAO dbAlumnox = new Alumno_DAO())
                                {
                                    Alumno beneficiario = new Alumno();
                                    beneficiario.ApellidoPaternno = HttpUtility.HtmlDecode(grvDatosIE_RENTA.Rows[fila].Cells[12].Text);
                                    beneficiario.ApellidoMaterno = HttpUtility.HtmlDecode(grvDatosIE_RENTA.Rows[fila].Cells[13].Text);
                                    beneficiario.Nombre = HttpUtility.HtmlDecode(grvDatosIE_RENTA.Rows[fila].Cells[14].Text);
                                    beneficiario.TipoDocumentoID = Convert.ToInt32(grvDatosIE_RENTA.Rows[fila].Cells[10].Text);
                                    beneficiario.NumeroDocumento = HttpUtility.HtmlDecode(grvDatosIE_RENTA.Rows[fila].Cells[11].Text);
                                    beneficiario.FechaNacimiento = Convert.ToDateTime(grvDatosIE_RENTA.Rows[fila].Cells[15].Text);
                                    beneficiario.Sexo = grvDatosIE_RENTA.Rows[fila].Cells[16].Text.ToUpper() == "M" ? 1 : grvDatosIE_RENTA.Rows[fila].Cells[16].Text.ToUpper() == "F" ? 2 : 0;
                                    beneficiario.GradoID = dbAlumnox.ObtenerGradoID(grvDatosIE_RENTA.Rows[fila].Cells[17].Text);
                                    beneficiario.Seccion = grvDatosIE_RENTA.Rows[fila].Cells[18].Text;
                                    beneficiario.FechaCreacion = DateTime.Now.Date;
                                    beneficiario.UsuarioCreacion = hdnUsuario.Value;
                                    beneficiario.Estado = true;
                                    beneficiarios.Add(beneficiario);
                                    //  string BeneficiarioPor = grvDatosIE_RENTA.Rows[fila].Cells[19].Text.ToUpper();
                                    //if (BeneficiarioPor.ToUpper().Trim() == "AMBOS PADRES")
                                    //{
                                    Padre padre = new Padre();
                                    padre.TipoDocumentoID = Convert.ToInt32(grvDatosIE_RENTA.Rows[fila].Cells[2].Text);
                                    padre.NumeroDocumento = grvDatosIE_RENTA.Rows[fila].Cells[3].Text;
                                    padre.ApellidoPaterno = HttpUtility.HtmlDecode(grvDatosIE_RENTA.Rows[fila].Cells[4].Text);
                                    padre.ApellidoMaterno = HttpUtility.HtmlDecode(grvDatosIE_RENTA.Rows[fila].Cells[5].Text);
                                    padre.Nombre = HttpUtility.HtmlDecode(grvDatosIE_RENTA.Rows[fila].Cells[6].Text);

                                    padre.FechaNacimiento = Convert.ToDateTime(grvDatosIE_RENTA.Rows[fila].Cells[7].Text);
                                    padre.TipoPadreID = grvDatosIE_RENTA.Rows[fila].Cells[9].Text.ToUpper().Trim() == "PADRE" ? 1 : grvDatosIE_RENTA.Rows[fila].Cells[9].Text.ToUpper().Trim() == "MADRE" ? 2 : grvDatosIE_RENTA.Rows[fila].Cells[9].Text.ToUpper().Trim() == "APODERADO" || grvDatosIE_RENTA.Rows[fila].Cells[9].Text.ToUpper().Trim() == "TUTOR" ? 3 : 0;
                                    //padre.TipoPadreID =Convert.ToInt32( grvDatosIE_RENTA.Rows[fila].Cells[10].Text);
                                    padre.FechaCreacion = DateTime.Now.Date;
                                    padre.UsuarioCreacion = hdnUsuario.Value;
                                    padre.Estado = true;
                                    padres.Add(padre);
                                }
                            }
                        }

                        Alumno_DAO dbAlumno = new Alumno_DAO();
                        //Generar codigo     
                        int? codGenerado = 0;


                        codGenerado = dbAlumno.AgregarAfiliadoCargaRentas(codigo, beneficiarios, padres, hdnUsuario.Value,
                                                                                                      Convert.ToInt32(hdnInstitucionEducativa.Value),
                                                                                                      Convert.ToInt32(hdnProductoID.Value), "", codigo.Cantidad, CodGeneradox + 1, tieneCodigoRentas,
                                                                                                      Convert.ToInt32(hdnCodigoGen.Value == "" ? "0" : hdnCodigoGen.Value), Convert.ToInt32(hdnAsociacionID.Value.Trim() == "" ? "0" : hdnAsociacionID.Value));


                        Alumno_DAO dbAlumnoExp = new Alumno_DAO();
                        int cant = 0;
                        if (CodGeneradox > grvDatosIE_RENTA.Rows.Count)
                        {
                            cant = CodGeneradox - grvDatosIE_RENTA.Rows.Count;
                        }
                        else
                        {
                            cant = 0;

                        }

                        DataSet dt = dbAlumnoExp.GEN_EXPORTARCODIGOS_RENTAS(codGenerado, cant);

                        if (dt.Tables[0].Rows.Count > 0)
                        {
                            //  ExportarCodigosAcccidentes(dt.Tables[0]);
                            HistorialDAO dbHist = new HistorialDAO();
                            CargaHistorial ch = new CargaHistorial();
                            ch.CodigoID = dt.Tables[0].Rows.Count;
                            ch.AsociacionID = Convert.ToInt32(hdnAsociacionID.Value);
                            ch.FechaCreacion = DateTime.Now;
                            ch.UsuarioCreacion = Session["Usuario"].ToString();
                            ch.Activo = true;
                            dbHist.RegistrarCargaHistorialCarga(ch);
                            StringBuilder sb = new StringBuilder();
                            sb.Clear();
                            sb.Append("<meta http - equiv = Content-Type content = 'text/html; charset=utf-8' >");
                            sb.Append("<table cellpadding='0' cellspacing='0' border='1'><tr>");
                            sb.Append("<td style='color:White; font-weight:bold; font-family:Arial; font-size:10px;'>");
                            sb.Append("<td style='background-color:#0B243B;color:white;text-align=center' colspan='14'><b>PADRES / ASEGURADOS</b></td>");
                            sb.Append("<td style='background-color:#FF8000;color:white;text-align=center' colspan='8'><b>ALUMNOS / BENEFICIARIOS</b></td></tr>");
                            sb.Append("<tr style='color:White;background=red; font-weight:bold; font-family:Arial; font-size:10px;'>");
                            sb.Append("<td style='background-color:#FF0000;'>ITEM</td>");
                            sb.Append("<td style='background-color:#FF0000;'>NombreNatural</td>");
                            sb.Append("<td style='background-color:#FF0000;'>PensionMensual</td>");
                            sb.Append("<td style='background-color:#FF0000;'>MesesPension</td>");
                            sb.Append("<td style='background-color:#FF0000;'>AnniosPension</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Moneda</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Prima</td>");
                            sb.Append("<td style='background-color:#FF0000;'>CodigoPago</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Tipodoc</td>");
                            sb.Append("<td style='background-color:#FF0000;'>NumeroDocumento</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Apellido Paterno</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Apellido Materno</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Nombre</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Fecha Nacimiento</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Parentesco</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Apellido Paterno</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Apellido Materno</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Nombre</td>");
                            sb.Append("<td style='background-color:#FF0000;'>TipoDoc</td>");
                            sb.Append("<td style='background-color:#FF0000;'>NumeroDocumento</td>");
                            sb.Append("<td style='background-color:#FF0000;'>FechaNacimiento</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Grado</td>");
                            sb.Append("<td style='background-color:#FF0000;'>Sección</td>");

                            sb.Append("</tr>");
                            Session["Exportado"] = "true";
                            int pos = 1;
                            foreach (DataRow item in dt.Tables[0].Rows)
                            {
                                sb.Append(@"<tr style='color:Black; font-family:Arial; font-size:10px;'>");
                                sb.Append(@"<td align='left'>" + pos + "</td>");
                                sb.Append(@"<td align='left'>" + item[1].ToString() + "</td>");
                                sb.Append(@"<td  style='mso-number-format:\#\,\#\#0\.00'>" + item[2].ToString() + "</td>");
                                sb.Append(@"<td>" + item[3].ToString() + "</td>");
                                sb.Append(@"<td>" + item[4].ToString() + "</td>");
                                sb.Append(String.Format("<td align='rigth'>{0}</td>", item[21].ToString()));
                                sb.Append(@"<td  style='mso-number-format:\#\,\#\#0\.00'>" + item[5].ToString() + "</td>");
                                sb.Append("<td  style='mso-number-format:\\@' >" + item[6].ToString() + "</td>");
                                sb.Append("<td  style='mso-number-format:\\@' >" + item[7].ToString() + "</td>");
                                sb.Append("<td  style = 'mso-number-format:\\@' >" + item[8].ToString() + "</td>");
                                sb.Append("<td  style='mso-number-format:\\@' >" + HttpUtility.HtmlDecode(item[9].ToString()) + "</td>");
                                sb.Append("<td   style='mso-number-format:\\@' >" + HttpUtility.HtmlDecode(item[10].ToString()) + "</td>");
                                sb.Append("<td  style='mso-number-format:\\@' >" + HttpUtility.HtmlDecode(item[11].ToString()) + "</td>");
                                sb.Append(@"<td style='mso-number-format:dd\-mm\-yyyy'>" + item[12].ToString() + "</td>");
                                sb.Append("<td style='mso-number-format:\\@'>" + item[22].ToString());
                                sb.Append("<td style='mso-number-format:\\@'>" + HttpUtility.HtmlDecode(item[13].ToString()) + "</td>");
                                sb.Append("<td style='mso-number-format:\\@'>" + HttpUtility.HtmlDecode(item[14].ToString()) + "</td>");
                                sb.Append("<td style='mso-number-format:\\@' >" + HttpUtility.HtmlDecode(item[15].ToString()) + "</td>");
                                sb.Append("<td style = 'mso-number-format:\\@' >" + item[16].ToString() + "</td>");
                                sb.Append("<td  style = 'mso-number-format:\\@' >" + item[17].ToString() + "</td>");
                                sb.Append(@"<td style = 'mso-number-format:dd\-mm\-yyyy'>" + item[18].ToString() + "</td>");
                                sb.Append(String.Format("<td align='rigth'>{0}</td>", item[19].ToString()));
                                sb.Append(String.Format("<td align='rigth'>{0}</td>", item[20].ToString()));

                                sb.Append("</tr>");
                                pos += 1;
                            }
                            //Guardar Historial

                            sb.Append("</table>");
                            try
                            {
                                Response.Clear();
                                Response.Buffer = true;
                                Response.Write(sb.ToString());
                                Response.ContentType = "application/vnd.ms-excel";
                                Response.AddHeader("Content-Disposition",
                                String.Format("attachment;Filename=CodigosPagosRentas_{0}_{1}.xls",
                                DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("HH:mm:ss")));

                                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                                HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
                                HttpContext.Current.Response.End();
                            }
                            catch (Exception ex)
                            {

                            }
                            finally
                            {
                      
                            }


                            //                Response.Redirect("CargarCodigos.aspx");
                            //          ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "javascript:location.reload(true);", true);
                            //txtmensaje.Text = "Codigos generados con exito...";
                            //string jss2 = "openModal();";
                            //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                        }
                    }
                }
            }
        }

        public List<string> cargarCaracteresEspeciales()
        {
            UtilDAO db = new UtilDAO();
            return db.ListarCaracteresEspeciales();
        }


        private string ValidarCaracteresEspeciales(string texto)
        {
            string novalidos = "";
            List<string> chars = (List<string>)Session["caracteres"];
            foreach (string caracter in chars)
            {
                if (HttpUtility.HtmlDecode(texto).Contains(caracter) == true)
                {
                    novalidos = caracter;
                }
            }
            return novalidos;
        }

        
          protected void btnBuscarAseguradosMn1_Click(object sender, EventArgs e)
        {
            //cargarAseguradosNombre(txtApeNombresMn1.Text);
            //lblCantidad.Text = Convert.ToString(grvAsegurados.Rows.Count);
            cargarAsegurados(txtApeNombresMn1.Text);
        }

        protected void grvAsegurados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvAsegurados.PageIndex = e.NewPageIndex;
            //cargarAseguradosNombre(txtApeNombresMn1.Text);
            cargarAsegurados(txtApeNombresMn1.Text);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "ActivaTab();", true);
        }

        protected void grvAsegurados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int fila = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "EditarAsegurado")
            {
                try
                {                    
                    cargarTipoDocumento();
                    if (hdnProductoID.Value == "1")
                    {
                        if (ddlTipoDocumento.SelectedValue == "2")
                        {
                            txtNumeroDocumento.MaxLength = 8;
                        }
                        else
                        {
                            txtNumeroDocumento.MaxLength = 20;
                        }
                    }

                    if (hdnProductoID.Value == "2")
                    {
                        if (ddlTipoDocumento2.SelectedValue == "2")
                        {
                            txtNumeroDocumento2.MaxLength = 8;
                        }
                        else
                        {
                            txtNumeroDocumento2.MaxLength = 20;
                        }
                    }
                    if(hdnProductoID.Value=="1")
                    {
                        txtApePaterno.Text = HttpUtility.HtmlDecode(grvAsegurados.Rows[fila].Cells[7].Text);
                        txtApeMaterno.Text = HttpUtility.HtmlDecode(grvAsegurados.Rows[fila].Cells[8].Text);
                        txtNombres.Text = HttpUtility.HtmlDecode(grvAsegurados.Rows[fila].Cells[9].Text);
                        ddlTipoDocumento.SelectedValue = grvAsegurados.Rows[fila].Cells[11].Text;
                        txtNumeroDocumento.Text = grvAsegurados.Rows[fila].Cells[12].Text;
                        txtFechaNacimiento.Text = Convert.ToDateTime(grvAsegurados.Rows[fila].Cells[10].Text).ToString("yyyy-MM-dd");
                        txtSeccion.Text = HttpUtility.HtmlDecode(grvAsegurados.Rows[fila].Cells[18].Text);
                        ddlGrado.SelectedValue = grvAsegurados.Rows[fila].Cells[17].Text;
                        //hdnAfiliacionSeguroID.Value = grvAsegurados.Rows[fila].Cells[12].Text;
                        hdnAlumnoID.Value = grvAsegurados.Rows[fila].Cells[16].Text;
                        hdnPadreID.Value = grvAsegurados.Rows[fila].Cells[20].Text;

                        if (grvAsegurados.Rows[fila].Cells[19].Text != "")
                        {
                            rbtGenero.SelectedIndex = Convert.ToInt32(grvAsegurados.Rows[fila].Cells[19].Text) - 1;
                        }
                        lblhijo.Text = "Datos del Asegurado: " + txtApePaterno.Text + " " + txtApeMaterno.Text + " " + txtNombres.Text; ;
                        //lblpadre.Text = "Datos del asegurado";
                    }
                    if (hdnProductoID.Value == "2")
                    {
                        txtApePaterno.Text = HttpUtility.HtmlDecode(grvAsegurados.Rows[fila].Cells[7].Text);
                        txtApeMaterno.Text = HttpUtility.HtmlDecode(grvAsegurados.Rows[fila].Cells[8].Text);
                        txtNombres.Text = HttpUtility.HtmlDecode(grvAsegurados.Rows[fila].Cells[9].Text);
                        ddlTipoDocumento.SelectedValue = grvAsegurados.Rows[fila].Cells[11].Text;
                        txtNumeroDocumento.Text = grvAsegurados.Rows[fila].Cells[12].Text;
                        txtFechaNacimiento.Text = Convert.ToDateTime(grvAsegurados.Rows[fila].Cells[10].Text).ToString("yyyy-MM-dd");
                        txtSeccion.Text = HttpUtility.HtmlDecode(grvAsegurados.Rows[fila].Cells[18].Text);
                        ddlGrado.SelectedValue = grvAsegurados.Rows[fila].Cells[17].Text;
                        //hdnAfiliacionSeguroID.Value = grvAsegurados.Rows[fila].Cells[12].Text;
                        hdnAlumnoID.Value = grvAsegurados.Rows[fila].Cells[16].Text;

                        if (grvAsegurados.Rows[fila].Cells[19].Text != "")
                        {
                            rbtGenero.SelectedIndex = Convert.ToInt32(grvAsegurados.Rows[fila].Cells[19].Text) - 1;
                        }
                        //---
                        txtApePaterno2.Text = HttpUtility.HtmlDecode(grvAsegurados.Rows[fila].Cells[21].Text);
                        txtApeMaterno2.Text = HttpUtility.HtmlDecode(grvAsegurados.Rows[fila].Cells[22].Text);
                        txtNombres2.Text = HttpUtility.HtmlDecode(grvAsegurados.Rows[fila].Cells[23].Text);
                        ddlTipoDocumento2.SelectedValue = grvAsegurados.Rows[fila].Cells[24].Text;
                        txtNumeroDocumento2.Text = grvAsegurados.Rows[fila].Cells[25].Text;
                        txtFechaNacimiento2.Text = Convert.ToDateTime(grvAsegurados.Rows[fila].Cells[26].Text).ToString("yyyy-MM-dd");
                        //hdnAfiliacionSeguroID.Value = grvAsegurados.Rows[fila].Cells[12].Text;
                        hdnPadreID.Value = grvAsegurados.Rows[fila].Cells[20].Text;
                        //if (grvAsegurados.Rows[fila].Cells[9].Text != "")
                        //{
                        //    rbtGenero.SelectedIndex = Convert.ToInt32(grvAsegurados.Rows[fila].Cells[9].Text) - 1;
                        //}
                        lblhijo.Text = "Datos del Beneficiario: " + txtApePaterno.Text + " "+txtApeMaterno.Text + " "+ txtNombres.Text ;
                        lblpadre.Text = "Datos del asegurado: "  + txtApePaterno2.Text + " " + txtApeMaterno2.Text + " " + txtNombres2.Text; ;
                        pnlBeneficiario2.Visible=true;
                    }


                    MVCodigos.ActiveViewIndex = 1;
                    //pnlBeneficiario2.Visible = false;
                    //pnlBeneficiario1.Visible = true;

                }
                catch (Exception ex)
                {

                }
            }
            if (e.CommandName == "AnularAsegurado")
            {
                try
                {
                    hdnEliminaOpcion.Value = "A";
                    hdnAlumnoSelect.Value = fila.ToString();
                    lblTitleConfirm.Text = "Anular registro de alumno";
                    lblmsgConfirm.Text = "¿Desea anular el registro del alumno: " + grvAsegurados.Rows[fila].Cells[1].Text + " " + grvAsegurados.Rows[fila].Cells[2].Text + " " + grvAsegurados.Rows[fila].Cells[3].Text + " ?";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "openConfirm();", true);
                }
                catch (Exception ex)
                {

                }
            }

            if (e.CommandName == "EliminarAsegurado")
            {
                try
                {
                    hdnEliminaOpcion.Value = "E";
                    hdnAlumnoSelect.Value = fila.ToString();
                    lblTitleConfirm.Text = "Eliminar registro de alumno";
                    lblmsgConfirm.Text = "¿Desea eliminar el registro del alumno: " + grvAsegurados.Rows[fila].Cells[1].Text + " " + grvAsegurados.Rows[fila].Cells[2].Text + " " + grvAsegurados.Rows[fila].Cells[3].Text + " ?";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "openConfirm();", true);



                }
                catch (Exception ex)
                {

                }
            }

        }

        protected void btnCancelarCiaSeguro_Click(object sender, EventArgs e)
        {
            MVCodigos.ActiveViewIndex = 0;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "ActivaTab();", true);
        }

        protected void btnGuardarCiaSeguro_Click(object sender, EventArgs e)
        {
            try
            {
                using (Alumno_DAO db = new Alumno_DAO())
                {
                    if (ddlTipoDocumento.SelectedValue == "0")
                    {
                        string jssc = "openModal();";
                        txtmensaje.Text = "Debe Seleccionar un Tipo Documento Del Asegurado";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssc, true);
                        return;
                    }
                    if (hdnProductoID.Value == "1")
                    {
                        int edad = cUtil.Edad(Convert.ToDateTime(txtFechaNacimiento.Text));
                        if (edad > 70)
                        {
                            string jssc = "openModal();";
                            txtmensaje.Text = "La edad del asegurado no puede ser mayor a los  70 años";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssc, true);
                            return;
                        }
                    }

                    if (hdnProductoID.Value == "2")
                    {
                        int edad = cUtil.Edad(Convert.ToDateTime(txtFechaNacimiento2.Text));
                        if (edad > 70)
                        {
                            string jssc = "openModal();";
                            txtmensaje.Text = "La edad del asegurado no puede ser mayor a los  70 años";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jssc, true);
                            return;
                        }
                    }

                    Alumno alu = new Alumno();
                    alu.ApellidoPaternno = txtApePaterno.Text.ToUpper();
                    alu.ApellidoMaterno = txtApeMaterno.Text.ToUpper();
                    alu.Nombre = txtNombres.Text.ToUpper();
                    alu.GradoID = Convert.ToInt32(ddlGrado.SelectedValue);
                    alu.Seccion = txtSeccion.Text.ToUpper();
                    alu.Sexo = Convert.ToInt32(rbtGenero.SelectedItem.Value);
                    alu.TipoDocumentoID = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
                    alu.NumeroDocumento = txtNumeroDocumento.Text;
                    alu.FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text);
                    db.Agregar(alu);

                    if (hdnProductoID.Value == "2")
                    {
                        using (PadreDAO dbPadre = new PadreDAO())
                        {
                            Padre padre = new Padre();
                            padre.ID = Convert.ToInt32(hdnPadreID.Value);
                            padre.ApellidoPaterno = txtApePaterno2.Text.ToUpper();
                            padre.ApellidoMaterno = txtApeMaterno2.Text.ToUpper();
                            padre.Nombre = txtNombres2.Text.ToUpper(); ;
                            padre.TipoDocumentoID = Convert.ToInt32(ddlTipoDocumento2.SelectedValue);
                            padre.NumeroDocumento = txtNumeroDocumento2.Text;
                            padre.FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento2.Text);
                            padre.BeneficiarioID = Convert.ToInt32(hdnAlumnoID.Value);
                            padre.UsuarioActualizacion = Session["Usuario"].ToString();
                            padre.FechaActualizacion = DateTime.Now.Date;
                            dbPadre.Grabar(padre);
                        }
                    }

                    cargarAsegurados(txtApeNombresMn1.Text);
                    MVCodigos.ActiveViewIndex = 0;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "ActivaTab();", true);                    
                }
            }
            catch (Exception ex)
            {

            }            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Asociacion.aspx");
        }

        protected void grvDatosIE_RENTA_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GrvPositivaAccidentes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            using (Alumno_DAO db = new Alumno_DAO())
            {
                try
                {
                    List<error> errores = (List<error>)Session["errores"];
                    string novalidos = "";
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        Image img = (Image)(e.Row.FindControl("imgAdvertencia"));
                        Label lblerror = (Label)(e.Row.FindControl("lblerror"));
                        novalidos = ValidarCaracteresEspeciales(e.Row.Cells[2].Text);
                        img.Visible = true;

                        if (novalidos != "")
                        {
                            img.ToolTip = "Registro contiene caracteres especiales";
                            img.Attributes.Add("data-content", "El caracter invalido en el Apellido Paterno se eliminará:" + novalidos);
                            img.Visible = true;
                            img.ImageUrl = "~/images/AnularAsociacion.png";
                        }
                        else
                        {
                            novalidos = ValidarCaracteresEspeciales(e.Row.Cells[3].Text);
                            if (novalidos != "")
                            {
                                img.ToolTip = "Registro contiene caracteres especiales";
                                img.Attributes.Add("data-content", "El caracter invalido en el Apellido Materno se eliminará:" + novalidos);
                                img.Visible = true;
                                img.ImageUrl = "~/images/AnularAsociacion.png";
                            }
                            else
                            {
                                novalidos = ValidarCaracteresEspeciales(e.Row.Cells[4].Text);
                                if (novalidos != "")
                                {
                                    img.ToolTip = "Registro contiene caracteres especiales";
                                    img.Attributes.Add("data-content", "El caracter invalido en el nombre se eliminará:" + novalidos);
                                    img.Visible = true;
                                    img.ImageUrl = "~/images/AnularAsociacion.png";
                                }
                                else if (e.Row.Cells[7].Text.Trim().Length != 8)
                                {
                                    img.ToolTip = "Registro con errores";
                                    img.Attributes.Add("data-content", "El DNI debe tener 8 digitos");
                                    img.Visible = true;
                                    img.ImageUrl = "~/images/AnularAsociacion.png";
                                    lblerror.Text = "SI";
                                    error er = new error();
                                    er.Item = e.Row.RowIndex + 1;
                                    er.Nombre = e.Row.Cells[2].Text + " " + e.Row.Cells[3].Text + " " + e.Row.Cells[4].Text;
                                    er.TipoDoc = e.Row.Cells[6].Text;
                                    er.NumDoc = e.Row.Cells[7].Text;
                                    er.Error = "El DNI debe tener 8 digitos";
                                    errores.Add(er);
                                }
                                else if (db.BUSCARALUMNO_SEGURO(2, e.Row.Cells[7].Text) > 1)
                                {
                                    img.ToolTip = "Registro con errores";
                                    img.Attributes.Add("data-content", "Tiene seguro vigente");
                                    img.Visible = true;
                                    img.ImageUrl = "~/images/AnularAsociacion.png";
                                    lblerror.Text = "SI";

                                    error er = new error();
                                    er.Item = e.Row.RowIndex + 1;
                                    er.Nombre = e.Row.Cells[2].Text + " " + e.Row.Cells[3].Text + " " + e.Row.Cells[4].Text;
                                    er.TipoDoc = e.Row.Cells[6].Text;
                                    er.NumDoc = e.Row.Cells[7].Text;
                                    er.Error = "Tiene seguro vigente";
                                    errores.Add(er);
                                }

                            }
                        }
                    }
                    Session["errores"] = errores;

                    if (errores.Count > 0)
                    {
                        btnDepurarGenerar.Enabled = false;
                        grvResultados.DataSource = errores;
                        grvResultados.DataBind();

                        string jss2 = "openResultados();";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                    }
                    return;
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected void GrvRimacAccidentes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string novalidos = "";
            List<error> errores = (List<error>)Session["errores"];
            
            using (Alumno_DAO db = new Alumno_DAO())
            {
                try
                {
                    
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        Image img = (Image)(e.Row.FindControl("imgAdvertencia"));
                        Label lblerror = (Label)(e.Row.FindControl("lblerror"));
                        novalidos = ValidarCaracteresEspeciales(e.Row.Cells[5].Text);
                        img.Visible = true;

                        if (novalidos != "")
                        {
                            img.ToolTip = "Registro contiene caracteres especiales";
                            img.Attributes.Add("data-content", "El caracter invalido en el Apellido Paterno se eliminará:" + novalidos);
                            img.Visible = true;
                            img.ImageUrl = "~/images/AnularAsociacion.png";
                        }
                        else
                        {
                            novalidos = ValidarCaracteresEspeciales(e.Row.Cells[6].Text);
                            if (novalidos != "")
                            {
                                img.ToolTip = "Registro contiene caracteres especiales";
                                img.Attributes.Add("data-content", "El caracter invalido en el Apellido Materno se eliminará:" + novalidos);
                                img.Visible = true;
                                img.ImageUrl = "~/images/AnularAsociacion.png";
                            }
                            else
                            {
                                novalidos = ValidarCaracteresEspeciales(e.Row.Cells[7].Text);
                                if (novalidos != "")
                                {
                                    img.ToolTip = "Registro contiene caracteres especiales";
                                    img.Attributes.Add("data-content", "El caracter invalido en el nombre se eliminará:" + novalidos);
                                    img.Visible = true;
                                    img.ImageUrl = "~/images/AnularAsociacion.png";
                                }
                                else if (e.Row.Cells[3].Text != "2" && e.Row.Cells[3].Text != "4" && e.Row.Cells[3].Text != "6" && e.Row.Cells[3].Text != "5")
                                {
                                    img.ToolTip = "Tipo documento no es valido";
                                    img.Attributes.Add("data-content", "No se encontro el tipo de documento:" + novalidos);
                                    img.Visible = true;
                                    img.ImageUrl = "~/images/AnularAsociacion.png";

                                    error er = new error();
                                    er.Item = e.Row.RowIndex+1;
                                    er.Nombre = e.Row.Cells[5].Text + " " + e.Row.Cells[6].Text + " " + e.Row.Cells[7].Text;
                                    er.TipoDoc = e.Row.Cells[3].Text;
                                    er.NumDoc = e.Row.Cells[4].Text;
                                    er.Error = "No se encontro el tipo de documento:";
                                    errores.Add(er);

                                }
                                else if (e.Row.Cells[4].Text.Length != 8 && e.Row.Cells[3].Text == "2")
                                {
                                    img.ToolTip = "Registro con errores";
                                    img.Attributes.Add("data-content", "El DNI debe tener 8 digitos");
                                    img.Visible = true;
                                    img.ImageUrl = "~/images/AnularAsociacion.png";

                                    error er = new error();
                                    er.Item = e.Row.RowIndex+1;
                                    er.Nombre = e.Row.Cells[5].Text + " " + e.Row.Cells[6].Text + " " + e.Row.Cells[7].Text;
                                    er.TipoDoc = e.Row.Cells[3].Text;
                                    er.NumDoc = e.Row.Cells[4].Text;
                                    er.Error = "El DNI debe tener 8 digitos";
                                    errores.Add(er);
                                }

                                else if (db.BUSCARALUMNO_SEGURO(Convert.ToInt32(e.Row.Cells[3].Text), e.Row.Cells[4].Text) >0)
                                {
                                    img.ToolTip = "Registro con errores";
                                    img.Attributes.Add("data-content", "Tiene seguro vigente");
                                    img.Visible = true;
                                    img.ImageUrl = "~/images/AnularAsociacion.png";
                                    lblerror.Text = "SI";

                                    error er = new error();
                                    er.Item = e.Row.RowIndex+1;
                                    er.Nombre = e.Row.Cells[5].Text + " " + e.Row.Cells[6].Text + " " + e.Row.Cells[7].Text;
                                    er.TipoDoc = e.Row.Cells[3].Text;
                                    er.NumDoc = e.Row.Cells[4].Text;
                                    er.Error = "Tiene seguro vigente";
                                    errores.Add(er);
                                }                                
                            }
                        }                    
                    }

                    Session["errores"] = errores;

                    if (errores.Count > 0)
                    {
                        btnDepurarGenerar.Enabled = false;
                        grvResultados.DataSource = errores;
                        grvResultados.DataBind();

                        string jss2 = "openResultados();";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                    }
                    return;
                }

                catch (Exception ex)
                {

                }
            }
        }

        protected void grvPacificoAccidentes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string novalidos = "";
            List<error> errores = (List<error>)Session["errores"];
            using (Alumno_DAO db = new Alumno_DAO())
            {
                try
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        Image img = (Image)(e.Row.FindControl("imgAdvertencia"));
                        Label lblerror = (Label)(e.Row.FindControl("lblerror"));
                        novalidos = ValidarCaracteresEspeciales(e.Row.Cells[1].Text);
                        img.Visible = true;

                        if (novalidos != "")
                        {
                            img.ToolTip = "Registro contiene caracteres especiales";
                            img.Attributes.Add("data-content", "El caracter invalido en el Apellido Paterno se eliminará:" + novalidos);
                            img.Visible = true;
                            img.ImageUrl = "~/images/AnularAsociacion.png";
                        }
                        else
                        {
                            novalidos = ValidarCaracteresEspeciales(e.Row.Cells[2].Text);
                            if (novalidos != "")
                            {
                                img.ToolTip = "Registro contiene caracteres especiales";
                                img.Attributes.Add("data-content", "El caracter invalido en el Apellido Materno se eliminará:" + novalidos);
                                img.Visible = true;
                                img.ImageUrl = "~/images/AnularAsociacion.png";
                            }
                            else
                            {
                                novalidos = ValidarCaracteresEspeciales(e.Row.Cells[3].Text);
                                if (novalidos != "")
                                {
                                    img.ToolTip = "Registro contiene caracteres especiales";
                                    img.Attributes.Add("data-content", "El caracter invalido en el nombre se eliminará:" + novalidos);
                                    img.Visible = true;
                                    img.ImageUrl = "~/images/AnularAsociacion.png";
                                }
                                //else if (e.Row.Cells[7].Text.Length != 8)
                                //{
                                //    img.ToolTip = "Registro con errores";
                                //    img.Attributes.Add("data-content", "El DNI debe tener 8 digitos");
                                //    img.Visible = true;
                                //    img.ImageUrl = "~/images/AnularAsociacion.png";
                                //    lblerror.Text = "SI";
                                //    error er = new error();
                                //    er.Item = e.Row.RowIndex;
                                //    er.Nombre = e.Row.Cells[1].Text + " " + e.Row.Cells[2].Text + " " + e.Row.Cells[3].Text;
                                //    er.TipoDoc = e.Row.Cells[6].Text;
                                //    er.NumDoc = e.Row.Cells[7].Text;
                                //    er.Error = "El DNI debe tener 8 digitos";
                                //    errores.Add(er);
                                //}
                                else if (db.BUSCARALUMNO_SEGURO(2, e.Row.Cells[7].Text) > 1)
                                {
                                    img.ToolTip = "Registro con errores";
                                    img.Attributes.Add("data-content", "Tiene seguro vigente");
                                    img.Visible = true;
                                    img.ImageUrl = "~/images/AnularAsociacion.png";
                                    lblerror.Text = "SI";
                                    error er = new error();
                                    er.Item = e.Row.RowIndex;
                                    er.Nombre = e.Row.Cells[1].Text + " " + e.Row.Cells[2].Text + " " + e.Row.Cells[3].Text;
                                    er.TipoDoc = e.Row.Cells[6].Text;
                                    er.NumDoc = e.Row.Cells[7].Text;
                                    er.Error = "Tiene seguro Vigente";
                                    errores.Add(er);
                                }
                            }
                        }
                    }

                    Session["errores"] = errores;
                    if (errores.Count > 0)
                    {
                        btnDepurarGenerar.Enabled = false;
                        grvResultados.DataSource = errores;
                        grvResultados.DataBind();

                        string jss2 = "openResultados();";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                    }
                    return;

                }
                catch (Exception ex)
                {
                }
            }
        }

        protected void grvDatosIE_RENTA_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            string novalidos = "";
             List<error>  errores = (List<error>) Session["errores"];

            using (Alumno_DAO db = new Alumno_DAO())
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Image img = (Image)(e.Row.FindControl("imgAdvertencia"));
                    Label lblerror = (Label)(e.Row.FindControl("lblerror"));
                    img.Visible = true;
                    novalidos = ValidarCaracteresEspeciales(e.Row.Cells[4].Text);
                    if (novalidos != "")
                    {
                        img.ToolTip = "Registro contiene caracteres especiales";
                        img.Attributes.Add("data-content", "El caracter invalido en el Apellido Paterno del asegurado se eliminará:" + novalidos);
                        img.Visible = true;
                        img.ImageUrl = "~/images/AnularAsociacion.png";
                    }
                    else
                    {
                        novalidos = ValidarCaracteresEspeciales(e.Row.Cells[5].Text);
                        if (novalidos != "")
                        {
                            img.ToolTip = "Registro contiene caracteres especiales";
                            img.Attributes.Add("data-content", "El caracter invalido en el Apellido Materno del asegurado se eliminará:" + novalidos);
                            img.Visible = true;
                            img.ImageUrl = "~/images/AnularAsociacion.png";
                        }
                        else
                        {
                            novalidos = ValidarCaracteresEspeciales(e.Row.Cells[6].Text);
                            if (novalidos != "")
                            {
                                img.ToolTip = "Registro contiene caracteres especiales";
                                img.Attributes.Add("data-content", "El caracter invalido en el nombre del asegurado se eliminará:" + novalidos);
                                img.Visible = true;
                                img.ImageUrl = "~/images/AnularAsociacion.png";
                            }
                            else if (Convert.ToInt32(e.Row.Cells[2].Text) != 1 && Convert.ToInt32(e.Row.Cells[2].Text) != 4 && Convert.ToInt32(e.Row.Cells[2].Text) !=6)
                            {
                                img.ToolTip = "Registro con errores";
                                img.Attributes.Add("data-content", "Error en el tipo de documento del asegurado");
                                img.Visible = true;
                                img.ImageUrl = "~/images/AnularAsociacion.png";
                                lblerror.Text = "SI";

                                error er = new error();
                                er.Item = e.Row.RowIndex;
                                er.Nombre = e.Row.Cells[4].Text + " " + e.Row.Cells[5].Text + " " + e.Row.Cells[6].Text;
                                er.TipoDoc = e.Row.Cells[2].Text;
                                er.NumDoc = e.Row.Cells[3].Text;
                                er.Error = "Error en el tipo de documento del Asegurado";
                                errores.Add(er);

                            }

                          else if ( IsDate( e.Row.Cells[7].Text)==false)
                            {
                                img.ToolTip = "Registro con errores";
                                img.Attributes.Add("data-content", "Fecha de nacimiento del asegurado no  es valida");
                                img.Visible = true;
                                img.ImageUrl = "~/images/AnularAsociacion.png";
                                lblerror.Text = "SI";

                                error er = new error();
                                er.Item = e.Row.RowIndex;
                                er.Nombre = e.Row.Cells[4].Text + " " + e.Row.Cells[5].Text + " " + e.Row.Cells[6].Text;
                                er.TipoDoc = e.Row.Cells[2].Text;
                                er.NumDoc = e.Row.Cells[3].Text;
                                er.Error = "Error Fecha de nacimiento no valida";
                                errores.Add(er);

                            }

                            else if (e.Row.Cells[3].Text.Length != 8 && e.Row.Cells[2].Text == "1" )
                            {
                                img.ToolTip = "Registro con errores";
                                img.Attributes.Add("data-content", "El número de DNI del asegurado debe tener 8 digitos");
                                img.Visible = true;
                                img.ImageUrl = "~/images/AnularAsociacion.png";
                                lblerror.Text = "SI";
                                error er = new error();
                                er.Item = e.Row.RowIndex;
                                er.Nombre = e.Row.Cells[4].Text + " " + e.Row.Cells[5].Text + " " + e.Row.Cells[6].Text;
                                er.TipoDoc = e.Row.Cells[5].Text;
                                er.NumDoc = e.Row.Cells[6].Text;
                                er.Error = "Error en el tipo de documento del beneficiario";
                                errores.Add(er);
                            }
                            else
                            {
                                novalidos = ValidarCaracteresEspeciales(e.Row.Cells[12].Text);
                                if (novalidos != "")
                                {
                                    img.ToolTip = "Registro contiene caracteres especiales";
                                    img.Attributes.Add("data-content", "El carácter invalido en el Apellido Paterno del beneficiario se eliminará:" + novalidos);
                                    img.Visible = true;
                                    img.ImageUrl = "~/images/AnularAsociacion.png";

                                    /*error er = new error();
                                    er.Item = e.Row.RowIndex;
                                    er.Nombre = e.Row.Cells[12].Text + " " + e.Row.Cells[13].Text + " " + e.Row.Cells[14].Text;
                                    er.TipoDoc = e.Row.Cells[10].Text;
                                    er.NumDoc = e.Row.Cells[11].Text;
                                    er.Error = "Error en el Apellido Paterno del Beneficiario";
                                    errores.Add(er);*/
                                }
                                else
                                {
                                    novalidos = ValidarCaracteresEspeciales(e.Row.Cells[13].Text);
                                    if (novalidos != "")
                                    {
                                        img.ToolTip = "Registro contiene caracteres especiales";
                                        img.Attributes.Add("data-content", "El carácter invalido en el Apellido Materno del beneficiario  se eliminará:" + novalidos);
                                        img.Visible = true;
                                        img.ImageUrl = "~/images/AnularAsociacion.png";

                                        /*error er = new error();
                                        er.Item = e.Row.RowIndex;
                                        er.Nombre = e.Row.Cells[12].Text + " " + e.Row.Cells[13].Text + " " + e.Row.Cells[14].Text;
                                        er.TipoDoc = e.Row.Cells[10].Text;
                                        er.NumDoc = e.Row.Cells[11].Text;
                                        er.Error = "Error en el Apellido Materno del Beneficiario";
                                        errores.Add(er);*/
                                    }
                                    else
                                    {
                                        novalidos = ValidarCaracteresEspeciales(e.Row.Cells[14].Text);
                                        if (novalidos != "")
                                        {
                                            img.ToolTip = "Registro contiene caracteres especiales";
                                            img.Attributes.Add("data-content", "El carácter invalido en el nombre del beneficiario se eliminará:" + novalidos);
                                            img.Visible = true;
                                            img.ImageUrl = "~/images/AnularAsociacion.png";

                                            /*error er = new error();
                                            er.Item = e.Row.RowIndex;
                                            er.Nombre = e.Row.Cells[12].Text + " " + e.Row.Cells[13].Text + " " + e.Row.Cells[14].Text;
                                            er.TipoDoc = e.Row.Cells[10].Text;
                                            er.NumDoc = e.Row.Cells[11].Text;
                                            er.Error = "Error en el Nombre del Beneficiario";
                                            errores.Add(er);*/
                                        }
                                        else if (Convert.ToInt32(e.Row.Cells[10].Text) != 1 && Convert.ToInt32(e.Row.Cells[10].Text) != 4 && Convert.ToInt32(e.Row.Cells[10].Text) != 6)
                                        {
                                            img.ToolTip = "Registro con errores";
                                            img.Attributes.Add("data-content", "Error en el tipo de documento del beneficiario");
                                            img.Visible = true;
                                            img.ImageUrl = "~/images/AnularAsociacion.png";
                                            lblerror.Text = "SI";

                                            error er = new error();
                                            er.Item = e.Row.RowIndex;
                                            er.Nombre = e.Row.Cells[12].Text + " " + e.Row.Cells[13].Text + " " + e.Row.Cells[14].Text; 
                                            er.TipoDoc = e.Row.Cells[10].Text;
                                            er.NumDoc = e.Row.Cells[11].Text;
                                            er.Error = "Error en el tipo de documento del beneficiario";
                                            errores.Add(er);

                                        }
                                        else if (e.Row.Cells[10].Text == "1" && e.Row.Cells[11].Text.Length != 8)
                                        {
                                            img.ToolTip = "Registro con errores";
                                            img.Attributes.Add("data-content", "El DNI del beneficiario debe tener 8 digitos");
                                            img.Visible = true;
                                            img.ImageUrl = "~/images/AnularAsociacion.png";
                                            lblerror.Text = "SI";

                                            error er = new error();
                                            er.Item = e.Row.RowIndex;
                                            er.Nombre = e.Row.Cells[12].Text + " " + e.Row.Cells[13].Text + " " + e.Row.Cells[14].Text;
                                            er.TipoDoc = e.Row.Cells[10].Text;
                                            er.NumDoc = e.Row.Cells[11].Text;
                                            er.Error = "El DNI del beneficiario debe tener 8 digitos";
                                            errores.Add(er);
                                        }
                                        else if (db.BUSCARALUMNO_PADRE(Convert.ToInt32(e.Row.Cells[2].Text), e.Row.Cells[3].Text, Convert.ToInt32(e.Row.Cells[10].Text), e.Row.Cells[11].Text, Convert.ToInt32(hdnAsociacionID.Value)) > 0)
                                        {
                                            img.ToolTip = "Registro con errores";
                                            img.Attributes.Add("data-content", "Tiene seguro vigente");
                                            img.Visible = true;
                                            img.ImageUrl = "~/images/AnularAsociacion.png";
                                            lblerror.Text = "SI";

                                            error er = new error();
                                            er.Item = e.Row.RowIndex;
                                            er.Nombre = e.Row.Cells[4].Text + " " + e.Row.Cells[5].Text + " " + e.Row.Cells[6].Text;
                                            er.TipoDoc = e.Row.Cells[2].Text;
                                            er.NumDoc = e.Row.Cells[3].Text;
                                            er.Error = "Tiene seguro vigente";
                                            errores.Add(er);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                Session["errores"] = errores;
                if (errores.Count > 0)
                {
                    btnDepurarGenerar.Enabled = false;
                    grvResultados.DataSource = errores;
                    grvResultados.DataBind();

                    string jss2 = "openResultados();";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);               
                }
                return;
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            using (Alumno_DAO db = new Alumno_DAO())
            {
                if (hdnEliminaOpcion.Value == "A")
                {
                    string Codigo = Convert.ToString(grvAsegurados.Rows[Convert.ToInt32(hdnAlumnoSelect.Value)].Cells[0].Text);
                    int CodigoDetalleID = Convert.ToInt32(grvAsegurados.Rows[Convert.ToInt32(hdnAlumnoSelect.Value)].Cells[15].Text);
                    db.AnularAsegurado(Codigo, CodigoDetalleID, false);
                }

                if (hdnEliminaOpcion.Value == "E")
                {
                    string Codigo = Convert.ToString(grvAsegurados.Rows[Convert.ToInt32(hdnAlumnoSelect.Value)].Cells[0].Text);
                    int CodigoDetalleID = Convert.ToInt32(grvAsegurados.Rows[Convert.ToInt32(hdnAlumnoSelect.Value)].Cells[15].Text);
                    db.EliminarCodigoAsegurado(CodigoDetalleID, hdnAfiliacionSeguroID.Value);
                }

                cargarAsegurados(txtApeNombresMn1.Text);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "ActivaTab();", true);
            }
        }

        protected void grvResultados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType==DataControlRowType.DataRow)
            {               
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
            }
        }
        public bool IsDate(object Expression)
        {
            try
            {
                  Convert.ToDateTime(Expression);
                  return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        protected void hdnCantidad_ValueChanged(object sender, EventArgs e)
        {

        }

        protected void GrvPositivaAccidentes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtIE_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnCancelarDepurar_Click(object sender, EventArgs e)
        {

            DataTable dt = cargarAseguradosExportar();
            /// DataSet dt = dbAlumnoExp.GEN_EXPORTARCODIGOS_ACCIDENTES(codGenerado, GrvPositivaAccidentes.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                //  ExportarCodigosAcccidentes(dt.Tables[0]);
                StringBuilder sb = new StringBuilder();

                sb.Append("<table cellpadding='0' cellspacing='0' border='1'>");
                sb.Append("<tr style='color:White; font-weight:bold; font-family:Arial; font-size:10px;'>");
                sb.Append("<td style='background-color:#FF0000;'>Codigo</td>");
                sb.Append("<td style='background-color:#FF0000;'>Apellido Paterno</td>");
                sb.Append("<td style='background-color:#FF0000;'>Apellido Materno</td>");
                sb.Append("<td style='background-color:#FF0000;'>Nombres</td>");
                sb.Append("<td style='background-color:#FF0000;'>Fecha Nacimiento</td>");
                sb.Append("<td style='background-color:#FF0000;'>Tipo Documento</td>");
                sb.Append("<td style='background-color:#FF0000;'>Nro.Documento</td>");
                sb.Append("<td style='background-color:#FF0000;'>Beneficiario</td>");
                sb.Append("<td style='background-color:#FF0000;'>Situación</td>");
                //sb.Append("<td style='background-color:#FF0000;'>Prima</td>");
                //sb.Append("<td style='background-color:#FF0000;'>Codigo de Pago</td>");
                //sb.Append("<td style='background-color:#FF0000;'>TipoDoc</td>");
                //sb.Append("<td style='background-color:#FF0000;'>NumeroDocumento</td>");
                //sb.Append("<td style='background-color:#FF0000;'>Apellido Paterno</td>");
                //sb.Append("<td style='background-color:#FF0000;'>Apelllido Materno</td>");
                //sb.Append("<td style='background-color:#FF0000;'>Nombre</td>");
                //sb.Append("<td style='background-color:#FF0000;'>FechaNacimiento</td>");
                //sb.Append("<td style='background-color:#FF0000;'>Grado</td>");
                //sb.Append("<td style='background-color:#FF0000;'>Seccion</td>");
                sb.Append("</tr>");
                foreach (DataRow item in dt.Rows)
                {
                    sb.Append("<tr style='color:Black; font-family:Arial; font-size:10px;'>");
                    sb.Append("<td  style='mso-number-format:\\@' >" + item[1].ToString() + "</td>");
                    sb.Append("<td  style='mso-number-format:\\@' >" + item[4].ToString() + "</td>");
                    sb.Append("<td  style='mso-number-format:\\@' >" + item[5].ToString() + "</td>");
                    sb.Append("<td  style='mso-number-format:\\@' >" + item[6].ToString() + "</td>");
                    sb.Append(@"<td style='mso-number-format:dd\-mm\-yyyy'>" + item[7].ToString());
                    sb.Append("<td  style='mso-number-format:\\@' >" + item[8].ToString() + "</td>");                 
                    sb.Append("<td  style='mso-number-format:\\@' >" + item[9].ToString() + "</td>");
                    sb.Append("<td  style='mso-number-format:\\@' >" + item[2].ToString() + "</td>");
                    sb.Append("<td  style='mso-number-format:\\@' >" + item[3].ToString() + "</td>");
                    //sb.Append(String.Format("<td align='center'>{0}</td>", item[10].ToString()));
                    //sb.Append("<td  style='mso-number-format:\\@' >" + item[11].ToString() + "</td>");
                    //sb.Append("<td  style='mso-number-format:\\@' >" + item[12].ToString() + "</td>");
                    //sb.Append("<td  style='mso-number-format:\\@' >" + item[13].ToString() + "</td>");
                    //sb.Append("<td  style='mso-number-format:\\@' >" + item[14].ToString() + "</td>");
                    //sb.Append("<td  style='mso-number-format:\\@' >" + item[15].ToString() + "</td>");
                    //sb.Append("<td  style='mso-number-format:\\@' >" + item[16].ToString() + "</td>");
                    //sb.Append("<td  style='mso-number-format:\\@' >" + item[17].ToString() + "</td>");
                    //sb.Append("<td  style='mso-number-format:\\@' >" + item[18].ToString() + "</td>");
                    //sb.Append("<td  style='mso-number-format:\\@' >" + item[19].ToString() + "</td>");
                    //sb.Append("<td  style='mso-number-format:\\@' >" + item[20].ToString() + "</td>");
                    //sb.Append("</tr>");
                }
                string file = txtIE.Text +"_"+ hdnCiaSeguro.Value.PadLeft(2, '0') +   "_" + hdnProductoID.Value.PadLeft(2,'0') + DateTime.Now.ToString("ddMMyyyy") + ".xls"; 
                sb.Append("</table>");
                Response.Clear();
                Response.Buffer = true;
                Response.Write(sb.ToString());
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;Filename=" +"Asegurados_" + file);
                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
                HttpContext.Current.Response.End();
            }
            }

       
    }
}
public class error
    {
      public int Item { get; set; }
     public string Nombre { get; set; }
     public string TipoDoc { get; set; }
    public string NumDoc { get; set; }
    public string Error{ get; set; }

}








