using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CollegeERP
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.QueryString["SuperAdmin"] != null)
            {
                this.MasterPageFile = "SuperAdmin.Master";
            }
            else
            {
                this.MasterPageFile = "MasterAdmin.Master";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["UserId"] == null) && Request.QueryString["SuperAdmin"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}
