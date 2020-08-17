using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public class PTax
    {
        public PTax()
        {
        }

        public int PTaxId { get; set; }
        public string PTaxStateCode { get; set; }
        public string PTaxStateDescription { get; set; }

    }
}
