using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Student
{
    public class StudentAttendance
    {
        public StudentAttendance()
        { 

        }

        public DataSet GetAll(Entity.Student.StudentAttendance attendance)
        {
            return DataAccess.Student.StudentAttendance.GetAll(attendance);
        }

        public string Save(Entity.Student.StudentAttendance attendance)
        {
            return DataAccess.Student.StudentAttendance.Save(attendance);
        }

        public DataTable GetAttendanceReport(Entity.Student.StudentAttendance attendance)
        {
            return DataAccess.Student.StudentAttendance.GetAttendanceReport(attendance);
        }

        public DataSet GetSubjectWiseAttendanceReport(Entity.Student.StudentAttendance attendance)
        {
            return DataAccess.Student.StudentAttendance.GetSubjectWiseAttendanceReport(attendance);
        }

        public int Delete(Entity.Student.StudentAttendance attendance)
        {
            return DataAccess.Student.StudentAttendance.Delete(attendance);
        }
    }
}
