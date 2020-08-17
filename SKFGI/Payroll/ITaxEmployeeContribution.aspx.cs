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

namespace CollegeERP.Payroll
{
    public partial class ITaxEmployeeContribution : System.Web.UI.Page
    {
        public int ITaxEmployeeContributionId
        {
            get { return Convert.ToInt32(ViewState["ITaxEmployeeContributionId"]); }
            set { ViewState["ITaxEmployeeContributionId"] = value; }
        }

        public int EmployeeId
        {
            get { return Convert.ToInt32(ViewState["EmployeeId"]); }
            set { ViewState["EmployeeId"] = value; }
        }

        public int CurrentFnYr
        {
            get { return Convert.ToInt32(ViewState["CurrentFnYr"]); }
            set { ViewState["CurrentFnYr"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.ITAX_EMPLOYEE_CNTR))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadInvestment();
                GetCurrentFnYr();
                ClearControls();
                LoadITaxEmployeeContributionList();

            }
        }

        protected void GetCurrentFnYr()
        {
            if (DateTime.Now.Month < 4)
            {
                CurrentFnYr = DateTime.Now.Year - 1;
            }
            else
            {
                CurrentFnYr = DateTime.Now.Year;
            }
        }

        protected void LoadITaxEmployeeContributionList()
        {
            string FName = txtFName.Text.Trim();
            string EmpCode = txtEmpCodeSearch.Text.Trim();
            int EmpId = int.Parse(HttpContext.Current.User.Identity.Name);
            BusinessLayer.Payroll.ITaxEmployeeContribution ObjITaxEmployeeContribution = new BusinessLayer.Payroll.ITaxEmployeeContribution();
            DataTable dt;
            if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.ALL_EMPLOYEES_IT_CONTRIBUTION))
            {
                dt = ObjITaxEmployeeContribution.GetAll(EmpId, "", "");
            }
            else
            {
                dt = ObjITaxEmployeeContribution.GetAll(0, FName, EmpCode);
            }

            if (dt != null)
            {
                dgvITaxEmployeeContribution.DataSource = dt;
                dgvITaxEmployeeContribution.DataBind();
            }
        }

        protected void LoadITaxEmployeeContributionDetails()
        {
            BusinessLayer.Payroll.ITaxEmployeeContribution ObjITaxEmployeeContribution = new BusinessLayer.Payroll.ITaxEmployeeContribution();
            Entity.Payroll.ITaxEmployeeContribution ITaxEmployeeContribution = new Entity.Payroll.ITaxEmployeeContribution();
            ITaxEmployeeContribution = ObjITaxEmployeeContribution.GetAllById(ITaxEmployeeContributionId);
            if (ITaxEmployeeContribution != null)
            {
                EmployeeId = ITaxEmployeeContribution.EmployeeId;
                ddlInvestment.SelectedValue = ITaxEmployeeContribution.ITaxInvestmentHeadId.ToString();
                txtProposedAmount.Text = ITaxEmployeeContribution.ProposedAmount.ToString("#0.00");
                txtApprovedAmount.Text = (ITaxEmployeeContribution.ApprovedAmount == null) ? "" : ITaxEmployeeContribution.ApprovedAmount.Value.ToString("#0.00");
                ltrFinancialYear.Text = ITaxEmployeeContribution.FinancialYear.ToString() + " - " + (ITaxEmployeeContribution.FinancialYear + 1).ToString();
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.ALL_EMPLOYEES_IT_CONTRIBUTION))
                {
                    txtApprovedAmount.ReadOnly = true;
                }
                else { txtApprovedAmount.ReadOnly = false; }
                btnSave.Text = "Update";
                Message.Show = false;
            }
        }

        protected void ClearControls()
        {
            ITaxEmployeeContributionId = 0;
            EmployeeId = int.Parse(HttpContext.Current.User.Identity.Name);
            txtProposedAmount.Text = "";
            txtApprovedAmount.Text = "";
            ltrFinancialYear.Text = CurrentFnYr.ToString() + " - " + (CurrentFnYr + 1).ToString();
            if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.ALL_EMPLOYEES_IT_CONTRIBUTION))
            {
                Searchtable.Visible = false;
                txtApprovedAmount.ReadOnly = true;
            }
            else { txtApprovedAmount.ReadOnly = false; }

            ddlInvestment.SelectedValue = "0";
            txtFName.Text = "";
            txtEmpCodeSearch.Text = "";
            Message.Show = false;
            btnSave.Text = "Save";
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                BusinessLayer.Payroll.ITaxEmployeeContribution ObjITaxEmployeeContribution = new BusinessLayer.Payroll.ITaxEmployeeContribution();
                Entity.Payroll.ITaxEmployeeContribution ITaxEmployeeContribution = new Entity.Payroll.ITaxEmployeeContribution();
                ITaxEmployeeContribution.ITaxEmployeeContributionId = ITaxEmployeeContributionId;
                ITaxEmployeeContribution.EmployeeId = EmployeeId;
                ITaxEmployeeContribution.ITaxInvestmentHeadId = int.Parse(ddlInvestment.SelectedValue.Trim());
                ITaxEmployeeContribution.ProposedAmount = decimal.Parse(txtProposedAmount.Text);
                if (txtApprovedAmount.Text.Trim().Length == 0)
                {
                    ITaxEmployeeContribution.ApprovedAmount = null;
                }
                else { ITaxEmployeeContribution.ApprovedAmount = decimal.Parse(txtApprovedAmount.Text); }

                ITaxEmployeeContribution.FinancialYear = CurrentFnYr;
                int RowsAffected = ObjITaxEmployeeContribution.Save(ITaxEmployeeContribution);

                if (RowsAffected != -1)
                {
                    ClearControls();
                    LoadITaxEmployeeContributionList();
                    Message.IsSuccess = true;
                    Message.Text = "ITaxEmployeeContribution Saved Successfully";
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Can Not Save. Duplicate ITaxEmployeeContribution Name Is Not Allowed";
                }
            }
            Message.Show = true;
        }

        protected void dgvITaxEmployeeContribution_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ITaxEmployeeContributionId = Convert.ToInt32(dgvITaxEmployeeContribution.DataKeys[e.NewEditIndex].Value);
            LoadITaxEmployeeContributionDetails();
        }

        protected void dgvITaxEmployeeContribution_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = Convert.ToInt32(dgvITaxEmployeeContribution.DataKeys[e.RowIndex].Value);
            BusinessLayer.Payroll.ITaxEmployeeContribution ObjITaxEmployeeContribution = new BusinessLayer.Payroll.ITaxEmployeeContribution();
            int RowsAffected = ObjITaxEmployeeContribution.Delete(Id);
            LoadITaxEmployeeContributionList();
            Message.Show = false;
        }

        protected void LoadInvestment()
        {
            BusinessLayer.Payroll.ITaxInvestmentHeads ObjITaxInvestmentHeads = new BusinessLayer.Payroll.ITaxInvestmentHeads();
            DataTable dt = ObjITaxInvestmentHeads.GetAll(0);
            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["ITaxInvestmentHeadId"] = "0";
                dr["ITaxInvestmentHeadName"] = "--Select Investment Name--";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                ddlInvestment.DataSource = dt;
                ddlInvestment.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadITaxEmployeeContributionDetails();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected bool Validation()
        {
            decimal ProposedAmount = decimal.Parse(txtProposedAmount.Text.Trim());
            decimal ApprovedAmount = (txtApprovedAmount.Text.Trim().Length == 0) ? 0 : decimal.Parse(txtApprovedAmount.Text.Trim());
            if (ApprovedAmount > ProposedAmount)
            {
                Message.IsSuccess = false;
                Message.Text = "Approved Amount Should Not Be More Than Proposed Amount";
                return false;
            }
            else { return true; }

        }
    }
}
