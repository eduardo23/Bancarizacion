using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace DAO_Hermes.Repositorios
{
   public class ConexionDAO
    {
        public static string cnx = ConfigurationManager.ConnectionStrings["BDHermesBancarizacion_Entitiess"].ConnectionString;
    }
}
