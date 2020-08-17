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

namespace SKFGI.Accounts
{
    public partial class StudentBillDetails : System.Web.UI.Page
    {
        DataSet ds;
        public int BillId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_BILL_DETAILS))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                Message.Show = false;
                txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd MMM yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                //LoadBillList();
            }
        }

        protected void LoadBillList()
        {
            dgvBillMaster.DataSource = GetBillTable();
            dgvBillMaster.DataBind();

            //btnPrint.Visible = true;
            //btnExportToExcel.Visible = true;
        }

        protected DataTable GetBillTable()
        {
            string Name = txtReceiptNo.Text.Trim();
            string FromDate = txtFromDate.Text.Trim() + " 00:00:00";
            string ToDate = txtToDate.Text.Trim() + " 23:59:59";
            int SemNo = 0;

            BusinessLayer.Accounts.StudentFeesCollection ObjFees = new BusinessLayer.Accounts.StudentFeesCollection();
            ds = new DataSet();
            ds = ObjFees.GetAllStudentBill(Name, FromDate, ToDate, SemNo);

            DataView dv = new DataView(ds.Tables[0]);
            dv.RowFilter = "company_id=" + Convert.ToInt32(Session["CompanyId"].ToString());
            return dv.ToTable();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadBillList();
        }

        protected void dgvBillMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //    //-----------Add On 06-08-2013 --------------------
                //    TotalAmt += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Amount"));
                //    //-------------------------------------------------
                BillId = int.Parse(dgvBillMaster.DataKeys[e.Row.RowIndex].Values["BillId"].ToString());
                //    CheckBox ChkIsRefund = (CheckBox)e.Row.FindControl("ChkIsRefund");
                //    int refund = (ChkIsRefund.Checked) ? 1 : 0;
                //    //-------------Add On 08-08-2013-------------------------
                //    if (int.Parse(dgvPaymentMaster.DataKeys[e.Row.RowIndex].Values["company_id"].ToString()) == 2)
                //    {
                //        ((ImageButton)e.Row.FindControl("btnPrint")).Attributes.Add("onclick", "javascript:openPopup('MoneyReceipt.aspx?id=" + PaymentId + "&refund=" + refund + "'); return false;");
                //    }
                //    else if (int.Parse(dgvPaymentMaster.DataKeys[e.Row.RowIndex].Values["company_id"].ToString()) == 4)
                //    {
                //        ((ImageButton)e.Row.FindControl("btnPrint")).Attributes.Add("onclick", "javascript:openPopup('MoneyReceiptDiploma.aspx?id=" + PaymentId + "&refund=" + refund + "'); return false;");
                //    }
                //    else
                //    {
                //        ((ImageButton)e.Row.FindControl("btnPrint")).Attributes.Add("onclick", "javascript:openPopup('MoneyReceiptMgmnt.aspx?id=" + PaymentId + "&refund=" + refund + "'); return false;");
                //    }
                //    //--------------------------------------------------------
                //    //comment ON 08-08-2013((ImageButton)e.Row.FindControl("btnPrint")).Attributes.Add("onclick", "javascript:openPopup('MoneyReceipt.aspx?id=" + PaymentId + "&refund=" + refund + "'); return false;");
                //    ((ImageButton)e.Row.FindControl("btnDelete")).Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.DELETE_VOUCHER);

                using (DataView dv = new DataView(ds.Tables[1]))
                {
                    dv.RowFilter = "BillId=" + BillId;
                    GridView dgv = (GridView)e.Row.FindControl("dgvBillDetails");
                    dgv.DataSource = dv.ToTable();
                    dgv.DataBind();
                }
            }
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    Label lblCashtotal = (Label)e.Row.FindControl("lblCashtotal");
            //    lblCashtotal.Text = TotalAmt.ToString();
            //}
        }

        protected void dgvBillMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = Convert.ToInt32(dgvBillMaster.DataKeys[e.RowIndex].Value.ToString());
            BusinessLayer.Accounts.StudentFeesCollection objFees = new BusinessLayer.Accounts.StudentFeesCollection();
            objFees.BillDelete(Id);
            LoadBillList();
        }

        protected void dgvBillDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
                GridView dgv = sender as GridView;

                int BillId = Convert.ToInt32(dgv.DataKeys[e.RowIndex].Values[0].ToString());
                int FeesHeadId = Convert.ToInt32(dgv.DataKeys[e.RowIndex].Values[1].ToString());
                BusinessLayer.Accounts.StudentFeesCollection objFees = new BusinessLayer.Accounts.StudentFeesCollection();
                objFees.FeeHeadWiseBillDelete(BillId, FeesHeadId);
                LoadBillList();
            
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //string Title = "Payment Details Report";
            //string[] _header = new string[3];
            //_header[0] = "Receipt No: " + txtReceiptNo.Text;
            //_header[1] = "From: " + txtFromDate.Text;
            //_header[2] = "To: " + txtToDate.Text;

            //string[] _footer = new string[0];
            //using (GridView dgv = new GridView())
            //{
            //    DataTable dt = GetPaymentTable();
            //    dt.Columns.Remove("PaymentId");
            //    dt.Columns.Remove("company_id");

            //    dgv.AutoGenerateColumns = true;
            //    dgv.DataSource = dt;
            //    dgv.DataBind();

            //    Print.ReportPrint(Title, _header, dgv, _footer);
            //    Response.Redirect("RPTShowGrid.aspx");
            //}
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            //string[] _header = new string[4];
            //_header[0] = "<b>Supreme Knowledge Foundation Group of Institutions</b>";
            //_header[1] = "Receipt No: " + txtReceiptNo.Text;
            //_header[2] = "From: " + txtFromDate.Text;
            //_header[3] = "To: " + txtToDate.Text;

            //string[] _footer = new string[0];

            //string file = "BILL_DETAILS_REPORT";

            //using (GridView dgv = new GridView())
            //{
            //    DataTable dt = GetPaymentTable();
            //    dt.Columns.Remove("PaymentId");
            //    dt.Columns.Remove("company_id");

            //    dgv.AutoGenerateColumns = true;
            //    dgv.DataSource = dt;
            //    dgv.DataBind();
            //    BusinessLayer.Common.Excel.SaveExcel(_header, dgv, _footer, file);
            //}
        }
    }
}