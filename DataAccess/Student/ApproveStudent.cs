using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.student
{
    public class ApproveStudent
    {
        public static DataSet GetAllStudent(Entity.Student.ApproveStudent Aps)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, Aps.intMode);
                oDm.Add("@batch_id", SqlDbType.Int, Aps.intBatchId);
                oDm.Add("@CourseId", SqlDbType.Int, Aps.intCourseID);
                oDm.Add("@student_id", SqlDbType.Int, Aps.intStudentID);
                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_approve_student", ref ds, "table");
            }
        }
        public static DataSet GetStudentById(Entity.Student.ApproveStudent Aps)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, Aps.intMode);             
                oDm.Add("@student_id", SqlDbType.Int, Aps.intStudentID);
                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_approve_student", ref ds, "table");
            }
        }
        public static DataSet PopulateLoadCombo(Entity.Student.ApproveStudent Aps)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, Aps.intMode);
                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_approve_student", ref ds, "table");
            }
        }

        public static int SaveDetails(Entity.Student.ApproveStudent Aps)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, Aps.intMode);
                oDm.Add("@student_id", SqlDbType.Int, Aps.intStudentID);
                oDm.Add("@StreamId", SqlDbType.Int, Aps.intStreamId);
                oDm.Add("@FeesId", SqlDbType.NVarChar, Aps.intFeesStructureID);
                oDm.Add("@updated_by", SqlDbType.Money, Aps.intupdatedBy);

                oDm.Add("@IsHostelFacility", SqlDbType.Bit, Aps.IsHostelFacility);
                oDm.Add("@IsLateral", SqlDbType.Bit, Aps.IsLateral);
                oDm.Add("@TFW", SqlDbType.Bit, Aps.TFW);

                oDm.Add("@CompanyID_FK", SqlDbType.Int, Aps.CompanyID_FK);
                oDm.Add("@BranchID_FK", SqlDbType.Int, Aps.BranchID_FK);
                oDm.Add("@FinYearID_FK", SqlDbType.Int, Aps.FinYearID_FK);
                oDm.Add("@DataFlow", SqlDbType.Int, Aps.DataFlow);
                oDm.Add("@HostelFeesId", SqlDbType.Int, Aps.HostelFeesId);
                return oDm.ExecuteNonQuery("usp_approve_student");

            }
        }
        public static DataSet PrintFees(Entity.Student.ApproveStudent Aps)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, Aps.intMode);
                oDm.Add("@student_id", SqlDbType.Int, Aps.intStudentID);
                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_approve_student", ref ds, "table");
            }
        }

        public static DataTable GetUserBaseApprovedStudentList(int ApprovedBy, DateTime FromDate, DateTime ToDate, bool IsApproved)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@ApprovedBy", SqlDbType.Int, ApprovedBy);
                oDm.Add("@FromDate", SqlDbType.DateTime, FromDate);
                oDm.Add("@ToDate", SqlDbType.DateTime, ToDate);
                oDm.Add("@IsApproved", SqlDbType.Bit, IsApproved);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_RPTUserBaseApprovedStudent");
            }
        }

    }
}
