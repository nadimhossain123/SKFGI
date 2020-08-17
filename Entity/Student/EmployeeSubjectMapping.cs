using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Student
{
    public class EmployeeSubjectMapping
    {
        public EmployeeSubjectMapping()
        {
        }

        public int EmployeeSubjectMappingId { get; set; }
        public int EmployeeId { get; set; }
        public int SubjectId { get; set; }

    }
}
