using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.HR
{
    public class LeaveApplicationConfig
    {
        public LeaveApplicationConfig()
        {
        }

        public static void SaveMaxDayLimit(int MaxDayLimit)
        {
            DataAccess.HR.LeaveApplicationConfig.SaveMaxDayLimit(MaxDayLimit);
        }

        public static int GetMaxDayLimit()
        {
            return DataAccess.HR.LeaveApplicationConfig.GetMaxDayLimit();
        }
    }
}
