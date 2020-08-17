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
    public partial class ApproveHostelList : System.Web.UI.Page
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
                if ((!HttpContext.Current.User.IsInRole(Entity.Common.Utility.APPROVE_HOSTEL_LIST)) && (Session["SuperAdmin"] == null))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                //LoadUser();
                //txtFromDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                //txtToDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                LoadBatch();
                LoadCourse();
            }
        }

        //protected void LoadUser()
        //{
        //    BusinessLayer.Common.Employee objEmployee = new BusinessLayer.Common.Employee();
        //    DataView dv = new DataView(objEmployee.GetAll("", ""));
        //    dv.RowFilter = "CompanyId=" + Session["CompanyID"].ToString();

        //    if (dv != null)
        //    {
        //        ddlEmployee.DataSource = dv;
        //        ddlEmployee.DataBind();
        //    }
        //    ddlEmployee.Items.Insert(0, li);
        //}
        protected void LoadStudent()
        {
            BusinessLayer.Student.ApproveHostel objAH = new BusinessLayer.Student.ApproveHostel();
            DataSet ds = objAH.GetStudentDetails(4);
            DataView dv = new DataView(ds.Tables[0]);
            //ltTotal.Text = "Total Hostel Alloted  :" + ds.Tables[1].Rows[0]["Total"].ToString();
            if (ddlBatch.SelectedIndex > 0 && ddlCourse.SelectedIndex == 0 && ddlStream.SelectedIndex == 0)
            {
                dv.RowFilter = "batch_id=" + int.Parse(ddlBatch.SelectedValue.Trim());
            }
            if (ddlBatch.SelectedIndex > 0 && ddlCourse.SelectedIndex > 0)
            {
                dv.RowFilter = "batch_id=" + int.Parse(ddlBatch.SelectedValue.Trim()) + "And " + "CourseId=" + int.Parse(ddlCourse.SelectedValue);
            }
            if (ddlBatch.SelectedIndex > 0 && ddlCourse.SelectedIndex > 0 && ddlStream.SelectedIndex > 0)
            {
                dv.RowFilter = "batch_id=" + int.Parse(ddlBatch.SelectedValue.Trim()) + "And " + "CourseId=" + int.Parse(ddlCourse.SelectedValue) + "And " + "StreamId=" + int.Parse(ddlStream.SelectedValue);
            }
            if (txtApplicantName.Text.Length > 0)
            {
                dv.RowFilter = "name like '" + this.txtApplicantName.Text + "%'";
            }

            if (ds.Tables.Count > 0)
            {
                dgvStudent.DataSource = dv;
                dgvStudent.DataBind();
            }
            if (ds.Tables.Count > 0)
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
        protected void PopulateGrid()
        {
            //int ApprovedBy = Convert.ToInt32(ddlEmployee.SelectedValue.Trim());
           // DateTime DateFrom = Convert.ToDateTime(txtFromDate.Text.Trim() + " 00:00:00");
           // DateTime DateTo = Convert.ToDateTime(txtToDate.Text.Trim() + " 00:00:00");
            //bool IsApproved = ChkIsApproved.Checked;

            //BusinessLayer.Student.ApproveHostel objAH=new BusinessLayer.Student.ApproveHostel();
            //DataSet ds = objAH.GetStudentDetailsByDate(4, DateFrom, DateTo);
            //if (ds.Tables.Count>0)
            //{
            //    dgvStudent.DataSource = ds.Tables[0];
            //    dgvStudent.DataBind();

            //    if (ds.Tables.Count > 0)
            //    {
            //        btnPrint.Visible = true;
            //        btnExportExcel.Visible = true;
            //    }
            //    else
            //    {
            //        btnPrint.Visible = false;
            //        btnExportExcel.Visible = false;
            //    }
            //}
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
        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStream();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadStudent();
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            string[] _header = new string[3];
            _header[0] = "<b>Supreme Knowledge Foundation Group of Institutions</b>";
           // _header[1] = "Approved" + " Student Hostel List From ";// +txtFromDate.Text + " To " + txtToDate.Text;// +" by " + ddlEmployee.SelectedItem.Text;
            _header[1] = "Printed on " + DateTime.Now.ToString();
            _header[2] = "";

            string[] _footer = new string[0];

            string file = "STUDENT_HOSTEL_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, dgvStudent, _footer, file);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string Title = "Approved/Rejected Student Hostel List";
            string[] _header = new string[1];
           // _header[0] = "Approved" + " Student Hostel List From ";// +txtFromDate.Text + " To " + txtToDate.Text; // +" by " + ddlEmployee.SelectedItem.Text;
            _header[0] = "";

            string[] _footer = new string[0];

            Print.ReportPrint(Title, _header, dgvStudent, _footer);
            Response.Redirect("../Accounts/RPTShowGrid.aspx");
        }
    }
}
