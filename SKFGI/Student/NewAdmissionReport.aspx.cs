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
    public partial class NewAdmissionReport : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["SuperAdmin"] != null)
            {
                this.MasterPageFile = "../SuperAdmin.Master";
            }
            else
            {
                this.MasterPageFile = "../MasterAdmin.Master";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["UserId"] == null)&&(Session["SuperAdmin"]==null))
            {
                Response.Redirect("../Login.aspx");
            }

            if (!IsPostBack)
            {
                if ((!HttpContext.Current.User.IsInRole(Entity.Common.Utility.NEW_STUDENT_ADMISSION_REPORT))&&(Session["SuperAdmin"]==null))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadSchoolList();
                LoadStateList();
                LoadBatch();
                LoadCourse();
                btnDownload.Visible = false;
                txtFrom.Text = DateTime.Now.AddMonths(-1).ToString("dd MMM yyyy");
                txtTo.Text = DateTime.Now.ToString("dd MMM yyyy");
            }
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

        protected void LoadBatch()
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

        protected void LoadCourse()
        {
            BusinessLayer.Student.BTechRegistration ObjRegistration = new BusinessLayer.Student.BTechRegistration();
            Entity.Student.BTechRegistration Registration = new Entity.Student.BTechRegistration();
            Registration.intMode = 2;
            Registration.CourseId = 0; // Course Id is not required to fetch all courses
            DataTable dt = ObjRegistration.GetAllCommonSP(Registration);
            DataView dv = new DataView(dt);
            dv.RowFilter = "CompanyId=" + Session["CompanyId"].ToString() + "or CompanyId = 0";
            if (dv != null)
            {
                ddlCourse.DataSource = dv;
                ddlCourse.DataBind();
            }
            LoadStream();
        }

        protected void LoadStream()
        {
            int CourseId = int.Parse(ddlCourse.SelectedValue.Trim());
            BusinessLayer.Student.BTechRegistration ObjRegistration = new BusinessLayer.Student.BTechRegistration();
            Entity.Student.BTechRegistration Registration = new Entity.Student.BTechRegistration();
            Registration.intMode = 3;
            Registration.CourseId = CourseId;
            DataTable dt = ObjRegistration.GetAllCommonSP(Registration);
            if (dt != null)
            {
                ddlStream.DataSource = dt;
                ddlStream.DataBind();
            }
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStream();
        }

        private void searchStudent()
        {
            BusinessLayer.Student.SearchStudent searchS = new BusinessLayer.Student.SearchStudent();
            Entity.Student.SearchStudent esearchS = new Entity.Student.SearchStudent();

            esearchS.CourseId = int.Parse(ddlCourse.SelectedValue.Trim());
            esearchS.StreamId = int.Parse(ddlStream.SelectedValue.Trim());
            esearchS.batch_id = int.Parse(ddlBatch.SelectedValue.Trim());
            esearchS.SchoolId = int.Parse(ddlSchool.SelectedValue.Trim());

            esearchS.StateId = int.Parse(ddlstate.SelectedValue.Trim());
            esearchS.CityId = int.Parse(ddlCity.SelectedValue.Trim());
            esearchS.DistrictId = int.Parse(ddlDistrict.SelectedValue.Trim());
            if (txtFrom.Text.Trim() == "") { esearchS.FromDate = DateTime.MinValue; }
            else esearchS.FromDate = Convert.ToDateTime(txtFrom.Text.Trim());
            if (txtTo.Text.Trim() == "") { esearchS.ToDate = DateTime.MinValue; }
            else esearchS.ToDate = Convert.ToDateTime(txtTo.Text.Trim());

            DataTable dt = searchS.GetNewAdmissionReport(esearchS);
            if (dt != null)
            {
                dgvStudent.DataSource = dt;
                dgvStudent.DataBind();

                if (dt.Rows.Count > 0)
                    btnDownload.Visible = true;
                else
                    btnDownload.Visible = false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            searchStudent();
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string[] _header = new string[7];
            _header[0] = "Course: " + ((ddlCourse.SelectedIndex > 0) ? ddlCourse.SelectedItem.Text : "All");
            _header[1] = "Stream: " + ((ddlStream.SelectedIndex > 0) ? ddlStream.SelectedItem.Text : "All");
            _header[2] = "Batch: " + ((ddlBatch.SelectedIndex > 0) ? ddlBatch.SelectedItem.Text : "All");
            _header[3] = "State: " + ((ddlstate.SelectedIndex > 0) ? ddlstate.SelectedItem.Text : "All");
            _header[4] = "District: " + ((ddlDistrict.SelectedIndex > 0) ? ddlDistrict.SelectedItem.Text : "All");
            _header[5] = "City: " + ((ddlCity.SelectedIndex > 0) ? ddlCity.SelectedItem.Text : "All");
            _header[6] = "School: " + ((ddlSchool.SelectedIndex > 0) ? ddlSchool.SelectedItem.Text : "All"); 

            string[] _footer = new string[0];
            string file = "NEW_ADMISSION_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, dgvStudent, _footer, file);
        }

        protected void dgvStudent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            { 
                
            }
        }

        protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDistrictList();
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCityList();
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
    }
}
