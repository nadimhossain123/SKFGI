﻿using System;
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
using DataAccess.Accounts;
using System.IO;

namespace CollegeERP.Accounts
{
    public partial class CashBankVoucher : System.Web.UI.Page
    {
        int colDRAmt = 3;
        int colCRAmt = 4;
        clsGeneralFunctions gf = new clsGeneralFunctions();
        DataSet ds;
        string strParams;
        char chr = Convert.ToChar(130);
        public string strFilter;
        ListItem li = new ListItem("Select", "0");
        public int RowIndex
        {
            get { return Convert.ToInt32(ViewState["RowIndex"]); }
            set { ViewState["RowIndex"] = value; }
        }

        string strValues = "";
        string strPrepareRPTHeader = "";
        string strPrepareRPTFooter = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.RECEIPT_PAYMENT_VOUCHER))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }

                ResetControls();

                ViewState["sortColumn"] = "VoucherDate";
                ViewState["sortDirection"] = gf.ConvertSortDirectionToSql(SortDirection.Descending);
                PopulateHeaderGrid(ViewState["sortColumn"].ToString(), ViewState["sortDirection"].ToString());
            }
        }

        private void AdjustButtons(bool bSave, bool bCancel, bool bPrint)
        {
            btnSave.Enabled = bSave;
            btnCancel.Enabled = bCancel;
            btnPrint.Enabled = bPrint;

        }

        private void ResetControls()
        {
            hdnCBVHeaderID.Value = "0";
            RowIndex = -1;
            Message.Show = false;
            TabContainer1.ActiveTabIndex = 0;

            PopulateAllDropDowns();

            txtVchNo.Text = "Auto Generated";
            txtChqNo.Text = "";
            txtChqDt.Text = "";
            txtVoucherDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            txtVoucherDate.Enabled = true;
            txtDrawnOn.Text = "";
            ddlType.SelectedIndex = 0;
            ddlType.Enabled = true;
            ddlSourceLedger.SelectedValue = "0";
            ltrLedgerBalance.Text = "";

            ddlLedg.SelectedValue = "0";
            ddlDRCR.SelectedIndex = 0;
            ddlCostCentre.SelectedValue = "0";
            txtAmt.Text = "0.00";

            txtAccount.Text = "0.00";
            txtNarration.Text = "";
            txtPayTo.Text = "";
            AdjustButtons(true, true, false);

            InitializeDataSet();
            //BlankDetailDS();
            Session["Details"] = ds;
            gvCshBnk.DataSource = ds.Tables[0].DefaultView;
            gvCshBnk.DataBind();

            gvCshBnk.Columns[colDRAmt].FooterText = "0.00";
            gvCshBnk.Columns[colCRAmt].FooterText = "0.00";

            btnSave.Text = "Save";

        }


        private void InitializeDataSet()
        {
            ds = new DataSet();
            ds.Tables.Add("TrnCashBankVoucherDetail");
            ds.Tables[0].Columns.Add("SrlNo");//sequence no in grid
            ds.Tables[0].Columns.Add("CBVDetailID");
            ds.Tables[0].Columns.Add("ByTo");
            ds.Tables[0].Columns.Add("LedgerID_FK");
            ds.Tables[0].Columns.Add("CostCenterID_FK");
            ds.Tables[0].Columns.Add("LedgerName");//Cash/Bank Ledger Name incase of Header AND CR/DR Ledger Name in case of Detail
            ds.Tables[0].Columns.Add("CostCenterName");
            ds.Tables[0].Columns.Add("DRAmount");
            ds.Tables[0].Columns.Add("CRAmount");

        }

        private void BlankDetailDS()
        {
            ds.Tables[0].Rows.Add();
            ds.Tables[0].Rows[0]["SrlNo"] = "";
            ds.Tables[0].Rows[0]["CBVDetailID"] = "0";
            ds.Tables[0].Rows[0]["ByTo"] = "";
            ds.Tables[0].Rows[0]["LedgerID_FK"] = "0";
            ds.Tables[0].Rows[0]["CostCenterID_FK"] = "0";
            ds.Tables[0].Rows[0]["LedgerName"] = "";
            ds.Tables[0].Rows[0]["CostCenterName"] = "";
            ds.Tables[0].Rows[0]["DRAmount"] = "0.00";
            ds.Tables[0].Rows[0]["CRAmount"] = "0.00";
            gvCshBnk.DataSource = ds.Tables[0].DefaultView;
            gvCshBnk.DataBind();

        }


        private string PrepareXMLString()
        {
            string strXMLString = "";
            ds = (DataSet)Session["Details"];
            strXMLString = "<NewDataSet>";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strXMLString += "<TrnCashBankVoucherDetail";
                strXMLString += " CBVDetailID = \"" + ds.Tables[0].Rows[i]["CBVDetailID"].ToString() + "\"";
                strXMLString += " SrlNo = \"" + ds.Tables[0].Rows[i]["SrlNo"].ToString() + "\"";
                strXMLString += " ByTo = \"" + ds.Tables[0].Rows[i]["ByTo"].ToString() + "\"";
                strXMLString += " LedgerID_FK = \"" + ds.Tables[0].Rows[i]["LedgerID_FK"].ToString() + "\"";
                strXMLString += " CostCenterID_FK = \"" + ds.Tables[0].Rows[i]["CostCenterID_FK"].ToString() + "\"";
                strXMLString += " DRAmount = \"" + ds.Tables[0].Rows[i]["DRAmount"].ToString() + "\"";
                strXMLString += " CRAmount = \"" + ds.Tables[0].Rows[i]["CRAmount"].ToString() + "\"";
                strXMLString += " />";
            }
            strXMLString += "</NewDataSet>";

            return strXMLString;
        }



        private void PopulateHeaderGrid(string strSortExpression, string sortDir)
        {
            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchID"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();

            string strHParams = strParams + chr.ToString();
            strHParams += "";

            strSortExpression = "VoucherDate";
            sortDir = "Desc";
            gf.BindGridViewSP(gvCBVView, "spSelect_TrnCashBankVoucher", strHParams, PrepareSearchFilter(), strSortExpression, sortDir);
        }

        private string PrepareSearchFilter()
        {
            string strFilterString = "";
            if (ddlVoucherType.SelectedValue.ToString() != "0")
            {
                strFilterString = "TransactionType = '" + ddlVoucherType.SelectedValue.ToString() + "'";
            }
            if (ddlLedgerVw.SelectedValue.ToString() != "0")
            {
                if (strFilterString == "")
                    strFilterString = "LedgerID_FK = " + ddlLedgerVw.SelectedValue.ToString();
                else
                    strFilterString += " AND LedgerID_FK = " + ddlLedgerVw.SelectedValue.ToString();
            }
            if (txtVDateFromVW.Text.Trim() != "")
            {
                if (strFilterString == "")
                    strFilterString = "VoucherDate >= '" + Convert.ToDateTime(txtVDateFromVW.Text.Trim()) + "'";
                else
                    strFilterString += " AND VoucherDate >= '" + Convert.ToDateTime(txtVDateFromVW.Text.Trim()) + "'";
            }
            if (txtVDateToVW.Text.Trim() != "")
            {
                if (strFilterString == "")
                    strFilterString = "VoucherDate <= '" + Convert.ToDateTime(txtVDateToVW.Text.Trim()) + "'";
                else
                    strFilterString += " AND VoucherDate <= '" + Convert.ToDateTime(txtVDateToVW.Text.Trim()) + "'";
            }
            if (txtVoucherNoVw.Text.Trim() != "")
            {
                if (strFilterString == "")
                    strFilterString = "CBVoucherNo like '" + txtVoucherNoVw.Text.Trim() + "%'";
                else
                    strFilterString += " AND CBVoucherNo like '" + txtVoucherNoVw.Text.Trim() + "%'";
            }
            return strFilterString;
        }

        
        private void PopulateAllDropDowns()
        {
            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchID"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();

            gf.BindAjaxDropDownColumnsBySP(ddlSourceLedger, "spSelect_MstGeneralLedgerBNKandCASH", strParams);
            ddlSourceLedger.Items.Insert(0, li);

            gf.BindDropDownColumnsBySP(ddlLedgerVw, "spSelect_MstGeneralLedgerBNKandCASH", strParams);
            ddlLedgerVw.Items.Insert(0, li);

            DataView dv = new DataView(gf.GetDropDownColumnsBySP("spSelect_MstGeneralLedgerFull", strParams));
            dv.RowFilter = "LedgerType NOT IN ('BNK', 'CASH')";

            if (dv != null)
            {
                ddlLedg.DataSource = dv;
                ddlLedg.DataBind();
            }
            ddlLedg.Items.Insert(0, li);
            
            strParams = Session["CompanyId"].ToString().Trim() + chr.ToString();
            strParams += "";
            gf.BindDropDownColumnsBySP(ddlCostCentre, "spSelect_MstCostCenter", strParams);
            ddlCostCentre.Items.Insert(0, li);
        }

        protected void PopulatePage(int intID)
        {
            Message.Show = false;
            RowIndex = -1;
            InitializeDataSet();
            DataSet ds1 = gf.ExecuteSelectSP("spSelect_TrnCashBankVoucher", Session["CompanyId"].ToString() + chr.ToString() + Session["FinYrID"].ToString() + chr.ToString() + Session["BranchID"].ToString() + chr.ToString() + Session["DataFlow"].ToString() + chr.ToString() + intID.ToString());

            if (ds1.Tables[0].Rows.Count > 0)
            {
                txtVchNo.Text = ds1.Tables[0].Rows[0]["CBVoucherNo"].ToString();
                txtVoucherDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["VoucherDate"].ToString()).ToString("dd MMM yyyy");
                txtVoucherDate.Enabled = false;
                ddlDRCR.SelectedValue = ddlType.SelectedValue = ds1.Tables[0].Rows[0]["TransactionType"].ToString();
                ddlType.Enabled = false;

                ddlSourceLedger.SelectedValue = ds1.Tables[0].Rows[0]["LedgerID_FK"].ToString();
                txtAccount.Text = Convert.ToDecimal(ds1.Tables[0].Rows[0]["OnAccountAmount"]).ToString("0.00");
                txtNarration.Text = ds1.Tables[0].Rows[0]["CBVNarration"].ToString();
                txtChqNo.Text = ds1.Tables[0].Rows[0]["ChequeNo"].ToString();// only for Header Info
                if (ds1.Tables[0].Rows[0]["ChequeDate"] == DBNull.Value)
                    txtChqDt.Text = "";
                else
                    txtChqDt.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["ChequeDate"]).ToString("dd MMM yyyy"); ;// only for Header Info
                txtDrawnOn.Text = ds1.Tables[0].Rows[0]["DrawnOn"].ToString();
                txtPayTo.Text = ds1.Tables[0].Rows[0]["PayTo"].ToString();

                DataSet ds2 = gf.ExecuteSelectSP("spSelect_TrnCashBankVoucherDetail", intID.ToString() + chr.ToString() + "");
                for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows.Add();
                    ds.Tables[0].Rows[i]["CBVDetailID"] = ds2.Tables[0].Rows[i]["CBVDetailID"];
                    ds.Tables[0].Rows[i]["SrlNo"] = ds2.Tables[0].Rows[i]["SrlNo"]; ;//sequence no of voucher item
                    ds.Tables[0].Rows[i]["ByTo"] = ds2.Tables[0].Rows[i]["ByTo"];
                    ds.Tables[0].Rows[i]["LedgerID_FK"] = ds2.Tables[0].Rows[i]["LedgerID_FK"];
                    ds.Tables[0].Rows[i]["CostCenterID_FK"] = ds2.Tables[0].Rows[i]["CostCenterID_FK"];
                    ds.Tables[0].Rows[i]["LedgerName"] = ds2.Tables[0].Rows[i]["LedgerName"];//Cash/Bank Ledger Name incase of Header AND CR/DR Ledger Name in case of Detail
                    ds.Tables[0].Rows[i]["CostCenterName"] = ds2.Tables[0].Rows[i]["CostCenterName"];
                    if (ds2.Tables[0].Rows[i]["ByTo"].ToString() == "RECEIVE")
                    {
                        ds.Tables[0].Rows[i]["DRAmount"] = Convert.ToDecimal(ds2.Tables[0].Rows[i]["DRAmount"]).ToString("0.00");
                        ds.Tables[0].Rows[i]["CRAmount"] = "0.00";
                    }
                    else
                    {
                        ds.Tables[0].Rows[i]["CRAmount"] = Convert.ToDecimal(ds2.Tables[0].Rows[i]["CRAmount"]).ToString("0.00");
                        ds.Tables[0].Rows[i]["DRAmount"] = "0.00";
                    }
                }

                btnSave.Text = "Update";

                if (txtVchNo.Text.ToString().Substring(4, 4) == "ENGG")
                {
                    btnPrint.Attributes.Add("onclick", "javascript:openPopup('RPTGridView.aspx?id=" + intID + "'); return false;"); 
                }
                else if (txtVchNo.Text.ToString().Substring(4, 4) == "DEPL")
                {
                    btnPrint.Attributes.Add("onclick", "javascript:openPopup('RPTGridViewDiploma.aspx?id=" + intID + "'); return false;");
                }
                else
                {
                    btnPrint.Attributes.Add("onclick", "javascript:openPopup('RPTGridViewMgmnt.aspx?id=" + intID + "'); return false;");
                }
            }
            else
                btnSave.Text = "Save";

            Session["Details"] = ds;
            GetLedgerDetails();
            calDRCR();
            gvCshBnk.DataSource = ds.Tables[0].DefaultView;
            gvCshBnk.DataBind();
            AdjustButtons(true, true, true);
        }

        private void calDRCR()
        {
            ds = (DataSet)Session["Details"];
            double TotDRAmt = 0;
            double TotCRAmt = 0;
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                TotDRAmt += Convert.ToDouble(ds.Tables[0].Rows[j]["DRAmount"]);
                TotCRAmt += Convert.ToDouble(ds.Tables[0].Rows[j]["CRAmount"]);
            }
            gvCshBnk.Columns[colDRAmt].FooterText = "<b>" + TotDRAmt.ToString("0.00") + "</b>";
            gvCshBnk.Columns[colCRAmt].FooterText = "<b>" + TotCRAmt.ToString("0.00") + "</b>";
            txtAccount.Text = Math.Abs(TotDRAmt - TotCRAmt).ToString("0.00");
        }

        protected bool DuplicateLedger(string strLedgerID, string strLedgerType, string strCostCenterID)
        {
            bool bLedger = false;
            ds = new DataSet();
            ds = (DataSet)Session["Details"];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["LedgerID_FK"].ToString() == strLedgerID && ds.Tables[0].Rows[i]["ByTo"].ToString() == strLedgerType && ds.Tables[0].Rows[i]["CostCenterID_FK"].ToString() == strCostCenterID && i != RowIndex)
                {
                    bLedger = true;
                    break;
                }
            }
            return bLedger;
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "PAYMENT")
                ddlDRCR.SelectedValue = "RECEIVE";
            else
                ddlDRCR.SelectedValue = "PAYMENT";

            //ddlDRCR.SelectedValue = ddlType.SelectedValue;

            ds = (DataSet)Session["Details"];

            for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
            {
                ChangeTransType(k);
            }

            Session["Details"] = ds;
            calDRCR();
            gvCshBnk.DataSource = ds.Tables[0].DefaultView;
            gvCshBnk.DataBind();
        }

        private void ChangeTransType(int i)
        {

            if (ds.Tables[0].Rows[i]["ByTo"].ToString() == "PAYMENT")
            {
                ds.Tables[0].Rows[i]["ByTo"] = "RECEIVE";
                ds.Tables[0].Rows[i]["DRAmount"] = ds.Tables[0].Rows[i]["CRAmount"].ToString();
                ds.Tables[0].Rows[i]["CRAmount"] = "0.00";
            }
            else
            {
                ds.Tables[0].Rows[i]["ByTo"] = "PAYMENT";
                ds.Tables[0].Rows[i]["CRAmount"] = ds.Tables[0].Rows[i]["DRAmount"].ToString();
                ds.Tables[0].Rows[i]["DRAmount"] = "0.00";
            }
            ds.Tables[0].AcceptChanges();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                string rtMsg = "";
                string strSPName = "";
                string strValues = "";
                int intID;

                strValues = DateTime.Now.ToString("dd MMM yyyy");
                strValues += chr.ToString() + "";
                DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);
                strValues = "";
                //if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() == Session["FinYrID"].ToString().Trim())
                //{
                    intID = Convert.ToInt32(hdnCBVHeaderID.Value);
                    if (intID == 0)
                    {
                        btnPrint.Enabled = false;
                        strSPName = "spInsert_TrnCashBankVoucher";
                    }
                    else
                    {
                        strValues = intID.ToString();
                        strSPName = "spUpdate_TrnCashBankVoucher";
                    }
                    ds = (DataSet)Session["Details"];
                    if (strValues == "")
                        strValues = Session["CompanyId"].ToString();
                    else
                        strValues += chr.ToString() + Session["CompanyId"].ToString();

                    strValues += chr.ToString() + Session["FinYrID"].ToString();
                    strValues += chr.ToString() + Session["BranchID"].ToString();
                    strValues += chr.ToString() + Session["DataFlow"].ToString();
                    strValues += chr.ToString() + txtVoucherDate.Text.Trim();
                    strValues += chr.ToString() + ddlType.SelectedItem.ToString();

                    if (txtChqNo.Text.Trim() == "")
                    {
                        strValues += chr.ToString() + "CASH";
                    }
                    else
                    {
                        strValues += chr.ToString() + "CHEQUE";
                    }
                    strValues += chr.ToString() + ddlSourceLedger.SelectedValue.ToString();
                    strValues += chr.ToString() + txtChqNo.Text.ToString();
                    strValues += chr.ToString() + txtChqDt.Text.ToString();
                    strValues += chr.ToString() + txtDrawnOn.Text.ToString();
                    strValues += chr.ToString() + txtAccount.Text;//@OnAccountAmount
                    strValues += chr.ToString() + txtAccount.Text; // @TotalAmount

                    strValues += chr.ToString() + txtNarration.Text.Trim();
                    strValues += chr.ToString() + Session["UserId"].ToString();
                    strValues += chr.ToString() + PrepareXMLString();
                    strValues += chr.ToString() + txtPayTo.Text.Trim();

                    rtMsg = gf.ExecuteAnySPOutput(strSPName, strValues);
                    string strMsg = rtMsg.Substring(0, 4);
                    if (strMsg == "True")
                    {
                        hdnCBVHeaderID.Value = rtMsg.Substring(5, rtMsg.Length - 5);
                        PopulatePage(Convert.ToInt32(hdnCBVHeaderID.Value));
                        Message.IsSuccess = true;
                        Message.Text = "Your request has been processed successfully!";
                        AdjustButtons(true, true, true);
                        PopulateHeaderGrid(ViewState["sortColumn"].ToString(), ViewState["sortDirection"].ToString());

                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = rtMsg;
                    }
                //}
                //else
                //{
                  //  Message.IsSuccess = false;
                    //Message.Text = "Please select a Date Between " + Session["SesFromDate"].ToString() + " & " + Session["SesToDate"].ToString() + "";
               // }
            }
            Message.Show = true;
        }

        protected bool Validate()
        {
            bool Result = true;
            string ErrorText = "";

            strValues = txtVoucherDate.Text.Trim();
            strValues += chr.ToString() + "";
            DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

            if (ddlSourceLedger.SelectedValue == "0" || ddlSourceLedger.Text == string.Empty)
            {
                Result = false;
                ErrorText = "Please Select Source Ledger";
            }
            else if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() != Session["FinYrID"].ToString().Trim())
            {
                Result = false;
                ErrorText = "Sorry! Voucher Date is not Within Current Financial Year. Please Check.";
            }
            //else if (Convert.ToDateTime(txtVoucherDate.Text.Trim()) > DateTime.Now)
            //{
            //    Result = false;
            //    ErrorText = "Sorry! Future Voucher Date is Not Allowed.";
            //}

            if (!Result)
            {
                Message.IsSuccess = false;
                Message.Text = ErrorText;
            }
            return Result;
        }

        protected bool IsValidDate(string input)
        {
            try
            {
                DateTime dt = DateTime.Parse(input);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (ddlLedg.SelectedValue == "0" || ddlLedg.Text == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Ledger is mandatory field!";
                Message.Show = true;
            }
            else if (Convert.ToDecimal(txtAmt.Text) == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Amount is mandatory field!";
                Message.Show = true;
            }
            else
            {
                if (!DuplicateLedger(ddlLedg.SelectedValue, ddlDRCR.SelectedValue,ddlCostCentre.SelectedValue))
                {
                    AddGridDetails();
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "This Ledger has already been selected! Please select a different Ledger.";
                    Message.Show = true;
                }

            }
        }

        protected void AddGridDetails()
        {
            ds = (DataSet)Session["Details"];
            if (RowIndex == -1)
            {
                int i = ds.Tables[0].Rows.Count;
                ds.Tables[0].Rows.Add();
                ds.Tables[0].Rows[i]["CBVDetailID"] = "0";
                ds.Tables[0].Rows[i]["SrlNo"] = (i + 1).ToString();
                ds.Tables[0].Rows[i]["ByTo"] = ddlDRCR.SelectedValue;
                ds.Tables[0].Rows[i]["LedgerID_FK"] = ddlLedg.SelectedValue;
                ds.Tables[0].Rows[i]["CostCenterID_FK"] = ddlCostCentre.SelectedValue;

                if (ddlLedg.SelectedValue == "0")
                    ds.Tables[0].Rows[i]["LedgerName"] = "";
                else
                    ds.Tables[0].Rows[i]["LedgerName"] = ddlLedg.SelectedItem.ToString();

                if (ddlCostCentre.SelectedValue == "0")
                    ds.Tables[0].Rows[i]["CostCenterName"] = "";
                else
                    ds.Tables[0].Rows[i]["CostCenterName"] = ddlCostCentre.SelectedItem.ToString();


                if (ddlDRCR.SelectedValue == "PAYMENT")
                {
                    ds.Tables[0].Rows[i]["CRAmount"] = txtAmt.Text;
                    ds.Tables[0].Rows[i]["DRAmount"] = "0.00";
                }
                else
                {
                    ds.Tables[0].Rows[i]["CRAmount"] = "0.00";
                    ds.Tables[0].Rows[i]["DRAmount"] = txtAmt.Text;
                }
                ds.Tables[0].AcceptChanges();

            }
            else
            {
                ds.Tables[0].Rows[RowIndex]["ByTo"] = ddlDRCR.SelectedValue;
                ds.Tables[0].Rows[RowIndex]["LedgerID_FK"] = ddlLedg.SelectedValue;
                ds.Tables[0].Rows[RowIndex]["CostCenterID_FK"] = ddlCostCentre.SelectedValue;

                if (ddlLedg.SelectedValue == "0")
                    ds.Tables[0].Rows[RowIndex]["LedgerName"] = "";
                else
                    ds.Tables[0].Rows[RowIndex]["LedgerName"] = ddlLedg.SelectedItem.ToString();

                if (ddlCostCentre.SelectedValue == "0")
                    ds.Tables[0].Rows[RowIndex]["CostCenterName"] = "";
                else
                    ds.Tables[0].Rows[RowIndex]["CostCenterName"] = ddlCostCentre.SelectedItem.ToString();


                if (ddlDRCR.SelectedValue == "PAYMENT")
                {
                    ds.Tables[0].Rows[RowIndex]["CRAmount"] = txtAmt.Text;
                    ds.Tables[0].Rows[RowIndex]["DRAmount"] = "0.00";
                }
                else
                {
                    ds.Tables[0].Rows[RowIndex]["CRAmount"] = "0.00";
                    ds.Tables[0].Rows[RowIndex]["DRAmount"] = txtAmt.Text;
                }
                ds.Tables[0].AcceptChanges();

            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["Details"] = ds;
                calDRCR();
                gvCshBnk.DataSource = ds.Tables[0].DefaultView;
                gvCshBnk.DataBind();
                txtAmt.Text = "0.00";
            }
            RowIndex = -1;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateHeaderGrid(ViewState["sortColumn"].ToString(), ViewState["sortDirection"].ToString());
        }


        protected void gvCBVView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCBVView.PageIndex = e.NewPageIndex;
            PopulateHeaderGrid(ViewState["sortColumn"].ToString(), ViewState["sortDirection"].ToString());
        }

        protected void gvCBVView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToString() == "imgbtnEdit")
            {
                hdnCBVHeaderID.Value = e.CommandArgument.ToString();
                PopulatePage(Convert.ToInt32(hdnCBVHeaderID.Value));
            }
            //if (e.CommandName.ToString() == "Delete")
            //{
            //    BusinessLayer.Accounts.StudentFeesCollection objSfc = new BusinessLayer.Accounts.StudentFeesCollection();
            //    int headerId=int.Parse(e.CommandArgument.ToString());
            //    objSfc.DeleteCVB(headerId);
            //    //****************
            //    PopulateHeaderGrid(ViewState["sortColumn"].ToString(), ViewState["sortDirection"].ToString());
            
            //}
        }

        protected void gvCBVView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet DS = gf.ExecuteSelectSP("USP_GetEmpPerMission_Delete_Voucher", Session["UserId"].ToString()
                );

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                ImageButton btnDelete = (ImageButton)e.Row.FindControl("btnDelete");
                if (DS.Tables[0].Rows[0][0].ToString() == "0")
                {

                    btnDelete.Visible = false;
                }
                else
                {
                    btnDelete.Visible = true;
                }
                ImageButton ib = new ImageButton();
                ib = (ImageButton)e.Row.FindControl("imgbtnEdit");
                //ib.CommandArgument = ((DataRowView)e.Row.DataItem)["CBVHeaderID"].ToString();
                //************************
                string VoucherNo = ((DataRowView)e.Row.DataItem)["CBVoucherNo"].ToString();
                string[] args = ib.CommandArgument.ToString().Split(',');
                //************************
                ib.Attributes["onClick"] = "changeTab();";
                ImageButton ibPrint = new ImageButton();
                ibPrint = (ImageButton)e.Row.FindControl("imgbtnPrint");
                ibPrint.CommandArgument = e.Row.RowIndex.ToString();
                if (((DataRowView)e.Row.DataItem)["CBVoucherNo"].ToString().Substring(4, 4) == "ENGG")
                {
                    ibPrint.Attributes.Add("onclick", "javascript:openPopup('RPTGridView.aspx?id=" + ((DataRowView)e.Row.DataItem)["CBVHeaderID"].ToString() + "'); return false;");
                }
                else if (((DataRowView)e.Row.DataItem)["CBVoucherNo"].ToString().Substring(4, 4) == "DEPL")
                {
                    ibPrint.Attributes.Add("onclick", "javascript:openPopup('RPTGridViewDiploma.aspx?id=" + ((DataRowView)e.Row.DataItem)["CBVHeaderID"].ToString() + "'); return false;");
                }
                else if (((DataRowView)e.Row.DataItem)["CBVoucherNo"].ToString().Substring(4, 4) == "SIMT")
                {
                    ibPrint.Attributes.Add("onclick", "javascript:openPopup('RPTGridViewSIMT.aspx?id=" + ((DataRowView)e.Row.DataItem)["CBVHeaderID"].ToString() + "'); return false;");
                }
                else
                {
                    ibPrint.Attributes.Add("onclick", "javascript:openPopup('RPTGridViewMgmnt.aspx?id=" + ((DataRowView)e.Row.DataItem)["CBVHeaderID"].ToString() + "'); return false;");
                }
             }
        }

        protected void gvCshBnk_RowEditing(object sender, GridViewEditEventArgs e)
        {
            RowIndex = e.NewEditIndex;
            PopulateControlUpdate();

        }

        private void PopulateControlUpdate()
        {
            Message.Show = false;
            ds = (DataSet)Session["Details"];

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlLedg.SelectedValue = ds.Tables[0].Rows[RowIndex]["LedgerID_FK"].ToString();
                ddlCostCentre.SelectedValue = ds.Tables[0].Rows[RowIndex]["CostCenterID_FK"].ToString();
                ddlDRCR.SelectedValue = ds.Tables[0].Rows[RowIndex]["ByTo"].ToString();
                if (ddlDRCR.SelectedValue == "RECEIVE")
                    txtAmt.Text = Convert.ToDouble(ds.Tables[0].Rows[RowIndex]["DRAmount"]).ToString("0.00");
                else
                    txtAmt.Text = Convert.ToDouble(ds.Tables[0].Rows[RowIndex]["CRAmount"]).ToString("0.00");
            }
        }

        protected void ddlSourceLedger_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLedgerDetails();
        }

        protected void GetLedgerDetails()
        {
            ltrLedgerBalance.Text = "";

            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchId"].ToString() + chr.ToString();
            strParams += ddlSourceLedger.SelectedValue.ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString() + chr.ToString();

            DataSet ds = gf.ExecuteSelectSP("spSelect_MstGeneralLedgerRefDetails", strParams);
            if (ds.Tables[0].Rows.Count > 0)
            {
                decimal ClosingBalance = Convert.ToDecimal(ds.Tables[0].Rows[0]["ClosingBalance"].ToString());
                if (ClosingBalance < 0)
                    ltrLedgerBalance.Text = "<i>Cur Bal.   </i><b style='color:Red;'>" + Math.Abs(ClosingBalance).ToString("n") + " Cr</b>";
                else
                    ltrLedgerBalance.Text = "<i>Cur Bal.   </i><b style='color:#259D17;'>" + Math.Abs(ClosingBalance).ToString("n") + " Dr</b>";
            }
        }

        private void DeleteCBV()
        {
           // BusinessLayer.Accounts.StudentFeesCollection objSfc = new BusinessLayer.Accounts.StudentFeesCollection();
           // objSfc.DeleteCVB(
        }

        protected void gvCBVView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            BusinessLayer.Accounts.StudentFeesCollection objSfc = new BusinessLayer.Accounts.StudentFeesCollection();
            int headerId = int.Parse( gvCBVView.DataKeys[e.RowIndex].Value.ToString());
            objSfc.DeleteCVB(headerId);
            //****************
            PopulateHeaderGrid(ViewState["sortColumn"].ToString(), ViewState["sortDirection"].ToString());
        }
    }
}
