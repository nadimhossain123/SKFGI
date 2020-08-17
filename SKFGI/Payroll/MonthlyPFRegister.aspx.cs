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
    public partial class MonthlyPFRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.PF_REGISTER))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadYear();
             }
           
        }

        protected void LoadYear()
        {
            ListItem yyyy;
            for (int year = DateTime.Now.Year; year >= 2011; year--)
            {
                yyyy = new ListItem(year.ToString(), year.ToString());
                ddlYear.Items.Add(yyyy);
            }

            yyyy = new ListItem("--Select Year--", "0");
            ddlYear.Items.Insert(0, yyyy);
            LoadMonth();
        }

        protected void LoadMonth()
        {
            BusinessLayer.Payroll.MonthYearList MonthYearList = new BusinessLayer.Payroll.MonthYearList();
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
              
        protected void btnShow_Click(object sender, EventArgs e)
        {
            int Year = Convert.ToInt32(ddlYear.SelectedValue.Trim());
            int Month = Convert.ToInt32(ddlMonth.SelectedValue.Trim());
            int CompanyId = Convert.ToInt32(Session["CompanyId"].ToString());

            BusinessLayer.Common.Report ReportLogic = new BusinessLayer.Common.Report();
            DataTable DT = ReportLogic.GetMonthlyPFRegister(Month, Year, CompanyId);

            decimal TotalBasic = 0;
            decimal TotalEmployeePF = 0;
            decimal TotalEmployerPF = 0;
            decimal TotalEDLIS = 0;
            decimal TotalPFAdminCharges = 0;
            decimal TotalEDLIAdminCharges = 0;
            decimal TotalSubTotalEmployerContribution = 0;

            if (DT != null)
            {
                if (DT.Rows.Count > 0)
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        TotalBasic += Convert.ToDecimal(DT.Rows[i]["EmployeeSalaryDataBasicAmount"].ToString());
                        TotalEmployeePF += Convert.ToDecimal(DT.Rows[i]["EmployeeSalaryDataPFAmount"].ToString());
                        TotalEmployerPF += Convert.ToDecimal(DT.Rows[i]["EmployeeSalaryDataEmployerPF"].ToString());
                        TotalEDLIS += Convert.ToDecimal(DT.Rows[i]["EmployeeSalaryDataEDLICharges"].ToString());
                        TotalPFAdminCharges += Convert.ToDecimal(DT.Rows[i]["EmployeeSalaryDataPFAdminCharges"].ToString());
                        TotalEDLIAdminCharges += Convert.ToDecimal(DT.Rows[i]["EmployeeSalaryDataEDLIAdminCharges"].ToString());
                        TotalSubTotalEmployerContribution += Convert.ToDecimal(DT.Rows[i]["SubTotalEmployerContribution"].ToString());
                    }
                }
                DataRow DR;
                DR = DT.NewRow();
                DR["PFNo"] = "Total";
                DR["EmployeeSalaryDataBasicAmount"] = TotalBasic.ToString("F0");
                DR["EmployeeSalaryDataPFAmount"] = TotalEmployeePF.ToString("F0");
                DR["EmployeeSalaryDataEmployerPF"] = TotalEmployerPF.ToString("F0");
                DR["EmployeeSalaryDataEDLICharges"] = TotalEDLIS.ToString("F0");
                DR["EmployeeSalaryDataPFAdminCharges"] = TotalPFAdminCharges.ToString("F0");
                DR["EmployeeSalaryDataEDLIAdminCharges"] = TotalEDLIAdminCharges.ToString("F0");
                DR["SubTotalEmployerContribution"] = TotalSubTotalEmployerContribution.ToString("F0");
                DT.Rows.Add(DR);
                DT.AcceptChanges();

                dgvReport.DataSource = DT;
                dgvReport.DataBind();

                trButton.Visible = true;
            }
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMonth();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string Title = "PF Contribution Report";
            string[] _header = new string[1];
            _header[0] = "Year: " + ddlYear.SelectedItem.Text + "  Month: " + ddlMonth.SelectedItem.Text;

            string[] _footer = new string[0];

            Print.ReportPrint(Title, _header, dgvReport, _footer);
            Page.ClientScript.RegisterStartupScript(GetType(), "javascript", "window.open('../Accounts/RPTShowGrid.aspx')", true);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string[] _header = new string[2];
            _header[0] = "Year: " + ddlYear.SelectedItem.Text;
            _header[1] = "Month: " + ddlMonth.SelectedItem.Text;

            string[] _footer = new string[0];
            string file = "PF_CONTRIBUTION_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, dgvReport, _footer, file);
        }

    }
}
