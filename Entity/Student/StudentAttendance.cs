using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Entity.Student
{
    public class StudentAttendance
    {
        public StudentAttendance()
        {
        }

        public int Id { get; set; }
        public int BatchId { get; set; }
        public int CourseId { get; set; }
        public int StreamId { get; set; }
        public int SectionId { get; set; }
        public int SubjectId { get; set; }
        public int SubSubjectId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public int Period { get; set; }
        public bool UpdateAccess { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public string StudentIdXML { get; set; }
        public int StudentId { get; set; }
        public DateTime Fromdate { get; set; }
        public DateTime Todate { get; set; }
        public int SubjectType { get; set; }
        
    }
}
