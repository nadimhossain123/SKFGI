using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Payroll
{
    public class ITaxInvestmentHeads
    {
        public static int Save(Entity.Payroll.ITaxInvestmentHeads ITaxInvestmentHeads)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pITaxSectionId", SqlDbType.Int, ITaxInvestmentHeads.ITaxSectionId);
                oDm.Add("@pITaxInvestmentHeadId", SqlDbType.Int, ITaxInvestmentHeads.ITaxInvestmentHeadId);
                oDm.Add("@pITaxInvestmentHeadName", SqlDbType.VarChar, 100,ITaxInvestmentHeads.ITaxInvestmentHeadName);
                oDm.Add("@ITaxType", SqlDbType.VarChar, 5, ITaxInvestmentHeads.ITaxFlagName);
                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_ITaxInvestmentHeads_Save");
            }
        }

        public static DataTable GetAll(int SectionId)
        {
            using (DataManager oDm = new DataManager())
            {
                if (SectionId == 0) { oDm.Add("@pITaxSectionId", SqlDbType.Int, DBNull.Value); }
                else { oDm.Add("@pITaxSectionId", SqlDbType.Int, SectionId); }
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_ITaxInvestmentHeads_GetAll");
            }
        }

        public static Entity.Payroll.ITaxInvestmentHeads GetAllById(int ITaxInvestmentHeadId)
        {
            using (DataManager oDm = new DataManager())
            {   
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pITaxInvestmentHeadId", SqlDbType.Int, ITaxInvestmentHeadId);
                SqlDataReader dr = oDm.ExecuteReader("usp_ITaxInvestmentHeads_GetAllById");
                Entity.Payroll.ITaxInvestmentHeads ITaxInvestmentHeads = new Entity.Payroll.ITaxInvestmentHeads();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ITaxInvestmentHeads.ITaxInvestmentHeadId = ITaxInvestmentHeadId;
                        ITaxInvestmentHeads.ITaxInvestmentHeadName = (dr["ITaxInvestmentHeadName"] == DBNull.Value) ? "" : (dr["ITaxInvestmentHeadName"].ToString());
                        ITaxInvestmentHeads.ITaxSectionId = (dr["ITaxSectionId"] == DBNull.Value) ? 0 : int.Parse(dr["ITaxSectionId"].ToString());
                        ITaxInvestmentHeads.ITaxFlagName = (dr["ITaxType"] == DBNull.Value) ? "" : (dr["ITaxType"].ToString());
                    }
                }
                return ITaxInvestmentHeads;
            }
        }

        public static int Delete(int ITaxInvestmentHeadId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pITaxInvestmentHeadId", SqlDbType.Int, ITaxInvestmentHeadId);
                return oDm.ExecuteNonQuery("usp_ITaxInvestmentHeads_Delete");
            }
        }
    }
}
