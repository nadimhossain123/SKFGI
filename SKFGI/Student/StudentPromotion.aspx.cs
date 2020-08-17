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
    public partial class StudentPromotion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_PROMOTION))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                Message.Show = false;
                LoadBatch();
                LoadCourse();
                dgvStudent.DataSource = null;
                dgvStudent.DataBind();
            }
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

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStream();
            LoadSemNo();
            searchStudent();
        }

        protected void LoadSemNo()
        {
            ddlNewSemNo.Items.Clear();
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
                ddlNewSemNo.Items.Add(lst);
            }

            lst = new ListItem("--Select--", "0");
            ddlNewSemNo.Items.Insert(0, lst);
            ddlNewSemNo.SelectedIndex = 0;
        }

        protected void searchStudent()
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
            BusinessLayer.Student.StudentPromotion ObjPromotion = new BusinessLayer.Student.StudentPromotion();
            Entity.Student.StudentPromotion Promotion = new Entity.Student.StudentPromotion();
            Promotion.NewSemNo = Convert.ToInt32(ddlNewSemNo.SelectedValue.Trim());

            DataTable DT = new DataTable();
            DT.Columns.Add("student_id", typeof(int));
            DataRow DR;

            foreach (GridViewRow GVR in dgvStudent.Rows)
            {
                CheckBox Chk = (CheckBox)GVR.FindControl("ChkSelect");
                if (Chk.Checked)
                {
                    DR = DT.NewRow();
                    DR["student_id"] = int.Parse(dgvStudent.DataKeys[GVR.RowIndex].Value.ToString());
                    DT.Rows.Add(DR);
                    DT.AcceptChanges();
                }
            }

            using (DataSet ds = new DataSet())
            {
                ds.Tables.Add(DT);
                Promotion.StudentIDXML = ds.GetXml();
            }

            ObjPromotion.UpdatePromotion(Promotion);
            searchStudent();

            Message.IsSuccess = true;
            Message.Text = "Selected Students are Updated Successfully";
            Message.Show = true;
        }
    }
}
