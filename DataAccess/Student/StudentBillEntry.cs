using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Student
{
     public class StudentBillEntry
    {
         public static int Save(Entity.Student.StudentBillEntry SingleBill)
         {
             using (DataManager oDm = new DataManager())
             {
                 oDm.Add("@CompanyID_FK", SqlDbType.Int, SingleBill.CompanyID_FK);
                 oDm.Add("@BranchID_FK", SqlDbType.Int, SingleBill.BranchID_FK);
                 oDm.Add("@FinYearID_FK", SqlDbType.Int, SingleBill.FinYearID_FK);
                 oDm.Add("@DataFlow", SqlDbType.Int, SingleBill.DataFlow);
                 //oDm.Add("@StudentId", SqlDbType.Int, SingleBill.StudentId);
                 oDm.Add("@SemNo", SqlDbType.Int, SingleBill.SemNo);
                 oDm.Add("@BillAmount", SqlDbType.Decimal, SingleBill.BillAmount);
                 oDm.Add("@CreatedBy", SqlDbType.Int, SingleBill.CreatedBy);
                 oDm.Add("@SingleBillXML", SqlDbType.Xml, SingleBill.SingleBillXML);
                 oDm.Add("@BillDate", SqlDbType.DateTime, SingleBill.BillDate);
                 oDm.Add("@StudentIdXML", SqlDbType.Xml, SingleBill.StudentIdXML);

                 oDm.CommandType = CommandType.StoredProcedure;
                 return  oDm.ExecuteNonQuery("[usp_StudentSingleBillEntry]");

             }
         }
    }
}
