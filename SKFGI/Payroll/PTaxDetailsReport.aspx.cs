using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

namespace CollegeERP.Payroll
{
    public partial class PTaxDetailsReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected StringBuilder PreparePTaxReport()
        {
            BusinessLayer.Common.PTax objPTax = new BusinessLayer.Common.PTax();
            DataSet ds = new DataSet();
            ds = objPTax.PTax_DetailsReport(ddlFromToYear.SelectedValue);

            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='PTaxTable' width='100%' border='1' style='font-family:Arial;font-size:9pt;background:#ffffff'>");
            sb.Append("<tr style='font-weight:bold;font-size:11pt;font-family:Arial; text-align:center; white-space:nowrap; background:#084200; color:#ffffff'>");
                sb.Append("<td colspan='2'>");
                    sb.Append("P. Tax Report "+ ddlFromToYear.SelectedItem.Text +"");
                sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr style='font-weight:bold;font-size:9pt;font-family:Arial; text-align:center; white-space:nowrap; background:#084200; color:#ffffff'><td rowspan='2'>Emp Code</td><td rowspan='2'>Name of Employees</td>");
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                sb.Append("<td colspan='2'>");
                sb.Append(dr["MonthName"].ToString());
                sb.Append("-");
                sb.Append(dr["Year"].ToString());
                sb.Append("</td>");
            }
            sb.Append("</tr>");
            sb.Append("<tr style='font-weight:bold;font-size:9pt;font-family:Arial; text-align:center; white-space:nowrap; background:#ff0000; color:#ffffff'>");
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                sb.Append("<td>Gr.Salary</td><td>P.Tax</td>");
            }
            sb.Append("</tr>");

            decimal[] total=new decimal[24];

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                sb.Append("<tr>");
                sb.Append("<td style='background:#ffffad'>"); sb.Append(dr["EmpCode"]); sb.Append("</td>");
                sb.Append("<td style='background:#ffffad'>"); sb.Append(dr["EmployeeName"]); sb.Append("</td>");

                //DataView dv = new DataView(ds.Tables[2]);
                //dv.RowFilter = "EmployeeSalaryData_EmpId='" + dr["EmployeeId"] + "'";

                int rowpos = 0;

                foreach (DataRow drm in ds.Tables[1].Rows)
                {
                    DataView dvs = new DataView(ds.Tables[2]);
                    dvs.RowFilter = "EmployeeSalaryDataMonth='" + drm["TEMPID"] + "' AND EmployeeSalaryDataYear='" + drm["Year"] + "' AND EmployeeSalaryData_EmpId='" + dr["EmployeeId"] + "'";

                    if (dvs.ToTable().Rows.Count > 0)
                    {
                        sb.Append("<td style='background:#c5f9a7'>"); sb.Append(dvs.ToTable().Rows[0]["EmployeeSalaryDataGrossSalary"]); sb.Append("</td>");
                        sb.Append("<td style='background:#c5f9a7'>"); sb.Append(dvs.ToTable().Rows[0]["EmployeeSalaryDataPTaxAmount"]); sb.Append("</td>");

                        total[rowpos] = decimal.Parse(total[rowpos].ToString()) + decimal.Parse(dvs.ToTable().Rows[0]["EmployeeSalaryDataGrossSalary"].ToString());
                        total[rowpos + 1] = decimal.Parse(total[rowpos + 1].ToString()) + decimal.Parse(dvs.ToTable().Rows[0]["EmployeeSalaryDataPTaxAmount"].ToString());
                        rowpos += 2;
                    }
                    else
                    {
                        sb.Append("<td>"); sb.Append("</td>");
                        sb.Append("<td>"); sb.Append("</td>");
                        total[rowpos] = decimal.Parse(total[rowpos].ToString()) + 0;
                        total[rowpos + 1] = decimal.Parse(total[rowpos + 1].ToString()) + 0;
                        rowpos += 2;
                    }
                }
                sb.Append("</tr>");
            }
            sb.Append("<tr>");
            sb.Append("<td colspan='2' style='background:#ffffad;font-weight:bold;text-align:center;'>TOTAL</td>");
            for (int i = 0; i < total.Length; i++)
            {
                sb.Append("<td style='background:#ffffad;font-weight:bold'>" + total[i].ToString() + "</td>");
                sb.Append("<td style='background:#ffffad;font-weight:bold'>" + total[++i].ToString() + "</td>");
            }
            sb.Append("</tr>");

            sb.Append("</table>");

            return sb;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            divPTaxReport.InnerHtml = PreparePTaxReport().ToString();
        }

        protected void ExportToExcel()
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/msexcel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "filename=PTaxReport"+DateTime.Now.ToString("dd-MM-yyyy")+".xls");
            HttpContext.Current.Response.Write(PreparePTaxReport());
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Flush();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
    }
}