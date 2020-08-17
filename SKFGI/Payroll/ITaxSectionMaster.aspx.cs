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
    public partial class ITaxSectionMaster : System.Web.UI.Page
    {
        public int ITaxSectionId
        {
            get { return Convert.ToInt32(ViewState["ITaxSectionId"]); }
            set { ViewState["ITaxSectionId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.ITAX_SECTION_MASTER))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadITaxSectionMasterList();
                ClearControls();

            }
        }

        protected void LoadITaxSectionMasterList()
        {
            BusinessLayer.Payroll.ITaxSectionMaster ObjITaxSectionMaster = new BusinessLayer.Payroll.ITaxSectionMaster();
            DataTable dt = ObjITaxSectionMaster.GetAll();
            if (dt != null)
            {
                dgvITaxSection.DataSource = dt;
                dgvITaxSection.DataBind();
            }
        }

        protected void LoadITaxSectionMasterDetails()
        {
            BusinessLayer.Payroll.ITaxSectionMaster ObjITaxSectionMaster = new BusinessLayer.Payroll.ITaxSectionMaster();
            Entity.Payroll.ITaxSectionMaster ITaxSectionMaster = new Entity.Payroll.ITaxSectionMaster();
            ITaxSectionMaster = ObjITaxSectionMaster.GetAllById(ITaxSectionId);
            if (ITaxSectionMaster != null)
            {
                txtITaxSectionName.Text = ITaxSectionMaster.ITaxSectionName;
                txtITaxMaxExemption.Text =ITaxSectionMaster.ITaxMaxExemption.ToString("#0.00");
                Message.Show = false;
            }
        }

        protected void ClearControls()
        {
            ITaxSectionId = 0;
            txtITaxSectionName.Text = "";
            txtITaxMaxExemption.Text = "";
            Message.Show = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Payroll.ITaxSectionMaster ObjITaxSectionMaster = new BusinessLayer.Payroll.ITaxSectionMaster();
            Entity.Payroll.ITaxSectionMaster ITaxSectionMaster = new Entity.Payroll.ITaxSectionMaster();
            ITaxSectionMaster.ITaxSectionId = ITaxSectionId;
            ITaxSectionMaster.ITaxSectionName = txtITaxSectionName.Text.Trim();
            ITaxSectionMaster.ITaxMaxExemption =Convert.ToDecimal(txtITaxMaxExemption.Text.Trim());
            int RowsAffected = ObjITaxSectionMaster.Save(ITaxSectionMaster);

            if (RowsAffected != -1)
            {
                ClearControls();
                LoadITaxSectionMasterList();
                Message.IsSuccess = true;
                Message.Text = "ITax SectionMaster Saved Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate ITax SectionMaster Name Is Not Allowed";
            }
            Message.Show = true;
        }

        protected void dgvITaxSection_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ITaxSectionId = Convert.ToInt32(dgvITaxSection.DataKeys[e.NewEditIndex].Value);
            LoadITaxSectionMasterDetails();
        }

        protected void dgvITaxSection_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = Convert.ToInt32(dgvITaxSection.DataKeys[e.RowIndex].Value);
            BusinessLayer.Payroll.ITaxSectionMaster ObjITaxSectionMaster = new BusinessLayer.Payroll.ITaxSectionMaster();
            int RowsAffected = ObjITaxSectionMaster.Delete(Id);
            if (RowsAffected != -1)
            {
                LoadITaxSectionMasterList();
                Message.Show = false;
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Delete. One or More Employee Is Attached With This ITaxSectionMaster.";
                Message.Show = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
       
    }
}
