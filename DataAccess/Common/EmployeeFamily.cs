using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class EmployeeFamily
    {
        public EmployeeFamily()
        {
        }

        public static void Save(Entity.Common.EmployeeFamily Family)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pEmployeeFamilyId", SqlDbType.Int, Family.EmployeeFamilyId);
                oDm.Add("@pEmployeeFamily_EmployeeId", SqlDbType.Int, Family.EmployeeFamily_EmployeeId);

                oDm.Add("@pMemberName", SqlDbType.VarChar, 100, Family.MemberName);
                oDm.Add("@pMemberOccupation", SqlDbType.VarChar, 50, Family.MemberOccupation);
                oDm.Add("@pMemberRelation", SqlDbType.VarChar, 30, Family.MemberRelation);
                oDm.Add("@pMemberGender", SqlDbType.VarChar, 8, Family.MemberGender);
                oDm.Add("@pHasMemberContact", SqlDbType.Char, 1, Family.HasMemberContact);
                oDm.Add("@pMemberContactEmail", SqlDbType.VarChar, 50, Family.MemberContactEmail);
                oDm.Add("@pMemberContactNo", SqlDbType.VarChar, 15, Family.MemberContactNo);
                oDm.Add("@pMemberAge", SqlDbType.Int, Family.MemberAge);

                oDm.Add("@pCreatedBy", SqlDbType.Int, Family.CreatedBy);
                oDm.Add("@pModifiedBy", SqlDbType.Int, Family.ModifiedBy);

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.ExecuteNonQuery("usp_EmployeeFamily_Save");
            }
        }

        public static DataTable GetAll(int EmployeeId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pEmployeeFamily_EmployeeId", SqlDbType.Int, EmployeeId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_EmployeeFamily_GetAll");
            }
        }

        public static Entity.Common.EmployeeFamily GetAllById(int EmployeeFamilyId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pEmployeeFamilyId", SqlDbType.Int, EmployeeFamilyId);
                SqlDataReader dr = oDm.ExecuteReader("usp_EmployeeFamily_GetAllById");
                Entity.Common.EmployeeFamily Family = new Entity.Common.EmployeeFamily();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Family.EmployeeFamilyId = EmployeeFamilyId;
                        Family.MemberName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                        Family.MemberOccupation = (dr[2] == DBNull.Value) ? "" : dr[2].ToString();
                        Family.MemberRelation = (dr[3] == DBNull.Value) ? "" : dr[3].ToString();

                        Family.MemberGender = (dr[4] == DBNull.Value) ? "Male" : dr[4].ToString();
                        Family.HasMemberContact = (dr[5] == DBNull.Value) ? "N" : dr[5].ToString();
                        Family.MemberContactEmail = (dr[6] == DBNull.Value) ? "" : dr[6].ToString();
                        Family.MemberContactNo = (dr[7] == DBNull.Value) ? "" : dr[7].ToString();
                        Family.MemberAge = (dr[8] == DBNull.Value) ? 0 : int.Parse(dr[8].ToString());

                    }
                }
                return Family;
            }
        }

        public static void Delete(int EmployeeFamilyId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pEmployeeFamilyId", SqlDbType.Int, EmployeeFamilyId);
                oDm.ExecuteNonQuery("usp_EmployeeFamily_Delete");
            }
        }
    }
}
