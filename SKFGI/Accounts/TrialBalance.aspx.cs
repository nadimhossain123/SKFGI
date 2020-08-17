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
using DataAccess.Accounts;

namespace CollegeERP.Accounts
{
    public partial class TrialBalance : System.Web.UI.Page
    {
        public string strFilter, strMessage;
        clsGeneralFunctions genObj = new clsGeneralFunctions();
        clsConnection objConn = new clsConnection();
        string strPrepareRPTHeader = "";
        string strPrepareRPTFooter = "";
        char chr = Convert.ToChar(130);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.TRIAL_BALANCE))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                ViewState["SortOrder"] = " ASC";
                txtToDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                populateCombo();
            }
        }

        private void populateCombo()
        {
            char chr = Convert.ToChar(130);
            string strValues = "";

            ddlFinancialYear.Items.Clear();
            genObj.BindDropDownColumnsBySP(ddlFinancialYear, "spSelect_MstFinancialYear", "");
            //ddlFinancialYear.Items.Insert(0, new ListItem("Select", "0"));
            ddlFinancialYear.SelectedValue = Session["FinYrID"].ToString();

            //By Satyabrata on 30/06/2010
            // A/c group population in search Tab 
            ddlGroupVw.Items.Clear();
            strValues = Session["CompanyId"].ToString() + chr.ToString() + "" + chr.ToString() + "Main Group";
            genObj.BindAjaxDropDownColumnsBySP(ddlGroupVw, "spSelect_MstAccountsGroup", strValues);
            ddlGroupVw.Items.Insert(0, new ListItem("ALL", "0"));
            ddlGroupVw.SelectedValue = "0";


        }

        protected void ddlBalanceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBalanceType.SelectedIndex == 0)
            {
                string strValues = ddlFinancialYear.SelectedValue.ToString();
                DataSet ds = genObj.ExecuteSelectSP("spSelect_MstFinancialYearForSelection", strValues);
                txtToDate.Text = Convert.ToDateTime("31 Mar " + ds.Tables[0].Rows[0]["EndYear"].ToString()).ToString("dd MMM yyyy");
            }
            else
            {
                string strValues = ddlFinancialYear.SelectedValue.ToString();
                DataSet ds = genObj.ExecuteSelectSP("spSelect_MstFinancialYearForSelection", strValues);
                txtToDate.Text = Convert.ToDateTime("01 Apr " + ds.Tables[0].Rows[0]["StartYear"].ToString()).ToString("dd MMM yyyy");
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateGrid("GROUPNAME", SortDirection.Ascending, strFilter);
        }
        private void PopulateGrid(string strSortExpression, SortDirection sortDir, string strFilter)
        {

            if (strFilter == "null" || strFilter == null)
                strFilter = "";
            string strValues = "";
            strValues = Session["CompanyId"].ToString();
            strValues += chr.ToString() + Session["BranchId"].ToString();
            //strValues += chr.ToString() + Session["FinYrID"].ToString();
            strValues += chr.ToString() + ddlFinancialYear.SelectedValue.Trim();
            strValues += chr.ToString() + Convert.ToDateTime(txtToDate.Text).ToString("dd-MMM-yyyy");
            strValues += chr.ToString() + Session["DataFlow"].ToString();
            strValues += chr.ToString() + ddlBalanceType.SelectedItem.Text.ToString();
            //Satyabrata on 01/07/2010
            strValues += chr.ToString() + ddlGroupVw.SelectedValue.Trim();
            strValues += chr.ToString() + ddlSubGroupVw.SelectedValue.Trim();
            //...........................
            genObj.BindGridViewSP(gvTrialBalnce, "spSelect_TrialBalance", strValues);
            strPrepareRPTHeader = @"<table border='0' width='100%' id='table1' cellspacing='1' cellpadding='2'>
                       <tr>         
	                    <td  align='center' >Trial Balance( " + ddlBalanceType.SelectedItem.Text + ") " + @"</td>                   
                       
                    </tr>
                    <tr>         
	                    <td  align='center' ><h3>For The Year :" + ddlFinancialYear.SelectedItem.Text.ToString() + "<br/>As on " + Convert.ToDateTime(txtToDate.Text).ToString("dd MMM yyyy") + @"</h3></td>
	                    
                       
                    </tr>
                     
                                  
                </table>
                ";
            //<tr>         

            //               <td class='TDRow' align='center'> <h1>As on " + Convert.ToDateTime(txtToDate.Text).ToString("dd-MMM-yyyy") + @"</h1></td>
            //           </tr>   
            //DataSet ds1 = genObj.ExecuteSelectSP("spSelect_MstFinancialYear_ForRPTTrailBalance", Session["SesCompanyID"].ToString() + chr.ToString() + Session["SesFinYrID"].ToString() + chr.ToString() + "");
            if (gvTrialBalnce.DataSource != null)
            {
                Session[clsGlobalVariable.sesReportPageHeader] = strPrepareRPTHeader;
                Session[clsGlobalVariable.sesReportPageFooter] = strPrepareRPTFooter;
                Session[clsGlobalVariable.sesReportGrid] = gvTrialBalnce;
                Session[clsGlobalVariable.sesReportTitle] = "<Font size='2pt'><b>" + Session["BranchName"] + "</b></font>";
                btnPrint.Attributes["onclick"] = "window.open('RPTShowGrid.aspx');";
            }
        }

        protected void ddlGroupVw_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strMessage = "";
            char chr = Convert.ToChar(130);
            string strValues = "";

            if (ddlGroupVw.SelectedValue != "0")
            {
                ddlSubGroupVw.Items.Clear();
                strValues = Session["CompanyId"].ToString();
                strValues += chr.ToString() + ddlGroupVw.SelectedValue.ToString();
                strValues += chr.ToString() + "Sub Group";
                //genObj.BindAjaxDropDownColumnsBySP(ddlSubGroupVw, "spSelect_MstAccountsGroup", strValues);
                DataView dv = new DataView(genObj.GetDropDownColumnsBySP("spSelect_MstAccountsGroup", strValues));
                dv.RowFilter = "GroupID_FK=" + ddlGroupVw.SelectedValue.ToString();

                ddlSubGroupVw.DataValueField = "GroupID";
                ddlSubGroupVw.DataTextField = "GroupName";
                ddlSubGroupVw.DataSource = dv;
                ddlSubGroupVw.DataBind();
                
                ddlSubGroupVw.Items.Insert(0, new ListItem("ALL", "0"));
                ddlSubGroupVw.SelectedValue = "0";
            }
            else
            {
                ddlSubGroupVw.Items.Clear();
                ddlSubGroupVw.Items.Insert(0, new ListItem("Select", "0"));
                ddlSubGroupVw.SelectedValue = "0";
            }

        }

       
    }
}
