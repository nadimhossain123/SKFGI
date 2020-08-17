using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public class EmployeeQualification
    {
        public EmployeeQualification()
        {
        }

        public int EmployeeQualificationId { get; set; }
        public int EmployeeQualification_EmployeeId { get; set; }
        public string QualificationName { get; set; }
        public string QualificationBoard { get; set; }
        public int QualificationPassingYear { get; set; }
        public decimal QualificationPercOfMarks { get; set; }
        public string QualificationStream { get; set; }
        public string QualificationType { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

    }
}
