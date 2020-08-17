using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Payroll
{
    public class ITaxSectionMaster
    {
        public ITaxSectionMaster()
        {
        }

        public int Save(Entity.Payroll.ITaxSectionMaster ITaxSectionMaster)
        {
            return DataAccess.Payroll.ITaxSectionMaster.Save(ITaxSectionMaster);
        }

        public DataTable GetAll()
        {
            return DataAccess.Payroll.ITaxSectionMaster.GetAll();
        }

        public Entity.Payroll.ITaxSectionMaster GetAllById(int ITaxSectionId)
        {
            return DataAccess.Payroll.ITaxSectionMaster.GetAllById(ITaxSectionId);
        }

        public int Delete(int ITaxSectionId)
        {
            return DataAccess.Payroll.ITaxSectionMaster.Delete(ITaxSectionId);
        }
    }
}
