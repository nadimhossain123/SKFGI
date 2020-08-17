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
    public partial class ExpenseReimbursement : System.Web.UI.Page
    {
        decimal TotAmt = 0;
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        string strParams = "";
        ListItem li = new ListItem("Select", "0");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.EXPENSE_REIMBURSEMENT))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                ClearControls();
                LoadLedger();
                LoadPendingClaimList();
            }
        }

        protected void ClearControls()
        {
            Message.Show = false;
            txtPaymentDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            txtChequeNo.Text = "";
            txtDrawnOn.Text = "";
            txtChequeDate.Text = "";
            ltrLedgerBalance.Text = "";
            ddlPaymentMode.SelectedIndex = 0;

            txtChequeNo.Enabled = false;
            txtDrawnOn.Enabled = false;
            txtChequeDate.Enabled = false;

            btnPrint.Attributes.Add("onclick", "javascript:alert('No Voucher To Print'); return false;");
        }

        protected void LoadLedger()
        {
            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchId"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();
            DataTable dt = gf.GetDropDownColumnsBySP("spSelect_MstGeneralLedgerBNKandCASH", strParams);

            if (dt != null)
            {
                ddlCashBankLedger.DataSource = dt;
                ddlCashBankLedger.DataBind();
            }
            ddlCashBankLedger.Items.Insert(0, li);

            DataView dv = new DataView(gf.GetDropDownColumnsBySP("spSelect_MstGeneralLedgerFull", strParams));
            dv.RowFilter = "LedgerType IN ('REIM')";
            
            if (dv != null)
            {
                ddlClaimLedger.DataSource = dv;
                ddlClaimLedger.DataBind();
            }
            ddlClaimLedger.Items.Insert(0, li);

        }

        protected void LoadPendingClaimList()
        {
            TotAmt = 0;
            BusinessLayer.HR.ExpenseClaim ObjClaim = new BusinessLayer.HR.ExpenseClaim();
            DataView dv = new DataView(ObjClaim.GetPendingClaim());
            dv.RowFilter = "CompanyId=" + Convert.ToInt32(Session["CompanyId"].ToString());

            dgvClaim.DataSource = dv.ToTable();
            dgvClaim.DataBind();

            txtTotalAmount.Text = TotAmt.ToString("#0.00");
        }

        protected void dgvClaim_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TotAmt += decimal.Parse(((TextBox)e.Row.FindControl("txtAmount")).Text);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                string strValues = DateTime.Now.ToString("dd MMM yyyy");
                strValues += chr.ToString() + "";
                DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

                if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() == Session["FinYrID"].ToString().Trim())
                {
                    BusinessLayer.HR.ExpenseClaim ObjClaim = new BusinessLayer.HR.ExpenseClaim();
                    Entity.HR.ExpenseClaim Claim = new Entity.HR.ExpenseClaim();
                    Claim.CashBankLedgerID = int.Parse(ddlCashBankLedger.SelectedValue.Trim());
                    Claim.PaymentDate = Convert.ToDateTime(txtPaymentDate.Text.Trim());
                    Claim.TransactionType = "PAYMENT";
                    Claim.ModeOfPayment = ddlPaymentMode.SelectedValue.Trim();
                    Claim.ChequeNo = txtChequeNo.Text.Trim();
                    if (txtChequeDate.Text.Trim().Length == 0)
                        Claim.ChequeDate = null;
                    else
                        Claim.ChequeDate = Convert.ToDateTime(txtChequeDate.Text.Trim());
                    
                    Claim.DrawnOn = txtDrawnOn.Text.Trim();

                    Claim.CompanyId = int.Parse(Session["CompanyId"].ToString());
                    Claim.BranchId = int.Parse(Session["BranchId"].ToString());
                    Claim.FinYrId = int.Parse(Session["FinYrID"].ToString());
                    Claim.TotalAmount = decimal.Parse(txtTotalAmount.Text.Trim());

                    DataTable dt = new DataTable();
                    dt.Columns.Add("ExpenseClaimId", typeof(int));
                    DataRow dr;
                    foreach (GridViewRow DGV in dgvClaim.Rows)
                    {
                        if (DGV.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox Chk = (CheckBox)DGV.FindControl("ChkSelect");
                            if (Chk.Checked == true)
                            {
                                dr = dt.NewRow();
                                dr["ExpenseClaimId"] = int.Parse(dgvClaim.DataKeys[DGV.RowIndex].Value.ToString());
                                dt.Rows.Add(dr);
                                dt.AcceptChanges();
                            }
                        }
                    }
                    using (DataSet ds = new DataSet())
                    {
                        ds.Tables.Add(dt);
                        Claim.ClaimDetails = ds.GetXml();
                    }

                    Claim.XMLCashBankVoucherDetails = PrepareXMLString();
                    Claim.CreatedBy = int.Parse(HttpContext.Current.User.Identity.Name);
                    Claim.CBVHeaderID = 0;

                    ObjClaim.SaveClaimReimbursement(Claim);
                    Message.IsSuccess = true;
                    Message.Text = "Claim Payment Saved Successfully";
                    LoadPendingClaimList();
                    GetLedgerBalance();
                    btnPrint.Attributes.Add("onclick", "javascript:window.open('RPTGridView.aspx?ID=" + Claim.CBVHeaderID + "'); return false;");
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Please select a Date Between " + Session["SesFromDate"].ToString() + " & " + Session["SesToDate"].ToString() + "";
                }
            }

            Message.Show = true;
        }

        private string PrepareXMLString()
        {
            string strXMLString = "";
            int SrlNo = 1;
            string ByTo = "RECEIVE";
            decimal CRAmount = 0;

            strXMLString = "<NewDataSet>";

            strXMLString += "<TrnCashBankVoucherDetail";
            strXMLString += " SrlNo = \"" + SrlNo.ToString() + "\"";
            strXMLString += " ByTo = \"" + ByTo + "\"";
            strXMLString += " LedgerID = \"" + ddlClaimLedger.SelectedValue.Trim() + "\"";
            strXMLString += " DRAmount = \"" + txtTotalAmount.Text + "\"";
            strXMLString += " CRAmount = \"" + CRAmount + "\"";
            strXMLString += " />";
            SrlNo += 1;

            strXMLString += "</NewDataSet>";
            return strXMLString;
        }

        protected bool Validate()
        {
            bool result;
            string ErrorText = "";
            string strValues = txtPaymentDate.Text.Trim();
            strValues += chr.ToString() + "";
            DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

            if (ddlCashBankLedger.SelectedValue == "0" || ddlCashBankLedger.Text == string.Empty)
            {
                result = false;
                ErrorText = "Please Select Cash/Bank Book";
            }
            else if (ddlClaimLedger.SelectedValue == "0" || ddlClaimLedger.Text == string.Empty)
            {
                result = false;
                ErrorText = "Please Select Reimbursement Ledger";
            }
            else if (ddlPaymentMode.SelectedValue == "CHEQUE")
            {
                if (txtChequeNo.Text.Trim().Length == 0 || txtChequeDate.Text.Trim().Length == 0)
                {
                    result = false;
                    ErrorText = "You Must Provide Cheque No and Cheque Date When Payment Mode Is Cheque.";
                }
                else { result = true; }
            }
            else if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() != Session["FinYrID"].ToString().Trim())
            {
                result = false;
                ErrorText = "Sorry! Payment Date is not Within Current Financial Year. Please Check.";
            }
            else if (Convert.ToDateTime(txtPaymentDate.Text.Trim()) > DateTime.Now)
            {
                result = false;
                ErrorText = "Sorry! Future Payment Date is Not Allowed.";
            }
            else { result = true; }

            if (!result)
            {
                Message.IsSuccess = false;
                Message.Text = ErrorText;
            }
            return result;
        }

        protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPaymentMode.SelectedValue == "CASH")
            {
                txtChequeNo.Text = "";
                txtDrawnOn.Text = "";
                txtChequeDate.Text = "";

                txtChequeNo.Enabled = false;
                txtDrawnOn.Enabled = false;
                txtChequeDate.Enabled = false;
            }
            else if (ddlPaymentMode.SelectedValue == "CHEQUE")
            {
                txtChequeDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                txtChequeNo.Enabled = true;
                txtDrawnOn.Enabled = true;
                txtChequeDate.Enabled = true;
            }
        }

        protected void ddlCashBankLedger_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLedgerBalance();
        }

        protected void GetLedgerBalance()
        {
            ltrLedgerBalance.Text = "";

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
    }
}
