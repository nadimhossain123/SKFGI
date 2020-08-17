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

namespace CollegeERP.HR
{
    public partial class LeaveSetting : System.Web.UI.Page
    {
        public int LeaveTypeId
        {
            get { return Convert.ToInt32(ViewState["LeaveTypeId"]); }
            set { ViewState["LeaveTypeId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.LEAVE_SETTING))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadLeaveTypeList();
                ClearControls();

            }
        }

        protected void LoadLeaveTypeList()
        {
            BusinessLayer.HR.LeaveType ObjLeaveType = new BusinessLayer.HR.LeaveType();
            DataTable dt = ObjLeaveType.GetAll();
            if (dt != null)
            {
                dgvLeaveType.DataSource = dt;
                dgvLeaveType.DataBind();
            }
        }

        protected void LoadLeaveTypeDetails()
        {
            BusinessLayer.HR.LeaveType ObjLeaveType = new BusinessLayer.HR.LeaveType();
            Entity.HR.LeaveType LeaveType = new Entity.HR.LeaveType();
            LeaveType = ObjLeaveType.GetAllById(LeaveTypeId);
            if (LeaveType != null)
            {
                txtLeaveType.Text = LeaveType.LeaveTypeName;
                txtLeaveType.Enabled = true;
                txtDescription.Text = LeaveType.Description;
                txtLeavePerMonth.Text = (LeaveType.LeavePerMonth == null) ? "" : LeaveType.LeavePerMonth.Value.ToString("#0.00");
                txtLeavePerYear.Text = (LeaveType.LeavePerYear == null) ? "" : LeaveType.LeavePerYear.Value.ToString();
                ChkIsCarryForwarded.Checked = LeaveType.IsCarryForwarded;
                ChkIsEncashable.Checked = LeaveType.IsEncashable;
                txtMaxCarryFwdLimit.Text = (LeaveType.MaxCarryFwdLimit == null) ? "" : LeaveType.MaxCarryFwdLimit.Value.ToString();
                ChkIsPaid.Checked = LeaveType.IsPaid;


                Message.Show = false;
                btnSave.Text = "Update";
            }
        }

        protected void ClearControls()
        {
            LeaveTypeId = 0;
            txtLeaveType.Text = "";
            txtLeaveType.Enabled = false;
            txtDescription.Text = "";
            txtLeavePerMonth.Text = "";
            txtLeavePerYear.Text = "";
            ChkIsCarryForwarded.Checked = false;
            ChkIsEncashable.Checked =false;
            txtMaxCarryFwdLimit.Text = "";
            ChkIsPaid.Checked = false;


            Message.Show = false;
            btnSave.Text = "Update";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.HR.LeaveType ObjLeaveType = new BusinessLayer.HR.LeaveType();
            Entity.HR.LeaveType LeaveType = new Entity.HR.LeaveType();
            LeaveType.LeaveTypeId = LeaveTypeId;
            LeaveType.LeaveTypeName = txtLeaveType.Text.Trim();
            LeaveType.Description = txtDescription.Text.Trim();

            if (txtLeavePerMonth.Text.Trim().Length == 0)
            {
                LeaveType.LeavePerMonth=null;
            }
            else { LeaveType.LeavePerMonth = decimal.Parse(txtLeavePerMonth.Text.Trim()); }

            
            if (txtLeavePerYear.Text.Trim().Length == 0)
            {
                LeaveType.LeavePerYear = null;
            }
            else { LeaveType.LeavePerYear = int.Parse(txtLeavePerYear.Text.Trim()); }

            
            LeaveType.IsCarryForwarded = ChkIsCarryForwarded.Checked;
            LeaveType.IsEncashable = ChkIsEncashable.Checked;

            if (txtMaxCarryFwdLimit.Text.Trim().Length == 0)
            {
                LeaveType.MaxCarryFwdLimit = null;
            }
            else { LeaveType.MaxCarryFwdLimit = int.Parse(txtMaxCarryFwdLimit.Text.Trim()); }

            LeaveType.IsPaid = ChkIsPaid.Checked;

            int RowsAffected = ObjLeaveType.Save(LeaveType);
            if (RowsAffected != -1)
            {
                ClearControls();
                LoadLeaveTypeList();
                Message.IsSuccess = true;
                Message.Text = "Leave Setting Saved/Updated Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate Leave Type Name Is Not Allowed";
            }
            Message.Show = true;
        }

        protected void dgvLeaveType_RowEditing(object sender, GridViewEditEventArgs e)
        {
            LeaveTypeId = int.Parse(dgvLeaveType.DataKeys[e.NewEditIndex].Value.ToString());
            LoadLeaveTypeDetails();
        }
    }
}
