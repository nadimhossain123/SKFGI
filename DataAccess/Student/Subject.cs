using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Student
{
    public class Subject
    {
        public Subject()
        {
        }

        public static int Save(Entity.Student.Subject Subject)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pSubjectId", SqlDbType.Int, Subject.SubjectId);
                oDm.Add("@pSubjectCode", SqlDbType.VarChar, 50, Subject.SubjectCode);
                oDm.Add("@pSubjectName", SqlDbType.VarChar, 100, Subject.SubjectName);

                if (Subject.ParentSubjectId_FK > 0)
                    oDm.Add("@pParentSubjectId_FK", SqlDbType.Int, Subject.ParentSubjectId_FK);
                else
                    oDm.Add("@pParentSubjectId_FK", SqlDbType.Int, DBNull.Value);

                oDm.Add("@pCourseId", SqlDbType.Int, Subject.CourseId);
                oDm.Add("@pStreamId", SqlDbType.Int, Subject.StreamId);
                oDm.Add("@pSemNo", SqlDbType.Int, Subject.SemNo);
                oDm.Add("@pIsPractical", SqlDbType.Bit, Subject.IsPractical);
                oDm.Add("@pMinAttendance", SqlDbType.Int, Subject.MinAttendence);

                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_Subject_Save");
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_Subject_GetAll");
            }
        }

        public static DataTable GetAllSubjectByEmployee(int CourseId, int StreamId, int EmployeeId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@CourseId", SqlDbType.Int, CourseId);
                oDm.Add("@StreamId", SqlDbType.Int, StreamId);
                oDm.Add("@EmployeeId", SqlDbType.Int, EmployeeId);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_Subject_GetAllByEmployee");
            }
        }

        public static Entity.Student.Subject GetAllById(int SubjectId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pSubjectId", SqlDbType.Int, SubjectId);
                SqlDataReader dr = oDm.ExecuteReader("usp_Subject_GetAllById");
                Entity.Student.Subject Subject = new Entity.Student.Subject();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Subject.SubjectId = SubjectId;
                        Subject.SubjectCode = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                        Subject.SubjectName = (dr[2] == DBNull.Value) ? "" : dr[2].ToString();
                        Subject.ParentSubjectId_FK = (dr[3] == DBNull.Value) ? 0 : int.Parse(dr[3].ToString());
                        Subject.CourseId = (dr[4] == DBNull.Value) ? 0 : int.Parse(dr[4].ToString());
                        Subject.StreamId = (dr[5] == DBNull.Value) ? 0 : int.Parse(dr[5].ToString());
                        Subject.SemNo = (dr[6] == DBNull.Value) ? 0 : int.Parse(dr[6].ToString());
                        Subject.IsPractical = (dr[7] == DBNull.Value) ? false : bool.Parse(dr[7].ToString());
                        Subject.MinAttendence = dr[8] == DBNull.Value ? 0 : int.Parse(dr[8].ToString());
                    }
                }
                return Subject;
            }
        }

        public static int Delete(int SubjectId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pSubjectId", SqlDbType.Int, SubjectId);
                return oDm.ExecuteNonQuery("usp_Subject_Delete");
            }
        }

        public static int SubjectMinAttendance_Update(bool isPractical, int minAttendance)
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Add("@SubjectType", SqlDbType.Bit, isPractical);
                dataManager.Add("@MinAttendance", SqlDbType.Int, (object)minAttendance);
                dataManager.CommandType = CommandType.StoredProcedure;
                return dataManager.ExecuteNonQuery("usp_SubjectMinAttendance_Update");
            }
        }
    }
}
