using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.HR
{
     public class LeaveManagerChange
    {
         public DataSet GetAllByEmployeeId(int EmployeeId, int IntMode)
         {
             return DataAccess.HR.LeaveManagerChange.GetAllByEmployeeId(EmployeeId, IntMode);
         }
         public int UpdateLeaveManager(string LeaveManagerUpdateXml)
         {
             return DataAccess.HR.LeaveManagerChange.UpdateLeaveManager(LeaveManagerUpdateXml);
         }
    }
}
