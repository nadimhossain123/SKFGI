using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class State
    {
        public State()
        {
        }

        public DataTable GetAll(int CountryId)
        {
            return DataAccess.Common.State.GetAll(CountryId);
        }
    }
}
