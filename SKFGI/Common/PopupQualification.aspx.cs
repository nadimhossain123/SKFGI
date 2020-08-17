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
    public partial class PopupQualification : System.Web.UI.Page
    {
        public int EmployeeId
        {
            get { return Convert.ToInt32(ViewState["EmployeeId"]); }
            set { ViewState["EmployeeId"] = value; }
        }

        public int EmployeeQualificationId
        {
            get { return Convert.ToInt32(ViewState["EmployeeQualificationId"]); }
            set { ViewState["EmployeeQualificationId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["EmployeeId"] != null && Request.QueryString["EmployeeId"].Trim().Length > 0)
                {
                    EmployeeId = Convert.ToInt32(Request.QueryString["EmployeeId"].Trim());
                    LoadEmployeeDetails();
                    LoadEmployeeQualificationList();
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

        protected void LoadEmployeeQualificationList()
        {
            BusinessLayer.Common.EmployeeQualification ObjQualification = new BusinessLayer.Common.EmployeeQualification();
            DataTable dt = ObjQualification.GetAll(EmployeeId);
            if (dt != null)
            {
                dgvQualification.DataSource = dt;
                dgvQualification.DataBind();
            }
        }

        protected void LoadEmployeeQualificationDetails()
        {
            BusinessLayer.Common.EmployeeQualification ObjQualification = new BusinessLayer.Common.EmployeeQualification();
            Entity.Common.EmployeeQualification Qualification = new Entity.Common.EmployeeQualification();
            Qualification = ObjQualification.GetAllById(EmployeeQualificationId);
            if (Qualification != null)
            {
                txtQualificationName.Text = Qualification.QualificationName;
                txtBoard.Text = Qualification.QualificationBoard;
                txtYearOfPassing.Text = Qualification.QualificationPassingYear.ToString();
                txtMarksPercent.Text = Qualification.QualificationPercOfMarks.ToString("#0.00");
                txtStream.Text = Qualification.QualificationStream;
                ddlQualificationType.SelectedValue = Qualification.QualificationType;
                Message.Show = false;
            }
        }

        protected void Clearcontrols()
        {
            EmployeeQualificationId = 0;
            Message.Show = false;
            txtQualificationName.Text = "";
            txtBoard.Text = "";
            txtYearOfPassing.Text = "";
            txtMarksPercent.Text = "0";
            txtStream.Text = "";
            ddlQualificationType.SelectedIndex = 0;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.EmployeeQualification ObjQualification = new BusinessLayer.Common.EmployeeQualification();
            Entity.Common.EmployeeQualification Qualification = new Entity.Common.EmployeeQualification();
            Qualification.EmployeeQualificationId = EmployeeQualificationId;
            Qualification.EmployeeQualification_EmployeeId = EmployeeId;
            Qualification.QualificationName = txtQualificationName.Text.Trim();
            Qualification.QualificationBoard = txtBoard.Text.Trim();
            Qualification.QualificationPassingYear = int.Parse(txtYearOfPassing.Text.Trim());
            Qualification.QualificationPercOfMarks = (txtMarksPercent.Text.Trim().Length == 0) ? 0 : decimal.Parse(txtMarksPercent.Text.Trim());
            Qualification.QualificationStream = txtStream.Text.Trim();
            Qualification.QualificationType = ddlQualificationType.SelectedValue.Trim();
            
            Qualification.CreatedBy = int.Parse(HttpContext.Current.User.Identity.Name);
            Qualification.ModifiedBy = int.Parse(HttpContext.Current.User.Identity.Name);
            ObjQualification.Save(Qualification);
            Clearcontrols();
            LoadEmployeeQualificationList();
            Message.IsSuccess = true;
            Message.Text = "Qualification Details Updated Successfuly";
            Message.Show = true;
        }

        protected void dgvQualification_RowEditing(object sender, GridViewEditEventArgs e)
        {
            EmployeeQualificationId = Convert.ToInt32(dgvQualification.DataKeys[e.NewEditIndex].Value);
            LoadEmployeeQualificationDetails();
        }

        protected void dgvQualification_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = Convert.ToInt32(dgvQualification.DataKeys[e.RowIndex].Value);
            BusinessLayer.Common.EmployeeQualification ObjQualification = new BusinessLayer.Common.EmployeeQualification();
            ObjQualification.Delete(Id);
            LoadEmployeeQualificationList();
            Message.Show = false;
        }
    }
}
