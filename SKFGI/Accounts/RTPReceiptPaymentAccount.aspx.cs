using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CollegeERP.Accounts
{
    public partial class RTPReceiptPaymentAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Message.Show = false;

                txtFromDate.Text = Session["SesFromDate"].ToString();
                txtToDate.Text = Session["SesToDate"].ToString();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BusinessLayer.Accounts.RPTReceiptPaymentAccount objRPTReceiptPaymentAccount = new BusinessLayer.Accounts.RPTReceiptPaymentAccount();
            Entity.Accounts.RPTReceiptPaymentAccount rptReceiptPaymentAccount = new Entity.Accounts.RPTReceiptPaymentAccount();

            rptReceiptPaymentAccount.CompanyId = int.Parse(Session["CompanyID"].ToString());
            rptReceiptPaymentAccount.BranchId = int.Parse(Session["BranchID"].ToString());
            rptReceiptPaymentAccount.FinYearId = int.Parse(Session["FinYrID"].ToString());
            rptReceiptPaymentAccount.FromDate = Convert.ToDateTime(txtFromDate.Text);
            rptReceiptPaymentAccount.ToDate = Convert.ToDateTime(txtToDate.Text);

            DataTable dt = objRPTReceiptPaymentAccount.RPTReceiptPaymentAccount_Report(rptReceiptPaymentAccount);
            gvReceiptPaymentAccount.DataSource = dt;
            gvReceiptPaymentAccount.DataBind();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string Title = "Receipt Payment Account Report";
            string[] _header = new string[3];
            _header[0] = "Receipt Payment Account From " + txtFromDate.Text + " To " + txtToDate.Text;
            _header[1] = "Printed on " + DateTime.Now.ToString();
            _header[2] = "";

            string[] _footer = new string[0];

            Print.ReportPrint(Title, _header, gvReceiptPaymentAccount, _footer);
            Response.Redirect("RPTShowGrid.aspx");
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            string[] _header = new string[4];
            _header[0] = "<b>Receipt Payment Account Report</b>";
            _header[1] = "Trial Balance From " + txtFromDate.Text + " To " + txtToDate.Text;
            _header[2] = "Printed on " + DateTime.Now.ToString();
            _header[3] = "";

            string[] _footer = new string[0];

            string file = "RECEIPT_PAYMENT_ACCOUNT_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, gvReceiptPaymentAccount, _footer, file);
        }
    }
}