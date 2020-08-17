using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Payroll
{
    public class Form16
    {
        public Form16()
        {
        }

        public int Save(Entity.Payroll.Form16 Form16,int EmployeeId)
        {
            return DataAccess.Payroll.Form16.Save(Form16, EmployeeId);
        }

        public DataTable GetAll(int EmployeeId)
        {
            return DataAccess.Payroll.Form16.GetAll(EmployeeId);
        }
    }
}
