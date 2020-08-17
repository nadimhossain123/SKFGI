using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Payroll
{
    public class Loan
    {
        public Loan()
        {
        }

        public void Save(Entity.Payroll.Loan Loan)
        {
            DataAccess.Payroll.Loan.Save(Loan);
        }

        public DataTable GetAll(int EmployeeId)
        {
            return DataAccess.Payroll.Loan.GetAll(EmployeeId);
        }

        public Entity.Payroll.Loan GetAllById(int LoanId)
        {
            return DataAccess.Payroll.Loan.GetAllById(LoanId);
        }

        public void Delete(int LoanId)
        {
            DataAccess.Payroll.Loan.Delete(LoanId);
        }
    }
}
