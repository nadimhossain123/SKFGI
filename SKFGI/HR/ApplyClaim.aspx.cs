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
    public partial class ApplyClaim : System.Web.UI.Page
    {
        public int EmployeeId
        {
            get { return Convert.ToInt32(ViewState["EmployeeId"]); }
            set { ViewState["EmployeeId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.CLAIM_APPLY))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }

                EmployeeId = int.Parse(HttpContext.Current.User.Identity.Name);
                LoadClaimStatus();
                LoadClaimHistory();
                LoadExpenseType();
                ClearControl();

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

                ddlStatus.DataSource = dt;
                ddlStatus.DataBind();

            }
        }

        protected void LoadClaimHistory()
        {
            BusinessLayer.HR.ExpenseClaim ObjClaim = new BusinessLayer.HR.ExpenseClaim();
            int ClaimStatusId = int.Parse(ddlStatus.SelectedValue.Trim());
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

            DataTable dt = ObjClaim.GetAll("", ClaimStatusId, EmployeeId, 0, FromDate, ToDate);// ApproverId =0 for selecting all claim of me regardless of the claim Manager
            
            if (dt != null)
            {
                dgvClaim.DataSource = dt;
                dgvClaim.DataBind();
            }
        }

        protected void LoadExpenseType()
        {
            BusinessLayer.HR.ExpenseType ObjExpType = new BusinessLayer.HR.ExpenseType();
            DataTable dt = ObjExpType.GetAll();
            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["ExpenseTypeId"] = "0";
                dr["ExpenseTypeName"] = "--Select Expense Type--";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                ddlExpenseType.DataSource = dt;
                ddlExpenseType.DataBind();
            }
        }

        protected void ClearControl()
        {
            Message.Show = false;
            ddlExpenseType.SelectedValue = "0";
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtExpAmount.Text = "";
            txtExpDate.Text = DateTime.Now.ToString("dd/MM/yyyy"); 
            ChkBillSubmitted.Checked = false;

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadClaimHistory();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                BusinessLayer.HR.ExpenseClaim ObjClaim = new BusinessLayer.HR.ExpenseClaim();
                Entity.HR.ExpenseClaim Claim = new Entity.HR.ExpenseClaim();
                Claim.ExpenseClaimId = 0; //Always 0 for saving
                Claim.ClaimTitle = txtTitle.Text.Trim();
                Claim.ClaimDescription = txtDescription.Text.Trim();
                Claim.EmployeeId = EmployeeId;
                Claim.ExpenseTypeId = int.Parse(ddlExpenseType.SelectedValue.Trim());
                Claim.ExpenseAmount = decimal.Parse(txtExpAmount.Text.Trim());

                string[] ExpDate = txtExpDate.Text.Trim().Split('/');
                Claim.ExpenseDate = Convert.ToDateTime(ExpDate[1].Trim() + "/" + ExpDate[0].Trim() + "/" + ExpDate[2].Trim() + " 00:00:00");

                Claim.BillSubmitted = ChkBillSubmitted.Checked;
                Claim.ClaimStatusId = 1;//Pending
                Claim.PaymentDate = null;
                Claim.BillReceived = false;
                Claim.ApproverComment = "";
                int RowsAffected = ObjClaim.Save(Claim);
                if (RowsAffected != -1)
                {
                    Claim = ObjClaim.GetAllById(Claim.ExpenseClaimId);
                    ClearControl();
                    LoadClaimHistory();
                    Message.IsSuccess = true;
                    Message.Text = "New Claim No " + Claim.ClaimNo + " Is Generated.";
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Sorry! You Can Not Apply For Claim. You Don't Have Approver For Claim Approval.";
                }
                Message.Show = true;

            }
            else
            {
                Message.Show = true;
            }
        }

        protected bool Validate()
        {
            string ErrorText = "";
            bool result = true;
            string[] ExpDate = txtExpDate.Text.Trim().Split('/');
            DateTime ExpenseDate = Convert.ToDateTime(ExpDate[1].Trim() + "/" + ExpDate[0].Trim() + "/" + ExpDate[2].Trim() + " 00:00:00");

            if (ExpenseDate > DateTime.Now)
            {
                ErrorText = "Expense Date Should Not Be More Than Current Date";
                result = false;
            }
            if (result == false)
            {
                Message.IsSuccess = false;
                Message.Text = ErrorText;
            }
            return result;
        }
    }
}
