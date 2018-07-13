using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using DAO_Hermes;
using DAO_Hermes.Repositorios;
using System.Net;
using System.Data;
using System.Net.Mail;
using System.IO;
using System;
using System.Text;
using System.Drawing;
using System.Data.OleDb;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using DAO_Hermes.ViewModel;

namespace Demo
{
    public static class cUtil
    {

        public static void ListarTrama(DropDownList cbo)
        {
            using (ConfiguracionArchivo_DAO db = new ConfiguracionArchivo_DAO())
            {
                cbo.DataSource = db.Listar();
                cbo.DataTextField = "Nombre";
                cbo.DataValueField = "ID";
                cbo.DataBind();

            }
        }
        public static void ListarTipoSeguro(DropDownList cbo)
        {
            using (TipoSeguro_DAO db = new TipoSeguro_DAO())
            {
                cbo.DataSource = db.Listar();
                cbo.DataTextField = "Nombre";
                cbo.DataValueField = "ID";
                cbo.DataBind();
            }
        }
        public static void ListarTipoDocumentos(DropDownList cbo)
        {
            cbo.Items.Clear();
            using (TipoDocumento_DAO db = new TipoDocumento_DAO())
            {
                cbo.DataSource = db.Listar();
                cbo.DataTextField = "Nombre";
                cbo.DataValueField = "ID";
                cbo.DataBind();
            }
            ListItem lstD = new ListItem("Seleccione Tipo de Documento", "0");
            lstD.Selected = true;
            cbo.Items.Add(lstD);
        }

        public static void ListarTipoDocumentosTodos(DropDownList cbo)
        {
            cbo.Items.Clear();
            using (TipoDocumento_DAO db = new TipoDocumento_DAO())
            {
                cbo.DataSource = db.ListarTodos();
                cbo.DataTextField = "Nombre";
                cbo.DataValueField = "ID";
                cbo.DataBind();
            }
            ListItem lstD = new ListItem("Seleccione Tipo de Documento", "0");
            lstD.Selected = true;
            cbo.Items.Add(lstD);
        }

        public static void ListarTipoDocumentoAccidente(DropDownList cbo)
        {
            cbo.Items.Clear();
            using (TipoDocumento_DAO db = new TipoDocumento_DAO())
            {
                cbo.DataSource = db.ListarDocAccidente();
                cbo.DataTextField = "Nombre";
                cbo.DataValueField = "Codigo";
                cbo.DataBind();
            }
            ListItem lstD = new ListItem("Seleccione Tipo de Documento", "0");
            lstD.Selected = true;
            cbo.Items.Add(lstD);
        }
        public static void ListarTipoDocumentoAccidenteTodos(DropDownList cbo)
        {
            cbo.Items.Clear();
            using (TipoDocumento_DAO db = new TipoDocumento_DAO())
            {
                cbo.DataSource = db.ListarDocAccidenteTodos();
                cbo.DataTextField = "Nombre";
                cbo.DataValueField = "Codigo";
                cbo.DataBind();
            }
            ListItem lstD = new ListItem("Seleccione Tipo de Documento", "0");
            lstD.Selected = true;
            cbo.Items.Add(lstD);
        }
        public static void ListarTipoDocumentoRentas(DropDownList cbo)
        {
            cbo.Items.Clear();
            using (TipoDocumento_DAO db = new TipoDocumento_DAO())
            {
                cbo.DataSource = db.ListarDocRenta();
                cbo.DataTextField = "Nombre";
                cbo.DataValueField = "CodigoRenta";
                cbo.DataBind();
            }
            ListItem lstD = new ListItem("Seleccione Tipo de Documento", "0");
            lstD.Selected = true;
            cbo.Items.Add(lstD);

        }

        public static void ListarTipoDocumentoRentasTodos(DropDownList cbo)
        {
            cbo.Items.Clear();
            using (TipoDocumento_DAO db = new TipoDocumento_DAO())
            {
                cbo.DataSource = db.ListarDocRentaTodos();
                cbo.DataTextField = "Nombre";
                cbo.DataValueField = "CodigoRenta";
                cbo.DataBind();
            }
            ListItem lstD = new ListItem("Seleccione Tipo de Documento", "0");
            lstD.Selected = true;
            cbo.Items.Add(lstD);

        }

        public static void ListarRol(DropDownList cbo)
        {
            using (TipoDocumento_DAO db = new TipoDocumento_DAO())
            {
                cbo.DataSource = db.ListarRol();
                cbo.DataTextField = "Id";
                cbo.DataValueField = "Nombre";
                cbo.DataBind();
            }
        }

        public static void ListarMonedas(DropDownList cbo)
        {
            using (MonedaDAO db = new MonedaDAO())
            {
                cbo.DataSource = db.Listar();
                cbo.DataTextField = "Nombre";
                cbo.DataValueField = "id";
                cbo.DataBind();
            }
        }
        public static void ListarCiaSeguros(DropDownList cbo)
        {
            using (CiaSeguro_DAO db = new CiaSeguro_DAO())
            {
                cbo.DataSource = db.ListarSeguros();
                cbo.DataTextField = "Nombre";
                cbo.DataValueField = "id";
                cbo.DataBind();
            }
        }

        public static void ListarTipoInstitucion(DropDownList cbo)
        {
            using (TipoInstitucionEducativaDAO db = new TipoInstitucionEducativaDAO())
            {
                cbo.DataSource = db.Listar();
                cbo.DataTextField = "Nombre";
                cbo.DataValueField = "Id";
                cbo.DataBind();
            }
        }

        public static void ListarBancos(DropDownList cbo)
        {
            using (BancoDAO db = new BancoDAO())
            {
                cbo.DataSource = db.ListarBancos();
                cbo.DataTextField = "Nombre";
                cbo.DataValueField = "ID";
                cbo.DataBind();
            }
        }


        public static void ListarUbigeo(DropDownList cbo, string dp, string pv, string ds)
        {
            using (UbigeoDAO db = new UbigeoDAO())
            {
                cbo.DataSource = db.ListarUbigeo(dp, pv, ds);
                cbo.DataTextField = "Nombre";
                cbo.DataValueField = "CodigoUbigeo";
                cbo.DataBind();
            }
        }


        public static string LeerTemplateHTML(string url)
        {
            using (StreamReader lector = new StreamReader(url, System.Text.Encoding.UTF7))
            {
                return lector.ReadToEnd();
            }
        }

        public static void EscribirArchivo(string url, string texto)
        {
            //using (StreamWriter lector = new StreamWriter(url, false))
            using (StreamWriter lector = new StreamWriter(url, false, Encoding.ASCII))
            {                
                lector.Write(texto);
                lector.Close();
            }
        }


        public static void EnvioMail(string Para, string De, string Asunto, string Mensaje, List<string> adjunto, bool EsHTML, string Clave, string SMTPServer, Int32 Puerto)
        {
            MailMessage correo = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            correo.From = new MailAddress(De, "Hermes Seguros", System.Text.Encoding.UTF8);
            correo.To.Add(Para);
            correo.SubjectEncoding = System.Text.Encoding.UTF8;
            correo.Subject = Asunto;
            correo.Body = Mensaje;
            correo.BodyEncoding = System.Text.Encoding.UTF8;
            correo.IsBodyHtml = EsHTML;
            correo.Priority = MailPriority.High;

            smtp.Credentials = new System.Net.NetworkCredential(De, Clave);
            smtp.Port = Puerto;
            smtp.Host = SMTPServer;
            smtp.EnableSsl = true;

            ServicePointManager.ServerCertificateValidationCallback =
               delegate (object s
                   , X509Certificate certificate
                   , X509Chain chai
                   , SslPolicyErrors sslPolicyErrors)
               { return true; };

            smtp.Send(correo);
        }

        public static void EnvioMail(string Para, string De, string Asunto, string Mensaje, bool EsHTML, string Clave, string SMTPServer, Int32 Puerto, string adjunto)
        {
            MailMessage correo = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            correo.From = new MailAddress(De, "Hermes Seguros", System.Text.Encoding.UTF8);
            correo.To.Add(Para);
            correo.SubjectEncoding = System.Text.Encoding.UTF8;
            correo.Subject = Asunto;
            correo.Body = Mensaje;
            correo.BodyEncoding = System.Text.Encoding.UTF8;
            correo.IsBodyHtml = EsHTML;
            correo.Priority = MailPriority.High;
            correo.Attachments.Add(new Attachment(adjunto));
            smtp.Credentials = new System.Net.NetworkCredential(De, Clave);
            smtp.Port = Puerto;
            smtp.Host = SMTPServer;
            smtp.EnableSsl = true;

            ServicePointManager.ServerCertificateValidationCallback =
               delegate (object s
                   , X509Certificate certificate
                   , X509Chain chai
                   , SslPolicyErrors sslPolicyErrors)
               { return true; };
            smtp.Send(correo);
        }

