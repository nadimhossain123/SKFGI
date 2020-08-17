using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Payroll
{
    public class ITaxEmployeePrevEmplDetails
    {
        public int Save(Entity.Payroll.ITaxEmployeePrevEmplDetails ITaxEmployeePrevEmplDetails)
        {
            return DataAccess.Payroll.ITaxEmployeePrevEmplDetails.Save(ITaxEmployeePrevEmplDetails);
        }

        public DataTable GetAll(int EmployeeId)
        {
            return DataAccess.Payroll.ITaxEmployeePrevEmplDetails.GetAll(EmployeeId);
        }

        public Entity.Payroll.ITaxEmployeePrevEmplDetails GetAllById(int ITaxPrevEmplHeadId, int EmployeeId)
        {
            return DataAccess.Payroll.ITaxEmployeePrevEmplDetails.GetAllById(ITaxPrevEmplHeadId, EmployeeId);
        }

        public int Delete(int ITaxPrevEmplHeadId, int EmployeeId)
        {
            return DataAccess.Payroll.ITaxEmployeePrevEmplDetails.Delete(ITaxPrevEmplHeadId, EmployeeId);
        }
    }
}
