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
    public partial class PopupPTaxDetails : System.Web.UI.Page
    {
        public int PTaxId
        {
            get { return Convert.ToInt32(ViewState["PTaxId"]); }
            set { ViewState["PTaxId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["PTaxId"] != null && Request.QueryString["PTaxId"].Trim().Length > 0)
                {
                    PTaxId = Convert.ToInt32(Request.QueryString["PTaxId"].Trim());
                    LoadPTaxDetails();
                    LoadSlabList();
                    Clearcontrols();
                }
            }
        }

        protected void LoadPTaxDetails()
        {
            BusinessLayer.Common.PTax ObjPTax = new BusinessLayer.Common.PTax();
            Entity.Common.PTax PTax = new Entity.Common.PTax();
            PTax = ObjPTax.GetAllById(PTaxId);
            if (PTax != null)
            {
                divtitle.InnerHtml = "<h7>" + PTax.PTaxStateDescription + "</h7>";
            }
        }

        protected void LoadSlabList()
        {
            BusinessLayer.Common.PTaxDetails ObjPTaxDetails = new BusinessLayer.Common.PTaxDetails();
            DataTable dt = ObjPTaxDetails.GetAll(PTaxId);
            if (dt != null)
            {
                dgvPTaxDetails.DataSource = dt;
                dgvPTaxDetails.DataBind();
            }
        }

        protected void Clearcontrols()
        {
            Message.Show = false;
            txtSlabNo.Text = "";
            txtAmountRangeFrom.Text = "";
            txtAmountRangeTo.Text = "";
            txtPTaxAmount.Text = "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.PTaxDetails ObjPTaxDetails = new BusinessLayer.Common.PTaxDetails();
            Entity.Common.PTaxDetails PTaxDetails = new Entity.Common.PTaxDetails();
            PTaxDetails.PTaxDetails_PTaxId = PTaxId;
            PTaxDetails.PTaxDetailsSlabNo = int.Parse(txtSlabNo.Text.Trim());
            PTaxDetails.PTaxDetailsFromAmount = decimal.Parse(txtAmountRangeFrom.Text.Trim());
            PTaxDetails.PTaxDetailsToAmount = decimal.Parse(txtAmountRangeTo.Text.Trim());
            PTaxDetails.PTaxDetailsAmount = decimal.Parse(txtPTaxAmount.Text.Trim());
            PTaxDetails.PTaxDetailsCUser_UserId = int.Parse(HttpContext.Current.User.Identity.Name);

            int RowsAffected= ObjPTaxDetails.Save(PTaxDetails);
            if (RowsAffected != -1)
            {
                Clearcontrols();
                LoadSlabList();
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate Slab No Is Not Allowed";
                Message.Show = true;
            }
        }

        protected void dgvPTaxDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = Convert.ToInt32(dgvPTaxDetails.DataKeys[e.RowIndex].Value);
            BusinessLayer.Common.PTaxDetails ObjPTaxDetails = new BusinessLayer.Common.PTaxDetails();
            ObjPTaxDetails.Delete(Id);
            LoadSlabList();
            Message.Show = false;
        }
    }
}
