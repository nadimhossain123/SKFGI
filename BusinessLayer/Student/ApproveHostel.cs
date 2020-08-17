using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Student
{
    public  class ApproveHostel
    {
        public DataSet GetStudentDetails(int IntMode)
        {
            return DataAccess.Student.ApproveHostel.GetStudentDetails(IntMode);
        }
        public DataSet GetStudentDetailsByDate(int IntMode, DateTime Datefrom, DateTime DateTo)
        {
            return DataAccess.Student.ApproveHostel.GetStudentDetailsByDate(IntMode, Datefrom, DateTo);
        }
        public int UpdateHostelApproveList(string ApproveHostelListXML)
        {
            return DataAccess.Student.ApproveHostel.UpdateHostelApproveList(ApproveHostelListXML);
        }
        public int UpdateHostelStudentApproveById(Entity.Student.ApproveHostel objAHE)
        {
            return DataAccess.Student.ApproveHostel.UpdateHostelStudentApproveById(objAHE);
        }
        public int UpdateHostelStudentReleaseById(Entity.Student.ApproveHostel objAHE)
        {
            return DataAccess.Student.ApproveHostel.UpdateHostelStudentReleaseById(objAHE);
        }
    }
}
