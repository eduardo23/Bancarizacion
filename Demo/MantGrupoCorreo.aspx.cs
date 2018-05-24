using DAO_Hermes.Repositorios;
using DAO_Hermes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Demo
{
    public partial class MantGrupoCorre : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static ClientResponse getListParametrosMaestro(string skey)
        {
            ClientResponse response;
            try
            {
                using (ParametrosMaestrosDAO dbParametrosMaestro = new ParametrosMaestrosDAO())
                {
                    response = dbParametrosMaestro.getLstParametroMaestro(skey);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }
        [WebMethod]
        public static ClientResponse getListGrupoCorreoXOrigen(int origen)
        {
            ClientResponse response;
            try
            {
                using (GrupoCorreoDAO dbGrupoCorreo = new GrupoCorreoDAO())
                {
                    response = dbGrupoCorreo.getGrupoCorreoXOrigen(origen);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }
        [WebMethod]
        public static ClientResponse getListGrupoCorreo(int paginaActual, int RegistroXpagina)
        {
            ClientResponse response;
            try
            {
                using (GrupoCorreoDAO dbGrupoCorreo = new GrupoCorreoDAO())
                {
                    response = dbGrupoCorreo.getLstGrupoCorreo(paginaActual, RegistroXpagina);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }

        [WebMethod]
        public static ClientResponse ActualiarGrupoCorreo(GrupoCorreo objeto)
        {
            ClientResponse response;
            try
            {
                using (GrupoCorreoDAO dbGrupoCorreo = new GrupoCorreoDAO())
                {
                    response = dbGrupoCorreo.ActualiarGrupoCorreo(objeto);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }
        [WebMethod]
        public static ClientResponse EliminarGrupoCorreo(GrupoCorreo objeto)
        {
            ClientResponse response;
            try
            {
                using (GrupoCorreoDAO dbGrupoCorreo = new GrupoCorreoDAO())
                {
                    response = dbGrupoCorreo.EliminarGrupoCorreo(objeto);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }

        [WebMethod]
        public static ClientResponse InsertGrupoCorreo(GrupoCorreo objeto)
        {
            ClientResponse response;
            try
            {
                using (GrupoCorreoDAO dbGrupoCorreo = new GrupoCorreoDAO())
                {
                    response = dbGrupoCorreo.InsertGrupoCorreo(objeto);
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