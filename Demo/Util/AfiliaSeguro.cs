using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAO_Hermes;
using DAO_Hermes.Repositorios;


namespace Demo.Util
{
    public class AfiliaSeguroAccidentes
    {
        public Alumno Alumno { get; set; }
        public AfiliacionSeguro AfiliacionSeguro { get; set; }

        public string AlumnoAfiliado { get; set; }

        public int CodigoLibreID { get; set; }
        public string CodigoLibre { get; set; }
        public string Producto { get; set; }
        public string TipoMoneda { get; set; }
        public string Prima { get; set; }
        public string Aseguradora { get; set; }
        public int MonedaId { get; set; }
        public string NombreColegio { get; set; }

        public int IdProducto { get; set; }
        public int IdAsociacion { get; set; }
    }


    public class AfiliaSeguroRenta
    {
        public int Id { get; set; }
        public Alumno Alumno { get; set; }
        public string AlumnoAfiliado { get; set; }
        public Padre Padre { get; set; }
        public AfiliacionSeguro AfiliacionSeguro { get; set; }
        public int CodigoLibreID { get; set; }
        public string CodigoLibre { get; set; }
        public string Producto { get; set; }
        public string TipoMoneda { get; set; }
        public string Prima { get; set; }
        public string Aseguradora { get; set; }
        public int MonedaId { get; set; }
        public string NombreColegio { get; set; }
        public int IdProducto { get; set; }
        public int IdAsociacion { get; set; }
    }

    public class AfiliaSeguroOncologico
    {
        public int Id { get; set; }
        public Alumno Alumno { get; set; }
        public string AlumnoAfiliado { get; set; }
        public Padre Padre { get; set; }
        public AfiliacionSeguro AfiliacionSeguro { get; set; }
        public int CodigoLibreID { get; set; }
        public string CodigoLibre { get; set; }
        public string Producto { get; set; }
        public string TipoMoneda { get; set; }
        public string Prima { get; set; }
        public string Aseguradora { get; set; }
        public int MonedaId { get; set; }
        public string NombreColegio { get; set; }
        public int IdProducto { get; set; }
        public int IdAsociacion { get; set; }

        //public string AsegMayordeEdad { get; set; }
        public PersonaDatosAdic PersonaDatosAdic { get; set; }
        public PersonaPreg PersonaPreg { get; set; }
    }

    //public class AfiliaSeguroRenta
    //{
    //    public int Id { get; set; }
    //    public Alumno Alumno { get; set; }
    //    public string AlumnoAfiliado { get; set; }
    //    public  Padre Padre  { get; set; }
    //    public AfiliacionSeguro AfiliacionSeguro { get; set; }
    //    public int CodigoLibreID { get; set; }
    //    public string CodigoLibre { get; set; }
    //    public string Producto { get; set; }
    //    public string TipoMoneda { get; set; }
    //    public string Prima { get; set; }
    //    public string Aseguradora { get; set; }
    //    public int MonedaId { get; set; }
    //    public string NombreColegio { get; set; }
    //    public int IdProducto { get; set; }
    //    public int IdAsociacion { get; set; }
    //}

}