using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Student
{
    public class DiplomaRegistration
    {

        //Staring of Save Function
        public static int Save(Entity.Student.DiplomaRegistration DipReg)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@int_mode", SqlDbType.Int, DipReg.intMode);
                oDm.Add("@int_company_id", SqlDbType.NVarChar, DipReg.intCompanyId);
                oDm.Add("@login_id", SqlDbType.NVarChar, DipReg.login_id);
                oDm.Add("@intStudentID", SqlDbType.Int, ParameterDirection.InputOutput, DipReg.studentID);
                oDm.Add("@CourseId", SqlDbType.Int, DipReg.CourseId);


                oDm.Add("@applicationNumber", SqlDbType.NVarChar, DipReg.strapplicationNumber);
                oDm.Add("@Batch", SqlDbType.NVarChar, DipReg.strBatch);
                oDm.Add("@EnrollmentNo", SqlDbType.NVarChar, DipReg.strEnrollmentNo);
                oDm.Add("@Rank", SqlDbType.NVarChar, DipReg.strRank);
                oDm.Add("@rankid", SqlDbType.NVarChar, DipReg.strRankid);

                oDm.Add("@IsLateral", SqlDbType.Bit, DipReg.strIsLateral);
                oDm.Add("@RegistrationNo", SqlDbType.VarChar, 50, DipReg.strRegistrationNo);
                oDm.Add("@UniversityRollNo", SqlDbType.VarChar, 50, DipReg.strUniversityRollNo);

                oDm.Add("@MigrationInfo", SqlDbType.VarChar, 50, DipReg.strMigrationInfo);
                oDm.Add("@IsHostelFacility", SqlDbType.Bit, DipReg.strIsHostelFacility);
                oDm.Add("@TFW", SqlDbType.Bit, DipReg.strTFW);
                oDm.Add("@IsReAdmission", SqlDbType.Bit, DipReg.strIsReAdmission);

                //oDm.Add("@Ece", SqlDbType.NVarChar, BtechReg.strEce);
                //oDm.Add("@Ee", SqlDbType.NVarChar, BtechReg.strEe);
                //oDm.Add("@Cse", SqlDbType.NVarChar, BtechReg.strCse);
                //oDm.Add("@Aeie", SqlDbType.NVarChar, BtechReg.strAeie);
                //oDm.Add("@Me", SqlDbType.NVarChar, BtechReg.strMe);
                //oDm.Add("@Ce", SqlDbType.NVarChar, BtechReg.strCe);

                oDm.Add("@StreamApplied", SqlDbType.NVarChar, DipReg.strStreamApplied);

                oDm.Add("@Phy", SqlDbType.NVarChar, DipReg.strPhy);
                oDm.Add("@LSc", SqlDbType.NVarChar, DipReg.strLSc);
                oDm.Add("@Math", SqlDbType.NVarChar, DipReg.strMath);
                oDm.Add("@Engg", SqlDbType.NVarChar, DipReg.strEngg);


                oDm.Add("@NameOfApplicant", SqlDbType.NVarChar, DipReg.strNameOfApplicant);
                oDm.Add("@Dob", SqlDbType.NVarChar, DipReg.strDob);
                oDm.Add("@FatherName", SqlDbType.NVarChar, DipReg.strFatherName);
                oDm.Add("@FatherOccupation", SqlDbType.NVarChar, DipReg.strFatherOccupation);
                oDm.Add("@MotherName", SqlDbType.NVarChar, DipReg.strMotherName);
                oDm.Add("@MotherOccupation", SqlDbType.NVarChar, DipReg.strMotherOccupation);
                oDm.Add("@GuardiansName", SqlDbType.NVarChar, DipReg.strGuardiansName);



                oDm.Add("@SchoolId", SqlDbType.Int, DipReg.SchoolID);
                oDm.Add("@PAddress", SqlDbType.NVarChar, DipReg.strPAddress);
                oDm.Add("@PState", SqlDbType.Int, DipReg.strPState);
                oDm.Add("@PDistrict", SqlDbType.Int, DipReg.strPDistrict);
                oDm.Add("@PCity", SqlDbType.Int, DipReg.strPCity);
                if (DipReg.strPPin.Length > 0)
                    oDm.Add("@PPin", SqlDbType.Int, DipReg.strPPin);
                else
                    oDm.Add("@PPin", SqlDbType.Int, DBNull.Value);
                oDm.Add("@CAddress", SqlDbType.NVarChar, DipReg.strCAddress);
                oDm.Add("@CState", SqlDbType.Int, DipReg.strCState);
                oDm.Add("@CDistrict", SqlDbType.Int, DipReg.strCDistrict);
                oDm.Add("@CCity", SqlDbType.Int, DipReg.strCCity);
                if (DipReg.strCPin.Length > 0)
                    oDm.Add("@CPin", SqlDbType.Int, DipReg.strCPin);
                else
                    oDm.Add("@CPin", SqlDbType.Int, DBNull.Value);
                
                //Added By Biswajit
                oDm.Add("@AdharNo", SqlDbType.NVarChar, DipReg.strAdhar);
                oDm.Add("@BankHolderName", SqlDbType.NVarChar, DipReg.strBankHName);
                oDm.Add("@BankAccNo", SqlDbType.NVarChar, DipReg.strBankAcc);
                oDm.Add("@BankIFSC", SqlDbType.NVarChar, DipReg.strBankIFSC);

                //End

                oDm.Add("@PResidential", SqlDbType.NVarChar, DipReg.strPResidential);
                oDm.Add("@SResidential", SqlDbType.NVarChar, DipReg.strSResidential);
                oDm.Add("@PMobile", SqlDbType.NVarChar, DipReg.strPMobile);
                oDm.Add("@SMobile", SqlDbType.NVarChar, DipReg.strSMobile);
                oDm.Add("@email", SqlDbType.NVarChar, DipReg.strEmail);
                oDm.Add("@HostalFacitity", SqlDbType.NVarChar, DipReg.strHostalFacitity);
                oDm.Add("@Gender", SqlDbType.NVarChar, DipReg.strGender);
                oDm.Add("@Marital", SqlDbType.NVarChar, DipReg.strMarital);
                oDm.Add("@MotherTong", SqlDbType.NVarChar, DipReg.strMotherTong);
                oDm.Add("@Nationality", SqlDbType.NVarChar, DipReg.strNationality);
                oDm.Add("@Realigion", SqlDbType.NVarChar, DipReg.strRealigion);
                oDm.Add("@Bengali", SqlDbType.NVarChar, DipReg.strBengali);
                oDm.Add("@Hindi", SqlDbType.NVarChar, DipReg.strHindi);
                oDm.Add("@English", SqlDbType.NVarChar, DipReg.strEnglish);
                oDm.Add("@Other", SqlDbType.NVarChar, DipReg.strOther);
                oDm.Add("@Cast", SqlDbType.NVarChar, DipReg.strCast);
                oDm.Add("@MonthlyIncome", SqlDbType.NVarChar, DipReg.strMonthlyIncome);
                oDm.Add("@RefferenceName1", SqlDbType.NVarChar, DipReg.strRefferenceName1);
                oDm.Add("@RefferenceAddress1", SqlDbType.NVarChar, DipReg.strRefferenceAddress1);
                oDm.Add("@RefferenceContactNumber1", SqlDbType.NVarChar, DipReg.strRefferenceContactNumber1);
                oDm.Add("@RefferenceName2", SqlDbType.NVarChar, DipReg.strRefferenceName2);
                oDm.Add("@RefferenceAddress2", SqlDbType.NVarChar, DipReg.strRefferenceAddress2);
                oDm.Add("@RefferenceContactNumber2", SqlDbType.NVarChar, DipReg.strRefferenceContactNumber2);


                oDm.Add("@XSubject", SqlDbType.NVarChar, DipReg.strXSubject);
                oDm.Add("@XBoard", SqlDbType.NVarChar, DipReg.strXBoard);
                oDm.Add("@XCollege", SqlDbType.NVarChar, DipReg.strXCollege);
                oDm.Add("@XYearOfPassing", SqlDbType.NVarChar, DipReg.strXYearOfPassing);
                oDm.Add("@XMarks", SqlDbType.NVarChar, DipReg.strXMarks);


                oDm.Add("@ITISubject", SqlDbType.NVarChar, DipReg.strITISubject);
                oDm.Add("@ITIBoard", SqlDbType.NVarChar, DipReg.strITIBoard);
                oDm.Add("@ITICollege", SqlDbType.NVarChar, DipReg.strITICollege);
                oDm.Add("@ITIYearOfPassing", SqlDbType.NVarChar, DipReg.strITIYearOfPassing);
                oDm.Add("@ITIMarks", SqlDbType.NVarChar, DipReg.strITIMarks);

                oDm.Add("@VSubject", SqlDbType.NVarChar, DipReg.strVSubject);
                oDm.Add("@VBoard", SqlDbType.NVarChar, DipReg.strVBoard);
                oDm.Add("@VCollege", SqlDbType.NVarChar, DipReg.strVCollege);
                oDm.Add("@VYearOfPassing", SqlDbType.NVarChar, DipReg.strVYearOfPassing);
                oDm.Add("@VMarks", SqlDbType.NVarChar, DipReg.strVMarks);

                //oDm.Add("@GSubject", SqlDbType.NVarChar, BtechReg.strGSubject);
                //oDm.Add("@GBoard", SqlDbType.NVarChar, BtechReg.strGBoard);
                //oDm.Add("@GCollege", SqlDbType.NVarChar, BtechReg.strGCollege);
                //oDm.Add("@GYearOfPassing", SqlDbType.NVarChar, BtechReg.strGYearOfPassing);
                //oDm.Add("@GMarks", SqlDbType.NVarChar, BtechReg.strGMarks);


                oDm.Add("@DocList1", SqlDbType.NVarChar, DipReg.strDocList1);
                oDm.Add("@DocList2", SqlDbType.NVarChar, DipReg.strDocList2);
                oDm.Add("@DocList3", SqlDbType.NVarChar, DipReg.strDocList3);
                oDm.Add("@DocList4", SqlDbType.NVarChar, DipReg.strDocList4);
                oDm.Add("@DocList5", SqlDbType.NVarChar, DipReg.strDocList5);
                oDm.Add("@DocList6", SqlDbType.NVarChar, DipReg.strDocList6);
                oDm.Add("@DocList7", SqlDbType.NVarChar, DipReg.strDocList7);
                //oDm.Add("@DocList8", SqlDbType.NVarChar, BtechReg.strDocList8);
                //oDm.Add("@DocList9", SqlDbType.NVarChar, BtechReg.strDocList9);
                oDm.Add("@BloodGroup", SqlDbType.VarChar, 8, DipReg.BloodGroup);
                oDm.Add("@Photo", SqlDbType.VarChar, 50, DipReg.strPhoto);
                oDm.Add("@SubmittedBy", SqlDbType.VarChar, 100, DipReg.SubmittedBy);
                oDm.CommandType = CommandType.StoredProcedure;

                int RowsAffected = oDm.ExecuteNonQuery("usp_diploma_registration");
                DipReg.studentID = (int)oDm["@intStudentID"].Value;
                return RowsAffected;
            }
        }
        //End of Save Function


        //Starting of GetStudentDetails
        public static DataSet GetStudentDetails(Entity.Student.DiplomaRegistration DipReg)
        {
            using (DataManager oDm = new DataManager())
            {
                DataSet ds = new DataSet();
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, DipReg.intMode);
                oDm.Add("@int_company_id", SqlDbType.Int, DipReg.intCompanyId);
                oDm.Add("@intStudentID", SqlDbType.Int, DipReg.studentID);
                oDm.Add("@CourseId", SqlDbType.Int, DipReg.CourseId);
                return oDm.GetDataSet("usp_diploma_registration", ref ds, "table");
            }
        }
        //End of GetStudentDetails

    }
}
