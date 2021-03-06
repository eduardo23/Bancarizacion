﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Hermes.ViewModel
{
    public class Plantilla
    {
        public int _id;
        public string _descripcion;
        public string _NombreArchivoHtml;
        public string _ruta_plantilla_html;
        public int _id_estado;
        public string _fec_reg;
        public int _fl_parrafo;
        public int _fl_nuevo; //fl_nuevo : 0 Nuevo fl_nuevo: 1 registro existente en la bd

        public int fl_parrafo
        {
            get { return _fl_parrafo; }
            set
            {
                _fl_parrafo = value;
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

        public string ruta_plantilla_html
        {
            get { return _ruta_plantilla_html; }
            set
            {
                _ruta_plantilla_html = value;
            }
        }
        public string NombreArchivoHtml
        {
            get { return _NombreArchivoHtml; }
            set
            {
                _NombreArchivoHtml = value;
            }
        }

        public string descripcion
        {
            get { return _descripcion; }
            set
            {
                _descripcion = value;
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

        public List<Plantilla_Detalle> list_plantilla_detalle;
        public Plantilla()
        {
            list_plantilla_detalle = new List<Plantilla_Detalle>();
        }
    }
}
