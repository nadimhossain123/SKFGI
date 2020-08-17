using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.SuperAdmin
{
    public class SuperAdmin
    {
        public SuperAdmin() { }

        public static int Update(string Password)
        {
            using (DataManager oDm = new DataManager())
            {
                if (Password != "")
                    oDm.Add("", SqlDbType.VarChar, 50, Password);
                else
                    oDm.Add("", SqlDbType.VarChar, 50, DBNull.Value);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteNonQuery("usp_SuperAdminPassword_Update");
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_SuperAdminPassword_GetAll");
            }
        }
    }
}
