using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class State
    {
        public State()
        {
        }

        public static DataTable GetAll(int CountryId)
        {
            using (DataManager oDm = new DataManager())
            {
                if (CountryId == 0)
                {
                    oDm.Add("@pState_CountryId", SqlDbType.Int, DBNull.Value);
                }
                else { oDm.Add("@pState_CountryId", SqlDbType.Int, CountryId); }
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_State_GetAll");
            }
        }
    }
}
