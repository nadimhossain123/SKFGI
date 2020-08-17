using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class Employee
    {
        public Employee()
        {
        }

        public int Save(Entity.Common.Employee Employee)
        {
          return  DataAccess.Common.Employee.Save(Employee);
        }

        public DataTable GetAll(string EmpCode, string FirstName)
        {
            return DataAccess.Common.Employee.GetAll(EmpCode, FirstName);
        }

        public Entity.Common.Employee GetAllById(int EmployeeId)
        {
            return DataAccess.Common.Employee.GetAllById(EmployeeId);
        }

        public Entity.Common.Employee AuthenticateUser(string UserName)
        {
            return DataAccess.Common.Employee.AuthenticateUser(UserName);
        }

        public void ChangePassword(Entity.Common.Employee Employee)
        {
            DataAccess.Common.Employee.ChangePassword(Employee);
        }
        public DataTable GetEmpByDesigAndDept(int DepartmentId, int DesignationId)
        {
            return DataAccess.Common.Employee.GetEmpByDesigAndDept(DepartmentId, DesignationId);
        }
    }
}
