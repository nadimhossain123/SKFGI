using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class Grade
    {
        public Grade()
        {
        }
        public static int Save(Entity.Common.Grade Grade)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pGradeId", SqlDbType.Int, Grade.GradeId);
                oDm.Add("@pGradeName", SqlDbType.VarChar, 50, Grade.GradeName);

                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_Grade_Save");
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_Grade_GetAll");
            }
        }

        public static Entity.Common.Grade GetAllById(int GradeId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pGradeId", SqlDbType.Int, GradeId);
                SqlDataReader dr = oDm.ExecuteReader("usp_Grade_GetAllById");
                Entity.Common.Grade Grade = new Entity.Common.Grade();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Grade.GradeId = GradeId;
                        Grade.GradeName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();

                    }
                }
                return Grade;
            }
        }

        public static int Delete(int GradeId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pGradeId", SqlDbType.Int, GradeId);
                return oDm.ExecuteNonQuery("usp_Grade_Delete");
            }
        }
    }
}
