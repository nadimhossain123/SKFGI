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
using CollegeERP.Accounts;

namespace CollegeERP.Student
{
    public partial class UserBaseApprovedStudentList : System.Web.UI.Page
    {

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["SuperAdmin"] != null)
            {
                this.MasterPageFile = "../SuperAdmin.Master";
            }
            else
            {
                this.MasterPageFile = "../MasterAdmin.Master";
            }
        }

        ListItem li = new ListItem("Select", "0");

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["UserId"] == null) && (Session["SuperAdmin"] == null))
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if ((!HttpContext.Current.User.IsInRole(Entity.Common.Utility.USER_BASE_APPROVED_STUDENT_LIST)) && (Session["SuperAdmin"] == null))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadUser();
                txtFromDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            }
        }

        protected void LoadUser()
        {
            BusinessLayer.Common.Employee objEmployee = new BusinessLayer.Common.Employee();
            DataView dv = new DataView(objEmployee.GetAll("", ""));
            dv.RowFilter = "CompanyId=" + Session["CompanyID"].ToString();

            if (dv != null)
            {
                ddlEmployee.DataSource = dv;
                ddlEmployee.DataBind();
            }
            ddlEmployee.Items.Insert(0, li);
        }

        protected void PopulateGrid()
        {
            int ApprovedBy = Convert.ToInt32(ddlEmployee.SelectedValue.Trim());
            DateTime FromDate = Convert.ToDateTime(txtFromDate.Text.Trim() + " 00:00:00");
            DateTime ToDate = Convert.ToDateTime(txtToDate.Text.Trim() + " 23:59:59");
            bool IsApproved = ChkIsApproved.Checked;

            BusinessLayer.Student.ApproveStudent objApprove = new BusinessLayer.Student.ApproveStudent();
            DataTable DT = objApprove.GetUserBaseApprovedStudentList(ApprovedBy, FromDate, ToDate, IsApproved);
            if (DT != null)
            {
                dgvStudent.DataSource = DT;
                dgvStudent.DataBind();

                if (DT.Rows.Count > 0)
                {
                    btnPrint.Visible = true;
                    btnExportExcel.Visible = true;
                }
                else
                {
                    btnPrint.Visible = false;
                    btnExportExcel.Visible = false;
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            string[] _header = new string[4];
            _header[0] = "<b>Supreme Knowledge Foundation Group of Institutions</b>";
            _header[1] = ((ChkIsApproved.Checked) ? "Approved" : "Rejected") + " Student List From " + txtFromDate.Text + " To " + txtToDate.Text + " by " + ddlEmployee.SelectedItem.Text;
            _header[2] = "Printed on " + DateTime.Now.ToString();
            _header[3] = "";

            string[] _footer = new string[0];

            string file = "STUDENT_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, dgvStudent, _footer, file);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string Title = "Approved/Rejected Student List";
            string[] _header = new string[2];
            _header[0] = ((ChkIsApproved.Checked) ? "Approved" : "Rejected") + " Student List From " + txtFromDate.Text + " To " + txtToDate.Text + " by " + ddlEmployee.SelectedItem.Text;
            _header[1] = "";

            string[] _footer = new string[0];

            Print.ReportPrint(Title, _header, dgvStudent, _footer);
            Response.Redirect("../Accounts/RPTShowGrid.aspx");
        }
    }
}
