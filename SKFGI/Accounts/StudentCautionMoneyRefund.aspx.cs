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
    public partial class StudentCautionMoneyRefund : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        string strParams = "";
        ListItem li = new ListItem("---SELECT---", "0");
        ListItem liS = new ListItem(" ", "0");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_CAUTION_MONEY_REFUND))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
              
                ClearControls();
            }
        }

        protected void LoadCashBankLedger()
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
            txtChequeNo.Text = "";
            txtDrawnOn.Text = "";
            txtChequeDate.Text = "";
            ltrLedgerBalance.Text = "";
            txtNarration.Text = "";
            ddlReceiptMode.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            txtFeesBookNo.Text = String.Empty;
            lblDropout.Visible = false;

            txtChequeNo.Enabled = false;
            txtDrawnOn.Enabled = false;
            txtChequeDate.Enabled = false;

            ImgPhoto.ImageUrl = "../Student/StudentPhoto/Male.jpg";

            dgvFeesHead.DataSource = null;
            dgvFeesHead.DataBind();

            //------------------14-09-2013
            LoadBatch();
            // LoadStream();
            LoadCourse();

            LoadCashBankLedger();
            LoadApprovedStudent();
            btnPrint.Attributes.Add("onclick", "javascript:alert('No Money Receipt To Print'); return false;");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlStudent.SelectedValue != "0" && ddlStudent.Text != string.Empty)
            {
                BusinessLayer.Accounts.StudentFeesCollection ObjFees = new BusinessLayer.Accounts.StudentFeesCollection();
                int StudentId = Convert.ToInt32(ddlStudent.SelectedValue.Trim());

                DataSet ds = ObjFees.GetStudentRefundableTrans(StudentId);
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
                    BusinessLayer.Accounts.StudentFeesCollection ObjFees = new BusinessLayer.Accounts.StudentFeesCollection();
                    Entity.Accounts.StudentFeesCollection Fees = new Entity.Accounts.StudentFeesCollection();
                    Fees.PaymentId = 0; //Always Save
                    Fees.StudentId = Convert.ToInt32(ddlStudent.SelectedValue.Trim());
                    Fees.SemNo = Convert.ToInt32(ddlSemester.SelectedValue.Trim());
                    Fees.Amount = Convert.ToDecimal(txtTotalAmt.Text.Trim());
                    Fees.PaymentDate = Convert.ToDateTime(txtVoucherDate.Text.Trim() + " 00:00:00");
                    Fees.CashBankLedgerID = Convert.ToInt32(ddlCashBankLedger.SelectedValue.Trim());
                    Fees.TransactionType = "PAYMENT";
                    Fees.ModeOfPayment = ddlReceiptMode.SelectedValue.Trim();
                    Fees.ChequeNo = txtChequeNo.Text.Trim();

                    if (txtChequeDate.Text.Trim().Length == 0)
                        Fees.ChequeDate = null;
                    else
                        Fees.ChequeDate = Convert.ToDateTime(txtChequeDate.Text.Trim());

                    Fees.DrawnOn = txtDrawnOn.Text.Trim();
                    Fees.CreatedBy = int.Parse(HttpContext.Current.User.Identity.Name);
                    Fees.CompanyId = int.Parse(Session["CompanyId"].ToString());
                    Fees.BranchId = int.Parse(Session["BranchId"].ToString());
                    Fees.FinYrId = int.Parse(Session["FinYrID"].ToString());
                    Fees.FeesBookNumber = txtFeesBookNo.Text.Trim();
                    Fees.Narration = txtNarration.Text.Trim();

                    DataTable DT = new DataTable();
                    DT.Columns.Add("FeesHeadId", typeof(int));
                    DT.Columns.Add("Amount", typeof(decimal));
                    DataRow DR;

                    foreach (GridViewRow DGV in dgvFeesHead.Rows)
                    {
                        if (DGV.RowType == DataControlRowType.DataRow)
                        {
                            TextBox txtAmount = (TextBox)DGV.FindControl("txtAmount");
                            decimal Amount = (txtAmount.Text.Trim().Length > 0) ? Convert.ToDecimal(txtAmount.Text.Trim()) : 0;

                            if (Amount > 0)
                            {
                                DR = DT.NewRow();
                                DR["FeesHeadId"] = Convert.ToInt32(dgvFeesHead.DataKeys[DGV.RowIndex].Values["id"].ToString());
                                DR["Amount"] = Amount;
                                DT.Rows.Add(DR);
                                DT.AcceptChanges();
                            }
                        }
                    }

                    using (DataSet ds = new DataSet())
                    {
                        ds.Tables.Add(DT);
                        Fees.PaymentDetailsXML = ds.GetXml().Replace("Table1>", "Table>");
                    }

                    Fees.XMLCashBankVoucherDetails = PrepareXMLString();
                    Fees.IsRefund = true;

                    ObjFees.SaveRefundableFees(Fees);
                    Fees = ObjFees.Refund_GetAllById(Fees.PaymentId);
                    btnSearch_Click(sender, e);
                    GetLedgerBalance();
                    txtVoucherDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                    ddlSemester.SelectedIndex = 0;
                    txtTotalAmt.Text = "0.00";

                    Message.IsSuccess = true;
                    Message.Text = "Money Receipt No " + Fees.MoneyReceiptNo + " is generated. You can take print out now";
                    txtReceiptNo.Text = Fees.MoneyReceiptNo;
                    //--------------Page Clear--------------
                   //txtReceiptNo.Text = "Auto Generated";
                    //txtVoucherDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                    txtChequeNo.Text = "";
                    txtDrawnOn.Text = "";
                    txtChequeDate.Text = "";
                    ltrLedgerBalance.Text = "";
                    txtNarration.Text = "";
                    ddlReceiptMode.SelectedIndex = 0;
                    ddlSemester.SelectedIndex = 0;
                    txtFeesBookNo.Text = String.Empty;
                    txtChequeNo.Enabled = false;
                    txtDrawnOn.Enabled = false;
                    txtChequeDate.Enabled = false;
                    ImgPhoto.ImageUrl = "../Student/StudentPhoto/Male.jpg";
                    dgvFeesHead.DataSource = null;
                    dgvFeesHead.DataBind();
                    LoadCashBankLedger();
                    LoadApprovedStudent();
                    string CompanyId = Session["CompanyId"].ToString();
                    //-----------------------------------------------------------Add On 08-08-2013
                    if (CompanyId == "2")
                    {
                        btnPrint.Attributes.Add("onclick", "javascript:openPopup('RPTGridView.aspx?id=" + Fees.CBVHeaderId + "'); return false;");
                    }
                    else if (CompanyId == "4")
                    {
                        btnPrint.Attributes.Add("onclick", "javascript:openPopup('RPTGridViewDiploma.aspx?id=" + Fees.CBVHeaderId + "'); return false;");
                    }
                    else if (CompanyId == "1")
                    {
                        btnPrint.Attributes.Add("onclick", "javascript:openPopup('RPTGridViewMgmnt.aspx?id=" + Fees.CBVHeaderId + "'); return false;");
                    }
                    //------------------------------------------------------------
                   // Comment On 08-08-2013 btnPrint.Attributes.Add("onclick", "javascript:openPopup('MoneyReceipt.aspx?id=" + Fees.PaymentId + "&refund=1&PrintRefund=1'); return false;");
                //}
                //else
                //{
                //    Message.IsSuccess = false;
                //    Message.Text = "Please select a Date Between " + Session["SesFromDate"].ToString() + " & " + Session["SesToDate"].ToString() + "";
                //}
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
            foreach (GridViewRow DGV in dgvFeesHead.Rows)
            {
                if (DGV.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtAmount = (TextBox)DGV.FindControl("txtAmount");
                    decimal Amount = (txtAmount.Text.Trim().Length > 0) ? Convert.ToDecimal(txtAmount.Text.Trim()) : 0;

                    if (Amount > 0)
                    {
                        strXMLString += "<TrnCashBankVoucherDetail";
                        strXMLString += " SrlNo = \"" + SrlNo.ToString() + "\"";
                        strXMLString += " ByTo = \"" + ByTo + "\"";
                        strXMLString += " LedgerID = \"" + dgvFeesHead.DataKeys[DGV.RowIndex].Values["IncomeLedgerID_FK"].ToString() + "\"";
                        strXMLString += " DRAmount = \"" + Amount + "\"";
                        strXMLString += " CRAmount = \"" + CRAmount + "\"";
                        strXMLString += " />";
                        SrlNo += 1;
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
            else if (ddlReceiptMode.SelectedValue == "CHEQUE")
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
                ErrorText = "Sorry! Voucher Date is not Within Current Financial Year. Please Check.";
            }
            else if (Convert.ToDateTime(txtVoucherDate.Text.Trim()) > DateTime.Now)
            {
                result = false;
                ErrorText = "Sorry! Future Voucher Date is Not Allowed.";
            }
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
            if (ddlReceiptMode.SelectedValue == "CASH")
            {
                txtChequeNo.Text = "";
                txtDrawnOn.Text = "";
                txtChequeDate.Text = "";

                txtChequeNo.Enabled = false;
                txtDrawnOn.Enabled = false;
                txtChequeDate.Enabled = false;
            }
            else if (ddlReceiptMode.SelectedValue == "CHEQUE")
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

        protected void dgvFeesHead_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                ((TextBox)e.Row.FindControl("txtAmount")).Attributes.Add("onkeydown", "javascript:moveEnter(" + (e.Row.RowIndex + 1) + ");");
            }
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
                lblDropout.Visible = true;
                lblDropout.Text = "DropOut";
            }
            else
            {
                lblDropout.Visible = false;
                lblDropout.Text = "";
            }
        }
    }
}
