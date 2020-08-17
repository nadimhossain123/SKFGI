using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Accounts
{
    public class RPTReceiptPaymentAccount
    {
        public RPTReceiptPaymentAccount()
        { }

        public static DataTable RPTReceiptPaymentAccount_Report(Entity.Accounts.RPTReceiptPaymentAccount rptReceiptPaymentAccount)
        {
            using (DataManager oDm = new DataManager())
            {
                if(rptReceiptPaymentAccount.CompanyId==0)
                    oDm.Add("@CompanyID_FK", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@CompanyID_FK", SqlDbType.Int, rptReceiptPaymentAccount.CompanyId);
                oDm.Add("@BranchID_FK", SqlDbType.Int, rptReceiptPaymentAccount.BranchId);
                oDm.Add("@FinYearID_FK", SqlDbType.Int, rptReceiptPaymentAccount.FinYearId);
                oDm.Add("@FromDate", SqlDbType.Date, rptReceiptPaymentAccount.FromDate);
                oDm.Add("@ToDate", SqlDbType.Date, rptReceiptPaymentAccount.ToDate);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("spRPT_ReceiptPaymentAccount");
            }
        }
    }
}
