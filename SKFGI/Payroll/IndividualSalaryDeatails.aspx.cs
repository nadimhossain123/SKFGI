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
using System.IO;
using CollegeERP.Accounts;

namespace CollegeERP.Payroll
{
    public partial class IndividualSalaryDeatails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.INDIVIDUAL_SALARY_DETAILS))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadEmployeeList();
            }
        }

        protected void LoadEmployeeList()
        {
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            DataView dv = new DataView(ObjEmployee.GetAll("", ""));
            dv.RowFilter = "CompanyId=" + Session["CompanyId"].ToString();

            if (dv != null)
            {
                ddlEmployee.DataSource = dv;
                ddlEmployee.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int EmployeeId = int.Parse(ddlEmployee.SelectedValue.Trim());
            int FinYr = int.Parse(ddlFinYear.SelectedValue.Trim());

            BusinessLayer.Payroll.EmployeeSalaryData ObjData = new BusinessLayer.Payroll.EmployeeSalaryData();
            DataSet ds = ObjData.GetIndividualSalaryDetails(EmployeeId, FinYr);

            if (ds.Tables[1].Rows.Count > 0)
            {
                txtEmployeeName.Text = ds.Tables[1].Rows[0]["EmpName"].ToString();
                txtEmpCode.Text = ds.Tables[1].Rows[0]["EmpCode"].ToString();
                txtDepartment.Text = ds.Tables[1].Rows[0]["DepartmentName"].ToString();
                txtDesignation.Text = ds.Tables[1].Rows[0]["DesignationName"].ToString();
                txtDOJ.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["DOJ"].ToString()).ToString("dd/MM/yyyy");
                txtPAN.Text = ds.Tables[1].Rows[0]["PANNo"].ToString();
            }

            //Basic salary and Present days 
            ds.Tables[0].Columns.Add("Present Days");
            ds.Tables[0].Columns.Add("Basic Salary");
            int Month = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Month = Convert.ToInt32(ds.Tables[0].Rows[i]["MonthNo"].ToString());
                DataView dv = new DataView(ds.Tables[2]);
                dv.RowFilter = "EmployeeSalaryDataMonth = " + Month;
                if (dv.ToTable().Rows.Count > 0)
                {
                    ds.Tables[0].Rows[i]["Present Days"] = dv.ToTable().Rows[0]["Attendance"].ToString();
                    ds.Tables[0].Rows[i]["Basic Salary"] = dv.ToTable().Rows[0]["BasicAmount"].ToString();
                }
            }


            //Salary Head
            for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
            {
                ds.Tables[0].Columns.Add(ds.Tables[3].Rows[i]["SalaryHeadDetails"].ToString());
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Month = Convert.ToInt32(ds.Tables[0].Rows[i]["MonthNo"].ToString());
                DataView dv = new DataView(ds.Tables[4]);
                dv.RowFilter = "EmployeeSalaryDataMonth = " + Month;
                if (dv.ToTable().Rows.Count > 0)
                {
                    for (int j = 0; j < dv.ToTable().Rows.Count; j++)
                    {
                        ds.Tables[0].Rows[i][dv.ToTable().Rows[j]["SalaryHeadDetails"].ToString()] = dv.ToTable().Rows[j]["HeadDataAmount"].ToString();
                    }
                }
            }

            //Salary Additional Head
            for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
            {
                ds.Tables[0].Columns.Add(ds.Tables[5].Rows[i]["SalaryAdditionalHeadDetails"].ToString());
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Month = Convert.ToInt32(ds.Tables[0].Rows[i]["MonthNo"].ToString());
                DataView dv = new DataView(ds.Tables[6]);
                dv.RowFilter = "EmployeeSalaryDataMonth = " + Month;
                if (dv.ToTable().Rows.Count > 0)
                {
                    for (int j = 0; j < dv.ToTable().Rows.Count; j++)
                    {
                        ds.Tables[0].Rows[i][dv.ToTable().Rows[j]["SalaryAdditionalHeadDetails"].ToString()] = dv.ToTable().Rows[j]["AdditionHeadAmount"].ToString();
                    }
                }
            }


            //Gross PF, ESI, P.Tax etc 
            ds.Tables[0].Columns.Add("Gross Salary");
            ds.Tables[0].Columns.Add("PF");
            ds.Tables[0].Columns.Add("ESIC");
            ds.Tables[0].Columns.Add("P.Tax");
            ds.Tables[0].Columns.Add("Loan");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Month = Convert.ToInt32(ds.Tables[0].Rows[i]["MonthNo"].ToString());
                DataView dv = new DataView(ds.Tables[7]);
                dv.RowFilter = "EmployeeSalaryDataMonth = " + Month;
                if (dv.ToTable().Rows.Count > 0)
                {
                    ds.Tables[0].Rows[i]["Gross Salary"] = dv.ToTable().Rows[0]["GrossSalary"].ToString();
                    ds.Tables[0].Rows[i]["PF"] = dv.ToTable().Rows[0]["PFAmount"].ToString();
                    ds.Tables[0].Rows[i]["ESIC"] = dv.ToTable().Rows[0]["ESIAmount"].ToString();
                    ds.Tables[0].Rows[i]["P.Tax"] = dv.ToTable().Rows[0]["PTaxAmount"].ToString();
                    ds.Tables[0].Rows[i]["Loan"] = dv.ToTable().Rows[0]["LoanAmount"].ToString();
                }
            }

            for (int i = 0; i < ds.Tables[8].Rows.Count; i++)
            {
                ds.Tables[0].Columns.Add(ds.Tables[8].Rows[i]["SalaryDeductionHeadDetails"].ToString());
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Month = Convert.ToInt32(ds.Tables[0].Rows[i]["MonthNo"].ToString());
                DataView dv = new DataView(ds.Tables[9]);
                dv.RowFilter = "EmployeeSalaryDataMonth = " + Month;

                if (dv.ToTable().Rows.Count > 0)
                {
                    for (int j = 0; j < dv.ToTable().Rows.Count; j++)
                    {
                        ds.Tables[0].Rows[i][dv.ToTable().Rows[j]["SalaryDeductionHeadDetails"].ToString()] = dv.ToTable().Rows[j]["DeductionHeadAmount"].ToString();
                    }
                }
            }

            ds.Tables[0].Columns.Add("Total Deduction");
            ds.Tables[0].Columns.Add("Net Salary");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Month = Convert.ToInt32(ds.Tables[0].Rows[i]["MonthNo"].ToString());
                DataView dv = new DataView(ds.Tables[7]);
                dv.RowFilter = "EmployeeSalaryDataMonth = " + Month;
                if (dv.ToTable().Rows.Count > 0)
                {
                    ds.Tables[0].Rows[i]["Total Deduction"] = dv.ToTable().Rows[0]["TotalDeduction"].ToString();
                    ds.Tables[0].Rows[i]["Net Salary"] = dv.ToTable().Rows[0]["NetSalary"].ToString();
                }
            }

            //Total Calculation
            decimal total = 0;
            DataRow dr = ds.Tables[0].NewRow();
            dr["MonthName"] = "Total";
            ds.Tables[0].Rows.Add(dr);
            ds.Tables[0].AcceptChanges();

            for (int col = 2; col < ds.Tables[0].Columns.Count; col++)
            {
                total = 0;
                for (int row = 0; row < ds.Tables[0].Rows.Count - 1; row++) //ds.Tables[0].Rows.Count - 1 means excluding footer row for showinh total
                {
                    if (ds.Tables[0].Rows[row][col].ToString() != "")
                    {
                        total += Convert.ToDecimal(ds.Tables[0].Rows[row][col].ToString());
                    }
                }
                ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][col] = total.ToString("F0");
            }

            ds.Tables[0].Columns.RemoveAt(0);

            dgvSalary.DataSource = ds.Tables[0];
            dgvSalary.DataBind();
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string[] _header = new string[8];
            _header[0] = "Emp Name: " + txtEmployeeName.Text;
            _header[1] = "Emp Code: " + txtEmpCode.Text;
            _header[2] = "Department: " + txtDepartment.Text;
            _header[3] = "Designation: " + txtDesignation.Text;
            _header[4] = "DOJ: " + txtDOJ.Text;
            _header[5] = "PAN No: " + txtPAN.Text;
            _header[6] = "Financial Year " + ddlFinYear.SelectedItem.Text;
            _header[7] = "Printed on " + DateTime.Now.ToString();

            string[] _footer = new string[0];
            string file = "INDIVIDUAL_SALARY_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, dgvSalary, _footer, file);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string Title = "Individual Salary Report";
            string[] _header = new string[8];
            _header[0] = "Emp Name: " + txtEmployeeName.Text;
            _header[1] = "Emp Code: " + txtEmpCode.Text;
            _header[2] = "Department: " + txtDepartment.Text;
            _header[3] = "Designation: " + txtDesignation.Text;
            _header[4] = "DOJ: " + txtDOJ.Text;
            _header[5] = "PAN No: " + txtPAN.Text;
            _header[6] = "Financial Year " + ddlFinYear.SelectedItem.Text;
            _header[7] = "Printed on " + DateTime.Now.ToString();

            string[] _footer = new string[0];

            Print.ReportPrint(Title, _header, dgvSalary, _footer);
            Response.Write("<script>");
            Response.Write("window.open('../Accounts/RPTShowGrid.aspx','_blank')");
            Response.Write("</script>");
            //Page.ClientScript.RegisterStartupScript(GetType(), "javascript", "window.open('../Accounts/RPTShowGrid.aspx')", true);
        }
    }
}
