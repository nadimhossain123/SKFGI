using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Student
{
    public class SemFeesGeneration
    {
        public SemFeesGeneration()
        {
        }

        public int CompanyID_FK { get; set; }
        public int BranchID_FK { get; set; }
        public int FinYearID_FK { get; set; }
        public int DataFlow { get; set; }
        public int CourseId { get; set; }
        public int batch_id { get; set; }
        public int StreamId { get; set; }
        public int SemNo { get; set; }
        public int FeesHeadId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime BillDate { get; set; }
        public int RowsAffected { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool ShowZeroDueBal { get; set; }

    }
}