        public static void EnvioMailSegundo(
                        string asunto, 
                        string para, 
                        string body,
                        List<string> listrutas, 
                        string De, 
                        string Clave, 
                        string SMTPServer, 
                        int Puerto)
        {
            MailMessage correo = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            correo.From = new MailAddress(De, "Hermes", System.Text.Encoding.UTF8);
            correo.To.Add(para);
            correo.SubjectEncoding = System.Text.Encoding.UTF8;
            correo.Subject = asunto;
            correo.Body = body;
            correo.BodyEncoding = System.Text.Encoding.UTF8;
           correo.IsBodyHtml = true;
            correo.Priority = MailPriority.High;
            foreach (var item in listrutas)
            {
                correo.Attachments.Add(new Attachment(item));
            }
            smtp.Credentials = new System.Net.NetworkCredential(De, Clave);
            smtp.Port = Puerto;
            smtp.Host = SMTPServer;
            smtp.EnableSsl = true;


            ServicePointManager.ServerCertificateValidationCallback =
               delegate (object s
                   , X509Certificate certificate
                   , X509Chain chai
                   , SslPolicyErrors sslPolicyErrors)
               { return true; };
            smtp.Send(correo);
        }


        public static void EnvioMails(string Para, string De, string Asunto, string Mensaje, bool EsHTML, string Clave, string SMTPServer, Int32 Puerto, List<string> adjuntos)
        {
            MailMessage correo = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            correo.From = new MailAddress(De, "Hermes Seguros", System.Text.Encoding.UTF8);
            correo.To.Add(Para);
            correo.SubjectEncoding = System.Text.Encoding.UTF8;
            correo.Subject = Asunto;
            correo.Body = Mensaje;
            correo.BodyEncoding = System.Text.Encoding.UTF8;
            correo.IsBodyHtml = EsHTML;
            correo.Priority = MailPriority.High;


            
            foreach (var item in adjuntos)
            {
                correo.Attachments.Add(new Attachment(item));
            }


            smtp.Credentials = new System.Net.NetworkCredential(De, Clave);
            smtp.Port = Puerto;
            smtp.Host = SMTPServer;
            smtp.EnableSsl = true;


            ServicePointManager.ServerCertificateValidationCallback =
               delegate (object s
                   , X509Certificate certificate
                   , X509Chain chai
                   , SslPolicyErrors sslPolicyErrors)
               { return true; };
            smtp.Send(correo);
        }

        public static void EnvioMails(string Para, string Cc, string De, string Asunto, string Mensaje, bool EsHTML, string Clave, string SMTPServer, Int32 Puerto, List<MemoryStream> adjuntos, List<string> Nameadjuntos)
        {
            MailMessage correo = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            correo.From = new MailAddress(De, "Hermes Seguros", System.Text.Encoding.UTF8);

            string[] sTos = Para.Split(';');
            foreach (string sTo in sTos)
            {
                if (sTo.Length > 0) {
                    correo.To.Add(sTo);
                }
            }            

            correo.CC.Add(Cc);
            correo.SubjectEncoding = System.Text.Encoding.UTF8;
            correo.Subject = Asunto;
            correo.Body = Mensaje;
            correo.BodyEncoding = System.Text.Encoding.UTF8;
            correo.IsBodyHtml = EsHTML;
            correo.Priority = MailPriority.High;


            int x = 0;
            foreach (var item in adjuntos)
            {
                //MemoryStream ms = File.getFileS .GetFileStream(item);
                correo.Attachments.Add(new Attachment(item, new FileInfo(Nameadjuntos[x]).Name));
                x = x + 1;
            }


            smtp.Credentials = new System.Net.NetworkCredential(De, Clave);
            smtp.Port = Puerto;
            smtp.Host = SMTPServer;
            smtp.EnableSsl = true;


            ServicePointManager.ServerCertificateValidationCallback =
               delegate (object s
                   , X509Certificate certificate
                   , X509Chain chai
                   , SslPolicyErrors sslPolicyErrors)
               { return true; };
            smtp.Send(correo);
        }


        public static string ObtenerValorParametro(string Categoria, string Nombre)
        {
            using (UtilDAO oUtil = new UtilDAO())
            {
                return oUtil.ObtenerValorParametro(Categoria, Nombre);
            }
        }

        public static string ObtenerValorParametroDes(string Categoria, string Nombre, string Descripcion)
        {
            using (UtilDAO oUtil = new UtilDAO())
            {
                return oUtil.ObtenerValorParametroDes(Categoria, Nombre, Descripcion);
            }
        }
        public static DataTable CrearDetalleAfiliacion()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", Type.GetType("System.Int32"));
            dt.Columns.Add("Asegurado", Type.GetType("System.String"));
            dt.Columns.Add("Beneficiario", Type.GetType("System.String"));
            dt.Columns.Add("NroDoc", Type.GetType("System.String"));
            dt.Columns.Add("FecNac", Type.GetType("System.String"));
            dt.Columns.Add("Seguro", Type.GetType("System.String"));
            dt.Columns.Add("TipoMoneda", Type.GetType("System.String"));
            dt.Columns.Add("Prima", Type.GetType("System.String"));
            dt.Columns.Add("CiaSeguros", Type.GetType("System.String"));
            dt.Columns.Add("Plan", Type.GetType("System.String"));
            dt.Columns.Add("Acepto", Type.GetType("System.Boolean"));
            dt.Columns.Add("Producto", Type.GetType("System.String"));
            dt.Columns.Add("IdAsegurado", Type.GetType("System.String"));
            dt.Columns.Add("IdMoneda", Type.GetType("System.Int32"));
            dt.Columns.Add("NombreColegio", Type.GetType("System.String"));
            dt.Columns.Add("IdProducto", Type.GetType("System.Int32"));
            dt.Columns.Add("IdAsociacion", Type.GetType("System.Int32"));
            //dt.Columns.Add("FilePlanSeguro", typeof(Byte[]));
            //dt.Columns.Add("FilePlanSeguro", Type.GetType("System.byte[]"));
            dt.Columns.Add("FileNamePlanSeguro", Type.GetType("System.String"));
            dt.Columns.Add("IdPadre", Type.GetType("System.Int32"));
            dt.Columns.Add("IdHijo", Type.GetType("System.Int32"));

            return dt;
        }

        public static DataTable CrearDetalleAfiliacionRenta2()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IdBeneficiario", Type.GetType("System.String"));
            dt.Columns.Add("Asegurado", Type.GetType("System.String"));
            dt.Columns.Add("Beneficiario", Type.GetType("System.String"));
            dt.Columns.Add("NroDoc", Type.GetType("System.String"));
            dt.Columns.Add("FecNac", Type.GetType("System.String"));
            dt.Columns.Add("Seguro", Type.GetType("System.String"));
            dt.Columns.Add("TipoMoneda", Type.GetType("System.String"));
            dt.Columns.Add("Prima", Type.GetType("System.String"));
            dt.Columns.Add("CiaSeguros", Type.GetType("System.String"));
            dt.Columns.Add("Plan", Type.GetType("System.String"));
            dt.Columns.Add("Acepto", Type.GetType("System.Boolean"));
            dt.Columns.Add("Producto", Type.GetType("System.String"));
            dt.Columns.Add("IdAsegurado", Type.GetType("System.String"));
            dt.Columns.Add("IdMoneda", Type.GetType("System.Int32"));
            dt.Columns.Add("NombreColegio", Type.GetType("System.String"));
            dt.Columns.Add("IdProducto", Type.GetType("System.Int32"));
            dt.Columns.Add("IdAsociacion", Type.GetType("System.Int32"));
            //dt.Columns.Add("FilePlanSeguro", typeof(Byte[]));
            //dt.Columns.Add("FilePlanSeguro", Type.GetType("System.byte[]"));
            dt.Columns.Add("FileNamePlanSeguro", Type.GetType("System.String"));
            dt.Columns.Add("IdPadre", Type.GetType("System.Int32"));
            dt.Columns.Add("IdHijo", Type.GetType("System.Int32"));

