using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Payroll
{
    public class ITaxEmployeeContribution
    {
        public int Save(Entity.Payroll.ITaxEmployeeContribution ITaxEmployeeContribution)
        {
            return DataAccess.Payroll.ITaxEmployeeContribution.Save(ITaxEmployeeContribution);
        }

        public DataTable GetAll(int EmployeeId, string FName, string EmpCode)
        {
            return DataAccess.Payroll.ITaxEmployeeContribution.GetAll(EmployeeId,FName,EmpCode);
        }

        public Entity.Payroll.ITaxEmployeeContribution GetAllById(int ITaxEmployeeContributionId)
        {
            return DataAccess.Payroll.ITaxEmployeeContribution.GetAllById(ITaxEmployeeContributionId);
        }

        public int Delete(int ITaxEmployeeContributionId)
        {
            return DataAccess.Payroll.ITaxEmployeeContribution.Delete(ITaxEmployeeContributionId);
        }

    }
}
