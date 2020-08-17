using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Student
{
    public class StudentAttendanceDetail
    {
        public int AttendanceId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int IsPresent { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string StartingClassTime { get; set; }
        public string EndingClassTime { get; set; }
    }
}
