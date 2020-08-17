using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Payroll
{
    public class ITaxPrevEmplHeads
    {
        public static int Save(Entity.Payroll.ITaxPrevEmplHeads ITaxPrevEmplHeads)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pITaxPrevEmplHeadId", SqlDbType.Int, ITaxPrevEmplHeads.ITaxPrevEmplHeadId);
                oDm.Add("@pITaxPrevEmplHeadName", SqlDbType.VarChar, 50, ITaxPrevEmplHeads.ITaxPrevEmplHeadName);
                oDm.Add("@ITaxType", SqlDbType.VarChar, 5, ITaxPrevEmplHeads.ITaxType);
                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_ITaxPrevEmplHeads_Save");
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_ITaxPrevEmplHeads_GetAll");
            }
        }

        public static Entity.Payroll.ITaxPrevEmplHeads GetAllById(int ITaxPrevEmplHeadId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pITaxPrevEmplHeadId", SqlDbType.Int, ITaxPrevEmplHeadId);
                SqlDataReader dr = oDm.ExecuteReader("usp_ITaxPrevEmplHeads_GetAllById");
                Entity.Payroll.ITaxPrevEmplHeads ITaxPrevEmplHeads = new Entity.Payroll.ITaxPrevEmplHeads();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ITaxPrevEmplHeads.ITaxPrevEmplHeadId = ITaxPrevEmplHeadId;
                        ITaxPrevEmplHeads.ITaxPrevEmplHeadName = (dr["ITaxPrevEmplHeadName"] == DBNull.Value) ? "" : (dr["ITaxPrevEmplHeadName"].ToString());
                        ITaxPrevEmplHeads.ITaxType = (dr["ITaxType"] == DBNull.Value) ? "" : (dr["ITaxType"].ToString());

                    }
                }
                return ITaxPrevEmplHeads;
            }
        }

        public static int Delete(int ITaxPrevEmplHeadId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pITaxPrevEmplHeadId", SqlDbType.Int, ITaxPrevEmplHeadId);
                return oDm.ExecuteNonQuery("usp_ITaxPrevEmplHeads_Delete");
            }
        }
    }
}
