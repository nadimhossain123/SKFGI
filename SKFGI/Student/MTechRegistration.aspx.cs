using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace CollegeERP.Student
{
    public partial class MTechRegistration : System.Web.UI.Page
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
                //if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_MTECH))
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
        protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDistrictList();
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCityList();
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
            BusinessLayer.Student.BTechRegistration ObjRegistration = new BusinessLayer.Student.BTechRegistration();
            Entity.Student.BTechRegistration Registration = new Entity.Student.BTechRegistration();
            Registration.intMode = 3;
            Registration.CourseId = 3;
            DataTable dt = ObjRegistration.GetAllCommonSP(Registration);
            if (dt != null)
            {
                dt.Rows.RemoveAt(0);
                dt.AcceptChanges();

                chkStream.DataSource = dt;
                chkStream.DataBind();
            }
        }

        private void loadStudentDetails(string studentID)
        {
            BusinessLayer.Student.MTechRegistration mtechReg = new BusinessLayer.Student.MTechRegistration();
            Entity.Student.MTechRegistration emtechReg = new Entity.Student.MTechRegistration();

            emtechReg.intMode = 5;
            emtechReg.CourseId = 3;
            emtechReg.studentID =int.Parse(studentID);
            DataSet ds = new DataSet();
            ds = mtechReg.GetStudentDetails(emtechReg);
            if (ds != null)
            {
                if (ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlBatch.SelectedValue = ds.Tables[0].Rows[0]["batch_id"].ToString();
                    //txtapplicationNumber.Text = ds.Tables[0].Rows[0]["appliation_no"].ToString();
                    selectRank(ds.Tables[0].Rows[0]["rank"].ToString());
                    txtRankid.Text = ds.Tables[0].Rows[0]["rankid"].ToString();
                    
                    //ChkHostelFacility.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsHostelFacility"].ToString());
                    RdbHostelFacility.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["IsHostelFacility"]);
                    txtRegistrationNo.Text = ds.Tables[0].Rows[0]["RegistrationNo"].ToString();
                    txtUniversityRollNo.Text = ds.Tables[0].Rows[0]["UniversityRollNo"].ToString();
                    txtMigrationInfo.Text = ds.Tables[0].Rows[0]["MigrationInfo"].ToString();

                    txtNameOfApplicant.Text = ds.Tables[0].Rows[0]["name"].ToString();
                    txtDob.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["dob"].ToString()).ToString("dd/MM/yyyy");

                    //Added By Biswajit
                    txtAdhar.Text = ds.Tables[0].Rows[0]["adhar_no"].ToString();
                    txtBankAccountName.Text = ds.Tables[0].Rows[0]["bankholder_name"].ToString();
                    txtBankAcNo.Text = ds.Tables[0].Rows[0]["bankacc_no"].ToString();
                    txtIFSCCode.Text = ds.Tables[0].Rows[0]["bankIFSC_code"].ToString();
                    //

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
                    
                    txtNationality.Text = ds.Tables[0].Rows[0]["nationality"].ToString();
                    txtRealigion.Text = ds.Tables[0].Rows[0]["realigion"].ToString();
                    
                    selectCast(ds.Tables[0].Rows[0]["caste_id"].ToString());
                    txtGateScore.Text = ds.Tables[0].Rows[0]["GateScore"].ToString();
                    txtAcademicYrExp.Text = ds.Tables[0].Rows[0]["AcademicYrExp"].ToString();
                    txtIndustryYrExp.Text = ds.Tables[0].Rows[0]["IndustryYrExp"].ToString();
                    txtSubmittedBy.Text = ds.Tables[0].Rows[0]["SubmittedBy"].ToString();

                    selectEducationBackground(ds.Tables[1]);

                    selectDocument(ds.Tables[2]);
                    selectStreamapplied(ds.Tables[3]);

                    btnSave.Text = "Update";
                    Message.Show = false;
                    btnPrint.Attributes.Add("onclick", "window.open('MTechRegistrationPrint.aspx?id=" + studentID + "'); return false;");
                    ddlBatch.Enabled = false;
                }
                else
                {
                    strStudentID = "0";
                    Response.Redirect(ResolveClientUrl("MTechRegistration.aspx"));
                }
            }
        }

        private void selectRank(string strRank)
        {
            switch (strRank)
            {
                case "1":
                    rbPgate.Checked = true;
                    break;
                case "2":
                    rbGate.Checked = true;
                    break;
                case "3":
                    rbDirect.Checked = true;
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
            rbPgate.Checked = true;
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
            BusinessLayer.Student.MTechRegistration mtechReg = new BusinessLayer.Student.MTechRegistration();
            Entity.Student.MTechRegistration emtechReg = new Entity.Student.MTechRegistration();

           // emtechReg.SchoolID = Convert.ToInt32(ddlSchool.SelectedValue);
            emtechReg.intMode = callMode;
            emtechReg.studentID =int.Parse(strStudentID);
            emtechReg.intCompanyId = BusinessLayer.Common.Company.GetCompanyId("Engineering");
            emtechReg.login_id = HttpContext.Current.User.Identity.Name;
            emtechReg.CourseId = 3;
            emtechReg.strapplicationNumber = "";
            emtechReg.strBatch = ddlBatch.SelectedValue.Trim();
            emtechReg.strRank = findSelectedRank();
            emtechReg.strRankid = txtRankid.Text.Trim().Replace("'", "''");
            emtechReg.strRegistrationNo = txtRegistrationNo.Text.Trim().Replace("'", "''");
            emtechReg.strUniversityRollNo = txtUniversityRollNo.Text.Trim().Replace("'", "''");
            emtechReg.strMigrationInfo = txtMigrationInfo.Text.Trim().Replace("'", "''");
            emtechReg.strIsHostelFacility = Convert.ToBoolean(RdbHostelFacility.SelectedItem.Value);

            emtechReg.strNameOfApplicant = txtNameOfApplicant.Text.Trim().Replace("'", "''");
            string[] Date = txtDob.Text.Trim().Split('/');
            emtechReg.strDob = (Date[1].Trim() + "/" + Date[0].Trim() + "/" + Date[2].Trim() + " 00:00:00").ToString();
            emtechReg.strFatherName = txtFatherName.Text.Trim().Replace("'", "''");
            emtechReg.strFatherOccupation = txtFatherOccupation.Text.Trim().Replace("'", "''");
            emtechReg.strMotherName = txtMotherName.Text.Trim().Replace("'", "''");
            emtechReg.strMotherOccupation = txtMotherOccupation.Text.Trim().Replace("'", "''");
            emtechReg.strGuardiansName = txtGuardiansName.Text.Trim().Replace("'", "''");


            //Added  By Biswajit

            emtechReg.strAdhar = txtAdhar.Text.Trim().Replace("'", "''");
            emtechReg.strBankHName = txtBankAccountName.Text.Trim().Replace("'", "''");
            emtechReg.strBankAcc = txtBankAcNo.Text.Trim().Replace("'", "''");
            emtechReg.strBankIFSC = txtIFSCCode.Text.Trim().Replace("'", "''");

            //

            emtechReg.SchoolID = Convert.ToInt32(ddlSchool.SelectedValue);
            emtechReg.strPAddress = txtPAddress.Text.Trim().Replace("'", "''");
            emtechReg.strPState = Convert.ToInt32(ddlstate.SelectedValue);
            emtechReg.strPCity = Convert.ToInt32(ddlCity.SelectedValue);
            emtechReg.strPDistrict = Convert.ToInt32(ddlDistrict.SelectedValue);
            emtechReg.strPPin = Convert.ToString(txtpin.Text.Trim());
            emtechReg.strCAddress = txtCAddress.Text.Trim().Replace("'", "''");
            emtechReg.strCState = Convert.ToInt32(ddlStatesearch.SelectedValue);
            emtechReg.strCDistrict = Convert.ToInt32(ddlDistrictSearch.SelectedValue);
            emtechReg.strCCity = Convert.ToInt32(ddlcitysearch.SelectedValue);
            emtechReg.strCPin = Convert.ToString(txtcPin.Text.Trim());

            emtechReg.strPResidential = txtPResidential.Text.Trim().Replace("'", "''");
            emtechReg.strSResidential = txtSResidential.Text.Trim().Replace("'", "''");
            emtechReg.strPMobile = txtPMobile.Text.Trim().Replace("'", "''");
            emtechReg.strSMobile = txtSMobile.Text.Trim().Replace("'", "''");
            emtechReg.strEmail = txtEmail.Text.Trim().Replace("'", "''");
            emtechReg.strHostalFacitity = findHostalSelected();
            emtechReg.strGender = findGender();
            emtechReg.strMarital = findMaritalStatus();

            emtechReg.strNationality = txtNationality.Text.Trim().Replace("'", "''");
            emtechReg.strRealigion = txtRealigion.Text.Trim().Replace("'", "''");
            emtechReg.strCast = findCast();

            emtechReg.strGateScore = txtGateScore.Text.Trim();
            emtechReg.strAcademicYrExp = (txtAcademicYrExp.Text.Trim().Length > 0) ? txtAcademicYrExp.Text.Trim() : "0";
            emtechReg.strIndustryYrExp = (txtIndustryYrExp.Text.Trim().Length > 0) ? txtIndustryYrExp.Text.Trim() : "0"; 

            emtechReg.strXSubject = txtXSubject.Text.Trim().Replace("'", "''");
            emtechReg.strXBoard = txtXBoard.Text.Trim().Replace("'", "''");
            emtechReg.strXCollege = txtXCollege.Text.Trim().Replace("'", "''");
            emtechReg.strXYearOfPassing = txtXYearOfPassing.Text.Trim().Replace("'", "''");
            emtechReg.strXMarks = txtXMarks.Text.Trim().Replace("'", "''");

            emtechReg.strXiiSubject = txtXiiSubject.Text.Trim().Replace("'", "''");
            emtechReg.strXiiBoard = txtXiiBoard.Text.Trim().Replace("'", "''");
            emtechReg.strXiiCollege = txtXiiCollege.Text.Trim().Replace("'", "''");
            emtechReg.strXiiYearOfPassing = txtXiiYearOfPassing.Text.Trim().Replace("'", "''");
            emtechReg.strXiiMarks = txtXiiMarks.Text.Trim().Replace("'", "''");

            emtechReg.strGSubject = txtGSubject.Text.Trim().Replace("'", "''");
            emtechReg.strGBoard = txtGBoard.Text.Trim().Replace("'", "''");
            emtechReg.strGCollege = txtGCollege.Text.Trim().Replace("'", "''");
            emtechReg.strGYearOfPassing = txtGYearOfPassing.Text.Trim().Replace("'", "''");
            emtechReg.strGMarks = txtGMarks.Text.Trim().Replace("'", "''");

            emtechReg.strDSubject = txtDSubject.Text.Trim().Replace("'", "''");
            emtechReg.strDBoard = txtDBoard.Text.Trim().Replace("'", "''");
            emtechReg.strDCollege = txtDCollege.Text.Trim().Replace("'", "''");
            emtechReg.strDYearOfPassing = txtDYearOfPassing.Text.Trim().Replace("'", "''");
            emtechReg.strDMarks = txtDMarks.Text.Trim().Replace("'", "''");

            emtechReg.strDocList1 = chkListDoc1.Checked == true ? "1" : "0";
            emtechReg.strDocList2 = chkListDoc2.Checked == true ? "2" : "0";
            emtechReg.strDocList3 = chkListDoc3.Checked == true ? "3" : "0";
            emtechReg.strDocList4 = chkListDoc4.Checked == true ? "4" : "0";
            emtechReg.strDocList5 = chkListDoc5.Checked == true ? "5" : "0";
            emtechReg.strDocList6 = chkListDoc6.Checked == true ? "6" : "0";
            emtechReg.strDocList7 = chkListDoc7.Checked == true ? "7" : "0";
            emtechReg.strDocList8 = chkListDoc8.Checked == true ? "8" : "0";
            
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
            emtechReg.strStreamApplied = checkedIItem;
            emtechReg.strPhoto = Photo;
            emtechReg.SubmittedBy = txtSubmittedBy.Text.Trim();

            int RowsAffected = mtechReg.Save(emtechReg);
            if (RowsAffected > 0)
            {
                if (Photo != "")
                {
                    string ff = (Server.MapPath("") + "\\StudentPhoto\\" + emtechReg.studentID + Photo);
                    uploadImage.PostedFile.SaveAs(ff);
                }
            }
            btnPrint.Attributes.Add("onclick", "window.open('MTechRegistrationPrint.aspx?id=" + emtechReg.studentID + "'); return false;");
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
            if (rbPgate.Checked == true)
                strCheckItem = "1";
            else if (rbGate.Checked == true)
                strCheckItem = "2";
            else if (rbDirect.Checked == true)
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
