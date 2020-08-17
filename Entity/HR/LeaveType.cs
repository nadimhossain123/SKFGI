using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.HR
{
    public class LeaveType
    {
        public LeaveType()
        {
        }

        public int LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        public string Description { get; set; }
        public decimal? LeavePerMonth { get; set; }
        public int? LeavePerYear { get; set; }
        public bool IsCarryForwarded { get; set; }
        public bool IsEncashable { get; set; }
        public int? MaxCarryFwdLimit { get; set; }
        public bool IsPaid { get; set; }
    }
}
