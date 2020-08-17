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
    public partial class LoanEntryDetails : System.Web.UI.Page
    {
        public int LoanId
        {
            get { return Convert.ToInt32(ViewState["LoanId"]); }
            set { ViewState["LoanId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.LOAN_SETTING))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadEmployee();
                ClearControls();
            }
        }

        protected void LoadEmployee()
        {
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            DataTable dt = ObjEmployee.GetAll("", "");
            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["EmployeeId"] = "0";
                dr["FullName"] = "--Select Employee Name--";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                ddlEmployee.DataSource = dt;
                ddlEmployee.DataBind();
            }
        }

        protected void ClearControls()
        {
            LoanId = 0;
            ddlEmployee.SelectedValue = "0";
            ddlEmployee.Enabled = true;
            txtEmployeeCode.Text = "";
            txtDepartment.Text = "";
            txtDesignation.Text = "";
            txtCategory.Text = "";

            txtApplicationDate.Text = "";
            txtSanctionDate.Text = "";
            txtDescription.Text = "";
            txtLoanAmount.Text = "";
            txtTotalTerm.Text = "";
            txtInterestRate.Text = "";
            txtInterestAmount.Text = "";
            txtRefundAmount.Text = "";
            txtEMIAmount.Text = "";
            ddlDeductionMonth.SelectedValue = DateTime.Now.Month.ToString();
            ddlDeductionYear.SelectedValue = DateTime.Now.Year.ToString();

            dgvLoan.DataSource = null;
            dgvLoan.DataBind();
            Message.Show = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEmployeeHistory();
        }

        protected void LoadEmployeeHistory()
        {
            int EmployeeId = int.Parse(ddlEmployee.SelectedValue.Trim());
            int DesignationId = 0;
            int DepartmentId = 0;
            int CategoryId = 0;

            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            Entity.Common.Employee Employee = new Entity.Common.Employee();
            Employee = ObjEmployee.GetAllById(EmployeeId);
            if (Employee != null)
            {
                txtEmployeeCode.Text = Employee.EmpCode;
            }

            BusinessLayer.Common.EmployeeOfficial ObjOfficial = new BusinessLayer.Common.EmployeeOfficial();
            Entity.Common.EmployeeOfficial Official = new Entity.Common.EmployeeOfficial();
            Official = ObjOfficial.GetAllByEmployeeId(EmployeeId);
            if (Official != null)
            {
                DepartmentId = Official.EmployeeOfficial_DepartmentId;
                DesignationId = Official.EmployeeOfficial_DesignationId;
                CategoryId = Official.EmployeeOfficial_CategoryId;
            }

            BusinessLayer.Common.Category ObjCategory = new BusinessLayer.Common.Category();
            Entity.Common.Category Category = new Entity.Common.Category();
            Category = ObjCategory.GetAllById(CategoryId);
            txtCategory.Text = Category.CategoryName;

            BusinessLayer.Common.Department ObjDepartment = new BusinessLayer.Common.Department();
            Entity.Common.Department Department = new Entity.Common.Department();
            Department = ObjDepartment.GetAllById(DepartmentId);
            txtDepartment.Text = Department.DepartmentName;

            BusinessLayer.Common.Designation ObjDesignation = new BusinessLayer.Common.Designation();
            Entity.Common.Designation Designation = new Entity.Common.Designation();
            Designation = ObjDesignation.GetAllById(DesignationId);
            txtDesignation.Text = Designation.DesignationName;

            BusinessLayer.Payroll.Loan ObjLoan = new BusinessLayer.Payroll.Loan();
            DataTable dt = ObjLoan.GetAll(EmployeeId);
            dgvLoan.DataSource = dt;
            dgvLoan.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Payroll.Loan ObjLoan = new BusinessLayer.Payroll.Loan();
            Entity.Payroll.Loan Loan = new Entity.Payroll.Loan();
            Loan.LoanId = LoanId;
            Loan.Loan_EmployeeId = int.Parse(ddlEmployee.SelectedValue.Trim());

            string[] ApplicationDate = txtApplicationDate.Text.Trim().Split('/');
            Loan.LoanApplicationDate = Convert.ToDateTime(ApplicationDate[1].Trim() + "/" + ApplicationDate[0].Trim() + "/" + ApplicationDate[2].Trim() + " " + DateTime.Now.ToString("hh:mm:ss"));

            string[] SanctionDate = txtSanctionDate.Text.Trim().Split('/');
            Loan.LoanSanctionDate = Convert.ToDateTime(SanctionDate[1].Trim() + "/" + SanctionDate[0].Trim() + "/" + SanctionDate[2].Trim() + " " + DateTime.Now.ToString("hh:mm:ss"));
            Loan.LoanDescription = txtDescription.Text.Trim();
            Loan.LoanAmount = decimal.Parse(txtLoanAmount.Text.Trim());
            Loan.LoanTotalMonth = int.Parse(txtTotalTerm.Text.Trim());
            Loan.LoanInterestRate = decimal.Parse(txtInterestRate.Text.Trim());
            Loan.LoanInterestAmount = decimal.Parse(txtInterestAmount.Text.Trim());
            Loan.LoanRefundAmount = decimal.Parse(txtRefundAmount.Text.Trim());
            Loan.LoanEMIAmount = decimal.Parse(txtEMIAmount.Text.Trim());
            Loan.LoanDeductionMonth = int.Parse(ddlDeductionMonth.SelectedValue.Trim());
            Loan.LoanDeductionYear = int.Parse(ddlDeductionYear.SelectedValue.Trim());

            Loan.CreatedBy = int.Parse(HttpContext.Current.User.Identity.Name);
            Loan.ModifiedBy = int.Parse(HttpContext.Current.User.Identity.Name);
            ObjLoan.Save(Loan);

            LoanId = 0;
            ddlEmployee.Enabled = true;
            LoadEmployeeHistory();
            Message.IsSuccess = true;
            Message.Text = "Load Details Saved Successfully";
            Message.Show = true;
        }

        protected void dgvLoan_RowEditing(object sender, GridViewEditEventArgs e)
        {
            LoanId = int.Parse(dgvLoan.DataKeys[e.NewEditIndex].Value.ToString());
            LoadLoanDetails();
        }

        protected void LoadLoanDetails()
        {
            BusinessLayer.Payroll.Loan ObjLoan = new BusinessLayer.Payroll.Loan();
            Entity.Payroll.Loan Loan = new Entity.Payroll.Loan();
            Loan = ObjLoan.GetAllById(LoanId);
            if (Loan != null)
            {
                ddlEmployee.SelectedValue = Loan.Loan_EmployeeId.ToString();
                LoadEmployeeHistory();
                ddlEmployee.Enabled = false;
                txtApplicationDate.Text = Loan.LoanApplicationDate.ToString("dd/MM/yyyy");
                txtSanctionDate.Text = Loan.LoanSanctionDate.ToString("dd/MM/yyyy");
                txtDescription.Text = Loan.LoanDescription;
                txtLoanAmount.Text = Loan.LoanAmount.ToString("#0.00");
                txtTotalTerm.Text = Loan.LoanTotalMonth.ToString();
                txtInterestRate.Text = Loan.LoanInterestRate.ToString("#0.00");
                txtInterestAmount.Text = Loan.LoanInterestAmount.ToString("#0.00");
                txtRefundAmount.Text = Loan.LoanRefundAmount.ToString("#0.00");
                txtEMIAmount.Text = Loan.LoanEMIAmount.ToString("#0.00");
                ddlDeductionMonth.SelectedValue = Loan.LoanDeductionMonth.ToString();
                ddlDeductionYear.SelectedValue = Loan.LoanDeductionYear.ToString();

                Message.Show = false;

            }
        }

        protected void dgvLoan_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = int.Parse(dgvLoan.DataKeys[e.RowIndex].Value.ToString());
            BusinessLayer.Payroll.Loan ObjLoan = new BusinessLayer.Payroll.Loan();
            ObjLoan.Delete(Id);
            Message.Show = false;

            int EmployeeId = int.Parse(ddlEmployee.SelectedValue.Trim());
            DataTable dt = ObjLoan.GetAll(EmployeeId);
            dgvLoan.DataSource = dt;
            dgvLoan.DataBind();
        }
    }
}
