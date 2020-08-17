using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class PTax
    {
        public PTax()
        {
        }

        public int Save(Entity.Common.PTax PTax)
        {
            return DataAccess.Common.PTax.Save(PTax);
        }

        public DataTable GetAll()
        {
            return DataAccess.Common.PTax.GetAll();
        }

        public Entity.Common.PTax GetAllById(int PTaxId)
        {
            return DataAccess.Common.PTax.GetAllById(PTaxId);
        }

        public DataSet PTax_DetailsReport(string fromtoyear)
        {
            return DataAccess.Common.PTax.PTax_DetailsReport(fromtoyear);
        }

        public DataSet PTax_SummeryReport(string fromtoyear)
        {
            return DataAccess.Common.PTax.PTax_SummeryReport(fromtoyear);
        }
    }
}
