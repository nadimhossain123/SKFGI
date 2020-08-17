using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Student
{
    public class SectionMaster
    {
        public SectionMaster()
        {
        }

        public DataTable GetAll()
        {
            return DataAccess.Student.SectionMaster.GetAll();
        }
    }
}
