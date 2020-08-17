using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class TermsAndCondition
    {
        public TermsAndCondition()
        {
        }

        public void Save(Entity.Common.TermsAndCondition Terms)
        {
            DataAccess.Common.TermsAndCondition.Save(Terms);
        }

        public DataTable GetAll()
        {
            return DataAccess.Common.TermsAndCondition.GetAll();
        }

        public Entity.Common.TermsAndCondition GetAllById(int TermsId)
        {
            return DataAccess.Common.TermsAndCondition.GetAllById(TermsId);
        }

        public DataTable GetAllEmployeeTerms(int EmployeeId)
        {
            return DataAccess.Common.TermsAndCondition.GetAllEmployeeTerms(EmployeeId);
        }

        public void SaveEmployeeTerms(Entity.Common.TermsAndCondition Terms)
        {
            DataAccess.Common.TermsAndCondition.SaveEmployeeTerms(Terms);
        }
    }
}
