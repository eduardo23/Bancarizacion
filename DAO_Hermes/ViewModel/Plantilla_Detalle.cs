using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Hermes.ViewModel
{
    public class Plantilla_Detalle
    {
        public int _id { get; set; }
        public string _NombreArchivoImagen { get; set; }
        public string _ruta_imagen { get; set; }
        public int _id_estado { get; set; }
        public string _fec_reg { get; set; }

        public int _fl_nuevo { get; set; } //fl_nuevo : 0 Nuevo fl_nuevo: 1 registro existente en la bd
        public int fl_nuevo
        {
            get { return _fl_nuevo; }
            set
            {
                _fl_nuevo = value;
            }
        }
        public int id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }
        public string fec_reg
        {
            get { return _fec_reg; }
            set
            {
                _fec_reg = value;
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
        public string ruta_imagen
        {
            get { return _ruta_imagen; }
            set
            {
                _ruta_imagen = value;
            }
        }
        public string NombreArchivoImagen
        {
            get { return _NombreArchivoImagen; }
            set
            {
                _NombreArchivoImagen = value;
            }
        }

    }
}
