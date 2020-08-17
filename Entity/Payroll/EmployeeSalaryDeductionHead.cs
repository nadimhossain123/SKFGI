using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Payroll
{
    public class EmployeeSalaryDeductionHead
    {
        public EmployeeSalaryDeductionHead()
        {
        }

        public int int_mode { get; set; }
        public int EmployeeSalaryDeductionHeadId { get; set; }
        public int EmployeeId { get; set; }
        public int SalaryDeductionHeadId { get; set; }
        public int DeductionMonth { get; set; }
        public int DeductionYear { get; set; }
        public decimal DeductionAmount { get; set; }
    }
}
