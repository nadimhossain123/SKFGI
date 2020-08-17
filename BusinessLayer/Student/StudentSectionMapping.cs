using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Student
{
    public class StudentSectionMapping
    {
        public StudentSectionMapping()
        {
        }

        public void UpdateSection(int section_id, string xml_file)
        {
            DataAccess.Student.StudentSectionMapping.UpdateSection(section_id, xml_file);
        }
    }
}
