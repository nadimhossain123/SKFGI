using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Student
{
    public class LibraryFine
    {
        public LibraryFine()
        {
        }

        public int LibraryFineId { get; set; }
        public string VoucherNo { get; set; }
        public int StudentId { get; set; }
        public string ReasonForFine { get; set; }
        public decimal FineAmount { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int FeesHeadId { get; set; }
        public int SemNo { get; set; }
        public int CompanyID_FK { get; set; }
        public int BranchID_FK { get; set; }
        public int FinYearID_FK { get; set; }
        public int DataFlow { get; set; }

        

    }
}
