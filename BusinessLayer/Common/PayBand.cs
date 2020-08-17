using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class PayBand
    {
        public PayBand()
        {
        }

        public int Save(Entity.Common.PayBand PayBand)
        {
            return DataAccess.Common.PayBand.Save(PayBand);
        }

        public DataTable GetAll()
        {
            return DataAccess.Common.PayBand.GetAll();
        }

        public Entity.Common.PayBand GetAllById(int PayBandId)
        {
            return DataAccess.Common.PayBand.GetAllById(PayBandId);
        }
        
    }
}
