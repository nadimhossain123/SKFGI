using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Common
{
    public class StatutorySalaryConfig
    {
        public StatutorySalaryConfig()
        {
        }

        public void Save(Entity.Common.StatutorySalaryConfig Config)
        {
            DataAccess.Common.StatutorySalaryConfig.Save(Config);
        }

        public Entity.Common.StatutorySalaryConfig GetAll()
        {
            return DataAccess.Common.StatutorySalaryConfig.GetAll();
        }
    }
}
