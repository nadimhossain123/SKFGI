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

namespace CollegeERP.Payroll
{
    public partial class PopupSalaryHeadDetails : System.Web.UI.Page
    {
        public int SalaryHeadId
        {
            get { return Convert.ToInt32(ViewState["SalaryHeadId"]); }
            set { ViewState["SalaryHeadId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadHeadList();
                ClearControls();

            }
        }

        protected void LoadHeadList()
        {
            BusinessLayer.Payroll.SalaryHead ObjHead = new BusinessLayer.Payroll.SalaryHead();
            DataTable dt = ObjHead.GetAll();
            if (dt != null)
            {
                dgvHead.DataSource = dt;
                dgvHead.DataBind();
            }
        }

        protected void LoadHeadDetails()
        {
            BusinessLayer.Payroll.SalaryHead ObjHead = new BusinessLayer.Payroll.SalaryHead();
            Entity.Payroll.SalaryHead Head = new Entity.Payroll.SalaryHead();
            Head = ObjHead.GetAllById(SalaryHeadId);
            if (Head != null)
            {
                txtHead.Text = Head.SalaryHeadDetails;
                ChkIsFixed.Checked = Head.IsFixed;
                if (Head.MaxRange == null)
                    txtMaxRange.Text = "";
                else
                    txtMaxRange.Text = Head.MaxRange.ToString();

                Message.Show = false;
            }
        }

        protected void ClearControls()
        {
            SalaryHeadId = 0;
            txtHead.Text = "";
            ChkIsFixed.Checked = false;
            txtMaxRange.Text = "";
            Message.Show = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Payroll.SalaryHead ObjHead = new BusinessLayer.Payroll.SalaryHead();
            Entity.Payroll.SalaryHead Head = new Entity.Payroll.SalaryHead();
            Head.SalaryHeadId = SalaryHeadId;
            Head.SalaryHeadDetails = txtHead.Text.Trim();
            Head.IsFixed = ChkIsFixed.Checked;
            if (txtMaxRange.Text.Trim().Length > 0)
                Head.MaxRange = decimal.Parse(txtMaxRange.Text.Trim());
            else
                Head.MaxRange = null;

            int RowsAffected = ObjHead.Save(Head);

            if (RowsAffected != -1)
            {
                ClearControls();
                LoadHeadList();
                Message.IsSuccess = true;
                Message.Text = "Salary Head Saved Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate Salary Head Name Is Not Allowed";
            }
            Message.Show = true;
        }

        protected void dgvHead_RowEditing(object sender, GridViewEditEventArgs e)
        {
            SalaryHeadId = Convert.ToInt32(dgvHead.DataKeys[e.NewEditIndex].Value);
            LoadHeadDetails();
        }
    }
}
