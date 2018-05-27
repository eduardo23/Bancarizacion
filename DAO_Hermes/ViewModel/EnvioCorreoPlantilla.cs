using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Hermes.ViewModel
{
    public class EnvioCorreoPlantilla
    {

        private int _id;
        private string _asunto;
        private int _id_origen;
        private Plantilla plantilla;
        private int _id_estado;
        private string _fec_registro;


        public EnvioCorreoPlantilla()
        {
            plantilla = new Plantilla();
        }
        public int id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }
        public string asunto
        {
            get { return _asunto; }
            set
            {
                _asunto = value;
            }
        }
        public int id_origen
        {
            get { return _id_origen; }
            set
            {
                _id_origen = value;
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
