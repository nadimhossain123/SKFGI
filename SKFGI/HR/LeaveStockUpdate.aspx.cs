using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CollegeERP.HR
{
    public partial class LeaveStockUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             if (Session["UserId"] == null)
                Response.Redirect("../Login.aspx");

             if (!IsPostBack)
             {
                 if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.LEAVE_STOCK_UPDATE))
                     Response.Redirect("../Unauthorized.aspx");

                 Message.Show = false;
                 btnUpdate.Text = "Update Leave Stock For " + DateTime.Now.Year;
             }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            BusinessLayer.HR.Leave objLeave = new BusinessLayer.HR.Leave();
            int RowsAffected = objLeave.UpdateLeaveStock();

            if (RowsAffected > 0)
            {
                Message.IsSuccess = true;
                Message.Text = "Leave Stock Updated Successfully For " + DateTime.Now.Year;
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Leave Stock Already Updated For " + DateTime.Now.Year;
            }
            Message.Show = true;
        }
    }
}
