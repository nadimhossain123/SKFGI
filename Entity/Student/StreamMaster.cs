using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Student
{
    public class StreamMaster
    {
        public StreamMaster()
        {
        }

        public int StreamId { get; set; }
        public int CourseId { get; set; }
        public string StreamName { get; set; }
        public int Capacity { get; set; }
    }
}
