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
    public partial class EmployeeTermsAndCondition : System.Web.UI.Page
    {
        public int EmployeeId
        {
            get { return Convert.ToInt32(ViewState["EmployeeId"]); }
            set { ViewState["EmployeeId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["EmployeeId"] != null && Request.QueryString["EmployeeId"].Trim().Length > 0)
                {
                    EmployeeId = Convert.ToInt32(Request.QueryString["EmployeeId"].Trim());
                    LoadEmployeeTerms();
                }
            }
        }

        protected void LoadEmployeeTerms()
        {
            BusinessLayer.Common.TermsAndCondition ObjTerms = new BusinessLayer.Common.TermsAndCondition();
            DataTable dt = ObjTerms.GetAllEmployeeTerms(EmployeeId);
            if (dt != null)
            {
                dgvTerms.DataSource = dt;
                dgvTerms.DataBind();
            }
        }

        protected void dgvTerms_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int EmployeeTermsId = int.Parse(((HiddenField)e.Row.FindControl("HiddenId")).Value);
                if (EmployeeTermsId != 0)
                {
                    ((ImageButton)e.Row.FindControl("btnApplicable")).ImageUrl = "../Images/YES.jpg";
                }
                else { ((ImageButton)e.Row.FindControl("btnApplicable")).ImageUrl = "../Images/NO.jpg"; }
            }
        }

        protected void dgvTerms_RowEditing(object sender, GridViewEditEventArgs e)
        {
            BusinessLayer.Common.TermsAndCondition ObjTerms = new BusinessLayer.Common.TermsAndCondition();
            Entity.Common.TermsAndCondition Terms = new Entity.Common.TermsAndCondition();
            Terms.TermsId = int.Parse(dgvTerms.DataKeys[e.NewEditIndex].Value.ToString());
            Terms.EmployeeId = EmployeeId;
            int EmployeeTermsId = int.Parse(((HiddenField)dgvTerms.Rows[e.NewEditIndex].FindControl("HiddenId")).Value);
            if (EmployeeTermsId == 0)
            {
                Terms.IsChecked = true;
            }
            else { Terms.IsChecked = false; }
            ObjTerms.SaveEmployeeTerms(Terms);
            LoadEmployeeTerms();
        }
    }
}
