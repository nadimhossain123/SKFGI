using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Payroll
{
    public class EmployeeSalaryDeductionHead
    {
        public EmployeeSalaryDeductionHead()
        {
        }

        public DataTable GetActiveEmployee(int CompanyId)
        {
            return DataAccess.Payroll.EmployeeSalaryDeductionHead.GetActiveEmployee(CompanyId);
        }

        public DataTable GetDeductionHead()
        {
            return DataAccess.Payroll.EmployeeSalaryDeductionHead.GetDeductionHead();
        }

        public void Save(Entity.Payroll.EmployeeSalaryDeductionHead DeductionHead)
        {
            DataAccess.Payroll.EmployeeSalaryDeductionHead.Save(DeductionHead);
        }

        public DataTable GetAll(Entity.Payroll.EmployeeSalaryDeductionHead DeductionHead)
        {
            return DataAccess.Payroll.EmployeeSalaryDeductionHead.GetAll(DeductionHead);
        }

        public void Delete(int EmployeeSalaryDeductionHeadId)
        {
            DataAccess.Payroll.EmployeeSalaryDeductionHead.Delete(EmployeeSalaryDeductionHeadId);
        }
    }
}
