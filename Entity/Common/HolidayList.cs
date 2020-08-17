using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public class HolidayList
    {
        public HolidayList()
        {
        }

        public int HolidayListId { get; set; }
        public string HolidayName { get; set; }
        public string HolidayRemarks { get; set; }
        public DateTime HolidayDate { get; set; }
        public int HolidayList_UserId { get; set; }
        public int HolidayList_ModUserId { get; set; }
    }
}
