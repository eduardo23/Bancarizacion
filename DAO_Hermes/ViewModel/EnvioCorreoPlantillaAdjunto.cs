using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Hermes.ViewModel
{
    public class EnvioCorreoPlantillaAdjunto
    {

        private int _id;
        private string _nombre_archivo;
        private string _ruta_archivo;
        private int _id_estado;
        private string _fec_registro;
        public int id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }
        public string nombre_archivo
        {
            get { return _nombre_archivo; }
            set
            {
                _nombre_archivo = value;
            }
        }
        public string ruta_archivo
        {
            get { return _ruta_archivo; }
            set
            {
                _ruta_archivo = value;
            }
        }
        public int id_estado
        {
            get { return _id_estado; }
            set
            {
                _id_estado = value;
            }
        }
        public string fec_registro
        {
            get { return _fec_registro; }
            set
            {
                _fec_registro = value;
            }
        }
    }
}
