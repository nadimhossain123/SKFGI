using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Student
{
    public class ApproveStudent
    {
        public int intMode { get; set; }
        public int intBatchId { get; set; }
        public int intCourseID { get; set; }
        public int intStudentID { get; set; }
        public int intStreamId { get; set; }
        public int intFeesStructureID { get; set; }

        public int intupdatedBy { get; set; }
        public bool IsHostelFacility { get; set; }
        public bool IsLateral { get; set; }
        public bool TFW { get; set; }

        public int CompanyID_FK { get; set; }
        public int BranchID_FK { get; set; }
        public int FinYearID_FK { get; set; }
        public int DataFlow { get; set; }
        public int HostelFeesId { get; set; }
        
    }
}
