using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class EmployeeQualification
    {
        public EmployeeQualification()
        {
        }

        public void Save(Entity.Common.EmployeeQualification Qualification)
        {
            DataAccess.Common.EmployeeQualification.Save(Qualification);
        }

        public DataTable GetAll(int EmployeeId)
        {
            return DataAccess.Common.EmployeeQualification.GetAll(EmployeeId);
        }

        public Entity.Common.EmployeeQualification GetAllById(int EmployeeQualificationId)
        {
            return DataAccess.Common.EmployeeQualification.GetAllById(EmployeeQualificationId);
        }

        public void Delete(int EmployeeQualificationId)
        {
            DataAccess.Common.EmployeeQualification.Delete(EmployeeQualificationId);
        }
    }
}
