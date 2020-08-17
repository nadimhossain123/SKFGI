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
    public partial class StudentSectionMapping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("../Login.aspx");

            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_SECTION_MAPPING))
                    Response.Redirect("../Unauthorized.aspx");

                Message.Show = false;
                LoadBatch();
                LoadCourse();
                LoadSection();
                dgvStudent.DataSource = null;
                dgvStudent.DataBind();
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
            LoadStream();
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

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStream();
            searchStudent();
        }

        private void LoadSection()
        {
            BusinessLayer.Student.SectionMaster objSectionMaster = new BusinessLayer.Student.SectionMaster();
            DataTable DT = objSectionMaster.GetAll();

            ddlSection.DataSource = DT;
            ddlSection.DataBind();
            ddlSection.Items.Insert(0, new ListItem("---SELECT---", "0"));
        }

        private void searchStudent()
        {
            BusinessLayer.Student.StudentPromotion ObjPromotion = new BusinessLayer.Student.StudentPromotion();
            Entity.Student.StudentPromotion Promotion = new Entity.Student.StudentPromotion();
            Promotion.CourseId = int.Parse(ddlCourse.SelectedValue.Trim());
            Promotion.batch_id = int.Parse(ddlBatch.SelectedValue.Trim());
            Promotion.StreamId = int.Parse(ddlStream.SelectedValue.Trim());
            DataTable DT = ObjPromotion.GetStudentList(Promotion);

            if (DT != null)
            {
                dgvStudent.DataSource = DT;
                dgvStudent.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            searchStudent();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Student.StudentSectionMapping ObjSection = new BusinessLayer.Student.StudentSectionMapping();
            int section_id = Convert.ToInt32(ddlSection.SelectedValue);
            DataTable DT = new DataTable();
            DT.Columns.Add("id", typeof(int));
           
            DataRow DR;

            foreach (GridViewRow GVR in dgvStudent.Rows)
            {
                CheckBox Chk = (CheckBox)GVR.FindControl("ChkSelect");
                if (Chk.Checked)
                {
                    DR = DT.NewRow();
                    DR["id"] = int.Parse(dgvStudent.DataKeys[GVR.RowIndex].Value.ToString());
                   
                    DT.Rows.Add(DR);
                    DT.AcceptChanges();
                }
            }
            string xml_file;
            using (DataSet ds = new DataSet())
            {
                ds.Tables.Add(DT);
                xml_file = ds.GetXml().Replace("Table1>", "Table>");
            }

            ObjSection.UpdateSection(section_id,xml_file);
            searchStudent();

            Message.IsSuccess = true;
            Message.Text = "Successfully Mapped";
            Message.Show = true;
        }

    }
}

