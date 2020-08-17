using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Student
{
    public class StreamMaster
    {
        public StreamMaster()
        {
        }

        public int Save(Entity.Student.StreamMaster Stream)
        {
            return DataAccess.Student.StreamMaster.Save(Stream);
        }

        public DataTable GetAll(int CourseId)
        {
            return DataAccess.Student.StreamMaster.GetAll(CourseId);
        }

        public void ChangeCapacity(Entity.Student.StreamMaster Stream)
        {
            DataAccess.Student.StreamMaster.ChangeCapacity(Stream);
        }
    }
}
