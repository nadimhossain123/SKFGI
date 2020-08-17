using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class EmployeeRole
    {
        public EmployeeRole()
        {
        }

        public int Save(Entity.Common.EmployeeRole EmployeeRole)
        {
            return DataAccess.Common.EmployeeRole.Save(EmployeeRole);
        }

        public DataTable GetAll(int EmployeeId)
        {
            return DataAccess.Common.EmployeeRole.GetAll(EmployeeId);
        }

        public void Delete(int EmployeeRoleMappingId)
        {
            DataAccess.Common.EmployeeRole.Delete(EmployeeRoleMappingId);
        }

        public void Save_RoleAccessLevel(int RoleId, int PermissionId, bool isChecked)
        {
            DataAccess.Common.EmployeeRole.Save_RoleAccessLevel(RoleId, PermissionId, isChecked);
        }

        public DataTable GetRoleAccessLevelByRoleId(int RoleId)
        {
            return DataAccess.Common.EmployeeRole.GetRoleAccessLevelByRoleId(RoleId);
        }
    }
}
