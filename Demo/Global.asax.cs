using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Demo
{
    public class Global : System.Web.HttpApplication
    {
        
        protected void Application_Start(object sender, EventArgs e)
        {

        }

        void Session_Start(object sender, EventArgs e)
        {
            Session["AfiliacionProducto"] = null;
            Session["DatosAsegurado"] = null;
            Session["DatosPadreAsegurado"] = null;
            Session["DatosAfiliaProducto"] = null;
            
            Session["usuAfiliacion"] = null;
            Session["idEducativa"] = null;
            Session["idContacto"] = null;
            Session["idbanco"] = null;
            Session["pathArch"] = null;
            Session["Usuario"] = null;

            Session["ListaAfiliaAccidentes"] = null;
            Session["ListaAfiliaRentas"] = null;
            Session["dtDetallePlan"] = null;
            Session["caracteres"] = null;
            Session["ListaAfiliaAccidentesx"] = null;
            Session["ListaAfiliaRentasx"] = null;
            Session["CA"] = 0;
            Session["CP"] = 0;
            Session["UltimoEliminado"] = false;
            Session["ED"] = null;
        }

        void Session_End(Object sender, EventArgs E)
        {
            Session["AfiliacionProducto"] = null;
            Session["DatosAsegurado"] = null;
            Session["DatosPadreAsegurado"] = null;
            Session["DatosAfiliaProducto"] = null;

            Session["usuAfiliacion"] = null;
            Session["idEducativa"] = null;
            Session["idContacto"] = null;
            Session["idbanco"] = null;
            Session["pathArch"] = null;
            Session["Usuario"] = null;

            Session["ListaAfiliaAccidentes"] = null;
            Session["ListaAfiliaRentas"] = null;
            Session["dtDetallePlan"] = null;
        }
    }
}
