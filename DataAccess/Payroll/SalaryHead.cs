using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Payroll
{
    public class SalaryHead
    {
        public SalaryHead()
        {
        }

        public static int Save(Entity.Payroll.SalaryHead SalaryHead)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pSalaryHeadId", SqlDbType.Int, SalaryHead.SalaryHeadId);
                oDm.Add("@pSalaryHeadDetails", SqlDbType.VarChar, 100, SalaryHead.SalaryHeadDetails);
                oDm.Add("@pIsFixed", SqlDbType.Bit, SalaryHead.IsFixed);
                if (SalaryHead.MaxRange == null)
                    oDm.Add("@pMaxRange", SqlDbType.Decimal, DBNull.Value);
                else
                    oDm.Add("@pMaxRange", SqlDbType.Decimal, SalaryHead.MaxRange);

                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_SalaryHead_Save");
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_SalaryHead_GetAll");
            }
        }

        public static Entity.Payroll.SalaryHead GetAllById(int SalaryHeadId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pSalaryHeadId", SqlDbType.Int, SalaryHeadId);
                SqlDataReader dr = oDm.ExecuteReader("usp_SalaryHead_GetAllById");
                Entity.Payroll.SalaryHead SalaryHead = new Entity.Payroll.SalaryHead();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        SalaryHead.SalaryHeadId = SalaryHeadId;
                        SalaryHead.SalaryHeadDetails = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                        SalaryHead.IsFixed = (dr[2] == DBNull.Value) ? false : bool.Parse(dr[2].ToString());
                        if (dr[3] == DBNull.Value)
                            SalaryHead.MaxRange = null;
                        else
                            SalaryHead.MaxRange = decimal.Parse(dr[3].ToString());

                    }
                }
                return SalaryHead;

            }
        }
        public static DataSet GetAllBySalaryHeadId(int SalaryHeadId, int IntMode)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@IntMode", SqlDbType.Int, IntMode);
                oDm.Add("@SalaryHeadId1", SqlDbType.Int, SalaryHeadId);
                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_SalaryHeadUpdation", ref ds, "table");
               
            }
        }
        public static int UpdateSalaryHead(string SalaryHeadUpdateXML)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.Add("@IntMode", SqlDbType.Int, 2);
                oDm.Add("@SalaryHeadUpdateXML", SqlDbType.Xml, SalaryHeadUpdateXML);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteNonQuery("usp_SalaryHeadUpdation");

            }
        }

    }
}
