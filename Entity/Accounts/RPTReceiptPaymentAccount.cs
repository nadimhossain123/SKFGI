using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Entity.Accounts
{
    public class RPTReceiptPaymentAccount
    {
        public RPTReceiptPaymentAccount()
        { }

        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinYearId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
