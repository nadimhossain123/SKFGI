using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.HR
{
    public class Leave
    {
        public Leave()
        {
        }

        public int LeaveId { get; set; }
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal NoOfDays { get; set; }
        public int LeaveStatusId { get; set; }
        public string Purpose { get; set; }
        public string Comment { get; set; }
        public bool isClassAdjusted { get; set; }
        public bool isExamDutyDuringLeave { get; set; }
        public bool IsAdjustment { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LeaveManagerId { get; set; }
        public bool IsDirectorApproved { get; set; }
        public bool? IsHalfDayApplied { get; set; }
        public int Count { get; set; }

    }
}
