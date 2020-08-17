using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Student
{
    public class StudentSubjectMapping
    {
        public StudentSubjectMapping()
        {
        }

        public static void Save(Entity.Student.StudentSubjectMapping mapping)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@ElectiveSubjectId", SqlDbType.Int, mapping.ElectiveSubjectId);
                oDm.Add("@StudentIdXML", SqlDbType.Xml, mapping.StudentIdXML);

                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_StudentSubjectMapping_Save");
            }
        }

        public static DataTable GetAll(Entity.Student.StudentSubjectMapping mapping)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@batch_id", SqlDbType.Int, mapping.batch_id);
                oDm.Add("@CourseId", SqlDbType.Int, mapping.CourseId);
                oDm.Add("@StreamId", SqlDbType.Int, mapping.StreamId);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_StudentSubjectMapping_GetAll");
            }
        }

        public static DataTable GetAllById(int StudentId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@StudentId", SqlDbType.Int, StudentId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_StudentSubjectMapping_GetAllById");
            }
        }

        public static void Delete(int Id)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@Id", SqlDbType.Int, Id);
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_StudentSubjectMapping_Delete");
            }
        }
    }
}
