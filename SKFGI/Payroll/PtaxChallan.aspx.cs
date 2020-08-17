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
    public partial class PtaxChallan : System.Web.UI.Page
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
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.PTAX_CHALLAN))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                EmployeeId = int.Parse(HttpContext.Current.User.Identity.Name);
                LoadYear();
                Message.Show = false;
                GetAllPTax();
            }
        }

        protected void dgvPO_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int Id = int.Parse(dgvPO.DataKeys[e.NewEditIndex].Value.ToString());
            GetPTaxById(Id);

        }
        protected void GetPTaxById(int Id)
        {
            BusinessLayer.Payroll.PTaxChallan objPTaxCha = new BusinessLayer.Payroll.PTaxChallan();
            int CMonth = int.Parse(ddlMonth.SelectedValue); 
            int CYear = int.Parse(ddlYear.SelectedValue);
            
            System.Data.DataTable dt;
            dt = objPTaxCha.GetPTaxById(2, CMonth, CYear, Id);
            if (dt != null)
            {
                ddlYear.SelectedValue = dt.Rows[0]["CYear"].ToString();
                LoadMonth();
                ddlMonth.SelectedValue =  dt.Rows[0]["CMonth"].ToString();                
                txtChequeNo.Text = dt.Rows[0]["ChequeNo"].ToString();
                txtChequeDate.Text = dt.Rows[0]["ChequeDate"].ToString();
                txtPtax.Text = dt.Rows[0]["Tax"].ToString();
                txtPenalty.Text=dt.Rows[0]["Penalty"].ToString();
                txtCompMoney.Text=dt.Rows[0]["CompMoney"].ToString();
                txtLateFees.Text=dt.Rows[0]["LateFees"].ToString();
                if (dt.Rows[0]["IsFinalized"].ToString() == "True")
                {
                    chkFinalize.Checked = true;
                }
                else
                {
                    chkFinalize.Checked = false;
                }
                btnSave.Text = "Update";
                hdnPTaxId.Value = dt.Rows[0]["Id"].ToString();
            }


        }
        protected void GetAllPTax()
        {
            BusinessLayer.Payroll.PTaxChallan objPTaxCha = new BusinessLayer.Payroll.PTaxChallan();
            int CMonth = int.Parse(ddlMonth.SelectedValue);
            int CYear = int.Parse(ddlYear.SelectedValue);
            System.Data.DataTable dt;
            dt = objPTaxCha.GetPTax(3, CMonth, CYear);
            if (dt != null)
            {
                dgvPO.DataSource = dt;
                dgvPO.DataBind();
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
        protected Entity.Payroll.PTaxChallan getFormFieldValues()
        {
            
            Entity.Payroll.PTaxChallan objPtxCha = new Entity.Payroll.PTaxChallan();
            objPtxCha.CMonth = int.Parse(ddlMonth.SelectedValue);
            objPtxCha.CYear = int.Parse(ddlYear.SelectedValue);
            objPtxCha.ChequeNo = txtChequeNo.Text.Trim();
            if (hdnPTaxId.Value != "0")
            {
                objPtxCha.Id = Convert.ToInt16(hdnPTaxId.Value);
            }
            if (txtChequeDate.Text.Length > 0)
            {
                objPtxCha.ChequeDate = Convert.ToDateTime(txtChequeDate.Text.Trim() + " 00:00:00");
            }
            //else
            //{
            //    objPtxCha.ChequeDate = 
            //}
            if (txtPtax.Text.Length > 0)
            {
                objPtxCha.Tax = Convert.ToDecimal(txtPtax.Text.ToString());
            }
            else
            {
                objPtxCha.Tax = 0;
            }
            if (txtPenalty.Text.Length > 0)
            {
                objPtxCha.Penalty = Convert.ToDecimal(txtPenalty.Text.ToString());
            }
            else
            {
                objPtxCha.Penalty = 0;
            }
            if (txtCompMoney.Text.Length > 0)
            {
                objPtxCha.CompMoney = Convert.ToDecimal(txtCompMoney.Text.ToString());
            }
            else
            {
                objPtxCha.CompMoney = 0;
            }
            if (txtLateFees.Text.Length > 0)
            {
                objPtxCha.LateFees = Convert.ToDecimal(txtLateFees.Text.ToString());
            }
            else
            {
                objPtxCha.LateFees = 0;
            }
            objPtxCha.CreatedBy =int.Parse( HttpContext.Current.User.Identity.Name);
            if (chkFinalize.Checked == true)
            { objPtxCha.IsFinalized = true; }
            else
            { objPtxCha.IsFinalized = false; }

            return objPtxCha;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            Entity.Payroll.PTaxChallan objPtxCha = new Entity.Payroll.PTaxChallan();
            objPtxCha = getFormFieldValues();
            BusinessLayer.Payroll.PTaxChallan objPtxChaB = new BusinessLayer.Payroll.PTaxChallan();
            if (Convert.ToInt16(hdnPTaxId.Value) == 0)
            {
                int rowAffected = objPtxChaB.Save(objPtxCha);
                if (rowAffected > 0)
                {
                    Message.IsSuccess = true;
                    Message.Text = "Challan saved Successfully";
                    Message.Show = true;
                    GenerateChallan();
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Challan Not saved";
                    Message.Show = true;
                }
            }
            else
            {
                int rowAffected = objPtxChaB.Update(objPtxCha);
                if (rowAffected > 0)
                {
                    Message.IsSuccess = true;
                    Message.Text = "Challan Updated Successfully";
                    Message.Show = true;
                    GenerateChallan();
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Challan Not Updated";
                    Message.Show = true;
                }
            }
            Message.Show = true;
        }
        protected void GenerateChallan()
        {
            BusinessLayer.Payroll.PTaxChallan objPTaxChlnB = new BusinessLayer.Payroll.PTaxChallan();
            string sm = Server.MapPath("");
            string path = sm + "\\Upload\\PTax.xlsx";
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
            object rowIndex = 2;
            if (txtPtax.Text.Length > 0)
            {
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[18, "M"]).Value2 = txtPtax.Text.Trim();
            }
            else
            {
                txtPtax.Text = "0.00";
            }
            if (txtLateFees.Text.Length > 0)
            {
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[19, "M"]).Value2 = txtLateFees.Text.Trim();
            }
            else
            {
                txtLateFees.Text = "0.00";
            }
            if (txtPenalty.Text.Length > 0)
            {
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[20, "M"]).Value2 = txtPenalty.Text.Trim();
            }
            else
            {
                txtPenalty.Text = "0.00";
            }
            if (txtCompMoney.Text.Length > 0)
            {
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[21, "M"]).Value2 = txtCompMoney.Text.Trim();
            }
            else
            {
                txtCompMoney.Text = "0.00";
            }
            decimal TotalAmt = Convert.ToDecimal(txtPtax.Text.ToString()) + Convert.ToDecimal(txtLateFees.Text.ToString()) + Convert.ToDecimal(txtPenalty.Text.ToString()) + Convert.ToDecimal(txtCompMoney.Text.ToString());
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[22, "M"]).Value2 = TotalAmt.ToString();//Total
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[35, "D"]).Value2 = TotalAmt.ToString();//Total
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[18, "D"]).Value2 = txtChequeNo.Text.Trim();
            if (txtChequeDate.Text.Length > 0)
            {
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[19, "D"]).Value2 = Convert.ToDateTime(txtChequeDate.Text.ToString()).ToString("dd/MM/yyyy");
            }
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[24, "I"]).Value2 = objPTaxChlnB.GetAmtInWord(TotalAmt);
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[35, "I"]).Value2 = objPTaxChlnB.GetAmtInWord(TotalAmt);
            if (ddlMonth.SelectedValue.Length == 1)
            {
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[16, "M"]).Value2 = 0;
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[16, "N"]).Value2 = ddlMonth.SelectedValue.ToString();
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[16, "Q"]).Value2 = 0;
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[16, "R"]).Value2 = ddlMonth.SelectedValue.ToString();
            }
            else
            {
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[16, "M"]).Value2 = 1;
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[16, "N"]).Value2 = ddlMonth.SelectedValue.ToString().Substring(1, 1);
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[16, "Q"]).Value2 = 1;
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[16, "R"]).Value2 = ddlMonth.SelectedValue.ToString().Substring(1,1);
            }
            //Year--------
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[16, "O"]).Value2 = ddlYear.SelectedValue.ToString().Substring(2, 1);
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[16, "P"]).Value2 = ddlYear.SelectedValue.ToString().Substring(3, 1);
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[16, "S"]).Value2 = ddlYear.SelectedValue.ToString().Substring(2, 1);
            ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[16, "T"]).Value2 = ddlYear.SelectedValue.ToString().Substring(3, 1);
            
             workBook.Save();
             workBook.Close(false, Type.Missing, Type.Missing);
             Download();
        }     

        protected void Download()
        {
            
            string sm = Server.MapPath("");
            string path = sm + "\\Upload\\PTax.xlsx";        
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=P-Tax.xslx");
            Response.WriteFile(path);
            Response.End();
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            BusinessLayer.Payroll.PTaxChallan objPtxCha = new BusinessLayer.Payroll.PTaxChallan();
            int Intmode = 1;
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = objPtxCha.GetPTax(Intmode, int.Parse(ddlMonth.SelectedValue), int.Parse(ddlYear.SelectedValue));
            txtPtax.Text = dt.Rows[0]["PtaxAmount"].ToString();


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
        }
        protected void ClearControl()
        {
            LoadMonth();
            LoadYear();            
           txtChequeNo.Text=string.Empty;
           txtChequeDate.Text = string.Empty;
           txtPtax.Text = string.Empty;
           txtPenalty.Text = string.Empty;
           txtCompMoney.Text = string.Empty;
           txtLateFees.Text = string.Empty;
           btnSave.Text = "Save";
           chkFinalize.Checked = false;
           hdnPTaxId.Value = "0";
        }

       
    }
}
