using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Common
{
    public class EmployeeOfficial
    {
        public EmployeeOfficial()
        {
        }

        public void Save(Entity.Common.EmployeeOfficial Official)
        {
            DataAccess.Common.EmployeeOfficial.Save(Official);
        }

        public Entity.Common.EmployeeOfficial GetAllByEmployeeId(int EmployeeId)
        {
            return DataAccess.Common.EmployeeOfficial.GetAllByEmployeeId(EmployeeId);
        }
    }
}
