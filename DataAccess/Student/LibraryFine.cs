using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.student
{
    public class LibraryFine
    {
        public LibraryFine()
        {
        }

        public static void Save(Entity.Student.LibraryFine Fine)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@LibraryFineId", SqlDbType.Int, ParameterDirection.InputOutput, Fine.LibraryFineId);
                oDm.Add("@StudentId", SqlDbType.Int, Fine.StudentId);

                oDm.Add("@ReasonForFine", SqlDbType.VarChar,200, Fine.ReasonForFine);
                oDm.Add("@FineAmount", SqlDbType.Decimal, Fine.FineAmount);
                oDm.Add("@CreatedBy", SqlDbType.Int, Fine.CreatedBy);
                oDm.Add("@ModifiedBy", SqlDbType.Int, Fine.ModifiedBy);
                oDm.Add("@FeesHeadId", SqlDbType.Int, Fine.FeesHeadId);
                oDm.Add("@SemNo", SqlDbType.Int, Fine.SemNo);

                oDm.Add("@CompanyID_FK", SqlDbType.Int, Fine.CompanyID_FK);
                oDm.Add("@BranchID_FK", SqlDbType.Int, Fine.BranchID_FK);
                oDm.Add("@FinYearID_FK", SqlDbType.Int, Fine.FinYearID_FK);
                oDm.Add("@DataFlow", SqlDbType.Int, Fine.DataFlow);
               
                oDm.CommandType = CommandType.StoredProcedure;

                oDm.ExecuteNonQuery("usp_LibraryFine_Save");
                Fine.LibraryFineId = (int)oDm["@LibraryFineId"].Value;
            }
        }

        public static DataTable GetAll(string VoucherNo,DateTime From,DateTime To)
        {
            using (DataManager oDm = new DataManager())
            {
                if (VoucherNo.Trim().Length == 0)
                    oDm.Add("@VoucherNo", SqlDbType.VarChar, 30, DBNull.Value);
                else 
                    oDm.Add("@VoucherNo", SqlDbType.VarChar, 30, VoucherNo);

                oDm.Add("@FromDate", SqlDbType.DateTime, From);
                oDm.Add("@ToDate", SqlDbType.DateTime, To); 

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_LibraryFine_GetAll");
            }
        }

        public static Entity.Student.LibraryFine GetAllById(int LibraryFineId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pLibraryFineId", SqlDbType.Int, LibraryFineId);
                SqlDataReader dr = oDm.ExecuteReader("usp_LibraryFine_GetAllById");
                Entity.Student.LibraryFine Fine = new Entity.Student.LibraryFine();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Fine.LibraryFineId = LibraryFineId;
                        Fine.VoucherNo = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                        Fine.StudentId = (dr[2] == DBNull.Value) ? 0 : int.Parse(dr[2].ToString());
                        Fine.ReasonForFine = (dr[3] == DBNull.Value) ? "" : dr[3].ToString();
                        Fine.FineAmount = (dr[4] == DBNull.Value) ? 0 : decimal.Parse(dr[4].ToString());
                        Fine.SemNo = (dr[5] == DBNull.Value) ? 0 : int.Parse(dr[5].ToString());

                    }
                }
                return Fine;
            }
        }

        public static DataTable GetApprovedStudentList()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_LibraryFine_GetApprovedStudentList");
            }
        }

        public DataTable GetApprovedStudentListWithDropOut()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_LibraryFine_GetApprovedStudentListWithDropOutStudent");
            }
        }


        public static DataTable GetNotDropoutList()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_LibraryFine_GetNotDropoutList");
            }
        }

        public static int LibraryFineDelete(int LibraryFineId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@LibraryFineId", SqlDbType.Int, LibraryFineId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteNonQuery("usp_LibraryFine_Delete");
            }
        }
    }
}
