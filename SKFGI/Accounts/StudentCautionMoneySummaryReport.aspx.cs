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

namespace CollegeERP.Accounts
{
    public partial class StudentCautionMoneySummaryReport : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["UserId"] == null) && (Session["SuperAdmin"] == null))
                Response.Redirect("../Login.aspx");

            if (!IsPostBack)
            {
                if ((!HttpContext.Current.User.IsInRole(Entity.Common.Utility.CAUTION_MONEY_SUMMARY_REPORT)) && (Session["SuperAdmin"] == null))
                    Response.Redirect("../Unauthorized.aspx");

                btnPrint.Visible = false;
                btnDownload.Visible = false;
                LoadCourse();
                LoadBatch();

                if (ddlBatch.Items.FindByText(DateTime.Now.Year.ToString()) != null)
                    ddlBatch.Items.FindByText(DateTime.Now.Year.ToString()).Selected = true;
            }
        }

        protected void LoadCourse()
        {
            BusinessLayer.Student.BTechRegistration ObjRegistration = new BusinessLayer.Student.BTechRegistration();
            Entity.Student.BTechRegistration Registration = new Entity.Student.BTechRegistration();
            Registration.intMode = 2;
            Registration.CourseId = 0; // Course Id is not required to fetch all courses
            DataTable dt = ObjRegistration.GetAllCommonSP(Registration);
            
            if (dt != null)
            {
                ddlCourse.DataSource = dt;
                ddlCourse.DataBind();
            }
            LoadStream();
        }

        protected void LoadStream()
        {
            BusinessLayer.Student.BTechRegistration ObjRegistration = new BusinessLayer.Student.BTechRegistration();
            Entity.Student.BTechRegistration Registration = new Entity.Student.BTechRegistration();
            Registration.intMode = 3;
            Registration.CourseId = Convert.ToInt32(ddlCourse.SelectedValue);
            DataTable dt = ObjRegistration.GetAllCommonSP(Registration);
            
            if (dt != null)
            {
                ddlStream.DataSource = dt;
                ddlStream.DataBind();
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

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStream();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int batch_id = Convert.ToInt32(ddlBatch.SelectedValue);
            int CourseId = Convert.ToInt32(ddlCourse.SelectedValue);
            int StreamId = Convert.ToInt32(ddlStream.SelectedValue);

            BusinessLayer.Accounts.CautionMoneyReport objReport = new BusinessLayer.Accounts.CautionMoneyReport();
            DataTable DT = objReport.GetSummaryReport(batch_id, CourseId, StreamId);

            if (DT != null)
            {
                dgvReport.DataSource = DT;
                dgvReport.DataBind();
            }

            decimal TotalReceivedAmount = 0;
            decimal TotalAdjustedAmount = 0;
            decimal TotalRefundedAmount = 0;
            decimal TotalBalanceAmount = 0;

            if (DT.Rows.Count > 0)
            {
                foreach (DataRow DR in DT.Rows)
                {
                    TotalReceivedAmount += Convert.ToDecimal(DR["ReceivedAmount"]);
                    TotalAdjustedAmount += Convert.ToDecimal(DR["AdjustedAmount"]);
                    TotalRefundedAmount += Convert.ToDecimal(DR["RefundedAmount"]);
                    TotalBalanceAmount += Convert.ToDecimal(DR["BalanceAmount"]);
                }
                ((Literal)dgvReport.FooterRow.FindControl("ltrTotalReceivedAmount")).Text = "<b>" + TotalReceivedAmount.ToString("n") + "</b>";
                ((Literal)dgvReport.FooterRow.FindControl("ltrTotalAdjustedAmount")).Text = "<b>" + TotalAdjustedAmount.ToString("n") + "</b>";
                ((Literal)dgvReport.FooterRow.FindControl("ltrTotalRefundedAmount")).Text = "<b>" + TotalRefundedAmount.ToString("n") + "</b>";
                ((Literal)dgvReport.FooterRow.FindControl("ltrTotalBalanceAmount")).Text = "<b>" + TotalBalanceAmount.ToString("n") + "</b>";

                btnPrint.Visible = true;
                btnDownload.Visible = true;
            }
            else
            {
                btnPrint.Visible = false;
                btnDownload.Visible = false;
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string[] _header = new string[3];
            _header[0] = "Course: " + ((ddlCourse.SelectedValue == "0") ? "All" : ddlCourse.SelectedItem.Text);
            _header[1] = "Batch: " + ((ddlBatch.SelectedValue == "0") ? "All" : ddlBatch.SelectedItem.Text);
            _header[2] = "Stream: " + ((ddlStream.SelectedValue == "0") ? "All" : ddlStream.SelectedItem.Text);

            string[] _footer = new string[0];
            string file = "CAUTION_MONEY_SUMMARY_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, dgvReport, _footer, file);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string Title = "Caution Money Summary Report";
            string[] _header = new string[3];
            _header[0] = "Course: " + ((ddlCourse.SelectedValue == "0") ? "All" : ddlCourse.SelectedItem.Text);
            _header[1] = "Batch: " + ((ddlBatch.SelectedValue == "0") ? "All" : ddlBatch.SelectedItem.Text);
            _header[2] = "Stream: " + ((ddlStream.SelectedValue == "0") ? "All" : ddlStream.SelectedItem.Text);

            string[] _footer = new string[0];

            Print.ReportPrint(Title, _header, dgvReport, _footer);
            Response.Redirect("../Accounts/RPTShowGrid.aspx");
        }
    }
}
