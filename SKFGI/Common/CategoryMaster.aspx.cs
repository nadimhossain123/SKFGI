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
    public partial class CategoryMaster : System.Web.UI.Page
    {
        public int CategoryId
        {
            get { return Convert.ToInt32(ViewState["CategoryId"]); }
            set { ViewState["CategoryId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategoryList();
                ClearControls();
                
            }
        }

        protected void LoadCategoryList()
        {
            BusinessLayer.Common.Category ObjCategory = new BusinessLayer.Common.Category();
            DataTable dt = ObjCategory.GetAll();
            if (dt != null)
            {
                dgvCategory.DataSource = dt;
                dgvCategory.DataBind();
            }
        }

        protected void LoadCategoryDetails()
        {
            BusinessLayer.Common.Category ObjCategory = new BusinessLayer.Common.Category();
            Entity.Common.Category Category = new Entity.Common.Category();
            Category = ObjCategory.GetAllById(CategoryId);
            if (Category != null)
            {
                txtCategory.Text = Category.CategoryName;
                Message.Show = false;
            }
        }

        protected void ClearControls()
        {
            CategoryId = 0;
            txtCategory.Text = "";
            Message.Show = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.Category ObjCategory = new BusinessLayer.Common.Category();
            Entity.Common.Category Category = new Entity.Common.Category();
            Category.CategoryId = CategoryId;
            Category.CategoryName = txtCategory.Text.Trim();
            int RowsAffected=ObjCategory.Save(Category);

            if (RowsAffected != -1)
            {
                ClearControls();
                LoadCategoryList();
                Message.IsSuccess = true;
                Message.Text = "Category Saved Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate Category Name Is Not Allowed";
            }
            Message.Show = true;
        }

        protected void dgvCategory_RowEditing(object sender, GridViewEditEventArgs e)
        {
            CategoryId = Convert.ToInt32(dgvCategory.DataKeys[e.NewEditIndex].Value);
            LoadCategoryDetails();
        }

        protected void dgvCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = Convert.ToInt32(dgvCategory.DataKeys[e.RowIndex].Value);
            BusinessLayer.Common.Category ObjCategory = new BusinessLayer.Common.Category();
            int RowsAffected = ObjCategory.Delete(Id);
            if (RowsAffected != -1)
            {
                LoadCategoryList();
                Message.Show = false;
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Delete. One or More Employee Is Attached With This Category.";
                Message.Show = true;
            }
        }
    }
}
