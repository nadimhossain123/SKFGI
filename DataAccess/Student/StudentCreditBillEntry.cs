using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Student
{
    public class StudentCreditBillEntry
    {
        public StudentCreditBillEntry()
        {
        }

        public static void Save(Entity.Student.StudentCreditBillEntry CreditBill)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@CompanyID_FK", SqlDbType.Int, CreditBill.CompanyID_FK);
                oDm.Add("@BranchID_FK", SqlDbType.Int, CreditBill.BranchID_FK);
                oDm.Add("@FinYearID_FK", SqlDbType.Int, CreditBill.FinYearID_FK);
                oDm.Add("@DataFlow", SqlDbType.Int, CreditBill.DataFlow);
                oDm.Add("@StudentId", SqlDbType.Int, CreditBill.StudentId);
                oDm.Add("@SemNo", SqlDbType.Int, CreditBill.SemNo);
                oDm.Add("@BillAmount", SqlDbType.Decimal, CreditBill.BillAmount);
                oDm.Add("@CreatedBy", SqlDbType.Int, CreditBill.CreatedBy);
                oDm.Add("@CreditBillXML", SqlDbType.Xml, CreditBill.CreditBillXML);
                oDm.Add("@BillDate", SqlDbType.DateTime, CreditBill.BillDate );

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_StudentCreditBillEntry");

            }
        }
      
    }
}
