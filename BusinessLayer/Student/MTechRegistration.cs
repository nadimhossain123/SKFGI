using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Student
{
    public class MTechRegistration
    {
        public MTechRegistration()
        {
        }

        public int Save(Entity.Student.MTechRegistration mtechReg)
        {
            return DataAccess.Student.MTechRegistration.Save(mtechReg);
        }

        public DataSet GetStudentDetails(Entity.Student.MTechRegistration mtechReg)
        {
            return DataAccess.Student.MTechRegistration.GetStudentDetails(mtechReg);
        }
    }
}
