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

namespace CollegeERP.Accounts
{
    public partial class StudentJournalTran : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        string strParams = "";
        ListItem li = new ListItem("---SELECT---", "0");
        ListItem liS = new ListItem(" ", "0");
        DataSet dsD;
        public int RowIndex
        {
            get { return Convert.ToInt32(ViewState["RowIndex"]); }
            set { ViewState["RowIndex"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_JOURNAL))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadCashBankLedger();
                LoadApprovedStudent();
                ClearControls();

                ViewState["sortColumn"] = "JVoucherDate";

                ViewState["sortDirection"] = gf.ConvertSortDirectionToSql(SortDirection.Descending);
                PopulateHeaderGrid(ViewState["sortColumn"].ToString(), ViewState["sortDirection"].ToString());
                //ViewState["sortColumntransaction"] = "Ledger Name";
                //ViewState["sortDirectiontransaction"] = gf.ConvertSortDirectionToSql(SortDirection.Descending);
            }
        }

        protected void LoadCashBankLedger()
        {
            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchId"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();

            DataView dv = new DataView(gf.GetDropDownColumnsBySP("spSelect_MstGeneralLedgerFull", strParams));
            dv.RowFilter = "LedgerType Not IN ('CASH','BNK')";

            if (dv != null)
            {
                ddlCashBankLedger.DataSource = dv;
                ddlCashBankLedger.DataBind();
            }
            ddlCashBankLedger.Items.Insert(0, li); 
        }

        private void InitializeDataSet()
        {
            dsD = new DataSet();
            dsD.Tables.Add("TrnVoucherDetails");//TrnSalesOrderDetail
            dsD.Tables[0].Columns.Add("JVHeaderID_FK");
            dsD.Tables[0].Columns.Add("JVDetailID");
            dsD.Tables[0].Columns.Add("Serial No");
            dsD.Tables[0].Columns.Add("BY/To");
            dsD.Tables[0].Columns.Add("Ledger Name");
            //dsD.Tables[0].Columns.Add("Closing Balance");
            //dsD.Tables[0].Columns.Add("Type");
            dsD.Tables[0].Columns.Add("Cost Center");
            dsD.Tables[0].Columns.Add("Reference No");
            dsD.Tables[0].Columns.Add("Amount DR");
            dsD.Tables[0].Columns.Add("Amount CR");
            dsD.Tables[0].Columns.Add("LedgerID");
            dsD.Tables[0].Columns.Add("CostCenterId");
            dsD.Tables[0].Columns.Add("StudentId");
            Session["SOD"] = dsD;

        }
        private void BlankDataSet()
        {
            InitializeDataSet();
            dsD.Tables[0].Rows.Add();
            dsD.Tables[0].Rows[0]["JVHeaderID_FK"] = "0";
            dsD.Tables[0].Rows[0]["JVDetailID"] = "0";
            dsD.Tables[0].Rows[0]["Serial No"] = "";
            dsD.Tables[0].Rows[0]["BY/To"] = "";
            dsD.Tables[0].Rows[0]["Ledger Name"] = "";
            //dsD.Tables[0].Rows[0]["Closing Balance"] = "";
            // dsD.Tables[0].Rows[0]["Type"] = "";
            dsD.Tables[0].Rows[0]["Cost Center"] = "";
            dsD.Tables[0].Rows[0]["Reference No"] = "";
            dsD.Tables[0].Rows[0]["Amount DR"] = "";
            dsD.Tables[0].Rows[0]["Amount CR"] = "";
            dsD.Tables[0].Rows[0]["LedgerID"] = "";
            dsD.Tables[0].Rows[0]["CostCenterId"] = "0";
            dsD.Tables[0].Rows[0]["StudentId"]="0";
            //grdvwtrnsctn.DataSource = dsD.Tables[0].DefaultView;
            //grdvwtrnsctn.DataBind();
            InitializeDataSet();
        }

        protected void LoadApprovedStudent()
        {
            DataAccess.student.LibraryFine objDFine = new DataAccess.student.LibraryFine();
            BusinessLayer.Student.LibraryFine ObjFine = new BusinessLayer.Student.LibraryFine();
            DataView dv = new DataView(objDFine.GetApprovedStudentListWithDropOut());
            dv.RowFilter = "company_id=" + Convert.ToInt32(Session["CompanyId"].ToString());

            if (dv != null)
            {
                ddlStudent.DataSource = dv;
                ddlStudent.DataBind();
            }
            ddlStudent.Items.Insert(0, li);
        }

        protected void ClearControls()
        {
            Message.Show = false;
            txtReceiptNo.Text = "Auto Generated";
            txtVoucherDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            hdnJournalID.Value = "0";
            lblDropOut.Visible = false;
            ltrLedgerBalance.Text = "";
            txtNarration.Text = "";
           
            ddlSemester.SelectedIndex = 0;
            ltrDrCr.Text = "<b>CR</b>";
           
            ddlCashBankLedger.SelectedIndex = 0;
            ddlStudent.SelectedIndex = 0;
            ImgPhoto.ImageUrl = "../Student/StudentPhoto/Male.jpg";

            dgvFeesHead.DataSource = null;
            dgvFeesHead.DataBind();

            //------------------
            txtVchDateSearch.Text = DateTime.Now.AddDays(-7).ToString("dd MMM yyyy");
            txtVchDateSearch.Attributes.Add("readonly", "readonly");
            txtVchDateSearchTo.Text = DateTime.Now.ToString("dd MMM yyyy");
            txtVchDateSearchTo.Attributes.Add("readonly", "readonly");
            //------------------
            btnSave.Text = "Save";
            txtAmount.Text = string.Empty;
            //------------------14-09-2013
            LoadBatch();
            // LoadStream();
            LoadCourse();

            btnPrint.Attributes.Add("onclick", "javascript:alert('No Money Receipt To Print'); return false;");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlStudent.SelectedValue != "0" && ddlStudent.Text != string.Empty)
            {
                BusinessLayer.Accounts.StudentFeesCollection ObjFees = new BusinessLayer.Accounts.StudentFeesCollection();
                int StudentId = Convert.ToInt32(ddlStudent.SelectedValue.Trim());

                DataSet ds = ObjFees.GetStudentUnpaidTrans(StudentId);
                if (ds.Tables[0].Rows.Count > 0)
                    ImgPhoto.ImageUrl = "../Student/" + ds.Tables[0].Rows[0]["Photo"].ToString();
                

                dgvFeesHead.DataSource = ds.Tables[1];
                dgvFeesHead.DataBind();

                Message.Show = false;
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Please Select Student";
                Message.Show = true;
            }
        }

        
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                string strValues = DateTime.Now.ToString("dd MMM yyyy");
                strValues += chr.ToString() + "";
                DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

                //if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() == Session["FinYrID"].ToString().Trim())
                //{                
                

                   //// btnPrint.Attributes.Add("onclick", "javascript:openPopup('MoneyReceipt.aspx?id=" + Fees.PaymentId + "&refund=0'); return false;");
                    if (Convert.ToDouble(txtAmount.Text) == Convert.ToDouble(txtTotalAmt.Text))
                    {
                        SaveVoucherDetails();
                        //-----------Add On 14-09-2013
                        txtReceiptNo.Text = "Auto Generated";
                        txtVoucherDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                        ltrLedgerBalance.Text = "";
                        txtNarration.Text = "";
                        ddlSemester.SelectedIndex = 0;
                        ltrDrCr.Text = "<b>CR</b>";
                        ddlCashBankLedger.SelectedIndex = 0;
                        ddlStudent.SelectedIndex = 0;
                        ImgPhoto.ImageUrl = "../Student/StudentPhoto/Male.jpg";
                        txtAmount.Text = string.Empty;
                        dgvFeesHead.DataSource = null;
                        dgvFeesHead.DataBind();
                        //----------------------------
                    }
                    else
                    { 
                         Message.IsSuccess = false;
                         Message.Text = "Total Debit and Credit Amount Not Matched";
                    }
                //}
                //else
                //{
                //    Message.IsSuccess = false;
                //    Message.Text = "Please select a Date Between " + Session["SesFromDate"].ToString() + " & " + Session["SesToDate"].ToString() + "";
                //}
            }
            Message.Show = true;
        }

        //private string PaymentDetails()
        //{
        //    DataTable DT = new DataTable();
        //    DT.Columns.Add("FeesHeadId", typeof(int));
        //    DT.Columns.Add("Amount", typeof(decimal));
        //    DataRow DR;

        //    foreach (GridViewRow DGV in dgvFeesHead.Rows)
        //    {
        //        if (DGV.RowType == DataControlRowType.DataRow)
        //        {
        //            TextBox txtAmount = (TextBox)DGV.FindControl("txtAmount");
        //            decimal Amount = (txtAmount.Text.Trim().Length > 0) ? Convert.ToDecimal(txtAmount.Text.Trim()) : 0;

        //            if (Amount > 0)
        //            {
        //                DR = DT.NewRow();
        //                DR["FeesHeadId"] = Convert.ToInt32(dgvFeesHead.DataKeys[DGV.RowIndex].Values["id"].ToString());
        //                DR["Amount"] = Amount;
        //                DT.Rows.Add(DR);
        //                DT.AcceptChanges();
        //            }
        //        }
        //    }

        //    using (DataSet ds = new DataSet())
        //    {
        //        ds.Tables.Add(DT);
        //        objStuJournal.PaymentDetailsXML = ds.GetXml().Replace("Table1>", "Table>");
        //    }
        //}
        private void SaveVoucherDetails()
        {
            string rtMsg = "";
            string Msg = "";

            string strSPName = "";
            string strValues = "";

            dsD = ((DataSet)Session["SOD"]);

            if (dgvFeesHead.Rows.Count==0)
            {
                Message.IsSuccess = false;
                Message.Text = "Please select any Student";
                Message.Show = true;

                return;
            }
            if (hdnJournalID.Value == "0")
            {
            strSPName = "usp_trnStudentJournal_Save";
            }
            else
            {
                strSPName = "usp_trnStudentJournal_Update";
            }
            
            strValues = hdnJournalID.Value.ToString().Trim();
            strValues += chr.ToString() + txtReceiptNo.Text.ToString().Trim(); 
            strValues += chr.ToString() + Session["CompanyId"].ToString().Trim();

            strValues += chr.ToString() + Session["FinYrID"].ToString().Trim();
            strValues += chr.ToString() + Session["BranchId"].ToString().Trim();
            strValues += chr.ToString() + Session["DataFlow"].ToString().Trim();

            strValues += chr.ToString() + txtVoucherDate.Text.ToString().Trim(); 
            strValues += chr.ToString() + "1";
            strValues += chr.ToString() + txtNarration.Text.ToString().Trim();
            strValues += chr.ToString() + Session["UserId"].ToString().Trim();
            strValues += chr.ToString() + int.Parse(ddlStudent.SelectedValue.ToString());
            strValues += chr.ToString() + txtTotalAmt.Text.ToString().Trim();
            strValues += chr.ToString() + int.Parse( ddlSemester.SelectedValue.ToString());
            strValues += chr.ToString() + PrepareXMLString();
            rtMsg = gf.ExecuteAnySPOutput(strSPName, strValues);
            Msg = rtMsg.Substring(0, 4);
            //strvchnoshow=rtMsg[1].ToString();
            if (Msg == "True")
            {
                hdnJournalID.Value = rtMsg.Substring(5, rtMsg.Length - 5);
                ViewState["ID"] = hdnJournalID.Value;
                BlankDataSet();
                PopulatePageControl(Convert.ToInt32(hdnJournalID.Value));

                Message.IsSuccess = true;
                Message.Text = "Your request has been processed successfully!";

                //AdjustButtons(true, true, true, false);
                //PopulateHeaderGrid(ViewState["sortColumn"].ToString(), ViewState["sortDirection"].ToString());

            }
            else
            {
                if (rtMsg == "Duplicate:")
                {
                    Message.IsSuccess = false;
                    Message.Text = "This Voucher No. is already exists!";

                }
                else if (rtMsg == "Blank:")
                {
                    Message.IsSuccess = false;
                    Message.Text = "Voucher No. can not be blank, please set the AutoCode master!";

                }
                else
                {

                }
                //AdjustButtons(true, true, false, false);
            }
            Message.Show = true;
        }

        private string PrepareXMLString()
        {
            string strXMLString = "";
            int SrlNo = 1;
            string ByTo = "RECEIVE";
            decimal DRAmount = 0;
            
            decimal CrAmount = 0;
           
            string LedgerType = string.Empty;
           
            if (ddlDrCr.SelectedItem.Text.ToString() == "DR")
           
           // { DRAmount = Convert.ToDecimal(txtAmount.Text.ToString()); CrAmount = 0; LedgerType = "To"; }
                { DRAmount = Convert.ToDecimal(txtAmount.Text.ToString()); CrAmount = 0; LedgerType = "By"; }
               //l
            else
            { DRAmount = 0; CrAmount = Convert.ToDecimal(txtAmount.Text.ToString()); LedgerType = "To"; }
            //{ DRAmount = 0; CrAmount = Convert.ToDecimal(txtAmount.Text.ToString()); LedgerType = "By"; }
            //u
            //---------------------------------
            int intJVDetailsId1;
            DataView dv1 = new DataView();
            DataSet ds1 = (DataSet)Session["SOD"];
            if (hdnJournalID.Value != "0")
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    //DataView dv = new DataView();
                    DataTable dt1 = ds1.Tables[0];
                    dv1 = dt1.DefaultView;
                    dv1.RowFilter = "[Serial No]='1'";
                    intJVDetailsId1 = int.Parse(dv1[0]["JVDetailID"].ToString());
                }
                else
                {
                    intJVDetailsId1 = 0;
                }
            }
            else
            {
                intJVDetailsId1 = 0;
            }
            //---------------------------------
            strXMLString = "<NewDataSet>";
            strXMLString += " <TrnJournalVoucherDetail";
            strXMLString += " JVHeaderID_FK = \"0\"";
            strXMLString += " JVDetailID = \"" + intJVDetailsId1 + "\"";
            strXMLString += " SrlNo = \"" + SrlNo  + "\"";
            strXMLString += " LedgerID = \"" +  ddlCashBankLedger.SelectedValue.ToString() + "\"";
            strXMLString += " ReferenceNo = \"0\"";
            strXMLString += " DRAmount = \"" + DRAmount + "\"";
            strXMLString += " CRAmount = \"" + CrAmount + "\"";
            strXMLString += " LedgerType = \"" + LedgerType + "\"";
            strXMLString += " CostCenterId = \"0\"";
            strXMLString += " />";
            foreach (GridViewRow DGV in dgvFeesHead.Rows)
            {
                if (DGV.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtAmountCr = (TextBox)DGV.FindControl("txtAmount");
                    decimal Amount = (txtAmountCr.Text.Trim().Length > 0) ? Convert.ToDecimal(txtAmountCr.Text.Trim()) : 0;
                    
                    if (Amount > 0)
                    {
                        SrlNo += 1;
                        decimal DrAmount1 = 0;
                        decimal CrAmount1 = 0;
                        string LedgerType1 = string.Empty;
                        if (ddlDrCr.SelectedItem.Text.ToString() == "DR")
                        { DrAmount1 = 0; CrAmount1 = Amount; LedgerType1 = "To"; }                        
                        //{ DrAmount1 = 0; CrAmount1 = Amount; LedgerType1 = "By"; }
                        //l
                        else
                        { DrAmount1 = Amount; CrAmount1 = 0; LedgerType1 = "By"; }
                        //{ DrAmount1 = Amount; CrAmount1 = 0; LedgerType1 = "To"; }
                        //u


                        //strXMLString += "<TrnCashBankVoucherDetail";
                        //strXMLString += " SrlNo = \"" + SrlNo.ToString() + "\"";
                        //strXMLString += " ByTo = \"" + ByTo + "\"";
                        //strXMLString += " LedgerID = \"" + dgvFeesHead.DataKeys[DGV.RowIndex].Values["AssestLedgerID_FK"].ToString() + "\"";
                        //strXMLString += " DRAmount = \"" + DRAmount + "\"";
                        //strXMLString += " CRAmount = \"" + Amount + "\"";
                        //strXMLString += " />";
                        int intJVDetailsId = 0;
                        DataView dv = new DataView();
                        DataSet ds = (DataSet)Session["SOD"];
                        if (hdnJournalID.Value != "0")
                        {
                            if (ds.Tables[0].Rows.Count >= SrlNo)
                            {
                                //DataView dv = new DataView();
                                DataTable dt = ds.Tables[0];
                                dv = dt.DefaultView;
                                dv.RowFilter = "[Serial No]='" + SrlNo + "'";
                                intJVDetailsId = int.Parse(dv[0]["JVDetailID"].ToString());
                            }
                            else
                            {
                                intJVDetailsId = 0;
                            }
                        }
                        else
                        {
                            intJVDetailsId = 0;
                        }
                        //DataTable dt = (DataTable)Session["SOD"];
                        //dv =(DataView) Session["SOD"];
                        ////dv = DataView(Session["SOD"]);
                        //dv.RowFilter=
                        //int JVDetailsId = 

                        strXMLString += " <TrnJournalVoucherDetail";
                        strXMLString += " JVHeaderID_FK = \"0\"";
                        strXMLString += " JVDetailID = \"" + intJVDetailsId + "\"";
                        strXMLString += " SrlNo = \"" + SrlNo + "\"";
                        strXMLString += " LedgerID = \"" + dgvFeesHead.DataKeys[DGV.RowIndex].Values["AssestLedgerID_FK"].ToString() + "\"";
                        strXMLString += " ReferenceNo = \"0\"";
                        strXMLString += " DRAmount = \"" + DrAmount1 + "\"";
                        strXMLString += " CRAmount = \"" + CrAmount1 + "\"";
                        strXMLString += " LedgerType = \"" + LedgerType1 + "\"";
                        strXMLString += " CostCenterId = \"0\"";
                        strXMLString += " />";
                    }
                }
            }

            strXMLString += "</NewDataSet>";
            return strXMLString;
        }

        protected bool Validate()
        {
            bool result = false;
            string ErrorText = "";
            string strValues = txtVoucherDate.Text.Trim();
            strValues += chr.ToString() + "";
            DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

            if (ddlCashBankLedger.SelectedValue == "0" || ddlCashBankLedger.Text == string.Empty)
            {
                ErrorText = "Please Select Cash/Bank Ledger.";
                result = false;
            }
            else if (ddlStudent.SelectedValue == "0" || ddlStudent.Text == string.Empty)
            {
                ErrorText = "Please Select Student.";
                result = false;
            }
            //else if (ddlReceiptMode.SelectedValue == "CHEQUE")
            //{
            //    if (txtChequeNo.Text.Trim().Length == 0 || txtChequeDate.Text.Trim().Length == 0)
            //    {
            //        result = false;
            //        ErrorText = "You Must Provide Cheque No and Cheque Date When Payment Mode Is Cheque.";
            //    }
            //    else { result = true; }
            //}
            else if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() != Session["FinYrID"].ToString().Trim())
            {
                result = false;
                ErrorText = "Sorry! Voucher Date is not Within Current Financial Year. Please Check.";
            }
            else if (Convert.ToDateTime(txtVoucherDate.Text.Trim()) > DateTime.Now)
            {
                result = false;
                ErrorText = "Sorry! Future Voucher Date is Not Allowed.";
            }
            //else if (Convert.ToDecimal(txtTotalAmt.Text.Trim()) > Convert.ToDecimal(ltrLedgerBalance.te )
            //{
            //    result = false;
            //    ErrorText = "Sorry! Future Voucher Date is Not Allowed.";
            //}
            else { result = true; }

            if (!result)
            {
                Message.IsSuccess = false;
                Message.Text = ErrorText;
            }
            return result;
        }

        
        protected void ddlReceiptMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlReceiptMode.SelectedValue == "CASH")
            //{
            //    txtChequeNo.Text = "";
            //    txtDrawnOn.Text = "";
            //    txtChequeDate.Text = "";

            //    txtChequeNo.Enabled = false;
            //    txtDrawnOn.Enabled = false;
            //    txtChequeDate.Enabled = false;
            //}
            //else if (ddlReceiptMode.SelectedValue == "CHEQUE")
            //{
            //    txtChequeDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            //    txtChequeNo.Enabled = true;
            //    txtDrawnOn.Enabled = true;
            //    txtChequeDate.Enabled = true;
            //}
        }

        protected void ddlCashBankLedger_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLedgerBalance();
        }

        protected void GetLedgerBalance()
        {
            ltrLedgerBalance.Text = "";

            //strParams = Session["CompanyId"].ToString() + chr.ToString();
            //strParams += Session["FinYrID"].ToString() + chr.ToString();
            //strParams += Session["BranchId"].ToString() + chr.ToString();
            //strParams += ddlCashBankLedger.SelectedValue.ToString() + chr.ToString();
            //strParams += Session["DataFlow"].ToString() + chr.ToString();

            //DataSet ds = gf.ExecuteSelectSP("[spSelect_MstGeneralLedgerRefDetails]", strParams);
            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchId"].ToString() + chr.ToString();
            strParams += ddlCashBankLedger.SelectedValue.ToString() + chr.ToString();
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

        protected void dgvFeesHead_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                ((TextBox)e.Row.FindControl("txtAmount")).Attributes.Add("onkeydown", "javascript:moveEnter(" + (e.Row.RowIndex + 1) + ");");
               
                
            }
                 
        }

        protected void grdvwtrnsctnsearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton ib = new ImageButton();
                ib = (ImageButton)e.Row.FindControl("imgbtneditsearch");
                ib.Attributes["onClick"] = "changeTab();";

                ((ImageButton)e.Row.FindControl("imgbtndeletesearch")).Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.DELETE_VOUCHER);
                ImageButton ibPrint = new ImageButton();
                ibPrint = (ImageButton)e.Row.FindControl("btnPrint");
                ibPrint.CommandArgument = e.Row.RowIndex.ToString();
                ibPrint.Attributes.Add("onclick", "javascript:openPopup('PrintJournalVoucher.aspx?id=" + ((DataRowView)e.Row.DataItem)["JVHeaderID"].ToString() + "'); return false;");
            }
        }
        protected void grdvwtrnsctnsearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            string strMessage = "";

            if (e.CommandName.ToString() == "imgbtneditsearch")
            {
                hdnJournalID.Value = e.CommandArgument.ToString();
                ViewState["ID"] = hdnJournalID.Value;
                BlankDataSet();
                PopulatePageControl(Convert.ToInt32(hdnJournalID.Value));
                btnSave.Enabled = true;
            }
            else if (e.CommandName.ToString() == "imgbtndeletesearch")
            {
                gf.ExecuteAnySPOutput("spDelete_TrnJournalVoucherStudent", e.CommandArgument.ToString());
                PopulateHeaderGrid(ViewState["sortColumn"].ToString(), ViewState["sortDirection"].ToString());
            }

        }
        private void PopulateHeaderGrid(string strSortExpression, string sortDir)
        {
            //For Search
            strParams = Session["CompanyId"].ToString().Trim();
            strParams += chr.ToString() + Session["FinYrID"].ToString().Trim();
            strParams += chr.ToString() + Session["BranchId"].ToString().Trim();
            strParams += chr.ToString() + Session["DataFlow"].ToString().Trim();
            string strHParams = strParams + chr.ToString() + "";
            //strHParams += "";
            gf.BindGridViewSP(grdvwtrnsctnsearch, "spSelect_TrnVoucherHeaderDetailsStudent", strHParams, PrepareSearchFilter(), strSortExpression, sortDir);

        }
        private string PrepareSearchFilter()
        {
            string strFilterString = "";
            if (txtVchNoSearch.Text.ToString() != "")
            {
                strFilterString = "JVoucherNo = '" + txtVchNoSearch.Text.ToString() + "'";
            }
            if (txtVchDateSearch.Text.Trim() != "")
            {
                if (strFilterString == "")
                    strFilterString = "JVoucherDate >= '" + Convert.ToDateTime(txtVchDateSearch.Text.Trim()) + "'";
                //strFilterString = "JVoucherDate >= '" + txtVchDateSearch.Text.ToString(),System.Configuration.ConfigurationManager.AppSettings["DateFormat"] + "'";
                else
                    strFilterString += " AND JVoucherDate >= '" + Convert.ToDateTime(txtVchDateSearch.Text.Trim()) + "'";
            }
            if (txtVchDateSearchTo.Text.Trim() != "")
            {
                if (strFilterString == "")
                    strFilterString = "JVoucherDate <= '" + Convert.ToDateTime(txtVchDateSearchTo.Text.Trim()) + "'";
                else
                    strFilterString += " AND JVoucherDate <= '" + Convert.ToDateTime(txtVchDateSearchTo.Text.Trim()) + "'";
            }

            return strFilterString ;
        }
        private void PopulateControlUpdate()
        {

            dsD = new DataSet();
            dsD = (DataSet)Session["SOD"];

            if (dsD.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToDouble(dsD.Tables[0].Rows[RowIndex]["Amount Dr"].ToString()) == 0)
                    //ddlDrCr.SelectedValue = "Cr";
                    ddlDrCr.SelectedValue = "Dr";
                else
                    //ddlDrCr.SelectedValue = "Dr";
                    ddlDrCr.SelectedValue = "Cr";
               // SetCRDRTextBox(ddlbyto.SelectedValue);
               ddlCashBankLedger.SelectedValue = dsD.Tables[0].Rows[RowIndex]["Ledger ID"].ToString();
               GetLedgerBalance();
                //txtbalance.Text = dsD.Tables[0].Rows[i]["Closing Balance"].ToString();
                //txttype.Text = dsD.Tables[0].Rows[i]["Type"].ToString();
                // hdnCCApp.Value = dsD.Tables[0].Rows[0]["Cost Center"].ToString();
                hdndtlid.Value = dsD.Tables[0].Rows[RowIndex]["JVDetailID"].ToString();
                //if (hdnCCApp.Value == "True")
                //GetCostCenterDetails();
                //txtrfno.Text = dsD.Tables[0].Rows[RowIndex]["Reference No"].ToString();
                //txtamnrdr.Text = dsD.Tables[0].Rows[RowIndex]["Amount Dr"].ToString();
                //txtamnrcr.Text = dsD.Tables[0].Rows[RowIndex]["Amount Cr"].ToString();
                //ddlcostcntr.SelectedValue = dsD.Tables[0].Rows[RowIndex]["CostCenterId"].ToString();
                //Print();
                //AdjustButtons(true, true, true, true);
            }
            else
            {

               // AdjustButtons(true, true, false, false);
            }
            Message.Show = false;

        }
        protected void btnSearchJV_Click(object sender, EventArgs e)
        {
            PopulateHeaderGrid(ViewState["sortColumn"].ToString(), ViewState["sortDirection"].ToString());
        }
        private void PopulatePageControl(int intID)
        {
            //Populate Controls For Edit
            string strValues = "";
            //RowIndex = -1;
            //txtblncdr.Text = "";
            //txtblnccr.Text = "";
            strValues = Session["CompanyId"].ToString().Trim();
            strValues += chr.ToString() + Session["FinYrID"].ToString().Trim();
            strValues += chr.ToString() + Session["BranchId"].ToString().Trim();
            strValues += chr.ToString() + Session["DataFlow"].ToString().Trim();
            strValues += chr.ToString() + intID;
            DataSet dsOH = gf.ExecuteSelectSP("spSelect_TrnVoucherHeaderPopulateStudent", strValues);

            if (dsOH.Tables[0].Rows.Count > 0)
            {
                txtReceiptNo.Text = dsOH.Tables[0].Rows[0]["JVoucherNo"].ToString();
                txtVoucherDate.Text = ((DateTime)dsOH.Tables[0].Rows[0]["JVoucherDate"]).ToString("dd MMM yyyy");
                txtNarration.Text = dsOH.Tables[0].Rows[0]["JVNarration"].ToString();
                ddlStudent.SelectedValue = dsOH.Tables[0].Rows[0]["StudentId"].ToString();
                if (Convert.ToDouble(dsOH.Tables[0].Rows[0]["AmountDr"]) == 0)
                {
                    //ddlDrCr.Text = "CR";
                    txtAmount.Text = dsOH.Tables[0].Rows[0]["AmountCr"].ToString();
                }
                else
                {
                    //ddlDrCr.Text = "DR";
                    txtAmount.Text = dsOH.Tables[0].Rows[0]["AmountDr"].ToString();

                }
               txtTotalAmt.Text = txtAmount.Text;
               ddlSemester.SelectedValue = dsOH.Tables[0].Rows[0]["SemNo"].ToString();
                //---------------
                BusinessLayer.Accounts.StudentFeesCollection ObjFees = new BusinessLayer.Accounts.StudentFeesCollection();
                int StudentId = Convert.ToInt32(dsOH.Tables[0].Rows[0]["StudentId"].ToString());

                DataSet ds = ObjFees.GetStudentUnpaidTransJV(StudentId);
                if (ds.Tables[0].Rows.Count > 0)
                    ImgPhoto.ImageUrl = "../Student/" + ds.Tables[0].Rows[0]["Photo"].ToString();


                dgvFeesHead.DataSource = ds.Tables[1];
                dgvFeesHead.DataBind();

                Message.Show = false;
                //---------------
                dsD = new DataSet();
                dsD = (DataSet)Session["SOD"];

                DataSet dsDT = gf.ExecuteSelectSP("spSelect_TrnVoucherDetailsPopulate", strValues);

                //Double dblamntdr=Double.Parse(dsOH.Tables[0].Rows[0]["DRAmount"].ToString());
                //Double dblamntcr = Double.Parse(dsOH.Tables[0].Rows[0]["CRAmount"].ToString());
                if (dsDT.Tables[0].Rows.Count > 0)
                {
                    //AdjustButtons(true, true, true, true);
                    for (int i = 0; i <= dsDT.Tables[0].Rows.Count - 1; i++)
                    {
                        
                        //--------------------------------------
                        if (int.Parse(dsDT.Tables[0].Rows[i]["SrlNo"].ToString()) == 1)
                        {
                            ddlCashBankLedger.SelectedValue = dsDT.Tables[0].Rows[i]["LedgerID"].ToString();
                            GetLedgerBalance();

                            if (Convert.ToDouble(dsDT.Tables[0].Rows[i]["DRAmount"]) == 0)
                            {
                                ddlDrCr.Text = "CR";//l
                                ltrDrCr.Text = "DR";//l
                            }
                            else
                            {
                                
                                ddlDrCr.Text = "DR";//u
                                ltrDrCr.Text = "CR";//u
                            }
                        }
                        else
                        {
                           //----------------
                           
                            //----------------
                            foreach (GridViewRow DGV in dgvFeesHead.Rows)
                            {
                                if (DGV.RowType == DataControlRowType.DataRow)
                                {
                                    //TextBox txtAmount = (TextBox)DGV.FindControl("txtAmount");
                                    //decimal Amount = (txtAmount.Text.Trim().Length > 0) ? Convert.ToDecimal(txtAmount.Text.Trim()) : 0;

                                    //if (Amount > 0)
                                    //{
                                    //    DR = DT.NewRow();
                                    //    DR["FeesHeadId"] = Convert.ToInt32(dgvFeesHead.DataKeys[DGV.RowIndex].Values["id"].ToString());
                                    //    DR["Amount"] = Amount;
                                    //    DT.Rows.Add(DR);
                                    //    DT.AcceptChanges();
                                    //}
                                    if (Convert.ToInt32(dgvFeesHead.DataKeys[DGV.RowIndex].Values["AssestLedgerID_FK"].ToString()) == Convert.ToInt32(dsDT.Tables[0].Rows[i]["LedgerID"].ToString()))
                                    {
                                        TextBox txtAmountgv = (TextBox)DGV.FindControl("txtAmount");
                                        if (Convert.ToDouble(dsDT.Tables[0].Rows[i]["DRAmount"]) == 0)
                                        {
                                            txtAmountgv.Text = dsDT.Tables[0].Rows[i]["CRAmount"].ToString();
                                        }
                                        else
                                        {
                                            txtAmountgv.Text = dsDT.Tables[0].Rows[i]["DRAmount"].ToString();
                                        }
                                    }
                                }
                            }
                            //----------------
                        
                        }

                        //--------------------------------------
                        
                        
                        
                        dsD.Tables[0].Rows.Add();
                        dsD.Tables[0].Rows[i]["JVHeaderID_FK"] = dsDT.Tables[0].Rows[i]["JVHeaderID_FK"];
                        dsD.Tables[0].Rows[i]["JVDetailID"] = dsDT.Tables[0].Rows[i]["JVDetailID"];
                        dsD.Tables[0].Rows[i]["Serial No"] = dsDT.Tables[0].Rows[i]["SrlNo"];
                        if (Convert.ToDouble(dsDT.Tables[0].Rows[i]["DRAmount"]) == 0)
                        {
                            dsD.Tables[0].Rows[i]["BY/To"] = "To";
                        }
                        else
                        {
                            dsD.Tables[0].Rows[i]["BY/To"] = "By";
                        }
                        dsD.Tables[0].Rows[i]["Ledger Name"] = dsDT.Tables[0].Rows[i]["LedgerName"];
                        //dsD.Tables[0].Rows[i]["Closing Balance"] = dsDT.Tables[0].Rows[i]["ClosingBalance"];
                        //dsD.Tables[0].Rows[i]["Type"] = dsDT.Tables[0].Rows[i]["LedgerType"];
                        dsD.Tables[0].Rows[i]["Cost Center"] = dsDT.Tables[0].Rows[i]["CostCenterName"];
                        dsD.Tables[0].Rows[i]["Reference No"] = dsDT.Tables[0].Rows[i]["ReferenceNo"];
                        dsD.Tables[0].Rows[i]["Amount DR"] = dsDT.Tables[0].Rows[i]["DRAmount"];
                        dsD.Tables[0].Rows[i]["Amount CR"] = dsDT.Tables[0].Rows[i]["CRAmount"];
                        dsD.Tables[0].Rows[i]["LedgerID"] = dsDT.Tables[0].Rows[i]["LedgerID"];
                        dsD.Tables[0].Rows[i]["CostCenterId"] = dsDT.Tables[0].Rows[i]["CostCenterId"];
                    }
                   // CalculateCRDRTotal(dsD);
                }
                //grdvwtrnsctn.DataSource = dsD.Tables[0].DefaultView;
                //grdvwtrnsctn.DataBind();
                Session["SOD"] = dsD;
                btnSave.Text = "Update";
                btnPrint.Attributes.Add("onclick", "javascript:openPopup('PrintJournalVoucher.aspx?id=" + intID + "'); return false;");
            }
            else
            {
                //btnSave.Text = "Save";
                //AdjustButtons(true, true, false, false);
            }
            Message.Show = false;

        }
        protected void ddlDrCr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDrCr.SelectedValue == "DR")
                ltrDrCr.Text = "<b>CR</b>";
            else if (ddlDrCr.SelectedValue == "CR")
                ltrDrCr.Text = "<b>DR</b>";
        }

        protected void dgvFeesHead_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void grdvwtrnsctn_RowEditing(object sender, GridViewEditEventArgs e)
        {
            RowIndex = e.NewEditIndex;
            PopulateControlUpdate();

        }

        protected void LoadBatch()
        {
            BusinessLayer.Student.BTechRegistration BtechReg = new BusinessLayer.Student.BTechRegistration();
            Entity.Student.BTechRegistration eBtechReg = new Entity.Student.BTechRegistration();
            eBtechReg.intMode = 1;
            eBtechReg.CourseId = 0;
            DataTable dt = new DataTable();
            dt = BtechReg.GetAllCommonSP(eBtechReg);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ddlBatch.DataSource = dt;
                    ddlBatch.DataBind();
                }
            }
        }
        protected void LoadCourse()
        {
            BusinessLayer.Student.BTechRegistration ObjRegistration = new BusinessLayer.Student.BTechRegistration();
            Entity.Student.BTechRegistration Registration = new Entity.Student.BTechRegistration();
            Registration.intMode = 2;
            Registration.CourseId = 0; // Course Id is not required to fetch all courses
            DataTable dt = ObjRegistration.GetAllCommonSP(Registration);
            if (dt != null)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "CompanyId = " + Session["CompanyId"].ToString() + " or CompanyId = 0";
                ddlCourse.DataSource = dv;
                ddlCourse.DataBind();
            }
            LoadStream();
        }
        protected void LoadStream()
        {
            int CourseId = int.Parse(ddlCourse.SelectedValue.Trim());
            BusinessLayer.Student.BTechRegistration ObjRegistration = new BusinessLayer.Student.BTechRegistration();
            Entity.Student.BTechRegistration Registration = new Entity.Student.BTechRegistration();
            Registration.intMode = 3;
            Registration.CourseId = CourseId;
            DataTable dt = ObjRegistration.GetAllCommonSP(Registration);
            if (dt != null)
            {
                ddlStream.DataSource = dt;
                ddlStream.DataBind();
            }
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadApprovedStudent();
            BusinessLayer.Student.LibraryFine ObjFine = new BusinessLayer.Student.LibraryFine();
            DataView dv = new DataView(ObjFine.GetApprovedStudentList());
            dv.RowFilter = "batch_id=" + int.Parse(ddlBatch.SelectedValue.Trim()) + "AND " + "company_id=" + Convert.ToInt32(Session["CompanyId"].ToString());

            if (dv != null)
            {
                ddlStudent.DataSource = dv;
                ddlStudent.DataBind();
            }
            ddlStudent.Items.Insert(0, liS);
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStream();
        }

        protected void ddlStream_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadApprovedStudent();
            BusinessLayer.Student.LibraryFine ObjFine = new BusinessLayer.Student.LibraryFine();
            DataView dv = new DataView(ObjFine.GetApprovedStudentList());
            dv.RowFilter = "batch_id=" + int.Parse(ddlBatch.SelectedValue.Trim()) + "AND " + "company_id=" + Convert.ToInt32(Session["CompanyId"].ToString()) + "AND " + "CourseId=" + int.Parse(ddlCourse.SelectedValue.Trim()) + "AND " + "StreamId=" + int.Parse(ddlStream.SelectedValue.Trim());

            if (dv != null)
            {
                ddlStudent.DataSource = dv;
                ddlStudent.DataBind();
            }
            ddlStudent.Items.Insert(0, liS);
        }

        protected void ddlStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["State"] = 0;
        }

        protected void ddlStudent_SelectedIndexChanged1(object sender, EventArgs e)
        {
            DataAccess.Student.StudentDropout ObjDropOut = new DataAccess.Student.StudentDropout();
            int StudentId = Convert.ToInt32(ddlStudent.SelectedValue);
            DataTable dt = new DataTable();
            int Count = 0;
            dt = ObjDropOut.FindIsStudentDropout(StudentId);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Count = Convert.ToInt32(dt.Rows[0]["COUNT"].ToString());
                }
            }
            if (Count > 0)
            {
                lblDropOut.Visible = true;
                lblDropOut.Text = "DropOut";
            }
            else
            {
                lblDropOut.Visible = false;
                lblDropOut.Text = "";
            }
        }
    }
}
