using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Common
{
    public class EmployeeAttendance
    {
        public EmployeeAttendance()
        {
        }

        public static void BulkSave(int Month, int Year, string AttendanceXML)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@AttendanceMonth", SqlDbType.Int, Month);
                oDm.Add("@AttendanceYear", SqlDbType.Int, Year);
                oDm.Add("@pAttendanceXML", SqlDbType.Xml, AttendanceXML);
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_EmployeeAttendance_BulkSave");
            }
        }

        public static DataTable GetAll(int Month, int Year)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@AttendanceMonth", SqlDbType.Int, Month);
                oDm.Add("@AttendanceYear", SqlDbType.Int, Year);
                
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_EmployeeAttendance_GetAll");
            }
        }
        public static DataTable GetAllNew(int Month, int Year)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@Month", SqlDbType.Int, Month);
                oDm.Add("@Year", SqlDbType.Int, Year);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_getEmployeeAttendanceAll");
            }
        }
        public static DataTable GetLeaveDetailById(int Month, int Year, int EmployeeId, int LeaveId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@Month", SqlDbType.Int, Month);
                oDm.Add("@Year", SqlDbType.Int, Year);
                oDm.Add("@EmployeeId", SqlDbType.Int, EmployeeId);
                oDm.Add("@LeaveTypeId", SqlDbType.Int, LeaveId);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_GetLeaveDetailById");
            }
        }
        public static void SaveNew(int Month, int Year,int CreatedBy)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@Month", SqlDbType.Int, Month);
                oDm.Add("@year", SqlDbType.Int, Year);
                oDm.Add("@CreatedBy", SqlDbType.Int, CreatedBy);
               
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_EmployeeAttendanceNewSave");
            }
        }
        public static void AbsentUpdate(int Month, int Year, int EmpId,decimal NoOfDays)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@Mnth", SqlDbType.Int, Month);
                oDm.Add("@Yr", SqlDbType.Int, Year);
                oDm.Add("@NoOfdays", SqlDbType.Decimal, NoOfDays);
                oDm.Add("@EmpId", SqlDbType.Int, EmpId);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_EmployeeAttendanceAbsentUpdate");
            }
        }
        public static int LeaveUpdate(int LeaveId, DateTime FromDate, DateTime ToDate,int Month,int Year,int EmpId,int CreatedBy)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@LeaveId", SqlDbType.Int, LeaveId);
                oDm.Add("@FromDate", SqlDbType.DateTime, FromDate);
                oDm.Add("@ToDate", SqlDbType.DateTime, ToDate);
                oDm.Add("@Month", SqlDbType.Int, Month);
                oDm.Add("@year", SqlDbType.Int, Year);
                oDm.Add("@EmpId", SqlDbType.Int, EmpId);
                oDm.Add("@CreatedBy", SqlDbType.Int, CreatedBy);

                oDm.CommandType = CommandType.StoredProcedure;
                return  oDm.ExecuteNonQuery("usp_EmployeeLeaveUpdate");
            }
        }
        public static int UpdateAttendance(Entity.Common.EmployeeAttendance empAttendance)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@Id", SqlDbType.Int, empAttendance.Id);
                oDm.Add("@EmployeeId", SqlDbType.Int, empAttendance.EmployeeId);
                oDm.Add("@EmpCode", SqlDbType.VarChar, 10, empAttendance.EmpCode);
                oDm.Add("@year", SqlDbType.Int, empAttendance.Year);
                oDm.Add("@Month", SqlDbType.Int, empAttendance.Month);
                oDm.Add("@TotalDays", SqlDbType.Decimal, empAttendance.TotalDays);
                oDm.Add("@Present", SqlDbType.Decimal, empAttendance.Present);
                oDm.Add("@Absent", SqlDbType.Decimal, empAttendance.Absent);
                oDm.Add("@CL", SqlDbType.Decimal, empAttendance.CL);
                oDm.Add("@EL", SqlDbType.Decimal, empAttendance.EL);
                oDm.Add("@Medical", SqlDbType.Decimal, empAttendance.Medical);
                oDm.Add("@SpecialLeave", SqlDbType.Decimal, empAttendance.SpecialLeave);
                oDm.Add("@Holiday", SqlDbType.Decimal, empAttendance.Holiday);
                oDm.Add("@OffDay", SqlDbType.Decimal, empAttendance.OffDay);
                oDm.Add("@TotalPayDay", SqlDbType.Decimal, empAttendance.TotalPayDay);
                //oDm.Add("@CreatedBy", SqlDbType.Int, );

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteNonQuery("usp_AttendanceUpdate");
            }
        }

    }
}
