using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.HR
{
    public class ExpenseClaim
    {
        public ExpenseClaim()
        {
        }

        public int Save(Entity.HR.ExpenseClaim Claim)
        {
            return DataAccess.HR.ExpenseClaim.Save(Claim);
        }

        public DataTable GetAll(string FirstName, int ClaimStatusId, int EmployeeId, int ApproverId, string FromDate, string ToDate)
        {
            return DataAccess.HR.ExpenseClaim.GetAll(FirstName, ClaimStatusId, EmployeeId,ApproverId,FromDate,ToDate);
        }

        public DataTable GetAllForDirectorApproval(string FromDate, string ToDate)
        {
            return DataAccess.HR.ExpenseClaim.GetAllForDirectorApproval(FromDate, ToDate);
        }

        public void SaveDirectorApproval(DataTable DTClaim, bool IsApproved)
        {
            DataAccess.HR.ExpenseClaim.SaveDirectorApproval(DTClaim, IsApproved);
        }

        public Entity.HR.ExpenseClaim GetAllById(int ExpenseClaimId)
        {
            return DataAccess.HR.ExpenseClaim.GetAllById(ExpenseClaimId);
        }

        public DataTable GetAllClaimStatus()
        {
            return DataAccess.HR.ExpenseClaim.GetAllClaimStatus();
        }

        public DataTable GetPendingClaim()
        {
            return DataAccess.HR.ExpenseClaim.GetPendingClaim();
        }

        public void SaveClaimReimbursement(Entity.HR.ExpenseClaim Claim)
        {
            DataAccess.HR.ExpenseClaim.SaveClaimReimbursement(Claim);
        }
    }
}
