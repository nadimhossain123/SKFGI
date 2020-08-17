using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Student
{
    public class StudentAttendance
    {
        public StudentAttendance()
        {
        }

        public static DataSet GetAll(Entity.Student.StudentAttendance attendance)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@BatchId", SqlDbType.Int, ParameterDirection.Input, attendance.BatchId);
                oDm.Add("@CourseId", SqlDbType.Int, ParameterDirection.Input, attendance.CourseId);
                oDm.Add("@StreamId", SqlDbType.Int, ParameterDirection.Input, attendance.StreamId);
                oDm.Add("@SectionId", SqlDbType.Int, ParameterDirection.Input, attendance.SectionId);
                oDm.Add("@SubjectId", SqlDbType.Int, ParameterDirection.Input, attendance.SubjectId);

                if (attendance.SubSubjectId > 0)
                    oDm.Add("@SubSubjectId", SqlDbType.Int, attendance.SubSubjectId);
                else
                    oDm.Add("@SubSubjectId", SqlDbType.Int, DBNull.Value);

                oDm.Add("@AttendanceDate", SqlDbType.DateTime, ParameterDirection.Input, attendance.AttendanceDate);
                oDm.Add("@Period", SqlDbType.Int, ParameterDirection.Input, attendance.Period);

                DataSet ds = new DataSet();
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.GetDataSet("usp_StudentAttendance_GetAll",ref ds,"Table");
            }
        }

        public static string Save(Entity.Student.StudentAttendance attendance)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@BatchId", SqlDbType.Int, ParameterDirection.Input, attendance.BatchId);
                oDm.Add("@CourseId", SqlDbType.Int, ParameterDirection.Input, attendance.CourseId);
                oDm.Add("@StreamId", SqlDbType.Int, ParameterDirection.Input, attendance.StreamId);
                oDm.Add("@SectionId", SqlDbType.Int, ParameterDirection.Input, attendance.SectionId);
                oDm.Add("@SubjectId", SqlDbType.Int, ParameterDirection.Input, attendance.SubjectId);
                oDm.Add("@AttendanceDate", SqlDbType.DateTime, ParameterDirection.Input, attendance.AttendanceDate);
                oDm.Add("@Period", SqlDbType.Int, ParameterDirection.Input, attendance.Period);
                oDm.Add("@Remarks", SqlDbType.NVarChar, 200, ParameterDirection.Input, attendance.Remarks);
                oDm.Add("@CreatedBy", SqlDbType.Int, ParameterDirection.Input, attendance.CreatedBy);
                oDm.Add("@StudentIdXML", SqlDbType.Xml, ParameterDirection.Input, attendance.StudentIdXML);
                oDm.Add("@UpdateAccess", SqlDbType.Bit,ParameterDirection.Input, attendance.UpdateAccess);
                oDm.Add("@Message", SqlDbType.VarChar, 100, ParameterDirection.InputOutput, "");

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_student_attendance_Save");
                return (string)oDm["@Message"].Value;
            }
        }
                
        public static DataTable GetAttendanceReport(Entity.Student.StudentAttendance attendance)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@BatchId", SqlDbType.Int, ParameterDirection.Input, attendance.BatchId);
                oDm.Add("@CourseId", SqlDbType.Int, ParameterDirection.Input, attendance.CourseId);
                oDm.Add("@StreamId", SqlDbType.Int, ParameterDirection.Input, attendance.StreamId);

                if (attendance.SectionId > 0)
                    oDm.Add("@SectionId", SqlDbType.Int, ParameterDirection.Input, attendance.SectionId);
                else
                    oDm.Add("@SectionId", SqlDbType.Int, ParameterDirection.Input, DBNull.Value);

                if (attendance.StudentId > 0)
                    oDm.Add("@StudentId", SqlDbType.Int, ParameterDirection.Input, attendance.StudentId);
                else
                    oDm.Add("@StudentId", SqlDbType.Int, ParameterDirection.Input, DBNull.Value);

                oDm.Add("@Fromdate", SqlDbType.DateTime, ParameterDirection.Input,attendance.Fromdate);
                oDm.Add("@Todate", SqlDbType.DateTime, ParameterDirection.Input,attendance.Todate);
                oDm.Add("@SubjectType", SqlDbType.Int, ParameterDirection.Input, attendance.SubjectType);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_StudentAttendance_DatewiseReport");
            }
        }

        public static DataSet GetSubjectWiseAttendanceReport(Entity.Student.StudentAttendance attendance)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@BatchId", SqlDbType.Int, ParameterDirection.Input, attendance.BatchId);
                oDm.Add("@CourseId", SqlDbType.Int, ParameterDirection.Input, attendance.CourseId);
                oDm.Add("@StreamId", SqlDbType.Int, ParameterDirection.Input, attendance.StreamId);

                if (attendance.SectionId > 0)
                    oDm.Add("@SectionId", SqlDbType.Int, ParameterDirection.Input, attendance.SectionId);
                else
                    oDm.Add("@SectionId", SqlDbType.Int, ParameterDirection.Input, DBNull.Value);

                if (attendance.StudentId > 0)
                    oDm.Add("@StudentId", SqlDbType.Int, ParameterDirection.Input, attendance.StudentId);
                else
                    oDm.Add("@StudentId", SqlDbType.Int, ParameterDirection.Input, DBNull.Value);

                oDm.Add("@Fromdate", SqlDbType.DateTime, ParameterDirection.Input, attendance.Fromdate);
                oDm.Add("@Todate", SqlDbType.DateTime, ParameterDirection.Input, attendance.Todate);
                oDm.Add("@SubjectType", SqlDbType.Int, ParameterDirection.Input, attendance.SubjectType);

                DataSet ds = new DataSet();
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.GetDataSet("usp_AttendanceSubjectWiseReport",ref ds,"Table");
            }
        }

        public static int Delete(Entity.Student.StudentAttendance attendance)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@BatchId", SqlDbType.Int, ParameterDirection.Input, attendance.BatchId);
                oDm.Add("@CourseId", SqlDbType.Int, ParameterDirection.Input, attendance.CourseId);
                oDm.Add("@StreamId", SqlDbType.Int, ParameterDirection.Input, attendance.StreamId);
                oDm.Add("@SectionId", SqlDbType.Int, ParameterDirection.Input, attendance.SectionId);
                oDm.Add("@SubjectId", SqlDbType.Int, ParameterDirection.Input, attendance.SubjectId);
                oDm.Add("@AttendanceDate", SqlDbType.DateTime, ParameterDirection.Input, attendance.AttendanceDate);
                oDm.Add("@Period", SqlDbType.Int, ParameterDirection.Input, attendance.Period);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteNonQuery("usp_student_attendance_Delete");
            }
        }
    }
}
