using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Student
{
    public class StudentSectionMapping
    {
        public StudentSectionMapping()
        {
        }

        public static void UpdateSection(int section_id,string xml_file)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@section_id", SqlDbType.Int, section_id);
                oDm.Add("@xml_file", SqlDbType.Xml, xml_file);
                
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_Student_Section_Mapping");
            }
        }
    }
}
