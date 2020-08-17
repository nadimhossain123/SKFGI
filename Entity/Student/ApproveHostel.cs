using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Student
{
    public class ApproveHostel
    {
        public int StudentId { get; set; }
        public string Hosteldetail { get; set; }
        public DateTime HostelDate { get; set; }
        public int IsHostelApproved { get; set; }
        public int IsHostelRelease { get; set; }
    }
}
