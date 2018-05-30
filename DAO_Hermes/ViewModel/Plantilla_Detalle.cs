using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Hermes.ViewModel
{
    public class Plantilla_Detalle
    {
        public int _id;
        public string _NombreArchivoImagen;
        public string _ruta_imagen;
        public string _ruta_site_imagen;
        public int _id_estado;
        public string _fec_reg;
        public int _fl_nuevo;//fl_nuevo : 0 Nuevo fl_nuevo: 1 registro existente en la bd

        public string ruta_site_imagen
        {
            get { return _ruta_site_imagen; }
            set
            {
                _ruta_site_imagen = value;
            }
        }
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
