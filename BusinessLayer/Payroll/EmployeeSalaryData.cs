using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Payroll
{
    public class EmployeeSalaryData
    {
        public EmployeeSalaryData()
        {
        }


        public DataSet GetAll(int Month, int Year, int CompanyId)
        {
            return DataAccess.Payroll.EmployeeSalaryData.GetAll(Month, Year, CompanyId);
        }

        public DataSet GetIndividualSalaryDetails(int EmployeeId, int FinancialYear)
        {
            return DataAccess.Payroll.EmployeeSalaryData.GetIndividualSalaryDetails(EmployeeId, FinancialYear);
        }

        public string MonthlySalaryGeneration(int Month, int Year, int CreatedBy, int CompanyId)
        {
            return DataAccess.Payroll.EmployeeSalaryData.MonthlySalaryGeneration(Month, Year, CreatedBy, CompanyId);
        }

        public void FinalizeSalary(int Month, int Year, int CompanyId)
        {
            DataAccess.Payroll.EmployeeSalaryData.FinalizeSalary(Month, Year, CompanyId);
        }

        public DataSet MonthlyPaySlip(int Month, int Year, int EmployeeId)
        {
            return DataAccess.Payroll.EmployeeSalaryData.MonthlyPaySlip(Month, Year, EmployeeId);
        }

    }
}
