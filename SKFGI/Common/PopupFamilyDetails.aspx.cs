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
    public partial class PopupFamilyDetails : System.Web.UI.Page
    {
        public int EmployeeId
        {
            get { return Convert.ToInt32(ViewState["EmployeeId"]); }
            set { ViewState["EmployeeId"] = value; }
        }

        public int EmployeeFamilyId
        {
            get { return Convert.ToInt32(ViewState["EmployeeFamilyId"]); }
            set { ViewState["EmployeeFamilyId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["EmployeeId"] != null && Request.QueryString["EmployeeId"].Trim().Length > 0)
                {
                    EmployeeId = Convert.ToInt32(Request.QueryString["EmployeeId"].Trim());
                    LoadEmployeeDetails();
                    LoadEmployeeFamilyList();
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

        protected void LoadEmployeeFamilyList()
        {
            BusinessLayer.Common.EmployeeFamily ObjFamily = new BusinessLayer.Common.EmployeeFamily();
            DataTable dt = ObjFamily.GetAll(EmployeeId);
            if (dt != null)
            {
                dgvFamily.DataSource = dt;
                dgvFamily.DataBind();
            }
        }

        protected void LoadEmployeeFamilyDetails()
        {
            BusinessLayer.Common.EmployeeFamily ObjFamily = new BusinessLayer.Common.EmployeeFamily();
            Entity.Common.EmployeeFamily Family = new Entity.Common.EmployeeFamily();
            Family = ObjFamily.GetAllById(EmployeeFamilyId);
            if (Family != null)
            {
                txtMemberName.Text = Family.MemberName;
                txtOccupation.Text = Family.MemberOccupation;
                txtRelation.Text = Family.MemberRelation;
                ddlGender.SelectedValue = Family.MemberGender;
                txtAge.Text = (Family.MemberAge == 0) ? "" : Family.MemberAge.ToString();
                ddlHasContactPerson.SelectedValue = Family.HasMemberContact;
                txtEmail.Text = Family.MemberContactEmail;
                txtContactNo.Text = Family.MemberContactNo;

                Message.Show = false;
            }
        }

        protected void Clearcontrols()
        {
            EmployeeFamilyId = 0;
            Message.Show = false;
            txtMemberName.Text = "";
            txtOccupation.Text = "";
            txtRelation.Text = "";
            ddlGender.SelectedIndex = 0;
            txtAge.Text = "";
            ddlHasContactPerson.SelectedIndex = 0;
            txtEmail.Text = "";
            txtContactNo.Text = "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.EmployeeFamily ObjFamily = new BusinessLayer.Common.EmployeeFamily();
            Entity.Common.EmployeeFamily Family = new Entity.Common.EmployeeFamily();
            Family.EmployeeFamilyId = EmployeeFamilyId;
            Family.EmployeeFamily_EmployeeId = EmployeeId;
            Family.MemberName = txtMemberName.Text.Trim();
            Family.MemberOccupation = txtOccupation.Text.Trim();
            Family.MemberRelation = txtRelation.Text.Trim();
            Family.MemberGender = ddlGender.SelectedValue.Trim();
            Family.HasMemberContact = ddlHasContactPerson.SelectedValue.Trim();
            Family.MemberContactEmail = txtEmail.Text.Trim();
            Family.MemberContactNo = txtContactNo.Text.Trim();
            Family.MemberAge = (txtAge.Text.Trim().Length == 0) ? 0 : int.Parse(txtAge.Text.Trim());
           
            Family.CreatedBy = int.Parse(HttpContext.Current.User.Identity.Name);
            Family.ModifiedBy = int.Parse(HttpContext.Current.User.Identity.Name);
            ObjFamily.Save(Family);
            Clearcontrols();
            LoadEmployeeFamilyList();
            Message.IsSuccess = true;
            Message.Text = "Family Details Updated Successfuly";
            Message.Show = true;
        }

        protected void dgvFamily_RowEditing(object sender, GridViewEditEventArgs e)
        {
            EmployeeFamilyId = Convert.ToInt32(dgvFamily.DataKeys[e.NewEditIndex].Value);
            LoadEmployeeFamilyDetails();
        }

        protected void dgvFamily_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = Convert.ToInt32(dgvFamily.DataKeys[e.RowIndex].Value);
            BusinessLayer.Common.EmployeeFamily ObjFamily = new BusinessLayer.Common.EmployeeFamily();
            ObjFamily.Delete(Id);
            LoadEmployeeFamilyList();
            Message.Show = false;
        }
    }
}
