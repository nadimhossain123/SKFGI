using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Payroll
{
    public class ITaxEmployeePrevEmplDetails
    {
        public int ITaxPrevEmplHeadId { get; set; }
        public int EmployeeId { get; set; }
        public decimal ITaxPrevEmplHeadAmount { get; set; }
        public int FinancialYear { get; set; }
    }
}
