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

namespace CollegeERP.Student
{
    public partial class TeacherSubjectMapping : System.Web.UI.Page
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
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.TEACHER_SUBJECT_MAPPING))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadMappingList();
                LoadSubject();
                ClearControls();
                btnNewSubject.Attributes.Add("onclick", "javascript:return openpopup('SubjectMaster.aspx');");
                
            }
        }

        protected void LoadSubject()
        {
            BusinessLayer.student.Subject ObjSubject = new BusinessLayer.student.Subject();
            DataTable dt = ObjSubject.GetAll();
            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["SubjectId"] = "0";
                dr["SubjectName"] = "--Select Subject--";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                ddlSubject.DataSource = dt;
                ddlSubject.DataBind();
            }
        }

        protected void ClearControls()
        {
            EmployeeId = 0;
            txtEmployeeCode.Text = "";
            txtEmployeeName.Text = "";

        }
        protected void LoadMappingList()
        {
            BusinessLayer.Student.EmployeeSubjectMapping Objmaping = new BusinessLayer.Student.EmployeeSubjectMapping();
            string FName = txtFName.Text.Trim();
            DataTable dt = Objmaping.GetAll(FName);
            if (dt != null)
            {
                dgvMapping.DataSource = dt;
                dgvMapping.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadMappingList();
        }

        protected void LoadMappingDetails()
        {
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            Entity.Common.Employee Employee = new Entity.Common.Employee();
            Employee = ObjEmployee.GetAllById(EmployeeId);
            if (Employee != null)
            {
                txtEmployeeCode.Text = Employee.EmpCode;
                txtEmployeeName.Text = Employee.FirstName + "" + Employee.MiddleName + "" + Employee.LastName;
            }

            BusinessLayer.Student.EmployeeSubjectMapping Objmaping = new BusinessLayer.Student.EmployeeSubjectMapping();
            DataTable dt = Objmaping.GetAllById(EmployeeId);
            if (dt != null)
            {
                dgvSubject.DataSource = dt;
                dgvSubject.DataBind();
            }
        }

        protected void dgvMapping_RowEditing(object sender, GridViewEditEventArgs e)
        {
            EmployeeId=int.Parse(dgvMapping.DataKeys[e.NewEditIndex].Value.ToString());
            LoadMappingDetails();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Student.EmployeeSubjectMapping Objmaping = new BusinessLayer.Student.EmployeeSubjectMapping();
            Entity.Student.EmployeeSubjectMapping Mapping = new Entity.Student.EmployeeSubjectMapping();
            Mapping.EmployeeId = EmployeeId;
            Mapping.SubjectId = int.Parse(ddlSubject.SelectedValue.Trim());
            Objmaping.Save(Mapping);

            LoadMappingDetails();
            LoadMappingList();
        }

        protected void dgvSubject_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int MappingId = int.Parse(dgvSubject.DataKeys[e.RowIndex].Value.ToString());
            BusinessLayer.Student.EmployeeSubjectMapping Objmaping = new BusinessLayer.Student.EmployeeSubjectMapping();
            Objmaping.Delete(MappingId);

            LoadMappingDetails();
            LoadMappingList();
        }
    }
}
