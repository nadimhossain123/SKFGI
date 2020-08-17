using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace CollegeERP.Student
{
    public partial class AllFees : System.Web.UI.Page
    {
        public int CourseId
        {
            get { return Convert.ToInt32(ViewState["CourseId"]); }
            set { ViewState["CourseId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.ALL_FEES))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadCourse();
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
            populateAllFees();
        }

        private void populateAllFees()
        {
            CourseId = int.Parse(ddlCourse.SelectedValue.Trim());
            BusinessLayer.Student.StreamGroup searchS = new BusinessLayer.Student.StreamGroup();
            Entity.Student.StreamGroup esearchS = new Entity.Student.StreamGroup();

            esearchS.intMode = 10;
            esearchS.courseID = CourseId;
            esearchS.intCompanyId = Convert.ToInt32(Session["CompanyId"].ToString());
            DataTable dt = new DataTable();
            dt = searchS.AllFees(esearchS);
            if (dt != null)
            {
                dgvAllFees.DataSource = dt;
                dgvAllFees.DataBind();

            }
        }

        protected void dgvAllFees_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int Id = Convert.ToInt32(dgvAllFees.DataKeys[e.NewEditIndex].Value);
            Response.Redirect(ResolveClientUrl("Fees.aspx?id=" + Id));
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateAllFees();
        }
    }
}