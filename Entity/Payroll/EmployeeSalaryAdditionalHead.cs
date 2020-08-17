using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Payroll
{
    public class EmployeeSalaryAdditionalHead
    {
        public EmployeeSalaryAdditionalHead()
        {
        }

        public int int_mode { get; set; }
        public int EmployeeSalaryAdditionalHeadId { get; set; }
        public int EmployeeId { get; set; }
        public int SalaryAdditionalHeadId { get; set; }
        public int AdditionMonth { get; set; }
        public int AdditionYear { get; set; }
        public decimal AdditionAmount { get; set; }
    }
}
