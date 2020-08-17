using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Student
{
    public class StudentPromotion
    {
        public StudentPromotion()
        {
        }

        public DataTable GetStudentList(Entity.Student.StudentPromotion Promotion)
        {
            return DataAccess.Student.StudentPromotion.GetStudentList(Promotion);
        }

        public void UpdatePromotion(Entity.Student.StudentPromotion Promotion)
        {
            DataAccess.Student.StudentPromotion.UpdatePromotion(Promotion);
        }
    }
}
