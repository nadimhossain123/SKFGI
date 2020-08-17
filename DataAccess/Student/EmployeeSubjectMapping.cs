using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Student
{
    public class EmployeeSubjectMapping
    {
        public EmployeeSubjectMapping()
        {
        }

        public static void Save(Entity.Student.EmployeeSubjectMapping Mapping)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pEmployeeId", SqlDbType.Int, Mapping.EmployeeId);
                oDm.Add("@pSubjectId", SqlDbType.Int, Mapping.SubjectId);

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.ExecuteNonQuery("usp_EmployeeSubjectMapping_Save");
            }
        }

        //For displaying Teacher-Subject Count list
        public static DataTable GetAll(string FirstName)
        {
            using (DataManager oDm = new DataManager())
            {
                if (FirstName.Trim().Length == 0)
                {
                    oDm.Add("@pFirstName", SqlDbType.VarChar, 100, DBNull.Value);
                }
                else
                {
                    oDm.Add("@pFirstName", SqlDbType.VarChar, 100, FirstName);
                }
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_EmployeeSubjectMapping_GetAll");
            }
        }

        public static DataTable GetAllById(int EmployeeId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pEmployeeId", SqlDbType.Int, EmployeeId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_EmployeeSubjectMapping_GetAllById");
            }
        }

        public static void Delete(int EmployeeSubjectMappingId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pEmployeeSubjectMappingId", SqlDbType.Int, EmployeeSubjectMappingId);
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.ExecuteNonQuery("usp_EmployeeSubjectMapping_Delete");
            }
        }
    }
}
