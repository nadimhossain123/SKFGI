using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Payroll
{
     public class EmployeeIncrement
    {
         public DataSet GetAllByDate(DateTime DateFrom, DateTime DateTo, int IntMode)
         {
             return DataAccess.Payroll.EmployeeIncrement.GetAllByDate(DateFrom, DateTo, IntMode);
         }
         public int UpdateEmployeeIncrement(string IncrementListXml, DateTime IncrDate, int CreatedBy)
         {
             return DataAccess.Payroll.EmployeeIncrement.UpdateEmployeeIncrement(IncrementListXml, IncrDate, CreatedBy);
         }
    }
}
