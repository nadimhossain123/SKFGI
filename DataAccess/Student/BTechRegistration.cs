using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.student
{
    public class BTechRegistration
    {
        //public static DataTable GetAllBatch(Entity.Student.BTechRegistration BtechReg)
        //{
        //    using (DataManager oDm = new DataManager())
        //    {
        //        oDm.CommandType = CommandType.StoredProcedure;
        //        oDm.Add("@int_mode", SqlDbType.Int, BtechReg.intMode);
        //        oDm.Add("@int_company_id", SqlDbType.Int, BtechReg.intCompanyId);
        //        return oDm.ExecuteDataTable("usp_btech_registration");
        //    }
        //}

        public static DataTable GetAllCommonSP(Entity.Student.BTechRegistration BtechReg)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, BtechReg.intMode); //1-for selecting all batch 2-for selectting all course and 3- for selecting all stream by courseid 
                oDm.Add("@CourseId", SqlDbType.Int, BtechReg.CourseId);
                return oDm.ExecuteDataTable("usp_BTechRegistration_CommonSP");
            }
        }
        public static int Save(Entity.Student.BTechRegistration BtechReg)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@int_mode", SqlDbType.Int, BtechReg.intMode);
                oDm.Add("@int_company_id", SqlDbType.NVarChar, BtechReg.intCompanyId);
                oDm.Add("@login_id", SqlDbType.NVarChar, BtechReg.login_id);
                oDm.Add("@intStudentID", SqlDbType.Int,ParameterDirection.InputOutput, BtechReg.studentID);
                oDm.Add("@CourseId", SqlDbType.Int, BtechReg.CourseId);


                oDm.Add("@applicationNumber", SqlDbType.NVarChar, BtechReg.strapplicationNumber);
                oDm.Add("@Batch", SqlDbType.NVarChar, BtechReg.strBatch);
                oDm.Add("@EnrollmentNo", SqlDbType.NVarChar, BtechReg.strEnrollmentNo);
                oDm.Add("@Rank", SqlDbType.NVarChar, BtechReg.strRank);
                oDm.Add("@rankid", SqlDbType.NVarChar, BtechReg.strRankid);

                oDm.Add("@IsLateral", SqlDbType.Bit, BtechReg.strIsLateral);
                oDm.Add("@RegistrationNo", SqlDbType.VarChar, 50, BtechReg.strRegistrationNo);
                oDm.Add("@UniversityRollNo", SqlDbType.VarChar, 50, BtechReg.strUniversityRollNo);

                oDm.Add("@MigrationInfo", SqlDbType.VarChar,50, BtechReg.strMigrationInfo);
                oDm.Add("@IsHostelFacility", SqlDbType.Bit, BtechReg.strIsHostelFacility);
                oDm.Add("@TFW", SqlDbType.Bit, BtechReg.strTFW);
                oDm.Add("@IsReAdmission", SqlDbType.Bit, BtechReg.strIsReAdmission);
                
                //oDm.Add("@Ece", SqlDbType.NVarChar, BtechReg.strEce);
                //oDm.Add("@Ee", SqlDbType.NVarChar, BtechReg.strEe);
                //oDm.Add("@Cse", SqlDbType.NVarChar, BtechReg.strCse);
                //oDm.Add("@Aeie", SqlDbType.NVarChar, BtechReg.strAeie);
                //oDm.Add("@Me", SqlDbType.NVarChar, BtechReg.strMe);
                //oDm.Add("@Ce", SqlDbType.NVarChar, BtechReg.strCe);

                oDm.Add("@StreamApplied", SqlDbType.NVarChar, BtechReg.strStreamApplied);

                oDm.Add("@Phy", SqlDbType.NVarChar, BtechReg.strPhy);
                oDm.Add("@Chem", SqlDbType.NVarChar, BtechReg.strChem);
                oDm.Add("@Math", SqlDbType.NVarChar, BtechReg.strMath);
                oDm.Add("@Engg", SqlDbType.NVarChar, BtechReg.strEngg);
                oDm.Add("@Bios", SqlDbType.NVarChar, BtechReg.strBios);


                oDm.Add("@NameOfApplicant", SqlDbType.NVarChar, BtechReg.strNameOfApplicant);
                oDm.Add("@Dob", SqlDbType.NVarChar, BtechReg.strDob);
                oDm.Add("@FatherName", SqlDbType.NVarChar, BtechReg.strFatherName);
                oDm.Add("@FatherOccupation", SqlDbType.NVarChar, BtechReg.strFatherOccupation);
                oDm.Add("@MotherName", SqlDbType.NVarChar, BtechReg.strMotherName);
                oDm.Add("@MotherOccupation", SqlDbType.NVarChar, BtechReg.strMotherOccupation);
                oDm.Add("@GuardiansName", SqlDbType.NVarChar, BtechReg.strGuardiansName);

                oDm.Add("@SchoolId", SqlDbType.Int, BtechReg.SchoolID);
                oDm.Add("@PAddress", SqlDbType.NVarChar, BtechReg.strPAddress);
                oDm.Add("@PState", SqlDbType.Int, BtechReg.strPState);
                oDm.Add("@PDistrict", SqlDbType.Int, BtechReg.strPDistrict);
                oDm.Add("@PCity", SqlDbType.Int, BtechReg.strPCity);
                if(BtechReg.strPPin.Length>0)
                oDm.Add("@PPin", SqlDbType.Int, BtechReg.strPPin);
                else
                    oDm.Add("@PPin", SqlDbType.Int, DBNull.Value);
                oDm.Add("@CAddress", SqlDbType.NVarChar, BtechReg.strCAddress);
                oDm.Add("@CState", SqlDbType.Int, BtechReg.strCState);
                oDm.Add("@CDistrict", SqlDbType.Int, BtechReg.strCDistrict);
                oDm.Add("@CCity", SqlDbType.Int, BtechReg.strCCity);
                if(BtechReg.strCPin.Length>0)
                oDm.Add("@CPin", SqlDbType.Int, BtechReg.strCPin);
                else
                    oDm.Add("@CPin", SqlDbType.Int, DBNull.Value);

                //Added By Biswajit
                oDm.Add("@AdharNo", SqlDbType.NVarChar, BtechReg.strAdhar);
                oDm.Add("@BankHolderName", SqlDbType.NVarChar, BtechReg.strBankHName);
                oDm.Add("@BankAccNo", SqlDbType.NVarChar, BtechReg.strBankAcc);
                oDm.Add("@BankIFSC", SqlDbType.NVarChar, BtechReg.strBankIFSC);

                //End

                oDm.Add("@PResidential", SqlDbType.NVarChar, BtechReg.strPResidential);
                oDm.Add("@SResidential", SqlDbType.NVarChar, BtechReg.strSResidential);
                oDm.Add("@PMobile", SqlDbType.NVarChar, BtechReg.strPMobile);
                oDm.Add("@SMobile", SqlDbType.NVarChar, BtechReg.strSMobile);
                oDm.Add("@email", SqlDbType.NVarChar, BtechReg.strEmail);
                oDm.Add("@HostalFacitity", SqlDbType.NVarChar, BtechReg.strHostalFacitity);
                oDm.Add("@Gender", SqlDbType.NVarChar, BtechReg.strGender);
                oDm.Add("@Marital", SqlDbType.NVarChar, BtechReg.strMarital);
                oDm.Add("@MotherTong", SqlDbType.NVarChar, BtechReg.strMotherTong);
                oDm.Add("@Nationality", SqlDbType.NVarChar, BtechReg.strNationality);
                oDm.Add("@Realigion", SqlDbType.NVarChar, BtechReg.strRealigion);
                oDm.Add("@Bengali", SqlDbType.NVarChar, BtechReg.strBengali);
                oDm.Add("@Hindi", SqlDbType.NVarChar, BtechReg.strHindi);
                oDm.Add("@English", SqlDbType.NVarChar, BtechReg.strEnglish);
                oDm.Add("@Other", SqlDbType.NVarChar, BtechReg.strOther);
                oDm.Add("@Cast", SqlDbType.NVarChar, BtechReg.strCast);
                oDm.Add("@MonthlyIncome", SqlDbType.NVarChar, BtechReg.strMonthlyIncome);
                oDm.Add("@RefferenceName1", SqlDbType.NVarChar, BtechReg.strRefferenceName1);
                oDm.Add("@RefferenceAddress1", SqlDbType.NVarChar, BtechReg.strRefferenceAddress1);
                oDm.Add("@RefferenceContactNumber1", SqlDbType.NVarChar, BtechReg.strRefferenceContactNumber1);
                oDm.Add("@RefferenceName2", SqlDbType.NVarChar, BtechReg.strRefferenceName2);
                oDm.Add("@RefferenceAddress2", SqlDbType.NVarChar, BtechReg.strRefferenceAddress2);
                oDm.Add("@RefferenceContactNumber2", SqlDbType.NVarChar, BtechReg.strRefferenceContactNumber2);

                oDm.Add("@XSubject", SqlDbType.NVarChar, BtechReg.strXSubject);
                oDm.Add("@XBoard", SqlDbType.NVarChar, BtechReg.strXBoard);
                oDm.Add("@XCollege", SqlDbType.NVarChar, BtechReg.strXCollege);
                oDm.Add("@XYearOfPassing", SqlDbType.NVarChar, BtechReg.strXYearOfPassing);
                oDm.Add("@XMarks", SqlDbType.NVarChar, BtechReg.strXMarks);

                oDm.Add("@XiiSubject", SqlDbType.NVarChar, BtechReg.strXiiSubject);
                oDm.Add("@XiiBoard", SqlDbType.NVarChar, BtechReg.strXiiBoard);
                oDm.Add("@XiiCollege", SqlDbType.NVarChar, BtechReg.strXiiCollege);
                oDm.Add("@XiiYearOfPassing", SqlDbType.NVarChar, BtechReg.strXiiYearOfPassing);
                oDm.Add("@XiiMarks", SqlDbType.NVarChar, BtechReg.strXiiMarks);

                oDm.Add("@DSubject", SqlDbType.NVarChar, BtechReg.strDSubject);
                oDm.Add("@DBoard", SqlDbType.NVarChar, BtechReg.strDBoard);
                oDm.Add("@DCollege", SqlDbType.NVarChar, BtechReg.strDCollege);
                oDm.Add("@DYearOfPassing", SqlDbType.NVarChar, BtechReg.strDYearOfPassing);
                oDm.Add("@DMarks", SqlDbType.NVarChar, BtechReg.strDMarks);

                oDm.Add("@GSubject", SqlDbType.NVarChar, BtechReg.strGSubject);
                oDm.Add("@GBoard", SqlDbType.NVarChar, BtechReg.strGBoard);
                oDm.Add("@GCollege", SqlDbType.NVarChar, BtechReg.strGCollege);
                oDm.Add("@GYearOfPassing", SqlDbType.NVarChar, BtechReg.strGYearOfPassing);
                oDm.Add("@GMarks", SqlDbType.NVarChar, BtechReg.strGMarks);

                oDm.Add("@DocList1", SqlDbType.NVarChar, BtechReg.strDocList1);
                oDm.Add("@DocList2", SqlDbType.NVarChar, BtechReg.strDocList2);
                oDm.Add("@DocList3", SqlDbType.NVarChar, BtechReg.strDocList3);
                oDm.Add("@DocList4", SqlDbType.NVarChar, BtechReg.strDocList4);
                oDm.Add("@DocList5", SqlDbType.NVarChar, BtechReg.strDocList5);
                oDm.Add("@DocList6", SqlDbType.NVarChar, BtechReg.strDocList6);
                oDm.Add("@DocList7", SqlDbType.NVarChar, BtechReg.strDocList7);
                oDm.Add("@DocList8", SqlDbType.NVarChar, BtechReg.strDocList8);
                oDm.Add("@DocList9", SqlDbType.NVarChar, BtechReg.strDocList9);
                oDm.Add("@BloodGroup", SqlDbType.VarChar, 8, BtechReg.BloodGroup);
                oDm.Add("@Photo", SqlDbType.VarChar, 50, BtechReg.strPhoto);
                oDm.Add("@SubmittedBy", SqlDbType.VarChar, 100, BtechReg.SubmittedBy);
                
                oDm.CommandType = CommandType.StoredProcedure;

                int RowsAffected = oDm.ExecuteNonQuery("usp_btech_registration");
                BtechReg.studentID = (int)oDm["@intStudentID"].Value;
                return RowsAffected;
            }
        }

        public static DataSet GetStudentDetails(Entity.Student.BTechRegistration BtechReg)
        {
            using (DataManager oDm = new DataManager())
            {
                DataSet ds = new DataSet();
                oDm.CommandType = CommandType.StoredProcedure;
                oDm.Add("@int_mode", SqlDbType.Int, BtechReg.intMode);
                oDm.Add("@int_company_id", SqlDbType.Int, BtechReg.intCompanyId);
                oDm.Add("@intStudentID", SqlDbType.Int, BtechReg.studentID);
                oDm.Add("@CourseId", SqlDbType.Int, BtechReg.CourseId);
                return oDm.GetDataSet("usp_btech_registration", ref ds, "table");
            }
        }
    }
}
