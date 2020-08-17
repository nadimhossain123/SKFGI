using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Student
{
    public class SemFeesGeneration
    {
        public SemFeesGeneration()
        {
        }

        public static void GenerateSemFees(Entity.Student.SemFeesGeneration SemFees)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@CompanyID_FK", SqlDbType.Int, SemFees.CompanyID_FK);
                oDm.Add("@BranchID_FK", SqlDbType.Int, SemFees.BranchID_FK);
                oDm.Add("@FinYearID_FK", SqlDbType.Int, SemFees.FinYearID_FK);
                oDm.Add("@DataFlow", SqlDbType.Int, SemFees.DataFlow);
                oDm.Add("@CourseId", SqlDbType.Int, SemFees.CourseId);
                oDm.Add("@batch_id", SqlDbType.Int, SemFees.batch_id);
                oDm.Add("@StreamId", SqlDbType.Int, SemFees.StreamId);
                oDm.Add("@SemNo", SqlDbType.Int, SemFees.SemNo);
                oDm.Add("@CreatedBy", SqlDbType.Int, SemFees.CreatedBy);
                oDm.Add("@BillDate", SqlDbType.DateTime, SemFees.BillDate);
                oDm.Add("@RowsAffected", SqlDbType.Int, ParameterDirection.InputOutput, SemFees.RowsAffected);
                
                oDm.ExecuteNonQuery("usp_SemFeesGeneration_Step1");
                SemFees.RowsAffected = (int)oDm["@RowsAffected"].Value;
            }
        }

        public static DataTable GetConsolidated_StudentOutstandingReport(Entity.Student.SemFeesGeneration SemFees)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                if (SemFees.CourseId == 0)
                    oDm.Add("@pCourseId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pCourseId", SqlDbType.Int, SemFees.CourseId);


                if (SemFees.batch_id == 0)
                    oDm.Add("@pbatch_id", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pbatch_id", SqlDbType.Int, SemFees.batch_id);
                

                if (SemFees.StreamId == 0)
                    oDm.Add("@pStreamId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pStreamId", SqlDbType.Int, SemFees.StreamId);


                if (SemFees.SemNo == 0)
                    oDm.Add("@pSemNo", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pSemNo", SqlDbType.Int, SemFees.SemNo);


                if (SemFees.FeesHeadId == 0)
                    oDm.Add("@pFeesHeadId", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@pFeesHeadId", SqlDbType.Int, SemFees.FeesHeadId);

                if (SemFees.FromDate == null)
                    oDm.Add("@pFromDate", SqlDbType.DateTime, DBNull.Value);
                else
                    oDm.Add("pFromDate", SqlDbType.DateTime, SemFees.FromDate);

                if (SemFees.ToDate == null)
                    oDm.Add("@pToDate", SqlDbType.DateTime, DBNull.Value);
                else
                    oDm.Add("@pToDate", SqlDbType.DateTime, SemFees.ToDate);

                oDm.Add("@ShowZeroDueBal", SqlDbType.Bit, SemFees.ShowZeroDueBal);

                
                return oDm.ExecuteDataTable("usp_GetConsolidatedStudentOutstandingReport");
            }
        }
    }
}
