using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Student
{
    public class StudentSubjectMapping
    {
        public StudentSubjectMapping()
        {
        }

        public void Save(Entity.Student.StudentSubjectMapping mapping)
        {
            DataAccess.Student.StudentSubjectMapping.Save(mapping);
        }

        public DataTable GetAll(Entity.Student.StudentSubjectMapping mapping)
        {
            return DataAccess.Student.StudentSubjectMapping.GetAll(mapping);
        }

        public DataTable GetAllById(int StudentId)
        {
            return DataAccess.Student.StudentSubjectMapping.GetAllById(StudentId);
        }

        public void Delete(int Id)
        {
            DataAccess.Student.StudentSubjectMapping.Delete(Id);
        }
    }
}
