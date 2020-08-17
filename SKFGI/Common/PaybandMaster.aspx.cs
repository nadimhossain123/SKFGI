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
    public partial class PaybandMaster : System.Web.UI.Page
    {
        public int PayBandId
        {
            get { return Convert.ToInt32(ViewState["PayBandId"]); }
            set { ViewState["PayBandId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.PAYBAND_MASTER))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                ClearControls();
                LoadPaybandList();
                
            }
        }

        protected void ClearControls()
        {
            PayBandId = 0;
            btnSave.Text = "Save";
            Message.Show = false;

            txtPayband.Text = "";
            txtScaleFrom.Text = "";
            txtScaleTo.Text = "";
            txtGradePay.Text = "";
        }

        protected void LoadPaybandList()
        {
            BusinessLayer.Common.PayBand ObjPayband = new BusinessLayer.Common.PayBand();
            DataTable dt = ObjPayband.GetAll();
            if (dt != null)
            {
                dgvPayband.DataSource = dt;
                dgvPayband.DataBind();
            }
        }

        protected void LoadPaybandDetails()
        {
            BusinessLayer.Common.PayBand ObjPayband = new BusinessLayer.Common.PayBand();
            Entity.Common.PayBand PayBand = new Entity.Common.PayBand();
            PayBand = ObjPayband.GetAllById(PayBandId);
            if (PayBand != null)
            {
                txtPayband.Text = PayBand.PayBandName;
                txtScaleFrom.Text = PayBand.ScaleFrom.ToString("#0.00");
                txtScaleTo.Text = PayBand.ScaleTo.ToString("#0.00");
                txtGradePay.Text = PayBand.GradePay.ToString("#0.00");

                btnSave.Text = "Update";
                Message.Show = false;
            }
        }

        protected bool Validate()
        {
            decimal ScaleFrom = decimal.Parse(txtScaleFrom.Text.Trim());
            decimal ScaleTo = decimal.Parse(txtScaleTo.Text.Trim());

            if (ScaleFrom > ScaleTo)
            {
                return false;
            }
            else { return true; }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                BusinessLayer.Common.PayBand ObjPayband = new BusinessLayer.Common.PayBand();
                Entity.Common.PayBand PayBand = new Entity.Common.PayBand();
                PayBand.PayBandId = PayBandId;
                PayBand.PayBandName = txtPayband.Text.Trim();
                PayBand.ScaleFrom = decimal.Parse(txtScaleFrom.Text.Trim());
                PayBand.ScaleTo = decimal.Parse(txtScaleTo.Text.Trim());
                PayBand.GradePay = decimal.Parse(txtGradePay.Text.Trim());
                int RowsAffected = ObjPayband.Save(PayBand);
                if (RowsAffected != -1)
                {
                    ClearControls();
                    LoadPaybandList();
                    Message.IsSuccess = true;
                    Message.Text = "Payband Saved Successfully";
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Can Not Save. Duplicate Payband Name Is Not Allowed";
                }
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Please Provide a Valid Payband Scale Range";
            }
            Message.Show = true;
        }

        protected void dgvPayband_RowEditing(object sender, GridViewEditEventArgs e)
        {
            PayBandId = Convert.ToInt32(dgvPayband.DataKeys[e.NewEditIndex].Value);
            LoadPaybandDetails();
        }
    }
}
