using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public class EmployeeAttendance
    {
        	public int Id {get; set;}
			public int EmployeeId {get; set;}
			public string EmpCode {get; set;}
			public int Month {get; set;}
			public int Year {get; set;}
			public int TotalDays {get; set;}			
			public decimal Present {get; set;}
			public decimal Absent {get; set;}
			public decimal CL {get; set;}
            public decimal EL { get; set; }
            public decimal Medical { get; set; }
            public decimal SpecialLeave { get; set; }
            public decimal Holiday { get; set; }
            public decimal OffDay { get; set; }
            public decimal TotalPayDay { get; set; }
    }
}
