using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Payroll
{
    public class SalaryHead
    {
        public SalaryHead()
        {
        }

        public int Save(Entity.Payroll.SalaryHead SalaryHead)
        {
            return DataAccess.Payroll.SalaryHead.Save(SalaryHead);
        }
        public int UpdateSalaryHead(string SalaryHeadUpdateXML)
        {
            return DataAccess.Payroll.SalaryHead.UpdateSalaryHead(SalaryHeadUpdateXML);
        }
        public DataTable GetAll()
        {
            return DataAccess.Payroll.SalaryHead.GetAll();
        }

        public Entity.Payroll.SalaryHead GetAllById(int SalaryHeadId)
        {
            return DataAccess.Payroll.SalaryHead.GetAllById(SalaryHeadId);
        }
        public DataSet GetAllBySalaryHeadId(int SalaryheadId, int IntMode)
        {
            return DataAccess.Payroll.SalaryHead.GetAllBySalaryHeadId(SalaryheadId, IntMode);
        }
    }
}
