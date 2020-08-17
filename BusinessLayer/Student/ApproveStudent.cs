using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Student
{
   public class ApproveStudent
    {
       public DataSet GetAllStudent(Entity.Student.ApproveStudent Aps)
       {
           return DataAccess.student.ApproveStudent.GetAllStudent(Aps);
       }
       public DataSet GetStudentById(Entity.Student.ApproveStudent Aps)
       {
           return DataAccess.student.ApproveStudent.GetStudentById(Aps);
       }
        public  DataSet PopulateLoadCombo(Entity.Student.ApproveStudent Aps)
        {
            return DataAccess.student.ApproveStudent.PopulateLoadCombo(Aps);
        }
        public  int SaveDetails(Entity.Student.ApproveStudent Aps)
        {
            return DataAccess.student.ApproveStudent.SaveDetails(Aps);
        }
        public DataSet PrintFees(Entity.Student.ApproveStudent Aps)
        {
            return DataAccess.student.ApproveStudent.PrintFees(Aps);
        }

        public DataTable GetUserBaseApprovedStudentList(int ApprovedBy, DateTime FromDate, DateTime ToDate, bool IsApproved)
        {
            return DataAccess.student.ApproveStudent.GetUserBaseApprovedStudentList(ApprovedBy, FromDate, ToDate, IsApproved);
        }
    }
}
