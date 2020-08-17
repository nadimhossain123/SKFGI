using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Payroll
{
    public class SalaryHead
    {
        public SalaryHead()
        {
        }

        public int SalaryHeadId { get; set; }
        public string SalaryHeadDetails { get; set; }
        public bool IsFixed { get; set; }
        public decimal? MaxRange { get; set; }

    }
}
