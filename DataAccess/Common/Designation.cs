using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class Designation
    {
        public Designation()
        {
        }

        public static int Save(Entity.Common.Designation Designation)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pDesignationId", SqlDbType.Int, Designation.DesignationId);
                oDm.Add("@pDesignationName", SqlDbType.VarChar, 100, Designation.DesignationName);

                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_Designation_Save");
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_Designation_GetAll");
            }
        }

        public static Entity.Common.Designation GetAllById(int DesignationId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pDesignationId", SqlDbType.Int, DesignationId);
                SqlDataReader dr = oDm.ExecuteReader("usp_Designation_GetAllById");
                Entity.Common.Designation Designation = new Entity.Common.Designation();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Designation.DesignationId = DesignationId;
                        Designation.DesignationName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();

                    }
                }
                return Designation;
            }
        }

        public static int Delete(int DesignationId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pDesignationId", SqlDbType.Int, DesignationId);
                return oDm.ExecuteNonQuery("usp_Designation_Delete");
            }
        }
    }
}
