using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CollegeERP.Student
{
    public partial class PopUpStudentSubjectMappingUpdate : System.Web.UI.Page
    {
        public int StudentId
        {
            get { return Convert.ToInt32(ViewState["StudentId"]); }
            set { ViewState["StudentId"] = value; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StudentId = Convert.ToInt32(Request.QueryString["StudentId"]);
                LoadElectiveSubjectList();
            }
        }

        private void LoadElectiveSubjectList()
        {
            BusinessLayer.Student.StudentSubjectMapping objMapping = new BusinessLayer.Student.StudentSubjectMapping();
            DataTable DT = objMapping.GetAllById(StudentId);

            dgvElectiveSubject.DataSource = DT;
            dgvElectiveSubject.DataBind();
        }

        protected void dgvElectiveSubject_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            BusinessLayer.Student.StudentSubjectMapping objMapping = new BusinessLayer.Student.StudentSubjectMapping();
            objMapping.Delete(Convert.ToInt32(dgvElectiveSubject.DataKeys[e.RowIndex].Value));
            LoadElectiveSubjectList();
        }
    }
}
