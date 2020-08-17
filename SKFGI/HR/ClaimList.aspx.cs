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
    public partial class ClaimList : System.Web.UI.Page
    {
        Entity.HR.ExpenseClaim Claim;
        public int ExpenseClaimId
        {
            get { return Convert.ToInt32(ViewState["ExpenseClaimId"]); }
            set { ViewState["ExpenseClaimId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.CLAIM_APPROVE))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadClaimStatus();
                LoadRequestList();
                ClearControls();
            }
        }

        protected void LoadClaimStatus()
        {
            BusinessLayer.HR.ExpenseClaim ObjClaim = new BusinessLayer.HR.ExpenseClaim();
            DataTable dt = ObjClaim.GetAllClaimStatus();
            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["ClaimStatusId"] = "0";
                dr["ClaimStatus"] = "All";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                ddlStatus_Search.DataSource = dt;
                ddlStatus_Search.DataBind();

            }


            DataView dv = new DataView(ObjClaim.GetAllClaimStatus());
            dv.RowFilter = "ClaimStatus <> 'Paid'";
            DataTable dt1 = dv.ToTable();

            if (dt1 != null)
            {
                DataRow dr = dt1.NewRow();
                dr["ClaimStatusId"] = "0";
                dr["ClaimStatus"] = "--Select Status--";
                dt1.Rows.InsertAt(dr, 0);
                dt1.AcceptChanges();

                ddlStatus.DataSource = dt1;
                ddlStatus.DataBind();
            }

        }

        protected void ClearControls()
        {
            ExpenseClaimId = 0;
            Message.Show = false;
            txtEmpName.Text = "";
            txtEmpCode.Text = "";
            txtApproverComment.Text = "";
            ChkBillReceived.Checked = false;
            ddlStatus.SelectedValue = "0";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void LoadRequestList()
        {
            string FName = txtFName.Text.Trim();
            int ClaimStatusId = int.Parse(ddlStatus_Search.SelectedValue.Trim());
            int EmployeeId = int.Parse(HttpContext.Current.User.Identity.Name);

            string FromDate = txtFromDate.Text.Trim();
            if (FromDate.Length != 0)
            {
                string[] ArrFrom = FromDate.Split('/');
                FromDate = ArrFrom[1].Trim() + "/" + ArrFrom[0].Trim() + "/" + ArrFrom[2].Trim() + " 00:00:00";
            }
            string ToDate = txtToDate.Text.Trim();
            if (ToDate.Length != 0)
            {
                string[] ArrTo = ToDate.Split('/');
                ToDate = ArrTo[1].Trim() + "/" + ArrTo[0].Trim() + "/" + ArrTo[2].Trim() + " 23:59:59";
            }

            BusinessLayer.HR.ExpenseClaim ObjClaim = new BusinessLayer.HR.ExpenseClaim();
            DataTable dt = ObjClaim.GetAll(FName, ClaimStatusId, 0, EmployeeId, FromDate, ToDate);
            if (dt != null) 
            {
                dgvClaim.DataSource = dt;
                dgvClaim.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadRequestList();
        }

        protected void dgvClaim_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((Literal)e.Row.FindControl("ltrStatus")).Text != "Pending")
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                }
            }
        }

      

        protected void LoadClaimDetails()
        {
            BusinessLayer.HR.ExpenseClaim ObjClaim = new BusinessLayer.HR.ExpenseClaim();
            Claim = new Entity.HR.ExpenseClaim();
            Claim = ObjClaim.GetAllById(ExpenseClaimId);
            if (Claim != null)
            {
                ddlStatus.SelectedValue = Claim.ClaimStatusId.ToString();
                ChkBillReceived.Checked = Claim.BillReceived;
                txtApproverComment.Text = Claim.ApproverComment;

                BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
                Entity.Common.Employee Employee = new Entity.Common.Employee();
                Employee = ObjEmployee.GetAllById(Claim.EmployeeId);
                txtEmpName.Text = Employee.FirstName + " " + Employee.MiddleName + " " + Employee.LastName;
                txtEmpCode.Text = Employee.EmpCode;
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.HR.ExpenseClaim ObjClaim = new BusinessLayer.HR.ExpenseClaim();
            Claim = new Entity.HR.ExpenseClaim();
            Claim.ExpenseClaimId = ExpenseClaimId;
            Claim.ClaimStatusId = int.Parse(ddlStatus.SelectedValue.Trim());
            Claim.BillReceived = ChkBillReceived.Checked;
            Claim.ApproverComment = txtApproverComment.Text.Trim();

            /* Optional Parameter Passing */
            Claim.ExpenseDate = DateTime.Now;
            Claim.PaymentDate = null;


            ObjClaim.Save(Claim);
            ClearControls();
            LoadRequestList();
            Message.IsSuccess = true;
            Message.Text = "Claim Modified Successfully";
            Message.Show = true;
        }

        protected void dgvClaim_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ExpenseClaimId = int.Parse(dgvClaim.DataKeys[e.NewEditIndex].Value.ToString());
            LoadClaimDetails();
        }
    }
}