            return dt;
        }
        public static void AgregarDetalleAfiliacion(DataTable Dt, String Asegurado, string Beneficiario, string NroDoc,
                                                                                            string FecNac, string Seguro, string TipoMoneda, decimal Prima, string CiaSeguros, string plan,
                                                                                            bool acepto, string producto, string idAsegurado, int idMoneda, string nombreColegio,
                                                                                            int IdProducto, int IdAsociacion, string FileNamePlanSeguro,int id , int IdHijo, int idPadre)
        {
            DataRow Fila = Dt.NewRow();

            Fila["Asegurado"] = Asegurado;
            Fila["Beneficiario"] = Beneficiario;
            Fila["NroDoc"] = NroDoc;
            Fila["FecNac"] = FecNac;
            Fila["Seguro"] = Seguro;
            Fila["TipoMoneda"] = Seguro;
            Fila["Prima"] = Prima;
            Fila["CiaSeguros"] = CiaSeguros;
            Fila["Plan"] = plan;
            Fila["Acepto"] = acepto;
            Fila["Producto"] = producto;
            Fila["idAsegurado"] = idAsegurado;
            Fila["IdMoneda"] = idMoneda;
            Fila["NombreColegio"] = nombreColegio;
            Fila["IdProducto"] = IdProducto;
            Fila["IdAsociacion"] = IdAsociacion;
            Fila["IdHijo"] =IdHijo;
            //Fila["FilePlanSeguro"] = FilePlanSeguro;
            Fila["FileNamePlanSeguro"] = FileNamePlanSeguro;
            //Fila["Id"] = id;
            Fila["IdPadre"] = idPadre;
           

            Dt.Rows.Add(Fila);
        }

        public static void EditarDetalleAfiliacion(DataTable Dt, String Asegurado, string Beneficiario, string NroDoc,
                                                                                            string FecNac, int id, int filaIndex)
        {

            DataRow Fila = Dt.Rows[filaIndex];
                Fila["Asegurado"] = Asegurado;
                Fila["Beneficiario"] = Beneficiario;
                Fila["NroDoc"] = NroDoc;
                Fila["FecNac"] = FecNac;            

        }
        public static void EditarDetalleAfiliacionRenta(DataTable Dt, String Asegurado, string Beneficiario, string NroDoc,
                                                                                            string FecNac, int id, int filaIndex, int idproducto, int tipoAsociacion, int i)
        {
            if (idproducto == 2 && tipoAsociacion == 1)
            {
                foreach (DataRow FilaA in Dt.Rows)
                {
                    FilaA["Asegurado"] = Asegurado;
                }
                Dt.Rows[filaIndex]["Beneficiario"] = Beneficiario;
                Dt.Rows[filaIndex]["NroDoc"] = NroDoc;
                Dt.Rows[filaIndex]["FecNac"] = FecNac;
            }
            else if (idproducto == 2 && tipoAsociacion == 2 && i == 1)
            {
                foreach (DataRow Fila in Dt.Rows)
                {
                    if (Fila["Asegurado"].ToString().Contains("Reservado") == false)
                    {
                        Fila["Asegurado"] = Asegurado;
                    }
                }
                Dt.Rows[filaIndex]["Beneficiario"] = Beneficiario;
                Dt.Rows[filaIndex]["NroDoc"] = NroDoc;
                Dt.Rows[filaIndex]["FecNac"] = FecNac;
            }
            else if (idproducto == 2 && tipoAsociacion == 2 && i == 2)
            {                
                foreach (DataRow Fila in Dt.Rows)
                {
                    Fila["Asegurado"] = Asegurado ;
                    Dt.Rows[filaIndex]["Beneficiario"] = Beneficiario;
                    Dt.Rows[filaIndex]["NroDoc"] = NroDoc;
                    Dt.Rows[filaIndex]["FecNac"] = FecNac;
                }

            }
        }
               
        public static DataTable DatosAsegurado()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", Type.GetType("System.Int32"));
            dt.Columns.Add("ApePaterno", Type.GetType("System.String"));
            dt.Columns.Add("ApeMaterno", Type.GetType("System.String"));
            dt.Columns.Add("Nombres", Type.GetType("System.String"));
            dt.Columns.Add("TipoDoc", Type.GetType("System.Int32"));
            dt.Columns.Add("NumDoc", Type.GetType("System.String"));
            dt.Columns.Add("FecNac", Type.GetType("System.String"));
            dt.Columns.Add("Grado", Type.GetType("System.String"));
            dt.Columns.Add("Seccion", Type.GetType("System.String"));
            dt.Columns.Add("Genero", Type.GetType("System.Boolean"));
            dt.Columns.Add("idAsegurado", Type.GetType("System.String"));
            return dt;
        }

        

        public static DataTable DatosPadreAseguradoRenta()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", Type.GetType("System.Int32"));
            dt.Columns.Add("ApePaterno", Type.GetType("System.String"));
            dt.Columns.Add("ApeMaterno", Type.GetType("System.String"));
            dt.Columns.Add("Nombres", Type.GetType("System.String"));
            dt.Columns.Add("TipoDoc", Type.GetType("System.Int32"));
            dt.Columns.Add("NumDoc", Type.GetType("System.String"));
            dt.Columns.Add("FecNac", Type.GetType("System.String"));
            dt.Columns.Add("Parentesco", Type.GetType("System.Int32"));
            dt.Columns.Add("idAsegurado", Type.GetType("System.String"));

            return dt;
        }

        public static void AgregarDetallePadreAsegurado(DataTable dt, String ApePaterno, string ApeMaterno, string Nombres,
                                                                                           string TipoDoc, string Numerodoc, string FecNac,  int  idRegistro,int parentesco)
        {
            DataRow Fila = dt.NewRow();
            Fila["Id"] = dt.Rows.Count;
            Fila["ApePaterno"] = ApePaterno;
            Fila["ApeMaterno"] = ApeMaterno;
            Fila["Nombres"] = Nombres;
            Fila["TipoDoc"] =  TipoDoc;
            Fila["NumDoc"] = Numerodoc;
            Fila["FecNac"] = FecNac;
            Fila["Parentesco"] =parentesco;
            dt.Rows.Add(Fila);
        }


        public static int AgregarDetalleAsegurado(DataTable dt, String ApePaterno, string ApeMaterno, string Nombres,
                                                                                           int TipoDoc, string Numerodoc, string FecNac, string Grado, string Seccion, bool Genero,
                                                                                           string idTipoProducto,int idRegistro)
        {
            if (idTipoProducto == "2")
            {
                foreach (DataRow fila in dt.Rows)
                {
                    if ((ApePaterno + ApeMaterno + Nombres).Trim().ToUpper() == (fila["ApePaterno"].ToString() + fila["ApeMaterno"].ToString() + fila["Nombres"].ToString()).Trim().ToUpper())
                    {
                        return 1;
                    }
                }
            }
            idRegistro= dt.Rows.Count;
            DataRow Fila = dt.NewRow();
            Fila["Id"] = idRegistro;
            Fila["ApePaterno"] = ApePaterno;
            Fila["ApeMaterno"] = ApeMaterno;
            Fila["Nombres"] = Nombres;
            Fila["TipoDoc"] = TipoDoc;
            Fila["NumDoc"] = Numerodoc;
            Fila["FecNac"] = FecNac;
            Fila["Grado"] = Grado;
            Fila["Seccion"] = Seccion;
            Fila["Genero"] = Genero;                        
            dt.Rows.Add(Fila);
            return 0;
        }

        public static void EditarDetalleAsegurado(DataTable dt, String ApePaterno, string ApeMaterno, string Nombres,
                                                                                           int TipoDoc, string Numerodoc, string FecNac, string Grado, string Seccion, bool Genero,
                                                                                            int filaIndex)
        {
            
              DataRow fila = dt.Rows[filaIndex];                
               fila["ApePaterno"] = ApePaterno;
                fila["ApeMaterno"] = ApeMaterno;
                fila["Nombres"] = Nombres;
                fila["TipoDoc"] = TipoDoc;
                fila["NumDoc"] = Numerodoc;
                fila["FecNac"] = FecNac;
                fila["Grado"] = Grado;
                fila["Seccion"] = Seccion;    
                fila["Genero"] = Genero;                            
        }

        public static void EditarDetalleAseguradoRenta(DataTable dt, String ApePaterno, string ApeMaterno, string Nombres,
                                                                                         int TipoDoc, string Numerodoc, string FecNac, string Grado, string Seccion,
                                                                                         bool Genero,
                                                                                         int idHijo,int tipoasociacion,int indice)
        {
            
            if (tipoasociacion == 1)
            {
                foreach (DataRow fila in dt.Rows)
                {
                    if (Convert.ToInt32(fila["id"]) == idHijo)
                    {
                        fila["ApePaterno"] = ApePaterno;
                        fila["ApeMaterno"] = ApeMaterno;

                        fila["Nombres"] = Nombres;
                        fila["TipoDoc"] = TipoDoc;
                        fila["NumDoc"] = Numerodoc;
                        fila["FecNac"] = FecNac;
                        fila["Grado"] = Grado;
                        fila["Seccion"] = Seccion;
                        fila["Genero"] = Genero;                        
                    }
                }
            }
            else if (tipoasociacion == 2)
            {
                //foreach (DataRow fila in dt.Rows)
                    for(int z=0;z<dt.Rows.Count;z++)
                    {                        
                        if (Convert.ToInt32(dt.Rows[z]["id"]) == indice)
                        {
                        dt.Rows[z]["ApePaterno"] = ApePaterno;                        

                        dt.Rows[z]["ApeMaterno"] = ApeMaterno;
                       
                        dt.Rows[z]["Nombres"] = Nombres;
                       
                        dt.Rows[z]["TipoDoc"] = TipoDoc;                       

                        dt.Rows[z]["NumDoc"] = Numerodoc;                        

                        dt.Rows[z]["FecNac"] = FecNac;                        

                        dt.Rows[z]["Grado"] = Grado;
                        
                        dt.Rows[z]["Seccion"] = Seccion;                        

                        dt.Rows[z]["Genero"] = Genero;                        
                    }
                }
            }
        }
        
        public static void EditarDetallePadreAsegurado(DataTable dt, String ApePaterno, string ApeMaterno, string Nombres,
                                                                                           int TipoDoc, string Numerodoc, string FecNac, int id, int TipoAsociacion ,int indice )
        {

            if (TipoAsociacion == 1)
            {
                foreach (DataRow fila in dt.Rows)
                {
                    //if (Convert.ToInt32(fila["id"]) == id && fila["ApePaterno"].ToString().ToUpper() != "RESERVADO")
                    if (fila["ApePaterno"].ToString().ToUpper() != "RESERVADO")
                    {
                        fila["ApePaterno"] = ApePaterno;
                        fila["ApeMaterno"] = ApeMaterno;
                        fila["Nombres"] = Nombres;
                        fila["TipoDoc"] = TipoDoc;
                        fila["NumDoc"] = Numerodoc;
                        fila["FecNac"] = FecNac;                        
                    }
                }
            }

            else if (TipoAsociacion == 2 && indice == 1)
            {

                foreach (DataRow fila in dt.Rows)
                {
                    //if (Convert.ToInt32(fila["id"]) == id && fila["ApePaterno"].ToString().ToUpper() != "RESERVADO")
                    if (fila["ApePaterno"].ToString().ToUpper() != "RESERVADO")
                    {
                        fila["ApePaterno"] = ApePaterno;
                        fila["ApeMaterno"] = ApeMaterno;
                        fila["Nombres"] = Nombres;
                        fila["TipoDoc"] = TipoDoc;
                        fila["NumDoc"] = Numerodoc;
                        fila["FecNac"] = FecNac;
                    }
                }            
            }
            else if (TipoAsociacion == 2 && indice == 2)
            {
                //if ((indice % 2) == 0)
                //{
                string asegurado = dt.Rows[id]["ApePaterno"].ToString().ToUpper() + dt.Rows[id]["ApeMaterno"].ToString().ToUpper() + dt.Rows[id]["Nombres"].ToString().ToUpper();

                for (int i = 0; i < dt.Rows.Count; i++)
                    {                    
                    if (dt.Rows[i]["ApePaterno"].ToString().ToUpper() + dt.Rows[id]["ApeMaterno"].ToString().ToUpper() + dt.Rows[id]["Nombres"].ToString().ToUpper() == asegurado)
                    {
                        dt.Rows[i]["ApePaterno"] = ApePaterno;
                        dt.Rows[i]["ApeMaterno"] = ApeMaterno;
                        dt.Rows[i]["Nombres"] = Nombres;
                        dt.Rows[i]["TipoDoc"] = TipoDoc;
                        dt.Rows[i]["NumDoc"] = Numerodoc;
                        dt.Rows[i]["FecNac"] = FecNac;                        
                    }
                }
              //  }
                //else
                //{
                //    for (int i = 1; i < dt.Rows.Count; i += 2)
                //    {
                //        dt.Rows[i]["ApePaterno"] = ApePaterno;
                //        dt.Rows[i]["ApeMaterno"] = ApeMaterno;
                //        dt.Rows[i]["Nombres"] = Nombres;
                //        dt.Rows[i]["TipoDoc"] = TipoDoc;
                //        dt.Rows[i]["NumDoc"] = Numerodoc;
                //        dt.Rows[i]["FecNac"] = FecNac;
                //    }
                //}

            }
        }

        //public static void EditarDetallePadreAseguradoRenta2(DataTable dt, String ApePaterno, string ApeMaterno, string Nombres,
        //                                                                                   int TipoDoc, string Numerodoc, string FecNac, int id, string idAsegurado, int filaindex, string padreAntiguo)
        //{
        //    if ((Convert.ToInt32(filaindex) % 2) == 0)
        //    {
        //        for (int fila = 0; fila < dt.Rows.Count; fila += 2)
        //        {
        //            dt.Rows[fila]["ApePaterno"] = ApePaterno;
        //            dt.Rows[fila]["ApeMaterno"] = ApeMaterno;
        //            dt.Rows[fila]["Nombres"] = Nombres;
        //            dt.Rows[fila]["TipoDoc"] = TipoDoc;
        //            dt.Rows[fila]["NumDoc"] = Numerodoc;
        //            dt.Rows[fila]["FecNac"] = FecNac;
        //            dt.Rows[fila]["idAsegurado"] = idAsegurado;
        //        }
        //    }
        //    else
        //    {
        //        for (int fila = 1; fila < dt.Rows.Count; fila += 2)
        //        {
        //            dt.Rows[fila]["ApePaterno"] = ApePaterno;
        //            dt.Rows[fila]["ApeMaterno"] = ApeMaterno;
        //            dt.Rows[fila]["Nombres"] = Nombres;
        //            dt.Rows[fila]["TipoDoc"] = TipoDoc;
        //            dt.Rows[fila]["NumDoc"] = Numerodoc;
        //            dt.Rows[fila]["FecNac"] = FecNac;
        //            dt.Rows[fila]["idAsegurado"] = idAsegurado;
        //        }
        //    }
        //}
        public static void EliminarAsegurado(DataTable dt, int id)
        {
            for (int fila = 0; fila <= dt.Rows.Count; fila++)
            {
                //if (Convert.ToInt32(fila[0]) == id)
                //{
                dt.Rows.RemoveAt(fila);
                //}
            }
        }
        
        public static void EliminarDetalleAsegurado(DataTable dt, int id)
        {
            for (int fila = 0; fila <= dt.Rows.Count; fila++)
            {
                //if (Convert.ToInt32(fila[0]) == id)
                //{
                dt.Rows.RemoveAt(fila);
                //}
            }
        }
        public static string LeerArchivoTexto(string archivo)
        {
            if (File.Exists(archivo))
            {
                using (StreamReader arch = new StreamReader(archivo, System.Text.Encoding.UTF7))
                {
                    return arch.ReadToEnd();
                }
            }
            else
            {
                throw new Exception("No existe el archivo");
            }
        }

        public static System.Drawing.Image Convertir_Bytes_Imagen(byte[] bytes)
        {
            if (bytes == null) return null;

            MemoryStream ms = new MemoryStream(bytes);
            Bitmap bm = null;
            try
            {
                bm = new Bitmap(ms);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return bm;
        }
        public static DataSet ObtenerDatosExcel(string path, string Tabla)
        {
            DataSet ds = new DataSet();
            if (File.Exists(path))
            {
                OleDbConnection oConn = new OleDbConnection();
                OleDbCommand oCmd = new OleDbCommand();
                OleDbDataAdapter oDa = new OleDbDataAdapter();
                oConn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + path + ";" + "Extended Properties=\"Excel 12.0 Xml;HDR=False;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text\"";

                oConn.Open();
                oCmd.CommandText = "SELECT  *  FROM " + Tabla + " Order by  Item ";
                oCmd.Connection = oConn;
                oDa.SelectCommand = oCmd;
                oDa.Fill(ds);
                oConn.Close();
            }
            return ds;
        }
        public static int ObtenerNumerosCampos(int ConfiguracionArchivoID)
        {
            using (ArchivoDAO db = new ArchivoDAO())
            {
                return db.NumeroCamposArchivo(ConfiguracionArchivoID);
            }
        }
        public static DataSet ListaCamposArchivo(int ConfiguracionArchivoID)
        {
            using (ArchivoDAO db = new ArchivoDAO())
            {
                return db.ListaCamposArchivo(ConfiguracionArchivoID);
            }
        }

        public static void ListarRecaudadores(DropDownList cbo)
        {
            using (ConfiguracionArchivo_DAO db = new ConfiguracionArchivo_DAO())
            {
                cbo.DataSource = db.ListarRecaudador();
                cbo.DataValueField = "ID";
                cbo.DataTextField = "nombre";
                cbo.DataBind();
            }
        }

        public static void ListarRoles(DropDownList cbo)
        {
            using (Usuario_DAO db = new Usuario_DAO())
            {
                cbo.DataSource = db.CargarRoles();
                cbo.DataValueField = "ID";
                cbo.DataTextField = "name";
                cbo.DataBind();
            }
        }


        public static DataTable LeerPagosScotiank(int bnkid, string bnkds, string usr, string path)
        {
            DataTable dtDatos = new DataTable();
            dtDatos.Columns.Add(new DataColumn("Tipo", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("CuentaEmpresa", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Secuencia", typeof(string)));
            //dtDatos.Columns.Add(new DataColumn("CodigoUsuario", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("CodDeudor", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("NumRecibo", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Usuario", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Moneda", typeof(string)));
            //dtDatos.Columns.Add(new DataColumn("Importe1", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("ImportePagado", typeof(string))); 
            dtDatos.Columns.Add(new DataColumn("Importe2", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Importe3", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Importe4", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Importe5", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Importe6", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("FechaVencimiento", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("FechaPago", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("TipoPago", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("MedioPago", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("NumOperacion", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("RefCobro", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Filler", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("FinRegistro", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Usr", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Bnkid", typeof(Int32)));
            dtDatos.Columns.Add(new DataColumn("Bnkds", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Mon", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Cod", typeof(Int32)));
            dtDatos.Columns.Add(new DataColumn("Obs", typeof(string)));

            int counter = 0;
            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                DataRow fila = dtDatos.NewRow();
                fila["Tipo"] = line.Substring(0, 1);
                fila["CuentaEmpresa"] = line.Substring(1, 14);
                fila["Secuencia"] = line.Substring(15, 3);
                //fila["CodigoUsuario"] = line.Substring(18, 15);
                fila["CodDeudor"] = line.Substring(18, 15);
                fila["NumRecibo"] = line.Substring(33, 1);                
                fila["Usuario"] = line.Substring(48, 20);
                fila["Moneda"] = line.Substring(68, 4);
                //fila["Importe1"] = line.Substring(72, 9);
                fila["ImportePagado"] = line.Substring(72, 9); 
                fila["Importe2"] = line.Substring(81, 9);
                fila["Importe3"] = line.Substring(90, 9);
                fila["Importe4"] = line.Substring(99, 9);
                fila["Importe5"] = line.Substring(118, 9);
                fila["Importe6"] = line.Substring(126, 8);
                fila["FechaVencimiento"] = line.Substring(134, 8);
                fila["FechaPago"] = line.Substring(134, 8);
                fila["TipoPago"] = line.Substring(142, 1);
                fila["MedioPago"] = line.Substring(143, 1);
                fila["NumOperacion"] = line.Substring(144, 13);
                fila["RefCobro"] = line.Substring(157, 20);
                fila["Filler"] = line.Substring(177, 2);
                fila["FinRegistro"] = line.Substring(179, 1);
                fila["Usr"] = usr;
                fila["Bnkid"] = bnkid;
                fila["Bnkds"] = bnkds;
                dtDatos.Rows.Add(fila);
                counter++;
            }
            //}
            file.Close();
            return dtDatos;
        }
        public static DataTable LeerPagosBBVA(int bnkid, string bnkds, string Usr, string path)
        {
            DataTable dtDatos = new DataTable();
            dtDatos.Columns.Add(new DataColumn("Tipo", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Referencias", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("CodDeudor", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("NombreUsuario", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("ImporteOrigen", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("ImportePagado", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("ImporteMora", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("OficinaPago", typeof(string)));
            //dtDatos.Columns.Add(new DataColumn("NumMovimiento", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("NumOperacion", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("FechaPago", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("FormasPago", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("CanalEntrada", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Vacio", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("TipoMoneda", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("CuentaRecaudadora", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Usr", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Bnkid", typeof(Int32)));
            dtDatos.Columns.Add(new DataColumn("Bnkds", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Mon", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Cod", typeof(Int32)));
            dtDatos.Columns.Add(new DataColumn("Obs", typeof(string)));
            int counter = 0;
            string line;
            string moneda = "";
            string cuentaRecaudadora = "";
            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                DataRow fila = dtDatos.NewRow();


                fila["Tipo"] = line.Substring(0, 2);
                if (counter == 0)
                {
                    moneda = line.Substring(16, 3);
                    cuentaRecaudadora = line.Substring(27, 18);
                }
                else
                {
                    fila["TipoMoneda"] = (moneda.ToUpper() == "PEN" ? "1" : moneda.ToUpper() == "USD" ? "2" : "0");
                    fila["CuentaRecaudadora"] = cuentaRecaudadora;                    
                    fila["NombreUsuario"] = line.Substring(2, 30);
                    fila["referencias"] = line.Substring(32, 48);
                    fila["codDeudor"] = line.Substring(32, 14);
                    fila["ImporteOrigen"] = line.Substring(80, 15);
                    //fila["ImporteDeposito"] = line.Substring(95, 15);
                    fila["ImportePagado"] = line.Substring(95, 15);
                    fila["ImporteMora"] = line.Substring(110, 15);
                    fila["OficinaPago"] = line.Substring(125, 4);
                    fila["NumOperacion"] = line.Substring(129, 6);
                    fila["FechaPago"] = line.Substring(135, 8);
                    fila["FormasPago"] = line.Substring(143, 2);
                    fila["CanalEntrada"] = line.Substring(145, 2);
                    fila["Vacio"] = line.Substring(147, 5);
                    fila["Usr"] = Usr;
                    fila["Bnkid"] = bnkid;
                    fila["Bnkds"] = bnkds;
                }

                dtDatos.Rows.Add(fila);
                counter++;
            }
            file.Close();
            return dtDatos;
        }

        public static DataTable LeerPagosInterbank(int bnkid, string bnkds, string Usr, string path)
        {
            DataTable dtDatos = new DataTable();
            dtDatos.Columns.Add(new DataColumn("CodRubro", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("CodEmpresa", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("CodServicio", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Moneda", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("CodDeudor", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("CodCuota", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("NumDocumento", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("NomDeudor", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("FechaPago", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("HoraPago", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("ImportePagado", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("ImporteMora", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("ImporteDescuento", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("FechaEmision", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("FechaVencimiento", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("NumOperacion", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("NumCheque", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("IndicadorCheque", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Blancos", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Usr", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Bnkid", typeof(Int32)));
            dtDatos.Columns.Add(new DataColumn("Bnkds", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Mon", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Cod", typeof(Int32)));
            dtDatos.Columns.Add(new DataColumn("Obs", typeof(string)));
            int counter = 0;
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                DataRow fila = dtDatos.NewRow();
                fila["CodRubro"] = line.Substring(0, 2);
                fila["CodEmpresa"] = line.Substring(2, 3);
                fila["CodServicio"] = line.Substring(5, 2);
                fila["Moneda"] = line.Substring(7, 2);
                fila["CodDeudor"] = line.Substring(9, 20);
                fila["CodCuota"] = line.Substring(29, 8);
                fila["NumDocumento"] = line.Substring(37, 15);
                fila["NomDeudor"] = line.Substring(52, 30);
                fila["FechaPago"] = line.Substring(82, 8);
                fila["HoraPago"] = line.Substring(90, 6);
                fila["ImportePagado"] = line.Substring(96, 13);
                fila["ImporteMora"] = line.Substring(109, 7);
                fila["ImporteDescuento"] = line.Substring(116, 7);
                fila["FechaEmision"] = line.Substring(123, 8);
                fila["FechaVencimiento"] = line.Substring(131, 8);
                fila["NumOperacion"] = line.Substring(139, 8);
                fila["NumCheque"] = line.Substring(147, 10);
                fila["IndicadorCheque"] = line.Substring(157, 1);
                fila["Blancos"] = line.Substring(158, 2);
                fila["Usr"] = Usr;
                fila["Bnkid"] = bnkid;
                fila["Bnkds"] = bnkds;
                dtDatos.Rows.Add(fila);
                counter++;
            }
            file.Close();
            return dtDatos;
        }

        public static DataTable CrearInstitucionesxCampaña()
        {
            DataTable dtDatos = new DataTable();
            dtDatos.Columns.Add(new DataColumn("AsociacionId", typeof(string)));
            dtDatos.Columns.Add(new DataColumn("Filtro", typeof(string)));
            return dtDatos;
        }
        public static void AgregarInstitucionesxCampaña(DataTable dt, InstitucionAsociada ins)
        {
            DataRow row = dt.NewRow();
            row[0] = ins.idAsociado;
            row[1] = ins.Filtro;
            dt.Rows.Add(row);
        }
        public static int Edad(DateTime fechaNacimiento)
        {
            //Obtengo la diferencia en años.
            int edad = DateTime.Now.Year - fechaNacimiento.Year;

            //Obtengo la fecha de cumpleaños de este año.
            DateTime nacimientoAhora = fechaNacimiento.AddYears(edad);
 
            if (DateTime.Now.CompareTo(nacimientoAhora) > 0)
            {
                edad--;
            }

            return edad;
        }


        public static StringBuilder sbDatosConsultaPagos(int ProductoId, List<Institucion_Educativa> Datos) {

            //StringBuilder sb = new StringBuilder();
            //Institucion_Educativa item;

            ////sb.Append("<html>");
            ////sb.Append("<head>");
            ////sb.Append("<style>");
            ////sb.Append(".CobTitFileAcc {font - weight:bold; font - family:Arial; font - size:10px; background - color:#FF0000;}");
            ////sb.Append("</style>");
            ////sb.Append("</head>");
            ////sb.Append("<body>");
            ////Cabecera
            //if (ProductoId == 1)
            //{                
            //    sb.Append("<table cellpadding='0' cellspacing='0' border='1' data-xls-columns='24'>");
            //    sb.Append("<tr>");
            //    sb.Append("<td data-xls-class='heading2' data-xls-col-index='0'>ITEM</td>");
            //    sb.Append("<td style='background-color:#FF0000;' data-xls-col-index='1'>Plan</td>");
            //    sb.Append("<td style='background-color:#FF0000;' data-xls-col-index='2'>Tipo Documento</td>");
            //    sb.Append("<td style='background-color:#FF0000;' data-xls-col-index='3'>Nro. Documento</td>");
            //    sb.Append("<td style='background-color:#FF0000;' data-xls-col-index='4'>Apellido Paterno</td>");
            //    sb.Append("<td style='background-color:#FF0000;' data-xls-col-index='5'>Apellido Materno</td>");
            //    sb.Append("<td style='background-color:#FF0000;' data-xls-col-index='6'>Nombres</td>");
            //    sb.Append("<td style='background-color:#FF0000;' data-xls-col-index='7'>Fecha Nacimiento</td>");
            //    sb.Append("<td style='background-color:#FF0000;' data-xls-col-index='8'>Domicilio</td>");
            //    sb.Append("<td style='background-color:#FF0000;' data-xls-col-index='9'>" + System.Web.HttpUtility.HtmlEncode("Profesión /Ocupación") + "</td>");
            //    sb.Append("<td style='background-color:#FF0000;' data-xls-col-index='10'>Becado</td>");
            //    sb.Append("<td style='background-color:#FF0000;' data-xls-col-index='11'>Sexo</td>");
            //    sb.Append("<td style='background-color:#FF0000;' data-xls-col-index='12'>Estado Civil</td>");
            //    sb.Append("<td style='background-color:#FF0000;' data-xls-col-index='13'>Grado</td>");
            //    sb.Append("<td style='background-color:#FF0000;' data-xls-col-index='14'>" + System.Web.HttpUtility.HtmlEncode("Sección") + "</td>");
            //    sb.Append("<td style='background-color:#FF0000;' data-xls-col-index='15'>Observaciones</td>");

            //    sb.Append("<td style='background-color:#FFFF00;' data-xls-col-index='16'>" + System.Web.HttpUtility.HtmlEncode("Institución Educativa") + "</td>");
            //    sb.Append("<td style='background-color:#FFFF00;' data-xls-col-index='17'>" + System.Web.HttpUtility.HtmlEncode("Nro. Póliza") + "</td>");
            //    sb.Append("<td style='background-color:#FFFF00;' data-xls-col-index='18'>" + System.Web.HttpUtility.HtmlEncode("Código de Contratante") + "</td>");
            //    sb.Append("<td style='background-color:#FFFF00;' data-xls-col-index='19'>Contratante</td>");
            //    sb.Append("<td style='background-color:#FFFF00;' data-xls-col-index='20'>Estado</td>");
            //    sb.Append("<td style='background-color:#FFFF00;' data-xls-col-index='21'>Fecha de Pago</td>");
            //    sb.Append("<td style='background-color:#FFFF00;' data-xls-col-index='22'>Banco</td>");
            //    sb.Append("<td style='background-color:#FFFF00;' data-xls-col-index='23'>" + System.Web.HttpUtility.HtmlEncode("Nro. Operación") + "</td>");
            //    sb.Append("</tr>");
            //}
            //else if (ProductoId == 2)
            //{
            //    sb.Append("<table cellpadding='0' cellspacing='0' border='1' data-xls-columns='27'>");
            //    sb.Append("<tr style='font-weight:bold; font-family:Arial; font-size:10px;'>");
            //    sb.Append("<td colspan='9' align='center' style='color:White; background-color:#666699;' data-xls-col-index='0'>ASEGURADO (PADRE Y/O MADRE)</td>");
            //    sb.Append("<td colspan='18' align='center' style='color:White; background-color:#993300;' data-xls-col-index='9'>BENEFICIARIO (ALUMNO)</td>");
            //    sb.Append("</tr>");
            //    sb.Append("<tr style='font-weight:bold; font-family:Arial; font-size:10px;'>");
            //    sb.Append("<td style='color:White; background-color:#FF6600;' data-xls-col-index='0'>ITEM</td>");
            //    sb.Append("<td style='color:White; background-color:#FF6600;' data-xls-col-index='1'>Tipo Documento</td>");
            //    sb.Append("<td style='color:White; background-color:#FF6600;' data-xls-col-index='2'>Nro. Documento</td>");
            //    sb.Append("<td style='color:White; background-color:#FF6600;' data-xls-col-index='3'>Apellido Paterno</td>");
            //    sb.Append("<td style='color:White; background-color:#FF6600;' data-xls-col-index='4'>Apellido Materno</td>");
            //    sb.Append("<td style='color:White; background-color:#FF6600;' data-xls-col-index='5'>Nombres</td>");
            //    sb.Append("<td style='color:White; background-color:#FF6600;' data-xls-col-index='6'>Fecha Nacimiento</td>");
            //    sb.Append("<td style='color:White; background-color:#FF6600;' data-xls-col-index='7'>Sexo</td>");
            //    sb.Append("<td style='color:White; background-color:#FF6600;' data-xls-col-index='8'>Parentesco</td>");
            //    sb.Append("<td style='color:White; background-color:#FF6600;' data-xls-col-index='9'>Tipo Documento</td>");
            //    sb.Append("<td style='color:White; background-color:#FF6600;' data-xls-col-index='10'>Nro. Documento</td>");
            //    sb.Append("<td style='color:White; background-color:#FF6600;' data-xls-col-index='11'>Apellido Paterno</td>");
            //    sb.Append("<td style='color:White; background-color:#FF6600;' data-xls-col-index='12'>Apellido Materno</td>");
            //    sb.Append("<td style='color:White; background-color:#FF6600;' data-xls-col-index='13'>Nombres</td>");
            //    sb.Append("<td style='color:White; background-color:#FF6600;' data-xls-col-index='14'>Fecha Nacimiento</td>");
            //    sb.Append("<td style='color:White; background-color:#FF6600;' data-xls-col-index='15'>Sexo</td>");
            //    sb.Append("<td style='color:White; background-color:#FF6600;' data-xls-col-index='16'>Grado</td>");
            //    sb.Append("<td style='color:White; background-color:#FF6600;' data-xls-col-index='17'>" + System.Web.HttpUtility.HtmlEncode("Sección") + "</td>");
            //    sb.Append("<td style='color:White; background-color:#FF6600;' data-xls-col-index='18'>Beneficiario por</td>");

            //    sb.Append("<td style='color:Black; background-color:#FFFF00;' data-xls-col-index='19'>" + System.Web.HttpUtility.HtmlEncode("Institución Educativa") + "</td>");
            //    sb.Append("<td style='color:Black; background-color:#FFFF00;' data-xls-col-index='20'>" + System.Web.HttpUtility.HtmlEncode("Nro Póliza") + "</td>");
            //    sb.Append("<td style='color:Black; background-color:#FFFF00;' data-xls-col-index='21'>" + System.Web.HttpUtility.HtmlEncode("Código de Contratante") + "</td>");
            //    sb.Append("<td style='color:Black; background-color:#FFFF00;' data-xls-col-index='22'>Contratante</td>");
            //    sb.Append("<td style='color:Black; background-color:#FFFF00;' data-xls-col-index='23'>Estado</td>");
            //    sb.Append("<td style='color:Black; background-color:#FFFF00;' data-xls-col-index='24'>Fecha de Pago</td>");
            //    sb.Append("<td style='color:Black; background-color:#FFFF00;' data-xls-col-index='25'>Banco</td>");
            //    sb.Append("<td style='color:Black; background-color:#FFFF00;' data-xls-col-index='26'>" + System.Web.HttpUtility.HtmlEncode("Nro Operación") + "</td>");
            //    sb.Append("</tr>");
            //}

            ////Detalle
            //for (int i = 0; i < Datos.Count; i++)
            //{
            //    item = Datos[i];
            //    sb.Append("<tr style='color:Black; font-family:Arial; font-size:10px;'>");
            //    if (ProductoId == 1)
            //    {
            //        sb.Append(String.Format("<td align='left' data-xls-col-index='0'>{0}</td>", Convert.ToString(i + 1)));
            //        sb.Append(String.Format("<td align='left' data-xls-col-index='1'>{0}</td>", ""));
            //        sb.Append(String.Format("<td align='left' data-xls-col-index='2'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.AlumnoTipoDocumentoDsc)));
            //        sb.Append(String.Format("<td align='left' style='vnd.ms-excel.numberformat:@' data-xls-col-index='3'>{0}</td>", item.AlumnoNumeroDocumento));
            //        sb.Append(String.Format("<td align='left' data-xls-col-index='4'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.AlumnoApellidoPaterno)));
            //        sb.Append(String.Format("<td align='left' data-xls-col-index='5'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.AlumnoApellidoMaterno)));
            //        sb.Append(String.Format("<td align='center' data-xls-col-index='6'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.AlumnoNombre)));
            //        sb.Append(String.Format("<td align='center' data-xls-col-index='7'>{0}</td>", Convert.ToDateTime(item.AlumnoFechaNacimiento).ToString("dd/MM/yyyy")));
            //        sb.Append(String.Format("<td align='left' data-xls-col-index='8'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.InstitucionEducativaDireccion)));
            //        sb.Append(String.Format("<td align='center' data-xls-col-index='9'>{0}</td>", ""));
            //        sb.Append(String.Format("<td align='center' data-xls-col-index='10'>{0}</td>", ""));
            //        sb.Append(String.Format("<td align='left' data-xls-col-index='11'>{0}</td>", item.AlumnoSexoDsc));
            //        sb.Append(String.Format("<td align='center' data-xls-col-index='12'>{0}</td>", ""));
            //        sb.Append(String.Format("<td align='right' data-xls-col-index='13'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.AlumnoGrado)));
            //        sb.Append(String.Format("<td align='right' data-xls-col-index='14'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.AlumnoSeccion)));
            //        sb.Append(String.Format("<td align='right' data-xls-col-index='15'>{0}</td>", ""));

            //        sb.Append(String.Format("<td align='right' data-xls-col-index='16'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.InstitucionEducativaNombre)));
            //        sb.Append(String.Format("<td align='right' data-xls-col-index='17'>{0}</td>", item.NroPoliza));
            //        sb.Append(String.Format("<td align='right' data-xls-col-index='18'>{0}</td>", item.CodigoContratante));
            //        sb.Append(String.Format("<td align='right' data-xls-col-index='19'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.NombreContratante)));
            //        sb.Append(String.Format("<td align='right' data-xls-col-index='20'>{0}</td>", item.IsPagadoDsc));
            //        sb.Append(String.Format("<td align='right' data-xls-col-index='21'>{0}</td>", Convert.ToDateTime(item.FechaPago).ToString("dd/MM/yyyy")));
            //        sb.Append(String.Format("<td align='right' data-xls-col-index='22'>{0}</td>", item.BancoPagoNombre));
            //        sb.Append(String.Format("<td align='right' style='vnd.ms-excel.numberformat:@' data-xls-col-index='23'>{0}</td>", item.OperacionBancaria));
            //    }
            //    else if (ProductoId == 2)
            //    {
            //        sb.Append(String.Format("<td align='left' data-xls-col-index='0'>{0}</td>", Convert.ToString(i + 1)));
            //        sb.Append(String.Format("<td align='left' data-xls-col-index='1'>{0}</td>", item.PadreTipoDocumento));
            //        sb.Append(String.Format("<td align='left' style='vnd.ms-excel.numberformat:@' data-xls-col-index='2'>{0}</td>", item.PadreNumeroDocumento));
            //        sb.Append(String.Format("<td align='left' data-xls-col-index='3'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.PadreApellidoPaterno)));
            //        sb.Append(String.Format("<td align='left' data-xls-col-index='4'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.PadreApellidoMaterno)));
            //        sb.Append(String.Format("<td align='left' data-xls-col-index='5'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.PadreNombre)));
            //        sb.Append(String.Format("<td align='center' data-xls-col-index='6'>{0}</td>", Convert.ToDateTime(item.PadreFechaNacimiento).ToString("dd/MM/yyyy")));
            //        sb.Append(String.Format("<td align='center' data-xls-col-index='7'>{0}</td>", ""));
            //        sb.Append(String.Format("<td align='center' data-xls-col-index='8'>{0}</td>", item.PadreTipoPadreNombre));
            //        sb.Append(String.Format("<td align='center' data-xls-col-index='9'>{0}</td>", item.AlumnoTipoDocumentoDsc));
            //        sb.Append(String.Format("<td align='center' style='vnd.ms-excel.numberformat:@' data-xls-col-index='10'>{0}</td>", item.AlumnoNumeroDocumento));
            //        sb.Append(String.Format("<td align='center' data-xls-col-index='11'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.AlumnoApellidoPaterno)));
            //        sb.Append(String.Format("<td align='center' data-xls-col-index='12'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.AlumnoApellidoMaterno)));
            //        sb.Append(String.Format("<td align='center' data-xls-col-index='13'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.AlumnoNombre)));
            //        sb.Append(String.Format("<td align='center' data-xls-col-index='14'>{0}</td>", Convert.ToDateTime(item.AlumnoFechaNacimiento).ToString("dd/MM/yyyy")));
            //        sb.Append(String.Format("<td align='center' data-xls-col-index='15'>{0}</td>", item.AlumnoSexoDsc));
            //        sb.Append(String.Format("<td align='right' data-xls-col-index='16'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.AlumnoGrado)));
            //        sb.Append(String.Format("<td align='right' data-xls-col-index='17'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.AlumnoSeccion)));
            //        sb.Append(String.Format("<td align='right' data-xls-col-index='18'>{0}</td>", item.PadreTipoPadreNombre));

            //        sb.Append(String.Format("<td align='right' data-xls-col-index='19'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.InstitucionEducativaNombre)));
            //        sb.Append(String.Format("<td align='right' data-xls-col-index='20'>{0}</td>", item.NroPoliza));
            //        sb.Append(String.Format("<td align='right' data-xls-col-index='21'>{0}</td>", item.CodigoContratante));
            //        sb.Append(String.Format("<td align='right' data-xls-col-index='22'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.NombreContratante)));
            //        sb.Append(String.Format("<td align='right' data-xls-col-index='23'>{0}</td>", item.IsPagadoDsc));
            //        sb.Append(String.Format("<td align='right' data-xls-col-index='24'>{0}</td>", Convert.ToDateTime(item.FechaPago).ToString("dd/MM/yyyy")));
            //        sb.Append(String.Format("<td align='right' data-xls-col-index='25'>{0}</td>", item.BancoPagoNombre));
            //        sb.Append(String.Format("<td align='right' style='vnd.ms-excel.numberformat:@' data-xls-col-index='26'>{0}</td>", " " + item.OperacionBancaria));
            //    }
            //    sb.Append("</tr>");


            //}
            //sb.Append("</table>");
            ////sb.Append("</body>");
            ////sb.Append("</html>");
            //return sb;

            StringBuilder sb = new StringBuilder();
            Institucion_Educativa item;

            sb.Append("<table cellpadding='0' cellspacing='0' border='1'>");
            //Cabecera
            if (ProductoId == 1)
            {
                sb.Append("<tr style='font-weight:bold; font-family:Arial; font-size:10px;'>");
                sb.Append("<td style='background-color:#FF0000;'>ITEM</td>");
                sb.Append("<td style='background-color:#FF0000;'>Plan</td>");
                sb.Append("<td style='background-color:#FF0000;'>Tipo Documento</td>");
                sb.Append("<td style='background-color:#FF0000;'>Nro. Documento</td>");
                sb.Append("<td style='background-color:#FF0000;'>Apellido Paterno</td>");
                sb.Append("<td style='background-color:#FF0000;'>Apellido Materno</td>");
                sb.Append("<td style='background-color:#FF0000;'>Nombres</td>");
                sb.Append("<td style='background-color:#FF0000;'>Fecha Nacimiento</td>");
                sb.Append("<td style='background-color:#FF0000;'>Domicilio</td>");
                sb.Append("<td style='background-color:#FF0000;'>" + System.Web.HttpUtility.HtmlEncode("Profesión /Ocupación") + "</td>");
                sb.Append("<td style='background-color:#FF0000;'>Becado</td>");
                sb.Append("<td style='background-color:#FF0000;'>Sexo</td>");
                sb.Append("<td style='background-color:#FF0000;'>Estado Civil</td>");
                sb.Append("<td style='background-color:#FF0000;'>Grado</td>");
                sb.Append("<td style='background-color:#FF0000;'>" + System.Web.HttpUtility.HtmlEncode("Sección") + "</td>");
                sb.Append("<td style='background-color:#FF0000;'>Observaciones</td>");

                sb.Append("<td style='background-color:#FFFF00;'>" + System.Web.HttpUtility.HtmlEncode("Institución Educativa") + "</td>");
                sb.Append("<td style='background-color:#FFFF00;'>" + System.Web.HttpUtility.HtmlEncode("Nro. Póliza") + "</td>");
                sb.Append("<td style='background-color:#FFFF00;'>" + System.Web.HttpUtility.HtmlEncode("Código de Contratante") + "</td>");
                sb.Append("<td style='background-color:#FFFF00;'>Contratante</td>");
                sb.Append("<td style='background-color:#FFFF00;'>Estado</td>");
                sb.Append("<td style='background-color:#FFFF00;'>Fecha de Pago</td>");
                sb.Append("<td style='background-color:#FFFF00;'>Banco</td>");
                sb.Append("<td style='background-color:#FFFF00;'>" + System.Web.HttpUtility.HtmlEncode("Nro. Operación") + "</td>");
                sb.Append("</tr>");
            }
            else if (ProductoId == 2)
            {

                sb.Append("<tr style='font-weight:bold; font-family:Arial; font-size:10px;'>");
                sb.Append("<td colspan='9' align='center' style='color:White; background-color:#666699;'>ASEGURADO (PADRE Y/O MADRE)</td>");
                sb.Append("<td colspan='18' align='center' style='color:White; background-color:#993300;'>BENEFICIARIO (ALUMNO)</td>");
                sb.Append("</tr>");
                sb.Append("<tr style='font-weight:bold; font-family:Arial; font-size:10px;'>");
                sb.Append("<td style='color:White; background-color:#FF6600;'>ITEM</td>");
                sb.Append("<td style='color:White; background-color:#FF6600;'>Tipo Documento</td>");
                sb.Append("<td style='color:White; background-color:#FF6600;'>Nro. Documento</td>");
                sb.Append("<td style='color:White; background-color:#FF6600;'>Apellido Paterno</td>");
                sb.Append("<td style='color:White; background-color:#FF6600;'>Apellido Materno</td>");
                sb.Append("<td style='color:White; background-color:#FF6600;'>Nombres</td>");
                sb.Append("<td style='color:White; background-color:#FF6600;'>Fecha Nacimiento</td>");
                sb.Append("<td style='color:White; background-color:#FF6600;'>Sexo</td>");
                sb.Append("<td style='color:White; background-color:#FF6600;'>Parentesco</td>");
                sb.Append("<td style='color:White; background-color:#FF6600;'>Tipo Documento</td>");
                sb.Append("<td style='color:White; background-color:#FF6600;'>Nro. Documento</td>");
                sb.Append("<td style='color:White; background-color:#FF6600;'>Apellido Paterno</td>");
                sb.Append("<td style='color:White; background-color:#FF6600;'>Apellido Materno</td>");
                sb.Append("<td style='color:White; background-color:#FF6600;'>Nombres</td>");
                sb.Append("<td style='color:White; background-color:#FF6600;'>Fecha Nacimiento</td>");
                sb.Append("<td style='color:White; background-color:#FF6600;'>Sexo</td>");
                sb.Append("<td style='color:White; background-color:#FF6600;'>Grado</td>");
                sb.Append("<td style='color:White; background-color:#FF6600;'>" + System.Web.HttpUtility.HtmlEncode("Sección") + "</td>");
                sb.Append("<td style='color:White; background-color:#FF6600;'>Beneficiario por</td>");

                sb.Append("<td style='color:Black; background-color:#FFFF00; '>" + System.Web.HttpUtility.HtmlEncode("Institución Educativa") + "</td>");
                sb.Append("<td style='color:Black; background-color:#FFFF00;'>" + System.Web.HttpUtility.HtmlEncode("Nro Póliza") + "</td>");
                sb.Append("<td style='color:Black; background-color:#FFFF00;'>" + System.Web.HttpUtility.HtmlEncode("Código de Contratante") + "</td>");
                sb.Append("<td style='color:Black; background-color:#FFFF00;'>Contratante</td>");
                sb.Append("<td style='color:Black; background-color:#FFFF00;'>Estado</td>");
                sb.Append("<td style='color:Black; background-color:#FFFF00;'>Fecha de Pago</td>");
                sb.Append("<td style='color:Black; background-color:#FFFF00;'>Banco</td>");
                sb.Append("<td style='color:Black; background-color:#FFFF00;'>" + System.Web.HttpUtility.HtmlEncode("Nro Operación") + "</td>");
                sb.Append("</tr>");
            }

            //Detalle
            for (int i = 0; i < Datos.Count; i++)
            {
                item = Datos[i];
                sb.Append("<tr style='color:Black; font-family:Arial; font-size:10px;'>");
                if (ProductoId == 1)
                {
                    sb.Append(String.Format("<td align='left'>{0}</td>", Convert.ToString(i + 1)));
                    sb.Append(String.Format("<td align='left'>{0}</td>", ""));
                    sb.Append(String.Format("<td align='left'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.AlumnoTipoDocumentoDsc)));
                    sb.Append(String.Format("<td align='left' style='vnd.ms-excel.numberformat:@'>{0}</td>", item.AlumnoNumeroDocumento));
                    sb.Append(String.Format("<td align='left'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.AlumnoApellidoPaterno)));
                    sb.Append(String.Format("<td align='left'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.AlumnoApellidoMaterno)));
                    sb.Append(String.Format("<td align='center'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.AlumnoNombre)));
                    sb.Append(String.Format("<td align='center'>{0}</td>", Convert.ToDateTime(item.AlumnoFechaNacimiento).ToString("dd/MM/yyyy")));
                    sb.Append(String.Format("<td align='left'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.InstitucionEducativaDireccion)));
                    sb.Append(String.Format("<td align='center'>{0}</td>", ""));
                    sb.Append(String.Format("<td align='center'>{0}</td>", ""));
                    sb.Append(String.Format("<td align='left'>{0}</td>", item.AlumnoSexoDsc));
                    sb.Append(String.Format("<td align='center'>{0}</td>", ""));
                    sb.Append(String.Format("<td align='rigth'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.AlumnoGrado)));
                    sb.Append(String.Format("<td align='rigth'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.AlumnoSeccion)));
                    sb.Append(String.Format("<td align='rigth'>{0}</td>", ""));

                    sb.Append(String.Format("<td align='rigth'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.InstitucionEducativaNombre)));
                    sb.Append(String.Format("<td align='rigth'>{0}</td>", item.NroPoliza));
                    sb.Append(String.Format("<td align='rigth'>{0}</td>", item.CodigoContratante));
                    sb.Append(String.Format("<td align='rigth'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.NombreContratante)));
                    sb.Append(String.Format("<td align='rigth'>{0}</td>", item.IsPagadoDsc));
                    sb.Append(String.Format("<td align='rigth'>{0}</td>", Convert.ToDateTime(item.FechaPago).ToString("dd/MM/yyyy")));
                    sb.Append(String.Format("<td align='rigth'>{0}</td>", item.BancoPagoNombre));
                    sb.Append(String.Format("<td align='rigth'; style='vnd.ms-excel.numberformat:@'>{0}</td>", item.OperacionBancaria));
                }
                else if (ProductoId == 2)
                {
                    sb.Append(String.Format("<td align='left'>{0}</td>", Convert.ToString(i + 1)));
                    sb.Append(String.Format("<td align='left'>{0}</td>", item.PadreTipoDocumento));
                    sb.Append(String.Format("<td align='left' style='vnd.ms-excel.numberformat:@'>{0}</td>", item.PadreNumeroDocumento));
                    sb.Append(String.Format("<td align='left'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.PadreApellidoPaterno)));
                    sb.Append(String.Format("<td align='left'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.PadreApellidoMaterno)));
                    sb.Append(String.Format("<td align='left'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.PadreNombre)));
                    sb.Append(String.Format("<td align='center'>{0}</td>", Convert.ToDateTime(item.PadreFechaNacimiento).ToString("dd/MM/yyyy")));
                    sb.Append(String.Format("<td align='center'>{0}</td>", ""));
                    sb.Append(String.Format("<td align='center'>{0}</td>", item.PadreTipoPadreNombre));
                    sb.Append(String.Format("<td align='center'>{0}</td>", item.AlumnoTipoDocumentoDsc));
                    sb.Append(String.Format("<td align='center' style='vnd.ms-excel.numberformat:@'>{0}</td>", item.AlumnoNumeroDocumento));
                    sb.Append(String.Format("<td align='center'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.AlumnoApellidoPaterno)));
                    sb.Append(String.Format("<td align='center'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.AlumnoApellidoMaterno)));
                    sb.Append(String.Format("<td align='center'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.AlumnoNombre)));
                    sb.Append(String.Format("<td align='center'>{0}</td>", Convert.ToDateTime(item.AlumnoFechaNacimiento).ToString("dd/MM/yyyy")));
                    sb.Append(String.Format("<td align='center'>{0}</td>", item.AlumnoSexoDsc));
                    sb.Append(String.Format("<td align='rigth'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.AlumnoGrado)));
                    sb.Append(String.Format("<td align='rigth'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.AlumnoSeccion)));
                    sb.Append(String.Format("<td align='rigth'>{0}</td>", item.PadreTipoPadreNombre));

                    sb.Append(String.Format("<td align='rigth'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.InstitucionEducativaNombre)));
                    sb.Append(String.Format("<td align='rigth'>{0}</td>", item.NroPoliza));
                    sb.Append(String.Format("<td align='rigth'>{0}</td>", item.CodigoContratante));
                    sb.Append(String.Format("<td align='rigth'>{0}</td>", System.Web.HttpUtility.HtmlEncode(item.NombreContratante)));
                    sb.Append(String.Format("<td align='rigth'>{0}</td>", item.IsPagadoDsc));
                    sb.Append(String.Format("<td align='rigth'>{0}</td>", Convert.ToDateTime(item.FechaPago).ToString("dd/MM/yyyy")));
                    sb.Append(String.Format("<td align='rigth'>{0}</td>", item.BancoPagoNombre));
                    sb.Append(String.Format("<td align='rigth' style='vnd.ms-excel.numberformat:@'>{0}</td>", " " + item.OperacionBancaria));
                }
                sb.Append("</tr>");


            }
            sb.Append("</table>");
            return sb;
        }

        public class InstitucionAsociada
        {
            public int idAsociado { get; set; }
            public string Filtro { get; set; }
        }

    }
}





