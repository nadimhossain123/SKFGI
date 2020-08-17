using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace CollegeERP.Common
{
    public partial class GradeMaster : System.Web.UI.Page
    {
        public int GradeId
        {
            get { return Convert.ToInt32(ViewState["GradeId"]); }
            set { ViewState["GradeId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGradeList();
                ClearControls();

            }
        }

        protected void LoadGradeList()
        {
            BusinessLayer.Common.Grade ObjGrade = new BusinessLayer.Common.Grade();
            DataTable dt = ObjGrade.GetAll();
            if (dt != null)
            {
                dgvGrade.DataSource = dt;
                dgvGrade.DataBind();
            }
        }

        protected void LoadGradeDetails()
        {
            BusinessLayer.Common.Grade ObjGrade = new BusinessLayer.Common.Grade();
            Entity.Common.Grade Grade = new Entity.Common.Grade();
            Grade = ObjGrade.GetAllById(GradeId);
            if (Grade != null)
            {
                txtGrade.Text = Grade.GradeName;
                Message.Show = false;
            }
        }

        protected void ClearControls()
        {
            GradeId = 0;
            txtGrade.Text = "";
            Message.Show = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.Grade ObjGrade = new BusinessLayer.Common.Grade();
            Entity.Common.Grade Grade = new Entity.Common.Grade();
            Grade.GradeId = GradeId;
            Grade.GradeName = txtGrade.Text.Trim();
            int RowsAffected= ObjGrade.Save(Grade);
            if (RowsAffected != -1)
            {
                ClearControls();
                LoadGradeList();
                Message.IsSuccess = true;
                Message.Text = "Grade Saved Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate Grade Name Is Not Allowed";
            }
            Message.Show = true;
        }

        protected void dgvGrade_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GradeId = Convert.ToInt32(dgvGrade.DataKeys[e.NewEditIndex].Value);
            LoadGradeDetails();
        }

        protected void dgvGrade_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = Convert.ToInt32(dgvGrade.DataKeys[e.RowIndex].Value);
            BusinessLayer.Common.Grade ObjGrade = new BusinessLayer.Common.Grade();
            int RowsAffected = ObjGrade.Delete(Id);
            if (RowsAffected != -1)
            {
                LoadGradeList();
                Message.Show = false;
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Delete. One or More Employee Is Attached With This Grade.";
                Message.Show = true;
            }
        }
    }
}
