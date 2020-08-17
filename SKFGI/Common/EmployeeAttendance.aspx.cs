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
using Microsoft.Office.Interop.Excel;

namespace CollegeERP.Common
{
    public partial class EmployeeAttendance : System.Web.UI.Page
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
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.IMPORT_EMPLOYEE_ATTENDANCE))
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
            System.Data.DataTable dt = dv.ToTable();

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

                System.Data.DataTable dt = dv.ToTable();
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

        protected void btnShow_Click(object sender, EventArgs e)
        {
            int Year = int.Parse(ddlYear.SelectedValue.Trim());
            int Month = int.Parse(ddlMonth.SelectedValue.Trim());
            BusinessLayer.Common.EmployeeAttendance Obj = new BusinessLayer.Common.EmployeeAttendance();
            System.Data.DataTable dt = Obj.GetAll(Month, Year);

            if (dt != null)
            {
                dgvAttendance.DataSource = dt;
                dgvAttendance.DataBind();
            }
            Message.Show = false;
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                string ff = "";
                if (uploadExcel.PostedFile.FileName != null && uploadExcel.PostedFile.ContentLength > 0)
                {
                    string fn = uploadExcel.FileName;
                    string fileExt = System.IO.Path.GetExtension(fn);
                    if (fileExt == ".xlsx")
                    {
                        string sm = Server.MapPath("");
                        ff = (sm + "\\Upload\\Attendance" + fileExt);
                        FileInfo file = new FileInfo(ff);
                        if (file.Exists)
                            file.Delete();

                        uploadExcel.PostedFile.SaveAs(ff);
                        Import();
                        Message.IsSuccess = true;
                        Message.Text = "Attendance Imported Successfully";
                        btnShow_Click(sender, e);

                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Please Select .xlsx File";

                    }
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Please Select Excel File To Import";
                }
            }
            catch (Exception ex)
            {
                Message.IsSuccess = false;
                Message.Text = ex.Message;
            }
            Message.Show = true;
        }

        protected void Import()
        {
            System.Data.DataTable DT = new System.Data.DataTable();
            DT.Columns.Add("EmpCode",typeof(string));
            DT.Columns.Add("Present",typeof(decimal));
            DT.Columns.Add("Absent",typeof(decimal));

            DataRow DR;
            string sm = Server.MapPath("");
            string ff = (sm + "\\Upload\\Attendance.xlsx");

            Microsoft.Office.Interop.Excel.ApplicationClass app = new ApplicationClass();
            // Create the workbook object by opening the excel file.
            Microsoft.Office.Interop.Excel.Workbook workBook = app.Workbooks.Open(ff,
                                                         0,
                                                         true,
                                                         5,
                                                         "",
                                                         "",
                                                         true,
                                                         Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                                         "\t",
                                                         false,
                                                         false,
                                                         0,
                                                         true,
                                                         1,
                                                         0);
            // Get the active worksheet using sheet name or active sheet
            Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.ActiveSheet;

            int index = 2;
            object rowIndex = 2;
            while (((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 1]).Value2 != null)
            {
                string EmpCode = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 1]).Value2.ToString();
                string Present = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 2]).Value2.ToString();
                string Absent = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 3]).Value2.ToString();

                DR = DT.NewRow();
                DR["EmpCode"] = EmpCode;
                DR["Present"] = decimal.Parse(Present);
                DR["Absent"] = decimal.Parse(Absent);
                DT.Rows.Add(DR);
                DT.AcceptChanges();

                index += 1;
                rowIndex = index; 
            }

            string AttendanceXML = string.Empty;
            using (DataSet ds = new DataSet())
            {
                ds.Tables.Add(DT);
                AttendanceXML = ds.GetXml();
            }

            int Year = int.Parse(ddlYear.SelectedValue.Trim());
            int Month = int.Parse(ddlMonth.SelectedValue.Trim());
            BusinessLayer.Common.EmployeeAttendance Obj = new BusinessLayer.Common.EmployeeAttendance();
            Obj.BulkSave(Month,Year,AttendanceXML);
        }
    }
}
