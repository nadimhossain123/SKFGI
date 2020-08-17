using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Student
{
    public class MBARegistration
    {
        public DataTable GetAllBatch(Entity.Student.MBARegistration mbaReg)
        {
            return DataAccess.student.MBARegistration.GetAllBatch(mbaReg);
        }

        public int Save(Entity.Student.MBARegistration mbaReg)
        {
            return DataAccess.student.MBARegistration.Save(mbaReg);
        }
        public DataSet GetStudentDetails(Entity.Student.MBARegistration mbaReg)
        {
            return DataAccess.student.MBARegistration.GetStudentDetails(mbaReg);
        }
    }
}
