using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Payroll
{
    public class ITaxInvestmentHeads
    {
        public int Save(Entity.Payroll.ITaxInvestmentHeads ITaxInvestmentHeads)
        {
            return DataAccess.Payroll.ITaxInvestmentHeads.Save(ITaxInvestmentHeads);
        }

        public DataTable GetAll(int SectionId)
        {
            return DataAccess.Payroll.ITaxInvestmentHeads.GetAll(SectionId);
        }

        public Entity.Payroll.ITaxInvestmentHeads GetAllById(int ITaxInvestmentHeadId)
        {
            return DataAccess.Payroll.ITaxInvestmentHeads.GetAllById(ITaxInvestmentHeadId);
        }

        public int Delete(int ITaxInvestmentHeadId)
        {
            return DataAccess.Payroll.ITaxInvestmentHeads.Delete(ITaxInvestmentHeadId);
        }
    }
}
