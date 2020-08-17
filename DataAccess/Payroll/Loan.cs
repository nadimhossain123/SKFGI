using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Payroll
{
    public class Loan
    {
        public Loan()
        {
        }

        public static void Save(Entity.Payroll.Loan Loan)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pLoanId", SqlDbType.Int, ParameterDirection.Input, Loan.LoanId);
                oDm.Add("@pLoan_EmployeeId", SqlDbType.Int, ParameterDirection.Input, Loan.Loan_EmployeeId);
                oDm.Add("@pLoanApplicationDate", SqlDbType.DateTime, ParameterDirection.Input, Loan.LoanApplicationDate);
                oDm.Add("@pLoanSanctionDate", SqlDbType.DateTime, ParameterDirection.Input, Loan.LoanSanctionDate);
                oDm.Add("@pLoanDescription", SqlDbType.VarChar,200, ParameterDirection.Input, Loan.LoanDescription);
                oDm.Add("@pLoanAmount", SqlDbType.Decimal, ParameterDirection.Input, Loan.LoanAmount);

                oDm.Add("@pLoanTotalMonth", SqlDbType.Int, ParameterDirection.Input, Loan.LoanTotalMonth);
                oDm.Add("@pLoanInterestRate", SqlDbType.Decimal, ParameterDirection.Input, Loan.LoanInterestRate);
                oDm.Add("@pLoanInterestAmount", SqlDbType.Decimal, ParameterDirection.Input, Loan.LoanInterestAmount);
                oDm.Add("@pLoanRefundAmount", SqlDbType.Decimal, ParameterDirection.Input, Loan.LoanRefundAmount);

                oDm.Add("@pLoanEMIAmount", SqlDbType.Decimal, ParameterDirection.Input, Loan.LoanEMIAmount);
                oDm.Add("@pLoanDeductionMonth", SqlDbType.Int, ParameterDirection.Input, Loan.LoanDeductionMonth);
                oDm.Add("@pLoanDeductionYear", SqlDbType.Int, ParameterDirection.Input, Loan.LoanDeductionYear);
                oDm.Add("@pCreatedBy", SqlDbType.Int, ParameterDirection.Input, Loan.CreatedBy);
                oDm.Add("@pModifiedBy", SqlDbType.Int, ParameterDirection.Input, Loan.ModifiedBy);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_Loan_Save");

            }
        }

        public static DataTable GetAll(int EmployeeId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pLoan_EmployeeId", SqlDbType.Int, ParameterDirection.Input, EmployeeId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_Loan_GetAll");
            }
        }

        public static Entity.Payroll.Loan GetAllById(int LoanId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pLoanId", SqlDbType.Int, ParameterDirection.Input, LoanId);

                SqlDataReader dr = oDm.ExecuteReader("usp_Loan_GetAllById");
                Entity.Payroll.Loan Loan = new Entity.Payroll.Loan();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Loan.LoanId = LoanId;
                        Loan.Loan_EmployeeId = (dr[1] == DBNull.Value) ? 0 : int.Parse(dr[1].ToString());
                        Loan.LoanApplicationDate = (dr[2] == DBNull.Value) ? DateTime.Now : DateTime.Parse(dr[2].ToString());
                        Loan.LoanSanctionDate = (dr[3] == DBNull.Value) ? DateTime.Now : DateTime.Parse(dr[3].ToString());
                        Loan.LoanDescription = (dr[4] == DBNull.Value) ? "" : dr[4].ToString();
                        Loan.LoanAmount = (dr[5] == DBNull.Value) ? 0 : decimal.Parse(dr[5].ToString());

                        Loan.LoanTotalMonth = (dr[6] == DBNull.Value) ? 0 : int.Parse(dr[6].ToString());
                        Loan.LoanInterestRate = (dr[7] == DBNull.Value) ? 0 : decimal.Parse(dr[7].ToString());
                        Loan.LoanInterestAmount = (dr[8] == DBNull.Value) ? 0 : decimal.Parse(dr[8].ToString());
                        Loan.LoanRefundAmount = (dr[9] == DBNull.Value) ? 0 : decimal.Parse(dr[9].ToString());
                        Loan.LoanEMIAmount = (dr[10] == DBNull.Value) ? 0 : decimal.Parse(dr[10].ToString());

                        Loan.LoanDeductionMonth = (dr[11] == DBNull.Value) ? DateTime.Now.Month : int.Parse(dr[11].ToString());
                        Loan.LoanDeductionYear = (dr[12] == DBNull.Value) ? DateTime.Now.Year : int.Parse(dr[12].ToString());

                    }
                }
                return Loan;
            }
        }

        public static void Delete(int LoanId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pLoanId", SqlDbType.Int, ParameterDirection.Input, LoanId);
                oDm.ExecuteNonQuery("usp_Loan_Delete");
            }
        }
    }
}
