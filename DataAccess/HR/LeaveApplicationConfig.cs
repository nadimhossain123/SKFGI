using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.HR
{
    public class LeaveApplicationConfig
    {
        public LeaveApplicationConfig()
        {
        }

        public static void SaveMaxDayLimit(int MaxDayLimit)
        {
            using (DataManager oDm = new DataManager())
            {
                string SQL = string.Format("Update LeaveApplicationConfig Set [MaxDayLimit]={0}", MaxDayLimit);
                oDm.CommandType = CommandType.Text;
                oDm.ExecuteNonQuery(SQL);
            }
        }

        public static int GetMaxDayLimit()
        {
            using (DataManager oDm = new DataManager())
            {
                string SQL = "Select MaxDayLimit From LeaveApplicationConfig";
                oDm.CommandType = CommandType.Text;
                return Convert.ToInt32(oDm.ExecuteDataTable(SQL).Rows[0]["MaxDayLimit"]);
            }
        }
    }
}
