using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.Accounts;
using System.Data;

namespace CollegeERP.Accounts
{
   




    public partial class CashBook : System.Web.UI.Page
    {






        ListItem li = new ListItem("Select", "0");
        public string sortOrder
        {
            get
            {
                if (ViewState["sortOrder"].ToString() == "desc")
                {
                    ViewState["sortOrder"] = "asc";
                }
                else
                {
                    ViewState["sortOrder"] = "desc";
                }
                return ViewState["sortOrder"].ToString();
            }
            set
            {
                ViewState["sortOrder"] = value;
            }
        }

        clsGeneralFunctions gf = new clsGeneralFunctions();
        string strValues = "";
        char chr = Convert.ToChar(130);

        decimal TotalDrAmountBank = 0;
        decimal TotalDrAmountCash = 0;
        decimal TotalCrAmountBank = 0;
        decimal TotalCrAmountCash = 0;

        

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["SuperAdmin"] != null)
            {
                this.MasterPageFile = "../SuperAdmin.Master";
            }
            else
            {
                this.MasterPageFile = "../MasterAdmin.Master";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if ((Session["UserId"] == null) && (Session["SuperAdmin"] == null))
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if ((!HttpContext.Current.User.IsInRole(Entity.Common.Utility.REPORT_LEDGER)) && (Session["SuperAdmin"] == null))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                populateCombo();
                ResetControls();
            }
        }
        protected void ResetControls()
        {
            Message.Show = false;
            btnExportExcel.Visible = false;
            btnPrint.Visible = false;
            txtFromDate.Text = Session["SesFromDate"].ToString();
            txtToDate.Text = Session["SesToDate"].ToString();
            
        }

        private void populateCombo()
        {
            strValues = Session["CompanyID"].ToString() + chr.ToString();
            strValues += Session["FinYrID"].ToString() + chr.ToString();
            strValues += Session["BranchID"].ToString() + chr.ToString();
            strValues += Session["DataFlow"].ToString();
            //gf.BindAjaxDropDownColumnsBySP(ddlLedger, "spSelect_MstGeneralLedgerFull", strValues);
            //ddlLedger.Items.Insert(0, li);
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //if (ddlLedger.SelectedValue == "0" || ddlLedger.Text == string.Empty)
            //{
            //    Message.IsSuccess = false;
            //    Message.Text = "Please Select Ledger";
            //    Message.Show = true;
            //}
            if (!DateRangeValidation())
            {
                Message.Show = true;
            }
            else
            {
                PopulateGrid();
                if (gvGeneralLedger.Rows.Count > 0)
                {
                    btnExportExcel.Visible = true;
                    btnPrint.Visible = true;
                }
                Message.Show = false;
            }
        }

        protected bool DateRangeValidation()
        {
            bool result = true;
            DateTime minValue = Convert.ToDateTime(Session["SesFromDate"].ToString());
            DateTime maxValue = Convert.ToDateTime(Session["SesToDate"].ToString());

            if (Convert.ToDateTime(txtFromDate.Text) < minValue || Convert.ToDateTime(txtFromDate.Text) > maxValue)
            {
                result = false;
                Message.IsSuccess = false;
                Message.Text = "Please Select From Date Within " + Session["SesFromDate"].ToString() + " to " + Session["SesToDate"].ToString();
            }
            else if (Convert.ToDateTime(txtToDate.Text) < minValue || Convert.ToDateTime(txtToDate.Text) > maxValue)
            {
                result = false;
                Message.IsSuccess = false;
                Message.Text = "Please Select To Date Within " + Session["SesFromDate"].ToString() + " to " + Session["SesToDate"].ToString();
            }
            return result;
        }

        private void PopulateGrid()
        {

            strValues = Session["CompanyID"].ToString() + chr.ToString();
            strValues += Session["BranchID"].ToString() + chr.ToString();
            //strValues += ddlLedger.SelectedValue.Trim() + chr.ToString();
            strValues += txtFromDate.Text.Trim() + chr.ToString();
            strValues += txtToDate.Text.Trim();

            DataSet ds = gf.ExecuteSelectSP("spSelect_TrnLedger_CashBookPrint", strValues);
            if (ds.Tables[0] != null)
            {
                gvGeneralLedger.DataSource = ds.Tables[0];
                gvGeneralLedger.DataBind();
            }

            //lblOpeningBal.Text = "Opening balance  :  " + ds.Tables[1].Rows[0][0].ToString();
            ////lblTotalDr.Text = "Total Debit between Dates  :  " + ds.Tables[2].Rows[0][0].ToString();  modified
            ////lblTotalCr.Text = "Total Credit between Dates  :  " + ds.Tables[3].Rows[0][0].ToString();
            //lblClosingBal.Text = "Closing balance  :  " + ds.Tables[2].Rows[0][0].ToString();


        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
           string[] _header = new string[4];
                if (Convert.ToInt32(Session["CompanyId"]) == 2)
                {

                    _header[0] = "<b>Bengal School Of Technology</b>";
                    _header[1] = "For " + " From " + txtFromDate.Text + " To " + txtToDate.Text;
                    _header[2] = "Printed on " + DateTime.Now.ToString();
                    _header[3] = "";
                }
                else
                {

                    _header[0] = "<b>Bengal School Of Technology & Management</b>";
                    _header[1] = "For " + " From " + txtFromDate.Text + " To " + txtToDate.Text;
                    _header[2] = "Printed on " + DateTime.Now.ToString();
                    _header[3] = "";
                }

            string[] _footer = new string[1];
            _footer[0] = "";

            gvGeneralLedger.ShowFooter = true;
            strValues = Session["CompanyID"].ToString() + chr.ToString();
            strValues += Session["BranchID"].ToString() + chr.ToString();
            //strValues += ddlLedger.SelectedValue.Trim() + chr.ToString();
            strValues += txtFromDate.Text.Trim() + chr.ToString();
            strValues += txtToDate.Text.Trim();

            DataSet ds = gf.ExecuteSelectSP("spSelect_TrnLedger_CashBookPrint", strValues);
            if (ds.Tables[0] != null)
            {
                gvGeneralLedger.DataSource = ds.Tables[0];
                gvGeneralLedger.DataBind();
            }
            

            string file = "LEDGER_BALANCE_REPORT";

                BusinessLayer.Common.Excel.SaveExcel(_header, gvGeneralLedger, _footer, file);
            
        }
        
        protected void btnPrint_Click(object sender, EventArgs e)
        {

            


            string Title = "Cash Book";
                string[] _header = new string[3];
                _header[0] = "For " + " From " + txtFromDate.Text + " To " + txtToDate.Text;
                _header[1] = "Printed on " + DateTime.Now.ToString();
                _header[2] = "";

                string[] _footer = new string[1];
                _footer[0] = "";
            


            GridViewRow row1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Normal);
                TableHeaderCell cell1 = new TableHeaderCell();


            cell1.Text = "";
            cell1.HorizontalAlign = HorizontalAlign.Center;
            cell1.ColumnSpan = 3;
            row1.Controls.Add(cell1);

            cell1 = new TableHeaderCell();
                cell1.ColumnSpan = 2;
                cell1.Text = "Receipt(Dr)";
                cell1.HorizontalAlign = HorizontalAlign.Center;
                row1.Controls.Add(cell1);

                cell1 = new TableHeaderCell();
                cell1.ColumnSpan = 2;
                cell1.Text = "Payment(Cr)";
                cell1.HorizontalAlign = HorizontalAlign.Center;
                row1.Controls.Add(cell1);


           //row.BackColor = System.Drawing.ColorTranslator.FromHtml("#3AC0F2");
            gvGeneralLedger.HeaderRow.Parent.Controls.AddAt(0, row1);

            DataTable dt = new DataTable();
            for (int i = 0; i < gvGeneralLedger.Columns.Count; i++)
            {
                dt.Columns.Add("column" + i.ToString());
            }
            foreach (GridViewRow row in gvGeneralLedger.Rows)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < gvGeneralLedger.Columns.Count; j++)
                {
                    dr["column" + j.ToString()] = row.Cells[j].Text;
                }
                dt.Rows.Add(dr);
            }


            gvGeneralLedger.ShowFooter = true;
            strValues = Session["CompanyID"].ToString() + chr.ToString();
            strValues += Session["BranchID"].ToString() + chr.ToString();
            //strValues += ddlLedger.SelectedValue.Trim() + chr.ToString();
            strValues += txtFromDate.Text.Trim() + chr.ToString();
            strValues += txtToDate.Text.Trim();
            DataSet ds = gf.ExecuteSelectSP("spSelect_TrnLedger_CashBookPrint", strValues);
            if (ds.Tables[0] != null)
            {
                gvGeneralLedger.DataSource = ds.Tables[0];
                gvGeneralLedger.DataBind();
            }


            Print.ReportPrint(Title, _header, gvGeneralLedger, _footer);
                Response.Redirect("CashBookShowGrid.aspx");
           
        }

        protected void gvGeneralLedger_DataBound(object sender, EventArgs e)
        {
            #region ADDING HEADER ROW            

            GridViewRow row1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Normal);
            TableHeaderCell cell1 = new TableHeaderCell();

            cell1.Text = "";
            cell1.HorizontalAlign = HorizontalAlign.Center;
            cell1.ColumnSpan = 3;
            row1.Controls.Add(cell1);

            cell1 = new TableHeaderCell();
            cell1.ColumnSpan = 2;
            cell1.Text = "Receipt(Dr)";
            cell1.HorizontalAlign = HorizontalAlign.Center;
            row1.Controls.Add(cell1);

            cell1 = new TableHeaderCell();
            cell1.ColumnSpan = 2;
            cell1.Text = "Payment(Cr)";
            cell1.HorizontalAlign = HorizontalAlign.Center;
            row1.Controls.Add(cell1);


            //row.BackColor = System.Drawing.ColorTranslator.FromHtml("#3AC0F2");
            gvGeneralLedger.HeaderRow.Parent.Controls.AddAt(0, row1);
            #endregion

        }
        protected void gvGeneralLedger_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                decimal TempTotalDrAmountBank = 0;
                decimal TempTotalDrAmountCash = 0;
                decimal TempTotalCrAmountBank = 0;
                decimal TempTotalCrAmountCash = 0;

                Label DrAmountBank = (Label)e.Row.FindControl("lblDrAmountBank");
                Label DrAmountCash = (Label)e.Row.FindControl("lblDrAmountCash");
                Label CrAmountBank = (Label)e.Row.FindControl("lblCrAmountBank");
                Label CrAmountCash = (Label)e.Row.FindControl("lblCrAmountCash");


                if (DrAmountBank.Text != "") TempTotalDrAmountBank = Convert.ToDecimal(DrAmountBank.Text);
                 TotalDrAmountBank += TempTotalDrAmountBank;

                if (DrAmountCash.Text != "") TempTotalDrAmountCash = Convert.ToDecimal(DrAmountCash.Text);
                 TotalDrAmountCash += TempTotalDrAmountCash;

                if (CrAmountBank.Text != "") TempTotalCrAmountBank = Convert.ToDecimal(CrAmountBank.Text);
                 TotalCrAmountBank += TempTotalCrAmountBank;

                if (CrAmountCash.Text != "") TempTotalCrAmountCash = Convert.ToDecimal(CrAmountCash.Text);
                TotalCrAmountCash += TempTotalCrAmountCash;
            }

            if (e.Row.RowType == DataControlRowType.Footer)

            {

                Label lblTotalDrAmountBank = (Label)e.Row.FindControl("lblTotalDrAmountBank");
                Label lblTotalDrAmountCash = (Label)e.Row.FindControl("lblTotalDrAmountCash");
                Label lblTotalCrAmountBank = (Label)e.Row.FindControl("lblTotalCrAmountBank");
                Label lblTotalCrAmountCash = (Label)e.Row.FindControl("lblTotalCrAmountCash");

                lblTotalDrAmountBank.Text = TotalDrAmountBank.ToString();
                lblTotalDrAmountCash.Text = TotalDrAmountCash.ToString();
                lblTotalCrAmountBank.Text = TotalCrAmountBank.ToString();
                lblTotalCrAmountCash.Text = TotalCrAmountCash.ToString();

                

            }

        }
        protected void ComponentGridView_Sorting(object sender, GridViewSortEventArgs e)
            {
                BindGridView(e.SortExpression, sortOrder);
            }
        private void BindGridView(string sortExp, string sortDir)
        {
            strValues = Session["CompanyID"].ToString() + chr.ToString();
            strValues += Session["BranchID"].ToString() + chr.ToString();
            //strValues += ddlLedger.SelectedValue.Trim() + chr.ToString();
            strValues += txtFromDate.Text.Trim() + chr.ToString();
            strValues += txtToDate.Text.Trim();

            DataSet ds = gf.ExecuteSelectSP("spSelect_TrnLedger_CashBookPrint", strValues);
            DataView myDataView = new DataView();
            myDataView = ds.Tables[0].DefaultView;
            if (sortExp != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
            }
            if (ds.Tables[0] != null)
            {
                gvGeneralLedger.DataSource = ds.Tables[0];
                gvGeneralLedger.DataBind();
            }

        }
    }
}