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

namespace CollegeERP.Student
{
    public partial class PopUpLibraryFineVoucher : System.Web.UI.Page
    {
        public int LibraryFineId
        {
            get { return Convert.ToInt32(ViewState["LibraryFineId"]); }
            set { ViewState["LibraryFineId"] = value; }
        }

        public int StudentId
        {
            get { return Convert.ToInt32(ViewState["StudentId"]); }
            set { ViewState["StudentId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim().Length > 0)
            {
                LibraryFineId = Convert.ToInt32(Request.QueryString["id"].Trim());
                LoadFineDetails();
                Page.ClientScript.RegisterStartupScript(GetType(), "javascript", "window.print()", true);
            }
        }

        protected void LoadFineDetails()
        {
            BusinessLayer.Student.LibraryFine ObjFine = new BusinessLayer.Student.LibraryFine();
            Entity.Student.LibraryFine Fine = new Entity.Student.LibraryFine();
            Fine = ObjFine.GetAllById(LibraryFineId);
            if (Fine != null)
            {
                ltrVoucherNo.Text = Fine.VoucherNo;
                ltrDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                StudentId = Fine.StudentId;
                ltrReason.Text = Fine.ReasonForFine;
                ltrAmount.Text = Fine.FineAmount.ToString("#0.00");
                ltrSemNo.Text = "SEM " + Fine.SemNo.ToString();

                LoadStudentInformation();
            }
        }

        protected void LoadStudentInformation()
        {
            BusinessLayer.Student.ApproveStudent Aps = new BusinessLayer.Student.ApproveStudent();
            Entity.Student.ApproveStudent eAps = new Entity.Student.ApproveStudent();
            eAps.intMode = 6;
            eAps.intStudentID = StudentId;
            DataSet ds = new DataSet();
            ds = Aps.GetAllStudent(eAps);

            ltrStudentName.Text = ds.Tables[0].Rows[0]["name"].ToString();
            ltrStudentCode.Text = ds.Tables[0].Rows[0]["student_code"].ToString();
            ltrCourse.Text = ds.Tables[0].Rows[0]["CourseName"].ToString();
            ltrBatch.Text = ds.Tables[0].Rows[0]["batch_name"].ToString();

        }
    }
}
