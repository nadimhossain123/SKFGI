using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Student
{
    public class HostelFeesGeneration
    {
        public HostelFeesGeneration()
        {
        }

        public DataTable GetAllStudent(Entity.Student.HostelFeesGeneration Fees)
        {
            return DataAccess.Student.HostelFeesGeneration.GetAllStudent(Fees);
        }

        public void GenerateFees(Entity.Student.HostelFeesGeneration Fees)
        {
            DataAccess.Student.HostelFeesGeneration.GenerateFees(Fees);
        }
    }
}
