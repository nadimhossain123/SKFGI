using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Payroll
{
    public class EmployeeSalaryData
    {
        public EmployeeSalaryData()
        {
        }

        public int EmployeeSalaryDataId { get; set; }
        public int EmployeeSalaryData_EmpId { get; set; }
        public decimal EmployeeSalaryDataBasicAmount { get; set; }
        public decimal EmployeeSalaryDataPFAmount { get; set; }
        public decimal EmployeeSalaryDataESIAmount { get; set; }
        public decimal EmployeeSalaryDataPTaxAmount { get; set; }
        public decimal EmployeeSalaryDataLoanAmount { get; set; }
        public int EmployeeSalaryDataMonth { get; set; }
        public int EmployeeSalaryDataYear { get; set; }
        public int CreatedBy { get; set; }
        public decimal EmployeeSalaryDataAttendance { get; set; }
        public decimal EmployeeSalaryDataNetSalary { get; set; }
        public EmployeeSalaryHeadDataDTO[] EmployeeSalaryHeadDataDTO { get; set; }
    }
}
