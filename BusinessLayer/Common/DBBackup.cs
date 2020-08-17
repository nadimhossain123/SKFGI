using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class DBBackup
    {
        public DBBackup()
        {

        }

        public void Save(Entity.Common.DBBackup backup)
        {
            DataAccess.Common.DBBackup.Save(backup);
        }

        public DataTable GetAll()
        {
            return DataAccess.Common.DBBackup.GetAll();
        }
    }
}
