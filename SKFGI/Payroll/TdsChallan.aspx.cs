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
using  Microsoft.Office.Interop.Excel;

namespace CollegeERP.Payroll
{
    public partial class TdsChallan : System.Web.UI.Page
    {
        BusinessLayer.Payroll.MonthYearList MonthYearList;       
        //All Public Variables
        public int Month;
        public int Year;
        public int EmployeeId;
        public DateTime DOJ;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.TDS_CHALLAN))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                EmployeeId = int.Parse(HttpContext.Current.User.Identity.Name);
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
        protected Entity.Payroll.TdsChallan getFormFieldValues()
        {

            Entity.Payroll.TdsChallan objTdsCha = new Entity.Payroll.TdsChallan();
            objTdsCha.TMonth = int.Parse(ddlMonth.SelectedValue);
            objTdsCha.TYear = int.Parse(ddlYear.SelectedValue);
             objTdsCha.ChequeNo = txtChequeNo.Text.Trim();
            if (txtChequeDate.Text.Length > 0)
            {
                objTdsCha.ChequeDate = Convert.ToDateTime(txtChequeDate.Text.Trim() + " 00:00:00");
            }
            //else
            //{
            //    objPtxCha.ChequeDate = 
            //}
            if (txtIncomeTax.Text.Length > 0)
            {
                objTdsCha.IncomeTax= Convert.ToDecimal( txtIncomeTax.Text.ToString());
            }
            else
            {
                objTdsCha.IncomeTax = 0;
            }
            if (txtPenalty.Text.Length > 0)
            {
                objTdsCha.Penalty = Convert.ToDecimal(txtPenalty.Text.ToString());
            }
            else
            {
                objTdsCha.Penalty = 0;
            }
            if ( txtSurcharge.Text.Length > 0)
            {
                objTdsCha.Surcharge = Convert.ToDecimal( txtSurcharge.Text.ToString());
            }
            else
            {
                objTdsCha.Surcharge = 0;
            }
            if ( txtEducess.Text.Length > 0)
            {
                objTdsCha.EduCess = Convert.ToDecimal( txtEducess.Text.ToString());
            }
            else
            {
                objTdsCha.EduCess = 0;
            }
            if ( txtInterest.Text.Length > 0)
            {
                objTdsCha.Interest = Convert.ToDecimal(  txtInterest.Text.ToString());
            }
            else
            {
                objTdsCha.Interest = 0;
            }
            objTdsCha.CreatedBy = int.Parse(HttpContext.Current.User.Identity.Name);
            if (chkFinalize.Checked == true)
            { objTdsCha.IsFinalized = true; }
            else
            { objTdsCha.IsFinalized = false; }

            return objTdsCha;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            Entity.Payroll.TdsChallan objTdsChaE = new Entity.Payroll.TdsChallan();
            objTdsChaE = getFormFieldValues();
            BusinessLayer.Payroll.TdsChallan objTdsChaB = new BusinessLayer.Payroll.TdsChallan();
            int rowAffected = objTdsChaB.Save(objTdsChaE);
            if (rowAffected > 0)
            {
                Message.IsSuccess = true;
                Message.Text = "TDS Challan saved Successfully";
                Message.Show = true;
                GenerateChallan();
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Challan Can Not saved";
                Message.Show = true;
            }  
            //GenerateChallan();
        }
        protected void GenerateChallan()
        {
            BusinessLayer.Payroll.TdsChallan tdschallan = new BusinessLayer.Payroll.TdsChallan();
            string sm = Server.MapPath("");
            string path = sm + "\\Upload\\TdsChallan.xlsx";
            Microsoft.Office.Interop.Excel.ApplicationClass app = new ApplicationClass();
            // Create the workbook object by opening the excel file.
            Microsoft.Office.Interop.Excel.Workbook workBook = app.Workbooks.Open(path,
                                                         0,
                                                         false,
                                                         5,
                                                         "",
                                                         "",
                                                         false,
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
            //int index = 2;
            //object rowIndex = 2;
            DataSet ds = tdschallan.GetCompanyDetail(int.Parse(Session["FinYrID"].ToString()), 2);
            System.Data.DataTable dtFinYr = ds.Tables[0];
            //Fin Year
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[3, "V"]).Value2 = dtFinYr.Rows[0]["StartYear"].ToString().Substring(0, 1);
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[3, "W"]).Value2 = dtFinYr.Rows[0]["StartYear"].ToString().Substring(1, 1);
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[3, "X"]).Value2 = dtFinYr.Rows[0]["StartYear"].ToString().Substring(2, 1);
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[3, "Y"]).Value2 = dtFinYr.Rows[0]["StartYear"].ToString().Substring(3, 1);
            //--------------
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[47, "L"]).Value2 = dtFinYr.Rows[0]["StartYear"].ToString().Substring(0, 1);
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[47, "M"]).Value2 = dtFinYr.Rows[0]["StartYear"].ToString().Substring(1, 1);
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[47, "N"]).Value2 = dtFinYr.Rows[0]["StartYear"].ToString().Substring(2, 1);
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[47, "O"]).Value2 = dtFinYr.Rows[0]["StartYear"].ToString().Substring(3, 1);
            
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[3, "AA"]).Value2 = dtFinYr.Rows[0]["EndYear"].ToString().Substring(2, 1);
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[3, "AB"]).Value2 = dtFinYr.Rows[0]["EndYear"].ToString().Substring(3, 1);
            //--------------
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[47, "Q"]).Value2 = dtFinYr.Rows[0]["EndYear"].ToString().Substring(2, 1);
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[47, "R"]).Value2 = dtFinYr.Rows[0]["EndYear"].ToString().Substring(3, 1);
            System.Data.DataTable dtComp = ds.Tables[1];
            //Tan No
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[8, "A"]).Value2 = dtComp.Rows[0]["CompanyTanNo"].ToString().Substring(0, 1);
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[8, "B"]).Value2 = dtComp.Rows[0]["CompanyTanNo"].ToString().Substring(1, 1);
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[8, "C"]).Value2 = dtComp.Rows[0]["CompanyTanNo"].ToString().Substring(2, 1);
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[8, "D"]).Value2 = dtComp.Rows[0]["CompanyTanNo"].ToString().Substring(3, 1);
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[8, "E"]).Value2 = dtComp.Rows[0]["CompanyTanNo"].ToString().Substring(4, 1);
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[8, "F"]).Value2 = dtComp.Rows[0]["CompanyTanNo"].ToString().Substring(5, 1);
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[8, "G"]).Value2 = dtComp.Rows[0]["CompanyTanNo"].ToString().Substring(6, 1);
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[8, "H"]).Value2 = dtComp.Rows[0]["CompanyTanNo"].ToString().Substring(7, 1);
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[8, "I"]).Value2 = dtComp.Rows[0]["CompanyTanNo"].ToString().Substring(8, 1);
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[8, "J"]).Value2 = dtComp.Rows[0]["CompanyTanNo"].ToString().Substring(9, 1);
            //Company Name
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[10, "A"]).Value2 = dtComp.Rows[0]["CompanyName"].ToString();
            //Company Address
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[12, "A"]).Value2 = dtComp.Rows[0]["CompanyAddress"].ToString();
            //Tel No
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[14, "D"]).Value2 = dtComp.Rows[0]["CompanyPhoneNo"].ToString();
            //Income tax
            decimal IncomeTax = 0;
            decimal Surcharge = 0;
            decimal Educess = 0;
            decimal Interest=0;
            decimal penulty = 0;
            if (txtIncomeTax.Text.Length > 0)
            {
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[20, "H"]).Value2 = Convert.ToDecimal(txtIncomeTax.Text.ToString());
                IncomeTax = Convert.ToDecimal(txtIncomeTax.Text.ToString());
            }
            else
            {
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[20, "H"]).Value2 = "0.00";
            }
            //Sur Charge
            if (txtSurcharge.Text.Length > 0)
            {
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[21, "H"]).Value2 = Convert.ToDecimal(txtSurcharge.Text.ToString());
                Surcharge = Convert.ToDecimal(txtSurcharge.Text.ToString());
            }
            else
            {
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[21, "H"]).Value2 = "0.00";
            }
            //Education Cess
            if (txtEducess.Text.Length > 0)
            {
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[22, "H"]).Value2 = Convert.ToDecimal(txtEducess.Text.ToString());
                Educess = Convert.ToDecimal(txtEducess.Text.ToString());
            }
            else
            {
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[22, "H"]).Value2 = "0.00";
            }
            //Interest
            if (txtInterest.Text.Length > 0)
            {
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[23, "H"]).Value2 = Convert.ToDecimal(txtInterest.Text.ToString());
                Interest = Convert.ToDecimal(txtInterest.Text.ToString());
            }
            else
            {
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[23, "H"]).Value2 = "0.00";

            }
            //Penalty
            if (txtPenalty.Text.Length > 0)
            {
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[24, "H"]).Value2 = Convert.ToDecimal(txtPenalty.Text.ToString());
                penulty = Convert.ToDecimal(txtPenalty.Text.ToString());
            }
            else
            {
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[24, "H"]).Value2 ="0.00";
            }
            //Total
            decimal Total = 0;
            Total = IncomeTax +  Surcharge + Educess + Interest + penulty ;
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[25, "H"]).Value2 = Convert.ToDecimal( Total.ToString());
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[38, "O"]).Value2 = Convert.ToDecimal(Total.ToString());
            //Amount In Words
            BusinessLayer.Payroll.PTaxChallan objPtaxChalan = new BusinessLayer.Payroll.PTaxChallan();
            //Total = 2255325;
            
            string AmtWords = objPtaxChalan.GetAmtInWord(Total);
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[26, "H"]).Value2 = AmtWords;
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[39, "E"]).Value2 = AmtWords;
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[28, "J"]).Value2 = txtChequeNo.Text.Trim();
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[38, "H"]).Value2 = txtChequeNo.Text.Trim();
            if (txtChequeDate.Text.Length > 0)
            {
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[28, "P"]).Value2 =Convert.ToDateTime( txtChequeDate.Text.ToString()).ToString("dd/MM/yyyy");
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[31, "C"]).Value2 = Convert.ToDateTime(txtChequeDate.Text.ToString()).ToString("dd/MM/yyyy");

            }
            //int posHundred = 0;
            //int posLakh = 0;
            //string strThousand = string.Empty;
            //string[] lkh = AmtWords.Split(' ');
            //int i = 0;
            //for (i=lkh.Length-1; i > 0; i--)
            //{
            //    if (lkh[i].ToString() == "hundred" )
            //    {
            //        if (lkh[i + 2].ToString() == "Only.")
            //        {
            //            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[28, "O"]).Value2 = lkh[i + 1].ToString();
            //        }
            //    }
            //    if (lkh[i].ToString() == "thousand")
            //    {
            //        posHundred = i;
            //        if (lkh[i+2].ToString() == "hundred")
            //        {
            //            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[28, "L"]).Value2 = lkh[i + 1].ToString();
            //        }
            //    }
            //    if (lkh[i].ToString() == "Lakhs" && lkh[i+1].ToString() == "")
            //    {
            //        posLakh = i+1;

            //        while (posHundred > posLakh)
            //        {
            //            strThousand = strThousand + " " + lkh[posLakh].ToString();
            //            posLakh=posLakh + 1;
            //        }
            //        ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[28, "H"]).Value2 = strThousand;
            //        ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[28, "E"]).Value2 = lkh[0].ToString();
            //    }
            //}
            //Twenty Lakhs  two hundred fifty-five thousand three hundred twenty-five Only.
            //if (AmtWords.Contains("Lakhs"))
            //{
                //int positionOflakh = AmtWords.IndexOf("Lakhs");
                //((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[28, "E"]).Value2 =AmtWords.ToString().Substring(0,positionOflakh-1);
                //string AmtThnd = AmtWords.ToString().Substring( 13, AmtWords.Length);
                //int positionThousand = AmtThnd.IndexOf("thousand");
                //((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[28, "H"]).Value2 = AmtThnd.ToString().Substring(0, positionThousand - 1);
            //}
            //((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[24, "H"]).Value2 = Convert.ToDecimal(txtPenalty.Text.ToString());

           
           
             workBook.Save();
             workBook.Close(false, Type.Missing, Type.Missing);
             Download();
        }

     

        protected void Download()
        {
            
            string sm = Server.MapPath("");
            string path = sm + "\\Upload\\TdsChallan.xlsx";        
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=Tds-Challan.xslx");
            Response.WriteFile(path);
            Response.End();
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {            
            BusinessLayer.Payroll.TdsChallan objTdsCha= new BusinessLayer.Payroll.TdsChallan();
            int Intmode = 1;
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = objTdsCha.GetIncomeTax(Intmode, int.Parse(ddlMonth.SelectedValue), int.Parse(ddlYear.SelectedValue));
            txtIncomeTax.Text = dt.Rows[0]["ITax"].ToString();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
            Message.Show = false;
        }
        protected void ClearControl()
        {
            LoadMonth();
            LoadYear();            
            txtChequeNo.Text=string.Empty;
            txtChequeDate.Text = string.Empty;
            txtIncomeTax.Text = string.Empty;
            txtPenalty.Text = string.Empty;
            txtInterest.Text = string.Empty;
            txtSurcharge.Text = string.Empty;
            txtEducess.Text = string.Empty;
            chkFinalize.Checked = false;
        }

       
    }
}
