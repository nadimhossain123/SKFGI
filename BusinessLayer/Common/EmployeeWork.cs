using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class EmployeeWork
    {
        public EmployeeWork()
        {
        }

        public void Save(Entity.Common.EmployeeWork Work)
        {
            DataAccess.Common.EmployeeWork.Save(Work);
        }

        public DataTable GetAll(int EmployeeId)
        {
            return DataAccess.Common.EmployeeWork.GetAll(EmployeeId);
        }

        public Entity.Common.EmployeeWork GetAllById(int EmployeeWorkId)
        {
            return DataAccess.Common.EmployeeWork.GetAllById(EmployeeWorkId);
        }

        public void Delete(int EmployeeWorkId)
        {
            DataAccess.Common.EmployeeWork.Delete(EmployeeWorkId);
        }
    }
}
