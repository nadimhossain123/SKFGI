using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CollegeERP.Student
{
    public partial class BulkSubjectAttendance : System.Web.UI.Page
    {
        private ListItem li = new ListItem("---SELECT---", "0");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSubjectList();
                ClearControls();
                Message.Show = false;
            }
        }

        protected void LoadSubjectList()
        {
            BusinessLayer.student.Subject objSubject = new BusinessLayer.student.Subject();
            DataTable all = new DataTable();

            all = objSubject.GetAll();
            if (all == null)
                return;
            this.dgvSubject.DataSource = all;
            this.dgvSubject.DataBind();
        }

        protected void ClearControls()
        {
            this.ddlSubjectType.SelectedIndex = 0;
            this.txtMinAttendance.Text = "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.student.Subject objSubject = new BusinessLayer.student.Subject();
            int i = objSubject.SubjectMinAttendance_Update((ddlSubjectType.SelectedIndex == 0) ? false : (this.ddlSubjectType.SelectedValue == "0") ? false : true, (txtMinAttendance.Text == "") ? 0 : int.Parse(this.txtMinAttendance.Text));
            if (i > 0)
            {
                this.Message.IsSuccess = true;
                this.Message.Text = "Updated successfully...";
            }
            else
            {
                this.Message.IsSuccess = false;
                this.Message.Text = "Please try again...";
            }
            this.Message.Show = true;
            this.ClearControls();
        }
    }
}
