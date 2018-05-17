using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAO_Hermes.Repositorios
{
    public class Alumno_DAO : IDisposable
    {
        public int AgregarAfiliadoAccidentes(Alumno alumno, AfiliacionSeguro AFIL_SEGURO, int CodigoID, string UsuarioAfiliacion,int asociacionid)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //Guardar Alumno
                        alumno.asociacionid = asociacionid;
                        db.Alumno.Add(alumno);
                        db.SaveChanges();
                        //Guardar Afiliacion Seguro
                        AfiliacionSeguroAlumno AFIL_SEG_ALU = new AfiliacionSeguroAlumno();

                        AFIL_SEG_ALU.AlumnoID = alumno.ID;
                        AFIL_SEG_ALU.idasociacion =asociacionid;
                        AFIL_SEG_ALU.fechaCreacion = DateTime.Now;
                        AFIL_SEG_ALU.AfiliacionSeguroID = alumno.ID;
                        AFIL_SEGURO.FechaCreacion = DateTime.Now;
                        AFIL_SEGURO.asociaciacionId = asociacionid;
                        db.AfiliacionSeguro.Add(AFIL_SEGURO);
                        db.SaveChanges();
                        //Guardar Afiliacion Seguro Alumno
                        AFIL_SEG_ALU.AfiliacionSeguroID = AFIL_SEGURO.ID;
                        db.AfiliacionSeguroAlumno.Add(AFIL_SEG_ALU);

                        db.SaveChanges();

                        using (CodigoDAO dbCodigo = new CodigoDAO())
                        {
                            
                            //CodigoDetalle codDet = new CodigoDetalle();
                            //codDet.AfiliacionSeguroAlumnoID = AFIL_SEG_ALU.ID;
                            //codDet.AfiliacionSeguroID = AFIL_SEGURO.ID;
                            //codDet.ID = CodigoID;
                            //codDet.Descripcion = (alumno.ApellidoPaternno + " " + alumno.ApellidoMaterno + " " + alumno.Nombre).ToUpper();
                            CodigoDetalle codigo = db.CodigoDetalle.Where(p => p.ID == CodigoID).Single();
                            codigo.AfiliacionSeguroID = AFIL_SEGURO.ID;
                            codigo.UsuarioAfiliacion = UsuarioAfiliacion;
                            codigo.AfiliacionSeguroAlumnoID = AFIL_SEG_ALU.ID;
                            codigo.Descripcion = alumno.ApellidoPaternno + " " + alumno.ApellidoMaterno + " " + alumno.Nombre;
                            db.SaveChanges();
                        }
                        //Confirmar grabacion
                        dbContextTransaction.Commit();
                        return 1;
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        return -1;
                    }
                }
            }
        }



        public int Agregar(Alumno alumno)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                if (ExisteAlumno(Convert.ToInt32(alumno.TipoDocumentoID), alumno.NumeroDocumento) == false)
                {
                    db.Alumno.Add(alumno);
                }
                else
                {
                    Alumno alu1 = db.Alumno.Where(p => p.TipoDocumentoID == alumno.TipoDocumentoID && p.NumeroDocumento == alumno.NumeroDocumento).FirstOrDefault();

                    alu1.Nombre = alumno.Nombre;
                    alu1.ApellidoPaternno = alumno.ApellidoPaternno.ToUpper();
                    alu1.ApellidoMaterno = alumno.ApellidoMaterno.ToUpper();
                    alu1.FechaNacimiento = alumno.FechaNacimiento;
                    alu1.GradoID = alumno.GradoID;
                    alu1.Seccion = alumno.Seccion;
                    alu1.Sexo = alumno.Sexo;
                    alu1.FechaCreacion = DateTime.Now;
                    alu1.TipoDocumentoID = alumno.TipoDocumentoID;
                    alu1.NumeroDocumento = alumno.NumeroDocumento;
                }
                return db.SaveChanges();
            }
        }
        //public int AgregarAfiliadoAccidentes(Alumno alumno, AfiliacionSeguro AFIL_SEGURO, int CodigoID)
        //        {
        //            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
        //            {
        //                using (var dbContextTransaction = db.Database.BeginTransaction())
        //                {
        //                    try
        //                    {
        //                        //Guardar Alumno
        //                        db.Alumno.Add(alumno);
        //                        db.SaveChanges();
        //                        //Guardar Afiliacion Seguro
        //                        AfiliacionSeguroAlumno AFIL_SEG_ALU = new AfiliacionSeguroAlumno();
        //                        AFIL_SEG_ALU.AlumnoID = alumno.ID;

        //                        db.AfiliacionSeguro.Add(AFIL_SEGURO);
        //                        db.SaveChanges();
        //                        //Guardar Afiliacion Seguro Alumno
        //                        AFIL_SEG_ALU.AfiliacionSeguroID = AFIL_SEGURO.ID;
        //                        db.AfiliacionSeguroAlumno.Add(AFIL_SEG_ALU);
        //                        db.SaveChanges();

        //                        using (CodigoDAO dbCodigo = new CodigoDAO())
        //                        {
        //                            CodigoDetalle codDet = new CodigoDetalle();
        //                            codDet.AfiliacionSeguroAlumnoID = AFIL_SEG_ALU.ID;
        //                            codDet.AfiliacionSeguroID = AFIL_SEGURO.ID;
        //                            codDet.ID = CodigoID;

        //                            CodigoDetalle codigo = db.CodigoDetalle.Where(p => p.ID == codDet.ID).Single();
        //                            codigo.AfiliacionSeguroID = codDet.AfiliacionSeguroID;
        //                            codigo.AfiliacionSeguroAlumnoID = codDet.AfiliacionSeguroAlumnoID;
        //                        }
        //                        //Confirmar grabacion
        //                        db.SaveChanges();
        //                        dbContextTransaction.Commit();
        //                        return 1;
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        dbContextTransaction.Rollback();
        //                        return -1;
        //                    }
        //                }
        //            }
        //        }
        public int? AgregarAfiliadoCargaAccidentesPacifico(Codigo codigo, List<Alumno> alumnos, string usuario,
                                                          int InstitucionEducativaId,
                                                          int Idproducto, string Descripcion, int? CantidadActual,
                                                          int? correlativo, bool tienecodigo, int codigoid, int asociacionID)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //int? Correlativo = 0;
                        string codInsED = "";
                        string codRel = "";
                        string SegProd = "";
                        string CodGenerado = "";
                        bool edit = false;
                        var cantCodGen = 0;

                        int? codigoNuevo = 0;
                        //Guardar Codigo
                        if (tienecodigo == false)
                        {
                            db.Codigo.Add(codigo);
                        }
                        else
                        {
                            bool existe = (db.Codigo.Where(p => p.ID == codigoid).Count() > 0);
                            if (existe == true)
                            {
                                var cod = db.Codigo.Where(p => p.ID == codigoid).Single();
                                correlativo = cod.Cantidad + 1;
                                cod.Cantidad = CantidadActual;
                                cantCodGen = cod.ID;
                                edit = true;
                            }
                        }
                        db.SaveChanges();
                        //Guardar Alumnos
                        foreach (Alumno alumno in alumnos)
                        {
                            alumno.asociacionid = asociacionID;
                            db.Alumno.Add(alumno);
                            db.SaveChanges();
                            //Guardar Afiliacion Seguro Alumno                                                
                            AfiliacionSeguro AFIL_SEGURO = new AfiliacionSeguro();
                            AFIL_SEGURO.asociaciacionId = asociacionID;
                            AFIL_SEGURO.InstitucionEducativaID = InstitucionEducativaId;
                            AFIL_SEGURO.Estado = true;
                            AFIL_SEGURO.FechaCreacion = DateTime.Now;
                            AFIL_SEGURO.UsuarioCreacion = usuario;
                            AFIL_SEGURO.CodigoPago = "0";
                            //Guardar Afiliacion Seguro
                            db.AfiliacionSeguro.Add(AFIL_SEGURO);
                            db.SaveChanges();
                            AfiliacionSeguroAlumno AFIL_SEG_ALU = new AfiliacionSeguroAlumno();
                            AFIL_SEG_ALU.AlumnoID = alumno.ID;
                            AFIL_SEG_ALU.idasociacion =asociacionID;
                            AFIL_SEG_ALU.fechaCreacion = DateTime.Now;
                            AFIL_SEG_ALU.AfiliacionSeguroID = AFIL_SEGURO.ID;
                            db.AfiliacionSeguroAlumno.Add(AFIL_SEG_ALU);
                            db.SaveChanges();
                            //Guardar seguro alumno
                            //Generar Codigo
                            //Codigo detalle

                            codInsED = codigo.InstitucionEducativaID.ToString().PadLeft(5, '0');
                            codRel = correlativo.ToString().PadLeft(5, '0');
                            SegProd = codigo.CIASeguroID.ToString().PadLeft(2, '0') + codigo.ProductoID.ToString().PadLeft(2, '0');
                            CodGenerado = codInsED + codRel + SegProd;
                            // 
                            CodigoDetalle codDet = new CodigoDetalle();
                            codDet.AfiliacionSeguroAlumnoID = AFIL_SEG_ALU.ID;
                            codDet.AfiliacionSeguroID = AFIL_SEGURO.ID;
                            codDet.Codigo = CodGenerado;
                            codDet.Descripcion = (alumno.ApellidoPaternno + " " + alumno.ApellidoMaterno + " " + alumno.Nombre).ToUpper();
                            if (edit == true)
                            {
                                codDet.CodigoID = cantCodGen;
                                codigoNuevo = codDet.CodigoID;
                            }
                            else
                            {
                                codDet.CodigoID = codigo.ID;
                                codigoNuevo = codDet.CodigoID;
                            }
                            codDet.Descripcion = correlativo.ToString().PadLeft(5, '0');
                            codDet.Correlativo = correlativo;

                            codDet.IsPagado = false;
                            codDet.ProductoID = Idproducto;
                            codDet.TipoCarga = true;
                            codDet.Activo = true;
                            codDet.FechaCreacion = DateTime.Now.Date;
                            codDet.UsuarioCreacion = usuario;
                            //codDet.AfiliacionSeguroAlumnoID = codDet.AfiliacionSeguroAlumnoID;                           
                            db.CodigoDetalle.Add(codDet);
                            db.SaveChanges();
                            correlativo += 1;
                        }
                        //Confirmar grabacion                        
                        dbContextTransaction.Commit();
                        return codigoNuevo;
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        return -1;
                    }
                }
            }
        }

        public int? AgregarAfiliadoCargaAccidentesRimac(Codigo codigo, List<Alumno> alumnos, string usuario, int InstitucionEducativaId,
                                                                                              int Idproducto, string Descripcion, int? CantidadActual, int? correlativo, bool tienecodigo, int codigoid, int asociacionId)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //int? Correlativo = 0;
                        string codInsED = "";
                        string codRel = "";
                        string SegProd = "";
                        string CodGenerado = "";
                        bool edit = false;
                        var cantCodGen = 0;

                        int? codigoNuevo = 0;
                        //Guardar Codigo
                        if (tienecodigo == false)
                        {
                            db.Codigo.Add(codigo);
                        }
                        else
                        {
                            bool existe = (db.Codigo.Where(p => p.ID == codigoid).Count() > 0);
                            if (existe == true)
                            {
                                var cod = db.Codigo.Where(p => p.ID == codigoid).Single();
                                correlativo = cod.Cantidad + 1;
                                cod.Cantidad = CantidadActual;
                                cantCodGen = cod.ID;
                                edit = true;
                            }
                        }
                        //Guardar Alumnos
                        foreach (Alumno alumno in alumnos)
                        {
                            alumno.asociacionid = asociacionId;
                            db.Alumno.Add(alumno);
                            db.SaveChanges();
                            //Guardar Afiliacion Seguro Alumno                                                
                            AfiliacionSeguro AFIL_SEGURO = new AfiliacionSeguro();
                            AFIL_SEGURO.InstitucionEducativaID = InstitucionEducativaId;
                            AFIL_SEGURO.Estado = true;
                            AFIL_SEGURO.asociaciacionId = asociacionId;
                            AFIL_SEGURO.FechaCreacion = DateTime.Now;
                            AFIL_SEGURO.UsuarioCreacion = usuario;
                            AFIL_SEGURO.CodigoPago = "0";
                            //Guardar Afiliacion Seguro
                            db.AfiliacionSeguro.Add(AFIL_SEGURO);
                            db.SaveChanges();
                            AfiliacionSeguroAlumno AFIL_SEG_ALU = new AfiliacionSeguroAlumno();
                            AFIL_SEG_ALU.AlumnoID = alumno.ID;
                            AFIL_SEG_ALU.AfiliacionSeguroID = AFIL_SEGURO.ID;
                            AFIL_SEG_ALU.idasociacion = asociacionId;
                            db.AfiliacionSeguroAlumno.Add(AFIL_SEG_ALU);
                            db.SaveChanges();
                            //Guardar seguro alumno
                            //Generar Codigo
                            //Codigo detalle

                            codInsED = codigo.InstitucionEducativaID.ToString().PadLeft(5, '0');
                            codRel = correlativo.ToString().PadLeft(5, '0');
                            SegProd = codigo.CIASeguroID.ToString().PadLeft(2, '0') + codigo.ProductoID.ToString().PadLeft(2, '0');
                            CodGenerado = codInsED + codRel + SegProd;
                            // 
                            CodigoDetalle codDet = new CodigoDetalle();
                            codDet.AfiliacionSeguroAlumnoID = AFIL_SEG_ALU.ID;
                            codDet.AfiliacionSeguroID = AFIL_SEGURO.ID;
                            codDet.Codigo = CodGenerado;
                            codDet.Descripcion= (alumno.ApellidoPaternno + " " + alumno.ApellidoMaterno + " " + alumno.Nombre).ToUpper();
                            codDet.UsuarioCreacion = alumno.UsuarioCreacion;
                            if (edit == true)
                            {
                                codDet.CodigoID = cantCodGen;
                                codigoNuevo = codDet.CodigoID;
                            }
                            else
                            {
                                codDet.CodigoID = codigo.ID;
                                codigoNuevo = codDet.CodigoID;
                            }
                            codDet.Descripcion = correlativo.ToString().PadLeft(5, '0');
                            codDet.Correlativo = correlativo;

                            codDet.IsPagado = false;
                            codDet.ProductoID = Idproducto;
                            codDet.TipoCarga = true;
                            codDet.Activo = true;
                            codDet.Descripcion = (alumno.ApellidoPaternno + " " + alumno.ApellidoMaterno + " " + alumno.Nombre).ToUpper();
                            codDet.FechaCreacion = DateTime.Now.Date;
                            codDet.UsuarioCreacion = usuario;
                            //codDet.AfiliacionSeguroAlumnoID = codDet.AfiliacionSeguroAlumnoID;                           
                            db.CodigoDetalle.Add(codDet);
                            db.SaveChanges();
                            correlativo += 1;
                        }
                        //Confirmar grabacion                        
                        dbContextTransaction.Commit();
                        return codigoNuevo;
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        return -1;
                    }
                }
            }
        }

        public int? AgregarAfiliadoCargaAccidentesPositiva(Codigo codigo, List<Alumno> alumnos, string usuario, int InstitucionEducativaId,
                                                                                              int Idproducto, string Descripcion, int? CantidadActual, int? correlativo, bool tienecodigo, int codigoid)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //int? Correlativo = 0;
                        string codInsED = "";
                        string codRel = "";
                        string SegProd = "";
                        string CodGenerado = "";
                        bool edit = false;
                        var cantCodGen = 0;

                        int? codigoNuevo = 0;
                        //Guardar Codigo
                        if (tienecodigo == false)
                        {
                            db.Codigo.Add(codigo);
                        }
                        else
                        {
                            bool existe = (db.Codigo.Where(p => p.ID == codigoid).Count() > 0);
                            if (existe == true)
                            {
                                var cod = db.Codigo.Where(p => p.ID == codigoid).Single();
                                correlativo = cod.Cantidad + 1;
                                cod.Cantidad = CantidadActual;
                                cantCodGen = cod.ID;
                                edit = true;
                            }
                        }
                        db.SaveChanges();
                        //Guardar Alumnos                        
                        foreach (Alumno alumno in alumnos)
                        {
                            db.Alumno.Add(alumno);
                            db.SaveChanges();

                            //Guardar Afiliacion Seguro Alumno                                                
                            AfiliacionSeguro AFIL_SEGURO = new AfiliacionSeguro();
                            AFIL_SEGURO.InstitucionEducativaID = InstitucionEducativaId;
                            AFIL_SEGURO.Estado = true;
                            AFIL_SEGURO.FechaCreacion = DateTime.Now;
                            AFIL_SEGURO.UsuarioCreacion = usuario;
                            AFIL_SEGURO.CodigoPago = "0";
                            //Guardar Afiliacion Seguro
                            db.AfiliacionSeguro.Add(AFIL_SEGURO);
                            db.SaveChanges();
                            AfiliacionSeguroAlumno AFIL_SEG_ALU = new AfiliacionSeguroAlumno();
                            AFIL_SEG_ALU.AlumnoID = alumno.ID;
                            AFIL_SEG_ALU.AfiliacionSeguroID = AFIL_SEGURO.ID;
                            db.AfiliacionSeguroAlumno.Add(AFIL_SEG_ALU);
                            db.SaveChanges();
                            //Guardar seguro alumno
                            //Generar Codigo
                            //Codigo detalle

                            codInsED = codigo.InstitucionEducativaID.ToString().PadLeft(5, '0');
                            codRel = correlativo.ToString().PadLeft(5, '0');
                            SegProd = codigo.CIASeguroID.ToString().PadLeft(2, '0') + codigo.ProductoID.ToString().PadLeft(2, '0');
                            CodGenerado = codInsED + codRel + SegProd;
                            // 
                            CodigoDetalle codDet = new CodigoDetalle();

                            codDet.AfiliacionSeguroAlumnoID = AFIL_SEG_ALU.ID;
                            codDet.AfiliacionSeguroID = AFIL_SEGURO.ID;
                            codDet.Codigo = CodGenerado;
                            codDet.Descripcion= (alumno.ApellidoPaternno + " " + alumno.ApellidoMaterno + " " + alumno.Nombre).ToUpper();
                            codDet.UsuarioCreacion = usuario;
                            codDet.FechaCreacion = DateTime.Now.Date;
                            if (edit == true)
                            {
                                codDet.CodigoID = cantCodGen;
                                codigoNuevo = codDet.CodigoID;
                            }
                            else
                            {
                                codDet.CodigoID = codigo.ID;
                                codigoNuevo = codDet.CodigoID;
                            }
                            codDet.Descripcion = correlativo.ToString().PadLeft(5, '0');
                            codDet.Correlativo = correlativo;

                            codDet.IsPagado = false;
                            codDet.ProductoID = Idproducto;
                            codDet.TipoCarga = true;
                            codDet.Activo = true;
                            codDet.Descripcion = (alumno.ApellidoPaternno + " " + alumno.ApellidoMaterno + " " + alumno.Nombre).ToUpper();
                            codDet.FechaCreacion = DateTime.Now.Date;
                            codDet.UsuarioCreacion = usuario;
                            //codDet.AfiliacionSeguroAlumnoID = codDet.AfiliacionSeguroAlumnoID;                           
                            db.CodigoDetalle.Add(codDet);
                            db.SaveChanges();
                            correlativo += 1;
                        }
                        //Confirmar grabacion                        
                        dbContextTransaction.Commit();
                        return codigoNuevo;
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        return -1;
                    }
                }
            }
        }
        public int AgregarAfiliadoOnco(Alumno alumno, Padre padre, PersonaDatosAdic datosadic, PersonaPreg preg, AfiliacionSeguro AFIL_SEGURO, int CodigoID, string UsuarioAfiliacion)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.AfiliacionSeguro.Add(AFIL_SEGURO);
                        db.SaveChanges();

                        //Guardar Alumno
                        db.Alumno.Add(alumno);
                        db.SaveChanges();

                        int nAfiliacionSeguroPadreID = 0;
                        int nAfiliacionSeguroAlumnoID = 0;
                        int nPersonaTipoId = 0;
                        int nPersonaId = 0;

                        int PersonaTipoIdPadre = 2;  //Padre, Apoderado de alumno menor de edad
                        int PersonaTipoIdAlumno = 1; //Alumno, mayor de edad
                        if (padre != null) {
                            padre.BeneficiarioID = alumno.ID;
                            db.Padre.Add(padre);
                            db.SaveChanges();

                            AfiliacionSeguroPadre AFIL_SEG_PADRE = new AfiliacionSeguroPadre();
                            AFIL_SEG_PADRE.PadreID = padre.ID;
                            AFIL_SEG_PADRE.AfiliacionSeguroID = AFIL_SEGURO.ID;
                            AFIL_SEG_PADRE.FechaCreacion= DateTime.Now;
                            AFIL_SEG_PADRE.asociacionID = alumno.asociacionid;
                            db.AfiliacionSeguroPadre.Add(AFIL_SEG_PADRE);
                            db.SaveChanges();
                            nAfiliacionSeguroPadreID = AFIL_SEG_PADRE.ID;
                            //
                            nPersonaTipoId = PersonaTipoIdPadre; //2; //Padre, Apoderado de alumno menor de edad
                            nPersonaId = padre.ID; //Los datos adicionales son del padre
                        }
                        else
                        {
                            AfiliacionSeguroAlumno AFIL_SEG_ALU = new AfiliacionSeguroAlumno();
                            AFIL_SEG_ALU.AlumnoID = alumno.ID;                            
                            AFIL_SEG_ALU.fechaCreacion = DateTime.Now;
                            AFIL_SEG_ALU.AfiliacionSeguroID = AFIL_SEGURO.ID;
                            AFIL_SEG_ALU.idasociacion = alumno.asociacionid;
                            db.AfiliacionSeguroAlumno.Add(AFIL_SEG_ALU);
                            db.SaveChanges();
                            nAfiliacionSeguroAlumnoID = AFIL_SEG_ALU.ID;
                            //
                            nPersonaTipoId = PersonaTipoIdAlumno; // 1;     //Alumno, mayor de edad
                            nPersonaId = alumno.ID; //Los datos adicionales son del alumno
                        }

                        PersonaDatosAdic DatosAdic = new PersonaDatosAdic();
                        datosadic.PersonaTipoId = nPersonaTipoId;
                        datosadic.PersonaId = nPersonaId;
                        db.PersonaDatosAdic.Add(datosadic);
                        db.SaveChanges();

                        PersonaPreg Preg = new PersonaPreg();
                        preg.PersonaTipoId = PersonaTipoIdAlumno;
                        preg.PersonaId = alumno.ID;
                        db.PersonaPreg.Add(preg);
                        db.SaveChanges();

                        using (CodigoDAO dbCodigo = new CodigoDAO())
                        {
                            CodigoDetalle codigo = db.CodigoDetalle.Where(p => p.ID == CodigoID).Single();
                            codigo.AfiliacionSeguroID = AFIL_SEGURO.ID;

                            if (nAfiliacionSeguroPadreID != 0)
                            {
                                codigo.AfiliacionSeguroPadreID = nAfiliacionSeguroPadreID;
                            }
                            if (nAfiliacionSeguroAlumnoID != 0)
                            {
                                codigo.AfiliacionSeguroAlumnoID = nAfiliacionSeguroAlumnoID;
                            }                            
                            codigo.FechaActualizacion = DateTime.Now;
                            codigo.UsuarioActualizacion = UsuarioAfiliacion;
                            codigo.UsuarioAfiliacion = UsuarioAfiliacion;
                            codigo.Descripcion = (alumno.ApellidoPaternno + " " + alumno.ApellidoMaterno + " " + alumno.Nombre).ToUpper();
                            db.SaveChanges();
                        }

                        dbContextTransaction.Commit();
                        return 1;
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        return -1;
                    }
                }
            }
        }

        public int AgregarAfiliadoRenta(Alumno alumno, Padre padre, AfiliacionSeguro AFIL_SEGURO, int CodigoID, string UsuarioAfiliacion)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //Guardar Alumno
                        db.Alumno.Add(alumno);
                        db.SaveChanges();
                        //Guardar Padres
                        int fila = 0;
                        //foreach (Padre padre in padres)
                        //{                    
                        padre.BeneficiarioID = alumno.ID;
                        db.Padre.Add(padre);
                        db.SaveChanges();
                        //Guardar Afiliacion Seguro
                        AfiliacionSeguroPadre AFIL_SEG_PADRE = new AfiliacionSeguroPadre();
                        AFIL_SEG_PADRE.PadreID = padre.ID;
                        db.AfiliacionSeguro.Add(AFIL_SEGURO);
                        db.SaveChanges();
                        //Guardar Afiliacion Seguro Alumno

                        AFIL_SEG_PADRE.AfiliacionSeguroID = AFIL_SEGURO.ID;
                        db.AfiliacionSeguroPadre.Add(AFIL_SEG_PADRE);
                        db.SaveChanges();

                        using (CodigoDAO dbCodigo = new CodigoDAO())
                        {                  
                            CodigoDetalle codigo = db.CodigoDetalle.Where(p => p.ID == CodigoID).Single();
                            codigo.AfiliacionSeguroID = AFIL_SEGURO.ID;
                            codigo.AfiliacionSeguroPadreID = AFIL_SEG_PADRE.ID;
                            codigo.FechaActualizacion = DateTime.Now;
                            codigo.UsuarioCreacion = padre.UsuarioCreacion;
                            codigo.UsuarioAfiliacion = UsuarioAfiliacion;
                            codigo.Descripcion = (padre.ApellidoPaterno + " " + padre.ApellidoMaterno + " " + padre.Nombre).ToUpper();
                            db.SaveChanges();
                            //codigo.UsuarioActualizacion=Session["Usuario"];
                        }
                        fila += 1;


                        dbContextTransaction.Commit();
                        return 1;
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        return -1;
                    }
                }
            }
        }

        public int AgregarAfiliadoRentaPadre2(Alumno alumno, Padre padre, AfiliacionSeguro AFIL_SEGURO, int CodigoID, 
            string UsuarioAfiliacion)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //Guardar Alumno
                        db.Alumno.Add(alumno);
                        db.SaveChanges();
                        //Guardar Padres
                        int fila = 0;
                        //foreach (Padre padre in padres)
                        //{
                        padre.BeneficiarioID = alumno.ID;
                        db.Padre.Add(padre);
                        db.SaveChanges();
                        //Guardar Afiliacion Seguro
                        AfiliacionSeguroPadre AFIL_SEG_PADRE = new AfiliacionSeguroPadre();
                        AFIL_SEG_PADRE.PadreID = padre.ID;
                        db.AfiliacionSeguro.Add(AFIL_SEGURO);
                        db.SaveChanges();
                        //Guardar Afiliacion Seguro Alumno

                        AFIL_SEG_PADRE.AfiliacionSeguroID = AFIL_SEGURO.ID;
                        db.AfiliacionSeguroPadre.Add(AFIL_SEG_PADRE);
                        db.SaveChanges();

                        using (CodigoDAO dbCodigo = new CodigoDAO())
                        {                            
                            CodigoDetalle codigo = db.CodigoDetalle.Where(p => p.ID == CodigoID).Single();
                            codigo.AfiliacionSeguroID = AFIL_SEGURO.ID;
                            codigo.AfiliacionSeguroPadreID = AFIL_SEG_PADRE.ID;
                            codigo.FechaActualizacion = DateTime.Now;
                            codigo.UsuarioActualizacion = padre.UsuarioCreacion;
                            codigo.Descripcion = (padre.ApellidoPaterno + " " + padre.ApellidoMaterno + " " + padre.Nombre).ToUpper();                          
                            codigo.UsuarioAfiliacion =UsuarioAfiliacion;
                        }
                        
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                        return 1;
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        return -1;
                    }
                }
            }
        }

        
        public int? AgregarAfiliadoCargaRentas(Codigo codigo, List<Alumno> alumnos, List<Padre> padres, string usuario,
                                                      int InstitucionEducativaId,
                                                      int Idproducto, string Descripcion, int? CantidadActual,
                                                      int? correlativo, bool tienecodigo, int codigoid ,int asociacionId)
        {

            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //int? Correlativo = 0;
                        string codInsED = "";
                        string codRel = "";
                        string SegProd = "";
                        string CodGenerado = "";
                        bool edit = false;
                        var cantCodGen = 0;

                        int? codigoNuevo = 0;
                        //Guardar Codigo
                        if (tienecodigo == false)
                        {
                            db.Codigo.Add(codigo);
                            db.SaveChanges();
                        }
                        else
                        {
                            bool existe = (db.Codigo.Where(p => p.ID == codigoid).Count() > 0);
                            if (existe == true)
                            {
                                Codigo cod = db.Codigo.Where(p => p.ID == codigoid).Single();
                                correlativo = cod.Cantidad + 1;
                                cod.Cantidad = CantidadActual;
                                cod.UsuarioActualizacion =usuario;
                                cod.FechaActualizacion = DateTime.Now;
                                cantCodGen = cod.ID;
                                edit = true;
                                db.SaveChanges();
                            }
                        }
                        //Guardar Alumnos
                        int i = 0;
                        foreach (Padre padre in padres)
                        {
                            //   alumnos[i].BeneficiarioID = padre.ID;
                            alumnos[i].asociacionid = asociacionId;
                            alumnos[i].FechaCreacion = DateTime.Now;
                            db.Alumno.Add(alumnos[i]);
                            db.SaveChanges();
                            padre.BeneficiarioID = alumnos[i].ID;
                            padre.asociacionID = asociacionId;
                            padre.FechaCreacion = DateTime.Now;
                            db.Padre.Add(padre);
                            db.SaveChanges();
                            //Guardar Afiliacion Seguro Alumno                                                
                            AfiliacionSeguro AFIL_SEGURO = new AfiliacionSeguro();
                            AFIL_SEGURO.InstitucionEducativaID = InstitucionEducativaId;
                            AFIL_SEGURO.Estado = true;
                            AFIL_SEGURO.asociaciacionId = asociacionId;
                            AFIL_SEGURO.FechaCreacion = DateTime.Now;
                            AFIL_SEGURO.UsuarioCreacion = usuario;
                            AFIL_SEGURO.CodigoPago = "0";
                            //Guardar Afiliacion Seguro
                            db.AfiliacionSeguro.Add(AFIL_SEGURO);
                            db.SaveChanges();
                            AfiliacionSeguroPadre AFIL_SEG_ALU = new AfiliacionSeguroPadre();
                            AFIL_SEG_ALU.PadreID = padre.ID;
                            AFIL_SEG_ALU.FechaCreacion = DateTime.Now;
                            AFIL_SEG_ALU.asociacionID=asociacionId;
                            AFIL_SEG_ALU.AfiliacionSeguroID = AFIL_SEGURO.ID;
                            db.AfiliacionSeguroPadre.Add(AFIL_SEG_ALU);
                            db.SaveChanges();
                            //Guardar seguro alumno
                            //Generar Codigo
                            //Codigo detalle
                            codInsED = codigo.InstitucionEducativaID.ToString().PadLeft(5, '0');
                            codRel = correlativo.ToString().PadLeft(5, '0');
                            SegProd = codigo.CIASeguroID.ToString().PadLeft(2, '0') + codigo.ProductoID.ToString().PadLeft(2, '0');
                            CodGenerado = codInsED + codRel + SegProd;
                            // 
                            CodigoDetalle codDet = new CodigoDetalle();
                            codDet.AfiliacionSeguroPadreID = AFIL_SEG_ALU.ID;
                            codDet.AfiliacionSeguroID = AFIL_SEGURO.ID;
                            codDet.Codigo = CodGenerado;
                            codDet.Descripcion = (padre.ApellidoPaterno + " " + padre.ApellidoMaterno + " " + padre.Nombre).ToUpper();
                            codDet.UsuarioCreacion = usuario;
                            codDet.FechaCreacion = DateTime.Now;
                            if (edit == true)
                            {
                                codDet.CodigoID = cantCodGen;
                                codigoNuevo = codDet.CodigoID;
                            }
                            else
                            {
                                codDet.CodigoID = codigo.ID;
                                codigoNuevo = codDet.CodigoID;
                            }
                            codDet.Descripcion = correlativo.ToString().PadLeft(5, '0');
                            codDet.Correlativo = correlativo;
                            codDet.IsPagado = false;
                            codDet.ProductoID = Idproducto;
                            codDet.TipoCarga = true;
                            codDet.Activo = true;
                                   codDet.Descripcion = (padre.ApellidoPaterno + " " + padre.ApellidoMaterno + " " + padre.Nombre).ToUpper();
                            codDet.FechaCreacion = DateTime.Now.Date;
                            codDet.UsuarioCreacion = usuario;
                            //codDet.AfiliacionSeguroAlumnoID = codDet.AfiliacionSeguroAlumnoID;                           
                     //       codDet.CodigoID = codigo.ID;
                            db.CodigoDetalle.Add(codDet);
                            db.SaveChanges();
                            correlativo += 1;
                            i = i + 1;                      
                        }
                       
                        //Confirmar grabacion                        
                        dbContextTransaction.Commit();
                        return codigoNuevo;
                    }

                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        return -1;
                    }
                }
            }
        }
                      
    public int AgregarAfiliadoRentas(Alumno alumno, List<Padre> padres, AfiliacionSeguro AFIL_SEGURO, int CodigoID, string Usuario,int asociacionID)
    {
        using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
        {
                int res = 0;
                using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                        //Guardar Alumno
                        alumno.FechaCreacion = DateTime.Now;
                        alumno.asociacionid = asociacionID;
                        db.Alumno.Add(alumno);
                        db.SaveChanges();
                    //Guardar Padres
                    int fila = 0;
                    foreach (Padre padre in padres)
                    {
                            padre.asociacionID = asociacionID;
                            padre.FechaCreacion = DateTime.Now;
                            padre.BeneficiarioID = alumno.ID;
                            db.Padre.Add(padre);
                            db.SaveChanges();
                        //Guardar Afiliacion Seguro
                        AfiliacionSeguroPadre AFIL_SEG_PADRE = new AfiliacionSeguroPadre();
                        AFIL_SEG_PADRE.PadreID = padre.ID;
                            AFIL_SEG_PADRE.FechaCreacion = DateTime.Now;
                            AFIL_SEG_PADRE.asociacionID = asociacionID;
                            AFIL_SEGURO.FechaCreacion = DateTime.Now;
                            AFIL_SEGURO.asociaciacionId = asociacionID;
                            db.AfiliacionSeguro.Add(AFIL_SEGURO);
                        db.SaveChanges();                        

                        AFIL_SEG_PADRE.AfiliacionSeguroID = AFIL_SEGURO.ID;
                        db.AfiliacionSeguroPadre.Add(AFIL_SEG_PADRE);
                        db.SaveChanges();

                        using (CodigoDAO dbCodigo = new CodigoDAO())
                        {
                            CodigoDetalle codDet = new CodigoDetalle();
                            codDet.AfiliacionSeguroPadreID = AFIL_SEG_PADRE.ID;
                            codDet.AfiliacionSeguroID = AFIL_SEGURO.ID;
                            codDet.ID = Convert.ToInt32(CodigoID);
                            codDet.Descripcion = (padre.ApellidoPaterno + " " + padre.ApellidoMaterno + " " + padre.Nombre).ToUpper();
                            codDet.FechaCreacion = DateTime.Now.Date;
                            codDet.UsuarioCreacion = Usuario;

                            //    CodigoDetalle codigo = db.CodigoDetalle.Where(p => p.ID == codDet.ID).Single();
                            //codigo.AfiliacionSeguroID = codDet.AfiliacionSeguroID;
                            //codigo.AfiliacionSeguroPadreID = codDet.AfiliacionSeguroPadreID;
                            //codigo.FechaActualizacion = DateTime.Now;
                            //codigo.UsuarioActualizacion = Usuario;

                            db.CodigoDetalle.Add(codDet);
                            db.SaveChanges();
                            fila += 1;
                        }
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                        res= 1;
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    res = -1;
                }
         return res;
            }
        }
    }


        public bool Existe(int idalumno)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                Alumno oalumno = db.Alumno.Find(idalumno);
                if (oalumno == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
        }
        public bool ExisteAlumno(int idTipoDocumento, string NumeroDoc)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var oalumno = db.Alumno.Where(p=>p.TipoDocumentoID==idTipoDocumento && p.NumeroDocumento==NumeroDoc);
                if (oalumno.Count()==0)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
        }

        //public int AgregarAfiliadoAccidentes(Alumno alumno, AfiliacionSeguro AFIL_SEGURO, int CodigoID)
        //{
        //    using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
        //    {
        //        using (var dbContextTransaction = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                //Guardar Alumno
        //                db.Alumno.Add(alumno);
        //                db.SaveChanges();
        //                //Guardar Afiliacion Seguro
        //                AfiliacionSeguroAlumno AFIL_SEG_ALU = new AfiliacionSeguroAlumno();
        //                AFIL_SEG_ALU.AlumnoID = alumno.ID;

        //                db.AfiliacionSeguro.Add(AFIL_SEGURO);
        //                db.SaveChanges();
        //                //Guardar Afiliacion Seguro Alumno
        //                AFIL_SEG_ALU.AfiliacionSeguroID = AFIL_SEGURO.ID;
        //                db.AfiliacionSeguroAlumno.Add(AFIL_SEG_ALU);
        //                db.SaveChanges();

        //                using (CodigoDAO dbCodigo = new CodigoDAO())
        //                {
        //                    CodigoDetalle codDet = new CodigoDetalle();
        //                    codDet.AfiliacionSeguroAlumnoID = AFIL_SEG_ALU.ID;
        //                    codDet.AfiliacionSeguroID = AFIL_SEGURO.ID;
        //                    codDet.ID = CodigoID;

        //                    CodigoDetalle codigo = db.CodigoDetalle.Where(p => p.ID == codDet.ID).Single();
        //                    codigo.AfiliacionSeguroID = codDet.AfiliacionSeguroID;
        //                    codigo.AfiliacionSeguroAlumnoID = codDet.AfiliacionSeguroAlumnoID;
        //                }
        //                //Confirmar grabacion
        //                db.SaveChanges();
        //                dbContextTransaction.Commit();
        //                return 1;
        //            }
        //            catch (Exception ex)
        //            {
        //                dbContextTransaction.Rollback();
        //                return -1;
        //            }
        //        }
        //    }
        //}


        public DataSet Obtener_Alumnos_Asegurados(int InstitucionEducativaID, int ProductoID,
                                                                                                       int CIASeguroID,int AsociacionID)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_OBT_ALUM_ASEGURADO", cn))
                {
                    cmd.Parameters.AddWithValue("@InstitucionEducativaID", InstitucionEducativaID);
                    cmd.Parameters.AddWithValue("@ProductoID", ProductoID);
                    cmd.Parameters.AddWithValue("@CIASeguroID", CIASeguroID);
                    cmd.Parameters.AddWithValue("@AsociacionID", AsociacionID);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
        }

        public DataSet ListarAlumnosAseguradosNombre(int InstitucionEducativaID, int ProductoID, int CIASeguroID, int AsociacionID, string Nombre)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_OBT_ALUMNOS_ASEGURADOS_NOMBRE", cn))
                {
                    cmd.Parameters.AddWithValue("@InstitucionEducativaID", InstitucionEducativaID);
                    cmd.Parameters.AddWithValue("@ProductoID", ProductoID);
                    cmd.Parameters.AddWithValue("@CIASeguroID", CIASeguroID);
                    cmd.Parameters.AddWithValue("@AsociacionID", AsociacionID);
                    cmd.Parameters.AddWithValue("@Nombre", Nombre);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
        }

        public int BUSCARALUMNO_SEGURO(int TipoDocumentoID,  string NumeroDocumento)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;      
            }   
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("BUSCARALUMNO_SEGURO", cn))
                {
                    cmd.Parameters.AddWithValue("@TipoDocumentoID", TipoDocumentoID);
                    cmd.Parameters.AddWithValue("@NumeroDocumento", NumeroDocumento);         
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    return Convert.ToInt32( cmd.ExecuteScalar()) ;                 
                }
            }
        }

        public int BUSCARALUMNO_PADRE(int TipoDocumentoID, string NumeroDocumento,  int TipoDocumentALU, string NumeroDOCAlu, int idasociacion )
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("BUSCARPADRE_SEGURO", cn))
                {
                    cmd.Parameters.AddWithValue("@TipoDocumentoID", TipoDocumentoID);
                    cmd.Parameters.AddWithValue("@NumeroDocumento", NumeroDocumento);
                    cmd.Parameters.AddWithValue("@TipoDocAlumno", TipoDocumentALU);
                    cmd.Parameters.AddWithValue("@NumDocAlumno", NumeroDOCAlu);
                    cmd.Parameters.AddWithValue("@ASOCIACIONID", idasociacion);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }



        public int OBTENER_CANT_CODIGOS(int id)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_OBTENER_CANT_CODIGOS", cn))
                {
                    cmd.Parameters.AddWithValue("@ID", id);                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    try
                    {
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch(Exception ex)
                    {
                        return 0;
                    }
                }
            }
        }


        public DataSet GEN_EXPORTARCODIGOS_ACCIDENTES(int? Codigo, int Cantidad)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_EXPORTARCODIGOS_ACCIDENTES", cn))
                {
                    cmd.Parameters.AddWithValue("@Codigo", Codigo);
                    cmd.Parameters.AddWithValue("@Cant", Cantidad);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
        }

        public DataSet GEN_EXPORTARCODIGOS_RENTAS(int? Codigo, int Cantidad)
        {
            string cnx = "";
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                cnx = db.Database.Connection.ConnectionString;
            }
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_EXPORTARCODIGOS_RENTAS", cn))
                {
                    cmd.Parameters.AddWithValue("@Codigo", Codigo);
              //      cmd.Parameters.AddWithValue("@Cant", Cantidad);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
        }

        public int ObtenerGradoID(string grado)
        {
            int gradoID = 0;
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                try
                {
                    gradoID = db.Grado.Where(p => p.Codigo.ToUpper() == grado.ToUpper()).First().ID;
                    return gradoID;
                }
                catch(Exception ex)
                {
                    return 0;
                }
            }
        }

        public int ObtenerGradoIDXNombre(string grado)
        {
            int gradoID = 0;
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                try
                {
                    gradoID = db.Grado.Where(p => p.Codigo.ToUpper() == grado.ToUpper()).First().ID;
                    return gradoID;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }



        public int AnularAsegurado(string Codigo, int id, bool Estado )
        {            
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                try
                {
                  string cnx = db.Database.Connection.ConnectionString;
                    using (SqlConnection cn = new SqlConnection(cnx))
                    {
                        using (SqlCommand cmd = new SqlCommand("USP_ANULAR_COD_SEGURO", cn))
                        {
                            cmd.Parameters.AddWithValue("@ID", id);
                            cmd.Parameters.AddWithValue("@ESTADO", Estado);
                            cmd.Parameters.AddWithValue("@CODIGO", Codigo);                            
                            cmd.CommandType = CommandType.StoredProcedure;
                            cn.Open();
                            return cmd.ExecuteNonQuery();                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }
        public int EliminarCodigoAsegurado(int id, string Codigo)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                try
                {
                    string cnx = db.Database.Connection.ConnectionString;
                    using (SqlConnection cn = new SqlConnection(cnx))
                    {
                        using (SqlCommand cmd = new SqlCommand("USP_ELIMINAR_COD_SEGURO", cn))
                        {
                            cmd.Parameters.AddWithValue("@ID", id);                            
                            cmd.Parameters.AddWithValue("@CODIGO", Codigo);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cn.Open();
                            return cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }



        //public int AgregarAfiliadoCargaRentas(Alumno alumno,List< Padre> padres, int CodigoID , int InstitucionEducativaID, string Usuario )
        //{
        //    using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
        //    {
        //        using (var dbContextTransaction = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                //Guardar Alumno
        //                db.Alumno.Add(alumno);
        //                db.SaveChanges();
        //                //Guardar Padres                          
        //                foreach (Padre padre in padres)
        //                {
        //                    padre.BeneficiarioID = alumno.ID;
        //                    db.Padre.Add(padre);
        //                    db.SaveChanges();
        //                    //Guardar Afiliacion Seguro
        //                    AfiliacionSeguro afilia = new AfiliacionSeguro();
        //                    afilia.InstitucionEducativaID = InstitucionEducativaID;
        //                    afilia.FechaCreacion = DateTime.Now.Date;
        //                    afilia.UsuarioCreacion = Usuario;
        //                    afilia.CodigoPago = "0";
        //                    afilia.Estado = true;
        //                    db.AfiliacionSeguro.Add(afilia);
        //                    db.SaveChanges();
        //                    AfiliacionSeguroPadre afiliaPadre = new AfiliacionSeguroPadre();
        //                    afiliaPadre.AfiliacionSeguroID = afilia.ID;
        //                    afiliaPadre.PadreID = padre.ID;
        //                    using (CodigoDAO dbCodigo = new CodigoDAO())
        //                    {
        //                        CodigoDetalle codDet = new CodigoDetalle();
        //                        codDet.AfiliacionSeguroPadreID = afiliaPadre.ID;
        //                        codDet.AfiliacionSeguroID = afilia.ID;
        //                        codDet.ID = Convert.ToInt32(CodigoID);
        //                        CodigoDetalle codigo = db.CodigoDetalle.Where(p => p.ID == codDet.ID).Single();
        //                    }
        //                //        codigo.AfiliacionSeguroID = codDet.AfiliacionSeguroID;
        //                //        codigo.AfiliacionSeguroPadreID = codDet.AfiliacionSeguroPadreID;
        //                //        codigo.FechaActualizacion = DateTime.Now;
        //                //        //codigo.UsuarioActualizacion=Session["Usuario"];
        //                //    }
        //                //fila += 1;

        //                db.SaveChanges();                                                
        //                dbContextTransaction.Commit();
        //                    return 1;
        //                }
        //                catch (Exception ex)
        //                {
        //                    dbContextTransaction.Rollback();
        //                    return -1;
        //                }
        //            }
        //        }              #region IDisposable Support
        private bool disposedValue = false; // Para detectar llamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: elimine el estado administrado (objetos administrados).
                }

                // TODO: libere los recursos no administrados (objetos no administrados) y reemplace el siguiente finalizador.
                // TODO: configure los campos grandes en nulos.

                disposedValue = true;
            }
        }

        // TODO: reemplace un finalizador solo si el anterior Dispose(bool disposing) tiene código para liberar los recursos no administrados.
        // ~Alumno_DAO() {
        //   // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
        //   Dispose(false);
        // }

        // Este código se agrega para implementar correctamente el patrón descartable.
        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
            Dispose(true);
            // TODO: quite la marca de comentario de la siguiente línea si el finalizador se ha reemplazado antes.
            // GC.SuppressFinalize(this);
        }
        
    }
}






