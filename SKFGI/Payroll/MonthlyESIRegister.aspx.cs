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
    public partial class MonthlyESIRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }

            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.ESI_REGISTER))
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
            DataTable DT = ReportLogic.GetMonthlyESIRegister(Month, Year, CompanyId);

            decimal TotalEmployeeESI = 0;
            decimal TotalEmployerESI = 0;

            if (DT != null)
            {
                if (DT.Rows.Count > 0)
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        TotalEmployeeESI += Convert.ToDecimal(DT.Rows[i]["EmployeeESI"].ToString());
                        TotalEmployerESI += Convert.ToDecimal(DT.Rows[i]["EmployerESI"].ToString());
                    }
                }
                DataRow DR;
                DR = DT.NewRow();
                DR["ESINo"] = "Total";
                DR["EmployeeESI"] = TotalEmployeeESI.ToString("F0");
                DR["EmployerESI"] = TotalEmployerESI.ToString("F0");
                DT.Rows.Add(DR);
                DT.AcceptChanges();

                DR = DT.NewRow();
                DR["ESINo"] = "Grand Total";
                DR["EmployerESI"] =(TotalEmployeeESI + TotalEmployerESI).ToString("F0");
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
            string Title = "ESIC Contribution Report";
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
            string file = "ESIC_CONTRIBUTION_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, dgvReport, _footer, file);
        }
    }
}
