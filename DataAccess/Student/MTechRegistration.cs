using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Student
{
    public class MTechRegistration
    {
        public MTechRegistration()
        {
        }

        public static int Save(Entity.Student.MTechRegistration mtechReg)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@int_mode", SqlDbType.Int, mtechReg.intMode);
                oDm.Add("@int_company_id", SqlDbType.NVarChar, mtechReg.intCompanyId);
                oDm.Add("@login_id", SqlDbType.NVarChar, mtechReg.login_id);
                oDm.Add("@intStudentID", SqlDbType.Int,ParameterDirection.InputOutput, mtechReg.studentID);
                oDm.Add("@CourseId", SqlDbType.Int, mtechReg.CourseId);

                oDm.Add("@applicationNumber", SqlDbType.NVarChar, mtechReg.strapplicationNumber);
                oDm.Add("@Batch", SqlDbType.NVarChar, mtechReg.strBatch);
                oDm.Add("@Rank", SqlDbType.NVarChar, mtechReg.strRank);
                oDm.Add("@rankid", SqlDbType.NVarChar, mtechReg.strRankid);
                oDm.Add("@RegistrationNo", SqlDbType.VarChar, 50, mtechReg.strRegistrationNo);
                oDm.Add("@UniversityRollNo", SqlDbType.VarChar, 50, mtechReg.strUniversityRollNo);
                oDm.Add("@MigrationInfo", SqlDbType.VarChar, 50, mtechReg.strMigrationInfo);
                oDm.Add("@IsHostelFacility", SqlDbType.Bit, mtechReg.strIsHostelFacility);

                oDm.Add("@NameOfApplicant", SqlDbType.NVarChar, mtechReg.strNameOfApplicant);
                oDm.Add("@Dob", SqlDbType.NVarChar, mtechReg.strDob);
                oDm.Add("@FatherName", SqlDbType.NVarChar, mtechReg.strFatherName);
                oDm.Add("@FatherOccupation", SqlDbType.NVarChar, mtechReg.strFatherOccupation);
                oDm.Add("@MotherName", SqlDbType.NVarChar, mtechReg.strMotherName);
                oDm.Add("@MotherOccupation", SqlDbType.NVarChar, mtechReg.strMotherOccupation);
                oDm.Add("@GuardiansName", SqlDbType.NVarChar, mtechReg.strGuardiansName);

                oDm.Add("@SchoolId", SqlDbType.Int, mtechReg.SchoolID);
                oDm.Add("@PAddress", SqlDbType.NVarChar, mtechReg.strPAddress);
                oDm.Add("@PState", SqlDbType.Int, mtechReg.strPState);
                oDm.Add("@PDistrict", SqlDbType.Int, mtechReg.strPDistrict);
                oDm.Add("@PCity", SqlDbType.Int, mtechReg.strPCity);
                if (mtechReg.strPPin.Length > 0)
                    oDm.Add("@PPin", SqlDbType.Int, mtechReg.strPPin);
                else
                    oDm.Add("@PPin", SqlDbType.Int, DBNull.Value);
                oDm.Add("@CAddress", SqlDbType.NVarChar, mtechReg.strCAddress);
                oDm.Add("@CState", SqlDbType.Int, mtechReg.strCState);
                oDm.Add("@CDistrict", SqlDbType.Int, mtechReg.strCDistrict);
                oDm.Add("@CCity", SqlDbType.Int, mtechReg.strCCity);
                if (mtechReg.strCPin.Length > 0)
                    oDm.Add("@CPin", SqlDbType.Int, mtechReg.strCPin);
                else
                    oDm.Add("@CPin", SqlDbType.Int, DBNull.Value);

                //Added By Biswajit
                oDm.Add("@AdharNo", SqlDbType.NVarChar, mtechReg.strAdhar);
                oDm.Add("@BankHolderName", SqlDbType.NVarChar, mtechReg.strBankHName);
                oDm.Add("@BankAccNo", SqlDbType.NVarChar, mtechReg.strBankAcc);
                oDm.Add("@BankIFSC", SqlDbType.NVarChar, mtechReg.strBankIFSC);

                //End

                oDm.Add("@PResidential", SqlDbType.NVarChar, mtechReg.strPResidential);
                oDm.Add("@SResidential", SqlDbType.NVarChar, mtechReg.strSResidential);
                oDm.Add("@PMobile", SqlDbType.NVarChar, mtechReg.strPMobile);
                oDm.Add("@SMobile", SqlDbType.NVarChar, mtechReg.strSMobile);
                oDm.Add("@email", SqlDbType.NVarChar, mtechReg.strEmail);
                oDm.Add("@HostalFacitity", SqlDbType.NVarChar, mtechReg.strHostalFacitity);
                oDm.Add("@Gender", SqlDbType.NVarChar, mtechReg.strGender);
                oDm.Add("@Marital", SqlDbType.NVarChar, mtechReg.strMarital);
                oDm.Add("@Nationality", SqlDbType.NVarChar, mtechReg.strNationality);
                oDm.Add("@Realigion", SqlDbType.NVarChar, mtechReg.strRealigion);
                oDm.Add("@Cast", SqlDbType.NVarChar, mtechReg.strCast);                

                oDm.Add("@XSubject", SqlDbType.NVarChar, mtechReg.strXSubject);
                oDm.Add("@XBoard", SqlDbType.NVarChar, mtechReg.strXBoard);
                oDm.Add("@XCollege", SqlDbType.NVarChar, mtechReg.strXCollege);
                oDm.Add("@XYearOfPassing", SqlDbType.NVarChar, mtechReg.strXYearOfPassing);
                oDm.Add("@XMarks", SqlDbType.NVarChar, mtechReg.strXMarks);

                oDm.Add("@XiiSubject", SqlDbType.NVarChar, mtechReg.strXiiSubject);
                oDm.Add("@XiiBoard", SqlDbType.NVarChar, mtechReg.strXiiBoard);
                oDm.Add("@XiiCollege", SqlDbType.NVarChar, mtechReg.strXiiCollege);
                oDm.Add("@XiiYearOfPassing", SqlDbType.NVarChar, mtechReg.strXiiYearOfPassing);
                oDm.Add("@XiiMarks", SqlDbType.NVarChar, mtechReg.strXiiMarks);

                oDm.Add("@GSubject", SqlDbType.NVarChar, mtechReg.strGSubject);
                oDm.Add("@GBoard", SqlDbType.NVarChar, mtechReg.strGBoard);
                oDm.Add("@GCollege", SqlDbType.NVarChar, mtechReg.strGCollege);
                oDm.Add("@GYearOfPassing", SqlDbType.NVarChar, mtechReg.strGYearOfPassing);
                oDm.Add("@GMarks", SqlDbType.NVarChar, mtechReg.strGMarks);

                oDm.Add("@DSubject", SqlDbType.NVarChar, mtechReg.strDSubject);
                oDm.Add("@DBoard", SqlDbType.NVarChar, mtechReg.strDBoard);
                oDm.Add("@DCollege", SqlDbType.NVarChar, mtechReg.strDCollege);
                oDm.Add("@DYearOfPassing", SqlDbType.NVarChar, mtechReg.strDYearOfPassing);
                oDm.Add("@DMarks", SqlDbType.NVarChar, mtechReg.strDMarks);
                
                oDm.Add("@DocList1", SqlDbType.NVarChar, mtechReg.strDocList1);
                oDm.Add("@DocList2", SqlDbType.NVarChar, mtechReg.strDocList2);
                oDm.Add("@DocList3", SqlDbType.NVarChar, mtechReg.strDocList3);
                oDm.Add("@DocList4", SqlDbType.NVarChar, mtechReg.strDocList4);
                oDm.Add("@DocList5", SqlDbType.NVarChar, mtechReg.strDocList5);
                oDm.Add("@DocList6", SqlDbType.NVarChar, mtechReg.strDocList6);
                oDm.Add("@DocList7", SqlDbType.NVarChar, mtechReg.strDocList7);
                oDm.Add("@DocList8", SqlDbType.NVarChar, mtechReg.strDocList8);
                
                oDm.Add("@StreamApplied", SqlDbType.NVarChar, 200, mtechReg.strStreamApplied);

                if (mtechReg.strGateScore == "")
                    oDm.Add("@GateScore", SqlDbType.NVarChar, DBNull.Value);
                else
                    oDm.Add("@GateScore", SqlDbType.NVarChar, mtechReg.strGateScore);

                oDm.Add("@AcademicYrExp", SqlDbType.NVarChar, mtechReg.strAcademicYrExp);
                oDm.Add("@IndustryYrExp", SqlDbType.NVarChar, mtechReg.strIndustryYrExp);
                oDm.Add("@Photo", SqlDbType.VarChar, 50, mtechReg.strPhoto);
                oDm.Add("@SubmittedBy", SqlDbType.VarChar, 100, mtechReg.SubmittedBy);

                oDm.CommandType = CommandType.StoredProcedure;

                int RowsAffected = oDm.ExecuteNonQuery("usp_MTech_registration");
                mtechReg.studentID = (int)oDm["@intStudentID"].Value;
                return RowsAffected;
            }
        }

        public static DataSet GetStudentDetails(Entity.Student.MTechRegistration mtechReg)
        {
            using (DataManager oDm = new DataManager())
            {
                DataSet ds = new DataSet();
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, mtechReg.intMode);
                oDm.Add("@int_company_id", SqlDbType.Int, mtechReg.intCompanyId);
                oDm.Add("@intStudentID", SqlDbType.Int, mtechReg.studentID);
                oDm.Add("@CourseId", SqlDbType.Int, mtechReg.CourseId);
                return oDm.GetDataSet("usp_MTech_registration", ref ds, "table");

            }
        }
    }
}
