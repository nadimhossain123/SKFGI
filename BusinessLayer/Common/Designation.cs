using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class Designation
    {
        public Designation()
        {
        }

        public int Save(Entity.Common.Designation Designation)
        {
            return DataAccess.Common.Designation.Save(Designation);
        }
        public DataTable GetAll()
        {
            return DataAccess.Common.Designation.GetAll();
        }
        public Entity.Common.Designation GetAllById(int DesignationId)
        {
            return DataAccess.Common.Designation.GetAllById(DesignationId);
        }

        public int Delete(int DesignationId)
        {
            return DataAccess.Common.Designation.Delete(DesignationId);
        }
    }
}
