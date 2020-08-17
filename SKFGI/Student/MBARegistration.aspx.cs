using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CollegeERP.Student
{
    public partial class MBARegistration : System.Web.UI.Page
    {
        public string strStudentID
        {
            get { return ViewState["strStudentID"].ToString(); }
            set { ViewState["strStudentID"] = value; }
        }
        public string Photo { get; set; }
        protected void Page_PreInit(Object sender, EventArgs e)
        {
            if (Convert.ToString(Session["UserId"]) == "1")
            {
                this.MasterPageFile = "~/StudentMaster.Master";
            }
            else
            {
                this.MasterPageFile = "~/MasterAdmin.Master";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }

            if (!IsPostBack)
            {
                //if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_MBA))
                //{
                //    Response.Redirect("../Unauthorized.aspx");
                //}
                LoadSchoolList();
                LoadStateList();
                LoadStateListForSearch();
                loadBatch();
                loadStream();
                if (Request.QueryString["type"] != null && Request.QueryString["type"].Trim() == "view")
                    this.Master.FindControl("menu1").Visible = false;

                if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim().Length > 0)
                {
                    strStudentID = Request.QueryString["id"].ToString();
                    loadStudentDetails(strStudentID);
                }
                else
                {
                    ClearFields(Form.Controls);
                }
            }

        }
        protected void LoadSchoolList()
        {
            Entity.Common.StateDistrictCity StateDistrictCity_Entity = new Entity.Common.StateDistrictCity();
            BusinessLayer.Common.StateDistrictCity StateDistrictCity_BAL = new BusinessLayer.Common.StateDistrictCity();
            StateDistrictCity_Entity.SchoolID = 0;
            StateDistrictCity_Entity.CityID = 0;
            StateDistrictCity_Entity.StateID = 0;
            StateDistrictCity_Entity.DistrictID = 0;
            StateDistrictCity_Entity.School = "";
            DataTable DT = StateDistrictCity_BAL.GetAllSchool(StateDistrictCity_Entity);
            if (DT != null)
            {
                ddlSchool.DataSource = DT;
                ddlSchool.DataTextField = "School";
                ddlSchool.DataValueField = "SchoolID";
                ddlSchool.DataBind();


            }
            ListItem lst;
            lst = new ListItem("--Select--", "0");
            ddlSchool.Items.Insert(0, lst);
        }

        protected void LoadStateList()
        {
            Entity.Common.StateDistrictCity StateDistrictCity_Entity = new Entity.Common.StateDistrictCity();
            BusinessLayer.Common.StateDistrictCity StateDistrictCity_BAL = new BusinessLayer.Common.StateDistrictCity();
            StateDistrictCity_Entity.StateID = 0;
            StateDistrictCity_Entity.State = "";
            DataTable DT = StateDistrictCity_BAL.GetAll(StateDistrictCity_Entity);
            if (DT != null)
            {
                ddlstate.DataSource = DT;
                ddlstate.DataTextField = "State";
                ddlstate.DataValueField = "StateID";
                ddlstate.DataBind();


            }
            ListItem lst;
            lst = new ListItem("--Select--", "0");
            ddlstate.Items.Insert(0, lst);
            LoadDistrictList();
        }
        protected void LoadDistrictList()
        {
            Entity.Common.StateDistrictCity StateDistrictCity_Entity = new Entity.Common.StateDistrictCity();
            BusinessLayer.Common.StateDistrictCity StateDistrictCity_BAL = new BusinessLayer.Common.StateDistrictCity();
            StateDistrictCity_Entity.DistrictID = 0;
            StateDistrictCity_Entity.StateID = Convert.ToInt32(ddlstate.SelectedValue);
            StateDistrictCity_Entity.District = "";
            DataTable DT = StateDistrictCity_BAL.GetAllDistrict(StateDistrictCity_Entity);
            DataView dv = new DataView(DT);
            dv.RowFilter = "StateID='" + ddlstate.SelectedValue + "'";
            DT = dv.ToTable();
            if (DT != null)
            {
                ddlDistrict.DataSource = DT;
                ddlDistrict.DataTextField = "District";
                ddlDistrict.DataValueField = "DistrictId";
                ddlDistrict.DataBind();


            }
            ListItem lst;
            lst = new ListItem("--Select--", "0");
            ddlDistrict.Items.Insert(0, lst);
            LoadCityList();
        }
        protected void LoadCityList()
        {
            Entity.Common.StateDistrictCity StateDistrictCity_Entity = new Entity.Common.StateDistrictCity();
            BusinessLayer.Common.StateDistrictCity StateDistrictCity_BAL = new BusinessLayer.Common.StateDistrictCity();
            StateDistrictCity_Entity.CityID = 0;
            StateDistrictCity_Entity.StateID = Convert.ToInt32(ddlstate.SelectedValue);
            StateDistrictCity_Entity.DistrictID = Convert.ToInt32(ddlDistrict.SelectedValue);
            StateDistrictCity_Entity.City = "";
            DataTable DT = StateDistrictCity_BAL.GetAllCity(StateDistrictCity_Entity);
            DataView dv = new DataView(DT);
            dv.RowFilter = "DistrictID='" + ddlDistrict.SelectedValue + "'";
            DT = dv.ToTable();
            if (DT != null)
            {
                ddlCity.DataSource = DT;
                ddlCity.DataTextField = "City";
                ddlCity.DataValueField = "CityID";
                ddlCity.DataBind();


            }
            ListItem lst;
            lst = new ListItem("--Select--", "0");
            ddlCity.Items.Insert(0, lst);
        }

        protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDistrictList();
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCityList();
        }

        protected void LoadStateListForSearch()
        {
            Entity.Common.StateDistrictCity StateDistrictCity_Entity = new Entity.Common.StateDistrictCity();
            BusinessLayer.Common.StateDistrictCity StateDistrictCity_BAL = new BusinessLayer.Common.StateDistrictCity();
            StateDistrictCity_Entity.StateID = 0;
            StateDistrictCity_Entity.State = "";
            DataTable DT = StateDistrictCity_BAL.GetAll(StateDistrictCity_Entity);
            if (DT != null)
            {

                ddlStatesearch.DataSource = DT;
                ddlStatesearch.DataTextField = "State";
                ddlStatesearch.DataValueField = "StateID";
                ddlStatesearch.DataBind();
            }
            ListItem lst;
            lst = new ListItem("--Select--", "0");
            ddlStatesearch.Items.Insert(0, lst);
            LoadDistrictListForSearch();
        }
        protected void LoadDistrictListForSearch()
        {
            Entity.Common.StateDistrictCity StateDistrictCity_Entity = new Entity.Common.StateDistrictCity();
            BusinessLayer.Common.StateDistrictCity StateDistrictCity_BAL = new BusinessLayer.Common.StateDistrictCity();
            StateDistrictCity_Entity.DistrictID = 0;
            StateDistrictCity_Entity.StateID = Convert.ToInt32(ddlStatesearch.SelectedValue);
            StateDistrictCity_Entity.District = "";
            DataTable DT = StateDistrictCity_BAL.GetAllDistrict(StateDistrictCity_Entity);
            DataView dv = new DataView(DT);
            dv.RowFilter = "StateID='" + ddlStatesearch.SelectedValue + "'";
            DT = dv.ToTable();
            if (DT != null)
            {
                ddlDistrictSearch.DataSource = DT;
                ddlDistrictSearch.DataTextField = "District";
                ddlDistrictSearch.DataValueField = "DistrictId";
                ddlDistrictSearch.DataBind();


            }
            ListItem lst;
            lst = new ListItem("--Select--", "0");
            ddlDistrictSearch.Items.Insert(0, lst);
            LoadCityListSearch();
        }
        protected void LoadCityListSearch()
        {
            Entity.Common.StateDistrictCity StateDistrictCity_Entity = new Entity.Common.StateDistrictCity();
            BusinessLayer.Common.StateDistrictCity StateDistrictCity_BAL = new BusinessLayer.Common.StateDistrictCity();
            StateDistrictCity_Entity.CityID = 0;
            StateDistrictCity_Entity.StateID = Convert.ToInt32(ddlStatesearch.SelectedValue);
            StateDistrictCity_Entity.DistrictID = Convert.ToInt32(ddlDistrictSearch.SelectedValue);
            StateDistrictCity_Entity.City = "";
            DataTable DT = StateDistrictCity_BAL.GetAllCity(StateDistrictCity_Entity);
            DataView dv = new DataView(DT);
            dv.RowFilter = "DistrictID='" + ddlDistrictSearch.SelectedValue + "'";
            DT = dv.ToTable();
            if (DT != null)
            {
                ddlcitysearch.DataSource = DT;
                ddlcitysearch.DataTextField = "City";
                ddlcitysearch.DataValueField = "CityID";
                ddlcitysearch.DataBind();


            }

            ListItem lst;
            lst = new ListItem("--Select--", "0");
            ddlcitysearch.Items.Insert(0, lst);
        }

        protected void ddlStatesearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDistrictListForSearch();
        }

        protected void ddlDistrictSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCityListSearch();
        }

        private void loadBatch()
        {
            BusinessLayer.Student.BTechRegistration BtechReg = new BusinessLayer.Student.BTechRegistration();
            Entity.Student.BTechRegistration eBtechReg = new Entity.Student.BTechRegistration();
            eBtechReg.intMode = 1;
            eBtechReg.CourseId = 0;
            DataTable dt = new DataTable();
            dt = BtechReg.GetAllCommonSP(eBtechReg);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ddlBatch.DataSource = dt;
                    ddlBatch.DataBind();
                }
            }
        }

        private void loadStream()
        {
            BusinessLayer.Student.BTechRegistration BtechReg = new BusinessLayer.Student.BTechRegistration();
            Entity.Student.BTechRegistration eBtechReg = new Entity.Student.BTechRegistration();
            eBtechReg.intMode = 3;
            eBtechReg.CourseId = 1;
            DataTable dt = new DataTable();
            dt = BtechReg.GetAllCommonSP(eBtechReg);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    dt.Rows.RemoveAt(0);
                    dt.AcceptChanges();
                    //----------------------------
                    chkStream.DataSource = dt;
                    chkStream.DataTextField = "stream_name";
                    chkStream.DataValueField = "StreamId";
                    chkStream.DataBind();

                }
            }
        }

        private void loadStudentDetails(string studentID)
        {
            BusinessLayer.Student.MBARegistration mbaReg = new BusinessLayer.Student.MBARegistration();
            Entity.Student.MBARegistration embaReg = new Entity.Student.MBARegistration();

            embaReg.intMode = 5;
            embaReg.CourseId = 1;
            embaReg.studentID = int.Parse(studentID);
            DataSet ds = new DataSet();
            ds = mbaReg.GetStudentDetails(embaReg);
            if (ds != null)
            {
                if (ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlBatch.SelectedValue = ds.Tables[0].Rows[0]["batch_id"].ToString();
                    //txtapplicationNumber.Text = ds.Tables[0].Rows[0]["appliation_no"].ToString();
                    selectRank(ds.Tables[0].Rows[0]["rank"].ToString());
                    txtRankid.Text = ds.Tables[0].Rows[0]["rankid"].ToString();

                    RdbHostelFacility.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["IsHostelFacility"]);
                    txtRegistrationNo.Text = ds.Tables[0].Rows[0]["RegistrationNo"].ToString();
                    txtUniversityRollNo.Text = ds.Tables[0].Rows[0]["UniversityRollNo"].ToString();
                    txtMigrationInfo.Text = ds.Tables[0].Rows[0]["MigrationInfo"].ToString();

                    txtNameOfApplicant.Text = ds.Tables[0].Rows[0]["name"].ToString();
                    txtDob.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["dob"].ToString()).ToString("dd/MM/yyy");
                    txtFatherName.Text = ds.Tables[0].Rows[0]["fathers_name"].ToString();
                    txtFatherOccupation.Text = ds.Tables[0].Rows[0]["f_Occupation"].ToString();
                    txtMotherName.Text = ds.Tables[0].Rows[0]["mothers_name"].ToString();
                    txtMotherOccupation.Text = ds.Tables[0].Rows[0]["m_Occupation"].ToString();
                    txtGuardiansName.Text = ds.Tables[0].Rows[0]["guardian_name"].ToString();

                    //Added By Biswajit
                    txtAdhar.Text = ds.Tables[0].Rows[0]["adhar_no"].ToString();
                    txtBankAccountName.Text = ds.Tables[0].Rows[0]["bankholder_name"].ToString();
                    txtBankAcNo.Text = ds.Tables[0].Rows[0]["bankacc_no"].ToString();
                    txtIFSCCode.Text = ds.Tables[0].Rows[0]["bankIFSC_code"].ToString();
                    //

                    ddlSchool.SelectedValue = ds.Tables[0].Rows[0]["SchoolID"].ToString();
                    txtPAddress.Text = ds.Tables[0].Rows[0]["p_address"].ToString();
                    txtpin.Text = ds.Tables[0].Rows[0]["PPin"].ToString();
                    ddlstate.SelectedValue = ds.Tables[0].Rows[0]["PStateID"].ToString();
                    LoadDistrictList();
                    ddlDistrict.SelectedValue = ds.Tables[0].Rows[0]["PDistrictID"].ToString();
                    LoadCityList();
                    ddlCity.SelectedValue = ds.Tables[0].Rows[0]["PCityID"].ToString();

                    txtCAddress.Text = ds.Tables[0].Rows[0]["c_address"].ToString();
                    txtcPin.Text = ds.Tables[0].Rows[0]["CPin"].ToString();
                    ddlStatesearch.SelectedValue = ds.Tables[0].Rows[0]["CStateID"].ToString();
                    LoadDistrictListForSearch();
                    ddlDistrictSearch.SelectedValue = ds.Tables[0].Rows[0]["CDistrictID"].ToString();
                    LoadCityListSearch();
                    ddlcitysearch.SelectedValue = ds.Tables[0].Rows[0]["CCityID"].ToString();

                    ImgPhoto.ImageUrl = ds.Tables[0].Rows[0]["Photo"].ToString();

                    txtPResidential.Text = ds.Tables[0].Rows[0]["p_residential"].ToString();
                    txtSResidential.Text = ds.Tables[0].Rows[0]["s_residential"].ToString();
                    txtPMobile.Text = ds.Tables[0].Rows[0]["p_mobile"].ToString();
                    txtSMobile.Text = ds.Tables[0].Rows[0]["s_mobile"].ToString();
                    txtEmail.Text = ds.Tables[0].Rows[0]["email"].ToString();
                    selectHostel(ds.Tables[0].Rows[0]["hostel_facility"].ToString());
                    selectGender(ds.Tables[0].Rows[0]["gender"].ToString());
                    selectMaritalStatus(ds.Tables[0].Rows[0]["marital_status"].ToString());
                    txtMotherTong.Text = ds.Tables[0].Rows[0]["mother_tongue"].ToString();
                    txtNationality.Text = ds.Tables[0].Rows[0]["nationality"].ToString();
                    txtRealigion.Text = ds.Tables[0].Rows[0]["realigion"].ToString();
                    selectBLanguage(ds.Tables[0].Rows[0]["language_bengali"].ToString());
                    selectHLanguage(ds.Tables[0].Rows[0]["language_hindi"].ToString());
                    selectELanguage(ds.Tables[0].Rows[0]["language_english"].ToString());
                    selectOLanguage(ds.Tables[0].Rows[0]["language_other"].ToString());
                    selectCast(ds.Tables[0].Rows[0]["caste_id"].ToString());
                    txtMonthlyIncome.Text = ds.Tables[0].Rows[0]["monthly_income"].ToString();
                    txtRefferenceName1.Text = ds.Tables[0].Rows[0]["reference_name1"].ToString();
                    txtRefferenceAddress1.Text = ds.Tables[0].Rows[0]["reference_address1"].ToString();
                    txtRefferenceContactNumber1.Text = ds.Tables[0].Rows[0]["reference_contact_number1"].ToString();
                    txtRefferenceName2.Text = ds.Tables[0].Rows[0]["reference_name2"].ToString();
                    txtRefferenceAddress2.Text = ds.Tables[0].Rows[0]["reference_address2"].ToString();
                    txtRefferenceContactNumber2.Text = ds.Tables[0].Rows[0]["reference_contact_number2"].ToString();
                    txtBloodGroup.Text = ds.Tables[0].Rows[0]["BloodGroup"].ToString();
                    txtSubmittedBy.Text = ds.Tables[0].Rows[0]["SubmittedBy"].ToString();

                    selectEducationBackground(ds.Tables[1]);

                    selectExprerienceDetails(ds.Tables[2]);

                    selectQualifyingExaminations(ds.Tables[3]);

                    selectDocument(ds.Tables[4]);
                    selectStreamapplied(ds.Tables[5]);

                    btnSave.Text = "Update";
                    Message.Show = false;
                    btnPrint.Attributes.Add("onclick", "window.open('MBARegistrationPrint.aspx?id=" + studentID + "'); return false;");
                    ddlBatch.Enabled = false;
                }
                else
                {
                    strStudentID = "0";
                    Response.Redirect(ResolveClientUrl("MBARegistration.aspx"));
                }
            }
        }

        private void selectQualifyingExaminations(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (dataTable.Rows[i]["serial_id"].ToString() == "1")
                    {
                        txtText1.Text = dataTable.Rows[i]["test"].ToString();
                        txtDateOfExam1.Text = dataTable.Rows[i]["examination_date"].ToString();
                        txtMarksObtain1.Text = dataTable.Rows[i]["marks_Obtained"].ToString();
                    }
                    else if (dataTable.Rows[i]["serial_id"].ToString() == "2")
                    {
                        txtText2.Text = dataTable.Rows[i]["test"].ToString();
                        txtDateOfExam2.Text = dataTable.Rows[i]["examination_date"].ToString();
                        txtMarksObtain2.Text = dataTable.Rows[i]["marks_Obtained"].ToString();
                    }
                    else if (dataTable.Rows[i]["serial_id"].ToString() == "3")
                    {
                        txtText3.Text = dataTable.Rows[i]["test"].ToString();
                        txtDateOfExam3.Text = dataTable.Rows[i]["examination_date"].ToString();
                        txtMarksObtain3.Text = dataTable.Rows[i]["marks_Obtained"].ToString();
                    }
                }
            }
        }

        private void selectExprerienceDetails(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {

                    if (dataTable.Rows[i]["serial_id"].ToString() == "1")
                    {
                        txt1NameOfCompany.Text = dataTable.Rows[i]["company_name"].ToString();
                        txt1JobProfile.Text = dataTable.Rows[i]["responsibility"].ToString();
                        txt1DateOfJoining.Text = dataTable.Rows[i]["joning_date"].ToString();
                        txt1DateOfLeave.Text = dataTable.Rows[i]["leaving_date"].ToString();
                        txt1Remark.Text = dataTable.Rows[i]["remarks"].ToString();
                    }
                    else if (dataTable.Rows[i]["serial_id"].ToString() == "2")
                    {
                        txt2NameOfCompany.Text = dataTable.Rows[i]["company_name"].ToString();
                        txt2JobProfile.Text = dataTable.Rows[i]["responsibility"].ToString();
                        txt2DateOfJoining.Text = dataTable.Rows[i]["joning_date"].ToString();
                        txt2DateOfLeave.Text = dataTable.Rows[i]["leaving_date"].ToString();
                        txt2Remark.Text = dataTable.Rows[i]["remarks"].ToString();
                    }
                    else if (dataTable.Rows[i]["serial_id"].ToString() == "3")
                    {
                        txt3NameOfCompany.Text = dataTable.Rows[i]["company_name"].ToString();
                        txt3JobProfile.Text = dataTable.Rows[i]["responsibility"].ToString();
                        txt3DateOfJoining.Text = dataTable.Rows[i]["joning_date"].ToString();
                        txt3DateOfLeave.Text = dataTable.Rows[i]["leaving_date"].ToString();
                        txt3Remark.Text = dataTable.Rows[i]["remarks"].ToString();
                    }

                }
            }
        }

        private void selectRank(string strRank)
        {
            switch (strRank)
            {
                case "1":
                    rbCmat.Checked = true;
                    break;
                case "2":
                    rbJeMat.Checked = true;
                    break;
                case "3":
                    rbMat.Checked = true;
                    break;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields(Form.Controls);
            btnPrint.Attributes.Add("onclick", "alert('No Form To Print'); return false;");
        }

        protected void ClearFields(ControlCollection pageControls)
        {
            strStudentID = "0";
            foreach (Control contl in pageControls)
            {
                string strCntName = (contl.GetType()).Name;
                switch (strCntName)
                {
                    case "TextBox":
                        TextBox tbSource = (TextBox)contl;
                        tbSource.Text = "";
                        break;
                    case "RadioButtonList":
                        RadioButtonList rblSource = (RadioButtonList)contl;
                        rblSource.SelectedIndex = -1;
                        break;
                    case "DropDownList":
                        DropDownList ddlSource = (DropDownList)contl;
                        //ddlSource.SelectedIndex = 0;
                        ddlSource.SelectedValue = "0";
                        break;
                    case "ListBox":
                        ListBox lbsource = (ListBox)contl;
                        lbsource.SelectedIndex = -1;
                        break;
                    case "RadioButton":
                        RadioButton rdb = (RadioButton)contl;
                        rdb.Checked = false;
                        break;
                    case "CheckBox":
                        CheckBox chk = (CheckBox)contl;
                        chk.Checked = false;
                        break;

                }
                ClearFields(contl.Controls);
                //RDBLateral.SelectedValue = "False";
                RdbHostelFacility.SelectedValue = "False";
               // DDLTFW.SelectedValue = "False";
            }

            ImgPhoto.ImageUrl = "StudentPhoto/Male.jpg";
            rbCmat.Checked = true;
            rbHostalN.Checked = true;
            rbMale.Checked = true;
            rbSingle.Checked = true;
            rbGeneral.Checked = true;
            for (int i = 0; i < chkStream.Items.Count; i++)
            {
                chkStream.Items[i].Selected = false;
            }
            ddlBatch.Enabled = true;
            btnSave.Text = "Save";
            Message.Show = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (checkStreamSelected() == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Can't Save. Please Select One Stream";
            }
            else if (checkStreamSelected() > 1)
            {
                Message.IsSuccess = false;
                Message.Text = "Can't Save. Please Select Only One Stream";
            }
            else if (checkDocumentsSelected() == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Can't Save. Please Select One Document Submitted";
            }
            else if (!IsValidPhoto())
            {
                Message.IsSuccess = false;
                Message.Text = "Please Select JPG or BMP File to Upload.";
            }
            else
            {
                if (strStudentID == "0")
                {
                    if (saveData(10) > 0)
                    {
                        ClearFields(Form.Controls);
                        Message.IsSuccess = true;
                        Message.Text = "Student Information Saved Successfully";
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Can't Save. Duplicate Application No Is Not Allowed";
                    }

                }
                else
                {
                    if (saveData(11) > 0)
                    {
                        ClearFields(Form.Controls);
                        Message.IsSuccess = true;
                        Message.Text = "Student Information Updated Successfully";
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Can't Save. Duplicate Application No Is Not Allowed";
                    }
                }
            }
            Message.Show = true;

        }

        private int saveData(int callMode)
        {
            BusinessLayer.Student.MBARegistration mbaReg = new BusinessLayer.Student.MBARegistration();
            Entity.Student.MBARegistration embaReg = new Entity.Student.MBARegistration();

            embaReg.intMode = callMode;
            embaReg.studentID = int.Parse(strStudentID);
            embaReg.intCompanyId = BusinessLayer.Common.Company.GetCompanyId("MBA");
            embaReg.login_id = HttpContext.Current.User.Identity.Name;
            embaReg.CourseId = 1;
            embaReg.strapplicationNumber = "";
            embaReg.strBatch = ddlBatch.SelectedValue.Trim();
            embaReg.strRank = findSelectedRank();
            embaReg.strRankid = txtRankid.Text.Trim().Replace("'", "''");
            embaReg.strRegistrationNo = txtRegistrationNo.Text.Trim().Replace("'", "''");
            embaReg.strUniversityRollNo = txtUniversityRollNo.Text.Trim().Replace("'", "''");
            embaReg.strMigrationInfo = txtMigrationInfo.Text.Trim().Replace("'", "''");
            embaReg.strIsHostelFacility = Convert.ToBoolean(RdbHostelFacility.SelectedItem.Value);

            embaReg.strNameOfApplicant = txtNameOfApplicant.Text.Trim().Replace("'", "''");
            string[] Date = txtDob.Text.Trim().Split('/');
            embaReg.strDob = (Date[1].Trim() + "/" + Date[0].Trim() + "/" + Date[2].Trim() + " 00:00:00").ToString();
            embaReg.strFatherName = txtFatherName.Text.Trim().Replace("'", "''");
            embaReg.strFatherOccupation = txtFatherOccupation.Text.Trim().Replace("'", "''");
            embaReg.strMotherName = txtMotherName.Text.Trim().Replace("'", "''");
            embaReg.strMotherOccupation = txtMotherOccupation.Text.Trim().Replace("'", "''");
            embaReg.strGuardiansName = txtGuardiansName.Text.Trim().Replace("'", "''");

            //Added  By Biswajit

            embaReg.strAdhar = txtAdhar.Text.Trim().Replace("'", "''");
            embaReg.strBankHName = txtBankAccountName.Text.Trim().Replace("'", "''");
            embaReg.strBankAcc = txtBankAcNo.Text.Trim().Replace("'", "''");
            embaReg.strBankIFSC = txtIFSCCode.Text.Trim().Replace("'", "''");

            //

            embaReg.SchoolID = Convert.ToInt32(ddlSchool.SelectedValue);
            embaReg.strPAddress = txtPAddress.Text.Trim().Replace("'", "''");
            embaReg.strPState = Convert.ToInt32(ddlstate.SelectedValue);
            embaReg.strPCity = Convert.ToInt32(ddlCity.SelectedValue);
            embaReg.strPDistrict = Convert.ToInt32(ddlDistrict.SelectedValue);
            embaReg.strPPin = Convert.ToString(txtpin.Text.Trim());
            embaReg.strCAddress = txtCAddress.Text.Trim().Replace("'", "''");
            embaReg.strCState = Convert.ToInt32(ddlStatesearch.SelectedValue);
            embaReg.strCDistrict = Convert.ToInt32(ddlDistrictSearch.SelectedValue);
            embaReg.strCCity = Convert.ToInt32(ddlcitysearch.SelectedValue);
            embaReg.strCPin = Convert.ToString(txtcPin.Text.Trim());

            embaReg.strPResidential = txtPResidential.Text.Trim().Replace("'", "''");
            embaReg.strSResidential = txtSResidential.Text.Trim().Replace("'", "''");
            embaReg.strPMobile = txtPMobile.Text.Trim().Replace("'", "''");
            embaReg.strSMobile = txtSMobile.Text.Trim().Replace("'", "''");
            embaReg.strEmail = txtEmail.Text.Trim().Replace("'", "''");
            embaReg.strHostalFacitity = findHostalSelected();
            embaReg.strGender = findGender();
            embaReg.strMarital = findMaritalStatus();
            embaReg.strMotherTong = txtMotherTong.Text.Trim().Replace("'", "''");
            embaReg.strNationality = txtNationality.Text.Trim().Replace("'", "''");
            embaReg.strRealigion = txtRealigion.Text.Trim().Replace("'", "''");
            embaReg.strBengali = findBLanguagePower();
            embaReg.strHindi = findHLanguagePower();
            embaReg.strEnglish = findELanguagePower();
            embaReg.strOther = findOLanguagePower();
            embaReg.strCast = findCast();
            embaReg.strMonthlyIncome = txtMonthlyIncome.Text.Trim().Replace("'", "''");
            embaReg.strRefferenceName1 = txtRefferenceName1.Text.Trim().Replace("'", "''");
            embaReg.strRefferenceAddress1 = txtRefferenceAddress1.Text.Trim().Replace("'", "''");
            embaReg.strRefferenceContactNumber1 = txtRefferenceContactNumber1.Text.Trim().Replace("'", "''");
            embaReg.strRefferenceName2 = txtRefferenceName2.Text.Trim().Replace("'", "''");
            embaReg.strRefferenceAddress2 = txtRefferenceAddress2.Text.Trim().Replace("'", "''");
            embaReg.strRefferenceContactNumber2 = txtRefferenceContactNumber2.Text.Trim().Replace("'", "''");

            embaReg.strXSubject = txtXSubject.Text.Trim().Replace("'", "''");
            embaReg.strXBoard = txtXBoard.Text.Trim().Replace("'", "''");
            embaReg.strXCollege = txtXCollege.Text.Trim().Replace("'", "''");
            embaReg.strXYearOfPassing = txtXYearOfPassing.Text.Trim().Replace("'", "''");
            embaReg.strXMarks = txtXMarks.Text.Trim().Replace("'", "''");

            embaReg.strXiiSubject = txtXiiSubject.Text.Trim().Replace("'", "''");
            embaReg.strXiiBoard = txtXiiBoard.Text.Trim().Replace("'", "''");
            embaReg.strXiiCollege = txtXiiCollege.Text.Trim().Replace("'", "''");
            embaReg.strXiiYearOfPassing = txtXiiYearOfPassing.Text.Trim().Replace("'", "''");
            embaReg.strXiiMarks = txtXiiMarks.Text.Trim().Replace("'", "''");

            embaReg.strGSubject = txtGSubject.Text.Trim().Replace("'", "''");
            embaReg.strGBoard = txtGBoard.Text.Trim().Replace("'", "''");
            embaReg.strGCollege = txtGCollege.Text.Trim().Replace("'", "''");
            embaReg.strGYearOfPassing = txtGYearOfPassing.Text.Trim().Replace("'", "''");
            embaReg.strGMarks = txtGMarks.Text.Trim().Replace("'", "''");

            embaReg.strDSubject = txtDSubject.Text.Trim().Replace("'", "''");
            embaReg.strDBoard = txtDBoard.Text.Trim().Replace("'", "''");
            embaReg.strDCollege = txtDCollege.Text.Trim().Replace("'", "''");
            embaReg.strDYearOfPassing = txtDYearOfPassing.Text.Trim().Replace("'", "''");
            embaReg.strDMarks = txtDMarks.Text.Trim().Replace("'", "''");

            embaReg.strNameofComapny1 = txt1NameOfCompany.Text.Trim().Replace("'", "''");
            embaReg.strResponsibility1 = txt1JobProfile.Text.Trim().Replace("'", "''");
            embaReg.strjoning1 = txt1DateOfJoining.Text.Trim().Replace("'", "''");
            embaReg.strLeaving1 = txt1DateOfLeave.Text.Trim().Replace("'", "''");
            embaReg.strRemarks1 = txt1Remark.Text.Trim().Replace("'", "''");

            embaReg.strNameofComapny2 = txt2NameOfCompany.Text.Trim().Replace("'", "''");
            embaReg.strResponsibility2 = txt2JobProfile.Text.Trim().Replace("'", "''");
            embaReg.strjoning2 = txt2DateOfJoining.Text.Trim().Replace("'", "''");
            embaReg.strLeaving2 = txt2JobProfile.Text.Trim().Replace("'", "''");
            embaReg.strRemarks2 = txt2Remark.Text.Trim().Replace("'", "''");

            embaReg.strNameofComapny3 = txt3NameOfCompany.Text.Trim().Replace("'", "''");
            embaReg.strResponsibility3 = txt3JobProfile.Text.Trim().Replace("'", "''");
            embaReg.strjoning3 = txt3DateOfJoining.Text.Trim().Replace("'", "''");
            embaReg.strLeaving3 = txt3JobProfile.Text.Trim().Replace("'", "''");
            embaReg.strRemarks3 = txt3Remark.Text.Trim().Replace("'", "''");

            embaReg.strtest1 = txtText1.Text.Trim().Replace("'", "''");
            embaReg.strExaminationDate1 = txtDateOfExam1.Text.Trim().Replace("'", "''");
            embaReg.strMarks1 = txtMarksObtain1.Text.Trim().Replace("'", "''");

            embaReg.strtest2 = txtText2.Text.Trim().Replace("'", "''");
            embaReg.strExaminationDate2 = txtDateOfExam2.Text.Trim().Replace("'", "''");
            embaReg.strMarks2 = txtMarksObtain3.Text.Trim().Replace("'", "''");

            embaReg.strtest3 = txtText3.Text.Trim().Replace("'", "''");
            embaReg.strExaminationDate2 = txtDateOfExam3.Text.Trim().Replace("'", "''");
            embaReg.strMarks3 = txtMarksObtain3.Text.Trim().Replace("'", "''");

            embaReg.strDocList1 = chkListDoc1.Checked == true ? "1" : "0";
            embaReg.strDocList2 = chkListDoc2.Checked == true ? "2" : "0";
            embaReg.strDocList3 = chkListDoc3.Checked == true ? "3" : "0";
            embaReg.strDocList4 = chkListDoc4.Checked == true ? "4" : "0";
            embaReg.strDocList5 = chkListDoc5.Checked == true ? "5" : "0";
            embaReg.strDocList6 = chkListDoc6.Checked == true ? "6" : "0";
            embaReg.strDocList7 = chkListDoc7.Checked == true ? "7" : "0";
            embaReg.strDocList8 = chkListDoc8.Checked == true ? "8" : "0";
            embaReg.strDocList9 = chkListDoc9.Checked == true ? "9" : "0";
            embaReg.BloodGroup = txtBloodGroup.Text.Trim();

            string checkedIItem = "";
            int checkedIItemCount = chkStream.Items.Count;
            for (int i = 0; i < checkedIItemCount; i++)
            {
                if (chkStream.Items[i].Selected)
                {
                    checkedIItem += chkStream.Items[i].Value.ToString() + ","; ;
                }
            }

            if (checkedIItem.Length > 0)
            {
                checkedIItem = checkedIItem.Remove(checkedIItem.LastIndexOf(","), 1);
            }
            embaReg.strStreamApplied = checkedIItem;
            embaReg.strPhoto = Photo;
            embaReg.SubmittedBy = txtSubmittedBy.Text.Trim();

            int RowsAffected = mbaReg.Save(embaReg);
            if (RowsAffected > 0)
            {
                if (Photo != "")
                {
                    string ff = (Server.MapPath("") + "\\StudentPhoto\\" + embaReg.studentID + Photo);
                    uploadImage.PostedFile.SaveAs(ff);
                }
            }
            btnPrint.Attributes.Add("onclick", "window.open('MBARegistrationPrint.aspx?id=" + embaReg.studentID + "'); return false;");
            return RowsAffected;
        }

        public bool IsValidPhoto()
        {
            bool IsValid = true;
            if (uploadImage.PostedFile.FileName != null && uploadImage.PostedFile.ContentLength > 0)
            {
                string fn = uploadImage.FileName;
                string fileExt = System.IO.Path.GetExtension(fn);
                if (fileExt == ".jpg" || fileExt == ".bmp" || fileExt == ".JPG")
                {
                    string sm = Server.MapPath("");
                    Photo = fileExt;
                }
                else
                    IsValid = false;
            }
            else
            {
                Photo = "";
            }
            return IsValid;
        }

        private string findSelectedRank()
        {
            string strCheckItem = "0";
            if (rbCmat.Checked == true)
                strCheckItem = "1";
            else if (rbJeMat.Checked == true)
                strCheckItem = "2";
            else if (rbMat.Checked == true)
                strCheckItem = "3";
            return strCheckItem;
        }

        private int checkStreamSelected()
        {
            int ItemCount = 0;
            for (int j = 0; j < chkStream.Items.Count; j++)
            {
                if (chkStream.Items[j].Selected == true)
                {
                    ItemCount += 1;
                }
            }
            return ItemCount;
        }

        private int checkDocumentsSelected()
        {
            int ItemCount = 0;
            if (chkListDoc1.Checked)
            {
                ItemCount += 1;
            }
            if (chkListDoc2.Checked)
            {
                ItemCount += 1;
            }
            if (chkListDoc3.Checked)
            {
                ItemCount += 1;
            }
            if (chkListDoc4.Checked)
            {
                ItemCount += 1;
            }
            if (chkListDoc5.Checked)
            {
                ItemCount += 1;
            }
            if (chkListDoc6.Checked)
            {
                ItemCount += 1;
            }
            if (chkListDoc7.Checked)
            {
                ItemCount += 1;
            }
            if (chkListDoc8.Checked)
            {
                ItemCount += 1;
            }
            if (chkListDoc9.Checked)
            {
                ItemCount += 1;
            }
            return ItemCount;
        }


        private string findCast()
        {
            string strCheckItem = "0";
            if (rbGeneral.Checked == true)
                strCheckItem = "1";
            else if (rbSc.Checked == true)
                strCheckItem = "2";
            else if (rbSt.Checked == true)
                strCheckItem = "3";
            else if (rbObc.Checked == true)
                strCheckItem = "4";
            return strCheckItem;
        }

        private string findOLanguagePower()
        {
            string LanguagePower = "";
            if (chkOSpeak.Checked == true)
                LanguagePower += "1,";
            if (chkORead.Checked == true)
                LanguagePower += "2,";
            if (chkOWrite.Checked == true)
                LanguagePower += "3,";
            if (LanguagePower != "")
                LanguagePower = LanguagePower.Remove(LanguagePower.LastIndexOf(","), 1);
            return LanguagePower;
        }

        private string findELanguagePower()
        {
            string LanguagePower = "";
            if (chkESpeak.Checked == true)
                LanguagePower += "1,";
            if (chkERead.Checked == true)
                LanguagePower += "2,";
            if (chkEWrite.Checked == true)
                LanguagePower += "3,";
            if (LanguagePower != "")
                LanguagePower = LanguagePower.Remove(LanguagePower.LastIndexOf(","), 1);
            return LanguagePower;
        }

        private string findHLanguagePower()
        {
            string LanguagePower = "";
            if (chkHSpeak.Checked == true)
                LanguagePower += "1,";
            if (chkHRead.Checked == true)
                LanguagePower += "2,";
            if (chkHWrite.Checked == true)
                LanguagePower += "3,";
            if (LanguagePower != "")
                LanguagePower = LanguagePower.Remove(LanguagePower.LastIndexOf(","), 1);
            return LanguagePower;
        }

        private string findBLanguagePower()
        {
            string LanguagePower = "";
            if (chkBSpeak.Checked == true)
                LanguagePower += "1,";
            if (chkBRead.Checked == true)
                LanguagePower += "2,";
            if (chkBWrite.Checked == true)
                LanguagePower += "3,";
            if (LanguagePower != "")
                LanguagePower = LanguagePower.Remove(LanguagePower.LastIndexOf(","), 1);
            return LanguagePower;
        }

        private string findMaritalStatus()
        {
            string strCheckItem = "0";
            if (rbMarried.Checked == true)
                strCheckItem = "1";
            else
                strCheckItem = "2";
            return strCheckItem;
        }

        private string findHostalSelected()
        {
            string strCheckItem = "0";
            if (rbHostalY.Checked == true)
                strCheckItem = "1";
            else
                strCheckItem = "2";
            return strCheckItem;
        }

        private string findGender()
        {
            string strCheckItem = "0";
            if (rbMale.Checked == true)
                strCheckItem = "1";
            else
                strCheckItem = "2";
            return strCheckItem;
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
                        txtXSubject.Text = dataTable.Rows[i]["major_subject"].ToString();
                        txtXBoard.Text = dataTable.Rows[i]["board"].ToString();
                        txtXCollege.Text = dataTable.Rows[i]["college"].ToString();
                        txtXYearOfPassing.Text = dataTable.Rows[i]["year_passing"].ToString();
                        txtXMarks.Text = dataTable.Rows[i]["division_marks"].ToString();
                    }
                    else if (dataTable.Rows[i]["exam_id"].ToString() == "2")
                    {
                        txtXiiSubject.Text = dataTable.Rows[i]["major_subject"].ToString();
                        txtXiiBoard.Text = dataTable.Rows[i]["board"].ToString();
                        txtXiiCollege.Text = dataTable.Rows[i]["college"].ToString();
                        txtXiiYearOfPassing.Text = dataTable.Rows[i]["year_passing"].ToString();
                        txtXiiMarks.Text = dataTable.Rows[i]["division_marks"].ToString();
                    }
                    else if (dataTable.Rows[i]["exam_id"].ToString() == "3")
                    {
                        txtDSubject.Text = dataTable.Rows[i]["major_subject"].ToString();
                        txtDBoard.Text = dataTable.Rows[i]["board"].ToString();
                        txtDCollege.Text = dataTable.Rows[i]["college"].ToString();
                        txtDYearOfPassing.Text = dataTable.Rows[i]["year_passing"].ToString();
                        txtDMarks.Text = dataTable.Rows[i]["division_marks"].ToString();
                    }
                    else if (dataTable.Rows[i]["exam_id"].ToString() == "4")
                    {
                        txtGSubject.Text = dataTable.Rows[i]["major_subject"].ToString();
                        txtGBoard.Text = dataTable.Rows[i]["board"].ToString();
                        txtGCollege.Text = dataTable.Rows[i]["college"].ToString();
                        txtGYearOfPassing.Text = dataTable.Rows[i]["year_passing"].ToString();
                        txtGMarks.Text = dataTable.Rows[i]["division_marks"].ToString();
                    }
                }
            }
        }

        private void selectCast(string cast)
        {
            switch (cast)
            {
                case "1":
                    rbGeneral.Checked = true;
                    break;
                case "2":
                    rbSc.Checked = true;
                    break;
                case "3":
                    rbSt.Checked = true;
                    break;
                case "4":
                    rbObc.Checked = true;
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
                        chkBSpeak.Checked = true;
                        break;
                    case "2":
                        chkBRead.Checked = true;
                        break;
                    case "3":
                        chkBWrite.Checked = true;
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
                        chkHSpeak.Checked = true;
                        break;
                    case "2":
                        chkHRead.Checked = true;
                        break;
                    case "3":
                        chkHWrite.Checked = true;
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
                        chkESpeak.Checked = true;
                        break;
                    case "2":
                        chkERead.Checked = true;
                        break;
                    case "3":
                        chkEWrite.Checked = true;
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
                        chkOSpeak.Checked = true;
                        break;
                    case "2":
                        chkORead.Checked = true;
                        break;
                    case "3":
                        chkOWrite.Checked = true;
                        break;
                }
            }
        }

        private void selectMaritalStatus(string strMaritalStatus)
        {
            switch (strMaritalStatus)
            {
                case "1":
                    rbMarried.Checked = true;
                    break;
                case "2":
                    rbSingle.Checked = true;
                    break;
            }
        }

        private void selectGender(string strGender)
        {
            switch (strGender)
            {
                case "1":
                    rbMale.Checked = true;
                    break;
                case "2":
                    rbFemale.Checked = true;
                    break;
            }
        }

        private void selectHostel(string strHostelFacility)
        {
            switch (strHostelFacility)
            {
                case "1":
                    rbHostalY.Checked = true;
                    rbHostalN.Checked = false;
                    break;
                case "2":
                    rbHostalN.Checked = true;
                    rbHostalY.Checked = false;
                    break;
            }
        }

        private void selectStreamapplied(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0 && chkStream.Items.Count > 0)
            {
                string[] appliedStream = dataTable.Rows[0]["Value"].ToString().Split(',');
                for (int j = 0; j < chkStream.Items.Count; j++)
                {
                    for (int i = 0; i < appliedStream.Length; i++)
                    {
                        if (chkStream.Items[j].Value == appliedStream[i].ToString())
                        {
                            chkStream.Items[j].Selected = true;
                        }
                    }
                }
            }
        }

        protected void chkSameasAbove_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSameasAbove.Checked)
            {
                txtCAddress.Text = txtPAddress.Text;
                ddlStatesearch.SelectedIndex = ddlstate.SelectedIndex;
                LoadDistrictListForSearch();
                ddlDistrictSearch.SelectedIndex = ddlDistrict.SelectedIndex;
                LoadCityListSearch();
                ddlcitysearch.SelectedIndex = ddlCity.SelectedIndex;
                txtcPin.Text = txtpin.Text;
            }
            else
            {
                txtCAddress.Text = "";
                ddlStatesearch.SelectedIndex = 0;
                ddlDistrictSearch.SelectedIndex = 0;
                ddlcitysearch.SelectedIndex = 0;
                txtcPin.Text = "";
            }
        }
    }

}