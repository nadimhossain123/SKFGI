using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class EmployeeWork
    {
        public EmployeeWork()
        {
        }

        public static void Save(Entity.Common.EmployeeWork Work)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pEmployeeWorkId", SqlDbType.Int, Work.EmployeeWorkId);
                oDm.Add("@pEmployeeWork_EmployeeId", SqlDbType.Int, Work.EmployeeWork_EmployeeId);

                oDm.Add("@pCompanyName", SqlDbType.VarChar, 50, Work.CompanyName);
                oDm.Add("@pWorkPeriod", SqlDbType.VarChar, 50, Work.WorkPeriod);
                oDm.Add("@pWorkDesignation", SqlDbType.VarChar, 50, Work.WorkDesignation);
                oDm.Add("@pWorkResponsibilities", SqlDbType.VarChar, 500, Work.WorkResponsibilities);

                oDm.Add("@pWorkSalary", SqlDbType.Decimal, Work.WorkSalary);

                oDm.Add("@pCreatedBy", SqlDbType.Int, Work.CreatedBy);
                oDm.Add("@pModifiedBy", SqlDbType.Int, Work.ModifiedBy);

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.ExecuteNonQuery("usp_EmployeeWork_Save");
            }
        }

        public static DataTable GetAll(int EmployeeId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pEmployeeWork_EmployeeId", SqlDbType.Int, EmployeeId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_EmployeeWork_GetAll");
            }
        }

        public static Entity.Common.EmployeeWork GetAllById(int EmployeeWorkId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pEmployeeWorkId", SqlDbType.Int, EmployeeWorkId);
                SqlDataReader dr = oDm.ExecuteReader("usp_EmployeeWork_GetAllById");
                Entity.Common.EmployeeWork Work = new Entity.Common.EmployeeWork();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Work.EmployeeWorkId = EmployeeWorkId;
                        Work.CompanyName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                        Work.WorkPeriod = (dr[2] == DBNull.Value) ? "" : dr[2].ToString();
                        Work.WorkDesignation = (dr[3] == DBNull.Value) ? "" : dr[3].ToString();

                        Work.WorkResponsibilities = (dr[4] == DBNull.Value) ? "" : dr[4].ToString();
                        Work.WorkSalary = (dr[5] == DBNull.Value) ? 0 : decimal.Parse(dr[5].ToString());
                        
                    }
                }
                return Work;
            }
        }

        public static void Delete(int EmployeeWorkId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pEmployeeWorkId", SqlDbType.Int, EmployeeWorkId);
                oDm.ExecuteNonQuery("usp_EmployeeWork_Delete");
            }
        }
    }
}
