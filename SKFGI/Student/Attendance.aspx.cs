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
    public partial class Attendance : System.Web.UI.Page
    {
        ListItem li = new ListItem("---SELECT---", "0");
        
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
            if (Session["UserId"] == null)
                Response.Redirect("../Login.aspx");

            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_ATTENDANCE))
                    Response.Redirect("../Unauthorized.aspx");

                ViewState["sortOrder"] = "";  
                LoadBatch();
                LoadCourse();
                LoadSection();
                LoadPeriod();
                ResetControls();
                btnDelete.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.ATTENDANCE_DELETE);             
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
            dv.RowFilter = "CompanyId=" + Session["CompanyId"].ToString() + "or CompanyId = 0";
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
            ddlStream.SelectedIndex = 0;
            LoadSubject();
        }

        private void LoadSubject()
        {
            int CourseId = Convert.ToInt32(ddlCourse.SelectedValue);
            int StreamId = Convert.ToInt32(ddlStream.SelectedValue);
            int EmployeeId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

            BusinessLayer.student.Subject ObjSubject = new BusinessLayer.student.Subject();
            DataView DV =new DataView(ObjSubject.GetAllSubjectByEmployee(CourseId, StreamId, EmployeeId));
            DV.RowFilter = "ParentSubjectId_FK is null";

            ddlSubject.DataSource = DV;
            ddlSubject.DataBind();
            ddlSubject.Items.Insert(0, li);
            ddlSubject.SelectedIndex = 0;

            LoadSubSubject();
        }

        private void LoadSubSubject()
        {
            int CourseId = Convert.ToInt32(ddlCourse.SelectedValue);
            int StreamId = Convert.ToInt32(ddlStream.SelectedValue);
            int EmployeeId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

            BusinessLayer.student.Subject ObjSubject = new BusinessLayer.student.Subject();
            DataView DV = new DataView(ObjSubject.GetAllSubjectByEmployee(CourseId, StreamId, EmployeeId));
            DV.RowFilter = "ParentSubjectId_FK = " + ddlSubject.SelectedValue;

            ddlSubSubject.DataSource = DV;
            ddlSubSubject.DataBind();
            ddlSubSubject.Items.Insert(0, li);
            ddlSubSubject.SelectedIndex = 0;
        }
        
        private void LoadSection()
        {
            BusinessLayer.Student.SectionMaster objSectionMaster = new BusinessLayer.Student.SectionMaster();
            DataTable DT = objSectionMaster.GetAll();

            ddlSection.DataSource = DT;
            ddlSection.DataBind();
            ddlSection.Items.Insert(0, li);
        }

        private void LoadPeriod()
        {
            DataTable DT = new DataTable();
            DT.Columns.Add("Period", typeof(int));
            DataRow DR;

            for (int i = -3; i <= 12; i++)
            {
                if (i != 0)
                {
                    DR = DT.NewRow();
                    DR[0] = i;
                    DT.Rows.Add(DR);
                    DT.AcceptChanges();
                }
            }
            ddlPeriod.DataSource = DT;
            ddlPeriod.DataBind();
            ddlPeriod.SelectedIndex = 0;
        }

        private void ResetControls()
        {
            Message.Show = false;
            
            ddlBatch.SelectedIndex = 0;
            ddlBatch.Enabled = true;

            ddlCourse.SelectedIndex = 0;
            ddlCourse.Enabled = true;
            LoadStream();
            ddlStream.Enabled = true;
            ddlSubject.Enabled = true;
            ddlSubSubject.Enabled = true;

            ddlSection.SelectedIndex = 0;
            ddlSection.Enabled = true;

            txtAttendanceDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            txtAttendanceDate.Enabled = true;
            ddlPeriod.SelectedValue = "1";

            ChkSelect.Checked = false;
            dgvStudent.DataSource = null;
            dgvStudent.DataBind();

            lblTotalPresent.Text = "0";
            lblTotalAbsent.Text = "0";
        }
               
        protected void btnShow_Click(object sender, EventArgs e)
        {
            DateTime AttendanceDate = Convert.ToDateTime(txtAttendanceDate.Text.Trim() + " 00:00:00");
            if (AttendanceDate > DateTime.Now)
            {
                Message.IsSuccess = false;
                Message.Text = "Attendance date should not be more than current date";
                Message.Show = true;
            }
            else if (ddlSubSubject.Items.Count > 1 && ddlSubSubject.SelectedIndex == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Select Sub-Subject";
                Message.Show = true;
            }
            else
            {
                BindGridView("", "");
                ddlBatch.Enabled = false;
                ddlCourse.Enabled = false;
                ddlStream.Enabled = false;
                ddlSection.Enabled = false;
                ddlSubject.Enabled = false;
                ddlSubSubject.Enabled = false;
                txtAttendanceDate.Enabled = false;

                Message.Show = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Student.StudentAttendance objStudentAttendance = new BusinessLayer.Student.StudentAttendance();
            Entity.Student.StudentAttendance attendance = new Entity.Student.StudentAttendance();
            attendance.BatchId = Convert.ToInt32(ddlBatch.SelectedValue);
            attendance.CourseId = Convert.ToInt32(ddlCourse.SelectedValue);
            attendance.StreamId = Convert.ToInt32(ddlStream.SelectedValue);
            attendance.SectionId = Convert.ToInt32(ddlSection.SelectedValue);
            attendance.SubjectId = Convert.ToInt32(ddlSubject.SelectedValue);
            attendance.AttendanceDate = Convert.ToDateTime(txtAttendanceDate.Text);
            attendance.Period = Convert.ToInt32(ddlPeriod.SelectedValue);
            attendance.Remarks = "";
            attendance.CreatedBy = Convert.ToInt32(Session["UserId"]);
            attendance.UpdateAccess = HttpContext.Current.User.IsInRole(Entity.Common.Utility.ATTENDANCE_UPDATE);

            DataTable DT = new DataTable();
            DT.Columns.Add("StudentId", typeof(int));
            DT.Columns.Add("IsPresent", typeof(bool));
            DataRow DR;

            foreach (GridViewRow GVR in dgvStudent.Rows)
            {
                if (GVR.RowType == DataControlRowType.DataRow)
                {
                    DR = DT.NewRow();
                    DR["StudentId"] = Convert.ToInt32(dgvStudent.DataKeys[GVR.RowIndex].Value);
                    DR["IsPresent"] = ((CheckBox)GVR.FindControl("ChkSelect")).Checked;
                    DT.Rows.Add(DR);
                    DT.AcceptChanges();
                }
            }

            using (DataSet ds = new DataSet())
            {
                ds.Tables.Add(DT);
                attendance.StudentIdXML = ds.GetXml().Replace("Table1>", "Table>");
            }
            string ReturnMessage = objStudentAttendance.Save(attendance);
            BindGridView("", "");

            if (ReturnMessage.Substring(0, 1) == "1")
            {
                Message.IsSuccess = true;
                Message.Text = ReturnMessage.Substring(2);
            }
            else if (ReturnMessage.Substring(0, 1) == "0")
            {
                Message.IsSuccess = false;
                Message.Text = ReturnMessage.Substring(2);
            }
            Message.Show = true;
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStream();
        }

        protected void ddlStream_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubject();
        }

        protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubSubject();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        private void BindGridView(string sortExp, string sortDir)
        {
            BusinessLayer.Student.StudentAttendance objStudentAttendance = new BusinessLayer.Student.StudentAttendance();
            Entity.Student.StudentAttendance attendance = new Entity.Student.StudentAttendance();
            attendance.BatchId = Convert.ToInt32(ddlBatch.SelectedValue);
            attendance.CourseId = Convert.ToInt32(ddlCourse.SelectedValue);
            attendance.StreamId = Convert.ToInt32(ddlStream.SelectedValue);
            attendance.SectionId = Convert.ToInt32(ddlSection.SelectedValue);
            attendance.SubjectId = Convert.ToInt32(ddlSubject.SelectedValue);
            attendance.SubSubjectId = Convert.ToInt32(ddlSubSubject.SelectedValue);
            attendance.AttendanceDate = Convert.ToDateTime(txtAttendanceDate.Text);
            attendance.Period = Convert.ToInt32(ddlPeriod.SelectedValue);

            DataSet ds = objStudentAttendance.GetAll(attendance);
            
            DataView myDataView = new DataView();
            myDataView = ds.Tables[0].DefaultView;

            if (sortExp != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
            }
            dgvStudent.DataSource = myDataView;
            dgvStudent.DataBind();

            DataRow[] DRs = ds.Tables[0].Select("IsPresent=1");
            int TotalPresent = DRs.Length;
            int TotalAbsent = ds.Tables[0].Rows.Count - TotalPresent;
            lblTotalPresent.Text = TotalPresent.ToString();
            lblTotalAbsent.Text = TotalAbsent.ToString();
            ChkSelect.Checked = false;
        }

        protected void dgvStudent_Sorting(object sender, GridViewSortEventArgs e)
        {
            BindGridView(e.SortExpression, sortOrder);
        }

        protected void dgvStudent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox ChkSelect = (CheckBox)e.Row.FindControl("ChkSelect");
                if (ChkSelect.Checked)
                    e.Row.CssClass = "GreenRowStyle";
                else
                    e.Row.CssClass = "RedRowStyle";
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            BusinessLayer.Student.StudentAttendance objStudentAttendance = new BusinessLayer.Student.StudentAttendance();
            Entity.Student.StudentAttendance attendance = new Entity.Student.StudentAttendance();
            attendance.BatchId = Convert.ToInt32(ddlBatch.SelectedValue);
            attendance.CourseId = Convert.ToInt32(ddlCourse.SelectedValue);
            attendance.StreamId = Convert.ToInt32(ddlStream.SelectedValue);
            attendance.SectionId = Convert.ToInt32(ddlSection.SelectedValue);
            attendance.SubjectId = Convert.ToInt32(ddlSubject.SelectedValue);
            attendance.AttendanceDate = Convert.ToDateTime(txtAttendanceDate.Text);
            attendance.Period = Convert.ToInt32(ddlPeriod.SelectedValue);
            int RowsAffected = objStudentAttendance.Delete(attendance);

            if (RowsAffected > 0)
            {
                Message.IsSuccess = true;
                Message.Text = "Attendance Deleted Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "No Attendance Record Found";
            }
            Message.Show = true;
            dgvStudent.DataSource = null;
            dgvStudent.DataBind();
            lblTotalPresent.Text = "0";
            lblTotalAbsent.Text = "0";
        }
    }
}
