using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Payroll
{
    public  class TdsChallan
    {
        public static int Save(Entity.Payroll.TdsChallan tdsChallan)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@TMonth", SqlDbType.Int, tdsChallan.TMonth);
                oDm.Add("@TYear", SqlDbType.Int, tdsChallan.TYear);
                oDm.Add("@ChequeNo", SqlDbType.VarChar, 100, tdsChallan.ChequeNo);
                if (tdsChallan.ChequeDate.ToString() == "01/01/0001 00:00:00")
                {
                    oDm.Add("@ChequeDate", SqlDbType.Decimal, DBNull.Value);
                }
                else
                {
                    oDm.Add("@ChequeDate", SqlDbType.DateTime, tdsChallan.ChequeDate);
                }
                oDm.Add("@IncomeTax", SqlDbType.Decimal, tdsChallan.IncomeTax);
                oDm.Add("@Interest", SqlDbType.Decimal, tdsChallan.Interest);
                oDm.Add("@EduCess", SqlDbType.Decimal, tdsChallan.EduCess);
                oDm.Add("@Penalty", SqlDbType.Decimal, tdsChallan.Penalty);
                oDm.Add("@Surcharge", SqlDbType.Decimal, tdsChallan.Surcharge);

                oDm.Add("@CreatedBy", SqlDbType.Int, tdsChallan.CreatedBy);
                //oDm.Add("@ModifiedBy", SqlDbType.Int, ptaxChallan.ModifiedBy);
                oDm.Add("@IsFinalized", SqlDbType.Int, tdsChallan.IsFinalized);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteNonQuery("usp_TdsChallanSave");
            }
        }

        public static DataTable GetIncomeTax(int IntMode, int TMonth, int TYear)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@TMonth", SqlDbType.Int, TMonth);
                oDm.Add("@IntMode", SqlDbType.Int, IntMode);
                oDm.Add("@TYear", SqlDbType.Int, TYear);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_TdsChallanSelect");
            }
        }
        public static DataSet GetCompanyDetail(int FinYearId,int IntMode)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@FinYearId", SqlDbType.Int, FinYearId);                
                oDm.Add("@IntMode", SqlDbType.Int, IntMode);
                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_TdsChallanSelect", ref ds, "table");

            }
        }

    }
}
