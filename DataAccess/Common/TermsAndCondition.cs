using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class TermsAndCondition
    {
        public TermsAndCondition()
        {
        }

        public static void Save(Entity.Common.TermsAndCondition Terms)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@int_mode", SqlDbType.Int, 1);
                oDm.Add("@TermsId", SqlDbType.Int, Terms.TermsId);
                oDm.Add("@TermsName", SqlDbType.VarChar,8000, Terms.TermsName);
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_TermsAndCondition");
            }
        }

        public static DataTable GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@int_mode", SqlDbType.Int, 2);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_TermsAndCondition");
            }
        }

        public static Entity.Common.TermsAndCondition GetAllById(int TermsId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, 3);
                oDm.Add("@TermsId", SqlDbType.Int, TermsId);
                SqlDataReader dr = oDm.ExecuteReader("usp_TermsAndCondition");
                Entity.Common.TermsAndCondition Terms = new Entity.Common.TermsAndCondition();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Terms.TermsId = TermsId;
                        Terms.TermsName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();

                    }
                }
                return Terms;
            }
        }

        public static DataTable GetAllEmployeeTerms(int EmployeeId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, 4);
                oDm.Add("@EmployeeId", SqlDbType.Int, EmployeeId);
                return oDm.ExecuteDataTable("usp_TermsAndCondition");
            }
        }

        public static void SaveEmployeeTerms(Entity.Common.TermsAndCondition Terms)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@int_mode", SqlDbType.Int, 5);
                oDm.Add("@TermsId", SqlDbType.Int, Terms.TermsId);
                oDm.Add("@EmployeeId", SqlDbType.Int, Terms.EmployeeId);
                oDm.Add("@IsChecked", SqlDbType.Bit, Terms.IsChecked);
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_TermsAndCondition");
            }
        }
    }
}
