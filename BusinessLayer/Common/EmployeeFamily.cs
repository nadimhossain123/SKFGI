using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class EmployeeFamily
    {
        public EmployeeFamily()
        {
        }

        public void Save(Entity.Common.EmployeeFamily Family)
        {
            DataAccess.Common.EmployeeFamily.Save(Family);
        }

        public DataTable GetAll(int EmployeeId)
        {
            return DataAccess.Common.EmployeeFamily.GetAll(EmployeeId);
        }

        public Entity.Common.EmployeeFamily GetAllById(int EmployeeFamilyId)
        {
            return DataAccess.Common.EmployeeFamily.GetAllById(EmployeeFamilyId);
        }

        public void Delete(int EmployeeFamilyId)
        {
            DataAccess.Common.EmployeeFamily.Delete(EmployeeFamilyId);
        }
    }
}
