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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace CollegeERP.Payroll
{
    public partial class AllEmployeesPaySlip : System.Web.UI.Page
    {
        BusinessLayer.Payroll.MonthYearList MonthYearList;

        //All Public Variables
        public int Month;
        public int Year;
        public int EmployeeId;
        public DateTime DOJ;
        System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem("--Select--", "0");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.ALL_EMPLOYEES_PAYSLIP))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadEmployee();
                Message.Show = false;
            }
        }

        protected void LoadEmployee()
        {
            BusinessLayer.Common.Employee objEmployee = new BusinessLayer.Common.Employee();
            DataView dv = new DataView(objEmployee.GetAll("", ""));
            dv.RowFilter = "CompanyId=" + Session["CompanyId"].ToString();
            dv.Sort = "FullName";

            if (dv != null)
            {
                ddlEmployee.DataSource = dv;
                ddlEmployee.DataBind();
            }
            ddlEmployee.Items.Insert(0, li);
            LoadYear();
        }

        protected void LoadYear()
        {
            MonthYearList = new BusinessLayer.Payroll.MonthYearList();
            EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue.Trim());
            if (EmployeeId > 0)
            {
                BusinessLayer.Common.EmployeeOfficial ObjOfficial = new BusinessLayer.Common.EmployeeOfficial();
                Entity.Common.EmployeeOfficial Official = new Entity.Common.EmployeeOfficial();
                Official = ObjOfficial.GetAllByEmployeeId(EmployeeId);
                DOJ = Official.DOJ;
                ddlYear.DataSource = MonthYearList.GetYearList(DOJ);
                ddlYear.DataBind();
            }
            else
            {
                ddlYear.DataSource = MonthYearList.GetBlankYearList();
                ddlYear.DataBind();
            }
            LoadMonth();
        }

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadYear();
        }

        protected void LoadMonth()
        {
            MonthYearList = new BusinessLayer.Payroll.MonthYearList();
            int Year = int.Parse(ddlYear.SelectedValue.Trim());
            if (Year != 0)
            {
                ddlMonth.DataSource = MonthYearList.GetMonthList(DOJ, Year);
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

        protected void btnShow_Click(object sender, EventArgs e)
        {
            Year = Convert.ToInt32(ddlYear.SelectedValue.Trim());
            Month = Convert.ToInt32(ddlMonth.SelectedValue.Trim());
            EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue.Trim());

            BusinessLayer.Payroll.EmployeeSalaryData ObjSalaryData = new BusinessLayer.Payroll.EmployeeSalaryData();
            DataSet dsPayslip = ObjSalaryData.MonthlyPaySlip(Month, Year, EmployeeId);

            if (dsPayslip.Tables.Count > 0)
            {
                GeneratePDF(dsPayslip);
                Download();
                Message.Show = false;
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Sorry! This Month's Salary is Not Finalized.";
                Message.Show = true;
            }
        }

        protected void GeneratePDF(DataSet ds)
        {
            var doc1 = new Document();
            string path = Server.MapPath(@"../Download/PaySlip.pdf");
            FileInfo fn = new FileInfo(path);
            if (fn.Exists)
            {
                fn.Delete();
            }

            PdfWriter.GetInstance(doc1, new FileStream(path, FileMode.Create));
            doc1.Open();

            Font Bold = FontFactory.GetFont("Book Antiqua", 9, 1);
            Font Normal = FontFactory.GetFont("Book Antiqua", 9, 0);
            PdfPTable table = new PdfPTable(4);
            //actual width of table in points
            table.TotalWidth = 516f;
            //fix the absolute width of the table
            table.LockedWidth = true;

            //relative col widths in proportions - 1/3 and 2/3
            float[] widths = new float[] { 2f, 3f, 2f, 3f };
            table.SetWidths(widths);
            table.HorizontalAlignment = 0;
            //leave a gap before and after the table
            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;

            PdfPCell cell;

            //Heading 
            cell = new PdfPCell(new Phrase("Supreme Knowledge Foundation Group of Institutions (SKFGI)", Bold));
            cell.Colspan = 4;
            cell.HorizontalAlignment = 1;
            cell.Border = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("[Organised by: Supreme Knowledge Foundation (SKF)]", Normal));
            cell.Colspan = 4;
            cell.HorizontalAlignment = 1;
            cell.Border = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Pay Slip", Bold));
            cell.Colspan = 4;
            cell.HorizontalAlignment = 1;
            cell.Border = 0;
            cell.PaddingBottom = 20;
            table.AddCell(cell);


            //Employee Basic Details
            //1st Row
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                cell = new PdfPCell(new Phrase(ds.Tables[0].Rows[i]["COL1"].ToString(), Normal));
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(ds.Tables[0].Rows[i]["COL2"].ToString(), Normal));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(ds.Tables[0].Rows[i]["COL3"].ToString(), Normal));
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(ds.Tables[0].Rows[i]["COL4"].ToString(), Normal));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);
            }

            //Gap
            cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 4;
            cell.Border = 0;
            cell.PaddingBottom = 20;
            table.AddCell(cell);


            //Salary Heading
            cell = new PdfPCell(new Phrase("Details of Payment", Normal));
            cell.Colspan = 2;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Details of Deduction", Normal));
            cell.Colspan = 2;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);


            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                cell = new PdfPCell(new Phrase(ds.Tables[1].Rows[i]["PaymentHead"].ToString(), Normal));
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(ds.Tables[1].Rows[i]["PaymentAmount"].ToString(), Normal));
                cell.HorizontalAlignment = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(ds.Tables[1].Rows[i]["DeductionHead"].ToString(), Normal));
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(ds.Tables[1].Rows[i]["DeductionAmount"].ToString(), Normal));
                cell.HorizontalAlignment = 2;
                table.AddCell(cell);
            }

            //Footer
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(Server.MapPath(@"..\Images\Signature.jpg"));
            jpg.ScaleToFit(100f, 70f);
            cell = new PdfPCell(jpg);
            cell.HorizontalAlignment = 2;
            cell.Colspan = 4;
            cell.Border = 0;
            cell.PaddingTop = 20;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(Convert.ToDateTime(ds.Tables[2].Rows[0]["FinalizeDate"].ToString()).ToString("dd/MM/yyyy"), Normal));
            cell.HorizontalAlignment = 2;
            cell.Colspan = 4;
            cell.Border = 0;
            cell.PaddingTop = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("-----------------------------------------------------------------------", Normal));
            cell.HorizontalAlignment = 2;
            cell.Colspan = 4;
            cell.Border = 0;
            cell.PaddingTop = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Signature of Authorised Signatory with Stamp & Date", Normal));
            cell.HorizontalAlignment = 2;
            cell.Colspan = 4;
            cell.Border = 0;
            cell.PaddingTop = 0;
            table.AddCell(cell);




            doc1.Add(table);
            //iTextSharp.text.html.simpleparser.StyleSheet styles = new iTextSharp.text.html.simpleparser.StyleSheet();
            //iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(doc1);
            //hw.Parse(new StringReader(HTML()));
            doc1.Close();
        }

        protected void Download()
        {
            string path = Server.MapPath(@"../Download/PaySlip.pdf");
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + EmployeeId.ToString() + "_" + Month.ToString() + "_" + Year.ToString() + ".pdf");
            Response.TransmitFile(path);
            Response.End();
        }
    }
}
