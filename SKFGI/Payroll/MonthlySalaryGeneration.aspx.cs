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
    public partial class MonthlySalaryGeneration : System.Web.UI.Page
    {
        BusinessLayer.Payroll.MonthYearList MonthYearList;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.MONTHLY_SALARY_GENERATION))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadYear();
                Message.Show = false;
            }
        }

        protected void LoadYear()
        {
            MonthYearList = new BusinessLayer.Payroll.MonthYearList();
            DataView dv = new DataView(MonthYearList.GetYear());
            dv.RowFilter = "YearNo <= " + DateTime.Now.Year;
            DataTable dt = dv.ToTable();

            DataRow dr = dt.NewRow();
            dr["YearNo"] = 0;
            dr["YearName"] = "--Select Year--";
            dt.Rows.InsertAt(dr, 0);
            dt.AcceptChanges();

            ddlYear.DataSource = dt;
            ddlYear.DataBind();

            LoadMonth();
        }

        protected void LoadMonth()
        {
            MonthYearList = new BusinessLayer.Payroll.MonthYearList();
            int Year = int.Parse(ddlYear.SelectedValue.Trim());
            if (Year != 0)
            {
                DataView dv = new DataView(MonthYearList.GetMonth());
                if (Year == DateTime.Now.Year)
                {
                    dv.RowFilter = "MonthNo <= " + DateTime.Now.Month;
                }

                DataTable dt = dv.ToTable();
                DataRow dr = dt.NewRow();
                dr["MonthNo"] = 0;
                dr["MonthName"] = "--Select Month--";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                ddlMonth.DataSource = dt;
                ddlMonth.DataBind();
            }
            else
            {

                ddlMonth.DataSource = MonthYearList.GetBlankMonthList();
                ddlMonth.DataBind();
            }
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMonth();
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            int Year = int.Parse(ddlYear.SelectedValue.Trim());
            int Month = int.Parse(ddlMonth.SelectedValue.Trim());
            int CompanyId = Convert.ToInt32(Session["CompanyId"].ToString());
            int CreatedBy = int.Parse(HttpContext.Current.User.Identity.Name);
            BusinessLayer.Payroll.EmployeeSalaryData ObjSalaryData = new BusinessLayer.Payroll.EmployeeSalaryData();

            string output = ObjSalaryData.MonthlySalaryGeneration(Month, Year, CreatedBy, CompanyId);

            int Status = Convert.ToInt32(output.Substring(0, 1));
            if (Status == 1)
            {
                Message.IsSuccess = true;
                Message.Text = output.Substring(2);
            }
            else if (Status == 0)
            {
                Message.IsSuccess = false;
                Message.Text = output.Substring(2);
            }
            Message.Show = true;
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            int Year = int.Parse(ddlYear.SelectedValue.Trim());
            int Month = int.Parse(ddlMonth.SelectedValue.Trim());
            int CompanyId = Convert.ToInt32(Session["CompanyId"].ToString());
            BusinessLayer.Payroll.EmployeeSalaryData ObjSalaryData = new BusinessLayer.Payroll.EmployeeSalaryData();
            DataSet ds = ObjSalaryData.GetAll(Month, Year, CompanyId);

            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                ds.Tables[0].Columns.Add(ds.Tables[1].Rows[i]["SalaryHeadDetails"].ToString());
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int EmployeeId = int.Parse(ds.Tables[0].Rows[i]["EmployeeId"].ToString());
                DataView dv = new DataView(ds.Tables[2]);
                dv.RowFilter = "EmployeeSalaryData_EmpId = " + EmployeeId;

                if (dv.ToTable().Rows.Count > 0)
                {
                    for (int j = 0; j < dv.ToTable().Rows.Count; j++)
                    {
                        ds.Tables[0].Rows[i][dv.ToTable().Rows[j]["SalaryHeadDetails"].ToString()] = dv.ToTable().Rows[j]["HeadDataAmount"].ToString();
                    }
                }
            }

            for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
            {
                ds.Tables[0].Columns.Add(ds.Tables[3].Rows[i]["SalaryAdditionalHeadDetails"].ToString());
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int EmployeeId = int.Parse(ds.Tables[0].Rows[i]["EmployeeId"].ToString());
                DataView dv = new DataView(ds.Tables[4]);
                dv.RowFilter = "EmployeeSalaryData_EmpId = " + EmployeeId;

                if (dv.ToTable().Rows.Count > 0)
                {
                    for (int j = 0; j < dv.ToTable().Rows.Count; j++)
                    {
                        ds.Tables[0].Rows[i][dv.ToTable().Rows[j]["SalaryAdditionalHeadDetails"].ToString()] = dv.ToTable().Rows[j]["AdditionHeadAmount"].ToString();
                    }
                }
            }


            ds.Tables[0].Columns.Add("Gross Salary");
            ds.Tables[0].Columns.Add("PF");
            ds.Tables[0].Columns.Add("ESIC");
            ds.Tables[0].Columns.Add("P.Tax");
            ds.Tables[0].Columns.Add("Loan");
           
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int EmployeeId = int.Parse(ds.Tables[0].Rows[i]["EmployeeId"].ToString());
                DataView dv = new DataView(ds.Tables[5]);
                dv.RowFilter = "EmployeeSalaryData_EmpId = " + EmployeeId;
                if (dv.ToTable().Rows.Count > 0)
                {
                   ds.Tables[0].Rows[i]["Gross Salary"] = dv.ToTable().Rows[0]["Gross Salary"].ToString();
                   ds.Tables[0].Rows[i]["PF"] = dv.ToTable().Rows[0]["PF"].ToString();
                   ds.Tables[0].Rows[i]["ESIC"] = dv.ToTable().Rows[0]["ESIC"].ToString();
                   ds.Tables[0].Rows[i]["P.Tax"] = dv.ToTable().Rows[0]["P.Tax"].ToString();
                   ds.Tables[0].Rows[i]["Loan"] = dv.ToTable().Rows[0]["Loan"].ToString();
                }
            }


            for (int i = 0; i < ds.Tables[6].Rows.Count; i++)
            {
                ds.Tables[0].Columns.Add(ds.Tables[6].Rows[i]["SalaryDeductionHeadDetails"].ToString());
            }

            //****************************TOTAL CAL**************
            decimal TotalIncomeTax = 0;
            decimal TotMisl = 0;
            //****************************************************
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int EmployeeId = int.Parse(ds.Tables[0].Rows[i]["EmployeeId"].ToString());
                DataView dv = new DataView(ds.Tables[7]);
                dv.RowFilter = "EmployeeSalaryData_EmpId = " + EmployeeId;

                if (dv.ToTable().Rows.Count > 0)
                {
                    for (int j = 0; j < dv.ToTable().Rows.Count; j++)
                    {
                        ds.Tables[0].Rows[i][dv.ToTable().Rows[j]["SalaryDeductionHeadDetails"].ToString()] = dv.ToTable().Rows[j]["DeductionHeadAmount"].ToString();
                        //****************************TOTAL CAL**************
                        
                        if (dv.ToTable().Rows[j]["SalaryDeductionHeadDetails"].ToString() == "Income Tax" && dv.ToTable().Rows[j]["DeductionHeadAmount"].ToString().Trim().Length > 0 )
                        { TotalIncomeTax += Convert.ToDecimal(dv.ToTable().Rows[j]["DeductionHeadAmount"].ToString()); }
                        if (dv.ToTable().Rows[j]["SalaryDeductionHeadDetails"].ToString() == "Mislennious" && dv.ToTable().Rows[j]["DeductionHeadAmount"].ToString().Trim().Length > 0)
                        { TotMisl += Convert.ToDecimal(dv.ToTable().Rows[j]["DeductionHeadAmount"].ToString()); }
                        //****************************************************
                    
                    }
                }
            }


            ds.Tables[0].Columns.Add("Total Deduction");
            ds.Tables[0].Columns.Add("Net Salary");
            ds.Tables[0].Columns.Add("A/C No");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int EmployeeId = int.Parse(ds.Tables[0].Rows[i]["EmployeeId"].ToString());
                DataView dv = new DataView(ds.Tables[5]);
                dv.RowFilter = "EmployeeSalaryData_EmpId = " + EmployeeId;
                if (dv.ToTable().Rows.Count > 0)
                {
                    ds.Tables[0].Rows[i]["Total Deduction"] = dv.ToTable().Rows[0]["Total Deduction"].ToString();
                    ds.Tables[0].Rows[i]["Net Salary"] = dv.ToTable().Rows[0]["Net Salary"].ToString();
                    ds.Tables[0].Rows[i]["A/C No"] = dv.ToTable().Rows[0]["A/C No"].ToString();
                }
            }
            
            ds.Tables[0].Columns.Add("Signature");

            ds.Tables[0].Columns[0].ColumnName = "SL";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ds.Tables[0].Rows[i][0] = (i + 1).ToString();
            }
            //********************************************
            decimal TotalGross = 0;
            decimal TotalPf = 0;
            decimal TotalEsic = 0;
            decimal TotalPTax = 0;
            decimal TotalLoan = 0;
            
            decimal TotalDeduction = 0;
            decimal TotalNet = 0;
            if (ds.Tables[5].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
                {
                    if (ds.Tables[5].Rows[i]["Gross Salary"].ToString().Trim().Length > 0)
                        TotalGross += Convert.ToDecimal(ds.Tables[5].Rows[i]["Gross Salary"].ToString());
                    if (ds.Tables[5].Rows[i]["PF"].ToString().Trim().Length > 0)
                        TotalPf += Convert.ToDecimal(ds.Tables[5].Rows[i]["PF"].ToString());
                    if (ds.Tables[5].Rows[i]["ESIC"].ToString().Trim().Length > 0)
                        TotalEsic += Convert.ToDecimal(ds.Tables[5].Rows[i]["ESIC"].ToString());
                    if (ds.Tables[5].Rows[i]["P.Tax"].ToString().Trim().Length > 0)
                        TotalPTax += Convert.ToDecimal(ds.Tables[5].Rows[i]["P.Tax"].ToString());
                    if (ds.Tables[5].Rows[i]["Loan"].ToString().Trim().Length > 0)
                        TotalLoan += Convert.ToDecimal(ds.Tables[5].Rows[i]["Loan"].ToString());
                    if (ds.Tables[5].Rows[i]["Total Deduction"].ToString().Trim().Length > 0)
                        TotalDeduction += Convert.ToDecimal(ds.Tables[5].Rows[i]["Total Deduction"].ToString());
                    if (ds.Tables[5].Rows[i]["Net Salary"].ToString().Trim().Length > 0)
                        TotalNet += Convert.ToDecimal(ds.Tables[5].Rows[i]["Net Salary"].ToString());
                    
                }
            }
            DataRow dr = ds.Tables[0].NewRow();
            dr["Arrear"] = "Total:";
            
            dr["Gross Salary"] = TotalGross.ToString("F2");
            dr["PF"] = TotalPf.ToString("F2");
            dr["ESIC"] = TotalEsic.ToString("F2");
            dr["P.Tax"] = TotalPTax.ToString("F2");
            dr["Loan"] = TotalLoan.ToString("F2");
            //dr[23] = TotalIncomeTax.ToString("F2");
            //dr[24] = TotMisl.ToString("F2");
            dr["Total Deduction"] = TotalDeduction.ToString("F2");
            dr["Net Salary"] = TotalNet.ToString("F2");
            ds.Tables[0].Rows.Add(dr);
            ds.Tables[0].AcceptChanges();
            //********************************************
            dgvSalary.DataSource = ds.Tables[0];
            dgvSalary.DataBind();

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string[] _header = new string[2];
            _header[0] = "Year: " + ddlYear.SelectedItem.Text;
            _header[1] = "Month: " + ddlMonth.SelectedItem.Text;

            string[] _footer = new string[0];
            string file = "SALARY_REGISTRATION_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, dgvSalary, _footer, file);
        }

        protected void btnPaymentAdvise_Click(object sender, EventArgs e)
        {
            int Year = int.Parse(ddlYear.SelectedValue.Trim());
            int Month = int.Parse(ddlMonth.SelectedValue.Trim());
            int CompanyId = Convert.ToInt32(Session["CompanyId"].ToString());
            BusinessLayer.Payroll.EmployeeSalaryData ObjSalaryData = new BusinessLayer.Payroll.EmployeeSalaryData();
            DataSet ds = ObjSalaryData.GetAll(Month, Year, CompanyId);

            decimal Total = 0;
            if (ds.Tables[8].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[8].Rows.Count; i++)
                {
                    if (ds.Tables[8].Rows[i][3].ToString().Trim().Length > 0)
                        Total += Convert.ToDecimal(ds.Tables[8].Rows[i][3].ToString());
                }
            }

            DataRow dr = ds.Tables[8].NewRow();
            dr[2] = "Total:";
            dr[3] = Total.ToString("F0");
            ds.Tables[8].Rows.Add(dr);
            ds.Tables[8].AcceptChanges();

            dgvSalary.DataSource = ds.Tables[8];
            dgvSalary.DataBind();
        }

        protected void btnFinalize_Click(object sender, EventArgs e)
        {
            int Year = int.Parse(ddlYear.SelectedValue.Trim());
            int Month = int.Parse(ddlMonth.SelectedValue.Trim());
            int CompanyId = Convert.ToInt32(Session["CompanyId"].ToString());
            BusinessLayer.Payroll.EmployeeSalaryData ObjSalaryData = new BusinessLayer.Payroll.EmployeeSalaryData();
            ObjSalaryData.FinalizeSalary(Month, Year, CompanyId);

            Message.IsSuccess = true;
            Message.Text = "Salary Finalized Successfully";
            Message.Show = true;
        }

        protected void btnPaymentAdviseOTH_Click(object sender, EventArgs e)
        {
            int Year = int.Parse(ddlYear.SelectedValue.Trim());
            int Month = int.Parse(ddlMonth.SelectedValue.Trim());
            int CompanyId = Convert.ToInt32(Session["CompanyId"].ToString());
            BusinessLayer.Payroll.EmployeeSalaryData ObjSalaryData = new BusinessLayer.Payroll.EmployeeSalaryData();
            DataSet ds = ObjSalaryData.GetAll(Month, Year, CompanyId);

            decimal Total = 0;
            if (ds.Tables[9].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[9].Rows.Count; i++)
                {
                    if (ds.Tables[9].Rows[i][3].ToString().Trim().Length > 0)
                        Total += Convert.ToDecimal(ds.Tables[9].Rows[i][3].ToString());
                }
            }

            DataRow dr = ds.Tables[9].NewRow();
            dr[2] = "Total:";
            dr[3] = Total.ToString("F0");
            ds.Tables[9].Rows.Add(dr);
            ds.Tables[9].AcceptChanges();

            dgvSalary.DataSource = ds.Tables[9];
            dgvSalary.DataBind();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string Title = "Salary Registration Report";
            string[] _header = new string[1];
            _header[0] = "Year: " + ddlYear.SelectedItem.Text + "  Month: " + ddlMonth.SelectedItem.Text; 

            string[] _footer = new string[0];

            Print.ReportPrint(Title, _header, dgvSalary, _footer);
            Response.Redirect("~/Accounts/RPTShowGrid.aspx");
            //Page.ClientScript.RegisterStartupScript(GetType(), "javascript", "window.open('../Accounts/RPTShowGrid.aspx')", true);
        }
    }
}
