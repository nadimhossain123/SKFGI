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
using System.Text;

namespace CollegeERP.Accounts
{
    public partial class StudentCautionMoneyIndividualReport : System.Web.UI.Page
    {
        ListItem li = new ListItem("All", "0");

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
                if ((!HttpContext.Current.User.IsInRole(Entity.Common.Utility.CAUTION_MONEY_INDIVIDUAL_REPORT)) && (Session["SuperAdmin"] == null))
                    Response.Redirect("../Unauthorized.aspx");

                LoadFeesHead();
                LoadBatch();
                LoadCourse();
                LoadApprovedStudent();
                Message.Show = false;
                btnPrint.Visible = false;
                btnDownload.Visible = false;
            }
        }

        private void LoadFeesHead()
        {
            BusinessLayer.Student.StreamGroup ObjFees = new BusinessLayer.Student.StreamGroup();
            DataView DV = new DataView(ObjFees.GetAllFeesHead());
            DV.RowFilter = "IsRefundable=1";

            ddlFeesHead.DataSource = DV;
            ddlFeesHead.DataBind();
            ddlFeesHead.Items.Insert(0, li);
        }

        private void LoadBatch()
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

        private void LoadCourse()
        {
            BusinessLayer.Student.BTechRegistration ObjRegistration = new BusinessLayer.Student.BTechRegistration();
            Entity.Student.BTechRegistration Registration = new Entity.Student.BTechRegistration();
            Registration.intMode = 2;
            Registration.CourseId = 0; // Course Id is not required to fetch all courses
            DataTable DT = ObjRegistration.GetAllCommonSP(Registration);

            if (DT != null)
            {
                ddlCourse.DataSource = DT;
                ddlCourse.DataBind();
            }
            LoadStream();
        }

        private void LoadStream()
        {
            BusinessLayer.Student.BTechRegistration ObjRegistration = new BusinessLayer.Student.BTechRegistration();
            Entity.Student.BTechRegistration Registration = new Entity.Student.BTechRegistration();
            Registration.intMode = 3;
            Registration.CourseId = Convert.ToInt32(ddlCourse.SelectedValue);
            DataTable DT = ObjRegistration.GetAllCommonSP(Registration);
            
            if (DT != null)
            {
                ddlStream.DataSource = DT;
                ddlStream.DataBind();
            }
        }

        private void LoadApprovedStudent()
        {
            BusinessLayer.Student.LibraryFine ObjFine = new BusinessLayer.Student.LibraryFine();
            DataView DV = new DataView(ObjFine.GetApprovedStudentList());

            StringBuilder filter = new StringBuilder();
            filter.Append(@"company_id=" + Convert.ToInt32(Session["CompanyId"]));

            if (ddlBatch.SelectedIndex > 0)
                filter.Append(@" and batch_id=" + Convert.ToInt32(ddlBatch.SelectedValue));

            if (ddlCourse.SelectedIndex > 0)
                filter.Append(@" and CourseId=" + Convert.ToInt32(ddlCourse.SelectedValue));

            if (ddlStream.SelectedIndex > 0)
                filter.Append(@" and StreamId=" + Convert.ToInt32(ddlStream.SelectedValue));

            DV.RowFilter = filter.ToString();

            if (DV != null)
            {
                ddlStudent.DataSource = DV;
                ddlStudent.DataBind();
            }
            ddlStudent.Items.Insert(0, new ListItem("---SELECT---","0"));
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadApprovedStudent();
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStream();
            LoadApprovedStudent();
        }

        protected void ddlStream_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadApprovedStudent();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlStudent.SelectedValue != "0" && ddlStudent.Text != string.Empty)
            {
                Message.Show = false;
                decimal TotalReceivedAmount = 0;
                decimal TotalRefundAmount = 0;

                int StudentId = Convert.ToInt32(ddlStudent.SelectedValue);
                int FeesHeadId = Convert.ToInt32(ddlFeesHead.SelectedValue);
                DateTime? FromDate;
                DateTime? ToDate;

                if (txtFromDate.Text.Length > 0)
                    FromDate = Convert.ToDateTime(txtFromDate.Text + " 00:00:00");
                else
                    FromDate = null;

                if (txtToDate.Text.Length > 0)
                    ToDate = Convert.ToDateTime(txtToDate.Text + " 23:59:59");
                else
                    ToDate = null;

                BusinessLayer.Accounts.CautionMoneyReport objReport = new BusinessLayer.Accounts.CautionMoneyReport();
                DataSet ds = objReport.GetIndividualReport(StudentId, FeesHeadId, FromDate, ToDate);
                LoadStudentInfo(ds.Tables[0]);

                if (ds.Tables[1] != null)
                {
                    dgvReport.DataSource = ds.Tables[1];
                    dgvReport.DataBind();
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow DR in ds.Tables[1].Rows)
                    {
                        if (DR["ReceivedAmount"].ToString().Trim().Length > 0)
                            TotalReceivedAmount += Convert.ToDecimal(DR["ReceivedAmount"]);

                        if (DR["RefundAmount"].ToString().Trim().Length > 0)
                            TotalRefundAmount += Convert.ToDecimal(DR["RefundAmount"]);
                    }

                    ((Literal)dgvReport.FooterRow.FindControl("ltrTotalReceivedAmount")).Text = "<b>" + TotalReceivedAmount.ToString("n") + "</b>";
                    ((Literal)dgvReport.FooterRow.FindControl("ltrTotalRefundAmount")).Text = "<b>" + TotalRefundAmount.ToString("n") + "</b>";
                    ((Literal)dgvReport.FooterRow.FindControl("ltrTotalBalanceAmount")).Text = (TotalReceivedAmount - TotalRefundAmount).ToString("n") + "</b>";

                    btnDownload.Visible = true;
                    btnPrint.Visible = true;
                }
                else
                {
                    btnDownload.Visible = false;
                    btnPrint.Visible = false;
                }
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Please Select Student";
                Message.Show = true;
            }
        }

        private void LoadStudentInfo(DataTable DT)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<b>Student Name : " + DT.Rows[0]["name"].ToString() + "</b><br />");
            sb.Append(@"<b>Batch Name : " + DT.Rows[0]["batch_name"].ToString() + "</b><br />");
            sb.Append(@"<b>Course Name : " + DT.Rows[0]["CourseName"].ToString() + "</b><br />");
            sb.Append(@"<b>Stream Name : " + DT.Rows[0]["stream_name"].ToString() + "</b><br /><br />");
            ltrStudentInfo.Text = sb.ToString();
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string[] _header = new string[6];
            _header[0] = "CAUTION MONEY LEDGER";
            _header[1] = ltrStudentInfo.Text;
            _header[2] = "";
            _header[3] = "Fees Head: " + ddlFeesHead.SelectedItem.Text;
            _header[4] = "From: " + txtFromDate.Text;
            _header[5] = "To: " + txtToDate.Text;

            string[] _footer = new string[0];
            string file = "CAUTION_MONEY_LEDGER_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, dgvReport, _footer, file);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string[] _header = new string[6];
            _header[0] = "CAUTION MONEY LEDGER";
            _header[1] = ltrStudentInfo.Text;
            _header[2] = "";
            _header[3] = "Fees Head: " + ddlFeesHead.SelectedItem.Text;
            _header[4] = "From: " + txtFromDate.Text;
            _header[5] = "To: " + txtToDate.Text;

            string[] _footer = new string[0];

            Print.ReportPrint(Title, _header, dgvReport, _footer);
            Response.Redirect("RPTShowGrid.aspx");
        }
    }
}
