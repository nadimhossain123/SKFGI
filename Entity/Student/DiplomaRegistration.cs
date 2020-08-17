using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Student
{
    public class DiplomaRegistration
    {
        public DiplomaRegistration() { }

        public int intMode { get; set; }
        public int intCompanyId { get; set; }
        public string login_id { get; set; }
        public int studentID { get; set; }
        public string stream_type { get; set; }

        public int CourseId { get; set; }
        public string strapplicationNumber { get; set; }
        public string strBatch { get; set; }
        public string strEnrollmentNo { get; set; }
        public string strRank { get; set; }
        public string strRankid { get; set; }

        
        public string strPhy { get; set; }
        public string strLSc { get; set; }
        public string strMath { get; set; }
        public string strEngg { get; set; }

        public string strStreamApplied { get; set; }


        public string strNameOfApplicant { get; set; }
        public string strDob { get; set; }
        public string strFatherName { get; set; }
        public string strFatherOccupation { get; set; }
        public string strMotherName { get; set; }
        public string strMotherOccupation { get; set; }
        public string strGuardiansName { get; set; }
        //Added by Biswajit
        public string strAdhar { get; set; }
        public string strBankHName { get; set; }
        public string strBankAcc { get; set; }
        public string strBankIFSC { get; set; }
        //
        public int SchoolID { get; set; }
        public string strPAddress { get; set; }
        public string strCAddress { get; set; }
        public int strPState { get; set; }
        public int strCState { get; set; }
        public int strPDistrict { get; set; }
        public int strCDistrict { get; set; }
        public int strPCity { get; set; }
        public int strCCity { get; set; }
        public string strPPin { get; set; }
        public string strCPin { get; set; }


        public string strPResidential { get; set; }
        public string strSResidential { get; set; }
        public string strPMobile { get; set; }
        public string strSMobile { get; set; }
        public string strEmail { get; set; }
        public string strHostalFacitity { get; set; }
        public string strGender { get; set; }
        public string strMarital { get; set; }
        public string strMotherTong { get; set; }
        public string strNationality { get; set; }
        public string strRealigion { get; set; }
        public string strBengali { get; set; }
        public string strHindi { get; set; }
        public string strEnglish { get; set; }
        public string strOther { get; set; }
        public string strCast { get; set; }


        public string strMonthlyIncome { get; set; }
        public string strRefferenceName1 { get; set; }
        public string strRefferenceAddress1 { get; set; }
        public string strRefferenceContactNumber1 { get; set; }
        public string strRefferenceName2 { get; set; }
        public string strRefferenceAddress2 { get; set; }
        public string strRefferenceContactNumber2 { get; set; }


        public string strXSubject { get; set; }
        public string strXBoard { get; set; }
        public string strXCollege { get; set; }
        public string strXYearOfPassing { get; set; }
        public string strXMarks { get; set; }

        public string strITISubject { get; set; }
        public string strITIBoard { get; set; }
        public string strITICollege { get; set; }
        public string strITIYearOfPassing { get; set; }
        public string strITIMarks { get; set; }

        public string strVSubject { get; set; }
        public string strVBoard { get; set; }
        public string strVCollege { get; set; }
        public string strVYearOfPassing { get; set; }
        public string strVMarks { get; set; }

        //public string strGSubject { get; set; }
        //public string strGBoard { get; set; }
        //public string strGCollege { get; set; }
        //public string strGYearOfPassing { get; set; }
        //public string strGMarks { get; set; }


        public string strDocList1 { get; set; }
        public string strDocList2 { get; set; }
        public string strDocList3 { get; set; }
        public string strDocList4 { get; set; }
        public string strDocList5 { get; set; }
        public string strDocList6 { get; set; }
        public string strDocList7 { get; set; }
        //public string strDocList8 { get; set; }
        //public string strDocList9 { get; set; }
        public string BloodGroup { get; set; }

        public bool strIsLateral { get; set; }
        public string strRegistrationNo { get; set; }
        public string strUniversityRollNo { get; set; }
        public string strMigrationInfo { get; set; }
        public bool strIsHostelFacility { get; set; }
        public bool strTFW { get; set; }
        public string strPhoto { get; set; }
        public bool strIsReAdmission { get; set; }
        public string SubmittedBy { get; set; }
    }
}
