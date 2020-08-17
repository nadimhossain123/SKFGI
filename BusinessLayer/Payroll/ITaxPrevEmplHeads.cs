using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Payroll
{
    public class ITaxPrevEmplHeads
    {
        public int Save(Entity.Payroll.ITaxPrevEmplHeads ITaxPrevEmplHeads)
        {
            return DataAccess.Payroll.ITaxPrevEmplHeads.Save(ITaxPrevEmplHeads);
        }

        public DataTable GetAll()
        {
            return DataAccess.Payroll.ITaxPrevEmplHeads.GetAll();
        }

        public Entity.Payroll.ITaxPrevEmplHeads GetAllById(int ITaxPrevEmplHeadId)
        {
            return DataAccess.Payroll.ITaxPrevEmplHeads.GetAllById(ITaxPrevEmplHeadId);
        }

        public int Delete(int ITaxPrevEmplHeadId)
        {
            return DataAccess.Payroll.ITaxPrevEmplHeads.Delete(ITaxPrevEmplHeadId);
        }
    }
}
