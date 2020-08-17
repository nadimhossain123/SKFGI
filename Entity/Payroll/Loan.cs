using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Payroll
{
    public class Loan
    {
        public Loan()
        {
        }

        public int LoanId { get; set; }
        public int Loan_EmployeeId { get; set; }
        public DateTime LoanApplicationDate { get; set; }
        public DateTime LoanSanctionDate { get; set; }
        public string LoanDescription { get; set; }
        public decimal LoanAmount { get; set; }
        public int LoanTotalMonth { get; set; }
        public decimal LoanInterestRate { get; set; }
        public decimal LoanInterestAmount { get; set; }
        public decimal LoanRefundAmount { get; set; }
        public decimal LoanEMIAmount { get; set; }
        public int LoanDeductionMonth { get; set; }
        public int LoanDeductionYear { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

    }
}
