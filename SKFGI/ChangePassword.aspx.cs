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

namespace CollegeERP
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                Message.Show = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            Entity.Common.Employee Employee = new Entity.Common.Employee();
            Employee.EmployeeId = int.Parse(HttpContext.Current.User.Identity.Name);
            Employee.Password = txtPassword.Text.Trim();
            ObjEmployee.ChangePassword(Employee);

            Message.IsSuccess = true;
            Message.Text = "Your Password is Changed Successfully";
            Message.Show = true;
        }
    }
}
