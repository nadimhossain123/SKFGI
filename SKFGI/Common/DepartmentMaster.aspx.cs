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
    public partial class DepartmentMaster : System.Web.UI.Page
    {
        public int DepartmentId
        {
            get { return Convert.ToInt32(ViewState["DepartmentId"]); }
            set { ViewState["DepartmentId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDepartmentList();
                ClearControls();

            }
        }

        protected void LoadDepartmentList()
        {
            BusinessLayer.Common.Department ObjDepartment = new BusinessLayer.Common.Department();
            DataTable dt = ObjDepartment.GetAll();
            if (dt != null)
            {
                dgvDepartment.DataSource = dt;
                dgvDepartment.DataBind();
            }
        }

        protected void LoadDepartmentDetails()
        {
            BusinessLayer.Common.Department ObjDepartment = new BusinessLayer.Common.Department();
            Entity.Common.Department Department = new Entity.Common.Department();
            Department = ObjDepartment.GetAllById(DepartmentId);
            if (Department != null)
            {
                txtDepartment.Text = Department.DepartmentName;
                Message.Show = false;
            }
        }

        protected void ClearControls()
        {
            DepartmentId = 0;
            txtDepartment.Text = "";
            Message.Show = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.Department ObjDepartment = new BusinessLayer.Common.Department();
            Entity.Common.Department Department = new Entity.Common.Department();
            Department.DepartmentId = DepartmentId;
            Department.DepartmentName = txtDepartment.Text.Trim();
            int RowsAffected= ObjDepartment.Save(Department);
            if (RowsAffected != -1)
            {
                ClearControls();
                LoadDepartmentList();
                Message.IsSuccess = true;
                Message.Text = "Department Saved Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate Department Name Is Not Allowed";
            }
            Message.Show = true;
        }

        protected void dgvDepartment_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DepartmentId = Convert.ToInt32(dgvDepartment.DataKeys[e.NewEditIndex].Value);
            LoadDepartmentDetails();
        }

        protected void dgvDepartment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = Convert.ToInt32(dgvDepartment.DataKeys[e.RowIndex].Value);
            BusinessLayer.Common.Department ObjDepartment = new BusinessLayer.Common.Department();
            int RowsAffected = ObjDepartment.Delete(Id);
            if (RowsAffected != -1)
            {
                LoadDepartmentList();
                Message.Show = false;
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Delete. One or More Employee Is Attached With This Department.";
                Message.Show = true;
            }
        }
    }
}
