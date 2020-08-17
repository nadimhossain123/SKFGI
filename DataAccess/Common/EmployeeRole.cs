using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class EmployeeRole
    {
        public EmployeeRole()
        {
        }

        public static int Save(Entity.Common.EmployeeRole EmployeeRole)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pEmployeeId", SqlDbType.Int, EmployeeRole.EmployeeId);
                oDm.Add("@pRoleId", SqlDbType.Int, EmployeeRole.RoleId);

                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_EmployeeRole_Save");
            }
        }

        public static DataTable GetAll(int EmployeeId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pEmployeeId", SqlDbType.Int, EmployeeId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_EmployeeRole_GetAll");
            }
        }

        public static void Delete(int EmployeeRoleMappingId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pEmployeeRoleMappingId", SqlDbType.Int, EmployeeRoleMappingId);
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_EmployeeRole_Delete");
            }
        }

        public static void Save_RoleAccessLevel(int RoleId, int PermissionId, bool isChecked)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pRoleId", SqlDbType.Int, RoleId);
                oDm.Add("@pPermissionId", SqlDbType.Int, PermissionId);
                oDm.Add("@pChecked", SqlDbType.Bit, isChecked);

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.ExecuteNonQuery("usp_RoleAccessLevel_Save");


            }
        }

        public static DataTable GetRoleAccessLevelByRoleId(int RoleId)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.Add("@pRoleId", SqlDbType.Int, ParameterDirection.Input, RoleId);

                return oDm.ExecuteDataTable("usp_RoleAccessLevel_GetByRoleId");


            }
        }
    }
}
