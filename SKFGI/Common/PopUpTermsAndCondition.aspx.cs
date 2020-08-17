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
    public partial class PopUpTermsAndCondition : System.Web.UI.Page
    {
        public int TermsId
        {
            get { return Convert.ToInt32(ViewState["TermsId"]); }
            set { ViewState["TermsId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearControls();
                LoadTermsList();
            }
        }

        protected void ClearControls()
        {
            TermsId = 0;
            txtTerms.Text = "";
            btnSave.Text = "Save";
        }

        protected void LoadTermsList()
        {
            BusinessLayer.Common.TermsAndCondition ObjTerms = new BusinessLayer.Common.TermsAndCondition();
            DataTable dt = ObjTerms.GetAll();
            if (dt != null)
            {
                dgvTerms.DataSource = dt;
                dgvTerms.DataBind();
            }
        }

        protected void LoadTermsDetails()
        {
            BusinessLayer.Common.TermsAndCondition ObjTerms = new BusinessLayer.Common.TermsAndCondition();
            Entity.Common.TermsAndCondition Terms = new Entity.Common.TermsAndCondition();
            Terms = ObjTerms.GetAllById(TermsId);
            if (Terms != null)
            {
                txtTerms.Text = Terms.TermsName;
                btnSave.Text = "Update";
            }
        }

        protected void dgvTerms_RowEditing(object sender, GridViewEditEventArgs e)
        {
            TermsId = int.Parse(dgvTerms.DataKeys[e.NewEditIndex].Value.ToString());
            LoadTermsDetails();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.TermsAndCondition ObjTerms = new BusinessLayer.Common.TermsAndCondition();
            Entity.Common.TermsAndCondition Terms = new Entity.Common.TermsAndCondition();
            Terms.TermsId = TermsId;
            Terms.TermsName = txtTerms.Text.Trim();
            ObjTerms.Save(Terms);
            ClearControls();
            LoadTermsList();
        }
    }
}
