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
    public partial class PopUpRole : System.Web.UI.Page
    {
        public int EmployeeId
        {
            get { return Convert.ToInt32(ViewState["EmployeeId"]); }
            set { ViewState["EmployeeId"] = value; }
        }

        public int RoleId
        {
            get { return Convert.ToInt32(ViewState["RoleId"]); }
            set { ViewState["RoleId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadRole();
                Message.Show = false;
                if (Request.QueryString["EmployeeId"] != null && Request.QueryString["EmployeeId"].Trim().Length > 0)
                {
                    EmployeeId = Convert.ToInt32(Request.QueryString["EmployeeId"].Trim());
                    RoleId = 0;
                    LoadEmployeeRole();
                }

            }
        }

        protected void LoadRole()
        {
            BusinessLayer.Common.Role ObjRole = new BusinessLayer.Common.Role();
            DataView dv = new DataView(ObjRole.GetAll());
            dv.RowFilter = "RoleId <> 1";

            if (dv != null)
            {
                dgvRole.DataSource = dv;
                dgvRole.DataBind();
                
                ddlRole.DataSource = dv;
                ddlRole.DataBind();
            }
            ddlRole.Items.Insert(0, new ListItem("Select", "0"));
        }

        protected void LoadEmployeeRole()
        {
            BusinessLayer.Common.EmployeeRole ObjRole = new BusinessLayer.Common.EmployeeRole();
            DataTable dt = ObjRole.GetAll(EmployeeId);
            if (dt != null)
            {
                dgvMap.DataSource = dt;
                dgvMap.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.Role ObjRole = new BusinessLayer.Common.Role();
            Entity.Common.Role Role = new Entity.Common.Role();
            Role.RoleId = RoleId;
            Role.RoleDescription = txtRole.Text.Trim();

            int RowsAffected = ObjRole.Save(Role);
            if (RowsAffected != -1)
            {
                LoadRole();
                txtRole.Text = "";
                RoleId = 0;
                Message.Show = false;
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Duplicate Role Is Not Allowed";
                Message.Show = true;
            }
        }

        protected void dgvRole_RowEditing(object sender, GridViewEditEventArgs e)
        {
            RoleId = int.Parse(dgvRole.DataKeys[e.NewEditIndex].Value.ToString());
            BusinessLayer.Common.Role ObjRole = new BusinessLayer.Common.Role();
            Entity.Common.Role Role = new Entity.Common.Role();
            Role = ObjRole.GetAllById(RoleId);
            if (Role != null)
            {
                txtRole.Text = Role.RoleDescription;
            }
        }

        protected void dgvRole_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = int.Parse(dgvRole.DataKeys[e.RowIndex].Value.ToString());
            BusinessLayer.Common.Role ObjRole = new BusinessLayer.Common.Role();
            int RowsAffected = ObjRole.Delete(Id);
            if (RowsAffected != -1)
            {
                LoadRole();
                Message.Show = false;
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can't Delete. One or More Employee Is Associated With This Role";
                Message.Show = true;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.EmployeeRole ObjRole = new BusinessLayer.Common.EmployeeRole();
            Entity.Common.EmployeeRole Role = new Entity.Common.EmployeeRole();
            Role.EmployeeId = EmployeeId;
            Role.RoleId = int.Parse(ddlRole.SelectedValue.Trim());
            int RowsAffected = ObjRole.Save(Role);

            if (RowsAffected != -1)
            {
                LoadEmployeeRole();
            }
        }

        protected void dgvMap_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int MappingId = int.Parse(dgvMap.DataKeys[e.RowIndex].Value.ToString());
            BusinessLayer.Common.EmployeeRole ObjRole = new BusinessLayer.Common.EmployeeRole();
            ObjRole.Delete(MappingId);
            LoadEmployeeRole();
        }
    }
}
