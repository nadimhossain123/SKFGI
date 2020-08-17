using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Student
{
    public class HostelFeesConfig
    {
        public HostelFeesConfig()
        {
        }

        public int Save(Entity.Student.HostelFeesConfig Hostel)
        {
            return DataAccess.Student.HostelFeesConfig.Save(Hostel);
        }

        public DataTable GetAll(int? batch_id)
        {
            return DataAccess.Student.HostelFeesConfig.GetAll(batch_id);
        }

        public DataSet GetAllById(int id)
        {
            return DataAccess.Student.HostelFeesConfig.GetAllById(id);
        }
    }
}
