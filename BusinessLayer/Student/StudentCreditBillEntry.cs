using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Student
{
    public class StudentCreditBillEntry
    {
        public StudentCreditBillEntry()
        {
        }

        public void Save(Entity.Student.StudentCreditBillEntry CreditBill)
        {
            DataAccess.Student.StudentCreditBillEntry.Save(CreditBill);
        }
       
    }
}
