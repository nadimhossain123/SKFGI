using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Common
{
    public class DBBackup
    {
        public DBBackup()
        {

        }

        public static void Save(Entity.Common.DBBackup backup)
        {
            using (DataManager oDm = new DataAccess.DataManager())
            {
                oDm.Add("@pEmployeeId", SqlDbType.Int, backup.EmployeeId);
                oDm.Add("@pBackupPath", SqlDbType.VarChar, 100, backup.BackupPath);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_DBBackup_Save");
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataAccess.DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_DBBackup_GetAll");
            }
        }
    }
}
