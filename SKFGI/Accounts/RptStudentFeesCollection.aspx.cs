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
using System.IO;

namespace CollegeERP.Accounts
{
    public partial class RptStudentFeesCollection : System.Web.UI.Page
    {

        decimal SumTotBill = 0, SumTotRecd = 0;
        ListItem li = new ListItem("---SELECT---", "0");

        ListItem liS = new ListItem(" ", "0");
        clsGeneralFunctions gf = new clsGeneralFunctions();
        DataSet ds;
        string strParams;
        char chr = Convert.ToChar(130);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //BindGrid();
                BindDropDown();
            }
        }

        private void BindDropDown()
        {
            strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchID"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();

            gf.BindDropDownColumnsBySP(ddlLedgerVw, "spSelect_MstGeneralLedgerBNKandCASH", strParams);
            ddlLedgerVw.Items.Insert(0, li);
        }

        private void BindGrid()
        {
            strParams = txtFromDate.Text + chr.ToString();
            strParams += txtToDate.Text + chr.ToString();
            strParams += txtChequeDateFrom.Text + chr.ToString();
            strParams += txtChequeDateTo.Text + chr.ToString();
            strParams += ddlLedgerVw.SelectedValue.ToUpper() + chr.ToString();
            strParams += ddlVoucherType.SelectedValue.ToString();
            

            ds = gf.ExecuteSelectSP("usp_StudentFeesCollectionReport", strParams);

            dgvStudentFeesCollection.DataSource = ds.Tables[0];
            dgvStudentFeesCollection.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string Title = "Student Fees Collection Report";
            string[] _header = new string[0];
            

            string[] _footer = new string[0];

            Print.ReportPrint(Title, _header, dgvStudentFeesCollection, _footer);
            Response.Redirect("RPTShowGrid.aspx");
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            string[] _header = new string[0];
            

            string[] _footer = new string[0];

            string file = "BRS_REPORT";

            BusinessLayer.Common.Excel.SaveExcel(_header, dgvStudentFeesCollection, _footer, file);
        }
    }
}
