using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class Branch
    {
        public Branch()
        {
        }

        public static int Save(Entity.Common.Branch Branch)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pBranchId", SqlDbType.Int, Branch.BranchId);
                oDm.Add("@pBranchName", SqlDbType.VarChar, 50, Branch.BranchName);
                
                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_Branch_Save");
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_Branch_GetAll");
            }
        }

        public static Entity.Common.Branch GetAllById(int BranchId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pBranchId", SqlDbType.Int, BranchId);
                SqlDataReader dr = oDm.ExecuteReader("usp_Branch_GetAllById");
                Entity.Common.Branch Branch = new Entity.Common.Branch();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Branch.BranchId = BranchId;
                        Branch.BranchName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                        
                    }
                }
                return Branch;
            }
        }

        public static int Delete(int BranchId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pBranchId", SqlDbType.Int, BranchId);
                return oDm.ExecuteNonQuery("usp_Branch_Delete");
            }
        }
    }
}
