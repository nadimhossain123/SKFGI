using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Student
{
    public class StudentBillEntry
    {

        public int Save(Entity.Student.StudentBillEntry SingleBill)
        {
            return DataAccess.Student.StudentBillEntry.Save(SingleBill);
        }
    }
}
