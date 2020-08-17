using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.HR
{
    public class ExpenseType
    {
        public ExpenseType()
        {
        }

        public static int Save(Entity.HR.ExpenseType ObjType)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pIntMode", SqlDbType.Int, 1);
                oDm.Add("@pExpenseTypeId", SqlDbType.Int, ObjType.ExpenseTypeId);
                oDm.Add("@pExpenseTypeName", SqlDbType.VarChar,30, ObjType.ExpenseTypeName);

                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_ExpenseType");
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pIntMode", SqlDbType.Int, 2);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_ExpenseType");
            }
        }

        public static Entity.HR.ExpenseType GetAllById(int ExpenseTypeId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pIntMode", SqlDbType.Int, 3);
                oDm.Add("@pExpenseTypeId", SqlDbType.Int, ExpenseTypeId);
                SqlDataReader dr = oDm.ExecuteReader("usp_ExpenseType");
                Entity.HR.ExpenseType Type = new Entity.HR.ExpenseType();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Type.ExpenseTypeId = ExpenseTypeId;
                        Type.ExpenseTypeName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();

                    }
                }
                return Type;
            }
        }

        public static int Delete(int ExpenseTypeId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pIntMode", SqlDbType.Int, 4);
                oDm.Add("@pExpenseTypeId", SqlDbType.Int, ExpenseTypeId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteNonQuery("usp_ExpenseType");
            }
        }
    }
}
