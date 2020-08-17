using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Payroll
{
    public class EmployeeSalary
    {
        public EmployeeSalary()
        {
        }

        public int EmployeeSalaryId { get; set; }
        public int EmployeeSalary_EmpId { get; set; }
        public decimal EmployeeSalaryBasicAmount { get; set; }
        public decimal EmployeeSalaryPFAmount { get; set; }
        public int EmployeeSalary_PTaxId { get; set; }
        public decimal EmployeeSalaryEmployerPF { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsFixedPF { get; set; }
        public decimal EmployeeSalaryGradePay { get; set; }
        public EmployeeSalaryHeadDTO[] EmployeeSalaryHeadDTO { get; set; }

    }
}
