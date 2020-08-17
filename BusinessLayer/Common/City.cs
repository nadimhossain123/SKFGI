using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class City
    {
        public City()
        {
        }

        public DataTable GetAll(int StateId)
        {
            return DataAccess.Common.City.GetAll(StateId);
        }
    }
}
