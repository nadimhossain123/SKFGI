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

namespace CollegeERP.Common
{
    public partial class PTax : System.Web.UI.Page
    {
        public int PTaxId
        {
            get { return Convert.ToInt32(ViewState["PTaxId"]); }
            set { ViewState["PTaxId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.PTAX_CONFIG))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                
                LoadPTaxList();
                ClearControls();
                
            }
        }


        protected void LoadPTaxList()
        {
            BusinessLayer.Common.PTax ObjPTax = new BusinessLayer.Common.PTax();
            DataTable dt = ObjPTax.GetAll();
            if (dt != null)
            {
                dgvPTax.DataSource = dt;
                dgvPTax.DataBind();
            }
        }

        protected void LoadPTaxDetails()
        {
            BusinessLayer.Common.PTax ObjPTax = new BusinessLayer.Common.PTax();
            Entity.Common.PTax PTax = new Entity.Common.PTax();
            PTax = ObjPTax.GetAllById(PTaxId);
            if (PTax != null)
            {
                txtStateCode.Text = PTax.PTaxStateCode;
                txtStateName.Text = PTax.PTaxStateDescription;
                Message.Show = false;
            }
        }

        protected void ClearControls()
        {
            PTaxId = 0;
            txtStateCode.Text = "";
            txtStateName.Text = "";

            Message.Show = false;
            btnSave.Text = "Save";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.PTax ObjPTax = new BusinessLayer.Common.PTax();
            Entity.Common.PTax PTax = new Entity.Common.PTax();
            PTax.PTaxId = PTaxId;
            PTax.PTaxStateCode = txtStateCode.Text.Trim();
            PTax.PTaxStateDescription = txtStateName.Text.Trim();

            int RowsAffected = ObjPTax.Save(PTax);
            if (RowsAffected != -1)
            {
                ClearControls();
                LoadPTaxList();
                Message.IsSuccess = true;
                Message.Text = "PTax Saved Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate State Code Is Not Allowed";
            }
            Message.Show = true;
        }

        protected void dgvPTax_RowEditing(object sender, GridViewEditEventArgs e)
        {
            PTaxId = Convert.ToInt32(dgvPTax.DataKeys[e.NewEditIndex].Value);
            LoadPTaxDetails();
        }

        protected void dgvPTax_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int Id = Convert.ToInt32(dgvPTax.DataKeys[e.Row.RowIndex].Value.ToString());
                ((LinkButton)e.Row.FindControl("lnkSlab")).Attributes.Add("onclick", "javascript:openpopup('PopupPTaxDetails.aspx?PTaxId=" + Id + "');");
                
            }
        }

        
    }
}
