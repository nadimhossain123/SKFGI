using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Student
{
   public class BTechRegistration
    {
       //public DataTable GetAllBatch(Entity.Student.BTechRegistration BtechReg)
       //{
       //    return DataAccess.student.BTechRegistration.GetAllBatch(BtechReg);
       //}
       public DataTable GetAllCommonSP(Entity.Student.BTechRegistration BtechReg)
       {
           return DataAccess.student.BTechRegistration.GetAllCommonSP(BtechReg);
       }
       public int Save(Entity.Student.BTechRegistration BtechReg)
       {
           return DataAccess.student.BTechRegistration.Save(BtechReg);
       }
       public DataSet GetStudentDetails(Entity.Student.BTechRegistration BtechReg)
       {
           return DataAccess.student.BTechRegistration.GetStudentDetails(BtechReg);
       }
    }
}
