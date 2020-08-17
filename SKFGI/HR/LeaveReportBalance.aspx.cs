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
    public partial class LeaveReportBalance : System.Web.UI.Page
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
                if ((!HttpContext.Current.User.IsInRole(Entity.Common.Utility.EMPLOYEE_LEAVE_BALANCE_REPORT)) && (Session["SuperAdmin"] == null))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                txtFrom.Text = DateTime.Now.AddMonths(-1).ToString("dd MMM yyyy");
                txtTo.Text = DateTime.Now.ToString("dd MMM yyyy");
                LoadEmployee();
                LoadDepartment();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlDepartment.SelectedValue == "0" && ddlEmployee.SelectedValue == "0")
            {
                DateTime FromDate = Convert.ToDateTime(txtFrom.Text.Trim() + " 00:00:00");
                DateTime ToDate = Convert.ToDateTime(txtTo.Text.Trim() + " 23:59:59");
                BusinessLayer.HR.Leave ObjLeave = new BusinessLayer.HR.Leave();
                DataTable DT = ObjLeave.RPTEmployeeLeaveBalance(FromDate, ToDate);

                if (DT != null)
                {
                    dgvReport.DataSource = DT;
                    dgvReport.DataBind();
                }
            }
            else if (ddlDepartment.SelectedValue != "0" && ddlEmployee.SelectedValue == "0")
            {
                DateTime FromDate = Convert.ToDateTime(txtFrom.Text.Trim() + " 00:00:00");
                DateTime ToDate = Convert.ToDateTime(txtTo.Text.Trim() + " 23:59:59");
                BusinessLayer.HR.Leave ObjLeave = new BusinessLayer.HR.Leave();
                DataTable DT = ObjLeave.RPTEmployeeLeaveBalance(FromDate, ToDate);
                DataView dv;
                dv = DT.DefaultView;
                dv.RowFilter = "DepartmentId=" + int.Parse(ddlDepartment.SelectedValue.ToString());

                if (dv != null)
                {

                    dgvReport.DataSource = dv;
                    dgvReport.DataBind();
                }

            }
            else if (ddlDepartment.SelectedValue == "0" && ddlEmployee.SelectedValue != "0")
            {
                DateTime FromDate = Convert.ToDateTime(txtFrom.Text.Trim() + " 00:00:00");
                DateTime ToDate = Convert.ToDateTime(txtTo.Text.Trim() + " 23:59:59");
                BusinessLayer.HR.Leave ObjLeave = new BusinessLayer.HR.Leave();
                DataTable DT = ObjLeave.RPTEmployeeLeaveBalance(FromDate, ToDate);
                DataView dv;
                dv = DT.DefaultView;
                dv.RowFilter = "EmployeeId=" + int.Parse(ddlEmployee.SelectedValue.ToString());

                if (dv != null)
                {

                    dgvReport.DataSource = dv;
                    dgvReport.DataBind();
                }

            }
        }
        protected void LoadEmployee()
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
        protected void LoadDepartment()
        {
            BusinessLayer.HR.Leave objBL = new BusinessLayer.HR.Leave();
            DataView dv = new DataView(objBL.GetDepartment());

            DataTable dt = dv.ToTable();
            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["DepartmentId"] = "0";
                dr["DepartmentName"] = "--Select--";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                ddlDepartment.DataSource = dt;
                ddlDepartment.DataBind();

            }

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string[] _header = new string[2];
            _header[0] = "From: " + txtFrom.Text.Trim();
            _header[1] = "To: " + txtTo.Text.Trim();

            string[] _footer = new string[0];
            string file = "EMPLOYEE_WISE_LEAVE_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, dgvReport, _footer, file);
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEmployee();
        }

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDepartment();
        }
    }
}
