using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Hermes.ViewModel
{
    public class GrupoCorreo
    {
        private int _id = 0;
        private string _descripcion = "";
        private int _estado = 0;
        private string _descestado = "";
        private int _origen = 0;
        private string _descorigen = "";
        public int id
        {
            get { return _id; }
            set
            {
                _id = value;
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
        public int estado
        {
            get { return _estado; }
            set
            {
                _estado = value;
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
        public int origen
        {
            get { return _origen; }
            set
            {
                _origen = value;
            }
        }
        public string descorigen
        {
            get { return _descorigen; }
            set
            {
                _descorigen = value;
            }
        }
    }
}
