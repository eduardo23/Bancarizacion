using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Hermes.ViewModel
{
    public class GestionCorreo
    {
        private int _id = 0;
        
        private string _nombre1 = "";
        private string _nombre2 = "";
        private string _apePaterno = "";
        private string _apeMaterno = "";
        private string _email = "";
        private int _id_estado = 0;
        private string _descestado = "";
        private string _usuariocreacion = "";
        private string _usuariomodificacion = "";
        private string _tokens = "";
        private string _fechabaja = "";

        private string _codigo = "";
        public GrupoCorreo grupocorreo { get; set; }
        public int id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }

        public string codigo
        {
            get { return _codigo; }
            set
            {
                _codigo = value;
            }
        }

        public string fechabaja
        {
            get { return _fechabaja; }
            set
            {
                _fechabaja = value;
            }
        }

        public string Tokens
        {
            get { return _tokens; }
            set
            {
                _tokens = value;
            }
        }
        public string Nombre1
        {
            get { return _nombre1; }
            set
            {
                _nombre1 = value;
            }
        }
        public string Nombre2
        {
            get { return _nombre2; }
            set
            {
                _nombre2 = value;
            }
        }
        public string ApePaterno
        {
            get { return _apePaterno; }
            set
            {
                _apePaterno = value;
            }
        }
        public string ApeMaterno
        {
            get { return _apeMaterno; }
            set
            {
                _apeMaterno = value;
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
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
        public string descestado
        {
            get { return _descestado; }
            set
            {
                _descestado = value;
            }
        }
        public string UsuarioCreacion
        {
            get { return _usuariocreacion; }
            set
            {
                _usuariocreacion = value;
            }
        }
        public string UsuarioModificacion
        {
            get { return _usuariomodificacion; }
            set
            {
                _usuariomodificacion = value;
            }
        }
        public GestionCorreo()
        {
            grupocorreo = new GrupoCorreo();
        }
    }
}
