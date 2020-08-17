using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Payroll
{
    public class EmployeeSalary
    {
        public EmployeeSalary()
        {
        }

        public int Save(Entity.Payroll.EmployeeSalary Salary)
        {
            return DataAccess.Payroll.EmployeeSalary.Save(Salary);
        }

        public DataTable GetAll(string EmpCode, string FirstName)
        {
            return DataAccess.Payroll.EmployeeSalary.GetAll(EmpCode, FirstName);
        }

        public DataTable GetAllSalaryHead(int EmployeeId)
        {
            return DataAccess.Payroll.EmployeeSalary.GetAllSalaryHead(EmployeeId);
        }

        public Entity.Payroll.EmployeeSalary GetAllById(int EmployeeId)
        {
            return DataAccess.Payroll.EmployeeSalary.GetAllById(EmployeeId);
        }

    }
}
