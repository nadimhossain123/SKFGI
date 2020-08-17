using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class PTax
    {
        public PTax()
        {
        }

        public static int Save(Entity.Common.PTax PTax)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pPTaxId", SqlDbType.Int, PTax.PTaxId);
                oDm.Add("@pPTaxStateCode", SqlDbType.VarChar, 20, PTax.PTaxStateCode);
                oDm.Add("@pPTaxStateDescription", SqlDbType.VarChar, 50, PTax.PTaxStateDescription);

                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_PTax_Save");
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_PTax_GetAll");
            }
        }

        public static Entity.Common.PTax GetAllById(int PTaxId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pPTaxId", SqlDbType.Int, PTaxId);
                SqlDataReader dr = oDm.ExecuteReader("usp_PTax_GetAllById");
                Entity.Common.PTax PTax = new Entity.Common.PTax();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PTax.PTaxId = PTaxId;
                        PTax.PTaxStateCode = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                        PTax.PTaxStateDescription = (dr[2] == DBNull.Value) ? "" : dr[2].ToString();

                    }
                }
                return PTax;
            }
        }

        public static DataSet PTax_DetailsReport(string fromtoyear)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@FromToYear", SqlDbType.NVarChar, 9, ParameterDirection.Input, fromtoyear);

                oDm.CommandType = CommandType.StoredProcedure;
                DataSet ds= new DataSet();
                return oDm.GetDataSet("usp_PTax_DetailsReport", ref ds, "Table");
            }
        }

        public static DataSet PTax_SummeryReport(string fromtoyear)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@FromToYear", SqlDbType.NVarChar, 9, ParameterDirection.Input, fromtoyear);

                oDm.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_PTax_SummeryReport", ref ds, "Table");
            }
        }
    }
}
