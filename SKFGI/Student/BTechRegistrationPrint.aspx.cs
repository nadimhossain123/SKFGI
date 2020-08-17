using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace CollegeERP.Student
{
    public partial class BTechRegistrationPrint : System.Web.UI.Page
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
            BusinessLayer.Student.BTechRegistration BtechReg = new BusinessLayer.Student.BTechRegistration();
            
            Entity.Student.BTechRegistration eBtechReg = new Entity.Student.BTechRegistration();
            eBtechReg.intMode = 5;
            eBtechReg.studentID = int.Parse(studentID);
            eBtechReg.CourseId = 2;
            DataSet ds = new DataSet();
            ds = BtechReg.GetStudentDetails(eBtechReg);
            //-------------Add---------------
            BusinessLayer.Common.Employee objEmp = new BusinessLayer.Common.Employee();
            Entity.Common.Employee Employee = new Entity.Common.Employee();
            //int LoginId = int.Parse(HttpContext.Current.User.Identity.Name);
            //Employee = objEmp.GetAllById(LoginId);
            //-------------------------------
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                   
                    ltrBatchname.Text = "<h2>APPLICATION FORM FOR ADMISSION IN " + ds.Tables[0].Rows[0]["batch_name"].ToString() + "</h2>";
                    lblSession.Text = ds.Tables[0].Rows[0]["batch_name"].ToString();
                    lblapplicationNumber.Text = ds.Tables[0].Rows[0]["appliation_no"].ToString();
                    lblEnrollmentNo.Text = ds.Tables[0].Rows[0]["enrollmentn_no"].ToString();
                    selectRank(ds.Tables[0].Rows[0]["rank"].ToString());
                    lblRankid.Text = ds.Tables[0].Rows[0]["rankid"].ToString();
                    //-----------Add on 05-08-2013-------------
                    //if ( Convert.ToBoolean( ds.Tables[0].Rows[0]["TFW"].ToString()) == true)
                    //{
                    //    chkTFW.Checked = true;
                    //}
                    //else { chkTFW.Checked = false; }
                    DDLTFW.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TFW"]);
                    //-----------Add On 27-08-2013-----------------------------
                    //if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IsLateral"].ToString()) == true)
                    //{
                    //    chkLateral.Checked = true;
                    //}
                    //else { chkLateral.Checked = false; }
                    RDBLateral.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["IsLateral"]);
                    //----------------------------------------

                    lblPhy.Text = ds.Tables[0].Rows[0]["phy"].ToString();
                    lblChem.Text = ds.Tables[0].Rows[0]["chem"].ToString();
                    lblMath.Text = ds.Tables[0].Rows[0]["math"].ToString();
                    lblEngg.Text = ds.Tables[0].Rows[0]["engg"].ToString();
                    lblBios.Text = ds.Tables[0].Rows[0]["bios"].ToString();

                    lblNameOfApplicant.Text = ds.Tables[0].Rows[0]["name"].ToString();
                    lblNameOfApplicant.Text = lblNameOfApplicant.Text.ToUpper();
                    lblApplicantName.Text = ds.Tables[0].Rows[0]["name"].ToString();
                    lblApplicantName.Text= lblApplicantName.Text.ToUpper();
                    lblDob.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["dob"].ToString()).ToString("dd/MM/yyy");

                    // New Fields
                    lblAdhar.Text= ds.Tables[0].Rows[0]["adhar_no"].ToString();
                    lblAccName.Text= ds.Tables[0].Rows[0]["bankholder_name"].ToString();
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

                    selectStreamapplied(ds.Tables[1]);
                    selectEducationBackground(ds.Tables[2]);
                    selectDocument(ds.Tables[3]);
                    //-----------
                    if (int.Parse(ds.Tables[0].Rows[0]["created_by"].ToString()) == int.Parse(ds.Tables[0].Rows[0]["updated_by"].ToString()))
                    {
                        int EmpId = int.Parse(ds.Tables[0].Rows[0]["created_by"].ToString());
                        Employee = objEmp.GetAllById(EmpId);
                        lblVarifiedBy.Text = Employee.FirstName + " " + Employee.MiddleName + " " + Employee.LastName;//dsEmp.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsEmp.Tables[0].Rows[0]["MiddleName"].ToString()+ " " + dsEmp.Tables[0].Rows[0]["LastName"].ToString();
                        lblAFVDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["created_date"].ToString()).ToString("dd/MM/yyy");//DateTime.Now.ToString("dd MMM yyyy");//Date.Now.ToShortDateString();
                    }
                    else
                    {
                        int EmpId = int.Parse(ds.Tables[0].Rows[0]["updated_by"].ToString());
                        Employee = objEmp.GetAllById(EmpId);
                        lblVarifiedBy.Text = Employee.FirstName + " " + Employee.MiddleName + " " + Employee.LastName;//dsEmp.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsEmp.Tables[0].Rows[0]["MiddleName"].ToString()+ " " + dsEmp.Tables[0].Rows[0]["LastName"].ToString();
                        lblAFVDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["updated_date"].ToString()).ToString("dd/MM/yyy"); //DateTime.Now.ToString("dd MMM yyyy");//Date.Now.ToShortDateString();
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
            eBtechReg.CourseId = 2;
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
                            //lblStreamApplied.Text = dt.Rows[j]["stream_name"].ToString();
                            StreamName(dt.Rows[j]["stream_name"].ToString());
                        }
                    }
                }
            }
        }

        private void StreamName(string cast)
        {
            switch (cast)
            {
                case "AEIE":
                    lblStreamApplied.Text = "AEIE(Applied Electronics and Instrumentation Engineering)";
                    break;
                case "CE":
                    lblStreamApplied.Text = "CE(Civil Engineering)";
                    break;
                case "CSE":
                    lblStreamApplied.Text = "CSE(Computer Science and Engineering)";
                    break;
                case "ECE":
                    lblStreamApplied.Text = "ECE(Electronic and Communication Engineering)";
                    break;
                case "EE":
                    lblStreamApplied.Text = "EE(Electrical Engineering)";
                    break;
                case "ME":
                    lblStreamApplied.Text = "ME(Mechanical Engineering)";
                    break;
            }
        }

        private void selectCast(string cast)
        {
            switch (cast)
            {
                case "1":
                    lblcast.Text = "General";
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
                    lblMaritalStatus.Text = " Married";
                    break;
                case "2":
                    lblMaritalStatus.Text = " Single";
                    break;
            }
        }

        private void selectGender(string strGender)
        {
            switch (strGender)
            {
                case "1":
                    lblGender.Text = " Male";
                    break;
                case "2":
                    lblGender.Text = " Female";
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
                    lblOption.Text = "WBJEE";
                    break;
                case "2":
                    lblOption.Text = "AIEEE";
                    break;
                case "3":
                    lblOption.Text = "JELAT";
                    break;
                case "4":
                    lblOption.Text = "DIRECT";
                    break;
                case "5":
                    lblOption.Text = "MQ";
                    break;
            }
        }
    }
}
