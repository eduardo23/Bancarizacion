using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DAO_Hermes;
using DAO_Hermes.Repositorios;
using DAO_Hermes.ViewModel;
using System.Text;
using System.Data.SqlClient;

namespace Demo
{
    public partial class Home : System.Web.UI.MasterPage
    {
        Usuario_DAO objn = new Usuario_DAO();
        LoguearUsuario obje = new LoguearUsuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //if (Request["userid"] != "" && Request["userid"] != null)
                //{
                    //using (Usuario_DAO db = new Usuario_DAO())
                    //{
                    //    string userid = Request["userid"];

                    //    Users user = db.BuscarUsuarioxId(userid);
                    //    if (user != null)
                    //    {
                    //        Session["Usuario"] = user.Email;
                    //        CL1.Visible = true;
                    //        CL2.Visible = true;
                    //        A.Visible = false;
                    //        B.Visible = false;
                    //        C.Visible = false;
                    //        D.Visible = false;
                    //        E.Visible = false;
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        Response.Redirect("iniciosesion.aspx");
                    //    }
                    //}

                    if(Session["Usuario"] == null)
                    {
                      Response.Redirect("InicioSesion.aspx");
                    }

                    obje.UserName = Session["Usuario"].ToString();
                    obje.Password = Session["Password"].ToString();
                    List<LoguearUsuario> logueo = objn.Validar_Usuario(obje);
                    if (logueo.Count > 0)
                    {
                        if (logueo[0].IdUsuario == "00000000-0000-0000-0000-000000000001")//Administrador
                        {
                            A.Visible = true;
                            B.Visible = true;
                            C.Visible = true;
                            D.Visible = true;
                            E.Visible = true;
                            CL1.Visible = true;
                            CL2.Visible = true;
                        }
                        else
                        {
                            CL1.Visible = true;
                            CL2.Visible = true;
                            A.Visible = false;
                            B.Visible = false;
                            C.Visible = false;
                            D.Visible = false;
                            E.Visible = false;
                        }
                    }
                    else
                    {
                    }
                       lblUsers.Text = Session["Usuario"].ToString();//Con esta session aparecerá el nombre el Usuario..
                //}
                //lblUsers.Text = Session["Usuario"].ToString();
            }

            //protected void btnCerrar_Click(object sender, EventArgs e)
            //{
            //    Session.Abandon();           
            //    HttpContext.Current.Response.Redirect("InicioSesion.aspx", true);

            //}

            //protected void btnCerrarSesion_Click(object sender, ImageClickEventArgs e)
            //{

            //}
        }
    }
}