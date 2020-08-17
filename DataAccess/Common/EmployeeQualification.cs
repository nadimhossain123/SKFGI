using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class EmployeeQualification
    {
        public EmployeeQualification()
        {
        }

        public static void Save(Entity.Common.EmployeeQualification Qualification)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pEmployeeQualificationId", SqlDbType.Int, Qualification.EmployeeQualificationId);
                oDm.Add("@pEmployeeQualification_EmployeeId", SqlDbType.Int, Qualification.EmployeeQualification_EmployeeId);

                oDm.Add("@pQualificationName", SqlDbType.VarChar,100, Qualification.QualificationName);
                oDm.Add("@pQualificationBoard", SqlDbType.VarChar,100, Qualification.QualificationBoard);
                oDm.Add("@pQualificationPassingYear", SqlDbType.Int, Qualification.QualificationPassingYear);
                oDm.Add("@pQualificationPercOfMarks", SqlDbType.Decimal, Qualification.QualificationPercOfMarks);
                oDm.Add("@pQualificationStream", SqlDbType.VarChar,50, Qualification.QualificationStream);
                oDm.Add("@pQualificationType", SqlDbType.VarChar,50, Qualification.QualificationType);

                oDm.Add("@pCreatedBy", SqlDbType.Int, Qualification.CreatedBy);
                oDm.Add("@pModifiedBy", SqlDbType.Int, Qualification.ModifiedBy);

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.ExecuteNonQuery("usp_EmployeeQualification_Save");
            }
        }

        public static DataTable GetAll(int EmployeeId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pEmployeeQualification_EmployeeId", SqlDbType.Int, EmployeeId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_EmployeeQualification_GetAll");
            }
        }

        public static Entity.Common.EmployeeQualification GetAllById(int EmployeeQualificationId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pEmployeeQualificationId", SqlDbType.Int, EmployeeQualificationId);
                SqlDataReader dr = oDm.ExecuteReader("usp_EmployeeQualification_GetAllById");
                Entity.Common.EmployeeQualification Qualification = new Entity.Common.EmployeeQualification();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Qualification.EmployeeQualificationId = EmployeeQualificationId;
                        Qualification.QualificationName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                        Qualification.QualificationBoard = (dr[2] == DBNull.Value) ? "" : dr[2].ToString();
                        Qualification.QualificationPassingYear = (dr[3] == DBNull.Value) ? 0 : int.Parse(dr[3].ToString());

                        Qualification.QualificationPercOfMarks = (dr[4] == DBNull.Value) ? 0 : decimal.Parse(dr[4].ToString());
                        Qualification.QualificationStream = (dr[5] == DBNull.Value) ? "" : dr[5].ToString();
                        Qualification.QualificationType = (dr[6] == DBNull.Value) ? "" : dr[6].ToString();

                    }
                }
                return Qualification;
            }
        }

        public static void Delete(int EmployeeQualificationId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pEmployeeQualificationId", SqlDbType.Int, EmployeeQualificationId);
                oDm.ExecuteNonQuery("usp_EmployeeQualification_Delete");
            }
        }
    }
}
