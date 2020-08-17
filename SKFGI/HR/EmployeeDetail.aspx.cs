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
    public partial class EmployeeDetail : System.Web.UI.Page
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
                if ((!HttpContext.Current.User.IsInRole(Entity.Common.Utility.EMPLOYEE_DETAIL)) && ((Session["SuperAdmin"] == null)))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
               
                LoadDesignation();
                LoadDepartment();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlDesignation.SelectedValue != "0" && ddlDepartment.SelectedValue == "0")
            {
                BusinessLayer.Common.Employee objEmp = new BusinessLayer.Common.Employee();
                int DepartmentId = int.Parse(ddlDepartment.SelectedValue);
                int DesignationId = int.Parse(ddlDesignation.SelectedValue);
                DataTable dt = objEmp.GetEmpByDesigAndDept(DepartmentId, DesignationId);
                DataView dv = dt.DefaultView;
                dv.RowFilter = "EmployeeOfficial_DesignationId=" + int.Parse(ddlDesignation.SelectedValue);
                if (dv != null)
                {
                    dgvReport.DataSource = dv;
                    dgvReport.DataBind();
                }
            
            }
            else if (ddlDesignation.SelectedValue == "0" && ddlDepartment.SelectedValue != "0")
            {
                BusinessLayer.Common.Employee objEmp = new BusinessLayer.Common.Employee();
                int DepartmentId = int.Parse(ddlDepartment.SelectedValue);
                int DesignationId = int.Parse(ddlDesignation.SelectedValue);
                DataTable dt = objEmp.GetEmpByDesigAndDept(DepartmentId, DesignationId);
                DataView dv = dt.DefaultView;
                dv.RowFilter = "EmployeeOfficial_DepartmentId=" + int.Parse(ddlDepartment.SelectedValue);
                if (dv != null)
                {
                    dgvReport.DataSource = dv;
                    dgvReport.DataBind();
                }
            }
            else
            {
                BusinessLayer.Common.Employee objEmp = new BusinessLayer.Common.Employee();
                int DepartmentId = int.Parse(ddlDepartment.SelectedValue);
                int DesignationId = int.Parse(ddlDesignation.SelectedValue);
                DataTable dt = objEmp.GetEmpByDesigAndDept(DepartmentId, DesignationId);
                DataView dv = dt.DefaultView;
                if (dv != null)
                {
                    dgvReport.DataSource = dv;
                    dgvReport.DataBind();
                }
            }
            
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string[] _header = new string[1];
            _header[0] = "Employee Detail Report ";//+ txtFrom.Text.Trim();

            string[] _footer = new string[0];
            string file = "EMPLOYEE_DETAIL_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, dgvReport, _footer, file);
        }
        protected void LoadDesignation()
        {
            BusinessLayer.Common.Designation objDesig = new BusinessLayer.Common.Designation();
            DataView dv = new DataView(objDesig.GetAll());
            //dv.RowFilter = "EmployeeId <> " + EmployeeId;
            DataTable dt = dv.ToTable();

            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["DesignationId"] = "0";
                dr["DesignationName"] = "All";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();
                ddlDesignation.DataSource = dt;
                ddlDesignation.DataBind();


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
                dr["DepartmentName"] = "All";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                ddlDepartment.DataSource = dt;
                ddlDepartment.DataBind();
             
            }
        
        }
        protected void LoadEmployeeDetail()
        {
            BusinessLayer.Common.Employee objEmp = new BusinessLayer.Common.Employee();
            int DepartmentId=int.Parse(ddlDepartment.SelectedValue);
            int DesignationId=int.Parse(ddlDesignation.SelectedValue);
            DataTable dt = objEmp.GetEmpByDesigAndDept(DepartmentId, DesignationId);
            if (dt != null)
            {
                dgvReport.DataSource = dt;
                dgvReport.DataBind();
            }
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDesignation();
        }

        protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDepartment();
        }

        protected void dgvReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            e.Row.Cells[63].Visible = false;
            e.Row.Cells[64].Visible = false;
        }
    }
}
