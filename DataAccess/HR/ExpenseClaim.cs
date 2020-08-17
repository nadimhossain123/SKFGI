using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.HR
{
    public class ExpenseClaim
    {
        public ExpenseClaim()
        {
        }

        public static int Save(Entity.HR.ExpenseClaim Claim)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pExpenseClaimId", SqlDbType.Int, ParameterDirection.InputOutput, Claim.ExpenseClaimId);
                oDm.Add("@pClaimTitle", SqlDbType.VarChar, 150, Claim.ClaimTitle);
                oDm.Add("@pClaimDescription", SqlDbType.VarChar, 250, Claim.ClaimDescription);
                oDm.Add("@pEmployeeId", SqlDbType.Int, Claim.EmployeeId);
                oDm.Add("@pExpenseTypeId", SqlDbType.Int, Claim.ExpenseTypeId);
                oDm.Add("@pExpenseAmount", SqlDbType.Decimal, Claim.ExpenseAmount);
                oDm.Add("@pExpenseDate", SqlDbType.DateTime, Claim.ExpenseDate);
                oDm.Add("@pBillSubmitted", SqlDbType.Bit, Claim.BillSubmitted);
                oDm.Add("@pClaimStatusId", SqlDbType.Int, Claim.ClaimStatusId);
                
                if (Claim.PaymentDate == null)
                {
                    oDm.Add("@pPaymentDate", SqlDbType.DateTime, DBNull.Value);
                }
                else { oDm.Add("@pPaymentDate", SqlDbType.DateTime, Claim.PaymentDate); }

                oDm.Add("@pBillReceived", SqlDbType.Bit, Claim.BillReceived);
                oDm.Add("@pApproverComment", SqlDbType.VarChar, 200, Claim.ApproverComment);

                oDm.CommandType = CommandType.StoredProcedure;
                int RowsAffected= oDm.ExecuteNonQuery("usp_ExpenseClaim_Save");
                Claim.ExpenseClaimId = (int)oDm["@pExpenseClaimId"].Value;
                return RowsAffected;
            }
        }

        public static DataTable GetAll(string FirstName, int ClaimStatusId, int EmployeeId, int ApproverId, string FromDate, string ToDate)
        {
            using (DataManager oDm = new DataManager())
            {
                if (FirstName.Trim().Length == 0) { oDm.Add("@pFirstName", SqlDbType.VarChar, 100, DBNull.Value); }
                else { oDm.Add("@pFirstName", SqlDbType.VarChar, 100, FirstName); }

                if (ClaimStatusId == 0)  {oDm.Add("@pClaimStatusId", SqlDbType.Int, DBNull.Value); }
                else { oDm.Add("@pClaimStatusId", SqlDbType.Int, ClaimStatusId); }

                if (EmployeeId == 0) { oDm.Add("@pEmployeeId", SqlDbType.Int, DBNull.Value); }
                else { oDm.Add("@pEmployeeId", SqlDbType.Int, EmployeeId); }

                if (ApproverId == 0) { oDm.Add("@pApproverId", SqlDbType.Int, DBNull.Value); }
                else { oDm.Add("@pApproverId", SqlDbType.Int, ApproverId); }

                if (FromDate.Trim().Length == 0) { oDm.Add("@pFromDate", SqlDbType.DateTime, DBNull.Value); }
                else { oDm.Add("@pFromDate", SqlDbType.DateTime, Convert.ToDateTime(FromDate)); }

                if (ToDate.Trim().Length == 0) { oDm.Add("@pToDate", SqlDbType.DateTime, DBNull.Value); }
                else { oDm.Add("@pToDate", SqlDbType.DateTime, Convert.ToDateTime(ToDate)); }

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_ExpenseClaim_GetAll");
            }
        }

        public static DataTable GetAllForDirectorApproval(string FromDate, string ToDate)
        {
            using (DataManager oDm = new DataManager())
            {
                if (FromDate.Trim().Length == 0) { oDm.Add("@pFromDate", SqlDbType.DateTime, DBNull.Value); }
                else { oDm.Add("@pFromDate", SqlDbType.DateTime, Convert.ToDateTime(FromDate)); }

                if (ToDate.Trim().Length == 0) { oDm.Add("@pToDate", SqlDbType.DateTime, DBNull.Value); }
                else { oDm.Add("@pToDate", SqlDbType.DateTime, Convert.ToDateTime(ToDate)); }

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_ExpenseClaim_GetForDirectorApproval");
            }
        }

        public static void SaveDirectorApproval(DataTable DTClaim, bool IsApproved)
        {
            using (DataManager oDm = new DataManager())
            {
                string ClaimXML = string.Empty;
                if (DTClaim != null && DTClaim.Rows.Count > 0)
                {
                    using (DataSet ds = new DataSet())
                    {
                        ds.Tables.Add(DTClaim);

                        ClaimXML = ds.GetXml();
                    }
                }

                oDm.Add("@pClaimDetails", SqlDbType.Xml, ClaimXML);
                oDm.Add("@pIsDirectorApproved", SqlDbType.Bit, IsApproved);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_ExpenseClaim_SaveDirectorApproval");
            }
        }

        public static Entity.HR.ExpenseClaim GetAllById(int ExpenseClaimId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pExpenseClaimId", SqlDbType.Int, ParameterDirection.Input, ExpenseClaimId);
                SqlDataReader dr = oDm.ExecuteReader("usp_ExpenseClaim_GetAllById");
                Entity.HR.ExpenseClaim Claim = new Entity.HR.ExpenseClaim();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Claim.ExpenseClaimId = ExpenseClaimId;
                        Claim.ClaimTitle = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                        Claim.ClaimDescription = (dr[2] == DBNull.Value) ? "" : dr[2].ToString();
                        Claim.ExpenseTypeId = (dr[3] == DBNull.Value) ? 0 : int.Parse(dr[3].ToString());
                        Claim.ExpenseAmount = (dr[4] == DBNull.Value) ? 0 : decimal.Parse(dr[4].ToString());

                        Claim.ExpenseDate = (dr[5] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(dr[5].ToString());
                        Claim.BillSubmitted = (dr[6] == DBNull.Value) ? false : bool.Parse(dr[6].ToString());

                        Claim.ClaimStatusId = (dr[7] == DBNull.Value) ? 0 : int.Parse(dr[7].ToString());
                        if (dr[8] == DBNull.Value) { Claim.PaymentDate = null; }
                        else { Claim.PaymentDate = DateTime.Parse(dr[8].ToString()); }

                        Claim.BillReceived = (dr[9] == DBNull.Value) ? false : bool.Parse(dr[9].ToString());
                        Claim.ApproverComment = (dr[10] == DBNull.Value) ? "" : dr[10].ToString();
                        Claim.ClaimNo = (dr[11] == DBNull.Value) ? "" : dr[11].ToString();
                        Claim.EmployeeId = (dr[12] == DBNull.Value) ? 0 : int.Parse(dr[12].ToString());
                    }
                }
                return Claim;
            }
        }

        public static DataTable GetAllClaimStatus()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_ExpenseClaimStatus_GetAll");
            }
        }

        public static DataTable GetPendingClaim()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_ExpenseClaim_GetPendingClaim");
            }
        }

        public static void SaveClaimReimbursement(Entity.HR.ExpenseClaim Claim)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@CashBankLedgerID", SqlDbType.Int, Claim.CashBankLedgerID);
                oDm.Add("@PaymentDate", SqlDbType.DateTime, Claim.PaymentDate);
                oDm.Add("@TransactionType", SqlDbType.VarChar,40, Claim.TransactionType);
                oDm.Add("@ModeOfPayment", SqlDbType.VarChar,15, Claim.ModeOfPayment);
                oDm.Add("@ChequeNo", SqlDbType.VarChar,30, Claim.ChequeNo);

                if (Claim.ChequeDate == null)
                { oDm.Add("@ChequeDate", SqlDbType.DateTime, DBNull.Value); }
                else { oDm.Add("@ChequeDate", SqlDbType.DateTime, Claim.ChequeDate); }

                oDm.Add("@DrawnOn", SqlDbType.VarChar,50, Claim.DrawnOn);

                oDm.Add("@CompanyId", SqlDbType.Int, Claim.CompanyId);
                oDm.Add("@BranchId", SqlDbType.Int, Claim.BranchId);
                oDm.Add("@FinYrId", SqlDbType.Int, Claim.FinYrId);
                oDm.Add("@TotalAmount", SqlDbType.Decimal, Claim.TotalAmount);

                oDm.Add("@ClaimDetails", SqlDbType.Xml, Claim.ClaimDetails);
                oDm.Add("@XMLCashBankVoucherDetails", SqlDbType.Xml, Claim.XMLCashBankVoucherDetails);
                oDm.Add("@CreatedBy", SqlDbType.Int, Claim.CreatedBy);
                oDm.Add("@CBVHeaderID", SqlDbType.Int,ParameterDirection.InputOutput, Claim.CBVHeaderID);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_ExpenseClaim_SaveReimbursement");

                Claim.CBVHeaderID = (int)oDm["@CBVHeaderID"].Value;
                
            }
        }
    }
}
