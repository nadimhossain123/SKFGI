using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace CollegeERP.Common
{
    public partial class PopupWorkDetails : System.Web.UI.Page
    {
        public int EmployeeId
        {
            get { return Convert.ToInt32(ViewState["EmployeeId"]); }
            set { ViewState["EmployeeId"] = value; }
        }

        public int EmployeeWorkId
        {
            get { return Convert.ToInt32(ViewState["EmployeeWorkId"]); }
            set { ViewState["EmployeeWorkId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["EmployeeId"] != null && Request.QueryString["EmployeeId"].Trim().Length > 0)
                {
                    EmployeeId = Convert.ToInt32(Request.QueryString["EmployeeId"].Trim());
                    LoadEmployeeDetails();
                    LoadEmployeeWorkList();
                    Clearcontrols();
                }
            }
        }

        protected void LoadEmployeeDetails()
        {
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            Entity.Common.Employee Employee = new Entity.Common.Employee();
            Employee = ObjEmployee.GetAllById(EmployeeId);
            if (Employee != null)
            {
                divtitle.InnerHtml = "<h6>" + Employee.FirstName + " " + Employee.MiddleName + " " + Employee.LastName + " (" + Employee.EmpCode + ")</h6>";
            }
        }

        protected void LoadEmployeeWorkList()
        {
            BusinessLayer.Common.EmployeeWork ObjWork = new BusinessLayer.Common.EmployeeWork();
            DataTable dt = ObjWork.GetAll(EmployeeId);
            if (dt != null)
            {
                dgvWork.DataSource = dt;
                dgvWork.DataBind();
            }
        }

        protected void LoadEmployeeWorkDetails()
        {
            BusinessLayer.Common.EmployeeWork ObjWork = new BusinessLayer.Common.EmployeeWork();
            Entity.Common.EmployeeWork Work = new Entity.Common.EmployeeWork();
            Work = ObjWork.GetAllById(EmployeeWorkId);
            if (Work != null)
            {
                txtCompanyName.Text = Work.CompanyName;
                txtWorkPeriod.Text = Work.WorkPeriod;
                txtDesignation.Text = Work.WorkDesignation;
                txtResponsibilities.Text = Work.WorkResponsibilities;

                txtSalary.Text = (Work.WorkSalary == 0) ? "" : Work.WorkSalary.ToString("#0.00");
                
                Message.Show = false;
            }
        }

        protected void Clearcontrols()
        {
            EmployeeWorkId = 0;
            Message.Show = false;
            txtCompanyName.Text = "";
            txtWorkPeriod.Text = "";
            txtDesignation.Text = "";
            txtResponsibilities.Text = "";

            txtSalary.Text = "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.EmployeeWork ObjWork = new BusinessLayer.Common.EmployeeWork();
            Entity.Common.EmployeeWork Work = new Entity.Common.EmployeeWork();
            Work.EmployeeWorkId = EmployeeWorkId;
            Work.EmployeeWork_EmployeeId = EmployeeId;
            Work.CompanyName = txtCompanyName.Text.Trim();
            Work.WorkPeriod = txtWorkPeriod.Text.Trim();
            Work.WorkDesignation = txtDesignation.Text.Trim();
            Work.WorkResponsibilities = txtResponsibilities.Text.Trim();
            Work.WorkSalary = (txtSalary.Text.Trim().Length == 0) ? 0 : decimal.Parse(txtSalary.Text.Trim());
           
            Work.CreatedBy = int.Parse(HttpContext.Current.User.Identity.Name);
            Work.ModifiedBy = int.Parse(HttpContext.Current.User.Identity.Name);
            ObjWork.Save(Work);
            Clearcontrols();
            LoadEmployeeWorkList();
            Message.IsSuccess = true;
            Message.Text = "Working Details Updated Successfuly";
            Message.Show = true;
        }

        protected void dgvWork_RowEditing(object sender, GridViewEditEventArgs e)
        {
            EmployeeWorkId = Convert.ToInt32(dgvWork.DataKeys[e.NewEditIndex].Value);
            LoadEmployeeWorkDetails();
        }

        protected void dgvWork_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = Convert.ToInt32(dgvWork.DataKeys[e.RowIndex].Value);
            BusinessLayer.Common.EmployeeWork ObjWork = new BusinessLayer.Common.EmployeeWork();
            ObjWork.Delete(Id);
            LoadEmployeeWorkList();
            Message.Show = false;
        }
    }
}
