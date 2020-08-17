using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class EmployeeOfficial
    {
        public EmployeeOfficial()
        {
        }

        public static void Save(Entity.Common.EmployeeOfficial Official)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pEmployeeOfficialId", SqlDbType.Int, ParameterDirection.InputOutput, Official.EmployeeOfficialId);
                oDm.Add("@pEmployeeOfficial_EmployeeId", SqlDbType.Int, Official.EmployeeOfficial_EmployeeId);
                oDm.Add("@pEmployeeOfficial_CategoryId", SqlDbType.Int, Official.EmployeeOfficial_CategoryId);
                oDm.Add("@pEmployeeOfficial_DepartmentId", SqlDbType.Int, Official.EmployeeOfficial_DepartmentId);
                oDm.Add("@pEmployeeOfficial_DesignationId", SqlDbType.Int, Official.EmployeeOfficial_DesignationId);
                oDm.Add("@pEmployeeOfficial_BranchId", SqlDbType.Int, Official.EmployeeOfficial_BranchId);
                oDm.Add("@pEmployeeOfficial_GradeId", SqlDbType.Int, Official.EmployeeOfficial_GradeId);

                oDm.Add("@pDOJ", SqlDbType.DateTime, Official.DOJ);

                if (Official.ConfDate == null)
                {
                    oDm.Add("@pConfDate", SqlDbType.DateTime, DBNull.Value);
                }
                else { oDm.Add("@pConfDate", SqlDbType.DateTime, Official.ConfDate); }

                if (Official.EffectiveDate == null)
                {
                    oDm.Add("@pEffectiveDate", SqlDbType.DateTime, DBNull.Value);
                }
                else { oDm.Add("@pEffectiveDate", SqlDbType.DateTime, Official.EffectiveDate); }

                oDm.Add("@pPTax", SqlDbType.Char,1, Official.PTax);
                oDm.Add("@pEvaluationType", SqlDbType.VarChar,20, Official.EvaluationType);
                if (Official.LastEvaluationDate == null)
                {
                    oDm.Add("@pLastEvaluationDate", SqlDbType.DateTime, DBNull.Value);
                }
                else { oDm.Add("@pLastEvaluationDate", SqlDbType.DateTime, Official.LastEvaluationDate); }

                oDm.Add("@pHasPF", SqlDbType.Char, 1, Official.HasPF);
                if (Official.PFEffectiveDate == null)
                {
                    oDm.Add("@pPFEffectiveDate", SqlDbType.DateTime, DBNull.Value);
                }
                else { oDm.Add("@pPFEffectiveDate", SqlDbType.DateTime, Official.PFEffectiveDate); }

                oDm.Add("@pPFNo", SqlDbType.VarChar, 20, Official.PFNo);
                oDm.Add("@pHasESI", SqlDbType.Char, 1, Official.HasESI);

                if (Official.ESIEffectiveDate == null)
                {
                    oDm.Add("@pESIEffectiveDate", SqlDbType.DateTime, DBNull.Value);
                }
                else { oDm.Add("@pESIEffectiveDate", SqlDbType.DateTime, Official.ESIEffectiveDate); }

                oDm.Add("@pESINo", SqlDbType.VarChar, 20, Official.ESINo);
                oDm.Add("@pHasTDS", SqlDbType.Char, 1, Official.HasTDS);

                if (Official.TDSEffectiveDate == null)
                {
                    oDm.Add("@pTDSEffectiveDate", SqlDbType.DateTime, DBNull.Value);
                }
                else { oDm.Add("@pTDSEffectiveDate", SqlDbType.DateTime, Official.TDSEffectiveDate); }

                oDm.Add("@pPANNo", SqlDbType.VarChar, 30, Official.PANNo);
                oDm.Add("@pHasHealthCard", SqlDbType.Char, 1, Official.HasHealthCard);
                oDm.Add("@pMediclaimNo", SqlDbType.VarChar, 30, Official.MediclaimNo);

                if (Official.DOR == null)
                {
                    oDm.Add("@pDOR", SqlDbType.DateTime, DBNull.Value);
                }
                else { oDm.Add("@pDOR", SqlDbType.DateTime, Official.DOR); }

                if (Official.DOL == null)
                {
                    oDm.Add("@pDOL", SqlDbType.DateTime, DBNull.Value);
                }
                else { oDm.Add("@pDOL", SqlDbType.DateTime, Official.DOL); }

                oDm.Add("@pNoticePeriod", SqlDbType.Int, Official.NoticePeriod);
                oDm.Add("@pReasonForLeaving", SqlDbType.VarChar, 200, Official.ReasonForLeaving);
                oDm.Add("@pCreatedBy", SqlDbType.Int, Official.CreatedBy);
                oDm.Add("@pModifiedBy", SqlDbType.Int, Official.ModifiedBy);
                oDm.Add("@pPayBandId", SqlDbType.Int, Official.PayBandId);

                oDm.Add("@pEmployeeType", SqlDbType.VarChar, 50, Official.EmployeeType);
                oDm.Add("@pWorkingDays", SqlDbType.Int, Official.WorkingDays);
                oDm.Add("@pFileNo", SqlDbType.VarChar, 20, Official.FileNo);
                oDm.Add("@pUNANo", SqlDbType.VarChar, 50, Official.UNANo);
                oDm.Add("@pAadhaar", SqlDbType.VarChar, 50, Official.Aadhaar);

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.ExecuteNonQuery("usp_EmployeeOfficial_Save");
                Official.EmployeeOfficialId = (int)oDm["@pEmployeeOfficialId"].Value;

            }
        }

        public static Entity.Common.EmployeeOfficial GetAllByEmployeeId(int EmployeeId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@pEmployeeOfficial_EmployeeId", SqlDbType.Int, ParameterDirection.Input, EmployeeId);
                SqlDataReader dr = oDm.ExecuteReader("usp_EmployeeOfficial_GetByEmployeeId");
                Entity.Common.EmployeeOfficial Official = new Entity.Common.EmployeeOfficial();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Official.EmployeeOfficialId = (dr[0] == DBNull.Value) ? 0 : int.Parse(dr[0].ToString());
                        Official.EmployeeOfficial_CategoryId = (dr[1] == DBNull.Value) ? 0 : int.Parse(dr[1].ToString());
                        Official.EmployeeOfficial_DepartmentId = (dr[2] == DBNull.Value) ? 0 : int.Parse(dr[2].ToString());
                        Official.EmployeeOfficial_DesignationId = (dr[3] == DBNull.Value) ? 0 : int.Parse(dr[3].ToString());
                        Official.EmployeeOfficial_BranchId = (dr[4] == DBNull.Value) ? 0 : int.Parse(dr[4].ToString());
                        Official.EmployeeOfficial_GradeId = (dr[5] == DBNull.Value) ? 0 : int.Parse(dr[5].ToString());

                        Official.DOJ = (dr[6] == DBNull.Value) ? DateTime.Now : DateTime.Parse(dr[6].ToString());

                        if (dr[7] == DBNull.Value) { Official.ConfDate = null; }
                        else { Official.ConfDate = DateTime.Parse(dr[7].ToString()); }

                        if (dr[8] == DBNull.Value) { Official.EffectiveDate = null; }
                        else { Official.EffectiveDate = DateTime.Parse(dr[8].ToString()); }

                        Official.PTax = (dr[9] == DBNull.Value) ? "N" : dr[9].ToString();
                        Official.EvaluationType = (dr[10] == DBNull.Value) ? "" : dr[10].ToString();

                        if (dr[11] == DBNull.Value) { Official.LastEvaluationDate = null; }
                        else { Official.LastEvaluationDate = DateTime.Parse(dr[11].ToString()); }

                        Official.HasPF = (dr[12] == DBNull.Value) ? "N" : dr[12].ToString();
                        if (dr[13] == DBNull.Value) { Official.PFEffectiveDate = null; }
                        else { Official.PFEffectiveDate = DateTime.Parse(dr[13].ToString()); }

                        Official.PFNo = (dr[14] == DBNull.Value) ? "" : dr[14].ToString();
                        Official.HasESI = (dr[15] == DBNull.Value) ? "N" : dr[15].ToString();

                        if (dr[16] == DBNull.Value) { Official.ESIEffectiveDate = null; }
                        else { Official.ESIEffectiveDate = DateTime.Parse(dr[16].ToString()); }

                        Official.ESINo = (dr[17] == DBNull.Value) ? "" : dr[17].ToString();
                        Official.HasTDS = (dr[18] == DBNull.Value) ? "N" : dr[18].ToString();

                        if (dr[19] == DBNull.Value) { Official.TDSEffectiveDate = null; }
                        else { Official.TDSEffectiveDate = DateTime.Parse(dr[19].ToString()); }

                        Official.PANNo = (dr[20] == DBNull.Value) ? "" : dr[20].ToString();
                        Official.HasHealthCard = (dr[21] == DBNull.Value) ? "N" : dr[21].ToString();
                        Official.MediclaimNo = (dr[22] == DBNull.Value) ? "" : dr[22].ToString();

                        if (dr[23] == DBNull.Value) { Official.DOR = null; }
                        else { Official.DOR = DateTime.Parse(dr[23].ToString()); }

                        if (dr[24] == DBNull.Value) { Official.DOL = null; }
                        else { Official.DOL = DateTime.Parse(dr[24].ToString()); }

                        Official.NoticePeriod = (dr[25] == DBNull.Value) ? 0 : int.Parse(dr[25].ToString());
                        Official.ReasonForLeaving = (dr[26] == DBNull.Value) ? "" : dr[26].ToString();
                        Official.PayBandId = (dr[27] == DBNull.Value) ? 0 : int.Parse(dr[27].ToString());
                        Official.EmployeeType = (dr[28] == DBNull.Value) ? "Teaching" : dr[28].ToString();
                        Official.WorkingDays = (dr[29] == DBNull.Value) ? 5 : int.Parse(dr[29].ToString());
                        Official.FileNo = (dr[30] == DBNull.Value) ? "" : dr[30].ToString();
                        Official.UNANo = (dr[31] == DBNull.Value) ? "" : dr[31].ToString();
                        Official.Aadhaar = (dr[32] == DBNull.Value) ? "" : dr[32].ToString();
                    }
                }
                return Official;
            }
        }
    }
}
