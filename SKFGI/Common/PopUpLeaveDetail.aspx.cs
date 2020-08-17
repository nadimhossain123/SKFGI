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
    public partial class PopUpLeaveDetail : System.Web.UI.Page
    {
        public int EmployeeId
        {
            get { return Convert.ToInt32(ViewState["EmployeeId"]); }
            set { ViewState["EmployeeId"] = value; }
        }

        public int LeaveTypeId
        {
            get { return Convert.ToInt32(ViewState["LeaveTypeId"]); }
            set { ViewState["LeaveTypeId"] = value; }
        }

        public int Month
        {
            get { return Convert.ToInt32(ViewState["Month"]); }
            set { ViewState["Month"] = value; }
        }

        public int Year
        {
            get { return Convert.ToInt32(ViewState["Year"]); }
            set { ViewState["Year"] = value; }
        }
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                Message.Show = false;
                //if (Request.QueryString["EmployeeId"] != null && Request.QueryString["EmployeeId"].Trim().Length > 0)
                //{
                //    EmployeeId = Convert.ToInt32(Request.QueryString["EmployeeId"].Trim());
                    
                //}
                GetLeaveDetail();
            }
        }
        protected void GetLeaveDetail()
        {
            BusinessLayer.Common.EmployeeAttendance objEmpAtnB = new BusinessLayer.Common.EmployeeAttendance();
            //LinkButton btnShow = sender as LinkButton;
            //GridViewRow gvrow = (GridViewRow)btnShow.NamingContainer;
            //int Year = int.Parse(ddlYear.SelectedValue.Trim());
            //int Month = int.Parse(ddlMonth.SelectedValue.Trim());
            DataTable dt = objEmpAtnB.GetLeaveDetailById(1, 2013, 1, 1);
            if (dt != null)
            {
                dgvLeaveDetail.DataSource = dt;
                dgvLeaveDetail.DataBind();
            }
        }


    }
}
