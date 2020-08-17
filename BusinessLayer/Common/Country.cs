using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class Country
    {
        public Country()
        {
        }

        public DataTable GetAll()
        {
            return DataAccess.Common.Country.GetAll();
        }
    }
}
