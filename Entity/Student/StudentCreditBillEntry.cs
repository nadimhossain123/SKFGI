using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Student
{
    public class StudentCreditBillEntry
    {
        public StudentCreditBillEntry()
        {
        }

        public int CompanyID_FK { get; set; }
        public int BranchID_FK { get; set; }
        public int FinYearID_FK { get; set; }
        public int DataFlow { get; set; }
        public int StudentId { get; set; }
        public int SemNo { get; set; }
        public decimal BillAmount { get; set; }
        public int CreatedBy { get; set; }
        public string CreditBillXML { get; set; }
        public DateTime BillDate { get; set; }

    }
}
