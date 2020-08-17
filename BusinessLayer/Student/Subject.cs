using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.student
{
    public class Subject
    {
        public Subject()
        {
        }

        public int Save(Entity.Student.Subject Subject)
        {
            return DataAccess.Student.Subject.Save(Subject);
        }

        public DataTable GetAll()
        {
            return DataAccess.Student.Subject.GetAll();
        }

        public DataTable GetAllSubjectByEmployee(int CourseId, int StreamId, int EmployeeId)
        {
            return DataAccess.Student.Subject.GetAllSubjectByEmployee(CourseId, StreamId, EmployeeId);
        }

        public Entity.Student.Subject GetAllById(int SubjectId)
        {
            return DataAccess.Student.Subject.GetAllById(SubjectId);
        }

        public int Delete(int SubjectId)
        {
            return DataAccess.Student.Subject.Delete(SubjectId);
        }

        public int SubjectMinAttendance_Update(bool isPractical, int minAttendance)
        {
            return DataAccess.Student.Subject.SubjectMinAttendance_Update(isPractical, minAttendance);
        }
    }
}
