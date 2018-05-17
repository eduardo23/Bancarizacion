using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAO_Hermes.Repositorios;
using System.Data;
using System.Text.RegularExpressions;
using Ionic.Zip;

namespace Demo
{
    public partial class GenerarCargarTXT : System.Web.UI.Page
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
            cUtil.ListarBancos(DDLBanco);
            cUtil.ListarMonedas(DDLMoneda);
            cUtil.ListarRecaudadores(DDLRecaudador);
        }

        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            if (DDLBanco.SelectedValue == "0" || DDLMoneda.SelectedValue == "0" || DDLRecaudador.SelectedValue == "0")
            {
                txtmensaje.Text = "Debe seleccionar  Banco,Moneda y Recaudador";
                string jss = "openModal()";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
            }
            else
            {
                try
                { 
                //SCOTIANK
                if (DDLBanco.SelectedValue == "1")
                {
                    System.Text.StringBuilder detalle = new System.Text.StringBuilder();

                    if (DDLMoneda.SelectedItem.Value == "3")
                    {
                            try
                            {
                                detalle.Append(Generar_Arch_Scotiank(1, 1, "320464900007", "20100883725", Convert.ToInt32(DDLRecaudador.SelectedValue)));
                                detalle.Append(Generar_Arch_Scotiank(1, 2, "320464900007", "20100883725", Convert.ToInt32(DDLRecaudador.SelectedValue)));
                            }
                            catch(Exception ex)
                            {

                            }
                    }
                    else
                    {
                            detalle.Append(Generar_Arch_Scotiank(1, Convert.ToInt32(DDLMoneda.SelectedValue), "320464900007", "20100883725",
                            Convert.ToInt32(DDLRecaudador.SelectedValue)));
                    }

                    string RUTA = cUtil.ObtenerValorParametroDes("FILE", "RUTA", "GENERAR");
                    string NombreArchivo = cUtil.ObtenerValorParametroDes("FILE", "SCOTIANK", "GENERAR");
                    NombreArchivo = RUTA + NombreArchivo;

                    cUtil.EscribirArchivo(NombreArchivo, detalle.ToString());
                    System.IO.FileInfo toDownload = new System.IO.FileInfo(NombreArchivo);
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
                    Response.AddHeader("Content-Length", toDownload.Length.ToString());
                    Response.ContentType = "text/plain";
                    Response.WriteFile(NombreArchivo);
                    Response.End();
                }

                if (DDLBanco.SelectedValue == "3")
                {
                    System.Text.StringBuilder detalle = new System.Text.StringBuilder();

                    if (DDLMoneda.SelectedItem.Value == "3")
                    {
                          detalle.Append(Generar_Arch_Interbank(3, 3, "", "", Convert.ToInt32(DDLRecaudador.SelectedValue)));
                    }
                    else
                    {
                        detalle.Append(Generar_Arch_Interbank(3, Convert.ToInt32(DDLMoneda.SelectedValue), "", "", Convert.ToInt32(DDLRecaudador.SelectedValue)));
                    }
                    string RUTA = cUtil.ObtenerValorParametroDes("FILE", "RUTA", "GENERAR");
                    string NombreArchivo = cUtil.ObtenerValorParametroDes("FILE", "INTERBANK", "GENERAR");
                    NombreArchivo = RUTA + NombreArchivo;

                    cUtil.EscribirArchivo(NombreArchivo, detalle.ToString());
                    System.IO.FileInfo toDownload = new System.IO.FileInfo(NombreArchivo);
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
                    Response.AddHeader("Content-Length", toDownload.Length.ToString());
                    Response.ContentType = "text/plain";
                    Response.WriteFile(NombreArchivo);
                    Response.End();
                }

                //BBVA
                if (DDLBanco.SelectedValue == "2")
                {
                    System.Text.StringBuilder detalleDolares = new System.Text.StringBuilder();
                    System.Text.StringBuilder detalle = new System.Text.StringBuilder();

                    if (DDLMoneda.SelectedItem.Value == "3")
                    {
                        detalle.Append(Generar_Arch_BBVA(2, 1, "320464900007", "20100883725", Convert.ToInt32(DDLRecaudador.SelectedValue)));

                        detalleDolares.Append(Generar_Arch_BBVA(2, 2, "320464900007", "20100883725", Convert.ToInt32(DDLRecaudador.SelectedValue)));
                        DownloadFiles(null, null, detalle.ToString(), detalleDolares.ToString());
                    }
                    else
                    {
                        string NombreArchivo = "";
                        detalle.Append(Generar_Arch_BBVA(Convert.ToInt32(DDLBanco.SelectedValue), Convert.ToInt32(DDLMoneda.SelectedValue), "320464900007", "20100883725", Convert.ToInt32(DDLRecaudador.SelectedValue)));
                        if (DDLMoneda.SelectedValue == "1")
                        {
                                NombreArchivo = cUtil.ObtenerValorParametroDes("FILE", "BBVA-SOLES", "GENERAR") + DateTime.Now.Date.ToString("MMdd") + ".TXT" ;
                        }
                        if (DDLMoneda.SelectedValue == "2" || DDLMoneda.SelectedValue == "3")
                        {
                            NombreArchivo = cUtil.ObtenerValorParametroDes("FILE", "BBVA-DOLARES", "GENERAR") +DateTime.Now.Date.ToString("MMdd")  + ".TXT";
                        }

                        
                        string RUTA = cUtil.ObtenerValorParametroDes("FILE", "RUTA", "GENERAR");
                        NombreArchivo = RUTA + NombreArchivo;

                        cUtil.EscribirArchivo(NombreArchivo, detalle.ToString());
                        System.IO.FileInfo toDownload = new System.IO.FileInfo(NombreArchivo);
                        Response.Clear();
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
                        Response.AddHeader("Content-Length", toDownload.Length.ToString());
                        //Response.Charset = System.Text.Encoding.GetEncoding(437);
                        //Response.ContentEncoding = System.Text.Encoding.GetEncoding(437);
                        Response.ContentType = "text/plain";
                        Response.WriteFile(NombreArchivo);
                        Response.Flush();
                        Response.End();
                    }
                 }
               }
                catch (Exception ex)
                {
                    txtmensaje.Text = "No existen registros para generar el archivo ";
                    string jss = "openModal()";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
                }
            }                
        }

        //void CargarArchivo(int bancoid,int monedaid)
        //{
        //    string RUTA = cUtil.ObtenerValorParametroDes("FILE", "RUTA", "GENERAR");
        //    string NombreArchivo = cUtil.ObtenerValorParametroDes("FILE", "BBVA-SOLES", "GENERAR");
        //    NombreArchivo = RUTA + NombreArchivo;

        //    System.Text.StringBuilder detalleSoles = new System.Text.StringBuilder();
        //    detalleSoles.Append(Generar_Arch_BBVA(bancoid, monedaid, "320464900007", "20100883725"));
        //    //detalleDolares.Append(Generar_Arch_BBVA(Convert.ToInt32(DDLBanco.SelectedValue), 2, "320464900007", "20100883725"));
        //    //string nombreSOL = @"c:\base\" + "Hermes_" + DateTime.Now.ToString("yyyMMddss") + ".txt";
        //    cUtil.EscribirArchivo(NombreArchivo, detalleSoles.ToString());
        //    System.IO.FileInfo toDownloadSOL = new System.IO.FileInfo(NombreArchivo);
        //    Response.Clear();
        //    Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownloadSOL.Name);
        //    Response.AddHeader("Content-Length", toDownloadSOL.Length.ToString());
        //    Response.ContentType = "text/plain";
        //    Response.WriteFile(NombreArchivo);
        //    Response.End();
        //}

        //public void Download(int bancoid, int monedaid)
        //{
        //    System.Text.StringBuilder detalleSoles = new System.Text.StringBuilder();
        //    detalleSoles.Append(Generar_Arch_BBVA(bancoid, monedaid, "320464900007", "20100883725"));            
        //    string nombreSOL = @"c:\base\" + "Hermes_" + DateTime.Now.ToString("yyyMMddss") + ".TXT";
        //    cUtil.EscribirArchivo(nombreSOL, detalleSoles.ToString());

        //    System.IO.FileInfo toDownload = new System.IO.FileInfo(nombreSOL);
        //    Response.BufferOutput = true;
        //    HttpContext.Current.Response.ClearContent();
        //    HttpContext.Current.Response.Clear();
        //    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
        //    HttpContext.Current.Response.AddHeader("Content-Length", toDownload.Length.ToString());
        //    HttpContext.Current.Response.ContentType = "application/octet-stream";
        //    HttpContext.Current.Response.TransmitFile(nombreSOL);
        //    Response.Flush();
        //    HttpContext.Current.ApplicationInstance.CompleteRequest();
        //}
        private string Generar_Arch_Scotiank(int BancoId, int MonedaId, string Cuenta, string RUC, int RecaudadorId)
        {

            DataTable dt = new DataTable();
            DataTable dtc = new DataTable();
            ConfiguracionArchivo_DAO db = new ConfiguracionArchivo_DAO();
            System.Text.StringBuilder detalle = new System.Text.StringBuilder();
            decimal TotalSoles = 0;
            decimal TotalDolares = 0;
            if (db.LIST_PARAM_BANCO_CFG(1, 2).Tables[0].Rows.Count > 0)
            {

                String FechaEnvio = DateTime.Now.ToString("yyyyMMdd");
                //Obtener Registro cabecera Banco Scotiank
                dt = db.LIST_PARAM_BANCO_CFG(1, 2).Tables[0];
                dtc = db.LIST_PARAM_BANCO_CFG(1, 4).Tables[0];

                DataSet ds = db.LISTAR_DET_SCOTIANK_CARGA(BancoId,
                                                                                                                   MonedaId, Cuenta, RecaudadorId);

                Int32 Cantidad = ds.Tables[0].Rows.Count;
                decimal TotalPrimas = 0;

                foreach (DataRow fila in ds.Tables[0].Rows)
                {
                    TotalPrimas += Convert.ToDecimal(fila["ImporteCobrar1"]);
                }

                if (MonedaId == 1)
                {
                    TotalSoles = TotalPrimas;
                }
                else if (MonedaId == 2)
                {
                    TotalDolares = TotalPrimas;
                }
            
            string RegistroCabecera = "";

                if (MonedaId == 1)
                {
                    RegistroCabecera = dt.Rows[0]["Valor"].ToString() +                                //Header
                                  dt.Rows[1]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[1]["Longitud"]), ' ') +  //CuentaEmpresa
                                  MonedaId.ToString().PadLeft(3, '0') +//CodSecuencia 001-002 -Soles o dolares                                  
                                  Cantidad.ToString().PadLeft(Convert.ToInt32(dt.Rows[5]["Longitud"]), '0') + //Cantidad de Registros
                                  TotalSoles.ToString().Replace(".", "").PadLeft(Convert.ToInt32(dt.Rows[6]["Longitud"]), '0') + //Total de soles
                                  "00000000000000000" + //dt.Rows[8]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[8]["Longitud"]), ' '); /RUC Empresa
                                                                                                                                                                      RUC.PadRight(Convert.ToInt32(dt.Rows[8]["Longitud"]), ' ') + //RUC Empresa  
                                  FechaEnvio.PadRight(Convert.ToInt32(dt.Rows[9]["Longitud"]), ' ') +  // FechaEnvio

                                  ds.Tables[0].Rows[0]["FechaVencimiento"].ToString() +// Fecha Vigencia
                                  dt.Rows[11]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[11]["Longitud"]), '0') +  // Filler
                                  dt.Rows[12]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[12]["Longitud"]), '0') +  // Dias Mora
                                  dt.Rows[13]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[13]["Longitud"]), '0') +  // Tipo Mora
                                  dt.Rows[18]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[18]["Longitud"]), '0') +  //  Mora FLAT
                                  dt.Rows[19]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[19]["Longitud"]), '0') +  //  % Mora                                
                                  dt.Rows[20]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[20]["Longitud"]), '0') +  //  Monto Fijo
                                  dt.Rows[21]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[21]["Longitud"]), '0') +  // Tipo descuento                                  
                                  dt.Rows[22]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[22]["Longitud"]), '0') +  // Monto a descontar
                                  dt.Rows[23]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[23]["Longitud"]), '0') +  // % Descuento
                                  dt.Rows[24]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[24]["Longitud"]), '0') +  // Dias Descuento
                                  dt.Rows[25]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[25]["Longitud"]), ' ') +  // Filler
                                  dt.Rows[26]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[26]["Longitud"]), ' ');  // Fin de archivo

                }


                if (MonedaId == 2)
                {
                                  RegistroCabecera = dt.Rows[0]["Valor"].ToString() +                                //Header
                                  dt.Rows[1]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[1]["Longitud"]), ' ') +  //CuentaEmpresa
                                  MonedaId.ToString().PadLeft(3, '0') +//CodSecuencia 001-002 -Soles o dolares                                  
                                  Cantidad.ToString().PadLeft(Convert.ToInt32(dt.Rows[5]["Longitud"]), '0') + //Cantidad de Registros
                                  "00000000000000000" +
                                  TotalDolares.ToString().Replace(".", "").PadLeft(Convert.ToInt32(dt.Rows[7]["Longitud"]), '0').Replace(".", "") + //Total de dolares
                                  RUC.PadRight(Convert.ToInt32(dt.Rows[8]["Longitud"]), ' ') + //RUC Empresa  
                                  FechaEnvio.PadRight(Convert.ToInt32(dt.Rows[9]["Longitud"]), ' ') +  // FechaEnvio

                                  ds.Tables[0].Rows[0]["FechaVencimiento"].ToString() +// Fecha Vigencia
                                  dt.Rows[11]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[11]["Longitud"]), '0') +  // Filler
                                  dt.Rows[12]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[12]["Longitud"]), '0') +  // Dias Mora
                                  dt.Rows[13]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[13]["Longitud"]), '0') +  // Tipo Mora
                                  dt.Rows[18]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[18]["Longitud"]), '0') +  //  Mora FLAT
                                  dt.Rows[19]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[19]["Longitud"]), '0') +  //  % Mora                                
                                  dt.Rows[20]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[20]["Longitud"]), '0') +  //  Monto Fijo
                                  dt.Rows[21]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[21]["Longitud"]), '0') +  // Tipo descuento                                  
                                  dt.Rows[22]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[22]["Longitud"]), '0') +  // Monto a descontar
                                  dt.Rows[23]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[23]["Longitud"]), '0') +  // % Descuento
                                  dt.Rows[24]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[24]["Longitud"]), '0') +  // Dias Descuento
                                  dt.Rows[25]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[25]["Longitud"]), ' ') +  // Filler
                                  dt.Rows[26]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[26]["Longitud"]), ' ');  // Fin de archivo
                }                                                                                                                                 //dt.Rows[8]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[8]["Longitud"]), ' '); /RUC Empresa



                ///Corte
             //   dtc = ds.Tables[1];
                string RegistroCorteRenta = "";

                if (MonedaId == 1)
                {
                    RegistroCorteRenta = "C320464900007  00101SEGURO DE ACCIDENTES ESTUDIANT0575247700001                                                                                                                                                                                              *" + "\r\n" +
                                         "C320464900007  00102SEGURO DE RENTA AMBOS PADRES -0575247700001                                                                                                                                                                                              *" + "\r\n" +
                                         "C320464900007  00103SEGURO ONCOLOGICO ESTUDIANTIL 0575247700001                                                                                                                                                                                              *" + "\r\n";

                    //for (int y = 0; y <= ds.Tables[1].Rows.Count - 1; y++)
                    //{
                    //    RegistroCorteRenta += ds.Tables[1].Rows[y]["NumCuenta"].ToString().PadRight(15, ' ') +
                    //    ds.Tables[1].Rows[y]["Moneda"].ToString().PadLeft(3, '0') +
                    //    ds.Tables[1].Rows[y]["ProductoID"].ToString().PadLeft(2, '0')  + 
                    //    (ds.Tables[1].Rows[y]["ProductoID"].ToString() == "1" ? "SEGURO DE ACCIDENTES ESTUDIANT" : ds.Tables[1].Rows[y]["ProductoID"].ToString() == "2" ?
                    //    "SEGURO DE RENTA AMBOS PADRES".PadRight(30, ' ') : "")  +  "0" + 
                    //    ds.Tables[1].Rows[y]["Numero"].ToString().PadRight(12, '0') +  " ".PadLeft(188) + "*" + "\r\n";
                    //}
                }

                if (MonedaId == 2)
                {
                    RegistroCorteRenta = "C320464900007  00201SEGURO DE ACCIDENTES ESTUDIANT0320464900007                                                                                                                                                                                              *" + "\r\n" +
                                         "C320464900007  00202SEGURO DE RENTA AMBOS PADRES -0320464900007                                                                                                                                                                                              *" + "\r\n" +
                                         "C320464900007  00203SEGURO ONCOLOGICO ESTUDIANTIL 0320464900007                                                                                                                                                                                              *" + "\r\n";
                    //for (int y = ds.Tables[1].Rows.Count - 1; y > -1; y--)
                    //{
                    //    RegistroCorteRenta += ds.Tables[1].Rows[y]["NumCuenta"].ToString().PadRight(15, ' ') +
                    //    ds.Tables[1].Rows[y]["Moneda"].ToString() +
                    //    ds.Tables[1].Rows[y]["ProductId"].ToString() +
                    //    ds.Tables[1].Rows[y]["Nombre"].ToString() +
                    //    ds.Tables[1].Rows[y]["APP"].ToString() +
                    //    ds.Tables[1].Rows[y]["Numero"].ToString().PadRight(14, ' ') +
                    //    " ".PadLeft(188) +
                    //    "*" + "\r\n";
                    //}
                    //for (int y = 0; y <= ds.Tables[1].Rows.Count - 1; y++)
                    //{
                    //    RegistroCorteRenta += ds.Tables[1].Rows[y]["NumCuenta"].ToString().PadRight(15, ' ') +
                    //    ds.Tables[1].Rows[y]["Moneda"].ToString().PadLeft(3, '0') +
                    //    ds.Tables[1].Rows[y]["ProductoID"].ToString().PadLeft(2, '0') + 
                    //    (ds.Tables[1].Rows[y]["ProductoID"].ToString() == "1" ? "SEGURO DE ACCIDENTES ESTUDIANT" : ds.Tables[1].Rows[y]["ProductoID"].ToString() == "2" ?
                    //    "SEGURO DE RENTA AMBOS PADRES".PadRight(30, ' ') : "") + 
                    //    "0" +
                    //    ds.Tables[1].Rows[y]["Numero"].ToString().PadRight(12, '0') +
                    //    " ".PadLeft(188) + "*" + "\r\n";
                    //}
                }
                detalle.Append(RegistroCabecera + "\r\n");
                int numreg = 1;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {
                        detalle.Append(fila[0].ToString() + //Tipo de Registro
                                            fila[1].ToString().PadRight(14, ' ') + //Cuenta empresa
                                            fila[2].ToString().PadLeft(3, '0') + //Secuencia Servicio
                                            fila[3].ToString().PadRight(15, ' ') + //Codigo Servicio
                                            numreg.ToString().PadLeft(15, '0') + //Numero de recibo
                                            fila[5].ToString() + //CodigoAgrupacion
                                            fila[6].ToString() + // Situacion
                                            fila[7].ToString() + //moneda de cobro
                                            //(QuitAccents(fila[8].ToString()).Length > 20 ? QuitAccents(fila[8].ToString()).Substring(0, 20) : QuitAccents(fila[8].ToString()).PadRight(20, ' ')) +  //Nombre del usuario
                                            (QuitAccents( fila[8].ToString() )) +  //Nombre del usuario
                                            (fila[9].ToString())
                                            + //Referencia recibo
                                            //(fila[9].ToString().Length <= 30 ? fila[9].ToString().PadRight(30, ' ') : fila[9].ToString().Substring(0, 29)) + //Referencia recibo
                                             fila[10].ToString() + //Concepto a Cobrar 1
                                             fila[11].ToString().Replace(".", "").PadLeft(9, '0') + //Importe a cobrar1
                                             fila[12].ToString() +//Concepto a Cobrar 2
                                             fila[13].ToString().Replace(".", "").PadLeft(9, '0') + ////Importe a cobrar2
                                             fila[14].ToString() +//Concepto a Cobrar 3
                                             fila[15].ToString().Replace(".", "").PadLeft(9, '0') + ////Importe a cobrar3
                                             fila[16].ToString() +//Concepto a Cobrar 4
                                             fila[17].ToString().Replace(".", "").PadLeft(9, '0') + ////Importe a cobrar4
                                             fila[18].ToString() +//Concepto a Cobrar 5
                                             fila[19].ToString().Replace(".", "").PadLeft(9, '0') +////Importe a cobrar5
                                             fila[20].ToString() +// Concepto a Cobrar 6
                                             fila[21].ToString().ToString().Replace(".", "").PadLeft(9, '0') +////Importe a cobrar6

                                             fila[22].ToString().ToString().Replace(".", "").PadLeft(15, '0') +//Total cobrar

                                             fila[23].ToString().ToString().Replace(".", "").PadLeft(15, '0') +//Saldo de la deuda

                                             fila[24].ToString().ToString().PadLeft(8, '0') +//minimo

                                             fila[25].ToString() +//orden cronologico

                                             fila[26].ToString() +//Fecha Emision del recibo

                                             fila[27].ToString() +//Fecha vencimiento
                                             fila[28].ToString() +//Dias prorroga
                                             fila[29].ToString() +//Espacios
                                             fila[30].ToString()); // Cierre;
                        detalle.Append("\r\n");
                        numreg += 1;
                    }
                    detalle.Append(RegistroCorteRenta);
                }
            }
            return detalle.ToString();
        }


        private string Generar_Arch_Interbank(int BancoId, int MonedaId, string Cuenta, string RUC, int RecuadadorId)
        {
            DataTable dt = new DataTable();
            ConfiguracionArchivo_DAO db = new ConfiguracionArchivo_DAO();
            System.Text.StringBuilder detalle = new System.Text.StringBuilder();

            if (db.LIST_PARAM_BANCO_CFG(3, 2).Tables[0].Rows.Count > 0)
            {
                decimal TotalSoles = 0;
                decimal TotalDolares = 0;
                String FechaEnvio = DateTime.Now.ToString("yyyyMMdd");
                //Obtener Registro cabecera Banco Scotiank
                dt = db.LIST_PARAM_BANCO_CFG(3, 2).Tables[0];
                //dtc = db.LIST_PARAM_BANCO_CFG(3, 4).Tables[0];
                DataSet ds = db.LISTAR_DET_Interbank_CARGA(BancoId, MonedaId, Cuenta, RecuadadorId);
                if (ds.Tables[0].Rows.Count==0)
                    {
                    throw new Exception("No hay registros para mostrar");                    
                  }
                Int32 Cantidad = ds.Tables[1].Rows.Count;

                foreach (DataRow fila in ds.Tables[1].Rows)
                {
                    if (fila["MonedaDeuda"].ToString() == "01")
                        TotalSoles += Convert.ToDecimal(fila["ImporteConcepto1"]);
                    else if (fila["MonedaDeuda"].ToString() == "10")
                        TotalDolares += Convert.ToDecimal(fila["ImporteConcepto1"]);
                }

                    string RegistroCabecera = dt.Rows[0]["Valor"].ToString() +  //Header
                                  dt.Rows[1]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[1]["Longitud"]), ' ') +  //Codigo Grupo,
                                  dt.Rows[2]["Valor"].ToString().PadLeft(Convert.ToInt32(dt.Rows[2]["Longitud"]), '0') +  //Codigo Unico,
                                  dt.Rows[3]["Valor"].ToString().PadLeft(Convert.ToInt32(dt.Rows[3]["Longitud"]), '0') +  //Codigo rubro
                                  dt.Rows[4]["Valor"].ToString().PadLeft(Convert.ToInt32(dt.Rows[4]["Longitud"]), '0') +  //Codigo Empresa
                                  dt.Rows[5]["Valor"].ToString().PadLeft(Convert.ToInt32(dt.Rows[5]["Longitud"]), '0') +  //Codigo Servicio
                                  dt.Rows[6]["Valor"].ToString().PadLeft(Convert.ToInt32(dt.Rows[6]["Longitud"]), '0') +  //Codigo Requerimiento
                                  dt.Rows[7]["Valor"].ToString().PadLeft(Convert.ToInt32(dt.Rows[7]["Longitud"]), '0') +  //Codigo Solicitud
                                  dt.Rows[8]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[8]["Longitud"]), ' ') +  //Descripcion Solicitud
                                  dt.Rows[9]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[9]["Longitud"]), ' ') +  //Canal de envio
                                  dt.Rows[10]["Valor"].ToString().PadRight(Convert.ToInt32(dt.Rows[10]["Longitud"]), ' ') +  //Tipo Informacion Reemplazo
                                  Cantidad.ToString().PadLeft(Convert.ToInt32(dt.Rows[12]["Longitud"]), '0') +  //Cantidad de Registros
                                  TotalSoles.ToString().Replace(".", "").PadLeft(15, '0') +
                                  TotalDolares.ToString().Replace(".", "").PadLeft(15, '0') +
                                  DateTime.Now.Date.ToString("yyyyMMdd") + //FechaProceso
                                  " ".PadLeft(88, ' ') + // Libre
                                  dt.Rows[17]["Valor"].ToString().PadLeft(Convert.ToInt32(dt.Rows[17]["Longitud"]), '0'); //Valor fijo                                 
                ///Corte             
                detalle.Append(RegistroCabecera + "\r\n");

                int numreg = 1;
                if (ds.Tables[1].Rows.Count > 1)
                {
                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {
                        detalle.Append(fila[0].ToString() + //tipo registro
                                            fila[1].ToString() + //codigoCuota
                                            fila[2].ToString() + //Numero de conceptos
                                            fila[3].ToString() + //Descripcion del concepto1
                                            fila[4].ToString() + //Descripcion del concepto2
                                            fila[5].ToString() + //Descripcion del concepto3
                                            fila[6].ToString() + //Descripcion del concepto4
                                            fila[7].ToString() + //Descripcion del concepto5
                                            fila[8].ToString() + //Descripcion del concepto6
                                            fila[9].ToString() + //Descripcion del concepto7
                                            fila[10].ToString() + //Libre
                                            fila[11].ToString()); //CodigoFijo

                        detalle.Append("\r\n");
                        numreg += 1;
                    }

                    foreach (DataRow fila in ds.Tables[1].Rows)
                    {
                        detalle.Append(fila[0].ToString() + //tipo registro
                                            fila[1].ToString().PadRight(20, ' ') + //codigoUsuario
                                            fila[2].ToString() + //CodigoCuota
                                            //(QuitAccents(fila[3].ToString()).Length > 30 ? QuitAccents(fila[3].ToString()).Substring(0, 30) : QuitAccents(fila[3].ToString()).PadRight(30, ' ')) +
                                            (QuitAccents(fila[3].ToString())) +
                                            fila[4].ToString() + //ReferenciaCorta1
                                            fila[5].ToString() + //ReferenciaCorta2
                                            fila[6].ToString() + //FechaEmision
                                            fila[7].ToString() + //FechaVencimiento
                                            fila[8].ToString() + //NumeroDocumento
                                            fila[9].ToString() + //MonedaDeuda
                                            fila[10].ToString().PadLeft(9,'0') + //importe del concepto1
                                            fila[11].ToString() + //importe del concepto2
                                            fila[12].ToString() + //importe del concepto3
                                            fila[13].ToString() + //importe del concepto4
                                            fila[14].ToString() + //importe del concepto5
                                            fila[15].ToString() + //importe del concepto6
                                            fila[16].ToString() + //importe del concepto7
                                            fila[17].ToString().PadLeft(2, ' ') + //Libre                                            
                                            "M" + //Tipo Operacion                             
                                            fila[19].ToString().PadLeft(13, ' ') + //Libre                   
                                            fila[20].ToString() +
                                            "\r\n");
                        numreg += 1;
                    }
                }
            }
            return detalle.ToString();
        }



        private string Generar_Arch_BBVA(int BancoId, int MonedaId, string Cuenta, string RUC, int RecaudadorId)
        {
            ConfiguracionArchivo_DAO db = new ConfiguracionArchivo_DAO();
            DataTable dtCabecera = new DataTable();
            DataTable dtDetalle = new DataTable();
            DataTable dtPie = new DataTable();
            DataSet dsBBVA = new DataSet();
            System.Text.StringBuilder detalle = new System.Text.StringBuilder();

            dsBBVA = db.LISTAR_DET_BBVA_CARGA(Convert.ToInt32(BancoId),
                                                                                               Convert.ToInt32(MonedaId),
                                                                                               RUC, RecaudadorId);

           
            if (dsBBVA.Tables[1].Rows.Count > 0)
            {
                dtCabecera = dsBBVA.Tables[0];
                dtDetalle = dsBBVA.Tables[1];
                dtPie = dsBBVA.Tables[2];

                string RegistroCabecera = dtCabecera.Rows[0]["TipoRegistro"].ToString() +
                                                                dtCabecera.Rows[0]["RUCEmpresa"].ToString() +
                                                                dtCabecera.Rows[0]["CodigoClase"].ToString() +
                                                                dtCabecera.Rows[0]["TipoMoneda"].ToString() +
                                                                Convert.ToDateTime(dtCabecera.Rows[0]["FechaGenArc"]).ToString("yyyyMMdd") +
                                                                dtCabecera.Rows[0]["Correlativo"].ToString() +
                                                                dtCabecera.Rows[0]["Vacio"].ToString();


                detalle.Append(RegistroCabecera + "\r\n");

                decimal TotalMaxCobrar = 0;
                decimal TotalMinCobrar = 0;

                string RegistroCorteDetalle = "";
                TotalMaxCobrar = 0;
                TotalMinCobrar = 0;
                foreach (DataRow Fila in dtDetalle.Rows)
                {
                    RegistroCorteDetalle = "";
                    string Nombre = Fila["NombreUsuario"].ToString();
                    //if (Nombre.Length > 30)
                    //{
                   //    string sNombre = Nombre.Substring(0, 30);
                        Nombre = QuitAccents(Nombre);
                    //}
                    //else
                    //{
                    //    Nombre = QuitAccents(Fila["NombreUsuario"].ToString()).PadRight(30, ' ');
                   
               
                    RegistroCorteDetalle = Fila["TipoRegistro"].ToString() +
                   QuitAccents( Nombre) +
                   QuitAccents( Fila["CodigoUsuario"].ToString().PadRight(14, ' ') )+
                    Fila["ReferenciaRecibo"].ToString().PadRight(34, ' ') +
                    Fila["FechaVencimiento"].ToString() +
                    Fila["FechaBloqueo"].ToString() +
                    Fila["PeriodosFacturados"].ToString() +
                    Fila["ImporteMaxCobrar"].ToString().Replace(".", "").PadLeft(15, '0') +
                    Fila["ImporteMinCobrar"].ToString().Replace(".", "").PadLeft(15, '0') +
                    Fila["InfoAdic"].ToString() +
                    Fila["cod_sub_concep1"].ToString() + Fila["ValSubconcep1"].ToString() +
                    Fila["cod_sub_concep2"].ToString() + Fila["ValSubconcep2"].ToString() +
                    Fila["cod_sub_concep3"].ToString() + Fila["ValSubconcep3"].ToString() +
                    Fila["cod_sub_concep4"].ToString() + Fila["ValSubconcep4"].ToString() +
                    Fila["cod_sub_concep5"].ToString() + Fila["ValSubconcep5"].ToString() +
                    Fila["cod_sub_concep6"].ToString() + Fila["ValSubconcep6"].ToString() +
                    Fila["cod_sub_concep7"].ToString() + Fila["ValSubconcep7"].ToString() +
                    Fila["cod_sub_concep8"].ToString() + Fila["ValSubconcep8"].ToString() +
                    Fila["NumeroCuentaCliente"].ToString() +
                    Fila["TipoIdentificacion"].ToString() +
                    Fila["NumIdentificacion"].ToString() +  
                    Fila["Vacio"].ToString() + "\r\n" ;

                    TotalMaxCobrar += Convert.ToDecimal(Fila["ImporteMaxCobrar"]);
                    TotalMinCobrar += Convert.ToDecimal(Fila["ImporteMinCobrar"]);
                    detalle.Append(RegistroCorteDetalle);
                }

                dtPie.Rows[0]["TotalNumDatosCobra"] = dtDetalle.Rows.Count;
                dtPie.Rows[0]["SumImpMax1"] = TotalMaxCobrar;
                dtPie.Rows[0]["SumImpMax2"] = TotalMinCobrar;

                string RegistroCortePie = dtPie.Rows[0]["TipoRegistro"].ToString() +
                                                                      dtPie.Rows[0]["TotalNumDatosCobra"].ToString().PadLeft(9, '0') +
                                                                      dtPie.Rows[0]["SumImpMax1"].ToString().Replace(".", "").PadLeft(18, '0') +
                                                                      dtPie.Rows[0]["SumImpMax2"].ToString().Replace(".", "").PadLeft(18, '0') +
                                                                      dtPie.Rows[0]["DatAdic"].ToString() +
                                                                      dtPie.Rows[0]["Vacio"].ToString();

                detalle.Append(RegistroCortePie);
                return detalle.ToString();                
            }
            else
            {
                throw new Exception("No hay registros para mostrar");
            }
       
        }

        private string QuitAccents(string texto)
        {
            string con = "áàäéèëíìïóòöúùuñÁÀÄÂÉÈËÍÌÏÓÒÖÚÙÜÑçÇ";
            string sin = "aaaeeeiiiooouuunAAA EEEIIIOOOUUUNcC";
            for (int i = 0; i < con.Length; i++)
            {
                texto = texto.Replace(con[i], sin[i]);
            }
            return texto ;
        }

        protected void btnbuscar0_Click(object sender, EventArgs e)
        {
            //SCOTIANK
            if (DDLBanco.SelectedValue == "1")
            {
                System.Text.StringBuilder detalle = new System.Text.StringBuilder();
                if (DDLMoneda.SelectedItem.Value == "3")
                {
                    detalle.Append(Generar_Arch_Scotiank(Convert.ToInt32(DDLBanco.SelectedValue), 1, "320464900007", "20100883725", Convert.ToInt32(DDLRecaudador.SelectedValue)));
                    detalle.Append(Generar_Arch_Scotiank(Convert.ToInt32(DDLBanco.SelectedValue), 2, "320464900007", "20100883725", Convert.ToInt32(DDLRecaudador.SelectedValue)));
                }
                else
                {
                    detalle.Append(Generar_Arch_Scotiank(Convert.ToInt32(DDLBanco.SelectedValue), Convert.ToInt32(DDLMoneda.SelectedValue), "320464900007", "20100883725", Convert.ToInt32(DDLRecaudador.SelectedValue)));
                }

                string nombre = @"c:\base\" + "Hermes_" + DateTime.Now.ToString("yyyMMddss") + ".txt";
                cUtil.EscribirArchivo(nombre, detalle.ToString());
                System.IO.FileInfo toDownload = new System.IO.FileInfo(nombre);
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
                Response.AddHeader("Content-Length", toDownload.Length.ToString());
                Response.ContentType = "text/plain";
                Response.WriteFile(nombre);
                Response.End();
            }

            //BBVA
            if (DDLBanco.SelectedValue == "2")
            {
                System.Text.StringBuilder detalleDolares = new System.Text.StringBuilder();
                System.Text.StringBuilder detalle = new System.Text.StringBuilder();

                if (DDLMoneda.SelectedItem.Value == "3")
                {
                    detalle.Append(Generar_Arch_BBVA(2, 1, "320464900007", "20100883725", Convert.ToInt32(DDLRecaudador.SelectedValue)));
                    detalleDolares.Append(Generar_Arch_BBVA(2, 2, "320464900007", "20100883725", Convert.ToInt32(DDLRecaudador.SelectedValue)));
                    DownloadFiles(null, null, detalle.ToString(), detalleDolares.ToString());
                }
                else
                {
                    detalle.Append(Generar_Arch_BBVA(Convert.ToInt32(DDLBanco.SelectedValue), Convert.ToInt32(DDLMoneda.SelectedValue), "320464900007", "20100883725", Convert.ToInt32(DDLRecaudador.SelectedValue)));
                    string nombre = @"c:\base\" + "Hermes_" + DateTime.Now.ToString("yyyMMddss") + ".txt";
                    cUtil.EscribirArchivo(nombre, detalle.ToString());
                    System.IO.FileInfo toDownload = new System.IO.FileInfo(nombre);
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
                    Response.AddHeader("Content-Length", toDownload.Length.ToString());
                    Response.ContentType = "text/plain";
                    Response.WriteFile(nombre);
                    Response.End();
                }
            }
        }

        protected void DownloadFiles(object sender, EventArgs e, string detalle, string detalle2)
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                //zip.AddDirectoryByName("FilesBBVA");
                //foreach (GridViewRow row in GridView1.Rows)
                //{
                //    if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //    {
                //        string filePath = (row.FindControl("lblFilePath") as Label).Text;
                //        zip.AddFile(filePath, "Files");
                //    }
                //}
                string file1 = @"c:\base\" + "Hermes_" + DateTime.Now.ToString("yyyMMddss") + ".txt";
                cUtil.EscribirArchivo(file1, detalle.ToString());
                System.IO.FileInfo toDownload = new System.IO.FileInfo(file1);

                string file2 = @"c:\base\" + "Hermes_" + DateTime.Now.ToString("yyyMMddss") + ".txt";
                cUtil.EscribirArchivo(file2, detalle2.ToString());
                System.IO.FileInfo toDownload2 = new System.IO.FileInfo(file2);

                zip.AddFile(file1, "Soles");
                zip.AddFile(file2, "Dolares");

                Response.Clear();
                Response.BufferOutput = false;
                string zipName = String.Format("{0}.zip", "Hermes_02_Ambos" + DateTime.Now.ToString("yyyMMddss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);
                Response.End();
            }
        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            if (DDLBanco.SelectedValue == "0" || DDLMoneda.SelectedValue == "0" || DDLRecaudador.SelectedValue == "0")
            {
                txtmensaje.Text = "Debe seleccionar  Banco,Moneda y Recaudador";
                string jss = "openModal()";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
            }
            else
            {
                using (CodigoDAO db = new CodigoDAO())
                {
                    string jss = "openCargarArchivo()";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);

                }
            }
        }

        protected void btnCargarArchivo_Click(object sender, EventArgs e)
        {
            if (fupArchivo.PostedFile.FileName != "")
            {
                DataTable dt= new DataTable();

                string rutaDestino = cUtil.ObtenerValorParametro("FILE", "RUTASHARED");
                string fileDestino = rutaDestino + @"\" + fupArchivo.PostedFile.FileName;
                fupArchivo.SaveAs(fileDestino);

                if (DDLBanco.SelectedValue == "1")
                {
                    dt = cUtil.LeerPagosScotiank(Convert.ToInt32(DDLBanco.SelectedValue), DDLBanco.SelectedItem.Text, Session["Usuario"].ToString(), fileDestino);
                    if (dt.Rows.Count > 0)
                    {
                        using (CodigoDAO db = new CodigoDAO())
                        {
                            db.procesarCodigosPagadosScotiank(dt);

                            DataRow[] rows = dt.Select("Tipo='T' or Tipo='H'");
                            foreach (DataRow row in rows)
                            {
                                dt.Rows.Remove(row);
                            }
                            dt.AcceptChanges();
                        }
                    }
                }
                else  if (DDLBanco.SelectedValue == "2")
                {
                    dt = cUtil.LeerPagosBBVA(Convert.ToInt32(DDLBanco.SelectedValue), DDLBanco.SelectedItem.Text, Session["Usuario"].ToString(), fileDestino);
                    if (dt.Rows.Count > 0)
                    {
                        using (CodigoDAO db = new CodigoDAO())
                        {
                            db.procesarCodigosPagadosBBVA(dt);

                            DataRow[] rows = dt.Select("Tipo='01' or Tipo='03'");
                            foreach (DataRow row in rows)
                            {
                                dt.Rows.Remove(row);
                            }
                            dt.AcceptChanges();
                        }
                    }
                }
               else  if (DDLBanco.SelectedValue == "3")
                {
                    dt = cUtil.LeerPagosInterbank(Convert.ToInt32(DDLBanco.SelectedValue), DDLBanco.SelectedItem.Text, Session["Usuario"].ToString(), fileDestino);
                    if (dt.Rows.Count > 0)
                    {
                        using (CodigoDAO db = new CodigoDAO())
                        {
                            db.procesarCodigosPagadosInterbank(dt);
                        }
                    }
                }

                grvResultados.DataSource = dt;
                grvResultados.DataBind();
                lblTotReg.Text = "Total registros: " + dt.Rows.Count.ToString();

                DataRow[] dr1 = dt.Select("cod=1");
                lblSiProc.Text = "Registros procesados: " + dr1.Count().ToString();

                DataRow[] dr2 = dt.Select("cod=2");
                lblNoProc.Text = "Registros no procesados: " + dr2.Count().ToString();

                string jss2 = "openResultados();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
            }
        }
    }
}








