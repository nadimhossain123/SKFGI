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
using BusinessLayer.Accounts;

namespace CollegeERP.Student
{
    public partial class FeesHeadMaster : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        ListItem li = new ListItem("Select", "0");

        public int id
        {
            get { return Convert.ToInt32(ViewState["id"]); }
            set { ViewState["id"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.FEES_HEAD_MASTER))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadLedger();
                ClearControls();
                LoadFeesHeadList();

            }
        }

        protected void LoadLedger()
        {
            //Load Customer Type Ledger For Tagging
            string strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchId"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();

            DataView dv = new DataView(gf.GetDropDownColumnsBySP("spSelect_MstGeneralLedgerFull", strParams));
            dv.RowFilter = "LedgerType IN ('CUST')";
            if (dv != null)
            {
                ddlAssestLedger.DataSource = dv;
                ddlAssestLedger.DataBind();

                ddlIncomeLedger.DataSource = dv;
                ddlIncomeLedger.DataBind();
            }

            ddlAssestLedger.Items.Insert(0, li);
            ddlIncomeLedger.Items.Insert(0, li);
        }

        protected void ClearControls()
        {
            id = 0;
            btnSave.Text = "Save";
            Message.Show = false;

            txtFeesHead.Text = "";
            rbtnListHeadType.SelectedValue = "SEM";
            ddlAssestLedger.SelectedIndex = 0;
            ddlIncomeLedger.SelectedIndex = 0;
            ChkIsRefundable.Checked = false;
            ChkIsOneTimeApplicable.Checked = false;
        }

        protected void LoadFeesHeadList()
        {
            BusinessLayer.Student.StreamGroup ObjFees = new BusinessLayer.Student.StreamGroup();
            DataTable dt = ObjFees.GetAllFeesHead();
            if (dt != null)
            {
                dgvFeesHead.DataSource = dt;
                dgvFeesHead.DataBind();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void dgvFeesHead_RowEditing(object sender, GridViewEditEventArgs e)
        {
            id = Convert.ToInt32(dgvFeesHead.DataKeys[e.NewEditIndex].Value);
            BusinessLayer.Student.StreamGroup ObjFees = new BusinessLayer.Student.StreamGroup();
            DataTable dt = ObjFees.GetAllFeesHeadById(id);
            if (dt.Rows.Count > 0)
            {
                txtFeesHead.Text = dt.Rows[0]["fees"].ToString();
                rbtnListHeadType.SelectedValue = dt.Rows[0]["FeesHeadType"].ToString();
                ddlAssestLedger.SelectedValue = dt.Rows[0]["AssestLedgerID_FK"].ToString();
                ddlIncomeLedger.SelectedValue = dt.Rows[0]["IncomeLedgerID_FK"].ToString();
                ChkIsRefundable.Checked = Convert.ToBoolean(dt.Rows[0]["IsRefundable"].ToString());
                ChkIsOneTimeApplicable.Checked = Convert.ToBoolean(dt.Rows[0]["IsOneTimeApplicable"].ToString());
            }
            btnSave.Text = "Update";
            Message.Show = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Student.StreamGroup ObjFees = new BusinessLayer.Student.StreamGroup();
            Entity.Student.StreamGroup Fees = new Entity.Student.StreamGroup();

            Fees.feesID = id;
            if (id == 0)
            {
                Fees.intMode = 9;
            }
            else
            {
                Fees.intMode = 13;
            }

            Fees.Fees = txtFeesHead.Text.Trim();
            Fees.FeesHeadType = rbtnListHeadType.SelectedValue.Trim();
            Fees.AssestLedgerID_FK = Convert.ToInt32(ddlAssestLedger.SelectedValue.Trim());
            Fees.IncomeLedgerID_FK = Convert.ToInt32(ddlIncomeLedger.SelectedValue.Trim());
            Fees.IsRefundable = ChkIsRefundable.Checked;
            Fees.IsOneTimeApplicable = ChkIsOneTimeApplicable.Checked;
            int RowsAffected = ObjFees.SaveFeesHead(Fees);
            if (RowsAffected != -1)
            {
                ClearControls();
                LoadFeesHeadList();
                Message.IsSuccess = true;
                Message.Text = "Fees Head Saved/Updated Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate Head Name Is Not Allowed";
            }
            Message.Show = true;
        }

        
    }
}
