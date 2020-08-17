using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.HR
{
    public class LeaveType
    {
        public LeaveType()
        {
        }

        public int Save(Entity.HR.LeaveType LeaveType)
        {
            return DataAccess.HR.LeaveType.Save(LeaveType);
        }

        public DataTable GetAll()
        {
            return DataAccess.HR.LeaveType.GetAll();
        }

        public Entity.HR.LeaveType GetAllById(int LeaveTypeId)
        {
            return DataAccess.HR.LeaveType.GetAllById(LeaveTypeId);
        }
    }
}
