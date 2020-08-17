using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Student
{
    public class StudentSubjectMapping
    {
        public StudentSubjectMapping()
        {
        }

        public int batch_id { get; set; }
        public int CourseId { get; set; }
        public int StreamId { get; set; }
        public int ElectiveSubjectId { get; set; }
        public string StudentIdXML { get; set; }

    }
}
