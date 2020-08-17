using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class EmployeeAttendance
    {
        public EmployeeAttendance()
        {
        }

        public void BulkSave(int Month, int Year, string AttendanceXML)
        {
            DataAccess.Common.EmployeeAttendance.BulkSave(Month, Year, AttendanceXML);
        }

        public DataTable GetAll(int Month, int Year)
        {
            return DataAccess.Common.EmployeeAttendance.GetAll(Month, Year);
        }
        public void SaveNew(int Month, int Year, int CreatedBy)
        {
            DataAccess.Common.EmployeeAttendance.SaveNew(Month, Year, CreatedBy);
        }
        public  void AbsentUpdate(int Month, int Year, int EmpId, decimal NoOfDays)
        {
            DataAccess.Common.EmployeeAttendance.AbsentUpdate(Month, Year, EmpId, NoOfDays);
        }
        public DataTable GetAllNew(int Month, int Year)
        {
            return DataAccess.Common.EmployeeAttendance.GetAllNew(Month, Year);
        }
        public int UpdateAttendance(Entity.Common.EmployeeAttendance empAttendance)
        {
            return DataAccess.Common.EmployeeAttendance.UpdateAttendance(empAttendance);
        }
        public DataTable GetLeaveDetailById(int Month, int Year, int EmployeeId, int LeaveId)
        {
            return DataAccess.Common.EmployeeAttendance.GetLeaveDetailById(Month, Year, EmployeeId, LeaveId);
        }
        public int LeaveUpdate(int LeaveId, DateTime FromDate, DateTime ToDate, int Month, int Year, int EmpId,int CreatedBy)
        {
            return DataAccess.Common.EmployeeAttendance.LeaveUpdate(LeaveId, FromDate, ToDate, Month, Year, EmpId, CreatedBy);
        }
    }
}
