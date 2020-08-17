using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.HR
{
    public class Leave
    {
        public Leave()
        {
        }

        public static int UpdateLeaveStock()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteNonQuery("usp_LeaveStock_Update");
            }
        }

        public static int Save(Entity.HR.Leave Leave)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pLeaveId", SqlDbType.Int, Leave.LeaveId);
                oDm.Add("@pEmployeeId", SqlDbType.Int, Leave.EmployeeId);
                oDm.Add("@pLeaveTypeId", SqlDbType.Int, Leave.LeaveTypeId);
                oDm.Add("@pStartDate", SqlDbType.DateTime, Leave.StartDate);
                oDm.Add("@pEndDate", SqlDbType.DateTime, Leave.EndDate); 
                oDm.Add("@pNoOfDays", SqlDbType.Decimal, Leave.NoOfDays);
                oDm.Add("@pLeaveStatusId", SqlDbType.Int, Leave.LeaveStatusId);
                oDm.Add("@pPurpose", SqlDbType.VarChar,150, Leave.Purpose);
                oDm.Add("@pComment", SqlDbType.VarChar, 150, Leave.Comment);

                oDm.Add("@pisClassAdjusted", SqlDbType.Bit, Leave.isClassAdjusted);
                oDm.Add("@pisExamDutyDuringLeave", SqlDbType.Bit, Leave.isExamDutyDuringLeave);
                oDm.Add("@pIsAdjustment", SqlDbType.Bit, Leave.IsAdjustment);

                if (Leave.IsHalfDayApplied == null)
                    oDm.Add("@pIsHalfDayApplied", SqlDbType.Bit, DBNull.Value);
                else
                    oDm.Add("@pIsHalfDayApplied", SqlDbType.Bit, Leave.IsHalfDayApplied);
                if (Leave.IsDirectorApproved == null)
                    oDm.Add("@IsDirectorApproved", SqlDbType.Bit, DBNull.Value);
                else
                    oDm.Add("@IsDirectorApproved", SqlDbType.Bit, Leave.IsDirectorApproved);
                oDm.Add("@pCount", SqlDbType.Int, ParameterDirection.Output, Leave.Count);
                oDm.CommandType = CommandType.StoredProcedure;

                Leave.Count = oDm.ExecuteNonQuery("usp_EmployeeLeave_Save");
                if (Leave.Count != -1)
                {
                    try
                    {
                        Leave.Count = (int)oDm["@pCount"].Value;
                    }
                    catch { Leave.Count = 0; }
                }
                
                return Leave.Count;
            }
        }
        public static int SaveDirect(Entity.HR.Leave Leave)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pLeaveId", SqlDbType.Int, Leave.LeaveId);
                oDm.Add("@pEmployeeId", SqlDbType.Int, Leave.EmployeeId);
                oDm.Add("@pLeaveTypeId", SqlDbType.Int, Leave.LeaveTypeId);
                oDm.Add("@pStartDate", SqlDbType.DateTime, Leave.StartDate);
                oDm.Add("@pEndDate", SqlDbType.DateTime, Leave.EndDate);
                oDm.Add("@pNoOfDays", SqlDbType.Decimal, Leave.NoOfDays);
                oDm.Add("@pLeaveStatusId", SqlDbType.Int, Leave.LeaveStatusId);
                oDm.Add("@pPurpose", SqlDbType.VarChar, 150, Leave.Purpose);
                oDm.Add("@pComment", SqlDbType.VarChar, 150, Leave.Comment);

                oDm.Add("@pisClassAdjusted", SqlDbType.Bit, Leave.isClassAdjusted);
                oDm.Add("@pisExamDutyDuringLeave", SqlDbType.Bit, Leave.isExamDutyDuringLeave);
                oDm.Add("@pIsAdjustment", SqlDbType.Bit, Leave.IsAdjustment);

                if (Leave.IsHalfDayApplied == null)
                    oDm.Add("@pIsHalfDayApplied", SqlDbType.Bit, DBNull.Value);
                else
                    oDm.Add("@pIsHalfDayApplied", SqlDbType.Bit, Leave.IsHalfDayApplied);
                if (Leave.IsDirectorApproved == null)
                    oDm.Add("@IsDirectorApproved", SqlDbType.Bit, DBNull.Value);
                else
                    oDm.Add("@IsDirectorApproved", SqlDbType.Bit, Leave.IsDirectorApproved);
                oDm.Add("@pCount", SqlDbType.Int, ParameterDirection.Output, Leave.Count);
                oDm.CommandType = CommandType.StoredProcedure;

                Leave.Count = oDm.ExecuteNonQuery("[usp_EmployeeLeaveDirect_Save]");
                Leave.Count = (int)oDm["@pCount"].Value;

                return Leave.Count;
            }
        }

        public static DataTable GetAll(string FirstName, int LeaveStatusId, int EmployeeId, int LeaveManagerId, string FromDate, string ToDate,int LeaveTypeId)
        {
            using (DataManager oDm = new DataManager())
            {
                if (FirstName.Trim().Length == 0) {oDm.Add("@pFirstName", SqlDbType.VarChar, 100, DBNull.Value);}
                else { oDm.Add("@pFirstName", SqlDbType.VarChar, 100, FirstName); }


                if (LeaveStatusId == 0){oDm.Add("@pLeaveStatusId", SqlDbType.Int, DBNull.Value);}
                else { oDm.Add("@pLeaveStatusId", SqlDbType.Int, LeaveStatusId); }

                if (EmployeeId == 0){oDm.Add("@pEmployeeId", SqlDbType.Int, DBNull.Value);}
                else { oDm.Add("@pEmployeeId", SqlDbType.Int, EmployeeId); }

                if (LeaveManagerId == 0) {oDm.Add("@pLeaveManagerId", SqlDbType.Int, DBNull.Value); }
                else { oDm.Add("@pLeaveManagerId", SqlDbType.Int, LeaveManagerId); }

                if (FromDate.Trim().Length == 0) { oDm.Add("@pFromDate", SqlDbType.DateTime, DBNull.Value); }
                else { oDm.Add("@pFromDate", SqlDbType.DateTime, Convert.ToDateTime(FromDate)); }

                if (ToDate.Trim().Length == 0) { oDm.Add("@pToDate", SqlDbType.DateTime, DBNull.Value); }
                else { oDm.Add("@pToDate", SqlDbType.DateTime, Convert.ToDateTime(ToDate)); }

                if (LeaveTypeId == 0) { oDm.Add("@pLeaveTypeId", SqlDbType.Int, DBNull.Value); }
                else { oDm.Add("@pLeaveTypeId", SqlDbType.Int, LeaveTypeId); }

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_EmployeeLeave_GetAll");
            }
        }

        public static DataTable GetAllForDirectorApproval(string FromDate, string ToDate)
        {
            using (DataManager oDm = new DataManager())
            {
                if (FromDate.Trim().Length == 0) { oDm.Add("@pFromDate", SqlDbType.DateTime, DBNull.Value); }
                else { oDm.Add("@pFromDate", SqlDbType.DateTime, Convert.ToDateTime(FromDate)); }

                if (ToDate.Trim().Length == 0) { oDm.Add("@pToDate", SqlDbType.DateTime, DBNull.Value); }
                else { oDm.Add("@pToDate", SqlDbType.DateTime, Convert.ToDateTime(ToDate)); }

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_EmployeeLeave_GetForDirectorApproval");
            }
        }

        public static void SaveDirectorApproval(DataTable DTLeave, bool IsApproved)
        {
            using (DataManager oDm = new DataManager())
            {
                string LeaveXML = string.Empty;
                if (DTLeave != null && DTLeave.Rows.Count > 0)
                {
                    using (DataSet ds = new DataSet())
                    {
                        ds.Tables.Add(DTLeave);

                        LeaveXML = ds.GetXml();
                    }
                }

                oDm.Add("@pLeaveDetails", SqlDbType.Xml, LeaveXML);
                oDm.Add("@pIsDirectorApproved", SqlDbType.Bit, IsApproved);
               
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_EmployeeLeave_SaveDirectorApproval");
            }
        }

        public static Entity.HR.Leave GetAllById(int LeaveId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pLeaveId", SqlDbType.Int, LeaveId);
                SqlDataReader dr = oDm.ExecuteReader("usp_EmployeeLeave_GetAllById");
                Entity.HR.Leave Leave = new Entity.HR.Leave();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Leave.LeaveId = LeaveId;
                        Leave.EmployeeId = (dr[1] == DBNull.Value) ? 0 : int.Parse(dr[1].ToString());
                        Leave.LeaveTypeId = (dr[2] == DBNull.Value) ? 0 : int.Parse(dr[2].ToString());

                        Leave.StartDate = (dr[3] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(dr[3].ToString());

                        Leave.EndDate = (dr[4] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(dr[4].ToString());
                        Leave.NoOfDays = (dr[5] == DBNull.Value) ? 0 : decimal.Parse(dr[5].ToString());
                        Leave.LeaveStatusId = (dr[6] == DBNull.Value) ? 0 : int.Parse(dr[6].ToString());

                        Leave.Purpose = (dr[7] == DBNull.Value) ? "" : dr[7].ToString();
                        Leave.Comment = (dr[8] == DBNull.Value) ? "" : dr[8].ToString();

                        Leave.isClassAdjusted = (dr[9] == DBNull.Value) ? false : bool.Parse(dr[9].ToString());
                        Leave.isExamDutyDuringLeave = (dr[10] == DBNull.Value) ? false : bool.Parse(dr[10].ToString());
                        Leave.IsAdjustment = (dr[11] == DBNull.Value) ? false : bool.Parse(dr[11].ToString());
                        Leave.CreatedOn = (dr[12] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(dr[12].ToString());
                        if (dr[13] == DBNull.Value)
                        {
                            Leave.IsHalfDayApplied=null;
                        }
                        else
                        {
                            Leave.IsHalfDayApplied = bool.Parse(dr[13].ToString());
                        }

                    }
                }
                return Leave;
            }
        }

        public static DataTable GetStockBalance(int EmployeeId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pEmployeeId", SqlDbType.Int, EmployeeId);
                return oDm.ExecuteDataTable("usp_LeaveStock_GetAllById");
            }
        }

        public static DataTable GetLeaveStatus()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_LeaveStatus_GetAll");
            }
        }
        public static DataTable RPTEmployeeLeave(DateTime FromDate, DateTime ToDate)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@FromDate", SqlDbType.DateTime, FromDate);
                oDm.Add("@ToDate", SqlDbType.DateTime, ToDate);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_RPTEmployeeLeave");
            }
        }
        public static DataTable RPTEmployeeLeaveBalance(DateTime FromDate, DateTime ToDate)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@FromDate", SqlDbType.DateTime, FromDate);
                oDm.Add("@ToDate", SqlDbType.DateTime, ToDate);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_RPTEmployeeLeaveBalance");
            }
        }
        public static DataTable GetDepartment()
        {
            using (DataManager oDm = new DataManager())
            {
                
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_Department_GetAll");
            }
        }
        public static DataTable GetLeaveDeleteList(string FirstName, int LeaveStatusId, int EmployeeId,  string FromDate, string ToDate, int LeaveTypeId)
        {
            using (DataManager oDm = new DataManager())
            {
                if (FirstName.Trim().Length == 0) { oDm.Add("@pFirstName", SqlDbType.VarChar, 100, DBNull.Value); }
                else { oDm.Add("@pFirstName", SqlDbType.VarChar, 100, FirstName); }


                if (LeaveStatusId == 0) { oDm.Add("@pLeaveStatusId", SqlDbType.Int, DBNull.Value); }
                else { oDm.Add("@pLeaveStatusId", SqlDbType.Int, LeaveStatusId); }

                if (EmployeeId == 0) { oDm.Add("@pEmployeeId", SqlDbType.Int, DBNull.Value); }
                else { oDm.Add("@pEmployeeId", SqlDbType.Int, EmployeeId); }

               

                if (FromDate.Trim().Length == 0) { oDm.Add("@pFromDate", SqlDbType.DateTime, DBNull.Value); }
                else { oDm.Add("@pFromDate", SqlDbType.DateTime, Convert.ToDateTime(FromDate)); }

                if (ToDate.Trim().Length == 0) { oDm.Add("@pToDate", SqlDbType.DateTime, DBNull.Value); }
                else { oDm.Add("@pToDate", SqlDbType.DateTime, Convert.ToDateTime(ToDate)); }

                if (LeaveTypeId == 0) { oDm.Add("@pLeaveTypeId", SqlDbType.Int, DBNull.Value); }
                else { oDm.Add("@pLeaveTypeId", SqlDbType.Int, LeaveTypeId); }

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("[usp_EmployeeLeave_GetAllForDel]");
            }
        }
        public static int LeaveDelete(int EmployeeId,int LeaveId,int LeaveTypeid,decimal NoOfdays)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@EmployeeId", SqlDbType.Int, EmployeeId );
                oDm.Add("@LeaveId", SqlDbType.Int, LeaveId);
                oDm.Add("@LeaveTypeid", SqlDbType.Int, LeaveTypeid);
                oDm.Add("@NoOfdays", SqlDbType.Decimal, NoOfdays);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteNonQuery("usp_LeaveDelete");
            }
        }
    }
}
