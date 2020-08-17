using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class Role
    {
        public Role()
        {
        }

        public static int Save(Entity.Common.Role Role)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pRoleId", SqlDbType.Int, Role.RoleId);
                oDm.Add("@pRoleDescription", SqlDbType.VarChar, 50, Role.RoleDescription);

                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_RoleMaster_Save");
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_RoleMaster_GetAll");
            }
        }

        public static Entity.Common.Role GetAllById(int RoleId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pRoleId", SqlDbType.Int, RoleId);
                SqlDataReader dr = oDm.ExecuteReader("usp_RoleMaster_GetAllById");
                Entity.Common.Role Role = new Entity.Common.Role();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Role.RoleId = RoleId;
                        Role.RoleDescription = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();

                    }
                }
                return Role;
            }
        }

        public static int Delete(int RoleId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pRoleId", SqlDbType.Int, RoleId);
                return oDm.ExecuteNonQuery("usp_RoleMaster_Delete");
            }
        }
    }
}
