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
    public partial class BranchMaster : System.Web.UI.Page
    {
        public int BranchId
        {
            get { return Convert.ToInt32(ViewState["BranchId"]); }
            set { ViewState["BranchId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBranchList();
                ClearControls();

            }
        }

        protected void LoadBranchList()
        {
            BusinessLayer.Common.Branch ObjBranch = new BusinessLayer.Common.Branch();
            DataTable dt = ObjBranch.GetAll();
            if (dt != null)
            {
                dgvBranch.DataSource = dt;
                dgvBranch.DataBind();
            }
        }

        protected void LoadBranchDetails()
        {
            BusinessLayer.Common.Branch ObjBranch = new BusinessLayer.Common.Branch();
            Entity.Common.Branch Branch = new Entity.Common.Branch();
            Branch = ObjBranch.GetAllById(BranchId);
            if (Branch != null)
            {
                txtBranch.Text = Branch.BranchName;
                Message.Show = false;
            }
        }

        protected void ClearControls()
        {
            BranchId = 0;
            txtBranch.Text = "";
            Message.Show = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.Branch ObjBranch = new BusinessLayer.Common.Branch();
            Entity.Common.Branch Branch = new Entity.Common.Branch();
            Branch.BranchId = BranchId;
            Branch.BranchName = txtBranch.Text.Trim();
            int RowsAffected= ObjBranch.Save(Branch);

            if (RowsAffected != -1)
            {
                ClearControls();
                LoadBranchList();
                Message.IsSuccess = true;
                Message.Text = "Branch Saved Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate Branch Name Is Not Allowed";
            }
            Message.Show = true;
        }

        protected void dgvBranch_RowEditing(object sender, GridViewEditEventArgs e)
        {
            BranchId = Convert.ToInt32(dgvBranch.DataKeys[e.NewEditIndex].Value);
            LoadBranchDetails();
        }

        protected void dgvBranch_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = Convert.ToInt32(dgvBranch.DataKeys[e.RowIndex].Value);
            BusinessLayer.Common.Branch ObjBranch = new BusinessLayer.Common.Branch();
            int RowsAffected = ObjBranch.Delete(Id);
            if (RowsAffected != -1)
            {
                LoadBranchList();
                Message.Show = false;
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Delete. One or More Employee Is Attached With This Branch.";
                Message.Show = true;
            }
        }
    }
}
