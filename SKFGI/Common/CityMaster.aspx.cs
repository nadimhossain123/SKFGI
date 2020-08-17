using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CollegeERP.Common
{
    public partial class CityMaster : System.Web.UI.Page
    {
        public int CityId
        {
            get { return Convert.ToInt32(ViewState["CityId"]); }
            set { ViewState["CityId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.CITY_MASTER))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadStateList();
                LoadStateListForSearch();
             
                ClearControls();
                LoadCityList();

            }
        }
        protected void ClearControls()
        {
            CityId = 0;
            btnSave.Text = "Save";
            Message.Show = false;
            ddlstate.SelectedIndex = 0;
            ddlStatesearch.SelectedIndex = 0;
            txtCity.Text = "";
            txtCitysearch.Text = "";
            ddlDistrict.SelectedIndex = 0;
            ddlDistrictSearch.SelectedIndex = 0;
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

        }
        protected void LoadCityList()
        {
            Entity.Common.StateDistrictCity StateDistrictCity_Entity = new Entity.Common.StateDistrictCity();
            BusinessLayer.Common.StateDistrictCity StateDistrictCity_BAL = new BusinessLayer.Common.StateDistrictCity();
            StateDistrictCity_Entity.CityID = 0;
            StateDistrictCity_Entity.StateID = Convert.ToInt32(ddlStatesearch.SelectedValue);
            StateDistrictCity_Entity.DistrictID = Convert.ToInt32(ddlDistrictSearch.SelectedValue);  
            StateDistrictCity_Entity.City = txtCitysearch.Text; 
            DataTable DT = StateDistrictCity_BAL.GetAllCity(StateDistrictCity_Entity);
            if (DT != null)
            {
                dgvState.DataSource = DT;
                dgvState.DataBind();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Entity.Common.StateDistrictCity StateDistrictCity_Entity = new Entity.Common.StateDistrictCity();
            BusinessLayer.Common.StateDistrictCity StateDistrictCity_BAL = new BusinessLayer.Common.StateDistrictCity();
            StateDistrictCity_Entity.StateID = Convert.ToInt32(ddlstate.SelectedValue);
            StateDistrictCity_Entity.City = txtCity.Text;
            StateDistrictCity_Entity.DistrictID = Convert.ToInt32(ddlDistrict.SelectedValue);
            StateDistrictCity_Entity.CityID = CityId;
            int RowAffected = StateDistrictCity_BAL.SaveCity(StateDistrictCity_Entity);
            if (RowAffected != -1)
            {
                ClearControls();
                LoadCityList();
                Message.IsSuccess = true;
                Message.Text = "City Saved Successfully";
                Message.Show = true;
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate City Name Is Not Allowed";
                Message.Show = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
            LoadCityList();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LoadCityList();
        }

        protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDistrictList();
        }

        protected void ddlStatesearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDistrictListForSearch();
        }

        protected void dgvState_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Entity.Common.StateDistrictCity StateDistrictCity_Entity = new Entity.Common.StateDistrictCity();
            BusinessLayer.Common.StateDistrictCity StateDistrictCity_BAL = new BusinessLayer.Common.StateDistrictCity();
            StateDistrictCity_Entity.CityID = Convert.ToInt32(e.CommandArgument);
            StateDistrictCity_Entity.City = "";
            StateDistrictCity_Entity.StateID = 0;
            StateDistrictCity_Entity.DistrictID = 0;
            if (e.CommandName == "Ed")
            {
                DataTable DT = StateDistrictCity_BAL.GetAllCity(StateDistrictCity_Entity);
                if (DT.Rows.Count != 0)
                {
                    LoadStateList();
                    ddlstate.SelectedValue = Convert.ToString(DT.Rows[0]["StateID"]);
                    LoadDistrictList();
                    ddlDistrict.SelectedValue = Convert.ToString(DT.Rows[0]["DistrictId"]);
                    CityId = Convert.ToInt32(DT.Rows[0]["CityID"]);
                    txtCity.Text = Convert.ToString(DT.Rows[0]["City"]);
                   
                   
                }
            }
        }

        protected void dgvState_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvState.PageIndex = e.NewPageIndex;
            LoadCityList();
        }
    }
}