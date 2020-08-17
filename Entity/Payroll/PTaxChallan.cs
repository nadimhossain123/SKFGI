using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Payroll
{
    public class PTaxChallan
    {

        public int   Id{get; set;}
        public int CMonth{get; set;}
        public int  CYear{get; set;}
        public string  ChequeNo{get; set;}
        public DateTime ChequeDate{get; set;}
        public decimal LateFees{get; set;}
        public decimal Penalty{get; set;}
        public decimal CompMoney{get; set;}
        public decimal Tax{get; set;}
        public int CreatedBy{get; set;}
        public DateTime CreatedDate{get; set;}
        public int ModifiedBy{get; set;}
        public DateTime ModifiedDate { get; set; }
        public Boolean IsFinalized { get; set; }

    }
}
