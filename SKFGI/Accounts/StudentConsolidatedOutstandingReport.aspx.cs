using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CollegeERP.Accounts
{
    public partial class StudentConsolidatedOutstandingReport : System.Web.UI.Page
    {
        ListItem li = new ListItem("---SELECT---", "0");
        decimal TotFees = 0, TotPaid = 0, TotDue = 0;

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
                if ((!HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_CONSOLIDATED_OUTSTANDING_REPORT)) && (Session["SuperAdmin"] == null))
                     Response.Redirect("../Unauthorized.aspx");

                 LoadCourse();
                 LoadBatch();
                 LoadFeesHead();
                 dgvBill.DataSource = null;
                 dgvBill.DataBind();
                 btnDownload.Visible = false;
            }
        }

        private void LoadFeesHead()
        {
            BusinessLayer.Student.StreamGroup ObjFees = new BusinessLayer.Student.StreamGroup();
            DataTable DT = ObjFees.GetAllFeesHead();

            ddlFeesHead.DataSource = DT;
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
            DataTable dt = ObjRegistration.GetAllCommonSP(Registration);
            if (dt != null)
            {
                ddlCourse.DataSource = dt;
                ddlCourse.DataBind();
            }
            LoadStream();
            LoadSemNo();
        }

        private void LoadStream()
        {
            int CourseId = int.Parse(ddlCourse.SelectedValue.Trim());
            BusinessLayer.Student.BTechRegistration ObjRegistration = new BusinessLayer.Student.BTechRegistration();
            Entity.Student.BTechRegistration Registration = new Entity.Student.BTechRegistration();
            Registration.intMode = 3;
            Registration.CourseId = CourseId;
            DataTable dt = ObjRegistration.GetAllCommonSP(Registration);
            if (dt != null)
            {
                ddlStream.DataSource = dt;
                ddlStream.DataBind();
            }
        }

        private void LoadSemNo()
        {
            ddlSemNo.Items.Clear();
            int CourseId = int.Parse(ddlCourse.SelectedValue.Trim());
            ListItem lst;
            int LastSemNo = 0;
            if (CourseId == 1 || CourseId == 3) //means MBA or MTech
            {
                LastSemNo = 4;
            }
            else { LastSemNo = 8; }

            for (int i = 1; i <= LastSemNo; i++)
            {
                lst = new ListItem("Sem-" + i.ToString(), i.ToString());
                ddlSemNo.Items.Add(lst);
            }

            ddlSemNo.Items.Insert(0, li);
            ddlSemNo.SelectedValue = "0";
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStream();
            LoadSemNo();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            TotFees = 0;
            TotPaid = 0;
            TotDue = 0;

            BusinessLayer.Student.SemFeesGeneration ObjSemFees = new BusinessLayer.Student.SemFeesGeneration();
            Entity.Student.SemFeesGeneration SemFees = new Entity.Student.SemFeesGeneration();
            if (txtFromDate.Text == "")
                SemFees.FromDate = null;
            else
                SemFees.FromDate = Convert.ToDateTime(txtFromDate.Text);

            if (txtToDate.Text == "")
                SemFees.ToDate = null;
            else
                SemFees.ToDate = Convert.ToDateTime(txtToDate.Text);

            SemFees.CourseId = int.Parse(ddlCourse.SelectedValue.Trim());
            SemFees.batch_id = int.Parse(ddlBatch.SelectedValue.Trim());
            SemFees.StreamId = int.Parse(ddlStream.SelectedValue.Trim());
            SemFees.SemNo = int.Parse(ddlSemNo.SelectedValue.Trim());
            SemFees.FeesHeadId = int.Parse(ddlFeesHead.SelectedValue.Trim());
            SemFees.ShowZeroDueBal = Convert.ToBoolean(chkShowZeroDue.Checked);

            DataTable dt = ObjSemFees.GetConsolidated_StudentOutstandingReport(SemFees);
            if (dt != null)
            {
                dgvBill.DataSource = dt;
                dgvBill.DataBind();
            }

            if (dt.Rows.Count > 0)
            {
                btnDownload.Visible = true;
                ((Literal)dgvBill.FooterRow.FindControl("ltrTotBillAmt")).Text = "<b>" + TotFees.ToString("#0.00") + "</b>";
                ((Literal)dgvBill.FooterRow.FindControl("ltrTotPaidAmt")).Text = "<b>" + TotPaid.ToString("#0.00") + "</b>";
                ((Literal)dgvBill.FooterRow.FindControl("ltrTotDueAmt")).Text = "<b>" + TotDue.ToString("#0.00") + "</b>";
            }
            else
            {
                btnDownload.Visible = false;
            }
        }

        protected void dgvBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
                TotFees += decimal.Parse(((Label)e.Row.FindControl("lblBillAmt")).Text.Trim());
                TotPaid += decimal.Parse(((Label)e.Row.FindControl("lblPaidAmt")).Text.Trim());
                TotDue += decimal.Parse(((Label)e.Row.FindControl("lblDueAmt")).Text.Trim());
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string[] _header = new string[5];
            _header[0] = "Course: " + ((ddlCourse.SelectedValue == "0") ? "All" : ddlCourse.SelectedItem.Text);
            _header[1] = "Batch: " + ((ddlBatch.SelectedValue == "0") ? "All" : ddlBatch.SelectedItem.Text);
            _header[2] = "Stream: " + ((ddlStream.SelectedValue == "0") ? "All" : ddlStream.SelectedItem.Text);
            _header[3] = "Sem: " + ((ddlSemNo.SelectedValue == "0") ? "All" : ddlSemNo.SelectedItem.Text);
            _header[4] = "Fees Head: " + ((ddlFeesHead.SelectedValue == "0") ? "All" : ddlFeesHead.SelectedItem.Text);

            string[] _footer = new string[0];
            string file = "CONSOLIDATED_STUDENT_OUTSTANDING_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, dgvBill, _footer, file);
        }
    
    }
}
