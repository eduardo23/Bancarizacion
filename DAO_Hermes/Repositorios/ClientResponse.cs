using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Hermes.Repositorios
{
   public class ClientResponse
    {
        public object Data { get; set; }
        public string DataJson { get; set; }
        public object Errores { get; set; }
        public string Mensaje { get; set; }
        public string Status { get; set; }
        public int StatusCode { get; set; }
        public string ViewResult { get; set; }
        public object paginacion { get; set; }
    }
}
