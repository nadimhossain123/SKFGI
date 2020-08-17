using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Accounts
{
    public class RPTReceiptPaymentAccount
    {
        public RPTReceiptPaymentAccount()
        { }

        public DataTable RPTReceiptPaymentAccount_Report(Entity.Accounts.RPTReceiptPaymentAccount rptReceiptPaymentAccount)
        {
            return DataAccess.Accounts.RPTReceiptPaymentAccount.RPTReceiptPaymentAccount_Report(rptReceiptPaymentAccount);
        }
    }
}
