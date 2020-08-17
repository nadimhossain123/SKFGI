using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Payroll
{
    public class PTaxChallan
    {
        public  int Save(Entity.Payroll.PTaxChallan ptaxChallan)
        {
            return DataAccess.Payroll.PTaxChallan.Save(ptaxChallan);    
        }
        public int Update(Entity.Payroll.PTaxChallan ptaxChallan)
        {
            return DataAccess.Payroll.PTaxChallan.Update(ptaxChallan);
        }
        public DataTable GetPTax(int IntMode, int CMonth, int CYear)
        {
            return DataAccess.Payroll.PTaxChallan.GetPTax(IntMode, CMonth, CYear);
        }
        public DataTable GetPTaxById(int IntMode, int CMonth, int CYear,int Id)
        {
            return DataAccess.Payroll.PTaxChallan.GetPTaxById(IntMode, CMonth, CYear, Id);
        }
        public string GetAmtInWord(decimal Amt)
        {
           return  DataAccess.Payroll.PTaxChallan.GetAmtInWord(Amt);
        }
    }
}
