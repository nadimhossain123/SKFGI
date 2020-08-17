using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Payroll
{
    public class EmployeeIncrement
    {
        public static DataSet GetAllByDate(DateTime DateFrom, DateTime DateTo, int IntMode)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@DateFrom", SqlDbType.DateTime, DateFrom);
                oDm.Add("@DateTo", SqlDbType.DateTime, DateTo);
                oDm.Add("@IntMode", SqlDbType.Int, IntMode);
                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_EmployeeIncrement", ref ds, "table");

            }
        }
        public static int UpdateEmployeeIncrement(string IncrementListXml,DateTime IncrDate,int CreatedBy)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.Add("@IntMode", SqlDbType.Int, 2);
                oDm.Add("@CreatedBy", SqlDbType.Int, CreatedBy);
                oDm.Add("@IncrementListXml", SqlDbType.Xml, IncrementListXml);
                oDm.Add("@IncrDate", SqlDbType.DateTime, IncrDate);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteNonQuery("usp_EmployeeIncrement");

            }
        }
    }
}
