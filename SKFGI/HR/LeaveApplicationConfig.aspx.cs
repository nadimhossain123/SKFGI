using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CollegeERP.HR
{
    public partial class LeaveApplicationConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("../Login.aspx");

            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.LEAVE_APPLICATION_CONFIG))
                    Response.Redirect("../Unauthorized.aspx");

                txtMaxDayLimit.Text = BusinessLayer.HR.LeaveApplicationConfig.GetMaxDayLimit().ToString();
                Message.Show = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.HR.LeaveApplicationConfig.SaveMaxDayLimit(Convert.ToInt32(txtMaxDayLimit.Text));
            Message.IsSuccess = true;
            Message.Text = "Saved Successfully";
            Message.Show = true;
        }
    }
}
