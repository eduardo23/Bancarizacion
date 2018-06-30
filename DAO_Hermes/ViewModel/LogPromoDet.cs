using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Hermes.ViewModel
{
    public class LogPromoDet
    {
        public int ID { get; set; }
        public int logPromoID { get; set; }
        public int id_grupo_correo { get; set; }
        public string destinatario { get; set; }

        public virtual LogPromo LogPromo { get; set; }
        public virtual GrupoCorreo GrupoCorreo { get; set; }

        public LogPromoDet()
        {
            LogPromo = new LogPromo();
            GrupoCorreo = new GrupoCorreo();
        }

    }
}
