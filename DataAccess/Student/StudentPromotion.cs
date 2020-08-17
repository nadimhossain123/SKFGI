using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Student
{
    public class StudentPromotion
    {
        public StudentPromotion()
        {
        }

        public static DataTable GetStudentList(Entity.Student.StudentPromotion Promotion)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@int_mode", SqlDbType.Int, 1);
                if (Promotion.CourseId > 0)
                    oDm.Add("@CourseId", SqlDbType.Int, Promotion.CourseId);
                else
                    oDm.Add("@CourseId", SqlDbType.Int, DBNull.Value);

                if (Promotion.batch_id > 0)
                    oDm.Add("@batch_id", SqlDbType.Int, Promotion.batch_id);
                else
                    oDm.Add("@batch_id", SqlDbType.Int, DBNull.Value);

                if (Promotion.StreamId > 0)
                    oDm.Add("@StreamId", SqlDbType.Int, Promotion.StreamId);
                else
                    oDm.Add("@StreamId", SqlDbType.Int, DBNull.Value);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_StudentPromotion");
            }
        }

        public static void UpdatePromotion(Entity.Student.StudentPromotion Promotion)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@int_mode", SqlDbType.Int, 2);
                oDm.Add("@NewSemNo", SqlDbType.Int, Promotion.NewSemNo);
                oDm.Add("@StudentIDXML", SqlDbType.Xml, Promotion.StudentIDXML);
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_StudentPromotion");
            }
        }
    }
}
