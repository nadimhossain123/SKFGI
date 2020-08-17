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

namespace CollegeERP.Payroll
{
    public partial class Form16 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
           
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.VIEW_FORM16))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadForm16();
            }
        }

        protected void LoadForm16()
        {
            BusinessLayer.Payroll.Form16 ObjForm16 = new BusinessLayer.Payroll.Form16();
            int EmployeeId = int.Parse(HttpContext.Current.User.Identity.Name);
            DataTable dt=new DataTable();
            if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.ALL_EMPLOYEES_FORM16))
            {
                dt = ObjForm16.GetAll(EmployeeId);
            }
            else
            {
                dt = ObjForm16.GetAll(0);
            }

            dgvForm16Generate.DataSource = dt;
            dgvForm16Generate.DataBind();
        }

        protected void dgvForm16Generate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int Id = Convert.ToInt32(dgvForm16Generate.DataKeys[e.Row.RowIndex].Value.ToString());
                ((ImageButton)e.Row.FindControl("btnPrint")).Attributes.Add("onclick", "javascript:openpopup('Form16Print.aspx?EmployeeId=" + Id + "','','600','700');");
            }
        }
    }
}
