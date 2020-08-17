using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CollegeERP.Student
{
    public partial class allStudent : System.Web.UI.Page
    {
        public int id
        {
            get { return Convert.ToInt32(ViewState["id"]); }
            set { ViewState["id"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
                        
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.ALL_STUDENT))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                Clearcontrols();
                LoadBatch();
                LoadCourse();
                searchStudent();
            } 
        }

        protected void Clearcontrols()
        {
            id = 0;
            Message.Show = false;
            tblPhoto.Visible = false;
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
            DataView dv = new DataView(dt);
            dv.RowFilter = "CompanyId=" + Session["CompanyId"].ToString() + "or CompanyId = 0";
            if (dv != null)
            {
                ddlCourse.DataSource = dv;
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

        private void searchStudent()
        {
            int CourseId = int.Parse(ddlCourse.SelectedValue.Trim());
            BusinessLayer.Student.SearchStudent searchS = new BusinessLayer.Student.SearchStudent();
            Entity.Student.SearchStudent esearchS = new Entity.Student.SearchStudent();

            esearchS.intMode = 1;
            esearchS.appliation_no = txtApplicationNo.Text.Trim();
            esearchS.name = txtApplicantName.Text.Trim();
            esearchS.CourseId = CourseId;
            esearchS.StreamId = int.Parse(ddlStream.SelectedValue.Trim());
            esearchS.batch_id = int.Parse(ddlBatch.SelectedValue.Trim());
            DataTable dt = new DataTable();
            dt = searchS.GetAllStudent(esearchS);
            DataView dv = new DataView(dt);
            if (txtSubmittedBy.Text.Trim().Length > 0)
                dv.RowFilter = "company_id=" + Session["CompanyId"].ToString() + "AND ISNULL(SubmittedBy,'') LIKE'" + txtSubmittedBy.Text.Trim() + "%'";
            else
                dv.RowFilter = "company_id=" + Session["CompanyId"].ToString();

            if (dv != null)
            {
                dgvAllStudent.DataSource = dv;
                dgvAllStudent.DataBind();
            }
        }

        protected void dgvAllStudent_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int Id = Convert.ToInt32(dgvAllStudent.DataKeys[e.NewEditIndex].Values["id"].ToString());
            int CourseId = Convert.ToInt32(dgvAllStudent.DataKeys[e.NewEditIndex].Values["CourseId"].ToString());
            if (CourseId == 2)
            {
                Response.Redirect("BTechRegistration.aspx?id=" + Id);
            }
            else if (CourseId == 1)
            {
                Response.Redirect("MBARegistration.aspx?id="+Id);
            }
            else if (CourseId == 3)
            {
                Response.Redirect("MTechRegistration.aspx?id=" + Id);
            }
            else if (CourseId == 4)
            {
                Response.Redirect("DiplomaRegistration.aspx?id=" + Id);
            }
            else if (CourseId == 5)
            {
                Response.Redirect("NonAICTERegistration.aspx?id=" + Id);
            }
        }

        protected void dgvAllStudent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvAllStudent.PageIndex = e.NewPageIndex;
            searchStudent();
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStream();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            searchStudent();
        }

        protected void dgvAllStudent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Photo"))
            {
                id = Convert.ToInt32(e.CommandArgument.ToString());
                tblPhoto.Visible = true;
                Message.Show = false;
                uploadImage.Focus();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clearcontrols();
        }

        protected void btnChangePhoto_Click(object sender, EventArgs e)
        {
            BusinessLayer.Student.SearchStudent searchS = new BusinessLayer.Student.SearchStudent();
            Entity.Student.SearchStudent esearchS = new Entity.Student.SearchStudent();
            esearchS.intMode = 2;
            esearchS.id = id;

            string ff = "";
            if (uploadImage.PostedFile.FileName != null && uploadImage.PostedFile.ContentLength > 0)
            {
                string fn = uploadImage.FileName;
                string fileExt = System.IO.Path.GetExtension(fn);
                if (fileExt == ".jpg" || fileExt == ".bmp" || fileExt == ".JPG")
                {
                    string sm = Server.MapPath("");
                    esearchS.Photo = fileExt;
                    searchS.ChangeStudentPhoto(esearchS);
                    ff = (sm + "\\StudentPhoto\\" + esearchS.id + fileExt);
                    uploadImage.PostedFile.SaveAs(ff);
                    Clearcontrols();
                    searchStudent();
                    Message.IsSuccess = true;
                    Message.Text = "Student Photo Changed Successfully";
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Please Select JPG or BMP File";
                }
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Please Select Student Photo";
            }
            Message.Show = true;
        }

        protected void dgvAllStudent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                 int Id = Convert.ToInt32(dgvAllStudent.DataKeys[e.Row.RowIndex].Values["id"].ToString());
                 int Course_id = Convert.ToInt32(dgvAllStudent.DataKeys[e.Row.RowIndex].Values["CourseId"].ToString());
                ((Image)e.Row.FindControl("ImgIDCard")).Attributes.Add("onclick", "javascript:openIDpopup('StudentIDCard.aspx?id=" + Id + "');");

                if (Course_id == 2)
                    ((ImageButton)e.Row.FindControl("btnPrint")).Attributes.Add("onclick", "window.open('BTechRegistrationPrint.aspx?id=" + Id + "'); return false;");
                else if (Course_id == 1)
                    ((ImageButton)e.Row.FindControl("btnPrint")).Attributes.Add("onclick", "window.open('MBARegistrationPrint.aspx?id=" + Id + "'); return false;");
                else if (Course_id == 3)
                    ((ImageButton)e.Row.FindControl("btnPrint")).Attributes.Add("onclick", "window.open('MTechRegistrationPrint.aspx?id=" + Id + "'); return false;");
                else if (Course_id == 4)
                    ((ImageButton)e.Row.FindControl("btnPrint")).Attributes.Add("onclick", "window.open('DiplomaRegistrationPrint.aspx?id=" + Id + "'); return false;");
                else if (Course_id == 5)
                    ((ImageButton)e.Row.FindControl("btnPrint")).Attributes.Add("onclick", "window.open('NonAICTERegistrationPrint.aspx?id=" + Id + "'); return false;");
            }
        }
    }
}