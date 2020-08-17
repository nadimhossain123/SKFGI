using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Payroll
{
    public class EmployeeSalaryData
    {
        public EmployeeSalaryData()
        {
        }

      
        //Salary Registration
        public static DataSet GetAll(int Month, int Year, int CompanyId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pMonth", SqlDbType.Int, Month);
                oDm.Add("@pYear", SqlDbType.Int, Year);
                oDm.Add("@pCompanyId", SqlDbType.Int, CompanyId);
                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_EmployeeSalaryData_GetAll",ref ds, "tbl");
                
                
            }
        }

        //Individual salary Details
        public static DataSet GetIndividualSalaryDetails(int EmployeeId, int FinancialYear)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@EmployeeId", SqlDbType.Int, EmployeeId);
                oDm.Add("@FinYr", SqlDbType.Int, FinancialYear);
                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_IndividualSalaryDetails", ref ds, "table");


            }
        }

        //Batch job to generate salary for all employees
        public static string MonthlySalaryGeneration(int Month, int Year, int CreatedBy, int CompanyId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pMonth", SqlDbType.Int, Month);
                oDm.Add("@pYear", SqlDbType.Int, Year);
                oDm.Add("@pCreatedBy", SqlDbType.Int, CreatedBy);
                oDm.Add("@pCompanyId", SqlDbType.Int, CompanyId);
                oDm.Add("@pMessage", SqlDbType.VarChar,250, ParameterDirection.InputOutput, "");
                oDm.ExecuteNonQuery("usp_MonthlySalaryGeneration_Step1");
                return (string)oDm["@pMessage"].Value;

            }
        }

        //Finalize salary
        public static void FinalizeSalary(int Month, int Year, int CompanyId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.Text;
                string Sql = string.Format("Update SalaryPayOut Set IsFinalized=1, FinalizeDate=GetDate() where SalaryMonth={0} and SalaryYear={1} and CompanyId={2}", Month, Year, CompanyId);
                oDm.ExecuteNonQuery(Sql);
            }
        }

        //For employees to generate their monthly salary slip
        public static DataSet MonthlyPaySlip(int Month, int Year, int EmployeeId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;

                oDm.Add("@pMonth", SqlDbType.Int, ParameterDirection.Input, Month);
                oDm.Add("@pYear", SqlDbType.Int, ParameterDirection.Input, Year);
                oDm.Add("@pEmployeeId", SqlDbType.Int, ParameterDirection.Input, EmployeeId);

                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_PaySlipGeneration", ref ds, "table");
            }
        }

        
    }
}
