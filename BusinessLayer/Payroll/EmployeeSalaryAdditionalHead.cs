using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Payroll
{
    public class EmployeeSalaryAdditionalHead
    {
        public EmployeeSalaryAdditionalHead()
        {
        }

        public DataTable GetAdditionalHead()
        {
            return DataAccess.Payroll.EmployeeSalaryAdditionalHead.GetAdditionalHead();
        }

        public void Save(Entity.Payroll.EmployeeSalaryAdditionalHead AdditionHead)
        {
            DataAccess.Payroll.EmployeeSalaryAdditionalHead.Save(AdditionHead);
        }

        public DataTable GetAll(Entity.Payroll.EmployeeSalaryAdditionalHead AdditionHead)
        {
            return DataAccess.Payroll.EmployeeSalaryAdditionalHead.GetAll(AdditionHead);
        }

        public void Delete(int EmployeeSalaryAdditionalHeadId)
        {
            DataAccess.Payroll.EmployeeSalaryAdditionalHead.Delete(EmployeeSalaryAdditionalHeadId);
        }
    }
}
