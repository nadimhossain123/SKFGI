using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Student
{
    public class Subject
    {
        public Subject()
        {
        }

        public int SubjectId { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public int ParentSubjectId_FK { get; set; }
        public int CourseId { get; set; }
        public int StreamId { get; set; }
        public int SemNo { get; set; }

        public bool IsPractical { get; set; }

        public int MinAttendence { get; set; }
    }
}
