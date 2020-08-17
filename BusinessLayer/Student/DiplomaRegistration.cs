using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Student
{
    public class DiplomaRegistration
    {
        //Starting of Save Function
        public int Save(Entity.Student.DiplomaRegistration DipReg)
        {
            return DataAccess.Student.DiplomaRegistration.Save(DipReg);
        }
        //End of Save Function


        //Starting of GetStudent Details Function
        public DataSet GetStudentDetails(Entity.Student.DiplomaRegistration DipReg)
        {
            return DataAccess.Student.DiplomaRegistration.GetStudentDetails(DipReg);
        }
        //End of GetStudent Details Function


    }
}
