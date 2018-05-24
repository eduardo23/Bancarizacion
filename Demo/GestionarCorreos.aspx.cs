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
    public partial class GestionarCorreos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static ClientResponse getGrupoCorreoCombo()
        {
            ClientResponse response;
            try
            {
                using (GrupoCorreoDAO dbGrupoCorreo = new GrupoCorreoDAO())
                {
                    response = dbGrupoCorreo.getGrupoCorreoCombo();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
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
        public static ClientResponse getListGestionCorreo(int paginaActual, int RegistroXpagina, int id_cbo_grupo_consultar)
        {
            ClientResponse response;
            try
            {
                using (GestionCorreoDAO dbGestionCorreo = new GestionCorreoDAO())
                {
                    response = dbGestionCorreo.getLstGestionCorreo(paginaActual, RegistroXpagina, id_cbo_grupo_consultar);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }
        [WebMethod]
        public static ClientResponse ActualiarGestionCorreo(GestionCorreo objeto)
        {
            ClientResponse response;
            try
            {
                using (GestionCorreoDAO dbGestionCorreo = new GestionCorreoDAO())
                {
                    response = dbGestionCorreo.ActualiarGestionCorreo(objeto);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }
        [WebMethod]
        public static ClientResponse EliminarGestionCorreo(GestionCorreo objeto)
        {
            ClientResponse response;
            try
            {
                using (GestionCorreoDAO dbGestionCorreo = new GestionCorreoDAO())
                {
                    response = dbGestionCorreo.EliminarGestionCorreo(objeto);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }
        [WebMethod]
        public static ClientResponse InsertGestionCorreo(GestionCorreo objeto)
        {
            ClientResponse response;
            try
            {
                using (GestionCorreoDAO dbGestionCorreo = new GestionCorreoDAO())
                {
                    response = dbGestionCorreo.InsertGestionCorreo(objeto);
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