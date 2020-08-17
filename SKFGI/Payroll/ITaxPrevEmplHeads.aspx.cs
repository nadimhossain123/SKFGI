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

namespace CollegeERP.Payroll
{
    public partial class ITaxPrevEmplHeads : System.Web.UI.Page
    {
        public int ITaxPrevEmplHeadId
        {
            get { return Convert.ToInt32(ViewState["ITaxPrevEmplHeadId"]); }
            set { ViewState["ITaxPrevEmplHeadId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.ITAX_PREV_EMPLHEAD))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadITaxPrevEmplHeadsList();
                ClearControls();

            }
        }

        protected void LoadITaxPrevEmplHeadsList()
        {
            BusinessLayer.Payroll.ITaxPrevEmplHeads ObjITaxPrevEmplHeads = new BusinessLayer.Payroll.ITaxPrevEmplHeads();
            DataTable dt = ObjITaxPrevEmplHeads.GetAll();
            if (dt != null)
            {
                dgvITaxPrevEmplHead.DataSource = dt;
                dgvITaxPrevEmplHead.DataBind();
            }
        }

        protected void LoadITaxPrevEmplHeadsDetails()
        {
            BusinessLayer.Payroll.ITaxPrevEmplHeads ObjITaxPrevEmplHeads = new BusinessLayer.Payroll.ITaxPrevEmplHeads();
            Entity.Payroll.ITaxPrevEmplHeads ITaxPrevEmplHeads = new Entity.Payroll.ITaxPrevEmplHeads();
            ITaxPrevEmplHeads = ObjITaxPrevEmplHeads.GetAllById(ITaxPrevEmplHeadId);
            if (ITaxPrevEmplHeads != null)
            {
                txtITaxPrevEmplHeadName.Text = ITaxPrevEmplHeads.ITaxPrevEmplHeadName;
                if (ITaxPrevEmplHeads.ITaxType == "Add")
                { rbAddition.Checked = true; }
                else if (ITaxPrevEmplHeads.ITaxType == "Ded")
                { rbDeduction.Checked = true; }
                else
                { rbAddition.Checked = false; rbDeduction.Checked = false; }
                Message.Show = false;
            }
        }

        protected void ClearControls()
        {
            ITaxPrevEmplHeadId = 0;
            txtITaxPrevEmplHeadName.Text = "";
            rbAddition.Checked = true;
            rbDeduction.Checked = false;
            Message.Show = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Payroll.ITaxPrevEmplHeads ObjITaxPrevEmplHeads = new BusinessLayer.Payroll.ITaxPrevEmplHeads();
            Entity.Payroll.ITaxPrevEmplHeads ITaxPrevEmplHeads = new Entity.Payroll.ITaxPrevEmplHeads();
            ITaxPrevEmplHeads.ITaxPrevEmplHeadId = ITaxPrevEmplHeadId;
            ITaxPrevEmplHeads.ITaxPrevEmplHeadName = txtITaxPrevEmplHeadName.Text.Trim();
            if (rbAddition.Checked == true)
            { ITaxPrevEmplHeads.ITaxType = "Add"; }
            else if (rbDeduction.Checked == true)
            { ITaxPrevEmplHeads.ITaxType = "Ded"; }

            int RowsAffected = ObjITaxPrevEmplHeads.Save(ITaxPrevEmplHeads);

            if (RowsAffected != -1)
            {
                ClearControls();
                LoadITaxPrevEmplHeadsList();
                Message.IsSuccess = true;
                Message.Text = "ITaxPrevEmplHeads Saved Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate ITaxPrevEmplHeads Name Is Not Allowed";
            }
            Message.Show = true;
        }

        protected void dgvITaxPrevEmplHead_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ITaxPrevEmplHeadId = Convert.ToInt32(dgvITaxPrevEmplHead.DataKeys[e.NewEditIndex].Value);
            LoadITaxPrevEmplHeadsDetails();
        }

        protected void dgvITaxPrevEmplHead_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = Convert.ToInt32(dgvITaxPrevEmplHead.DataKeys[e.RowIndex].Value);
            BusinessLayer.Payroll.ITaxPrevEmplHeads ObjITaxPrevEmplHeads = new BusinessLayer.Payroll.ITaxPrevEmplHeads();
            int RowsAffected = ObjITaxPrevEmplHeads.Delete(Id);
           
            LoadITaxPrevEmplHeadsList();
            Message.Show = false;
           
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
    }
}
