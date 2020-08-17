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
    public partial class StudentOutstandingReport : System.Web.UI.Page
    {
        decimal SumTotBill = 0, SumTotRecd = 0;
        ListItem li = new ListItem("---SELECT---", "0");
        
        ListItem liS = new ListItem(" ", "0");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_OUTSTANDING_REPORT))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                Message.Show = false;
                btnPrint.Visible = false;
                btnDownload.Visible = false;
                LoadApprovedStudent();
                LoadBatch();
                // LoadStream();
                LoadCourse();
            }
        }

        protected void LoadApprovedStudent()
        {
            BusinessLayer.Student.LibraryFine ObjFine = new BusinessLayer.Student.LibraryFine();
            DataAccess.student.LibraryFine objDfine = new DataAccess.student.LibraryFine();
            DataView DV = new DataView(objDfine.GetApprovedStudentListWithDropOut());
            DV.RowFilter = "company_id=" + Convert.ToInt32(Session["CompanyId"].ToString());

            if (DV != null)
            {
                ddlStudent.DataSource = DV;
                ddlStudent.DataBind();
            }
            ddlStudent.Items.Insert(0, li);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlStudent.SelectedValue != "0" && ddlStudent.Text != string.Empty)
            {
                Message.Show = false;
                SumTotBill = 0;
                SumTotRecd = 0;
                
                BusinessLayer.Accounts.StudentFeesCollection ObjFees = new BusinessLayer.Accounts.StudentFeesCollection();
                int StudentId = int.Parse(ddlStudent.SelectedValue.Trim());
                string FromDate = txtFromDate.Text.Trim();
                string Todate = txtToDate.Text.Trim();

                DataSet ds = ObjFees.GetStudentOutstandingReport(StudentId,FromDate,Todate);
                LoadStudentInfo(ds.Tables[0]);

                if (ds.Tables[1] != null)
                {
                    dgvBill.DataSource = ds.Tables[1];
                    dgvBill.DataBind();
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    btnDownload.Visible = true;
                    btnPrint.Visible = true;
                    ((Label)dgvBill.FooterRow.FindControl("lblSumTotBill")).Text = SumTotBill.ToString("n");
                    ((Label)dgvBill.FooterRow.FindControl("lblSumTotRecd")).Text = SumTotRecd.ToString("n");
                    ((Label)dgvBill.FooterRow.FindControl("lblSumBalance")).Text = (SumTotBill - SumTotRecd).ToString("n");
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

        protected void LoadStudentInfo(DataTable DT)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<b>Student Name : " + DT.Rows[0]["name"].ToString() + "</b><br />");
            sb.Append(@"<b>Batch Name : " + DT.Rows[0]["batch_name"].ToString() + "</b><br />");
            sb.Append(@"<b>Course Name : " + DT.Rows[0]["CourseName"].ToString() + "</b><br />");
            sb.Append(@"<b>Stream Name : " + DT.Rows[0]["stream_name"].ToString() + "</b><br />");
            sb.Append(@"<b>Student Phone No : " + DT.Rows[0]["studentmobile"].ToString() + "</b><br />");
            sb.Append(@"<b>Parent Phone No : " + DT.Rows[0]["parentmobile"].ToString() + "</b><br /><br />");
            ltrStudentInfo.Text = sb.ToString();
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

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadApprovedStudent();
            BusinessLayer.Student.LibraryFine ObjFine = new BusinessLayer.Student.LibraryFine();
            DataView dv = new DataView(ObjFine.GetApprovedStudentList());
            dv.RowFilter = "batch_id=" + int.Parse(ddlBatch.SelectedValue.Trim()) + "AND " + "company_id=" + Convert.ToInt32(Session["CompanyId"].ToString());

            if (dv != null)
            {
                ddlStudent.DataSource = dv;
                ddlStudent.DataBind();
            }
            ddlStudent.Items.Insert(0, liS);
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStream();
        }

        protected void ddlStream_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadApprovedStudent();
            BusinessLayer.Student.LibraryFine ObjFine = new BusinessLayer.Student.LibraryFine();
            DataView dv = new DataView(ObjFine.GetApprovedStudentList());
            dv.RowFilter = "batch_id=" + int.Parse(ddlBatch.SelectedValue.Trim()) + "AND " + "company_id=" + Convert.ToInt32(Session["CompanyId"].ToString()) + "AND " + "CourseId=" + int.Parse(ddlCourse.SelectedValue.Trim()) + "AND " + "StreamId=" + int.Parse(ddlStream.SelectedValue.Trim());

            if (dv != null)
            {
                ddlStudent.DataSource = dv;
                ddlStudent.DataBind();
            }
            ddlStudent.Items.Insert(0, liS);
        }

        protected void dgvBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((Label)e.Row.FindControl("lblTotBill")).Text.Trim().Length > 0)
                    SumTotBill += Convert.ToDecimal(((Label)e.Row.FindControl("lblTotBill")).Text.Trim());

                if (((Label)e.Row.FindControl("lblTotRecd")).Text.Trim().Length > 0)
                    SumTotRecd += Convert.ToDecimal(((Label)e.Row.FindControl("lblTotRecd")).Text.Trim());
                //-------------------Add On 08-08-2013
                string fieldVal;
                fieldVal = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DocumentNo"));
                if (fieldVal.Length == 0)
                {
                    Label lblDate = (Label)e.Row.FindControl("lblDocumentDate");
                    lblDate.Visible = false;
                }
                //-------------------

            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string[] _header = new string[4];
            _header[0] = "Supreme Knowledge Foundation Group of Institutions";
            _header[1] = "Student Ledger (PRINTER) From " + txtFromDate.Text + " To " + txtToDate.Text + " Printed on " + DateTime.Now.ToString();
            _header[2] = "Student Name: " + ddlStudent.SelectedItem.Text;
            _header[3] = "";

            string[] _footer = new string[0];
            string file = "STUDENT_OUTSTANDING_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, dgvBill, _footer, file);
        }


        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string Title = "Student Outstanding Report";
            string[] _header = new string[3];
            _header[0] = "Student Ledger (PRINTER) From " + txtFromDate.Text + " To " + txtToDate.Text + " Printed on " + DateTime.Now.ToString();
            _header[1] = "Student Name: " + ddlStudent.SelectedItem.Text;
            _header[2] = "";

            string[] _footer = new string[0];

            Print.ReportPrint(Title, _header, dgvBill, _footer);
            Response.Redirect("RPTShowGrid.aspx");
        }
    }
}
