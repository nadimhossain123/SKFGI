using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Payroll
{
    public class Form16
    {
        public Form16()
        {
        }

        public static int Save(Entity.Payroll.Form16 Form16,int EmployeeId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pYear", SqlDbType.Int, Form16.FinYear);
                oDm.Add("@pEmployeeId", SqlDbType.Int, EmployeeId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteNonQuery("usp_Form16ReportGenerate_Step2");
               // return oDm.ExecuteNonQuery("usp_Form16ReportGenerate");
            }
        }
        public static DataTable GetAll(int EmployeeId)
        {
            using (DataManager oDm = new DataManager())
            {
                if (EmployeeId == 0) { oDm.Add("@pEmployeeId", SqlDbType.Int, DBNull.Value); }
                else { oDm.Add("@pEmployeeId", SqlDbType.Int, EmployeeId); }

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_Form16ReportGetAll");
            }
        }
    }
}
