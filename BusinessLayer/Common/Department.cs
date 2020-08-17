using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public  class Department
    {
        public Department()
        {
        }

        public int Save(Entity.Common.Department Department)
        {
            return DataAccess.Common.Department.Save(Department);
        }
        public DataTable GetAll()
        {
            return DataAccess.Common.Department.GetAll();
        }

        public Entity.Common.Department GetAllById(int DepartmentId)
        {
            return DataAccess.Common.Department.GetAllById(DepartmentId);
        }

        public int Delete(int DepartmentId)
        {
            return DataAccess.Common.Department.Delete(DepartmentId);
        }
    }
}
