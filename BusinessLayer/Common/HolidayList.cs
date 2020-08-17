using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class HolidayList
    {
        public HolidayList()
        {
        }

        public int Save(Entity.Common.HolidayList HolidayList)
        {
            return DataAccess.Common.HolidayList.Save(HolidayList);
        }
        public DataTable GetAll()
        {
            return DataAccess.Common.HolidayList.GetAll();
        }

        public Entity.Common.HolidayList GetAllById(int HolidayListId)
        {
            return DataAccess.Common.HolidayList.GetAllById(HolidayListId);
        }

        public void Delete(int HolidayListId)
        {
            DataAccess.Common.HolidayList.Delete(HolidayListId);
        }
    }
}
