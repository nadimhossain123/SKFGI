using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Payroll
{
     public class PTaxChallan
    {
         public static int Save(Entity.Payroll.PTaxChallan ptaxChallan)
         {
             using (DataManager oDm = new DataManager())
             {
                 oDm.Add("@CMonth", SqlDbType.Int, ptaxChallan.CMonth);
                 oDm.Add("@CYear", SqlDbType.Int, ptaxChallan.CYear);
                 oDm.Add("@ChequeNo", SqlDbType.VarChar,100, ptaxChallan.ChequeNo);
                 if ( ptaxChallan.ChequeDate.ToString() == "01/01/0001 00:00:00")
                 {
                     oDm.Add("@ChequeDate", SqlDbType.Decimal, DBNull.Value);
                 }
                 else
                 {
                     oDm.Add("@ChequeDate", SqlDbType.DateTime, ptaxChallan.ChequeDate);
                 }
                 oDm.Add("@LateFees", SqlDbType.Decimal, ptaxChallan.LateFees);
                 oDm.Add("@Penalty", SqlDbType.Decimal, ptaxChallan.Penalty);
                
                 oDm.Add("@Tax", SqlDbType.Decimal, ptaxChallan.Tax);
                 oDm.Add("@CompMoney", SqlDbType.Decimal, ptaxChallan.CompMoney);
                 oDm.Add("@CreatedBy", SqlDbType.Int, ptaxChallan.CreatedBy);
                 //oDm.Add("@ModifiedBy", SqlDbType.Int, ptaxChallan.ModifiedBy);
                 oDm.Add("@IsFinalized", SqlDbType.Int, ptaxChallan.IsFinalized);               
                 oDm.CommandType = CommandType.StoredProcedure;
                 return oDm.ExecuteNonQuery("usp_PtaxChallanSave");
             }
         }
         public static int Update(Entity.Payroll.PTaxChallan ptaxChallan)
         {
             using (DataManager oDm = new DataManager())
             {
                 oDm.Add("@Id", SqlDbType.Int, ptaxChallan.Id);
                 oDm.Add("@CMonth", SqlDbType.Int, ptaxChallan.CMonth);
                 oDm.Add("@CYear", SqlDbType.Int, ptaxChallan.CYear);
                 oDm.Add("@ChequeNo", SqlDbType.VarChar, 100, ptaxChallan.ChequeNo);
                 if (ptaxChallan.ChequeDate.ToString() == "01/01/0001 00:00:00")
                 {
                     oDm.Add("@ChequeDate", SqlDbType.Decimal, DBNull.Value);
                 }
                 else
                 {
                     oDm.Add("@ChequeDate", SqlDbType.DateTime, ptaxChallan.ChequeDate);
                 }
                 oDm.Add("@LateFees", SqlDbType.Decimal, ptaxChallan.LateFees);
                 oDm.Add("@Penalty", SqlDbType.Decimal, ptaxChallan.Penalty);

                 oDm.Add("@Tax", SqlDbType.Decimal, ptaxChallan.Tax);
                 oDm.Add("@CompMoney", SqlDbType.Decimal, ptaxChallan.CompMoney);
                 oDm.Add("@CreatedBy", SqlDbType.Int, ptaxChallan.CreatedBy);
                 //oDm.Add("@ModifiedBy", SqlDbType.Int, ptaxChallan.ModifiedBy);
                 oDm.Add("@IsFinalized", SqlDbType.Int, ptaxChallan.IsFinalized);
                 oDm.CommandType = CommandType.StoredProcedure;
                 return oDm.ExecuteNonQuery("[usp_PtaxChallanUpdate]");
             }
         }
         public static DataTable GetPTax(int IntMode,int CMonth, int CYear)
         {
             using (DataManager oDm = new DataManager())
             {
                 oDm.Add("@CMonth", SqlDbType.Int, CMonth);
                 oDm.Add("@IntMode", SqlDbType.Int, IntMode);
                 oDm.Add("@CYear", SqlDbType.Int, CYear);
                 oDm.CommandType = CommandType.StoredProcedure;
                 return oDm.ExecuteDataTable("usp_PtaxChallanSelect");
             }
         }
         public static DataTable GetPTaxById(int IntMode, int CMonth, int CYear,int Id)
         {
             using (DataManager oDm = new DataManager())
             {
                 oDm.Add("@CMonth", SqlDbType.Int, CMonth);
                 oDm.Add("@IntMode", SqlDbType.Int, IntMode);
                 oDm.Add("@CYear", SqlDbType.Int, CYear);
                 oDm.Add("@Id", SqlDbType.Int, Id);
                 oDm.CommandType = CommandType.StoredProcedure;
                 return oDm.ExecuteDataTable("usp_PtaxChallanSelect");
             }
         }
         public static string GetAmtInWord(decimal Amt)
         {
             using (DataManager oDm = new DataManager())
             {
                 string strQry = "Select dbo.udf_Num_ToWords(" + Amt + ") AmtWords";
                 oDm.CommandType = CommandType.Text;
                 DataTable dt = oDm.ExecuteDataTable(strQry);
                 return dt.Rows[0]["AmtWords"].ToString();             
             }
         }
    }
}
