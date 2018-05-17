using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Hermes.Repositorios
{
    public class Pagination
    {
        public int CurrentPage { get; set; }
        //      [JsonProperty("ItemsPerPage")]
        public int ItemsPerPage { get; set; }
        //    [JsonProperty("TotalItems")]
        public int TotalItems { get; set; }
        //  [JsonProperty("TotalPages")]
        public int TotalPages { get; set; }
    }
}
