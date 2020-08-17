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
using CollegeERP.Accounts;

namespace CollegeERP.Payroll
{
    public partial class EmployeeIncrementReport : System.Web.UI.Page
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
                if ((!HttpContext.Current.User.IsInRole(Entity.Common.Utility.EMPLOYEE_INCREMENT_REPORT)) && (Session["SuperAdmin"] == null))
                {
                    Response.Redirect("../Unauthorized.aspx");    
                }

                dgvEmployee.DataSource = null;
                dgvEmployee.DataBind();
                btnPrint.Visible = false;
                btnExport.Visible = false;
             }           
        }        
      
        protected void btnShow_Click(object sender, EventArgs e)
        {
            DateTime DateFrom = Convert.ToDateTime(txtDateFrom.Text.Trim() + " 00:00:00");
            DateTime DateTo = Convert.ToDateTime(txtDateTo.Text.Trim() + " 23:59:59");
          //*********************************************
            BusinessLayer.Payroll.EmployeeIncrement objEI = new BusinessLayer.Payroll.EmployeeIncrement();
            DataSet ds = new DataSet();
            ds = objEI.GetAllByDate(DateFrom, DateTo, 1);

            dgvEmployee.DataSource = ds.Tables[0];
            dgvEmployee.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
            {
                btnPrint.Visible = true;
                btnExport.Visible = true;
            }
            else
            {
                btnPrint.Visible = false;
                btnExport.Visible = false;
            }          
        }
       
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string Title = "Employee Increment Report";
            string[] _header = new string[1];
            _header[0] = "Date From: " + txtDateFrom.Text.Trim() + " To " + txtDateTo.Text;

            string[] _footer = new string[0];
            Print.ReportPrint(Title, _header, dgvEmployee, _footer);
            Response.Redirect("../Accounts/RPTShowGrid.aspx");
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string[] _header = new string[2];
            _header[0] = "Employee Increment Report";
            _header[1] = "Date From : " + txtDateFrom.Text.Trim() + " To " + txtDateTo.Text.Trim();
            
            string[] _footer = new string[0];
            string file = "Employee_Increment_Report";

            BusinessLayer.Common.Excel.SaveExcel(_header, dgvEmployee, _footer, file);
        }
    }
}
