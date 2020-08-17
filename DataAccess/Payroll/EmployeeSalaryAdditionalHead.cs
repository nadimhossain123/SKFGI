using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Payroll
{
    public class EmployeeSalaryAdditionalHead
    {
        public EmployeeSalaryAdditionalHead()
        {
        }

        public static DataTable GetAdditionalHead()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@int_mode", SqlDbType.Int, 3);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_EmployeeSalaryAdditionalHead");
            }
        }

        public static void Save(Entity.Payroll.EmployeeSalaryAdditionalHead AdditionHead)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@int_mode", SqlDbType.Int, 4);
                oDm.Add("@EmployeeId", SqlDbType.Int, AdditionHead.EmployeeId);
                oDm.Add("@SalaryAdditionalHeadId", SqlDbType.Int, AdditionHead.SalaryAdditionalHeadId);
                oDm.Add("@AdditionMonth", SqlDbType.Int, AdditionHead.AdditionMonth);
                oDm.Add("@AdditionYear", SqlDbType.Int, AdditionHead.AdditionYear);
                oDm.Add("@AdditionAmount", SqlDbType.Decimal, AdditionHead.AdditionAmount);
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_EmployeeSalaryAdditionalHead");
            }
        }

        public static DataTable GetAll(Entity.Payroll.EmployeeSalaryAdditionalHead AdditionHead)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@int_mode", SqlDbType.Int, 5);
                if (AdditionHead.EmployeeId > 0)
                    oDm.Add("@EmployeeId", SqlDbType.Int, AdditionHead.EmployeeId);
                else
                    oDm.Add("@EmployeeId", SqlDbType.Int, DBNull.Value);

                if (AdditionHead.SalaryAdditionalHeadId > 0)
                    oDm.Add("@SalaryAdditionalHeadId", SqlDbType.Int, AdditionHead.SalaryAdditionalHeadId);
                else
                    oDm.Add("@SalaryAdditionalHeadId", SqlDbType.Int, DBNull.Value);

                if (AdditionHead.AdditionMonth > 0)
                    oDm.Add("@AdditionMonth", SqlDbType.Int, AdditionHead.AdditionMonth);
                else
                    oDm.Add("@AdditionMonth", SqlDbType.Int, DBNull.Value);

                if (AdditionHead.AdditionYear > 0)
                    oDm.Add("@AdditionYear", SqlDbType.Int, AdditionHead.AdditionYear);
                else
                    oDm.Add("@AdditionYear", SqlDbType.Int, DBNull.Value);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_EmployeeSalaryAdditionalHead");
            }
        }

        public static void Delete(int EmployeeSalaryAdditionalHeadId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@int_mode", SqlDbType.Int, 6);
                oDm.Add("@EmployeeSalaryAdditionalHeadId", SqlDbType.Int, EmployeeSalaryAdditionalHeadId);
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_EmployeeSalaryAdditionalHead");
            }
        }
    }
}
