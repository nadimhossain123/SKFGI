using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Student
{
   public class NonAICTERegistration
    {
        public static DataTable GetAllCommonSP(Entity.Student.NonAICTERegistration NonAICTEReg)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, NonAICTEReg.intMode); //1-for selecting all batch 2-for selectting all course and 3- for selecting all stream by courseid 
                oDm.Add("@CourseId", SqlDbType.Int, NonAICTEReg.CourseId);
                return oDm.ExecuteDataTable("usp_NonAICTE_CommonSP");
            }
        }

        public static int Save(Entity.Student.NonAICTERegistration NonAICTEReg)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@int_mode", SqlDbType.Int, NonAICTEReg.intMode);
                oDm.Add("@int_company_id", SqlDbType.NVarChar, NonAICTEReg.intCompanyId);
                oDm.Add("@login_id", SqlDbType.NVarChar, NonAICTEReg.login_id);
                oDm.Add("@intStudentID", SqlDbType.Int, ParameterDirection.InputOutput, NonAICTEReg.studentID);
                oDm.Add("@CourseId", SqlDbType.Int, NonAICTEReg.CourseId);


                oDm.Add("@applicationNumber", SqlDbType.NVarChar, NonAICTEReg.strapplicationNumber);
                oDm.Add("@Batch", SqlDbType.NVarChar, NonAICTEReg.strBatch);
                oDm.Add("@EnrollmentNo", SqlDbType.NVarChar, NonAICTEReg.strEnrollmentNo);
                oDm.Add("@Rank", SqlDbType.NVarChar, NonAICTEReg.strRank);
                oDm.Add("@rankid", SqlDbType.NVarChar, NonAICTEReg.strRankid);

                //oDm.Add("@IsLateral", SqlDbType.Bit, NonAICTEReg.strIsLateral);
                oDm.Add("@RegistrationNo", SqlDbType.VarChar, 50, NonAICTEReg.strRegistrationNo);
                oDm.Add("@UniversityRollNo", SqlDbType.VarChar, 50, NonAICTEReg.strUniversityRollNo);

                oDm.Add("@MigrationInfo", SqlDbType.VarChar, 50, NonAICTEReg.strMigrationInfo);
                oDm.Add("@IsHostelFacility", SqlDbType.Bit, NonAICTEReg.strIsHostelFacility);
                //oDm.Add("@TFW", SqlDbType.Bit, NonAICTEReg.strTFW);
                oDm.Add("@IsReAdmission", SqlDbType.Bit, NonAICTEReg.strIsReAdmission);

                //oDm.Add("@Ece", SqlDbType.NVarChar, NonAICTEReg.strEce);
                //oDm.Add("@Ee", SqlDbType.NVarChar, NonAICTEReg.strEe);
                //oDm.Add("@Cse", SqlDbType.NVarChar, NonAICTEReg.strCse);
                //oDm.Add("@Aeie", SqlDbType.NVarChar, NonAICTEReg.strAeie);
                //oDm.Add("@Me", SqlDbType.NVarChar, NonAICTEReg.strMe);
                //oDm.Add("@Ce", SqlDbType.NVarChar, NonAICTEReg.strCe);

                oDm.Add("@StreamApplied", SqlDbType.NVarChar, NonAICTEReg.strStreamApplied);

                //oDm.Add("@Phy", SqlDbType.NVarChar, NonAICTEReg.strPhy);
                //oDm.Add("@Chem", SqlDbType.NVarChar, NonAICTEReg.strChem);
                //oDm.Add("@Math", SqlDbType.NVarChar, NonAICTEReg.strMath);
                //oDm.Add("@Engg", SqlDbType.NVarChar, NonAICTEReg.strEngg);
                //oDm.Add("@Bios", SqlDbType.NVarChar, NonAICTEReg.strBios);


                oDm.Add("@NameOfApplicant", SqlDbType.NVarChar, NonAICTEReg.strNameOfApplicant);
                oDm.Add("@Dob", SqlDbType.NVarChar, NonAICTEReg.strDob);
                oDm.Add("@FatherName", SqlDbType.NVarChar, NonAICTEReg.strFatherName);
                oDm.Add("@FatherOccupation", SqlDbType.NVarChar, NonAICTEReg.strFatherOccupation);
                oDm.Add("@MotherName", SqlDbType.NVarChar, NonAICTEReg.strMotherName);
                oDm.Add("@MotherOccupation", SqlDbType.NVarChar, NonAICTEReg.strMotherOccupation);
                oDm.Add("@GuardiansName", SqlDbType.NVarChar, NonAICTEReg.strGuardiansName);

                oDm.Add("@SchoolId", SqlDbType.Int, NonAICTEReg.SchoolID);
                oDm.Add("@PAddress", SqlDbType.NVarChar, NonAICTEReg.strPAddress);
                oDm.Add("@PState", SqlDbType.Int, NonAICTEReg.strPState);
                oDm.Add("@PDistrict", SqlDbType.Int, NonAICTEReg.strPDistrict);
                oDm.Add("@PCity", SqlDbType.Int, NonAICTEReg.strPCity);
                if (NonAICTEReg.strPPin.Length > 0)
                    oDm.Add("@PPin", SqlDbType.Int, NonAICTEReg.strPPin);
                else
                    oDm.Add("@PPin", SqlDbType.Int, DBNull.Value);
                oDm.Add("@CAddress", SqlDbType.NVarChar, NonAICTEReg.strCAddress);
                oDm.Add("@CState", SqlDbType.Int, NonAICTEReg.strCState);
                oDm.Add("@CDistrict", SqlDbType.Int, NonAICTEReg.strCDistrict);
                oDm.Add("@CCity", SqlDbType.Int, NonAICTEReg.strCCity);
                if (NonAICTEReg.strCPin.Length > 0)
                    oDm.Add("@CPin", SqlDbType.Int, NonAICTEReg.strCPin);
                else
                    oDm.Add("@CPin", SqlDbType.Int, DBNull.Value);

                //Added By Biswajit
                oDm.Add("@AdharNo", SqlDbType.NVarChar, NonAICTEReg.strAdhar);
                oDm.Add("@BankHolderName", SqlDbType.NVarChar, NonAICTEReg.strBankHName);
                oDm.Add("@BankAccNo", SqlDbType.NVarChar, NonAICTEReg.strBankAcc);
                oDm.Add("@BankIFSC", SqlDbType.NVarChar, NonAICTEReg.strBankIFSC);

                //End

                oDm.Add("@PResidential", SqlDbType.NVarChar, NonAICTEReg.strPResidential);
                oDm.Add("@SResidential", SqlDbType.NVarChar, NonAICTEReg.strSResidential);
                oDm.Add("@PMobile", SqlDbType.NVarChar, NonAICTEReg.strPMobile);
                oDm.Add("@SMobile", SqlDbType.NVarChar, NonAICTEReg.strSMobile);
                oDm.Add("@email", SqlDbType.NVarChar, NonAICTEReg.strEmail);
                oDm.Add("@HostalFacitity", SqlDbType.NVarChar, NonAICTEReg.strHostalFacitity);
                oDm.Add("@Gender", SqlDbType.NVarChar, NonAICTEReg.strGender);
                oDm.Add("@Marital", SqlDbType.NVarChar, NonAICTEReg.strMarital);
                oDm.Add("@MotherTong", SqlDbType.NVarChar, NonAICTEReg.strMotherTong);
                oDm.Add("@Nationality", SqlDbType.NVarChar, NonAICTEReg.strNationality);
                oDm.Add("@Realigion", SqlDbType.NVarChar, NonAICTEReg.strRealigion);
                oDm.Add("@Bengali", SqlDbType.NVarChar, NonAICTEReg.strBengali);
                oDm.Add("@Hindi", SqlDbType.NVarChar, NonAICTEReg.strHindi);
                oDm.Add("@English", SqlDbType.NVarChar, NonAICTEReg.strEnglish);
                oDm.Add("@Other", SqlDbType.NVarChar, NonAICTEReg.strOther);
                oDm.Add("@Cast", SqlDbType.NVarChar, NonAICTEReg.strCast);
                oDm.Add("@MonthlyIncome", SqlDbType.NVarChar, NonAICTEReg.strMonthlyIncome);
                oDm.Add("@RefferenceName1", SqlDbType.NVarChar, NonAICTEReg.strRefferenceName1);
                oDm.Add("@RefferenceAddress1", SqlDbType.NVarChar, NonAICTEReg.strRefferenceAddress1);
                oDm.Add("@RefferenceContactNumber1", SqlDbType.NVarChar, NonAICTEReg.strRefferenceContactNumber1);
                oDm.Add("@RefferenceName2", SqlDbType.NVarChar, NonAICTEReg.strRefferenceName2);
                oDm.Add("@RefferenceAddress2", SqlDbType.NVarChar, NonAICTEReg.strRefferenceAddress2);
                oDm.Add("@RefferenceContactNumber2", SqlDbType.NVarChar, NonAICTEReg.strRefferenceContactNumber2);

                oDm.Add("@XSubject", SqlDbType.NVarChar, NonAICTEReg.strXSubject);
                oDm.Add("@XBoard", SqlDbType.NVarChar, NonAICTEReg.strXBoard);
                oDm.Add("@XCollege", SqlDbType.NVarChar, NonAICTEReg.strXCollege);
                oDm.Add("@XYearOfPassing", SqlDbType.NVarChar, NonAICTEReg.strXYearOfPassing);
                oDm.Add("@XTotalMrkObtng", SqlDbType.NVarChar, NonAICTEReg.strXTotalMrkObtn);
                oDm.Add("@XMarks", SqlDbType.NVarChar, NonAICTEReg.strXMarks);

                oDm.Add("@XiiSubject", SqlDbType.NVarChar, NonAICTEReg.strXiiSubject);
                oDm.Add("@XiiBoard", SqlDbType.NVarChar, NonAICTEReg.strXiiBoard);
                oDm.Add("@XiiCollege", SqlDbType.NVarChar, NonAICTEReg.strXiiCollege);
                oDm.Add("@XiiYearOfPassing", SqlDbType.NVarChar, NonAICTEReg.strXiiYearOfPassing);
                oDm.Add("@XiiTotalMrkObtng", SqlDbType.NVarChar, NonAICTEReg.strXiiTotalMrkObtn);
                oDm.Add("@XiiMarks", SqlDbType.NVarChar, NonAICTEReg.strXiiMarks);

                oDm.Add("@DSubject", SqlDbType.NVarChar, NonAICTEReg.strDSubject);
                oDm.Add("@DBoard", SqlDbType.NVarChar, NonAICTEReg.strDBoard);
                oDm.Add("@DCollege", SqlDbType.NVarChar, NonAICTEReg.strDCollege);
                oDm.Add("@DYearOfPassing", SqlDbType.NVarChar, NonAICTEReg.strDYearOfPassing);
                oDm.Add("@DTotalMrkObtng", SqlDbType.NVarChar, NonAICTEReg.strDTotalMrkObtn);
                oDm.Add("@DMarks", SqlDbType.NVarChar, NonAICTEReg.strDMarks);

                oDm.Add("@GSubject", SqlDbType.NVarChar, NonAICTEReg.strGSubject);
                oDm.Add("@GBoard", SqlDbType.NVarChar, NonAICTEReg.strGBoard);
                oDm.Add("@GCollege", SqlDbType.NVarChar, NonAICTEReg.strGCollege);
                oDm.Add("@GYearOfPassing", SqlDbType.NVarChar, NonAICTEReg.strGYearOfPassing);
                oDm.Add("@GTotalMrkObtng", SqlDbType.NVarChar, NonAICTEReg.strGTotalMrkObtn);
                oDm.Add("@GMarks", SqlDbType.NVarChar, NonAICTEReg.strGMarks);

                oDm.Add("@DocList1", SqlDbType.NVarChar, NonAICTEReg.strDocList1);
                oDm.Add("@DocList2", SqlDbType.NVarChar, NonAICTEReg.strDocList2);
                oDm.Add("@DocList3", SqlDbType.NVarChar, NonAICTEReg.strDocList3);
                oDm.Add("@DocList4", SqlDbType.NVarChar, NonAICTEReg.strDocList4);
                oDm.Add("@DocList5", SqlDbType.NVarChar, NonAICTEReg.strDocList5);
                oDm.Add("@DocList6", SqlDbType.NVarChar, NonAICTEReg.strDocList6);
                oDm.Add("@DocList7", SqlDbType.NVarChar, NonAICTEReg.strDocList7);
                oDm.Add("@DocList8", SqlDbType.NVarChar, NonAICTEReg.strDocList8);
                oDm.Add("@DocList9", SqlDbType.NVarChar, NonAICTEReg.strDocList9);
                oDm.Add("@DocList10", SqlDbType.NVarChar, NonAICTEReg.strDocList10);
                oDm.Add("@BloodGroup", SqlDbType.VarChar, 8, NonAICTEReg.BloodGroup);
                oDm.Add("@Photo", SqlDbType.VarChar, 50, NonAICTEReg.strPhoto);
                oDm.Add("@SubmittedBy", SqlDbType.VarChar, 100, NonAICTEReg.SubmittedBy);

                oDm.CommandType = CommandType.StoredProcedure;

                int RowsAffected = oDm.ExecuteNonQuery("usp_NonAICTE_registration");
                NonAICTEReg.studentID = (int)oDm["@intStudentID"].Value;
                return RowsAffected;
            }
        }

        public static DataSet GetStudentDetails(Entity.Student.NonAICTERegistration NonAICTEReg)
        {
            using (DataManager oDm = new DataManager())
            {
                DataSet ds = new DataSet();
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, NonAICTEReg.intMode);
                oDm.Add("@int_company_id", SqlDbType.Int, NonAICTEReg.intCompanyId);
                oDm.Add("@intStudentID", SqlDbType.Int, NonAICTEReg.studentID);
                oDm.Add("@CourseId", SqlDbType.Int, NonAICTEReg.CourseId);
                return oDm.GetDataSet("usp_NonAICTE_registration", ref ds, "table");
            }
        }



    }
}
