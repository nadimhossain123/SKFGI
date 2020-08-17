using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Payroll
{
    public class EmployeeSalaryHeadDTO
    {
        public EmployeeSalaryHeadDTO()
        {
        }

        public int EmployeeSalaryHead_SalaryHeadId { get; set; }
        public decimal EmployeeSalaryHeadPercent { get; set; }
        public decimal EmployeeSalaryHeadAmount { get; set; }

    }
}
