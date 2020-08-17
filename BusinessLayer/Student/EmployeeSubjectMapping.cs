using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Student
{
    public class EmployeeSubjectMapping
    {
        public EmployeeSubjectMapping()
        {
        }

        public void Save(Entity.Student.EmployeeSubjectMapping Mapping)
        {
            DataAccess.Student.EmployeeSubjectMapping.Save(Mapping);
        }

        public DataTable GetAll(string FirstName)
        {
            return DataAccess.Student.EmployeeSubjectMapping.GetAll(FirstName);
        }

        public DataTable GetAllById(int EmployeeId)
        {
            return DataAccess.Student.EmployeeSubjectMapping.GetAllById(EmployeeId);
        }

        public void Delete(int EmployeeSubjectMappingId)
        {
            DataAccess.Student.EmployeeSubjectMapping.Delete(EmployeeSubjectMappingId);
        }
    }
}
