using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.HR
{
    public class Leave
    {
        public Leave()
        {
        }

        public int UpdateLeaveStock()
        {
            return DataAccess.HR.Leave.UpdateLeaveStock();
        }

        public int Save(Entity.HR.Leave Leave)
        {
            return DataAccess.HR.Leave.Save(Leave);
        }
        public int SaveDirect(Entity.HR.Leave Leave)
        {
            return DataAccess.HR.Leave.SaveDirect(Leave);
        }

        public DataTable GetAll(string FirstName, int LeaveStatusId, int EmployeeId, int LeaveManagerId, string FromDate, string ToDate, int LeaveTypeId)
        {
            return DataAccess.HR.Leave.GetAll(FirstName, LeaveStatusId, EmployeeId, LeaveManagerId, FromDate, ToDate, LeaveTypeId);
        }

        public DataTable GetAllForDirectorApproval(string FromDate, string ToDate)
        {
            return DataAccess.HR.Leave.GetAllForDirectorApproval(FromDate, ToDate);
        }

        public void SaveDirectorApproval(DataTable DTLeave, bool IsApproved)
        {
            DataAccess.HR.Leave.SaveDirectorApproval(DTLeave, IsApproved);
        }

        public Entity.HR.Leave GetAllById(int LeaveId)
        {
            return DataAccess.HR.Leave.GetAllById(LeaveId);
        }

        public DataTable GetStockBalance(int EmployeeId)
        {
            return DataAccess.HR.Leave.GetStockBalance(EmployeeId);
        }

        public DataTable GetLeaveStatus()
        {
            return DataAccess.HR.Leave.GetLeaveStatus();
        }
        public DataTable RPTEmployeeLeave(DateTime FromDate, DateTime ToDate)
        {
            return DataAccess.HR.Leave.RPTEmployeeLeave(FromDate, ToDate);
        }
        public DataTable RPTEmployeeLeaveBalance(DateTime FromDate, DateTime ToDate)
        {
            return DataAccess.HR.Leave.RPTEmployeeLeaveBalance(FromDate, ToDate);
        }
        public DataTable GetDepartment()
        {
            return DataAccess.HR.Leave.GetDepartment();
        }
        public DataTable GetLeaveDeleteList(string FirstName, int LeaveStatusId, int EmployeeId, string FromDate, string ToDate, int LeaveTypeId)
        {
            return DataAccess.HR.Leave.GetLeaveDeleteList(FirstName, LeaveStatusId, EmployeeId, FromDate, ToDate, LeaveTypeId);
        }
        public int LeaveDelete(int EmployeeId, int LeaveId, int LeaveTypeid, decimal NoOfdays)
        {
           return DataAccess.HR.Leave.LeaveDelete(EmployeeId, LeaveId, LeaveTypeid, NoOfdays);
        }

    }
}
