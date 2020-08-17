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
using BusinessLayer.Accounts;

namespace CollegeERP.Student
{
    public partial class SemFeesGeneration : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.SEM_FEES_GENERATION))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                Message.Show = false;
                txtBillDate.Text = DateTime.Now.ToString("dd MMM yyyy");
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
            DataView dv = new DataView(ObjRegistration.GetAllCommonSP(Registration));
            dv.RowFilter = "CompanyId=" + Session["CompanyId"].ToString() + " Or CompanyId=0";
            if (dv != null)
            {
                ddlCourse.DataSource = dv;
                ddlCourse.DataBind();
            }
            LoadStream();
            LoadSemNo();
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

        protected void LoadSemNo()
        {
            ddlSemNo.Items.Clear();
            int CourseId = int.Parse(ddlCourse.SelectedValue.Trim());
            ListItem lst;
            int LastSemNo = 0;
            if (CourseId == 1 || CourseId == 3) //means MBA or MTech
            {
                LastSemNo = 4;
            }
            else if (CourseId == 2)
            {
                LastSemNo = 8; 
            }
            else if (CourseId == 4)
            {
                LastSemNo = 6;
            }
            else if (CourseId == 5)
            {
                LastSemNo = 6;
            }

            for (int i = 1; i <= LastSemNo; i++)
            {
                lst = new ListItem("Sem-" + i.ToString(), i.ToString());
                ddlSemNo.Items.Add(lst);
            }

            lst = new ListItem("--Select--", "0");
            ddlSemNo.Items.Insert(0, lst);
            ddlSemNo.SelectedValue = "0";
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
            LoadSemNo();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strValues = txtBillDate.Text.Trim();
            strValues += chr.ToString() + "";
            DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

            if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() == Session["FinYrID"].ToString().Trim())
            {
                BusinessLayer.Student.SemFeesGeneration ObjSemFees = new BusinessLayer.Student.SemFeesGeneration();
                Entity.Student.SemFeesGeneration SemFees = new Entity.Student.SemFeesGeneration();
                //int CourseId = Convert.ToInt32(ddlCourse.SelectedValue.Trim());
                SemFees.CompanyID_FK = Convert.ToInt32(Session["CompanyId"]);
                SemFees.BranchID_FK = Convert.ToInt32(Session["BranchId"].ToString());
                SemFees.FinYearID_FK = Convert.ToInt32(Session["FinYrID"].ToString());
                SemFees.DataFlow = Convert.ToInt32(Session["DataFlow"].ToString());
                SemFees.CourseId = Convert.ToInt32(ddlCourse.SelectedValue.Trim());
                SemFees.batch_id = Convert.ToInt32(ddlBatch.SelectedValue.Trim());
                SemFees.StreamId = Convert.ToInt32(ddlStream.SelectedValue.Trim());
                SemFees.SemNo = Convert.ToInt32(ddlSemNo.SelectedValue.Trim());
                SemFees.CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                SemFees.BillDate = Convert.ToDateTime(txtBillDate.Text.Trim() + " 00:00:00");
                SemFees.RowsAffected = 0;

                ObjSemFees.GenerateSemFees(SemFees);
                if (SemFees.RowsAffected > 0)
                {
                    Message.IsSuccess = true;
                    Message.Text = SemFees.RowsAffected + " Students Semester Fee Generated.";
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Sorry! No Students Semester Fee Generated.";
                }
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Please select a Date Between " + Session["SesFromDate"].ToString() + " & " + Session["SesToDate"].ToString();
            }
            Message.Show = true;
        }

    }
}
