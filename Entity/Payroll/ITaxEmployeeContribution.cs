using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Payroll
{
    public class ITaxEmployeeContribution
    {
        public int ITaxEmployeeContributionId { get; set; }
        public int EmployeeId { get; set; }
        public int ITaxInvestmentHeadId { get; set; }
        public decimal ProposedAmount { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public int FinancialYear { get; set; } 
    }
}
