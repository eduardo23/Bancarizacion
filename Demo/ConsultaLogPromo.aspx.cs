using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using DAO_Hermes.Repositorios;

namespace Demo
{
    public partial class ConsultaLogPromo : System.Web.UI.Page
    {

        public static String UserId = "";
        public static ClientResponse response;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static ClientResponse listar_reporte(string remitente, string FechaInicial, string FechaFinal, int paginaActual, int RegistroXpagina)
        {
            ClientResponse response;
            try
            {
                using (LogPromoDAO dbLog = new LogPromoDAO())
                {
                    DateTime? dFechaInicial = null;
                    if (FechaInicial != "")
                    {
                        dFechaInicial = Convert.ToDateTime(FechaInicial);
                    }

                    DateTime? dFechaFinal = null;
                    if (FechaFinal != "")
                    {
                        dFechaFinal = Convert.ToDateTime(FechaFinal);
                    }

                    response = dbLog.getLstLogPromo(remitente, dFechaInicial, dFechaFinal, paginaActual, RegistroXpagina);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }

    }
}