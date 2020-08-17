using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class Department
    {
        public Department()
        {
        }

        public static int Save(Entity.Common.Department Department)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pDepartmentId", SqlDbType.Int, Department.DepartmentId);
                oDm.Add("@pDepartmentName", SqlDbType.VarChar, 50, Department.DepartmentName);

                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_Department_Save");
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_Department_GetAll");
            }
        }

        public static Entity.Common.Department GetAllById(int DepartmentId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pDepartmentId", SqlDbType.Int, DepartmentId);
                SqlDataReader dr = oDm.ExecuteReader("usp_Department_GetAllById");
                Entity.Common.Department Department = new Entity.Common.Department();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Department.DepartmentId = DepartmentId;
                        Department.DepartmentName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();

                    }
                }
                return Department;
            }
        }

        public static int Delete(int DepartmentId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pDepartmentId", SqlDbType.Int, DepartmentId);
                return oDm.ExecuteNonQuery("usp_Department_Delete");
            }
        }
    }
}
