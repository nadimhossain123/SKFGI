using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Payroll
{
    public class ITaxSectionMaster
    {
        public int ITaxSectionId { get; set; }
        public string ITaxSectionName { get; set; }
        public decimal ITaxMaxExemption { get; set; }
    }
}
