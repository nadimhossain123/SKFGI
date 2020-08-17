using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CollegeERP.Student
{
    public partial class MBARegistrationPrint : System.Web.UI.Page
    {

        public string strStudentID
        {
            get { return ViewState["strStudentID"].ToString(); }
            set { ViewState["strStudentID"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim().Length > 0)
                {
                    strStudentID = Request.QueryString["id"].ToString();
                    loadStudentDetails(strStudentID);
                }
            }

        }

        private void loadStudentDetails(string studentID)
        {
            BusinessLayer.Student.MBARegistration MBAReg = new BusinessLayer.Student.MBARegistration();
            Entity.Student.MBARegistration eMBAReg = new Entity.Student.MBARegistration();
            eMBAReg.intMode = 5;
            eMBAReg.studentID = int.Parse(studentID);
            eMBAReg.CourseId = 1;
            DataSet ds = new DataSet();
            ds = MBAReg.GetStudentDetails(eMBAReg);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ltrBatchname.Text = "<h2>APPLICATION FORM FOR ADMISSION IN " + ds.Tables[0].Rows[0]["batch_name"].ToString() + "</h2>";
                    lblapplicationNumber.Text = ds.Tables[0].Rows[0]["appliation_no"].ToString();
                    lblEnrollmentNo.Text = ds.Tables[0].Rows[0]["enrollmentn_no"].ToString();
                    selectRank(ds.Tables[0].Rows[0]["rank"].ToString());
                    lblRankid.Text = ds.Tables[0].Rows[0]["rankid"].ToString();

                    lblNameOfApplicant.Text = ds.Tables[0].Rows[0]["name"].ToString();
                    lblDob.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["dob"].ToString()).ToString("dd/MM/yyy");

                    // New Fields
                    lblAdhar.Text = ds.Tables[0].Rows[0]["adhar_no"].ToString();
                    lblAccName.Text = ds.Tables[0].Rows[0]["bankholder_name"].ToString();
                    lblAccNo.Text = ds.Tables[0].Rows[0]["bankacc_no"].ToString();
                    lblIFSC.Text = ds.Tables[0].Rows[0]["bankIFSC_code"].ToString();
                    //

                    lblFatherName.Text = ds.Tables[0].Rows[0]["fathers_name"].ToString();
                    lblFatherOccupation.Text = ds.Tables[0].Rows[0]["f_Occupation"].ToString();
                    lblMotherName.Text = ds.Tables[0].Rows[0]["mothers_name"].ToString();
                    lblMotherOccupation.Text = ds.Tables[0].Rows[0]["m_Occupation"].ToString();
                    lblGuardiansName.Text = ds.Tables[0].Rows[0]["guardian_name"].ToString();

                    lblschool.Text = ds.Tables[0].Rows[0]["School"].ToString();
                    lblPAddress.Text = ds.Tables[0].Rows[0]["p_address"].ToString();
                    lblCAddress.Text = ds.Tables[0].Rows[0]["c_address"].ToString();

                    lblppin.Text = ds.Tables[0].Rows[0]["PPin"].ToString();
                    lblpcity.Text = ds.Tables[0].Rows[0]["PCity"].ToString();
                    lblpdistrict.Text = ds.Tables[0].Rows[0]["PDistrict"].ToString();
                    lblpstate.Text = ds.Tables[0].Rows[0]["PState"].ToString();

                    lblcpin.Text = ds.Tables[0].Rows[0]["CPin"].ToString();
                    lblccity.Text = ds.Tables[0].Rows[0]["CCity"].ToString();
                    lblcdistrict.Text = ds.Tables[0].Rows[0]["CDistrict"].ToString();
                    lblcstate.Text = ds.Tables[0].Rows[0]["CState"].ToString();

                    ImgPhoto.ImageUrl = ds.Tables[0].Rows[0]["Photo"].ToString();

                    lblPResidential.Text = ds.Tables[0].Rows[0]["p_residential"].ToString();
                    lblSResidential.Text = ds.Tables[0].Rows[0]["s_residential"].ToString();
                    lblPMobile.Text = ds.Tables[0].Rows[0]["p_mobile"].ToString();
                    lblSMobile.Text = ds.Tables[0].Rows[0]["s_mobile"].ToString();
                    lblEmail.Text = ds.Tables[0].Rows[0]["email"].ToString();
                    selectHostel(ds.Tables[0].Rows[0]["IsHostelFacility"].ToString());
                    selectGender(ds.Tables[0].Rows[0]["gender"].ToString());
                    selectMaritalStatus(ds.Tables[0].Rows[0]["marital_status"].ToString());
                    lblMotherTong.Text = ds.Tables[0].Rows[0]["mother_tongue"].ToString();
                    lblNationality.Text = ds.Tables[0].Rows[0]["nationality"].ToString();
                    lblRealigion.Text = ds.Tables[0].Rows[0]["realigion"].ToString();
                    selectBLanguage(ds.Tables[0].Rows[0]["language_bengali"].ToString());
                    selectHLanguage(ds.Tables[0].Rows[0]["language_hindi"].ToString());
                    selectELanguage(ds.Tables[0].Rows[0]["language_english"].ToString());
                    selectOLanguage(ds.Tables[0].Rows[0]["language_other"].ToString());
                    selectCast(ds.Tables[0].Rows[0]["caste_id"].ToString());
                    lblMonthlyIncome.Text = ds.Tables[0].Rows[0]["monthly_income"].ToString();
                    lblRefferenceName1.Text = ds.Tables[0].Rows[0]["reference_name1"].ToString();
                    lblRefferenceAddress1.Text = ds.Tables[0].Rows[0]["reference_address1"].ToString();
                    lblRefferenceContactNumber1.Text = ds.Tables[0].Rows[0]["reference_contact_number1"].ToString();
                    lblRefferenceName2.Text = ds.Tables[0].Rows[0]["reference_name2"].ToString();
                    lblRefferenceAddress2.Text = ds.Tables[0].Rows[0]["reference_address2"].ToString();
                    lblRefferenceContactNumber2.Text = ds.Tables[0].Rows[0]["reference_contact_number2"].ToString();
                    lblBloodGroup.Text = ds.Tables[0].Rows[0]["BloodGroup"].ToString();
                    lblSubmittedBy.Text = ds.Tables[0].Rows[0]["SubmittedBy"].ToString();

                    selectEducationBackground(ds.Tables[1]);
                    selectExperienceDetails(ds.Tables[2]);
                    selectQualifyingExam(ds.Tables[3]);
                    selectDocument(ds.Tables[4]);
                    selectStreamapplied(ds.Tables[5]);
                }

            }
        }

        private void selectQualifyingExam(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (dataTable.Rows[i]["serial_id"].ToString() == "1")
                    {
                        lblTest1.Text = dataTable.Rows[i]["Test"].ToString();
                        lblDateOfExam1.Text = dataTable.Rows[i]["examination_date"].ToString();
                        lblMarksObtain1.Text = dataTable.Rows[i]["marks_Obtained"].ToString();
                    }
                    else if (dataTable.Rows[i]["serial_id"].ToString() == "2")
                    {
                        lblTest2.Text = dataTable.Rows[i]["Test"].ToString();
                        lblDateOfExam2.Text = dataTable.Rows[i]["examination_date"].ToString();
                        lblMarksObtain2.Text = dataTable.Rows[i]["marks_Obtained"].ToString();
                    }

                    else
                    {
                        lblTest3.Text = dataTable.Rows[i]["Test"].ToString();
                        lblDateOfExam3.Text = dataTable.Rows[i]["examination_date"].ToString();
                        lblMarksObtain3.Text = dataTable.Rows[i]["marks_Obtained"].ToString();
                       
                    }
                  
                }
            }
          
        }


        private void selectExperienceDetails(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (dataTable.Rows[i]["serial_id"].ToString() == "1")
                    {
                        lbl1NameOfCompany.Text = dataTable.Rows[i]["company_name"].ToString();
                        lbl1JobProfile.Text = dataTable.Rows[i]["responsibility"].ToString();
                        lbl1DateOfJoining.Text = dataTable.Rows[i]["joning_date"].ToString();
                        lbl1DateOfLeave.Text = dataTable.Rows[i]["leaving_date"].ToString();
                        lbl1Remark.Text = dataTable.Rows[i]["remarks"].ToString();
                    }
                    else if (dataTable.Rows[i]["serial_id"].ToString() == "2")
                    {
                        lbl2NameOfCompany.Text = dataTable.Rows[i]["company_name"].ToString();
                        lbl2JobProfile.Text = dataTable.Rows[i]["responsibility"].ToString();
                        lbl2DateOfJoining.Text = dataTable.Rows[i]["joning_date"].ToString();
                        lbl2DateOfLeave.Text = dataTable.Rows[i]["leaving_date"].ToString();
                        lbl2Remark.Text = dataTable.Rows[i]["remarks"].ToString();

                    }
                    else
                    {
                        lbl3NameOfCompany.Text = dataTable.Rows[i]["company_name"].ToString();
                        lbl3JobProfile.Text = dataTable.Rows[i]["responsibility"].ToString();
                        lbl3DateOfJoining.Text = dataTable.Rows[i]["joning_date"].ToString();
                        lbl3DateOfLeave.Text = dataTable.Rows[i]["leaving_date"].ToString();
                        lbl3Remark.Text = dataTable.Rows[i]["remarks"].ToString();
                    
                    }

                }

            }

        }

        private void selectDocument(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (dataTable.Rows[i]["document_id"].ToString() == "1")
                        chkListDoc1.Checked = true;
                    else if (dataTable.Rows[i]["document_id"].ToString() == "2")
                        chkListDoc2.Checked = true;
                    else if (dataTable.Rows[i]["document_id"].ToString() == "3")
                        chkListDoc3.Checked = true;
                    else if (dataTable.Rows[i]["document_id"].ToString() == "4")
                        chkListDoc4.Checked = true;
                    else if (dataTable.Rows[i]["document_id"].ToString() == "5")
                        chkListDoc5.Checked = true;
                    else if (dataTable.Rows[i]["document_id"].ToString() == "6")
                        chkListDoc6.Checked = true;
                    else if (dataTable.Rows[i]["document_id"].ToString() == "7")
                        chkListDoc7.Checked = true;
                    else if (dataTable.Rows[i]["document_id"].ToString() == "8")
                        chkListDoc8.Checked = true;
                    else if (dataTable.Rows[i]["document_id"].ToString() == "9")
                        chkListDoc9.Checked = true;

                }
            }
        }

        private void selectEducationBackground(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (dataTable.Rows[i]["exam_id"].ToString() == "1")
                    {
                        lblXSubject.Text = dataTable.Rows[i]["major_subject"].ToString();
                        lblXBoard.Text = dataTable.Rows[i]["board"].ToString();
                        lblXCollege.Text = dataTable.Rows[i]["college"].ToString();
                        lblXYearOfPassing.Text = dataTable.Rows[i]["year_passing"].ToString();
                        lblXMarks.Text = dataTable.Rows[i]["division_marks"].ToString();
                    }
                    else if (dataTable.Rows[i]["exam_id"].ToString() == "2")
                    {
                        lblXiiSubject.Text = dataTable.Rows[i]["major_subject"].ToString();
                        lblXiiBoard.Text = dataTable.Rows[i]["board"].ToString();
                        lblXiiCollege.Text = dataTable.Rows[i]["college"].ToString();
                        lblXiiYearOfPassing.Text = dataTable.Rows[i]["year_passing"].ToString();
                        lblXiiMarks.Text = dataTable.Rows[i]["division_marks"].ToString();
                    }
                    else if (dataTable.Rows[i]["exam_id"].ToString() == "3")
                    {
                        lblDSubject.Text = dataTable.Rows[i]["major_subject"].ToString();
                        lblDBoard.Text = dataTable.Rows[i]["board"].ToString();
                        lblDCollege.Text = dataTable.Rows[i]["college"].ToString();
                        lblDYearOfPassing.Text = dataTable.Rows[i]["year_passing"].ToString();
                        lblDMarks.Text = dataTable.Rows[i]["division_marks"].ToString();
                    }
                    else if (dataTable.Rows[i]["exam_id"].ToString() == "4")
                    {
                        lblGSubject.Text = dataTable.Rows[i]["major_subject"].ToString();
                        lblGBoard.Text = dataTable.Rows[i]["board"].ToString();
                        lblGCollege.Text = dataTable.Rows[i]["college"].ToString();
                        lblGYearOfPassing.Text = dataTable.Rows[i]["year_passing"].ToString();
                        lblGMarks.Text = dataTable.Rows[i]["division_marks"].ToString();
                    }
                }
            }
        }

        

        private void selectStreamapplied(DataTable dataTable)
        {
            BusinessLayer.Student.BTechRegistration BtechReg = new BusinessLayer.Student.BTechRegistration();
            Entity.Student.BTechRegistration eBtechReg = new Entity.Student.BTechRegistration();
            eBtechReg.intMode = 3;
            eBtechReg.CourseId = 1;
            DataTable dt = new DataTable();
            dt = BtechReg.GetAllCommonSP(eBtechReg);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                string[] appliedStream = dataTable.Rows[0]["Value"].ToString().Split(',');
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    for (int i = 0; i < appliedStream.Length; i++)
                    {
                        if (dt.Rows[j]["StreamId"].ToString() == appliedStream[i].ToString())
                        {
                            lblStreamApplied.Text = dt.Rows[j]["stream_name"].ToString();
                        }
                    }
                }
            }
        }

        private void selectCast(string cast)
        {
            switch (cast)
            {
                case "1":
                    lblcast.Text = "G";
                    break;
                case "2":
                    lblcast.Text = "SC";
                    break;
                case "3":
                    lblcast.Text = "ST";
                    break;
                case "4":
                    lblcast.Text = "OBC";
                    break;
            }
        }

        private void selectBLanguage(string BLanguage)
        {
            string[] split = BLanguage.Split(',');
            foreach (string item in split)
            {
                switch (item)
                {
                    case "1":
                        lblBengali.Text += "Speak, ";
                        break;
                    case "2":
                        lblBengali.Text += "Read, ";
                        break;
                    case "3":
                        lblBengali.Text += "Write ";
                        break;
                }
            }
        }

        private void selectHLanguage(string HLanguage)
        {
            string[] split = HLanguage.Split(',');
            foreach (string item in split)
            {
                switch (item)
                {
                    case "1":
                        lblHindi.Text += "Speak, ";
                        break;
                    case "2":
                        lblHindi.Text += "Read, ";
                        break;
                    case "3":
                        lblHindi.Text += "Write ";
                        break;
                }
            }
        }

        private void selectELanguage(string ELanguage)
        {
            string[] split = ELanguage.Split(',');
            foreach (string item in split)
            {
                switch (item)
                {
                    case "1":
                        lblEnglish.Text += "Speak, ";
                        break;
                    case "2":
                        lblEnglish.Text += "Read, ";
                        break;
                    case "3":
                        lblEnglish.Text += "Write ";
                        break;
                }
            }
        }

        private void selectOLanguage(string OLanguage)
        {
            string[] split = OLanguage.Split(',');
            foreach (string item in split)
            {
                switch (item)
                {
                    case "1":
                        lblOther.Text += "Speak, ";
                        break;
                    case "2":
                        lblOther.Text += "Read, ";
                        break;
                    case "3":
                        lblOther.Text += "Write ";
                        break;
                }
            }
        }

        private void selectMaritalStatus(string strMaritalStatus)
        {
            switch (strMaritalStatus)
            {
                case "1":
                    lblMaritalStatus.Text = " :Married";
                    break;
                case "2":
                    lblMaritalStatus.Text = " :Single";
                    break;
            }
        }

        private void selectGender(string strGender)
        {
            switch (strGender)
            {
                case "1":
                    lblGender.Text = " :Male";
                    break;
                case "2":
                    lblGender.Text = " :Female";
                    break;
            }
        }

        private void selectHostel(string strHostelFacility)
        {
            switch (strHostelFacility)
            {
                case "True":
                    lblHostelFacility.Text = "Yes";
                    break;
                case "False":
                    lblHostelFacility.Text = "No";
                    break;
            }
        }

        private void selectRank(string strRank)
        {
            switch (strRank)
            {
                case "1":
                    lblOption.Text = "CMAT";
                    break;
                case "2":
                    lblOption.Text = "JMAT";
                    break;
                case "3":
                    lblOption.Text = "MAT";
                    break;

            }
        }

        
    }
}
