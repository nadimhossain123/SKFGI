using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Student
{
    public class HostelFeesGeneration
    {
        public HostelFeesGeneration()
        {

        }

        public static DataTable GetAllStudent(Entity.Student.HostelFeesGeneration Fees)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@batch_id", SqlDbType.Int, Fees.batch_id);
                oDm.Add("@CompanyID_FK", SqlDbType.Int, Fees.CompanyID_FK);
                oDm.Add("@Year", SqlDbType.Int, Fees.Year);
                oDm.Add("@Quarter", SqlDbType.Int, Fees.Quarter);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_HostelBillGeneration_GetStudent");
            }
        }

        public static void GenerateFees(Entity.Student.HostelFeesGeneration Fees)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@CompanyID_FK", SqlDbType.Int, Fees.CompanyID_FK);
                oDm.Add("@BranchID_FK", SqlDbType.Int, Fees.BranchID_FK);
                oDm.Add("@FinYearID_FK", SqlDbType.Int, Fees.FinYearID_FK);
                oDm.Add("@DataFlow", SqlDbType.Int, Fees.DataFlow);
                oDm.Add("@Year", SqlDbType.Int, Fees.Year);
                oDm.Add("@Quarter", SqlDbType.Int, Fees.Quarter);
                oDm.Add("@CreatedBy", SqlDbType.Int, Fees.CreatedBy);
                oDm.Add("@StudentIdXML", SqlDbType.Xml, Fees.StudentIdXML);
                oDm.Add("@feesdetailsxml", SqlDbType.Xml, Fees.feesdetailsxml);
                oDm.Add("@BillDate", SqlDbType.DateTime, Fees.BillDate);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_HostelBillGeneration_Step1");
            }
        }
    }
}
