using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class Category
    {
        public Category()
        {
        }

        public static int Save(Entity.Common.Category Category)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pCategoryId", SqlDbType.Int, Category.CategoryId);
                oDm.Add("@pCategoryName", SqlDbType.VarChar, 100, Category.CategoryName);

                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("usp_Category_Save");
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_Category_GetAll");
            }
        }

        public static Entity.Common.Category GetAllById(int CategoryId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pCategoryId", SqlDbType.Int, CategoryId);
                SqlDataReader dr = oDm.ExecuteReader("usp_Category_GetAllById");
                Entity.Common.Category Catagory = new Entity.Common.Category();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Catagory.CategoryId = CategoryId;
                        Catagory.CategoryName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();

                    }
                }
                return Catagory;
            
            }
        }
        public static int Delete(int CategoryId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pCategoryId", SqlDbType.Int, CategoryId);
                return oDm.ExecuteNonQuery("usp_Category_Delete");
            }
        
        }
    }
}
