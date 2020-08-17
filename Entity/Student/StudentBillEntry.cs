using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Student
{
    public  class StudentBillEntry
    {

        public int CompanyID_FK { get; set; }
        public int BranchID_FK { get; set; }
        public int FinYearID_FK { get; set; }
        public int DataFlow { get; set; }
        public string StudentIdXML { get; set; }//string replace int
        public int SemNo { get; set; }
        public decimal BillAmount { get; set; }
        public int CreatedBy { get; set; }
        public string SingleBillXML { get; set; }
        public DateTime BillDate { get; set; }
    }
}
