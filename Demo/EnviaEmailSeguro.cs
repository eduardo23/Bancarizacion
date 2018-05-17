using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo
{
    public class EnviaEmailSeguro
    {
        public string Para { get; set; }
        public string Asunto { get; set; }
        public string NombreCompleto { get; set; }
        public string Codigo { get; set; }
        public string Prima { get; set; }
        public string aseguradora { get; set; }
        public string Producto { get; set; }
        public string asegurado { get; set; }
        public string beneficiario { get; set; }
        public string Usuario { get; set; }

        public int codMoneda { get; set; }
        public int idAsegurado { get; set; }

    }
}