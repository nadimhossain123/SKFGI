using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class PTaxDetails
    {
        public PTaxDetails()
        {
        }

        public int Save(Entity.Common.PTaxDetails PTaxDetails)
        {
           return DataAccess.Common.PTaxDetails.Save(PTaxDetails);
        }

        public DataTable GetAll(int PTaxId)
        {
            return DataAccess.Common.PTaxDetails.GetAll(PTaxId);
        }
        public void Delete(int PTaxDetailsId)
        {
            DataAccess.Common.PTaxDetails.Delete(PTaxDetailsId);
        }
    }
}
