using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Student
{
    public class HostelFeesConfig
    {
        public HostelFeesConfig()
        {
        }

        public int id { get; set; }
        public int batch_id { get; set; }
        public string fees_name { get; set; }
        public string feesdetailsxml { get; set; }
    }
}
