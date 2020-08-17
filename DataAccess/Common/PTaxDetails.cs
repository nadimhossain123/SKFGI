using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class PTaxDetails
    {
        public PTaxDetails()
        {
        }

        public static int Save(Entity.Common.PTaxDetails PTaxDetails)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pPTaxDetails_PTaxId", SqlDbType.Int, PTaxDetails.PTaxDetails_PTaxId);
                oDm.Add("@pPTaxDetailsSlabNo", SqlDbType.Int, PTaxDetails.PTaxDetailsSlabNo);
                oDm.Add("@pPTaxDetailsFromAmount", SqlDbType.Decimal, PTaxDetails.PTaxDetailsFromAmount);
                oDm.Add("@pPTaxDetailsToAmount", SqlDbType.Decimal, PTaxDetails.PTaxDetailsToAmount);
                oDm.Add("@pPTaxDetailsAmount", SqlDbType.Decimal, PTaxDetails.PTaxDetailsAmount);
                oDm.Add("@pPTaxDetailsCUser_UserId", SqlDbType.Int, PTaxDetails.PTaxDetailsCUser_UserId);

                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_PTaxDetails_Save");
            }
        }

        public static DataTable GetAll(int PTaxId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pPTaxDetails_PTaxId", SqlDbType.Int, PTaxId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_PTaxDetails_GetAll");
            }
        }

        public static void Delete(int PTaxDetailsId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pPTaxDetailsId", SqlDbType.Int, PTaxDetailsId);
                oDm.ExecuteNonQuery("usp_PTaxDetails_Delete");
            }
        }
    }
}
