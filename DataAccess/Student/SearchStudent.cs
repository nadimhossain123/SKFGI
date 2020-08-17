using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.student
{
    public class SearchStudent
    {
        public static DataTable GetAllStudent(Entity.Student.SearchStudent searchS)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, searchS.intMode);
                if (searchS.appliation_no.Trim().Length == 0) { oDm.Add("@appliation_no", SqlDbType.NVarChar, 20, DBNull.Value); }
                else { oDm.Add("@appliation_no", SqlDbType.NVarChar, 20, searchS.appliation_no); }

                if (searchS.name.Trim().Length == 0) { oDm.Add("@name", SqlDbType.NVarChar, 200, DBNull.Value); }
                else { oDm.Add("@name", SqlDbType.NVarChar, 200, searchS.name); }

                if (searchS.CourseId == 0) { oDm.Add("@CourseId", SqlDbType.Int, DBNull.Value); }
                else { oDm.Add("@CourseId", SqlDbType.Int, searchS.CourseId); }

                if (searchS.StreamId == 0) { oDm.Add("@StreamId", SqlDbType.Int, DBNull.Value); }
                else { oDm.Add("@StreamId", SqlDbType.Int, searchS.StreamId); }

                if (searchS.batch_id == 0) { oDm.Add("@batch_id", SqlDbType.Int, DBNull.Value); }
                else { oDm.Add("@batch_id", SqlDbType.Int, searchS.batch_id); }

                return oDm.ExecuteDataTable("usp_search_student");
            }
        }

        public static DataTable GetNewAdmissionReport(Entity.Student.SearchStudent searchS)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                
                if (searchS.CourseId == 0) { oDm.Add("@CourseId", SqlDbType.Int, DBNull.Value); }
                else { oDm.Add("@CourseId", SqlDbType.Int, searchS.CourseId); }

                if (searchS.StreamId == 0) { oDm.Add("@StreamId", SqlDbType.Int, DBNull.Value); }
                else { oDm.Add("@StreamId", SqlDbType.Int, searchS.StreamId); }

                if (searchS.batch_id == 0) { oDm.Add("@batch_id", SqlDbType.Int, DBNull.Value); }
                else { oDm.Add("@batch_id", SqlDbType.Int, searchS.batch_id); }

                if (searchS.StateId == 0) { oDm.Add("@StateID", SqlDbType.Int, DBNull.Value); }
                else { oDm.Add("@StateID", SqlDbType.Int, searchS.StateId); }

                if (searchS.DistrictId == 0) { oDm.Add("@DistrictID", SqlDbType.Int, DBNull.Value); }
                else { oDm.Add("@DistrictID", SqlDbType.Int, searchS.DistrictId); }

                if (searchS.CityId == 0) { oDm.Add("@CityID", SqlDbType.Int, DBNull.Value); }
                else { oDm.Add("@CityID", SqlDbType.Int, searchS.CityId); }

                if (searchS.SchoolId == 0) { oDm.Add("@SchoolID", SqlDbType.Int, DBNull.Value); }
                else { oDm.Add("@SchoolID", SqlDbType.Int, searchS.SchoolId); }
                if (searchS.FromDate == DateTime.MinValue) { oDm.Add("@FromDate", SqlDbType.Date, DBNull.Value); }
                else oDm.Add("@FromDate", SqlDbType.Date, searchS.FromDate);
                if (searchS.ToDate == DateTime.MinValue) { oDm.Add("@ToDate", SqlDbType.Date, DBNull.Value); }
                else oDm.Add("@ToDate", SqlDbType.Date, searchS.ToDate);

                return oDm.ExecuteDataTable("usp_NewStudentAdmissionReport");
            }
        }

        public static void ChangeStudentPhoto(Entity.Student.SearchStudent searchS)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, searchS.intMode);
                oDm.Add("@id", SqlDbType.Int, searchS.id);
                oDm.Add("@Photo", SqlDbType.VarChar,50, searchS.Photo);
                oDm.ExecuteNonQuery("usp_search_student");
            }
        }

        public static int GetStudentCourseId(int StudentId)
        {
            using (DataManager oDm = new DataManager())
            {
                string sql = string.Format("Select CourseId from tbl_student where id={0}", StudentId);
                oDm.CommandType = CommandType.Text;
                DataTable dt = oDm.ExecuteDataTable(sql);
                if (dt.Rows.Count > 0)
                    return Convert.ToInt32(dt.Rows[0][0].ToString());
                else
                    return 0;
            }
        }

        public static DataTable GetStudentIDCard(int id)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, 3);
                oDm.Add("@id", SqlDbType.Int, id);
                return oDm.ExecuteDataTable("usp_search_student");
            }
        }

        //---------Add------------
        public static DataTable GetStudentStream(int CourseId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, 4);
                oDm.Add("@CourseId", SqlDbType.Int, CourseId);
                return oDm.ExecuteDataTable("usp_search_student");
            }
        }
        public static int getStreamId(int Id)
        {
            using (DataManager oDm = new DataManager())
            {
                string sql = string.Format("select isnull(StreamId,'0') As StreamId from tbl_student where id={0}", Id);
                oDm.CommandType = CommandType.Text;
                DataTable dt = oDm.ExecuteDataTable(sql);
                if (dt.Rows.Count > 0)
                    if (Convert.ToInt32(dt.Rows[0][0].ToString()) == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return Convert.ToInt32(dt.Rows[0][0].ToString());

                    }
                else
                    return 0;
            }
        }
        public static int Update(int Id, int StreamId,int BatchId)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.Add("@Id", SqlDbType.Int, 50, Id);
                oDm.Add("@StreamId", SqlDbType.Int, 50, StreamId);
                oDm.Add("@batch_idNew", SqlDbType.Int, 50, BatchId);
                oDm.Add("@int_mode", SqlDbType.Int, 5);

                oDm.CommandType = CommandType.StoredProcedure;

                int RowsAffected = oDm.ExecuteNonQuery("usp_search_student");
                return RowsAffected;
            }
        }
        //Batch
        public static DataTable GetStudentBatch(int Id)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, 7);
                oDm.Add("@id", SqlDbType.Int, Id);
                return oDm.ExecuteDataTable("usp_search_student");
            }
        }
        //Fee
        public static DataTable GetStudentFees(int Id)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, 6);
                oDm.Add("@id", SqlDbType.Int, Id);
                return oDm.ExecuteDataTable("usp_search_student");
            }
        }
        public static int getFeesId(int Id)
        {
            using (DataManager oDm = new DataManager())
            {
                string sql = string.Format("select isnull(FeesId,'0') as FeesId from tbl_student where id={0}", Id);
                oDm.CommandType = CommandType.Text;
                DataTable dt = oDm.ExecuteDataTable(sql);
                if (dt.Rows.Count > 0)
                    if (Convert.ToInt32(dt.Rows[0][0].ToString()) == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return Convert.ToInt32(dt.Rows[0][0].ToString());

                    }
                else
                    return 0;
            }
        }
        public static int getBatchId(int Id)
        {
            using (DataManager oDm = new DataManager())
            {
                string sql = string.Format("select isnull(batch_id,'0') as batch_id from tbl_student where id={0}", Id);
                oDm.CommandType = CommandType.Text;
                DataTable dt = oDm.ExecuteDataTable(sql);
                if (dt.Rows.Count > 0)
                    if (Convert.ToInt32(dt.Rows[0][0].ToString()) == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return Convert.ToInt32(dt.Rows[0][0].ToString());

                    }
                else
                    return 0;
            }
        }
        public static int UpdateFeesStructure(int Id, int FeesId)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.Add("@id", SqlDbType.Int, 50, Id);
                oDm.Add("@StreamId", SqlDbType.Int, 50, FeesId);//Here @StreamId used as Feesid
                oDm.Add("@int_mode", SqlDbType.Int, 7);

                oDm.CommandType = CommandType.StoredProcedure;

                int RowsAffected = oDm.ExecuteNonQuery("usp_search_student");
                return RowsAffected;
            }
        }
        //------------------------

    }
}
