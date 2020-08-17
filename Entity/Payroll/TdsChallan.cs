using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Payroll
{
    public class TdsChallan
    {

        public int Id{get; set;}
        public string ChequeNo { get; set; }
        public DateTime ChequeDate { get; set; }
        public int TMonth{get; set;}
        public int TYear{get; set;}
        public decimal IncomeTax{get; set;}
        public decimal Surcharge{get; set;}
        public decimal EduCess{get; set;}
        public decimal Interest{get; set;}
        public decimal Penalty{get; set;}
        public Boolean IsFinalized{get; set;}
        public int CreatedBy{get; set;}
        public DateTime CreatedDate{get; set;}
        public int ModifiedBy{get; set;}
        public DateTime ModifiedDate { get; set; }

    }
}
