using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.HR
{
    public class ExpenseClaim
    {
        public ExpenseClaim()
        {
        }

        public int ExpenseClaimId { get; set; }
        public string ClaimNo { get; set; }
        public string ClaimTitle { get; set; }
        public string ClaimDescription { get; set; }
        public int EmployeeId { get; set; }
        public int ExpenseTypeId { get; set; }
        public decimal ExpenseAmount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public bool BillSubmitted { get; set; }
        public int ClaimStatusId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool BillReceived { get; set; }
        public string ApproverComment { get; set; }

        public int CashBankLedgerID { get; set; }
        public string TransactionType { get; set; }
        public string ModeOfPayment { get; set; }
        public string ChequeNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string DrawnOn { get; set; }
        public int CreatedBy { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinYrId { get; set; }
        public decimal TotalAmount { get; set; }
        public string ClaimDetails { get; set; }
        public string XMLCashBankVoucherDetails { get; set; }
        public int CBVHeaderID { get; set; }



    }
}
