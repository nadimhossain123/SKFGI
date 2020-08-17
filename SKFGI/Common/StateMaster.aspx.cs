using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CollegeERP.Common
{
    public partial class StateMaster : System.Web.UI.Page
    {
        public int StateId
        {
            get { return Convert.ToInt32(ViewState["StateId"]); }
            set { ViewState["StateId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.STATE_MASTER))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                ClearControls();
                LoadStateList();

            }
        }
        protected void LoadStateList()
        {
            Entity.Common.StateDistrictCity StateDistrictCity_Entity = new Entity.Common.StateDistrictCity();
            BusinessLayer.Common.StateDistrictCity StateDistrictCity_BAL = new BusinessLayer.Common.StateDistrictCity();
            StateDistrictCity_Entity.StateID = 0;
            StateDistrictCity_Entity.State = txtstatesearch.Text;
            DataTable DT= StateDistrictCity_BAL.GetAll(StateDistrictCity_Entity);
            if (DT != null)
            {
                dgvState.DataSource = DT;
                dgvState.DataBind();
            }
        }
        protected void ClearControls()
        {
            StateId = 0;
            btnSave.Text = "Save";
            Message.Show = false;
            txtState.Text = "";           
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            Entity.Common.StateDistrictCity StateDistrictCity_Entity = new Entity.Common.StateDistrictCity();
            BusinessLayer.Common.StateDistrictCity StateDistrictCity_BAL = new BusinessLayer.Common.StateDistrictCity();
            StateDistrictCity_Entity.StateID = StateId;
            StateDistrictCity_Entity.State = txtState.Text;
            int RowAffected = StateDistrictCity_BAL.Save(StateDistrictCity_Entity);
            if (RowAffected != -1)
            {
                ClearControls();
                LoadStateList();
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
            LoadStateList();
        }

        protected void dgvState_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvState.PageIndex = e.NewPageIndex;
            LoadStateList();
        }

        protected void dgvState_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Entity.Common.StateDistrictCity StateDistrictCity_Entity = new Entity.Common.StateDistrictCity();
            BusinessLayer.Common.StateDistrictCity StateDistrictCity_BAL = new BusinessLayer.Common.StateDistrictCity();
            StateDistrictCity_Entity.StateID = Convert.ToInt32(e.CommandArgument);
            StateDistrictCity_Entity.State = "";
            if (e.CommandName == "Ed")
            {
                DataTable DT = StateDistrictCity_BAL.GetAll(StateDistrictCity_Entity);
                if (DT.Rows.Count!=0)
                {
                    StateId = Convert.ToInt32(DT.Rows[0]["StateID"]);
                    txtState.Text = Convert.ToString(DT.Rows[0]["State"]);
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LoadStateList();
        }
    }
}