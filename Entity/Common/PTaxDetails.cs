using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public class PTaxDetails
    {
        public PTaxDetails()
        {
        }

        public int PTaxDetailsId { get; set; }
        public int PTaxDetails_PTaxId { get; set; }
        public int PTaxDetailsSlabNo { get; set; }
        public decimal PTaxDetailsFromAmount { get; set; }
        public decimal PTaxDetailsToAmount { get; set; }
        public decimal PTaxDetailsAmount { get; set; }
        public int PTaxDetailsCUser_UserId { get; set; }
    }
}
