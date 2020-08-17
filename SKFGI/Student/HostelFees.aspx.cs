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

namespace CollegeERP.Student
{
    public partial class HostelFees : System.Web.UI.Page
    {
        public int id
        {
            get { return Convert.ToInt32(ViewState["id"]); }
            set { ViewState["id"] = value; }
        }
        ListItem li = new ListItem("Select", "0");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.HOSTEL_FEES_CONFIGURATION))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadBatch();
                ClearControls();
                LoadFeesList();
            }
        }

        protected void LoadBatch()
        {
            BusinessLayer.Student.BTechRegistration BtechReg = new BusinessLayer.Student.BTechRegistration();
            Entity.Student.BTechRegistration eBtechReg = new Entity.Student.BTechRegistration();
            eBtechReg.intMode = 1;
            eBtechReg.CourseId = 0;
            DataTable dt = new DataTable();
            dt = BtechReg.GetAllCommonSP(eBtechReg);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ddlBatch.DataSource = dt;
                    ddlBatch.DataBind();
                }
            }
        }

        protected void ClearControls()
        {
            id = 0;
            btnSave.Text = "Save";
            Message.Show = false;

            ddlBatch.SelectedIndex = 0;
            ddlBatch.Enabled = true;
            txtFeesName.Text = "";

            LoadHostelFeesHead();
        }

        protected void LoadHostelFeesHead()
        {
            BusinessLayer.Student.StreamGroup objStreamGroup = new BusinessLayer.Student.StreamGroup();
            DataTable DT = objStreamGroup.GetAllHostelFeesHead();

            if (DT != null)
            {
                dgvFeesHead.DataSource = DT;
                dgvFeesHead.DataBind();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void LoadFeesList()
        {
            BusinessLayer.Student.HostelFeesConfig objHostelFees = new BusinessLayer.Student.HostelFeesConfig();
            DataTable DT = objHostelFees.GetAll(null);

            if (DT != null)
            {
                dgvFeesList.DataSource = DT;
                dgvFeesList.DataBind();
            }
        }

        protected void LoadFeesDetails()
        {
            BusinessLayer.Student.HostelFeesConfig objHostelFees = new BusinessLayer.Student.HostelFeesConfig();
            DataSet ds = objHostelFees.GetAllById(id);

            if (ds != null)
            {
                ddlBatch.SelectedValue = ds.Tables[0].Rows[0]["batch_id"].ToString();
                ddlBatch.Enabled = false;
                txtFeesName.Text = ds.Tables[0].Rows[0]["fees_name"].ToString();

                dgvFeesHead.DataSource = ds.Tables[1];
                dgvFeesHead.DataBind();

                btnSave.Text = "Update";
                Message.Show = false;
            }
        }

       

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Student.HostelFeesConfig objHostelFees = new BusinessLayer.Student.HostelFeesConfig();
            Entity.Student.HostelFeesConfig HostelFees = new Entity.Student.HostelFeesConfig();
            HostelFees.id = id;
            HostelFees.batch_id = Convert.ToInt32(ddlBatch.SelectedValue.Trim());
            HostelFees.fees_name = txtFeesName.Text.Trim();

            DataTable DT = new DataTable();
            DT.Columns.Add("fees_header_id", typeof(int));
            DT.Columns.Add("amount", typeof(decimal));
            DataRow DR;

            foreach (GridViewRow GVR in dgvFeesHead.Rows)
            {
                if (GVR.RowType == DataControlRowType.DataRow)
                {
                    DR = DT.NewRow();
                    DR["fees_header_id"] = Convert.ToInt32(dgvFeesHead.DataKeys[GVR.RowIndex].Value.ToString());
                    TextBox txtAmount = (TextBox)GVR.FindControl("txtAmount");
                    DR["amount"] = (txtAmount.Text.Trim().Length > 0) ? Convert.ToDecimal(txtAmount.Text.Trim()) : 0;
                    DT.Rows.Add(DR);
                    DT.AcceptChanges();
                }
            }

            using (DataSet ds = new DataSet())
            {
                ds.Tables.Add(DT);
                HostelFees.feesdetailsxml = ds.GetXml().Replace("Table1>", "Table>");
            }

            int RowsAffected = objHostelFees.Save(HostelFees);
            if (RowsAffected != -1)
            {
                ClearControls();
                LoadFeesList();
                Message.IsSuccess = true;
                Message.Text = "Hostel Fees Saved/Updated Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate Fees Name Is Not Allowed";
            }
            Message.Show = true;

        }

        protected void dgvFeesList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            id = Convert.ToInt32(dgvFeesList.DataKeys[e.NewEditIndex].Value.ToString());
            LoadFeesDetails();
        }

       
    }
}
