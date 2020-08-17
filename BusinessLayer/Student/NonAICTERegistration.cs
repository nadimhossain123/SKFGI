using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Student
{
    public class NonAICTERegistration
    {
        public DataTable GetAllCommonSP(Entity.Student.NonAICTERegistration NonAICTEReg)
        {
            return DataAccess.Student.NonAICTERegistration.GetAllCommonSP(NonAICTEReg);
        }
        public int Save(Entity.Student.NonAICTERegistration NonAICTEReg)
        {
            return DataAccess.Student.NonAICTERegistration.Save(NonAICTEReg);
        }
        public DataSet GetStudentDetails(Entity.Student.NonAICTERegistration NonAICTEReg)
        {
            return DataAccess.Student.NonAICTERegistration.GetStudentDetails(NonAICTEReg);
        }
    }
}
