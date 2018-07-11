using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Hermes.ViewModel
{
    public class LogPromo
    {
        public int ID { get; set; }
        public string remitente { get; set; }
        public string asunto { get; set; }

        public int PlantillaID { get; set; }

        public Plantilla Plantilla { get; set; }
        public virtual List<LogPromoDet> LogPromoDet { get; set; }

        public string fecha { get; set; }

        public Users Users { get; set; }

        public LogPromo() {
            this.Plantilla = new Plantilla();
            this.LogPromoDet = new List<LogPromoDet>();
            this.Users = new Users();
        }

    }
}
