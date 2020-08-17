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
    public partial class StudentIDCard : System.Web.UI.Page
    {
        public int StudentId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim().Length > 0)
                {
                    StudentId = Convert.ToInt32(Request.QueryString["id"].Trim());
                    LoadIDCardDetails();
                    Page.ClientScript.RegisterStartupScript(GetType(), "javascript", "window.print()", true);
                }
            }
        }

        protected void LoadIDCardDetails()
        {
            BusinessLayer.Student.SearchStudent ObjSearch = new BusinessLayer.Student.SearchStudent();
            DataTable dt = ObjSearch.GetStudentIDCard(StudentId);
            if (dt.Rows.Count > 0)
            {
                ImgID.ImageUrl = dt.Rows[0]["Photo"].ToString();
                ltrStudentName.Text = "Name : <b><span style='color:#27B3DE;'> " + dt.Rows[0]["name"].ToString().ToUpper() + "</span></b>";
                ltrStudentCode.Text = "Student ID : <span style='color:#3F7FC0;'>" + dt.Rows[0]["student_code"].ToString().ToUpper() + "</span>";
                ltrStream.Text = "Stream : <span style='color:#3F7FC0;'>" + dt.Rows[0]["stream_name"].ToString().ToUpper() + "</span>";
                ltrDOB.Text = "Date of Birth : <span style='color:#3F976A;'>" + dt.Rows[0]["dob"].ToString().ToUpper() + "</span>";
                ltrBloodGroup.Text = "Blood Group : <span style='color:#3F976A;'>" + dt.Rows[0]["BloodGroup"].ToString().ToUpper() + "</span>";
                ltrValid.Text = "Valid upto : <span style='color:#3F976A;'>" + dt.Rows[0]["ValidUpto"].ToString().ToUpper() + "</span>";

                ltrAddress.Text = "<b>Correspondence Address:</b> <span style='color:#27B3DE;'>" + dt.Rows[0]["address"].ToString().ToUpper() + "</span>";
                ltrS_Mobile.Text = "Ph.(Student) : <span style='color:#27B3DE;'>" + dt.Rows[0]["s_mobile"].ToString().ToUpper() + "</span>";
                ltrP_Mobile.Text = "Ph.(Residence) : <span style='color:#27B3DE;'>" + dt.Rows[0]["p_mobile"].ToString().ToUpper() + "</span>";

            }
        }
    }
}
