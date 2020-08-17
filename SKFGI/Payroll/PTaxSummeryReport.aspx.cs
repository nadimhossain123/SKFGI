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
    public partial class PTaxSummeryReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected StringBuilder PreparePTaxReport()
        {
            BusinessLayer.Common.PTax objPTax = new BusinessLayer.Common.PTax();
            DataSet ds = new DataSet();
            ds = objPTax.PTax_SummeryReport(ddlFromToYear.SelectedValue);

            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='PTaxTable' width='100%' border='1' style='font-family:Arial;font-size:9pt;background:#ffffff'>");
            sb.Append("<tr style='font-weight:bold;font-size:11pt;font-family:Arial; text-align:center; white-space:nowrap; background:#084200; color:#ffffff'>");
            sb.Append("<td colspan='6'>");
            sb.Append("STATEMENT OF PROFESSION TAX PAYABLE AND TAX PAID (To be produced at the time of assessment)");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr style='font-weight:bold;font-size:11pt;font-family:Arial; text-align:center; white-space:nowrap; background:#084200; color:#ffffff'>");
            sb.Append("<td colspan='6'>");
            sb.Append("(It must be properly filled up and signed by the assessee)");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr style='font-weight:bold;font-size:11pt;font-family:Arial; text-align:center; white-space:nowrap; background:#084200; color:#ffffff'>");
            sb.Append("<td colspan='6'>");
            sb.Append("Registered Employer :- Supreme Knowledge Foundation Group of Institutions");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr style='font-weight:bold;font-size:11pt;font-family:Arial; text-align:center; white-space:nowrap; background:#084200; color:#ffffff'>");
            sb.Append("<td colspan='6'>");
            sb.Append("Registration No :- RWS - 2231085");
            sb.Append("</td>");
            sb.Append("</tr>");

            sb.Append("<tr style='font-weight:bold;font-size:9pt;font-family:Arial; text-align:center; white-space:nowrap; background:#084200; color:#ffffff'>");
            sb.Append("<td rowspan='2'>Month</td>");
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                sb.Append("<td colspan='2'>Employees drawing salary upto Rs. /-</td>");
            }
            sb.Append("<td colspan='2'>Total</td>");
            sb.Append("</tr>");

            sb.Append("<tr style='font-weight:bold;font-size:9pt;font-family:Arial; text-align:center; white-space:nowrap; background:#084200; color:#ffffff'>");
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                sb.Append("<td>No.</td>");
                sb.Append("<td>P.T. Payable @ " + dr["PTaxDetailsAmount"] + "</td>");
            }
            sb.Append("<td>Total Emp.</td>");
            sb.Append("<td>Total P.T.</td>");
            sb.Append("</tr>");

            decimal[] total = new decimal[ds.Tables[1].Rows.Count * 2 + 2];

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int totalNoOfEmp = 0;
                decimal totalEmployeeSalaryDataPTaxAmount = 0;
                int rowpos = 0;

                sb.Append("<tr>");
                sb.Append("<td style='background:#ffffad'>" + dr["MonthName"] + "-" + dr["Year"] + "</td>");

                foreach (DataRow drEmp in ds.Tables[2].Rows)
                {
                    if (dr["TEMPID"].ToString() == drEmp["EmployeeSalaryDataMonth"].ToString())
                    {
                        sb.Append("<td style='background:#c5f9a7'>" + drEmp["NoOfEmp"] + "</td>");
                        sb.Append("<td style='background:#c5f9a7'>" + Convert.ToString(int.Parse(drEmp["NoOfEmp"].ToString()) * decimal.Parse(drEmp["EmployeeSalaryDataPTaxAmount"].ToString())) + "</td>");

                        totalNoOfEmp += int.Parse(drEmp["NoOfEmp"].ToString());
                        totalEmployeeSalaryDataPTaxAmount += int.Parse(drEmp["NoOfEmp"].ToString()) * decimal.Parse(drEmp["EmployeeSalaryDataPTaxAmount"].ToString());


                        total[rowpos] = int.Parse(total[rowpos].ToString()) + int.Parse(drEmp["NoOfEmp"].ToString());
                        total[rowpos + 1] = decimal.Parse(total[rowpos + 1].ToString()) + int.Parse(drEmp["NoOfEmp"].ToString()) * decimal.Parse(drEmp["EmployeeSalaryDataPTaxAmount"].ToString());
                        rowpos += 2;
                    }
                }
                total[total.Length - 2] = int.Parse(total[total.Length - 2].ToString()) + totalNoOfEmp;
                total[total.Length - 1] = decimal.Parse(total[total.Length - 1].ToString()) + totalEmployeeSalaryDataPTaxAmount;
                sb.Append("<td style='background:#ffffad'>" + totalNoOfEmp + "</td>");
                sb.Append("<td style='background:#ffffad'>" + totalEmployeeSalaryDataPTaxAmount + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("<tr>");
            sb.Append("<td style='background:#ffffad;font-weight:bold'>TOTAL</td>");
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
            HttpContext.Current.Response.AddHeader("Content-Disposition", "filename=PTaxReport" + DateTime.Now.ToString("dd-MM-yyyy") + ".xls");
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