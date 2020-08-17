using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class Role
    {
        public Role()
        {
        }

        public int Save(Entity.Common.Role Role)
        {
            return DataAccess.Common.Role.Save(Role);
        }

        public DataTable GetAll()
        {
            return DataAccess.Common.Role.GetAll();
        }

        public Entity.Common.Role GetAllById(int RoleId)
        {
            return DataAccess.Common.Role.GetAllById(RoleId);
        }

        public int Delete(int RoleId)
        {
            return DataAccess.Common.Role.Delete(RoleId);
        }
    }
}
