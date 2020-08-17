using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Student
{
    public class SectionMaster
    {
        public SectionMaster()
        {
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                string sql = "Select id,section_name From tbl_section_master";
                oDm.CommandType = CommandType.Text;
                return oDm.ExecuteDataTable(sql);
            }
        }
    }
}
