using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Payroll
{
    public class ITaxSectionMaster
    {
        public static int Save(Entity.Payroll.ITaxSectionMaster ITaxSectionMaster)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pITaxSectionId", SqlDbType.Int, ITaxSectionMaster.ITaxSectionId);
                oDm.Add("@pITaxSectionName", SqlDbType.VarChar,50, ITaxSectionMaster.ITaxSectionName);
                oDm.Add("@pITaxMaxExemption", SqlDbType.Decimal, ITaxSectionMaster.ITaxMaxExemption);
                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_ITaxSectionMaster_Save");
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_ITaxSectionMaster_GetAll");
            }
        }

        public static Entity.Payroll.ITaxSectionMaster GetAllById(int ITaxSectionId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pITaxSectionId", SqlDbType.Int, ITaxSectionId);
                SqlDataReader dr = oDm.ExecuteReader("usp_ITaxSectionMaster_GetAllById");
                Entity.Payroll.ITaxSectionMaster ITaxSectionMaster = new Entity.Payroll.ITaxSectionMaster();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ITaxSectionMaster.ITaxSectionId = ITaxSectionId;
                        ITaxSectionMaster.ITaxSectionName = (dr["ITaxSectionName"] == DBNull.Value) ? "" : (dr["ITaxSectionName"].ToString());
                        ITaxSectionMaster.ITaxMaxExemption = (dr["ITaxMaxExemption"] == DBNull.Value) ? 0 : decimal.Parse(dr["ITaxMaxExemption"].ToString());
                    }
                }
                return ITaxSectionMaster;
            }
        }

        public static int Delete(int ITaxSectionId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pITaxSectionId", SqlDbType.Int, ITaxSectionId);
                return oDm.ExecuteNonQuery("usp_ITaxSectionMaster_Delete");
            }
        }
    }
}
