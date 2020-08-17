using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Payroll
{
    public class ITaxEmployeePrevEmplDetails
    {
        public static int Save(Entity.Payroll.ITaxEmployeePrevEmplDetails ITaxEmployeePrevEmplDetails)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pITaxPrevEmplHeadId", SqlDbType.Int, ITaxEmployeePrevEmplDetails.ITaxPrevEmplHeadId);
                oDm.Add("@pEmployeeId", SqlDbType.Int, ITaxEmployeePrevEmplDetails.EmployeeId);
                oDm.Add("@pITaxPrevEmplHeadAmount", SqlDbType.Decimal, ITaxEmployeePrevEmplDetails.ITaxPrevEmplHeadAmount);
                oDm.Add("@pFinancialYear", SqlDbType.Int, ITaxEmployeePrevEmplDetails.FinancialYear);
                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_ITaxEmployeePrevEmplDetails_Save");
            }
        }

        public static DataTable GetAll(int EmployeeId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pEmployeeId", SqlDbType.Int, EmployeeId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_ITaxEmployeePrevEmplDetails_GetAll");
            }
        }

        public static Entity.Payroll.ITaxEmployeePrevEmplDetails GetAllById(int ITaxPrevEmplHeadId, int EmployeeId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pITaxPrevEmplHeadId", SqlDbType.Int, ITaxPrevEmplHeadId);
                oDm.Add("@pEmployeeId", SqlDbType.Int, EmployeeId);
                SqlDataReader dr = oDm.ExecuteReader("usp_ITaxEmployeePrevEmplDetails_GetAllById");
                Entity.Payroll.ITaxEmployeePrevEmplDetails ITaxEmployeePrevEmplDetails = new Entity.Payroll.ITaxEmployeePrevEmplDetails();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ITaxEmployeePrevEmplDetails.ITaxPrevEmplHeadId = ITaxPrevEmplHeadId;
                        ITaxEmployeePrevEmplDetails.EmployeeId = EmployeeId;
                        ITaxEmployeePrevEmplDetails.ITaxPrevEmplHeadAmount = (dr["ITaxPrevEmplHeadAmount"] == DBNull.Value) ? 0 :decimal.Parse(dr["ITaxPrevEmplHeadAmount"].ToString());
                        ITaxEmployeePrevEmplDetails.FinancialYear = (dr["FinancialYear"] == DBNull.Value) ? 0 : int.Parse(dr["FinancialYear"].ToString());
                    }
                }
                return ITaxEmployeePrevEmplDetails;
            }
        }

        public static int Delete(int ITaxPrevEmplHeadId, int EmployeeId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pITaxPrevEmplHeadId", SqlDbType.Int, ITaxPrevEmplHeadId);
                oDm.Add("@pEmployeeId", SqlDbType.Int, EmployeeId);
                return oDm.ExecuteNonQuery("usp_ITaxEmployeePrevEmplDetails_Delete");
            }
        }
    }
}
