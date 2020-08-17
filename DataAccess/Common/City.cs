using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class City
    {
        public City()
        {
        }

        public static DataTable GetAll(int StateId)
        {
            using (DataManager oDm = new DataManager())
            {
                if (StateId == 0)
                {
                    oDm.Add("@pCity_StateId", SqlDbType.Int, DBNull.Value);
                }
                else { oDm.Add("@pCity_StateId", SqlDbType.Int, StateId); }
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_City_GetAll");
            }
        }
    }
}
