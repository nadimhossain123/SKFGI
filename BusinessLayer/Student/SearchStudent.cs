using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Student
{
    public class SearchStudent
    {
        public DataTable GetAllStudent(Entity.Student.SearchStudent searchS)
        {
            return DataAccess.student.SearchStudent.GetAllStudent(searchS);
        }

        public DataTable GetNewAdmissionReport(Entity.Student.SearchStudent searchS)
        {
            return DataAccess.student.SearchStudent.GetNewAdmissionReport(searchS);
        }

        public void ChangeStudentPhoto(Entity.Student.SearchStudent searchS)
        {
            DataAccess.student.SearchStudent.ChangeStudentPhoto(searchS);
        }

        public static int GetStudentCourseId(int StudentId)
        {
            return DataAccess.student.SearchStudent.GetStudentCourseId(StudentId);
        }
        public DataTable GetStudentIDCard(int id)
        {
            return DataAccess.student.SearchStudent.GetStudentIDCard(id);
        }

        //------------Add------------
        public DataTable GetStudentStream(int CourseId)
        {
            return DataAccess.student.SearchStudent.GetStudentStream(CourseId);
        }
        public static int getStreamId(int Id)
        {
            return DataAccess.student.SearchStudent.getStreamId(Id);
        }
        public  static int Update(int Id, int StreamId,int BatchId)
        {
            return DataAccess.student.SearchStudent.Update(Id, StreamId, BatchId);
        }
        public  DataTable GetStudentBatch(int Id)
        {
            return DataAccess.student.SearchStudent.GetStudentBatch(Id);
        }
        public DataTable GetStudentFees(int Id)
        {
            return DataAccess.student.SearchStudent.GetStudentFees(Id);
        }
        public static int getFeesId(int Id)
        {
            return DataAccess.student.SearchStudent.getFeesId(Id);
        }
        public static int UpdateFeesStructure(int Id, int FeesId)
        {
            return DataAccess.student.SearchStudent.UpdateFeesStructure(Id, FeesId);
        }
        //---------------------------


    }
}
