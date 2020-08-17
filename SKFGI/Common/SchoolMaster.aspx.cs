using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CollegeERP.Common
{
    public partial class SchoolMaster : System.Web.UI.Page
    {
        public int SchoolId
        {
            get { return Convert.ToInt32(ViewState["SchoolId"]); }
            set { ViewState["SchoolId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.SCHOOL_MASTER))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadStateList();
                LoadStateListForSearch();
                ClearControls();
                LoadSchoolList();

            }
        }

        protected void LoadSchoolList()
        {
            Entity.Common.StateDistrictCity StateDistrictCity_Entity = new Entity.Common.StateDistrictCity();
            BusinessLayer.Common.StateDistrictCity StateDistrictCity_BAL = new BusinessLayer.Common.StateDistrictCity();
            StateDistrictCity_Entity.SchoolID = 0;
            StateDistrictCity_Entity.CityID = Convert.ToInt32(ddlcitysearch.SelectedValue);   
            StateDistrictCity_Entity.StateID = Convert.ToInt32(ddlStatesearch.SelectedValue);
            StateDistrictCity_Entity.DistrictID = Convert.ToInt32(ddlDistrictSearch.SelectedValue);
            StateDistrictCity_Entity.School = txtSchoolsearch.Text;  
            DataTable DT = StateDistrictCity_BAL.GetAllSchool(StateDistrictCity_Entity);
            if (DT != null)
            {
                dgvState.DataSource = DT;
                dgvState.DataBind();
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
        protected void LoadCityListSearch()
        {
            Entity.Common.StateDistrictCity StateDistrictCity_Entity = new Entity.Common.StateDistrictCity();
            BusinessLayer.Common.StateDistrictCity StateDistrictCity_BAL = new BusinessLayer.Common.StateDistrictCity();
            StateDistrictCity_Entity.CityID = 0;
            StateDistrictCity_Entity.StateID = Convert.ToInt32(ddlStatesearch.SelectedValue);
            StateDistrictCity_Entity.DistrictID = Convert.ToInt32(ddlDistrictSearch.SelectedValue);
            StateDistrictCity_Entity.City = "";
            DataTable DT = StateDistrictCity_BAL.GetAllCity(StateDistrictCity_Entity);
            if (DT !=null)
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
        protected void ClearControls()
        {
            SchoolId = 0;
            btnSave.Text = "Save";
            Message.Show = false;
            ddlstate.SelectedIndex = 0;
            ddlStatesearch.SelectedIndex = 0;
            txtaddress.Text = "";
            txtpin.Text = "";
            txtSchool.Text = "";
            txtSchoolsearch.Text = "";
            ddlDistrict.SelectedIndex = 0;
            ddlDistrictSearch.SelectedIndex = 0;
            ddlCity.SelectedIndex = 0;
            ddlcitysearch.SelectedIndex = 0;
        }

        protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDistrictList();
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCityList();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Entity.Common.StateDistrictCity StateDistrictCity_Entity = new Entity.Common.StateDistrictCity();
            BusinessLayer.Common.StateDistrictCity StateDistrictCity_BAL = new BusinessLayer.Common.StateDistrictCity();
            StateDistrictCity_Entity.StateID = Convert.ToInt32(ddlstate.SelectedValue);
            StateDistrictCity_Entity.CityID = Convert.ToInt32(ddlCity.SelectedValue); 
            StateDistrictCity_Entity.DistrictID = Convert.ToInt32(ddlDistrict.SelectedValue);
            StateDistrictCity_Entity.SchoolID = SchoolId;
            StateDistrictCity_Entity.School = txtSchool.Text;
            StateDistrictCity_Entity.Address = txtaddress.Text;
            StateDistrictCity_Entity.Pin =Convert.ToInt32( txtpin.Text);
            int RowAffected = StateDistrictCity_BAL.SaveSchool(StateDistrictCity_Entity);
            if (RowAffected != -1)
            {
                ClearControls();
                LoadSchoolList();
                Message.IsSuccess = true;
                Message.Text = "School Saved Successfully";
                Message.Show = true;
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate School Name Is Not Allowed";
                Message.Show = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void ddlStatesearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDistrictListForSearch();
        }

        protected void ddlDistrictSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCityListSearch();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LoadSchoolList();
        }

        protected void dgvState_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Entity.Common.StateDistrictCity StateDistrictCity_Entity = new Entity.Common.StateDistrictCity();
            BusinessLayer.Common.StateDistrictCity StateDistrictCity_BAL = new BusinessLayer.Common.StateDistrictCity();
            StateDistrictCity_Entity.SchoolID = Convert.ToInt32(e.CommandArgument);
            StateDistrictCity_Entity.School = "";
            StateDistrictCity_Entity.StateID = 0;
            StateDistrictCity_Entity.DistrictID = 0;
            StateDistrictCity_Entity.CityID = 0;
            if (e.CommandName == "Ed")
            {
                DataTable DT = StateDistrictCity_BAL.GetAllSchool(StateDistrictCity_Entity);
                if (DT.Rows.Count != 0)
                {
                    LoadStateList();
                    SchoolId = Convert.ToInt32(DT.Rows[0]["SchoolID"]);
                    txtSchool.Text = Convert.ToString(DT.Rows[0]["School"]);
                    ddlstate.SelectedValue = Convert.ToString(DT.Rows[0]["StateID"]);
                    LoadDistrictList();
                    ddlDistrict.SelectedValue = Convert.ToString(DT.Rows[0]["DistrictId"]);
                    LoadCityList();
                    ddlCity.SelectedValue = Convert.ToString(DT.Rows[0]["CityID"]);
                    txtpin.Text = Convert.ToString(DT.Rows[0]["PIN"]);
                    txtaddress.Text = Convert.ToString(DT.Rows[0]["Address"]);
                }
            }
        }

        protected void dgvState_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvState.PageIndex = e.NewPageIndex;
            LoadSchoolList();
        }
    }
}