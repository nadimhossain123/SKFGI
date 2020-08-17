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
    public partial class DesignationMaster : System.Web.UI.Page
    {
        public int DesignationId
        {
            get { return Convert.ToInt32(ViewState["DesignationId"]); }
            set { ViewState["DesignationId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDesignationList();
                ClearControls();

            }
        }

        protected void LoadDesignationList()
        {
            BusinessLayer.Common.Designation ObjDesignation = new BusinessLayer.Common.Designation();
            DataTable dt = ObjDesignation.GetAll();
            if (dt != null)
            {
                dgvDesignation.DataSource = dt;
                dgvDesignation.DataBind();
            }
        }

        protected void LoadDesignationDetails()
        {
            BusinessLayer.Common.Designation ObjDesignation = new BusinessLayer.Common.Designation();
            Entity.Common.Designation Designation = new Entity.Common.Designation();
            Designation = ObjDesignation.GetAllById(DesignationId);
            if (Designation != null)
            {
                txtDesignation.Text = Designation.DesignationName;
                Message.Show = false;
            }
        }

        protected void ClearControls()
        {
            DesignationId = 0;
            txtDesignation.Text = "";
            Message.Show = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.Designation ObjDesignation = new BusinessLayer.Common.Designation();
            Entity.Common.Designation Designation = new Entity.Common.Designation();
            Designation.DesignationId = DesignationId;
            Designation.DesignationName = txtDesignation.Text.Trim();
            int RowsAffected= ObjDesignation.Save(Designation);
            if (RowsAffected != -1)
            {
                ClearControls();
                LoadDesignationList();
                Message.IsSuccess = true;
                Message.Text = "Designation Saved Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate Designation Name Is Not Allowed";
            }
            Message.Show = true;
        }

        protected void dgvDesignation_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DesignationId = Convert.ToInt32(dgvDesignation.DataKeys[e.NewEditIndex].Value);
            LoadDesignationDetails();
        }

        protected void dgvDesignation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = Convert.ToInt32(dgvDesignation.DataKeys[e.RowIndex].Value);
            BusinessLayer.Common.Designation ObjDesignation = new BusinessLayer.Common.Designation();
            int RowsAffected = ObjDesignation.Delete(Id);
            if (RowsAffected != -1)
            {
                LoadDesignationList();
                Message.Show = false;
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Delete. One or More Employee Is Attached With This Designation.";
                Message.Show = true;
            }
        }
    }
}
