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
    public partial class ITaxInvestmentHeads : System.Web.UI.Page
    {
        public int ITaxInvestmentHeadId
        {
            get { return Convert.ToInt32(ViewState["ITaxInvestmentHeadId"]); }
            set { ViewState["ITaxInvestmentHeadId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.ITAX_INVESTMENT_HEAD))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadSection();
                LoadITaxInvestmentHeadsList();
                ClearControls();
                

            }
        }

        protected void LoadITaxInvestmentHeadsList()
        {
            int SectionId = int.Parse(ddlSection.SelectedValue.Trim());
            BusinessLayer.Payroll.ITaxInvestmentHeads ObjITaxInvestmentHeads = new BusinessLayer.Payroll.ITaxInvestmentHeads();
            DataTable dt = ObjITaxInvestmentHeads.GetAll(SectionId);
            if (dt != null)
            {
                dgvITaxInvestment.DataSource = dt;
                dgvITaxInvestment.DataBind();
            }
        }

        protected void LoadITaxInvestmentHeadsDetails()
        {
            BusinessLayer.Payroll.ITaxInvestmentHeads ObjITaxInvestmentHeads = new BusinessLayer.Payroll.ITaxInvestmentHeads();
            Entity.Payroll.ITaxInvestmentHeads ITaxInvestmentHeads = new Entity.Payroll.ITaxInvestmentHeads();
            ITaxInvestmentHeads = ObjITaxInvestmentHeads.GetAllById(ITaxInvestmentHeadId);
            if (ITaxInvestmentHeads != null)
            {
                txtITaxInvestmentHeadName.Text = ITaxInvestmentHeads.ITaxInvestmentHeadName;
                ddlSection.SelectedValue = ITaxInvestmentHeads.ITaxSectionId.ToString();
                if (ITaxInvestmentHeads.ITaxFlagName == "Add")
                { rbAddition.Checked = true; }
                else if (ITaxInvestmentHeads.ITaxFlagName == "Ded")
                { rbDeduction.Checked = true; }
                else
                { rbDeduction.Checked = false; rbAddition.Checked = false; }
                Message.Show = false;
            }
        }

        protected void ClearControls()
        {
            ITaxInvestmentHeadId = 0;
            txtITaxInvestmentHeadName.Text = "";
            ddlSection.SelectedValue = "0";
            rbAddition.Checked = true;
            Message.Show = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Payroll.ITaxInvestmentHeads ObjITaxInvestmentHeads = new BusinessLayer.Payroll.ITaxInvestmentHeads();
            Entity.Payroll.ITaxInvestmentHeads ITaxInvestmentHeads = new Entity.Payroll.ITaxInvestmentHeads();
            ITaxInvestmentHeads.ITaxInvestmentHeadId = ITaxInvestmentHeadId;
            ITaxInvestmentHeads.ITaxInvestmentHeadName = txtITaxInvestmentHeadName.Text.Trim();
            ITaxInvestmentHeads.ITaxSectionId = int.Parse(ddlSection.SelectedValue.Trim());
            if (rbAddition.Checked == true)
            { ITaxInvestmentHeads.ITaxFlagName = "Add"; }
            else
            { ITaxInvestmentHeads.ITaxFlagName = "Ded"; }

            int RowsAffected = ObjITaxInvestmentHeads.Save(ITaxInvestmentHeads);

            if (RowsAffected != -1)
            {
                ClearControls();
                LoadITaxInvestmentHeadsList();
                Message.IsSuccess = true;
                Message.Text = "ITaxInvestmentHeads Saved Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate ITaxInvestmentHeads Name Is Not Allowed";
            }
            Message.Show = true;
        }

        protected void dgvITaxInvestment_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ITaxInvestmentHeadId = Convert.ToInt32(dgvITaxInvestment.DataKeys[e.NewEditIndex].Value);
            LoadITaxInvestmentHeadsDetails();
        }

        protected void dgvITaxInvestment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = Convert.ToInt32(dgvITaxInvestment.DataKeys[e.RowIndex].Value);
            BusinessLayer.Payroll.ITaxInvestmentHeads ObjITaxInvestmentHeads = new BusinessLayer.Payroll.ITaxInvestmentHeads();
            int RowsAffected = ObjITaxInvestmentHeads.Delete(Id);
           
            LoadITaxInvestmentHeadsList();
            Message.Show = false;
        }
        protected void LoadSection()
        {
            BusinessLayer.Payroll.ITaxSectionMaster ObjITaxITaxSectionMaster = new BusinessLayer.Payroll.ITaxSectionMaster();
            DataTable dt = ObjITaxITaxSectionMaster.GetAll();
            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["ITaxSectionId"] = "0";
                dr["ITaxSectionName"] = "--Select Section Name--";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                ddlSection.DataSource = dt;
                ddlSection.DataBind();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadITaxInvestmentHeadsList();
        }
    }
}
