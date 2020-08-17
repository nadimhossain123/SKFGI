using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class PayBand
    {
        public PayBand()
        {
        }

        public static int Save(Entity.Common.PayBand PayBand)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@int_mode", SqlDbType.Int, 1);
                oDm.Add("@PayBandId", SqlDbType.Int, PayBand.PayBandId);
                oDm.Add("@PayBandName", SqlDbType.VarChar, 50, PayBand.PayBandName);

                oDm.Add("@ScaleFrom", SqlDbType.Decimal, PayBand.ScaleFrom);
                oDm.Add("@ScaleTo", SqlDbType.Decimal, PayBand.ScaleTo);
                oDm.Add("@GradePay", SqlDbType.Decimal, PayBand.GradePay);

                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_PayBand");
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@int_mode", SqlDbType.Int, 2);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_PayBand");
            }
        }

        public static Entity.Common.PayBand GetAllById(int PayBandId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, 3);
                oDm.Add("@PayBandId", SqlDbType.Int, PayBandId);
                SqlDataReader dr = oDm.ExecuteReader("usp_PayBand");
                Entity.Common.PayBand PayBand=new Entity.Common.PayBand();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PayBand.PayBandId = PayBandId;
                        PayBand.PayBandName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();

                        PayBand.ScaleFrom = (dr[2] == DBNull.Value) ? 0 : decimal.Parse(dr[2].ToString());
                        PayBand.ScaleTo = (dr[3] == DBNull.Value) ? 0 : decimal.Parse(dr[3].ToString());
                        PayBand.GradePay = (dr[4] == DBNull.Value) ? 0 : decimal.Parse(dr[4].ToString());

                    }
                }
                return PayBand;
            }
        }

    }
}
