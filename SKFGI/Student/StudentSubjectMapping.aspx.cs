using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CollegeERP.Student
{
    public partial class StudentSubjectMapping : System.Web.UI.Page
    {
        ListItem li = new ListItem("---SELECT---", "0");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("../Login.aspx");

            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_ELECTIVE_SUBJECT_MAPPING))
                    Response.Redirect("../Unauthorized.aspx");

                Message.Show = false;
                LoadBatch();
                LoadCourse();
                ddlElectiveSubject.Items.Add(li);
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
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BusinessLayer.Student.StudentSubjectMapping objMapping = new BusinessLayer.Student.StudentSubjectMapping();
            Entity.Student.StudentSubjectMapping mapping = new Entity.Student.StudentSubjectMapping();
            mapping.batch_id = Convert.ToInt32(ddlBatch.SelectedValue);
            mapping.CourseId = Convert.ToInt32(ddlCourse.SelectedValue);
            mapping.StreamId = Convert.ToInt32(ddlStream.SelectedValue);
            DataView DV = new DataView(objMapping.GetAll(mapping));
            
            dgvStudent.DataSource = DV;
            dgvStudent.DataBind();
            ChkSelect.Checked = false;

            //Load Elective Subjects
            BusinessLayer.student.Subject objSubjectMaster = new BusinessLayer.student.Subject();
            DV = new DataView(objSubjectMaster.GetAll());
            DV.RowFilter = "CourseId=" + ddlCourse.SelectedValue + " and StreamId=" + ddlStream.SelectedValue + " and ParentSubjectId_FK is not null";

            ddlElectiveSubject.DataSource = DV;
            ddlElectiveSubject.DataBind();
            ddlElectiveSubject.Items.Insert(0, li);
            Message.Show = false;

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Student.StudentSubjectMapping objMapping = new BusinessLayer.Student.StudentSubjectMapping();
            Entity.Student.StudentSubjectMapping mapping = new Entity.Student.StudentSubjectMapping();
            mapping.ElectiveSubjectId = Convert.ToInt32(ddlElectiveSubject.SelectedValue);
            
            DataTable DT = new DataTable();
            DT.Columns.Add("id", typeof(int));

            DataRow DR;

            foreach (GridViewRow GVR in dgvStudent.Rows)
            {
                if (GVR.RowType == DataControlRowType.DataRow)
                {
                    CheckBox Chk = (CheckBox)GVR.FindControl("ChkSelect");
                    if (Chk.Checked)
                    {
                        DR = DT.NewRow();
                        DR["id"] = Convert.ToInt32(dgvStudent.DataKeys[GVR.RowIndex].Value);
                        DT.Rows.Add(DR);
                        DT.AcceptChanges();
                    }
                }
            }

            using (DataSet ds = new DataSet())
            {
                ds.Tables.Add(DT);
                mapping.StudentIdXML= ds.GetXml().Replace("Table1>", "Table>");
            }

            objMapping.Save(mapping);
            btnSearch_Click(sender, e);
           
            Message.IsSuccess = true;
            Message.Text = "Saved Successfully";
            Message.Show = true;
        }

    }
}
