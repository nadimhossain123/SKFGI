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
using BusinessLayer.Accounts;
using System.Globalization;

namespace CollegeERP.Accounts
{
    public partial class FundTransfer : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        string strParams = "";
        string strValues = "";
        //string strPrepareRPTHeader = "";
        //string strPrepareRPTFooter = "";
        public string strFilter;
        ListItem li = new ListItem("Select", "0");

        public int CVHeaderID
        {
            get { return Convert.ToInt32(ViewState["CVHeaderID"]); }
            set { ViewState["CVHeaderID"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.FUND_TRANSFER))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }

                LoadDropdown();
                ResetControls();
                PopulateHeaderGrid();
            }
        }

        protected void LoadDropdown()
        {
            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchID"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();

            //gf.BindAjaxDropDownColumnsBySP(ddlLedger, "spSelect_MstGeneralLedgerBNKandCASH", strParams);
            ddlLedger.Items.Insert(0, li);

            //gf.BindAjaxDropDownColumnsBySP(ddlParentLedger, "spSelect_CompanyName", strParams);
            ddlParentLedger.Items.Insert(0, li);


            gf.BindAjaxDropDownColumnsBySP(  ddlComp1 , "spSelect_CompanyName", strParams);
            ddlComp1.Items.Insert(0, li);

            gf.BindAjaxDropDownColumnsBySP(ddlComp2, "spSelect_CompanyName", strParams);
            ddlComp2.Items.Insert(0, li);
        }

        protected void ResetControls()
        {
            CVHeaderID = 0;
            btnSave.Text = "Save";
            Message.Show = false;

            txtVchNo.Text = "Auto Generated";
            txtVoucherDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            txtChequeNo.Text = "";
            txtChequeDate.Text = "";
            txtDrawnOn.Text = "";
            ddlParentLedger.SelectedValue = "0";
            ddlLedger.SelectedValue = "0";
            ddlComp1.SelectedValue = "0";
            ddlComp2.SelectedValue = "0";
            ddlDrCr.SelectedIndex = 0;
            ltrDrCr.Text = "<b>CR</b>";
            txtAmount.Text = "";
            txtAmountCopy.Text = "";
            txtNarration.Text = "Enter Voucher Narration...";
            ltrLedgerBalance.Text = "";
            ltrParentLedgerBalance.Text = "";
            btnPrint.Attributes["onClick"] = "alert('Please Create a Voucher Before Printing')";

        }

        protected void PopulatePage()
        {
            strValues = Session["CompanyID"].ToString();
            strValues += chr.ToString() + Session["FinYrID"].ToString();
            strValues += chr.ToString() + Session["BranchID"].ToString();
            strValues += chr.ToString() + Session["DataFlow"].ToString();
            strValues += chr.ToString() + CVHeaderID.ToString();
            DataSet ds = gf.ExecuteSelectSP("spSelect_trnFundTransfer", strValues);

            txtVchNo.Text = ds.Tables[0].Rows[0]["CVoucherNo"].ToString();
            txtVoucherDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["CVoucherDate"].ToString()).ToString("dd MMM yyyy");
         
           //*************************Ledger*******************
            ddlComp1.SelectedValue = ds.Tables[0].Rows[0]["CompanyID_FK"].ToString();
            PopulateLedger();
            strParams = String.Empty;
            strParams = ds.Tables[0].Rows[0]["LedgerId_FK"].ToString() + chr.ToString();
            strParams += ds.Tables[0].Rows[0]["CompanyID_FK"].ToString();
            LoadLedgerBalance(ddlLedger, ltrLedgerBalance, strParams);
            ddlLedger.SelectedValue = ds.Tables[0].Rows[0]["LedgerId_FK"].ToString();
            //********************Ledger Parent*****************            
            ddlComp2.SelectedValue = ds.Tables[0].Rows[0]["CompanyID_FK2"].ToString();
            PopulateLedgerParent();
            strParams = String.Empty;
            strParams = ds.Tables[0].Rows[0]["ParentLedgerId_FK"].ToString() + chr.ToString();
            strParams += ds.Tables[0].Rows[0]["CompanyID_FK2"].ToString();
            LoadLedgerBalance(ddlParentLedger, ltrParentLedgerBalance, strParams);
            ddlParentLedger.SelectedValue = ds.Tables[0].Rows[0]["ParentLedgerId_FK"].ToString();
            //**************************************************
            txtChequeNo.Text = ds.Tables[0].Rows[0]["ChequeNo"].ToString();
            if (ds.Tables[0].Rows[0]["ChequeDate"].ToString().Trim().Length > 0)
                txtChequeDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ChequeDate"].ToString()).ToString("dd MMM yyyy");
            else
                txtChequeDate.Text = "";

            txtDrawnOn.Text = ds.Tables[0].Rows[0]["DrawnOn"].ToString();
            ddlDrCr.SelectedValue = ds.Tables[0].Rows[0]["TransactionType"].ToString();
            ltrDrCr.Text = (ddlDrCr.SelectedValue == "DR") ? "<b>CR</b>" : "</b>DR</b>";

            txtAmount.Text = txtAmountCopy.Text = ds.Tables[0].Rows[0]["TotalAmount"].ToString();
            txtNarration.Text = ds.Tables[0].Rows[0]["CVNarration"].ToString();

            Message.Show = false;
            btnSave.Text = "Update";

            //btnPrint.Attributes.Add("onclick", "javascript:openPopup('PrintContraVoucher.aspx?id=" + CVHeaderID + "'); return false;");
        }

        protected void ddlLedger_SelectedIndexChanged(object sender, EventArgs e)
        {
            strParams = ddlLedger.SelectedValue.ToString() + chr.ToString();
            strParams += ddlComp1.SelectedValue.ToString();
            LoadLedgerBalance(ddlLedger, ltrLedgerBalance, strParams);
        }

        protected void LoadLedgerBalance(AjaxControlToolkit.ComboBox ddl, Literal ltr, string strParams)
        {
                     
            ltr.Text = "";
            DataSet ds = gf.ExecuteSelectSP("spSelect_MstGeneralLedgerByCompany", strParams);
            if (ds.Tables[0].Rows.Count > 0)
            {
                decimal ClosingBalance = Convert.ToDecimal(ds.Tables[0].Rows[0]["ClosingBalance"].ToString());
                if (ClosingBalance < 0)
                    ltr.Text = "<i>Cur Bal.   </i><b style='color:Red;'>" + Math.Abs(ClosingBalance).ToString("n") + " Cr</b>";
                else
                    ltr.Text = "<i>Cur Bal.   </i><b style='color:#259D17;'>" + Math.Abs(ClosingBalance).ToString("n") + " Dr</b>";
            }
            txtAmountCopy.Text = txtAmount.Text;
        }

        protected void ddlParentLedger_SelectedIndexChanged(object sender, EventArgs e)
        {
            strParams = ddlParentLedger.SelectedValue.ToString() + chr.ToString();
            strParams += ddlComp2.SelectedValue.ToString();
            LoadLedgerBalance(ddlParentLedger, ltrParentLedgerBalance, strParams);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            strValues = DateTime.Now.ToString("dd MMM yyyy");
            strValues += chr.ToString() + "";
            DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

            //if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() == Session["FinYrID"].ToString().Trim())
            //{
                if (Validate())
                {
                    if (ddlComp1.SelectedValue == ddlComp2.SelectedValue)
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Not Allowed";
                        Message.Show = true;
                    }
                    else
                    {
                        SaveVoucherDetails();
                    }
                }
            //}
            //else
            //{
            //    Message.IsSuccess = false;
            //    Message.Text = "Please select a Date Between " + Session["SesFromDate"].ToString() + " & " + Session["SesToDate"].ToString();
            //}
            Message.Show = true;
        }


        private void SaveVoucherDetails()
        {
            string rtMsg = "";
            string Msg = "";
            string strSPName = "";
            strValues = "";

            if (CVHeaderID == 0)
            {
                strSPName = "spInsert_TrnFundTransfer";
            }
            else
            {
                //strValues = CVHeaderID.ToString();
                //strSPName = "spUpdate_TrnCONTRAVoucher";
            }

            if (strValues == "")
                strValues = txtVchNo.Text.Trim();
            else
                strValues += chr.ToString() + txtVchNo.Text.Trim();

            strValues += chr.ToString() + ddlComp1.SelectedValue.ToString().Trim();
            strValues += chr.ToString() + Session["FinYrID"].ToString().Trim();
            strValues += chr.ToString() + Session["BranchId"].ToString().Trim();
            strValues += chr.ToString() + Session["DataFlow"].ToString().Trim();
            strValues += chr.ToString() + txtVoucherDate.Text.ToString().Trim();
            strValues += chr.ToString() + "1";
            strValues += chr.ToString() + ddlLedger.SelectedValue.Trim();
            strValues += chr.ToString() + ddlDrCr.SelectedValue.Trim();

            if (txtChequeNo.Text.Trim() == "")
                strValues += chr.ToString() + "CASH";
            else
                strValues += chr.ToString() + "CHEQUE";

            strValues += chr.ToString() + txtChequeNo.Text.Trim();
            strValues += chr.ToString() + txtChequeDate.Text.Trim();
            strValues += chr.ToString() + txtDrawnOn.Text.Trim();
            strValues += chr.ToString() + ddlParentLedger.SelectedValue.Trim();
            strValues += chr.ToString() + txtAmount.Text.Trim();
            strValues += chr.ToString() + txtNarration.Text.ToString().Trim();
            strValues += chr.ToString() + Session["UserId"].ToString().Trim();
            strValues += chr.ToString() + ddlComp2.SelectedValue.ToString().Trim();

            rtMsg = gf.ExecuteAnySPOutput(strSPName, strValues);
            Msg = rtMsg.Substring(0, 4);

            if (Msg == "True")
            {
                CVHeaderID = Convert.ToInt32(rtMsg.Substring(5, rtMsg.Length - 5));
                PopulateHeaderGrid();
                PopulatePage();

                Message.IsSuccess = true;
                Message.Text = "Your request has been processed successfully!";

            }
            else
            {
                if (rtMsg == "Duplicate:")
                {
                    Message.IsSuccess = false;
                    Message.Text = "This Voucher No. is already exists!";

                }
            }
            Message.Show = true;
        }

        protected bool Validate()
        {
            bool result = true;
            string error = "";
            strValues = txtVoucherDate.Text.Trim();
            strValues += chr.ToString() + "";
            DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

            if (ddlLedger.SelectedValue == "0" || ddlLedger.Text == string.Empty)
            {
                result = false;
                error = "Please Select Cash/Bank Book";
            }
            else if (ddlParentLedger.SelectedValue == "0" || ddlParentLedger.Text == string.Empty)
            {
                result = false;
                error = "Please Select Cash/Bank Book";
            }
            else if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() != Session["FinYrID"].ToString().Trim())
            {
                result = false;
                error = "Sorry! Voucher Date is not Within Current Financial Year. Please Check.";
            }
            else if (Convert.ToDateTime(txtVoucherDate.Text.Trim()) > DateTime.Now)
            {
                result = false;
                error = "Sorry! Future Voucher Date is Not Allowed.";
            }

            if (result == false)
            {
                Message.IsSuccess = false;
                Message.Text = error;
            }
            return result;

        }


        private void PopulateHeaderGrid()
        {
            strParams = Session["CompanyId"].ToString().Trim();
            strParams += chr.ToString() + Session["FinYrID"].ToString().Trim();
            strParams += chr.ToString() + Session["BranchId"].ToString().Trim();
            strParams += chr.ToString() + Session["DataFlow"].ToString().Trim();
            string strHParams = strParams + chr.ToString() + "";
            gf.BindGridViewSP(grdvwtrnsctnsearch, "spSelect_trnFundTransfer", strHParams, PrepareSearchFilter());

        }

        private string PrepareSearchFilter()
        {
            string strFilterString = "";
            if (txtVchNoSearch.Text.ToString() != "")
            {
                strFilterString = "CVoucherNo = '" + txtVchNoSearch.Text.ToString() + "'";
            }
            if (txtVchDateSearch.Text.Trim() != "")
            {
                if (strFilterString == "")
                    strFilterString = "CVoucherDate >= '" + Convert.ToDateTime(txtVchDateSearch.Text.Trim()) + "'";
                else
                    strFilterString += " AND CVoucherDate >= '" + Convert.ToDateTime(txtVchDateSearch.Text.Trim()) + "'";
            }
            if (txtVchDateSearchTo.Text.Trim() != "")
            {
                if (strFilterString == "")
                    strFilterString = "CVoucherDate <= '" + Convert.ToDateTime(txtVchDateSearchTo.Text.Trim()) + "'";
                else
                    strFilterString += " AND CVoucherDate <= '" + Convert.ToDateTime(txtVchDateSearchTo.Text.Trim()) + "'";
            }
            return strFilterString;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateHeaderGrid();
        }

        protected void grdvwtrnsctnsearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton ib = new ImageButton();
                ib = (ImageButton)e.Row.FindControl("imgbtnedit");
                ib.Attributes["onClick"] = "changeTab();";
                //((ImageButton)e.Row.FindControl("imgbtnDelete")).Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.DELETE_VOUCHER);
                //((ImageButton)e.Row.FindControl("btnPrint")).Attributes.Add("onclick", "javascript:openPopup('PrintContraVoucher.aspx?id=" + grdvwtrnsctnsearch.DataKeys[e.Row.RowIndex].Value.ToString() + "'); return false;");
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        protected void grdvwtrnsctnsearch_RowEditing(object sender, GridViewEditEventArgs e)
        {
            CVHeaderID = Convert.ToInt32(grdvwtrnsctnsearch.DataKeys[e.NewEditIndex].Value);
            PopulatePage();
        }

        protected void grdvwtrnsctnsearch_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(grdvwtrnsctnsearch.DataKeys[e.RowIndex].Value);
            bool Success = Convert.ToBoolean(gf.ExecuteAnySPOutput("spDelete_TrnContraVoucher", id.ToString()));
            if (Success)
               PopulateHeaderGrid();
        }

        protected void ddlDrCr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDrCr.SelectedValue == "DR")
                ltrDrCr.Text = "<b>CR</b>";
            else if (ddlDrCr.SelectedValue == "CR")
                ltrDrCr.Text = "<b>DR</b>";
        }

        protected void txtVchNo_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlComp1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateLedger();
        }
        protected void PopulateLedger()
        {
            strParams = ddlComp1.SelectedIndex.ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchID"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();


            gf.BindAjaxDropDownColumnsBySP(ddlLedger, "spSelect_MstGeneralLedgerBNKandCASH", strParams);
            ddlLedger.Items.Insert(0, li);
        }

        protected void ddlComp2_SelectedIndexChanged(object sender, EventArgs e)
        {

            PopulateLedgerParent();
        }

        protected void PopulateLedgerParent()
        {
            strParams = ddlComp2.SelectedIndex.ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchID"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();

            gf.BindAjaxDropDownColumnsBySP(ddlParentLedger, "spSelect_MstGeneralLedgerBNKandCASH", strParams);
            ddlParentLedger.Items.Insert(0, li);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }
    }
}
