using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.student
{
    public class MBARegistration
    {
        public static DataTable GetAllBatch(Entity.Student.MBARegistration mbaReg)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, mbaReg.intMode);
                oDm.Add("@int_company_id", SqlDbType.Int, mbaReg.intCompanyId);
                return oDm.ExecuteDataTable("usp_MCA_registration");
            }
        }

        public static int Save(Entity.Student.MBARegistration mbaReg)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@int_mode", SqlDbType.Int, mbaReg.intMode);
                oDm.Add("@int_company_id", SqlDbType.NVarChar, mbaReg.intCompanyId);
                oDm.Add("@login_id", SqlDbType.NVarChar, mbaReg.login_id);
                oDm.Add("@intStudentID", SqlDbType.Int,ParameterDirection.InputOutput, mbaReg.studentID);
                oDm.Add("@CourseId", SqlDbType.Int, mbaReg.CourseId);

                oDm.Add("@applicationNumber", SqlDbType.NVarChar, mbaReg.strapplicationNumber);
                oDm.Add("@Batch", SqlDbType.NVarChar, mbaReg.strBatch);
                oDm.Add("@Rank", SqlDbType.NVarChar, mbaReg.strRank);
                oDm.Add("@rankid", SqlDbType.NVarChar, mbaReg.strRankid);
                oDm.Add("@RegistrationNo", SqlDbType.VarChar,50, mbaReg.strRegistrationNo);
                oDm.Add("@UniversityRollNo", SqlDbType.VarChar,50, mbaReg.strUniversityRollNo);
                oDm.Add("@MigrationInfo", SqlDbType.VarChar,50, mbaReg.strMigrationInfo);
                oDm.Add("@IsHostelFacility", SqlDbType.Bit, mbaReg.strIsHostelFacility);

                oDm.Add("@NameOfApplicant", SqlDbType.NVarChar, mbaReg.strNameOfApplicant);
                oDm.Add("@Dob", SqlDbType.NVarChar, mbaReg.strDob);
                oDm.Add("@FatherName", SqlDbType.NVarChar, mbaReg.strFatherName);
                oDm.Add("@FatherOccupation", SqlDbType.NVarChar, mbaReg.strFatherOccupation);
                oDm.Add("@MotherName", SqlDbType.NVarChar, mbaReg.strMotherName);
                oDm.Add("@MotherOccupation", SqlDbType.NVarChar, mbaReg.strMotherOccupation);
                oDm.Add("@GuardiansName", SqlDbType.NVarChar, mbaReg.strGuardiansName);

                oDm.Add("@SchoolId", SqlDbType.Int, mbaReg.SchoolID);
                oDm.Add("@PAddress", SqlDbType.NVarChar, mbaReg.strPAddress);
                oDm.Add("@PState", SqlDbType.Int, mbaReg.strPState);
                oDm.Add("@PDistrict", SqlDbType.Int, mbaReg.strPDistrict);
                oDm.Add("@PCity", SqlDbType.Int, mbaReg.strPCity);
                if (mbaReg.strPPin.Length > 0)
                    oDm.Add("@PPin", SqlDbType.Int, mbaReg.strPPin);
                else
                    oDm.Add("@PPin", SqlDbType.Int, DBNull.Value);
                oDm.Add("@CAddress", SqlDbType.NVarChar, mbaReg.strCAddress);
                oDm.Add("@CState", SqlDbType.Int, mbaReg.strCState);
                oDm.Add("@CDistrict", SqlDbType.Int, mbaReg.strCDistrict);
                oDm.Add("@CCity", SqlDbType.Int, mbaReg.strCCity);
                if (mbaReg.strCPin.Length > 0)
                    oDm.Add("@CPin", SqlDbType.Int, mbaReg.strCPin);
                else
                    oDm.Add("@CPin", SqlDbType.Int, DBNull.Value);



                //Added By Biswajit
                oDm.Add("@AdharNo", SqlDbType.NVarChar, mbaReg.strAdhar);
                oDm.Add("@BankHolderName", SqlDbType.NVarChar, mbaReg.strBankHName);
                oDm.Add("@BankAccNo", SqlDbType.NVarChar, mbaReg.strBankAcc);
                oDm.Add("@BankIFSC", SqlDbType.NVarChar, mbaReg.strBankIFSC);

                //End

                oDm.Add("@PResidential", SqlDbType.NVarChar, mbaReg.strPResidential);
                oDm.Add("@SResidential", SqlDbType.NVarChar, mbaReg.strSResidential);
                oDm.Add("@PMobile", SqlDbType.NVarChar, mbaReg.strPMobile);
                oDm.Add("@SMobile", SqlDbType.NVarChar, mbaReg.strSMobile);
                oDm.Add("@email", SqlDbType.NVarChar, mbaReg.strEmail);
                oDm.Add("@HostalFacitity", SqlDbType.NVarChar, mbaReg.strHostalFacitity);
                oDm.Add("@Gender", SqlDbType.NVarChar, mbaReg.strGender);
                oDm.Add("@Marital", SqlDbType.NVarChar, mbaReg.strMarital);
                oDm.Add("@MotherTong", SqlDbType.NVarChar, mbaReg.strMotherTong);
                oDm.Add("@Nationality", SqlDbType.NVarChar, mbaReg.strNationality);
                oDm.Add("@Realigion", SqlDbType.NVarChar, mbaReg.strRealigion);
                oDm.Add("@Bengali", SqlDbType.NVarChar, mbaReg.strBengali);
                oDm.Add("@Hindi", SqlDbType.NVarChar, mbaReg.strHindi);
                oDm.Add("@English", SqlDbType.NVarChar, mbaReg.strEnglish);
                oDm.Add("@Other", SqlDbType.NVarChar, mbaReg.strOther);
                oDm.Add("@Cast", SqlDbType.NVarChar, mbaReg.strCast);
                oDm.Add("@MonthlyIncome", SqlDbType.NVarChar, mbaReg.strMonthlyIncome);
                oDm.Add("@RefferenceName1", SqlDbType.NVarChar, mbaReg.strRefferenceName1);
                oDm.Add("@RefferenceAddress1", SqlDbType.NVarChar, mbaReg.strRefferenceAddress1);
                oDm.Add("@RefferenceContactNumber1", SqlDbType.NVarChar, mbaReg.strRefferenceContactNumber1);
                oDm.Add("@RefferenceName2", SqlDbType.NVarChar, mbaReg.strRefferenceName2);
                oDm.Add("@RefferenceAddress2", SqlDbType.NVarChar, mbaReg.strRefferenceAddress2);
                oDm.Add("@RefferenceContactNumber2", SqlDbType.NVarChar, mbaReg.strRefferenceContactNumber2);

                oDm.Add("@XSubject", SqlDbType.NVarChar, mbaReg.strXSubject);
                oDm.Add("@XBoard", SqlDbType.NVarChar, mbaReg.strXBoard);
                oDm.Add("@XCollege", SqlDbType.NVarChar, mbaReg.strXCollege);
                oDm.Add("@XYearOfPassing", SqlDbType.NVarChar, mbaReg.strXYearOfPassing);
                oDm.Add("@XMarks", SqlDbType.NVarChar, mbaReg.strXMarks);

                oDm.Add("@XiiSubject", SqlDbType.NVarChar, mbaReg.strXiiSubject);
                oDm.Add("@XiiBoard", SqlDbType.NVarChar, mbaReg.strXiiBoard);
                oDm.Add("@XiiCollege", SqlDbType.NVarChar, mbaReg.strXiiCollege);
                oDm.Add("@XiiYearOfPassing", SqlDbType.NVarChar, mbaReg.strXiiYearOfPassing);
                oDm.Add("@XiiMarks", SqlDbType.NVarChar, mbaReg.strXiiMarks);

                oDm.Add("@GSubject", SqlDbType.NVarChar, mbaReg.strGSubject);
                oDm.Add("@GBoard", SqlDbType.NVarChar, mbaReg.strGBoard);
                oDm.Add("@GCollege", SqlDbType.NVarChar, mbaReg.strGCollege);
                oDm.Add("@GYearOfPassing", SqlDbType.NVarChar, mbaReg.strGYearOfPassing);
                oDm.Add("@GMarks", SqlDbType.NVarChar, mbaReg.strGMarks);

                oDm.Add("@DSubject", SqlDbType.NVarChar, mbaReg.strDSubject);
                oDm.Add("@DBoard", SqlDbType.NVarChar, mbaReg.strDBoard);
                oDm.Add("@DCollege", SqlDbType.NVarChar, mbaReg.strDCollege);
                oDm.Add("@DYearOfPassing", SqlDbType.NVarChar, mbaReg.strDYearOfPassing);
                oDm.Add("@DMarks", SqlDbType.NVarChar, mbaReg.strDMarks);

                oDm.Add("@NameofComapny1  ", SqlDbType.NVarChar, mbaReg.strNameofComapny1);
                oDm.Add("@Responsibility1 ", SqlDbType.NVarChar, mbaReg.strResponsibility1);
                oDm.Add("@joning1 ", SqlDbType.NVarChar, mbaReg.strjoning1);
                oDm.Add("@Leaving1 ", SqlDbType.NVarChar, mbaReg.strLeaving1);
                oDm.Add("@Remarks1 ", SqlDbType.NVarChar, mbaReg.strRemarks1);

                oDm.Add("@NameofComapny2  ", SqlDbType.NVarChar, mbaReg.strNameofComapny2);
                oDm.Add("@Responsibility2 ", SqlDbType.NVarChar, mbaReg.strResponsibility2);
                oDm.Add("@joning2 ", SqlDbType.NVarChar, mbaReg.strjoning2);
                oDm.Add("@Leaving2 ", SqlDbType.NVarChar, mbaReg.strLeaving2);
                oDm.Add("@Remarks2 ", SqlDbType.NVarChar, mbaReg.strRemarks2);

                oDm.Add("@NameofComapny3  ", SqlDbType.NVarChar, mbaReg.strNameofComapny3);
                oDm.Add("@Responsibility3 ", SqlDbType.NVarChar, mbaReg.strResponsibility3);
                oDm.Add("@joning3 ", SqlDbType.NVarChar, mbaReg.strjoning3);
                oDm.Add("@Leaving3 ", SqlDbType.NVarChar, mbaReg.strLeaving3);
                oDm.Add("@Remarks3 ", SqlDbType.NVarChar, mbaReg.strRemarks3);

                oDm.Add("@test1  ", SqlDbType.NVarChar, mbaReg.strtest1);
                oDm.Add("@ExaminationDate1 ", SqlDbType.NVarChar, mbaReg.strExaminationDate1);
                oDm.Add("@Marks1 ", SqlDbType.NVarChar, mbaReg.strMarks1);

                oDm.Add("@test2  ", SqlDbType.NVarChar, mbaReg.strtest2);
                oDm.Add("@ExaminationDate2 ", SqlDbType.NVarChar, mbaReg.strExaminationDate2);
                oDm.Add("@Marks2 ", SqlDbType.NVarChar, mbaReg.strMarks2);

                oDm.Add("@test3  ", SqlDbType.NVarChar, mbaReg.strtest3);
                oDm.Add("@ExaminationDate3 ", SqlDbType.NVarChar, mbaReg.strExaminationDate2);
                oDm.Add("@Marks3 ", SqlDbType.NVarChar, mbaReg.strMarks3);

                oDm.Add("@DocList1", SqlDbType.NVarChar, mbaReg.strDocList1);
                oDm.Add("@DocList2", SqlDbType.NVarChar, mbaReg.strDocList2);
                oDm.Add("@DocList3", SqlDbType.NVarChar, mbaReg.strDocList3);
                oDm.Add("@DocList4", SqlDbType.NVarChar, mbaReg.strDocList4);
                oDm.Add("@DocList5", SqlDbType.NVarChar, mbaReg.strDocList5);
                oDm.Add("@DocList6", SqlDbType.NVarChar, mbaReg.strDocList6);
                oDm.Add("@DocList7", SqlDbType.NVarChar, mbaReg.strDocList7);
                oDm.Add("@DocList8", SqlDbType.NVarChar, mbaReg.strDocList8);
                oDm.Add("@DocList9", SqlDbType.NVarChar, mbaReg.strDocList9);

                oDm.Add("@BloodGroup", SqlDbType.VarChar,8, mbaReg.BloodGroup);
                oDm.Add("@StreamApplied", SqlDbType.NVarChar,200, mbaReg.strStreamApplied);
                oDm.Add("@Photo", SqlDbType.VarChar, 50, mbaReg.strPhoto);
                oDm.Add("@SubmittedBy", SqlDbType.VarChar, 100, mbaReg.SubmittedBy);
                oDm.CommandType = CommandType.StoredProcedure;

                int RowsAffected= oDm.ExecuteNonQuery("usp_MCA_registration");
                mbaReg.studentID = (int)oDm["@intStudentID"].Value;
                return RowsAffected;
            }
        }

        public static DataSet GetStudentDetails(Entity.Student.MBARegistration mbaReg)
        {
            using (DataManager oDm = new DataManager())
            {
                DataSet ds=new DataSet();
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, mbaReg.intMode);
                oDm.Add("@int_company_id", SqlDbType.Int, mbaReg.intCompanyId);
                oDm.Add("@intStudentID", SqlDbType.Int, mbaReg.studentID);
                oDm.Add("@CourseId", SqlDbType.Int, mbaReg.CourseId);
                return oDm.GetDataSet("usp_MCA_registration", ref ds, "table");
                //return oDm.GetDataSet("usp_MCA_registration");
                
            }
        }

    }
}
