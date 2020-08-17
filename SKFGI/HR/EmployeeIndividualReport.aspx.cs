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

namespace CollegeERP.HR
{
    public partial class EmployeeIndividualReport : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["SuperAdmin"] != null)
            {
                this.MasterPageFile = "../SuperAdmin.Master";
            }
            else
            {
                this.MasterPageFile = "../MasterAdmin.Master";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["UserId"] == null) && (Session["SuperAdmin"] == null))
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if ((!HttpContext.Current.User.IsInRole(Entity.Common.Utility.EMP_INDIVIDUAL_REPORT)) && ((Session["SuperAdmin"] == null)))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                BindEmployee();
                Message.Show = false;
            }
        }

        protected void BindEmployee()
        {
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            DataView dv = new DataView(ObjEmployee.GetAll("", ""));
            //dv.RowFilter = "EmployeeId <> " + EmployeeId;
            DataTable dt = dv.ToTable();

            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["EmployeeId"] = "0";
                dr["FullName"] = "--Select--";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                

                ddlEmployee.DataSource = dt;
                ddlEmployee.DataBind();

                
            }
        }      
      

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
           // LoadEmpManager(int.Parse(ddlEmployee.SelectedValue.ToString()), 0);
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {

            int Id =int.Parse(ddlEmployee.SelectedValue);
           // btnShow.Attributes.Add("onclientclick", "javascript:openpopup('EmpIndividualPrint.aspx?EmployeeId=" + Id + "','','600','700');");
            //string queryString = "billformat.aspx";
           // string jquery = "window.open('" + queryString + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "javascript:openpopup('EmpIndividualPrint.aspx?EmployeeId=" + Id + "','','600','700');", true);


        }
    }
}
