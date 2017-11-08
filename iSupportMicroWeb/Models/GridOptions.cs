using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iSupportMicroWeb.Models
{
    public class GridOptions
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public String sort { get; set; }
    }
}
