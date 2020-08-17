using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.HR
{
    public class LeaveType
    {
        public LeaveType()
        {
        }

        public static int Save(Entity.HR.LeaveType LeaveType)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pLeaveTypeId", SqlDbType.Int, LeaveType.LeaveTypeId);
                oDm.Add("@pLeaveTypeName", SqlDbType.VarChar, 30, LeaveType.LeaveTypeName);

                oDm.Add("@pDescription", SqlDbType.VarChar, 150, LeaveType.Description);
                if (LeaveType.LeavePerMonth == null)
                {
                    oDm.Add("@pLeavePerMonth", SqlDbType.Decimal, DBNull.Value);
                }
                else { oDm.Add("@pLeavePerMonth", SqlDbType.Decimal, LeaveType.LeavePerMonth); }

                if (LeaveType.LeavePerYear == null)
                {
                    oDm.Add("@pLeavePerYear", SqlDbType.Int, DBNull.Value);
                }
                else { oDm.Add("@pLeavePerYear", SqlDbType.Int, LeaveType.LeavePerYear); }

                oDm.Add("@pIsCarryForwarded", SqlDbType.Bit, LeaveType.IsCarryForwarded);
                oDm.Add("@pIsEncashable", SqlDbType.Bit, LeaveType.IsEncashable);

                if (LeaveType.MaxCarryFwdLimit == null)
                {
                    oDm.Add("@pMaxCarryFwdLimit", SqlDbType.Int, DBNull.Value);
                }
                else { oDm.Add("@pMaxCarryFwdLimit", SqlDbType.Int, LeaveType.MaxCarryFwdLimit); }

                oDm.Add("@pIsPaid", SqlDbType.Bit, LeaveType.IsPaid); 

                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_LeaveType_Save");
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_LeaveType_GetAll");
            }
        }

        public static Entity.HR.LeaveType GetAllById(int LeaveTypeId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pLeaveTypeId", SqlDbType.Int, LeaveTypeId);
                SqlDataReader dr = oDm.ExecuteReader("usp_LeaveType_GetAllById");
                Entity.HR.LeaveType LeaveType = new Entity.HR.LeaveType();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        LeaveType.LeaveTypeId = LeaveTypeId;
                        LeaveType.LeaveTypeName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                        LeaveType.Description = (dr[2] == DBNull.Value) ? "" : dr[2].ToString();

                        if (dr[3] == DBNull.Value) { LeaveType.LeavePerMonth = null; }
                        else { LeaveType.LeavePerMonth = decimal.Parse(dr[3].ToString()); }

                        if (dr[4] == DBNull.Value) { LeaveType.LeavePerYear = null; }
                        else { LeaveType.LeavePerYear = int.Parse(dr[4].ToString()); }

                        LeaveType.IsCarryForwarded = (dr[5] == DBNull.Value) ? false : bool.Parse(dr[5].ToString());
                        LeaveType.IsEncashable = (dr[6] == DBNull.Value) ? false : bool.Parse(dr[6].ToString());

                        if (dr[7] == DBNull.Value) { LeaveType.MaxCarryFwdLimit = null; }
                        else { LeaveType.MaxCarryFwdLimit = int.Parse(dr[7].ToString()); }

                        LeaveType.IsPaid = (dr[8] == DBNull.Value) ? false : bool.Parse(dr[8].ToString());

                    }
                }
                return LeaveType;
            }
        }

    }
}
