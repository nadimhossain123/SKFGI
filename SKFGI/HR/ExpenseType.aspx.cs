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

namespace CollegeERP.HR
{
    public partial class ExpenseType : System.Web.UI.Page
    {
        public int ExpenseTypeId
        {
            get { return Convert.ToInt32(ViewState["ExpenseTypeId"]); }
            set { ViewState["ExpenseTypeId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.CLAIM_SETTING))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadExpenseTypeList();
                ClearControls();

            }
        }

        protected void LoadExpenseTypeList()
        {
            BusinessLayer.HR.ExpenseType ObjExpType = new BusinessLayer.HR.ExpenseType();
            DataTable dt = ObjExpType.GetAll();
            if (dt != null)
            {
                dgvExpenseType.DataSource = dt;
                dgvExpenseType.DataBind();
            }
        }

        protected void LoadExpenseTypeDetails()
        {
            BusinessLayer.HR.ExpenseType ObjExpType = new BusinessLayer.HR.ExpenseType();
            Entity.HR.ExpenseType ExpenseType = new Entity.HR.ExpenseType();
            ExpenseType = ObjExpType.GetAllById(ExpenseTypeId);
            if (ExpenseType != null)
            {
                txtExpenseType.Text = ExpenseType.ExpenseTypeName;
                Message.Show = false;
            }
        }

        protected void ClearControls()
        {
            ExpenseTypeId = 0;
            txtExpenseType.Text = "";
            Message.Show = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.HR.ExpenseType ObjExpType = new BusinessLayer.HR.ExpenseType();
            Entity.HR.ExpenseType ExpenseType = new Entity.HR.ExpenseType();
            ExpenseType.ExpenseTypeId = ExpenseTypeId;
            ExpenseType.ExpenseTypeName = txtExpenseType.Text.Trim();
            int RowsAffected = ObjExpType.Save(ExpenseType);
            if (RowsAffected != -1)
            {
                ClearControls();
                LoadExpenseTypeList();
                Message.IsSuccess = true;
                Message.Text = "Expense Type Saved Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate Expense Type Is Not Allowed";
            }
            Message.Show = true;
        }

        protected void dgvExpenseType_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ExpenseTypeId = Convert.ToInt32(dgvExpenseType.DataKeys[e.NewEditIndex].Value.ToString());
            LoadExpenseTypeDetails();
        }
    }
}
