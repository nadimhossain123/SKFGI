using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Student
{
   public class StudentDropout
    {

        public int Save(Entity.Student.StudentDropout objStuDrop)
        {
            return DataAccess.Student.StudentDropout.Save(objStuDrop);
        }
        public DataTable GetAll(int BatchId)
        {
            return DataAccess.Student.StudentDropout.GetAll(BatchId);
        }

        public void Delete(int Id)
        {
            DataAccess.Student.StudentDropout.Delete(Id);
        }
    }
}
