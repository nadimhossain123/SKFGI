using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SKFGI.Student
{
    public partial class NonAICTERegistration : System.Web.UI.Page
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
                //if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_BTECH))
                //{
                //    Response.Redirect("../Unauthorized.aspx");
                //}
                LoadStateList();
                LoadStateListForSearch();
                LoadSchoolList();
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


        private void loadStudentDetails(string studentID)
        {
            BusinessLayer.Student.NonAICTERegistration NonAICTEReg = new BusinessLayer.Student.NonAICTERegistration();
            Entity.Student.NonAICTERegistration eNonAICTEReg = new Entity.Student.NonAICTERegistration();
            eNonAICTEReg.intMode = 5;
            eNonAICTEReg.studentID = int.Parse(studentID);
            eNonAICTEReg.CourseId = 5;
            DataSet ds = new DataSet();
            ds = NonAICTEReg.GetStudentDetails(eNonAICTEReg);
            if (ds != null)
            {
                if (ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {

                    ddlBatch.SelectedValue = ds.Tables[0].Rows[0]["batch_id"].ToString();
                    //txtapplicationNumber.Text = ds.Tables[0].Rows[0]["appliation_no"].ToString();
                    txtEnrollmentNo.Text = ds.Tables[0].Rows[0]["enrollmentn_no"].ToString();
                    selectRank(ds.Tables[0].Rows[0]["rank"].ToString());
                    txtRankid.Text = ds.Tables[0].Rows[0]["rankid"].ToString();
                    //RDBLateral.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["IsLateral"]);
                    DDLReadmission.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["IsReAdmission"]);

                    RdbHostelFacility.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["IsHostelFacility"]);
                    txtRegistrationNo.Text = ds.Tables[0].Rows[0]["RegistrationNo"].ToString();
                    txtUniversityRollNo.Text = ds.Tables[0].Rows[0]["UniversityRollNo"].ToString();
                    txtMigrationInfo.Text = ds.Tables[0].Rows[0]["MigrationInfo"].ToString();
                    //DDLTFW.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TFW"]);

                    // txtPhy.Text = ds.Tables[0].Rows[0]["phy"].ToString();
                    // txtChem.Text = ds.Tables[0].Rows[0]["chem"].ToString();
                    // txtMath.Text = ds.Tables[0].Rows[0]["math"].ToString();
                    //txtEngg.Text = ds.Tables[0].Rows[0]["engg"].ToString();

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

                    selectStreamapplied(ds.Tables[1]);
                    selectEducationBackground(ds.Tables[2]);
                    selectDocument(ds.Tables[3]);

                    Message.Show = false;
                    btnSave.Text = "Update";
                    btnPrint.Attributes.Add("onclick", "window.open('NonAICTERegistration.aspx?id=" + studentID + "'); return false;");

                    ddlBatch.Enabled = false;
                }
                else
                {
                    strStudentID = "0";
                    Response.Redirect(ResolveClientUrl("NonAICTERegistration.aspx"));
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
                    else if (dataTable.Rows[i]["document_id"].ToString() == "10")
                        ChkListDoc10.Checked = true;

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
                        txtXTotalMrkObtn.Text = dataTable.Rows[i]["total_M_Obtn"].ToString();
                        txtXMarks.Text = dataTable.Rows[i]["division_marks"].ToString();
                    }
                    else if (dataTable.Rows[i]["exam_id"].ToString() == "2")
                    {
                        txtXiiSubject.Text = dataTable.Rows[i]["major_subject"].ToString();
                        txtXiiBoard.Text = dataTable.Rows[i]["board"].ToString();
                        txtXiiCollege.Text = dataTable.Rows[i]["college"].ToString();
                        txtXiiYearOfPassing.Text = dataTable.Rows[i]["year_passing"].ToString();
                        txtXiiTotalMrkObtn.Text = dataTable.Rows[i]["total_M_Obtn"].ToString();
                        txtXiiMarks.Text = dataTable.Rows[i]["division_marks"].ToString();
                    }
                    else if (dataTable.Rows[i]["exam_id"].ToString() == "3")
                    {
                        txtDSubject.Text = dataTable.Rows[i]["major_subject"].ToString();
                        txtDBoard.Text = dataTable.Rows[i]["board"].ToString();
                        txtDCollege.Text = dataTable.Rows[i]["college"].ToString();
                        txtDYearOfPassing.Text = dataTable.Rows[i]["year_passing"].ToString();
                        txtDTotalMrkObtn.Text = dataTable.Rows[i]["total_M_Obtn"].ToString();
                        txtDMarks.Text = dataTable.Rows[i]["division_marks"].ToString();
                    }
                    else if (dataTable.Rows[i]["exam_id"].ToString() == "4")
                    {
                        txtGSubject.Text = dataTable.Rows[i]["major_subject"].ToString();
                        txtGBoard.Text = dataTable.Rows[i]["board"].ToString();
                        txtGCollege.Text = dataTable.Rows[i]["college"].ToString();
                        txtGYearOfPassing.Text = dataTable.Rows[i]["year_passing"].ToString();
                        txtGTotalMrkObtn.Text = dataTable.Rows[i]["total_M_Obtn"].ToString();
                        txtGMarks.Text = dataTable.Rows[i]["division_marks"].ToString();
                    }
                }
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

        private void selectRank(string strRank)
        {
            switch (strRank)
            {
                case "1":
                    rbCET.Checked = true;
                    break;

                case "2":
                    rbDirect.Checked = true;
                    break;

            }
        }

        private void loadBatch()
        {
            BusinessLayer.Student.NonAICTERegistration NonAICTEReg = new BusinessLayer.Student.NonAICTERegistration();
            Entity.Student.NonAICTERegistration eNonAICTEReg = new Entity.Student.NonAICTERegistration();
            eNonAICTEReg.intMode = 1;
            eNonAICTEReg.CourseId = 0;
            DataTable dt = new DataTable();
            dt = NonAICTEReg.GetAllCommonSP(eNonAICTEReg);
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
            BusinessLayer.Student.NonAICTERegistration NonAICTEReg = new BusinessLayer.Student.NonAICTERegistration();
            Entity.Student.NonAICTERegistration eNonAICTEReg = new Entity.Student.NonAICTERegistration();
            eNonAICTEReg.intMode = 3;
            eNonAICTEReg.CourseId = 5;
            DataTable dt = new DataTable();
            dt = NonAICTEReg.GetAllCommonSP(eNonAICTEReg);

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

        protected void ClearControls()
        {
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
                    // Four month Entry 2019-05-04
                    //if (DateTime.Now < Convert.ToDateTime("2019-08-01")) // Sayantani : Commented on 05.08.2019 , Ref. Whats App from Biswajit
                    //{
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
                    //}//Command soon
                    //else {
                    //    Message.IsSuccess = false;
                    //    Message.Text = "Can't Registration. Four month is testing over..!";
                    //}
                }
                else
                {
                    //if (DateTime.Now < Convert.ToDateTime("2019-08-01"))
                    //{
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
                    //}//Command soon
                    //else
                    //{
                    //    Message.IsSuccess = false;
                    //    Message.Text = "Can't Registration. Four month is testing over..!";
                    //}
                }
            }
            Message.Show = true;

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
            if (ChkListDoc10.Checked)
            {
                ItemCount += 1;
            }
            return ItemCount;
        }

        private int updateData()
        {
            int RowsAffected = 0;
            return RowsAffected;
        }

        private int saveData(int callMode)
        {
            BusinessLayer.Student.NonAICTERegistration NonAICTEReg = new BusinessLayer.Student.NonAICTERegistration();
            Entity.Student.NonAICTERegistration eNonAICTEReg = new Entity.Student.NonAICTERegistration();

            eNonAICTEReg.intMode = callMode;
            eNonAICTEReg.studentID = int.Parse(strStudentID);
            eNonAICTEReg.intCompanyId = BusinessLayer.Common.Company.GetCompanyId("SIMT");
            eNonAICTEReg.login_id = HttpContext.Current.User.Identity.Name;
            eNonAICTEReg.CourseId = 5;
            eNonAICTEReg.strapplicationNumber = "";
            eNonAICTEReg.strBatch = ddlBatch.SelectedValue.Trim();
            eNonAICTEReg.strEnrollmentNo = txtEnrollmentNo.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strRank = findSelectedRank();
            eNonAICTEReg.strRankid = txtRankid.Text.Trim().Replace("'", "''");

            eNonAICTEReg.strRegistrationNo = txtRegistrationNo.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strUniversityRollNo = txtUniversityRollNo.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strMigrationInfo = txtMigrationInfo.Text.Trim().Replace("'", "''");

            //eBtechReg.strIsLateral = Convert.ToBoolean(RDBLateral.SelectedItem.Value);
            eNonAICTEReg.strIsHostelFacility = Convert.ToBoolean(RdbHostelFacility.SelectedItem.Value);
            // eBtechReg.strTFW = Convert.ToBoolean(DDLTFW.SelectedItem.Value);
            eNonAICTEReg.strIsReAdmission = Convert.ToBoolean(DDLReadmission.SelectedItem.Value);

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
            eNonAICTEReg.strStreamApplied = checkedIItem;
            //eBtechReg.strEce = chkEce.Checked == true ? "1" : "0";
            //eBtechReg.strEe = chkEe.Checked == true ? "2" : "0";
            //eBtechReg.strCse = chkCse.Checked == true ? "3" : "0";
            //eBtechReg.strAeie = chkAeie.Checked == true ? "4" : "0";
            //eBtechReg.strMe = chkMe.Checked == true ? "5" : "0";
            //eBtechReg.strCe = chkCe.Checked == true ? "6" : "0";


            //eBtechReg.strPhy = txtPhy.Text.Trim().Length > 0 ? txtPhy.Text.Trim() : "0";
            //eBtechReg.strChem = txtChem.Text.Trim().Length > 0 ? txtChem.Text.Trim() : "0";
            //eBtechReg.strMath = txtMath.Text.Trim().Length > 0 ? txtMath.Text.Trim() : "0";
            //eBtechReg.strEngg = txtEngg.Text.Trim().Length > 0 ? txtEngg.Text.Trim() : "0";
            //eBtechReg.strBios = txtBios.Text.Trim().Length > 0 ? txtBios.Text.Trim() : "0";


            eNonAICTEReg.strNameOfApplicant = txtNameOfApplicant.Text.Trim().Replace("'", "''");
            string[] Date = txtDob.Text.Trim().Split('/');
            eNonAICTEReg.strDob = (Date[1].Trim() + "/" + Date[0].Trim() + "/" + Date[2].Trim() + " 00:00:00").ToString();
            eNonAICTEReg.strFatherName = txtFatherName.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strFatherOccupation = txtFatherOccupation.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strMotherName = txtMotherName.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strMotherOccupation = txtMotherOccupation.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strGuardiansName = txtGuardiansName.Text.Trim().Replace("'", "''");


            eNonAICTEReg.SchoolID = Convert.ToInt32(ddlSchool.SelectedValue);
            eNonAICTEReg.strPAddress = txtPAddress.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strPState = Convert.ToInt32(ddlstate.SelectedValue);
            eNonAICTEReg.strPCity = Convert.ToInt32(ddlCity.SelectedValue);
            eNonAICTEReg.strPDistrict = Convert.ToInt32(ddlDistrict.SelectedValue);
            eNonAICTEReg.strPPin = Convert.ToString(txtpin.Text.Trim());
            eNonAICTEReg.strCAddress = txtCAddress.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strCState = Convert.ToInt32(ddlStatesearch.SelectedValue);
            eNonAICTEReg.strCDistrict = Convert.ToInt32(ddlDistrictSearch.SelectedValue);
            eNonAICTEReg.strCCity = Convert.ToInt32(ddlcitysearch.SelectedValue);
            eNonAICTEReg.strCPin = Convert.ToString(txtcPin.Text.Trim());


            //Added  By Biswajit

            eNonAICTEReg.strAdhar = txtAdhar.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strBankHName = txtBankAccountName.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strBankAcc = txtBankAcNo.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strBankIFSC = txtIFSCCode.Text.Trim().Replace("'", "''");

            //

            eNonAICTEReg.strPResidential = txtPResidential.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strSResidential = txtSResidential.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strPMobile = txtPMobile.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strSMobile = txtSMobile.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strEmail = txtEmail.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strHostalFacitity = findHostalSelected();
            eNonAICTEReg.strGender = findGender();
            eNonAICTEReg.strMarital = findMaritalStatus();
            eNonAICTEReg.strMotherTong = txtMotherTong.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strNationality = txtNationality.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strRealigion = txtRealigion.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strBengali = findBLanguagePower();
            eNonAICTEReg.strHindi = findHLanguagePower();
            eNonAICTEReg.strEnglish = findELanguagePower();
            eNonAICTEReg.strOther = findOLanguagePower();
            eNonAICTEReg.strCast = findCast();
            eNonAICTEReg.strMonthlyIncome = txtMonthlyIncome.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strRefferenceName1 = txtRefferenceName1.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strRefferenceAddress1 = txtRefferenceAddress1.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strRefferenceContactNumber1 = txtRefferenceContactNumber1.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strRefferenceName2 = txtRefferenceName2.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strRefferenceAddress2 = txtRefferenceAddress2.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strRefferenceContactNumber2 = txtRefferenceContactNumber2.Text.Trim().Replace("'", "''");

            eNonAICTEReg.strXSubject = txtXSubject.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strXBoard = txtXBoard.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strXCollege = txtXCollege.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strXYearOfPassing = txtXYearOfPassing.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strXTotalMrkObtn = txtXTotalMrkObtn.Text.Trim().Replace("'", "'");
            eNonAICTEReg.strXMarks = txtXMarks.Text.Trim().Replace("'", "''");

            eNonAICTEReg.strXiiSubject = txtXiiSubject.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strXiiBoard = txtXiiBoard.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strXiiCollege = txtXiiCollege.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strXiiYearOfPassing = txtXiiYearOfPassing.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strXiiTotalMrkObtn = txtXiiTotalMrkObtn.Text.Trim().Replace("'", "'");
            eNonAICTEReg.strXiiMarks = txtXiiMarks.Text.Trim().Replace("'", "''");

            eNonAICTEReg.strGSubject = txtGSubject.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strGBoard = txtGBoard.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strGCollege = txtGCollege.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strGYearOfPassing = txtGYearOfPassing.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strGTotalMrkObtn = txtGTotalMrkObtn.Text.Trim().Replace("'", "'");
            eNonAICTEReg.strGMarks = txtGMarks.Text.Trim().Replace("'", "''");

            eNonAICTEReg.strDSubject = txtDSubject.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strDBoard = txtDBoard.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strDCollege = txtDCollege.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strDYearOfPassing = txtDYearOfPassing.Text.Trim().Replace("'", "''");
            eNonAICTEReg.strDTotalMrkObtn = txtDTotalMrkObtn.Text.Trim().Replace("'", "'");
            eNonAICTEReg.strDMarks = txtDMarks.Text.Trim().Replace("'", "''");

            eNonAICTEReg.strDocList1 = chkListDoc1.Checked == true ? "1" : "0";
            eNonAICTEReg.strDocList2 = chkListDoc2.Checked == true ? "2" : "0";
            eNonAICTEReg.strDocList3 = chkListDoc3.Checked == true ? "3" : "0";
            eNonAICTEReg.strDocList4 = chkListDoc4.Checked == true ? "4" : "0";
            eNonAICTEReg.strDocList5 = chkListDoc5.Checked == true ? "5" : "0";
            eNonAICTEReg.strDocList6 = chkListDoc6.Checked == true ? "6" : "0";
            eNonAICTEReg.strDocList7 = chkListDoc7.Checked == true ? "7" : "0";
            eNonAICTEReg.strDocList8 = chkListDoc8.Checked == true ? "8" : "0";
            eNonAICTEReg.strDocList9 = chkListDoc9.Checked == true ? "9" : "0";
            eNonAICTEReg.strDocList9 = ChkListDoc10.Checked == true ? "10" : "0";
            eNonAICTEReg.BloodGroup = txtBloodGroup.Text.Trim();
            eNonAICTEReg.strPhoto = Photo;
            eNonAICTEReg.SubmittedBy = txtSubmittedBy.Text.Trim();

            int RowsAffected = NonAICTEReg.Save(eNonAICTEReg);
            if (RowsAffected > 0)
            {
                if (Photo != "")
                {
                    string ff = (Server.MapPath("") + "\\StudentPhoto\\" + eNonAICTEReg.studentID + Photo);
                    uploadImage.PostedFile.SaveAs(ff);
                }
            }
            btnPrint.Attributes.Add("onclick", "window.open('BTechRegistrationPrint.aspx?id=" + eNonAICTEReg.studentID + "'); return false;");
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

        private string findSelectedRank()
        {
            string strCheckItem = "0";
            if (rbCET.Checked == true)
                strCheckItem = "1";
            else if (rbDirect.Checked == true)
                strCheckItem = "2";

            return strCheckItem;
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
            //rbWbjee.Checked = true;
            rbHostalN.Checked = true;
            rbMale.Checked = true;
            rbSingle.Checked = true;
            rbGeneral.Checked = true;
            DDLReadmission.SelectedValue = "False";
            for (int i = 0; i < chkStream.Items.Count; i++)
            {
                chkStream.Items[i].Selected = false;
            }
            ddlBatch.Enabled = true;
            btnSave.Text = "Save";
            Message.Show = false;
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