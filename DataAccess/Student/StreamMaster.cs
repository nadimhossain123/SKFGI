using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Student
{
    public class StreamMaster
    {
        public StreamMaster()
        {
        }

        public static int Save(Entity.Student.StreamMaster Stream)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, 1);
                oDm.Add("@CourseId", SqlDbType.Int, Stream.CourseId);
                oDm.Add("@stream_name", SqlDbType.NVarChar, 20, Stream.StreamName);
                oDm.Add("@Capacity", SqlDbType.Int, Stream.Capacity);
                return oDm.ExecuteNonQuery("usp_stream_master");
            }
        }

        public static DataTable GetAll(int CourseId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, 2);
                if (CourseId == 0) { oDm.Add("@CourseId", SqlDbType.Int, DBNull.Value); }
                else { oDm.Add("@CourseId", SqlDbType.Int, CourseId); }
                
                return oDm.ExecuteDataTable("usp_stream_master");
            }
        }

        public static void ChangeCapacity(Entity.Student.StreamMaster Stream)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, 3);
                oDm.Add("@stream_id", SqlDbType.Int, Stream.StreamId);
                oDm.Add("@Capacity", SqlDbType.Int, Stream.Capacity);
                oDm.ExecuteNonQuery("usp_stream_master");
            }
        }
    }
}
