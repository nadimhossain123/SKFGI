using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Student
{
    public class StreamGroup
    {
        public DataTable GetParentStream(Entity.Student.StreamGroup stremGroup)
        {
            return DataAccess.student.StreamGroup.GetParentStream(stremGroup);
        }
        public DataTable GetFeesHead(Entity.Student.StreamGroup stremGroup)
        {
            return DataAccess.student.StreamGroup.GetFeesHead(stremGroup);
        }
        public DataSet GetLoad(Entity.Student.StreamGroup stremGroup)
        {
            return DataAccess.student.StreamGroup.GetLoad(stremGroup);
        }
        public int SaveHeader(Entity.Student.StreamGroup stremGroup)
        {
            return DataAccess.student.StreamGroup.SaveHeader(stremGroup);
        }
        public int SaveDetails(Entity.Student.StreamGroup stremGroup)
        {
            return DataAccess.student.StreamGroup.SaveDetails(stremGroup);
        }
        public int SaveBatch(Entity.Student.StreamGroup stremGroup)
        {
            return DataAccess.student.StreamGroup.SaveBatch(stremGroup);
        }

        public DataTable GetOtherFeesHead()
        {
            return DataAccess.student.StreamGroup.GetOtherFeesHead();
        }

        public int SaveFeesHead(Entity.Student.StreamGroup stremGroup)
        {
            return DataAccess.student.StreamGroup.SaveFeesHead(stremGroup);
        }

        public DataTable GetAllFeesHead()
        {
            return DataAccess.student.StreamGroup.GetAllFeesHead();
        }

        public DataTable GetAllFeesHeadById(int feesId)
        {
            return DataAccess.student.StreamGroup.GetAllFeesHeadById(feesId);
        }

        public DataTable AllFees(Entity.Student.StreamGroup stremGroup)
        {
            return DataAccess.student.StreamGroup.AllFees(stremGroup);
        }
        public DataTable FeesBasedOnID(Entity.Student.StreamGroup stremGroup)
        {
            return DataAccess.student.StreamGroup.FeesBasedOnID(stremGroup);
        }

        public DataTable GetAllHostelFeesHead()
        {
            return DataAccess.student.StreamGroup.GetAllHostelFeesHead();
        }

        public DataTable GetAllSemesterFeesHead()
        {
            return DataAccess.student.StreamGroup.GetAllSemesterFeesHead();
        }
    }
}
