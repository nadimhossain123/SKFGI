using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CollegeERP.Common
{
    public partial class DistrictMaster : System.Web.UI.Page
    {
        public int DistrictId
        {
            get { return Convert.ToInt32(ViewState["DistrictId"]); }
            set { ViewState["DistrictId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.DISTRICT_MASTER))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadStateList();
                ClearControls();
                LoadDistrictList();

            }
        }
        protected void LoadStateList()
        {
            Entity.Common.StateDistrictCity StateDistrictCity_Entity = new Entity.Common.StateDistrictCity();
            BusinessLayer.Common.StateDistrictCity StateDistrictCity_BAL = new BusinessLayer.Common.StateDistrictCity();
            StateDistrictCity_Entity.StateID = 0;
            StateDistrictCity_Entity.State ="";
            DataTable DT = StateDistrictCity_BAL.GetAll(StateDistrictCity_Entity);
            if (DT != null)
            {
                ddlstate.DataSource = DT;
                ddlstate.DataTextField = "State";
                ddlstate.DataValueField = "StateID";
                ddlstate.DataBind();

                ddlstatesearch.DataSource = DT;
                ddlstatesearch.DataTextField = "State";
                ddlstatesearch.DataValueField = "StateID";
                ddlstatesearch.DataBind();
            }
            ListItem lst;
            lst = new ListItem("--Select--", "0");
            ddlstate.Items.Insert(0, lst);
            ddlstatesearch.Items.Insert(0, lst);
        }
        protected void ClearControls()
        {
            DistrictId = 0;
            btnSave.Text = "Save";
            Message.Show = false;
            ddlstate.SelectedIndex = 0;
            ddlstatesearch.SelectedIndex = 0;
            txtDistrict.Text = "";
            txtdistrictsearch.Text = "";
        }
        protected void LoadDistrictList()
        {
            Entity.Common.StateDistrictCity StateDistrictCity_Entity = new Entity.Common.StateDistrictCity();
            BusinessLayer.Common.StateDistrictCity StateDistrictCity_BAL = new BusinessLayer.Common.StateDistrictCity();
            StateDistrictCity_Entity.DistrictID = 0;
            StateDistrictCity_Entity.StateID = Convert.ToInt32(ddlstatesearch.SelectedValue);
            StateDistrictCity_Entity.District = txtdistrictsearch.Text;
            DataTable DT = StateDistrictCity_BAL.GetAllDistrict(StateDistrictCity_Entity);
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
            StateDistrictCity_Entity.District = txtDistrict.Text;
            StateDistrictCity_Entity.DistrictID = DistrictId;
            int RowAffected = StateDistrictCity_BAL.SaveDistrict(StateDistrictCity_Entity);
            if (RowAffected != -1)
            {
                ClearControls();
                LoadDistrictList();
                Message.IsSuccess = true;
                Message.Text = "State Saved Successfully";
                Message.Show = true;
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate State Name Is Not Allowed";
                Message.Show = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
            LoadDistrictList();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LoadDistrictList();
        }

        protected void dgvState_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Entity.Common.StateDistrictCity StateDistrictCity_Entity = new Entity.Common.StateDistrictCity();
            BusinessLayer.Common.StateDistrictCity StateDistrictCity_BAL = new BusinessLayer.Common.StateDistrictCity();
            StateDistrictCity_Entity.DistrictID = Convert.ToInt32(e.CommandArgument);
            StateDistrictCity_Entity.District = "";
            StateDistrictCity_Entity.StateID = 0;
            if (e.CommandName == "Ed")
            {
                DataTable DT = StateDistrictCity_BAL.GetAllDistrict(StateDistrictCity_Entity);
                if (DT.Rows.Count != 0)
                {
                    LoadStateList();
                    DistrictId = Convert.ToInt32(DT.Rows[0]["DistrictId"]);
                    txtDistrict.Text = Convert.ToString(DT.Rows[0]["District"]);
                    ddlstate.SelectedValue = Convert.ToString(DT.Rows[0]["StateID"]);
                }
            }
        }

        protected void dgvState_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvState.PageIndex = e.NewPageIndex;
            LoadDistrictList();
        }
    }
}