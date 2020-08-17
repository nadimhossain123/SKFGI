using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public class EmployeeWork
    {
        public EmployeeWork()
        {
        }

        public int EmployeeWorkId { get; set; }
        public int EmployeeWork_EmployeeId { get; set; }
        public string CompanyName { get; set; }
        public string WorkPeriod { get; set; }
        public string WorkDesignation { get; set; }
        public string WorkResponsibilities { get; set; }
        public decimal WorkSalary { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
