using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Student
{
    public class HostelFeesGeneration
    {
        public HostelFeesGeneration()
        {
        }

        public int batch_id { get; set; }
        public int CompanyID_FK { get; set; }
        public int BranchID_FK { get; set; }
        public int FinYearID_FK { get; set; }
        public int DataFlow { get; set; }
        public int Year { get; set; }
        public int Quarter { get; set; }
        public int CreatedBy { get; set; }
        public string StudentIdXML { get; set; }
        public string feesdetailsxml { get; set; }
        public DateTime BillDate { get; set; }

    }
}
