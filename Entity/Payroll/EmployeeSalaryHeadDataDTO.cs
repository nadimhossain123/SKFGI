using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Payroll
{
    public class EmployeeSalaryHeadDataDTO
    {
        public EmployeeSalaryHeadDataDTO()
        {
        }

        public int EmployeeSalaryHeadDataId {get;set;}
        public int EmployeeSalaryHeadData_SalaryHeadId {get;set;}
        public decimal EmployeeSalaryHeadDataAmount { get; set; }

    }
}
