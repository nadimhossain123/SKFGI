using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Payroll
{
    public class EmployeeSalaryDeductionHead
    {
        public EmployeeSalaryDeductionHead()
        {
        }

        public static DataTable GetActiveEmployee(int CompanyId)
        {
            using (DataManager oDm = new DataManager())
            {
                string sql = string.Format(@"select EmployeeId,EmpCode + ' - ' + upper(FirstName + ' ' + MiddleName + ' ' + LastName) FullName from Employee where IsActive=1 and CompanyId={0} order by EmpCode", CompanyId);
                oDm.CommandType = CommandType.Text;
                return oDm.ExecuteDataTable(sql);
            }
        }

        public static DataTable GetDeductionHead()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@int_mode", SqlDbType.Int, 3);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_EmployeeSalaryDeductionHead");
            }
        }

        public static void Save(Entity.Payroll.EmployeeSalaryDeductionHead DeductionHead)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@int_mode", SqlDbType.Int, 4);
                oDm.Add("@EmployeeId", SqlDbType.Int, DeductionHead.EmployeeId);
                oDm.Add("@SalaryDeductionHeadId", SqlDbType.Int, DeductionHead.SalaryDeductionHeadId);
                oDm.Add("@DeductionMonth", SqlDbType.Int, DeductionHead.DeductionMonth);
                oDm.Add("@DeductionYear", SqlDbType.Int, DeductionHead.DeductionYear);
                oDm.Add("@DeductionAmount", SqlDbType.Decimal, DeductionHead.DeductionAmount);
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_EmployeeSalaryDeductionHead");
            }
        }

        public static DataTable GetAll(Entity.Payroll.EmployeeSalaryDeductionHead DeductionHead)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@int_mode", SqlDbType.Int, 5);
                if (DeductionHead.EmployeeId > 0)
                    oDm.Add("@EmployeeId", SqlDbType.Int, DeductionHead.EmployeeId);
                else
                    oDm.Add("@EmployeeId", SqlDbType.Int, DBNull.Value);

                if (DeductionHead.SalaryDeductionHeadId > 0)
                    oDm.Add("@SalaryDeductionHeadId", SqlDbType.Int, DeductionHead.SalaryDeductionHeadId);
                else
                    oDm.Add("@SalaryDeductionHeadId", SqlDbType.Int, DBNull.Value);

                if (DeductionHead.DeductionMonth > 0)
                    oDm.Add("@DeductionMonth", SqlDbType.Int, DeductionHead.DeductionMonth);
                else
                    oDm.Add("@DeductionMonth", SqlDbType.Int, DBNull.Value);

                if (DeductionHead.DeductionYear > 0)
                    oDm.Add("@DeductionYear", SqlDbType.Int, DeductionHead.DeductionYear);
                else
                    oDm.Add("@DeductionYear", SqlDbType.Int, DBNull.Value);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_EmployeeSalaryDeductionHead");
            }
        }

        public static void Delete(int EmployeeSalaryDeductionHeadId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@int_mode", SqlDbType.Int, 6);
                oDm.Add("@EmployeeSalaryDeductionHeadId", SqlDbType.Int, EmployeeSalaryDeductionHeadId);
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_EmployeeSalaryDeductionHead");
            }
        }
    }
}
