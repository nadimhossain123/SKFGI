using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Payroll
{
    public class TdsChallan
    {

        public DataTable GetIncomeTax(int IntMode, int TMonth, int TYear)
        {
            return DataAccess.Payroll.TdsChallan.GetIncomeTax(IntMode, TMonth, TYear);
        }
        public int Save(Entity.Payroll.TdsChallan tdsChallan)
        {
            return DataAccess.Payroll.TdsChallan.Save(tdsChallan);
        }
        public DataSet GetCompanyDetail(int FinYearId, int IntMode)
        {
            return DataAccess.Payroll.TdsChallan.GetCompanyDetail(FinYearId, IntMode);
        }
    }
}
