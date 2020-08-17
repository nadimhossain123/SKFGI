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
    public partial class ITaxEmployeePrevEmplDetails : System.Web.UI.Page
    {
        public int ITaxPrevEmplHeadId
        {
            get { return Convert.ToInt32(ViewState["ITaxPrevEmplHeadId"]); }
            set { ViewState["ITaxPrevEmplHeadId"] = value; }
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
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.ITAX_EMPLOYEE_PREVEMPL_DETAILS))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                EmployeeId = int.Parse(HttpContext.Current.User.Identity.Name);
                LoadPrevEmplHead();
                GetCurrentFnYr();
                ClearControls();
                LoadITaxEmployeePrevEmplDetailsList();
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

        

        protected void LoadITaxEmployeePrevEmplDetailsList()
        {
            BusinessLayer.Payroll.ITaxEmployeePrevEmplDetails ObjITaxEmployeePrevEmplDetails = new BusinessLayer.Payroll.ITaxEmployeePrevEmplDetails();
            DataTable dt = ObjITaxEmployeePrevEmplDetails.GetAll(EmployeeId);
            if (dt != null)
            {
                dgvITaxEmployeePrevEmplDetails.DataSource = dt;
                dgvITaxEmployeePrevEmplDetails.DataBind();
            }
        }

        protected void LoadITaxEmployeePrevEmplDetailsDetails()
        {
            BusinessLayer.Payroll.ITaxEmployeePrevEmplDetails ObjITaxEmployeePrevEmplDetails = new BusinessLayer.Payroll.ITaxEmployeePrevEmplDetails();
            Entity.Payroll.ITaxEmployeePrevEmplDetails ITaxEmployeePrevEmplDetails = new Entity.Payroll.ITaxEmployeePrevEmplDetails();
            ITaxEmployeePrevEmplDetails = ObjITaxEmployeePrevEmplDetails.GetAllById(ITaxPrevEmplHeadId, EmployeeId);
            if (ITaxEmployeePrevEmplDetails != null)
            {
                EmployeeId = ITaxEmployeePrevEmplDetails.EmployeeId;
                ddlPreviusEmp.SelectedValue = ITaxEmployeePrevEmplDetails.ITaxPrevEmplHeadId.ToString();
                txtITaxPrevEmplHeadAmount.Text = ITaxEmployeePrevEmplDetails.ITaxPrevEmplHeadAmount.ToString("#0.00");
                ltrFinancialYear.Text = ITaxEmployeePrevEmplDetails.FinancialYear.ToString() + " - " + (ITaxEmployeePrevEmplDetails.FinancialYear + 1).ToString();
                Message.Show = false;
            }
        }

        protected void ClearControls()
        {
            txtITaxPrevEmplHeadAmount.Text = "";
            ddlPreviusEmp.SelectedValue = "0";
            ltrFinancialYear.Text = CurrentFnYr.ToString() + " - " + (CurrentFnYr + 1).ToString();
            Message.Show = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Payroll.ITaxEmployeePrevEmplDetails ObjITaxEmployeePrevEmplDetails = new BusinessLayer.Payroll.ITaxEmployeePrevEmplDetails();
            Entity.Payroll.ITaxEmployeePrevEmplDetails ITaxEmployeePrevEmplDetails = new Entity.Payroll.ITaxEmployeePrevEmplDetails();
            ITaxEmployeePrevEmplDetails.ITaxPrevEmplHeadId = int.Parse(ddlPreviusEmp.SelectedValue.Trim());
            ITaxEmployeePrevEmplDetails.EmployeeId = EmployeeId;
            ITaxEmployeePrevEmplDetails.ITaxPrevEmplHeadAmount = decimal.Parse(txtITaxPrevEmplHeadAmount.Text);
            ITaxEmployeePrevEmplDetails.FinancialYear = CurrentFnYr;
            int RowsAffected = ObjITaxEmployeePrevEmplDetails.Save(ITaxEmployeePrevEmplDetails);

            if (RowsAffected != -1)
            {
                LoadITaxEmployeePrevEmplDetailsList();
                ClearControls();
                Message.IsSuccess = true;
                Message.Text = "ITaxEmployeePrevEmplDetails Saved Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate ITaxEmployeePrevEmplDetails Name Is Not Allowed";
            }
            Message.Show = true;
        }

        protected void dgvITaxEmployeePrevEmplDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ITaxPrevEmplHeadId = Convert.ToInt32(dgvITaxEmployeePrevEmplDetails.DataKeys[e.NewEditIndex].Values["ITaxPrevEmplHeadId"]);
            EmployeeId = Convert.ToInt32(dgvITaxEmployeePrevEmplDetails.DataKeys[e.NewEditIndex].Values["EmployeeId"]);
            LoadITaxEmployeePrevEmplDetailsDetails();
        }

        protected void dgvITaxEmployeePrevEmplDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = Convert.ToInt32(dgvITaxEmployeePrevEmplDetails.DataKeys[e.RowIndex].Values["ITaxPrevEmplHeadId"]);
            int EmpId = Convert.ToInt32(dgvITaxEmployeePrevEmplDetails.DataKeys[e.RowIndex].Values["EmployeeId"]);
            BusinessLayer.Payroll.ITaxEmployeePrevEmplDetails ObjITaxEmployeePrevEmplDetails = new BusinessLayer.Payroll.ITaxEmployeePrevEmplDetails();
            int RowsAffected = ObjITaxEmployeePrevEmplDetails.Delete(Id, EmpId);

            LoadITaxEmployeePrevEmplDetailsList();
            Message.Show = false;

        }

        protected void LoadPrevEmplHead()
        {
            BusinessLayer.Payroll.ITaxPrevEmplHeads ObjITaxInvestmentHeads = new BusinessLayer.Payroll.ITaxPrevEmplHeads();
            DataTable dt = ObjITaxInvestmentHeads.GetAll();
            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["ITaxPrevEmplHeadId"] = "0";
                dr["ITaxPrevEmplHeadName"] = "--Select Previous Empl Name--";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                ddlPreviusEmp.DataSource = dt;
                ddlPreviusEmp.DataBind();
            }
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
    }
}
