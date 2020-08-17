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
using System.IO;
using CollegeERP.Accounts;
using System.Drawing;

namespace CollegeERP.Student
{
    public partial class AttendanceReport : System.Web.UI.Page
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

        ListItem li = new ListItem("---SELECT---", "0");
        private int lastCol = 0;
        private bool IsPrint = false;

        public string sortOrder
        {
            get
            {
                if (ViewState["sortOrder"].ToString() == "desc")
                {
                    ViewState["sortOrder"] = "asc";
                }
                else
                {
                    ViewState["sortOrder"] = "desc";
                }
                return ViewState["sortOrder"].ToString();
            }
            set
            {
                ViewState["sortOrder"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["UserId"] == null) && (Session["SuperAdmin"] == null))
                Response.Redirect("../Login.aspx");

            if (!IsPostBack)
            {
                if ((!HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_ATTENDANCE_REPORT)) && ((Session["SuperAdmin"] == null)))
                    Response.Redirect("../Unauthorized.aspx");

                ViewState["sortOrder"] = "";
                LoadBatch();
                LoadCourse();
                LoadStream();
                LoadSection();
                LoadStudentList();

                txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd MMM yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                dgvReport.DataSource = null;
                dgvReport.DataBind();
                btnPrint.Visible = false;
                btnDownload.Visible = false;
            }
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
            DataView dv = new DataView(dt);
            dv.RowFilter = "CompanyId=" + Session["CompanyId"] + "or CompanyId = 0";
            if (dv != null)
            {
                ddlCourse.DataSource = dv;
                ddlCourse.DataBind();
            }
        }

        private void LoadStream()
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

        private void LoadSection()
        {
            BusinessLayer.Student.SectionMaster objSectionMaster = new BusinessLayer.Student.SectionMaster();
            DataTable DT = objSectionMaster.GetAll();

            ddlSection.DataSource = DT;
            ddlSection.DataBind();
            ddlSection.Items.Insert(0, li);
        }

        private void LoadStudentList()
        {
            BusinessLayer.Student.LibraryFine ObjFine = new BusinessLayer.Student.LibraryFine();
            DataView DV = new DataView(ObjFine.GetApprovedStudentList());

            string filter = "1=1";

            if (ddlBatch.SelectedIndex > 0)
                filter += " and batch_id=" + ddlBatch.SelectedValue;

            if (ddlCourse.SelectedIndex > 0)
                filter += " and CourseId=" + ddlCourse.SelectedValue;

            if (ddlStream.SelectedIndex > 0)
                filter += " and StreamId=" + ddlStream.SelectedValue;

            if (ddlSection.SelectedIndex > 0)
                filter += " and section_id=" + ddlSection.SelectedValue;

            filter += "and company_id=" + Session["CompanyId"].ToString();

            DV.RowFilter = filter;
            ddlStudent.DataSource = DV;
            ddlStudent.DataBind();
            ddlStudent.Items.Insert(0, li);
        }

        protected void LoadSubjectWiseReport()
        {
            BusinessLayer.Student.StudentAttendance objStudentAttendance = new BusinessLayer.Student.StudentAttendance();
            Entity.Student.StudentAttendance attendance = new Entity.Student.StudentAttendance();
            attendance.BatchId = Convert.ToInt32(ddlBatch.SelectedValue);
            attendance.CourseId = Convert.ToInt32(ddlCourse.SelectedValue);
            attendance.StreamId = Convert.ToInt32(ddlStream.SelectedValue);
            attendance.SectionId = Convert.ToInt32(ddlSection.SelectedValue);
            attendance.StudentId = Convert.ToInt32(ddlStudent.SelectedValue);
            attendance.Fromdate = Convert.ToDateTime(txtFromDate.Text + " 00:00:00");
            attendance.Todate = Convert.ToDateTime(txtToDate.Text + " 23:59:59");
            attendance.SubjectType = Convert.ToInt32(ddlSubjectType.SelectedValue);

            DataSet ds = objStudentAttendance.GetSubjectWiseAttendanceReport(attendance);
            DataTable DTSubject = ds.Tables[0].Copy();
            DataTable DTAttendance = ds.Tables[1].Copy();
            DataTable DTStudent = ds.Tables[2].Copy();
            DataTable DTOverallAttendance = ds.Tables[3].Copy();

            int student_id = 0;
            int subject_id = 0;
            decimal TotalClass = 0;
            DataView DV;

            foreach (DataRow DR in DTSubject.Rows)
                DTStudent.Columns.Add(DR["SubjectId"].ToString());

            DataRow[] ArrDR = null;

            DTAttendance.Columns.Add("MinAttendance");

            for (int index = 0; index < DTAttendance.Rows.Count; ++index)
            {
                DataView dataView = new DataView(DTSubject);
                dataView.RowFilter = "SubjectId='" + DTAttendance.Rows[index]["SubjectId"].ToString() + "'";
                if (dataView.ToTable().Rows.Count > 0)
                    DTAttendance.Rows[index]["MinAttendance"] = (object)dataView.ToTable().Rows[0]["MinAttendance"].ToString();
                DTAttendance.AcceptChanges();
            }

            subject_id = 0;

            DataTable dtMarked = new DataTable();
            dtMarked.Columns.Add("StudentId");

            foreach (DataRow DR in DTStudent.Rows)
            {
                bool flag = false;
                student_id = Convert.ToInt32(DR["id"]);
                for (int col_index = 4; col_index < DTStudent.Columns.Count; col_index++)
                {
                    subject_id = Convert.ToInt32(DTStudent.Columns[col_index].ColumnName);
                    DV = new DataView(DTAttendance);
                    DV.RowFilter = "StudentId=" + student_id + " and SubjectId=" + subject_id;
                    if (DV.ToTable().Rows.Count > 0)
                    {
                        TotalClass = Convert.ToDecimal(DV.ToTable().Rows[0]["TotalClass"]);
                        decimal Present = Convert.ToDecimal(DV.ToTable().Rows[0]["TotalPresent"]);
                        decimal Percent = 0;

                        if (TotalClass > 0)
                            Percent = (Present * 100) / TotalClass;

                        if (Percent < Convert.ToDecimal(DV.ToTable().Rows[0]["MinAttendance"].ToString()))
                        {
                            DR[col_index] = "<span style='background:#FCAB41'>" + Present.ToString("N0") + "/" + TotalClass.ToString("N0") + " (" + Percent.ToString("0.00") + "%)</span>";
                            if (IsPrint)
                            {
                                flag = true;// New Addition Below 3 lines
                                dtMarked.Rows.Add();
                                dtMarked.Rows[dtMarked.Rows.Count - 1]["StudentId"] = student_id;
                                dtMarked.AcceptChanges();
                            }
                        }
                        else
                            DR[col_index] = Present.ToString("N0") + "/" + TotalClass.ToString("N0") + " (" + Percent.ToString("0.00") + "%)";
                    }
                    else
                    {
                        DR[col_index] = "<span style='background:#FCAB41'>0/0(0.00%)</span>";
                        if (IsPrint)
                        {
                            flag = true; // New Addition Below 3 Lines
                            dtMarked.Rows.Add();
                            dtMarked.Rows[dtMarked.Rows.Count - 1]["StudentId"] = student_id;
                            dtMarked.AcceptChanges();
                        }
                    }
                }

                if (flag)
                {
                    dtMarked.Rows.Add();
                    dtMarked.Rows[dtMarked.Rows.Count - 1]["StudentId"] = subject_id;
                    dtMarked.AcceptChanges();
                }
            }

            for (int col_index = 4; col_index < DTStudent.Columns.Count; col_index++)
            {
                subject_id = Convert.ToInt32(DTStudent.Columns[col_index].ColumnName);
                ArrDR = DTSubject.Select("SubjectId=" + subject_id);
                DTStudent.Columns[col_index].ColumnName = ArrDR[0]["SubjectCode"].ToString();
            }

            DTStudent.Columns.Add("Total");
            foreach (DataRow DR in DTStudent.Rows)
            {
                student_id = Convert.ToInt32(DR["id"]);
                ArrDR = DTOverallAttendance.Select("id=" + student_id);
                //DR[DTStudent.Columns.Count - 1] = ArrDR[0]["Present"].ToString() + "/" + ArrDR[0]["Total Class"].ToString() + " (" + ArrDR[0]["Present(%)"].ToString() + "%)";
                if (ArrDR.Length > 0)
                {
                    DR[DTStudent.Columns.Count - 1] = ArrDR[0]["Present(%)"].ToString() + "%";
                    if (IsPrint && txtMinTotalAttendance.Text != "" && Convert.ToDecimal(ArrDR[0]["Present(%)"].ToString()) <= Convert.ToDecimal(txtMinTotalAttendance.Text))
                    {
                        dtMarked.Rows.Add();
                        dtMarked.Rows[dtMarked.Rows.Count - 1]["StudentId"] = student_id;
                        dtMarked.AcceptChanges();
                    }
                }
                else
                {
                    DR[DTStudent.Columns.Count - 1] = "0%";
                    if (IsPrint)
                    {
                        dtMarked.Rows.Add();
                        dtMarked.Rows[dtMarked.Rows.Count - 1]["StudentId"] = student_id;
                        dtMarked.AcceptChanges();
                    }
                }
            }

            string str = "";
            for (int i = 0; i < dtMarked.Rows.Count; ++i)
            {
                if (i != 0)
                    str += ",";
                str += dtMarked.Rows[i]["StudentId"].ToString();
            }

            if (dtMarked.Rows.Count > 0)
            {
                DataView dv = new DataView(DTStudent);
                dv.RowFilter = ("Id in (" + str + ")");
                DTStudent = dv.ToTable();
                DTStudent.AcceptChanges();
            }

            DTStudent.Columns.RemoveAt(0);
            BindGridView(DTStudent, "", "");

            if (DTStudent.Rows.Count > 0)
            {
                btnPrint.Visible = true;
                btnDownload.Visible = true;
            }
            else
            {
                btnPrint.Visible = false;
                btnDownload.Visible = false;
            }
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStream();
            LoadStudentList();
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            IsPrint = this.chkPrintMarked.Checked;

            if (hdnPrintMode.Value == "1")
                LoadSubjectWiseReport();
            else
                btnSummaryReport_Click(sender, e);

            string[] _header = new string[8];
            _header[0] = "Batch: " + ((ddlBatch.SelectedIndex > 0) ? ddlBatch.SelectedItem.Text : "All");
            _header[1] = "Course: " + ((ddlCourse.SelectedIndex > 0) ? ddlCourse.SelectedItem.Text : "All");
            _header[2] = "Stream: " + ((ddlStream.SelectedIndex > 0) ? ddlStream.SelectedItem.Text : "All");
            _header[3] = "Section: " + ((ddlSection.SelectedIndex > 0) ? ddlSection.SelectedItem.Text : "All");
            _header[4] = "Student: " + ((ddlStudent.SelectedIndex > 0) ? ddlStudent.SelectedItem.Text : "All");
            _header[5] = "From: " + txtFromDate.Text;
            _header[6] = "To: " + txtToDate.Text;
            _header[7] = "";

            string[] _footer = new string[0];
            string file = "STUDENT_ATTENDANCE_REPORT";
            BusinessLayer.Common.Excel.SaveExcel(_header, dgvReport, _footer, file);
        }

        protected void dgvReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++, lastCol++)
                {
                    //e.Row.Cells[i].Text = Server.HtmlDecode(e.Row.Cells[i].Text);
                    e.Row.Cells[i].Wrap = false;
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (this.txtMinTotalAttendance.Text != "")
                {
                    if (Convert.ToDecimal(e.Row.Cells[lastCol - 1].Text.Split('%')[0]) < Convert.ToDecimal(txtMinTotalAttendance.Text))
                        e.Row.Cells[lastCol - 1].BackColor = Color.Orange;
                }
                for (int i = 0; i < e.Row.Cells.Count - 1; ++i)
                    e.Row.Cells[i].Text = this.Server.HtmlDecode(e.Row.Cells[i].Text);
            }
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStudentList();
        }

        protected void ddlStream_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStudentList();
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStudentList();
        }

        protected void btnSummaryReport_Click(object sender, EventArgs e)
        {
            hdnPrintMode.Value = "2";

            BusinessLayer.Student.StudentAttendance objStudentAttendance = new BusinessLayer.Student.StudentAttendance();
            Entity.Student.StudentAttendance attendance = new Entity.Student.StudentAttendance();
            attendance.BatchId = Convert.ToInt32(ddlBatch.SelectedValue);
            attendance.CourseId = Convert.ToInt32(ddlCourse.SelectedValue);
            attendance.StreamId = Convert.ToInt32(ddlStream.SelectedValue);
            attendance.SectionId = Convert.ToInt32(ddlSection.SelectedValue);
            attendance.StudentId = Convert.ToInt32(ddlStudent.SelectedValue);
            attendance.Fromdate = Convert.ToDateTime(txtFromDate.Text + " 00:00:00");
            attendance.Todate = Convert.ToDateTime(txtToDate.Text + " 23:59:59");
            attendance.SubjectType = Convert.ToInt32(ddlSubjectType.SelectedValue);

            DataTable DT = objStudentAttendance.GetAttendanceReport(attendance);

            if (IsPrint)
            {
                DataTable dtMarked = new DataTable();
                dtMarked.Columns.Add("StudentId");

                foreach (DataRow dr in DT.Rows)
                {
                    if (decimal.Parse(dr["Present(%)"].ToString()) < decimal.Parse(txtMinTotalAttendance.Text))
                    {
                        dtMarked.Rows.Add();
                        dtMarked.Rows[dtMarked.Rows.Count - 1]["StudentId"] = dr["Id"];
                        dtMarked.AcceptChanges();
                    }
                }

                string str = "";
                for (int i = 0; i < dtMarked.Rows.Count; ++i)
                {
                    if (i != 0)
                        str += ",";
                    str += dtMarked.Rows[i]["StudentId"].ToString();
                }

                if (dtMarked.Rows.Count > 0)
                {
                    DataView dv = new DataView(DT);
                    dv.RowFilter = ("Id in (" + str + ")");
                    DT = dv.ToTable();
                    DT.AcceptChanges();
                }
            }

            DT.Columns.RemoveAt(0);
            DT.AcceptChanges();

            BindGridView(DT, "", "");

            if (DT.Rows.Count > 0)
            {
                btnPrint.Visible = true;
                btnDownload.Visible = true;
            }
            else
            {
                btnPrint.Visible = false;
                btnDownload.Visible = false;
            }
        }

        protected void btnSubjectWiseReport_Click(object sender, EventArgs e)
        {
            hdnPrintMode.Value = "1";
            this.IsPrint = false;
            this.LoadSubjectWiseReport();
        }

        private void BindGridView(DataTable myDataTable, string sortExp, string sortDir)
        {
            DataView myDataView = new DataView();
            myDataView = myDataTable.DefaultView;

            if (sortExp != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
            }
            dgvReport.DataSource = myDataView;
            dgvReport.DataBind();
            Session["myDataTable"] = myDataTable;
        }

        protected void dgvReport_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable myDataTable = (DataTable)Session["myDataTable"];
            BindGridView(myDataTable, e.SortExpression, sortOrder);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            IsPrint = this.chkPrintMarked.Checked;

            if (hdnPrintMode.Value == "1")
                LoadSubjectWiseReport();
            else
                btnSummaryReport_Click(sender, e);

            string Title = "Student Attendance Report";
            string[] _header = new string[8];
            _header[0] = "Batch: " + ((ddlBatch.SelectedIndex > 0) ? ddlBatch.SelectedItem.Text : "All");
            _header[1] = "Course: " + ((ddlCourse.SelectedIndex > 0) ? ddlCourse.SelectedItem.Text : "All");
            _header[2] = "Stream: " + ((ddlStream.SelectedIndex > 0) ? ddlStream.SelectedItem.Text : "All");
            _header[3] = "Section: " + ((ddlSection.SelectedIndex > 0) ? ddlSection.SelectedItem.Text : "All");
            _header[4] = "Student: " + ((ddlStudent.SelectedIndex > 0) ? ddlStudent.SelectedItem.Text : "All");
            _header[5] = "From: " + txtFromDate.Text;
            _header[6] = "To: " + txtToDate.Text;
            _header[7] = "";

            string[] _footer = new string[0];

            Print.ReportPrint(Title, _header, dgvReport, _footer);
            Response.Redirect("../Accounts/RPTShowGrid.aspx");
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
    }
}
