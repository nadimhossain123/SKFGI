using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace CollegeERP.Accounts
{
    public partial class MoneyReceiptMgmnt : System.Web.UI.Page
    {
        public int PaymentId;
        public int Refund;
        public int PrintRefund;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim().Length > 0)
                    PaymentId = Convert.ToInt32(Request.QueryString["id"].Trim());

                if (Request.QueryString["refund"] != null && Request.QueryString["refund"].Trim().Length > 0)
                    Refund = Convert.ToInt32(Request.QueryString["refund"].Trim());

                if (Request.QueryString["PrintRefund"] != null && Request.QueryString["PrintRefund"].Trim().Length > 0)
                    PrintRefund = Convert.ToInt32(Request.QueryString["PrintRefund"].Trim());

                if (PrintRefund == 0)
                {
                    LoadReceiptDetails();
                }
                else
                {
                    LoadRefundReceiptDetails();
                }
            }
        }

        protected void LoadReceiptDetails()
        {
            BusinessLayer.Accounts.StudentFeesCollection ObjFees = new BusinessLayer.Accounts.StudentFeesCollection();
            DataSet ds = ObjFees.GetMoneyReceipt(PaymentId);
            LoadBasicInformation(ds.Tables[0]);
            dgvBill.DataSource = ds.Tables[1];
            dgvBill.DataBind();

            ((Literal)dgvBill.FooterRow.FindControl("ltrTotalAmt")).Text = "<b>" + ds.Tables[0].Rows[0]["Amount"].ToString() + "</b>";
        }
        protected void LoadRefundReceiptDetails()
        {
            BusinessLayer.Accounts.StudentFeesCollection ObjFees = new BusinessLayer.Accounts.StudentFeesCollection();
            DataSet ds = ObjFees.GetRefundMoneyReceipt(PaymentId);
            LoadBasicInformation(ds.Tables[0]);
            dgvBill.DataSource = ds.Tables[1];
            dgvBill.DataBind();

            ((Literal)dgvBill.FooterRow.FindControl("ltrTotalAmt")).Text = "<b>" + ds.Tables[0].Rows[0]["Amount"].ToString() + "</b>";
        }

        protected void LoadBasicInformation(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                if (Refund == 1)
                {
                    ltrHeader.Text = "Advance Refund Receipt";
                }
                ltrReceiptNo.Text = "<b>Sl. No. </b>" + dt.Rows[0]["MoneyReceiptNo"].ToString();
                ltrPaymentDate.Text = "<b>Date </b>" + Convert.ToDateTime(dt.Rows[0]["PaymentDate"].ToString()).ToString("dd/MM/yyyy");
                ltrName.Text = "<b>Name: </b>" + dt.Rows[0]["name"].ToString();
                ltrStudentCode.Text = "<b>Student ID: </b>" + dt.Rows[0]["student_code"].ToString();
                ltrOther.Text = "<b>Year: </b>" + dt.Rows[0]["batch_name"].ToString() + "   <b>Course: </b>" + dt.Rows[0]["CourseName"].ToString() + "   <b>Stream: </b>" + dt.Rows[0]["stream_name"].ToString() + " <b>Semester: </b>" + dt.Rows[0]["SemNo"].ToString();
                ltrAmtInWord.Text = "<b>Rupees (in words) </b>" + dt.Rows[0]["AmountInWords"].ToString();

                string ReceiptMode = dt.Rows[0]["PaymentMode"].ToString();
                ltrReceiptMode.Text = (Refund == 0) ? "<b>Received : </b>" : "<b>Paid : </b>";
                switch (ReceiptMode)
                {
                    case "CASH": ltrReceiptMode.Text += "Cash/<strike>Cheque</strike>&nbsp;<b>No.</b> "; break;
                    case "CHEQUE": ltrReceiptMode.Text += "<strike>Cash</strike>/Cheque&nbsp;<b>No.</b> "; break;
                    
                }
                ltrReceiptMode.Text += dt.Rows[0]["ChequeNo"].ToString() + "       <b>Date: </b>";
                if (dt.Rows[0]["ChequeDate"].ToString() != "")
                {
                    ltrReceiptMode.Text += Convert.ToDateTime(dt.Rows[0]["ChequeDate"].ToString()).ToString("dd MM yyyy");
                }
                ltrBankName.Text = "<b>Drawn on: </b>" + dt.Rows[0]["BankName"].ToString();
                ltrNarration.Text="<b>Narration: </b>" + dt.Rows[0]["Narration"].ToString();

                //if (Session["CompanyId"].ToString() == "1")
                //    ltrFooter.Text = "Please pay Cheque/DD in favour of SUPREME KNOWLEDGE FOUNDATION GROUP OF INSTITUTIONS. (SKFGI-MGMT)" ;
                //else
                //    ltrFooter.Text = "Please pay Cheque/DD in favour of SUPREME KNOWLEDGE FOUNDATION GROUP OF INSTITUTIONS. (SKFGI-ENGG)";

                //ltrSignature.Text =((Refund == 0) ? "Received by " : "Paid by ") + dt.Rows[0]["ReceivedBy"].ToString().ToUpper();
                if (Refund == 0)//---------Add On 10-08-2013
                {
                    if (Session["CompanyId"].ToString() == "1")
                        ltrFooter.Text = "Please pay Cheque/DD in favour of SUPREME KNOWLEDGE FOUNDATION GROUP OF INSTITUTIONS. (SKFGI-MGMT)";
                    else
                        ltrFooter.Text = "Please pay Cheque/DD in favour of SUPREME KNOWLEDGE FOUNDATION GROUP OF INSTITUTIONS. (SKFGI-ENGG)";
                    ltrPaidBy.Text = "";

                    ltrSignature.Text = ((Refund == 0) ? "Received by " : "Paid by ") + dt.Rows[0]["ReceivedBy"].ToString().ToUpper();

                }
                else
                {
                    lblText.Text = "";
                    ltrPaidBy.Text = ((Refund == 0) ? "Received by " : "Paid by ") + dt.Rows[0]["ReceivedBy"].ToString().ToUpper();
                    ltrSignature.Text = "Received By";
                }
            }
        }
    }
}
