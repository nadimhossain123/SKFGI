using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.HR
{
    public class LeaveManagerChange
    {
        public static DataSet GetAllByEmployeeId(int EmployeeId, int IntMode)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@IntMode", SqlDbType.Int, IntMode);
                oDm.Add("@EmployeeId", SqlDbType.Int, EmployeeId);
                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_EmployeeLeaveManagerChange", ref ds, "table");

            }
        }
        public static int UpdateLeaveManager(string LeaveManagerUpdateXml)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.Add("@IntMode", SqlDbType.Int, 1);
                oDm.Add("@LeaveManagerUpdateXml", SqlDbType.Xml, LeaveManagerUpdateXml);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteNonQuery("usp_EmployeeLeaveManagerChange");

            }
        }
    }
}
