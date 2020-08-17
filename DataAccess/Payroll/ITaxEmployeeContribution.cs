using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Payroll
{
    public class ITaxEmployeeContribution
    {
        public static int Save(Entity.Payroll.ITaxEmployeeContribution ITaxEmployeeContribution)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pITaxEmployeeContributionId", SqlDbType.Int, ITaxEmployeeContribution.ITaxEmployeeContributionId);
                oDm.Add("@pEmployeeId", SqlDbType.Int, ITaxEmployeeContribution.EmployeeId);
                oDm.Add("@pITaxInvestmentHeadId", SqlDbType.Int, ITaxEmployeeContribution.ITaxInvestmentHeadId);
                oDm.Add("@pProposedAmount", SqlDbType.Decimal, ITaxEmployeeContribution.ProposedAmount);

                if (ITaxEmployeeContribution.ApprovedAmount == null)
                {
                    oDm.Add("@pApprovedAmount", SqlDbType.Decimal, DBNull.Value);
                }
                else { oDm.Add("@pApprovedAmount", SqlDbType.Decimal, ITaxEmployeeContribution.ApprovedAmount); }
                oDm.Add("@pFinancialYear", SqlDbType.Int, ITaxEmployeeContribution.FinancialYear);
                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_ITaxEmployeeContribution_Save");
            }
        }

        public static DataTable GetAll(int EmployeeId,string FName, string EmpCode)
        {
            using (DataManager oDm = new DataManager())
            {
                if (EmployeeId == 0) { oDm.Add("@pEmployeeId", SqlDbType.Int, DBNull.Value); }
                else { oDm.Add("@pEmployeeId", SqlDbType.Int, EmployeeId); }

                if (FName.Trim().Length == 0) { oDm.Add("@pFirstName", SqlDbType.VarChar,100, DBNull.Value); }
                else { oDm.Add("@pFirstName", SqlDbType.VarChar,100, FName); }

                if (EmpCode.Trim().Length == 0) { oDm.Add("@pEmpCode", SqlDbType.VarChar, 100, DBNull.Value); }
                else { oDm.Add("@pEmpCode", SqlDbType.VarChar, 100, EmpCode); }

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_ITaxEmployeeContribution_GetAll");
            }
        }

        public static Entity.Payroll.ITaxEmployeeContribution GetAllById(int ITaxEmployeeContributionId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pITaxEmployeeContributionId", SqlDbType.Int, ITaxEmployeeContributionId);
                SqlDataReader dr = oDm.ExecuteReader("usp_ITaxEmployeeContribution_GetAllById");
                Entity.Payroll.ITaxEmployeeContribution ITaxEmployeeContribution = new Entity.Payroll.ITaxEmployeeContribution();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ITaxEmployeeContribution.ITaxEmployeeContributionId = ITaxEmployeeContributionId;
                        ITaxEmployeeContribution.EmployeeId = (dr["EmployeeId"] == DBNull.Value) ? 0 : int.Parse(dr["EmployeeId"].ToString());
                        ITaxEmployeeContribution.ITaxInvestmentHeadId = (dr["ITaxInvestmentHeadId"] == DBNull.Value) ? 0 : int.Parse(dr["ITaxInvestmentHeadId"].ToString());
                        ITaxEmployeeContribution.ProposedAmount = (dr["ProposedAmount"] == DBNull.Value) ? 0 : decimal.Parse(dr["ProposedAmount"].ToString());
                        if (dr["ApprovedAmount"] == DBNull.Value)
                        {
                            ITaxEmployeeContribution.ApprovedAmount=null;
                        }
                        else {ITaxEmployeeContribution.ApprovedAmount=decimal.Parse(dr["ApprovedAmount"].ToString());}
                        ITaxEmployeeContribution.FinancialYear = (dr["FinancialYear"] == DBNull.Value) ? 0 : int.Parse(dr["FinancialYear"].ToString());
                    }
                }
                return ITaxEmployeeContribution;
            }
        }

        public static int Delete(int ITaxEmployeeContributionId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pITaxEmployeeContributionId", SqlDbType.Int, ITaxEmployeeContributionId);
                return oDm.ExecuteNonQuery("usp_ITaxEmployeeContribution_Delete");
            }
        }
    }
}
