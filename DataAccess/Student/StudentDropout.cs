using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess.Student
{
   public class StudentDropout
    {
          public static int Save( Entity.Student.StudentDropout objStuDrop)
           {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@int_mode", SqlDbType.Int, objStuDrop.intMode);
                oDm.Add("@Student_Id", SqlDbType.Int, objStuDrop.Student_Id);
                oDm.Add("@Sem", SqlDbType.Int, objStuDrop.Sem);
                oDm.Add("@DODate", SqlDbType.DateTime, objStuDrop.DODate);
                oDm.Add("@login_id", SqlDbType.NVarChar, objStuDrop.login_id );
                oDm.Add("@Reason", SqlDbType.NVarChar, objStuDrop.Reason );
                oDm.CommandType = CommandType.StoredProcedure;

                int RowsAffected = oDm.ExecuteNonQuery("usp_Student_Dropout");
                return RowsAffected;
            }
           }
          public static DataTable GetAll(int BatchId)
          {
              using (DataManager oDm = new DataManager())
              {
                  oDm.Add("@int_mode", SqlDbType.Int,2);
                  oDm.Add("@login_id", SqlDbType.NVarChar, String.Empty);
                  oDm.Add("@BatchId", SqlDbType.Int, BatchId);
                  //oDm.Add("@DateTo", SqlDbType.DateTime, DateTo);

                  oDm.CommandType = CommandType.StoredProcedure;
                  return oDm.ExecuteDataTable("usp_Student_Dropout");
              }
          }

          public DataTable FindIsStudentDropout(int StudentId)
          {
              using (DataManager oDm = new DataManager())
              {
                  oDm.Add("@StudentId", SqlDbType.Int, StudentId);
                  oDm.CommandType = CommandType.StoredProcedure;
                  DataTable dt = new DataTable();
                  dt = oDm.ExecuteDataTable("usp_Student_IsDropOut");
                  return dt;
              }
          }

        public static void Delete(int Id)
        {
            using (DataManager oDm = new DataAccess.DataManager())
            {
                oDm.Add("@pId", SqlDbType.Int, Id);
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_Student_Dropout_Delete");
            }
        }
    }
}
