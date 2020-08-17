using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class HolidayList
    {
        public HolidayList()
        {

        }

        public static int Save(Entity.Common.HolidayList HolidayList)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pHolidayListId", SqlDbType.Int, HolidayList.HolidayListId);
                oDm.Add("@pHolidayName", SqlDbType.VarChar,100, HolidayList.HolidayName);
                oDm.Add("@pHolidayRemarks", SqlDbType.VarChar,100, HolidayList.HolidayRemarks);
                oDm.Add("@pHolidayDate", SqlDbType.DateTime, HolidayList.HolidayDate);
                oDm.Add("@pHolidayList_UserId", SqlDbType.Int, HolidayList.HolidayList_UserId);
                oDm.Add("@pHolidayList_ModUserId", SqlDbType.Int, HolidayList.HolidayList_ModUserId);

                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_HolidayList_Save");
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_HolidayList_GetAll");
            }
        }

        public static Entity.Common.HolidayList GetAllById(int HolidayListId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pHolidayListId", SqlDbType.Int, HolidayListId);
                SqlDataReader dr = oDm.ExecuteReader("usp_HolidayList_GetAllById");
                Entity.Common.HolidayList HolidayList = new Entity.Common.HolidayList();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        HolidayList.HolidayListId = HolidayListId;
                        HolidayList.HolidayName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                        HolidayList.HolidayRemarks = (dr[2] == DBNull.Value) ? "" : dr[2].ToString();
                        HolidayList.HolidayDate = (dr[3] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(dr[3].ToString());
                        
                    }
                }
                return HolidayList;
            }
        }

        public static void Delete(int HolidayListId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pHolidayListId", SqlDbType.Int, HolidayListId);
                oDm.ExecuteNonQuery("usp_HolidayList_Delete");
            }
        }
    }
}
