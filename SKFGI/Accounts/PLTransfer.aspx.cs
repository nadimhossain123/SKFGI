using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.Accounts;
using System.Data;

namespace SKFGI.Accounts
{
    public partial class PLTransfer : System.Web.UI.Page
    {
        clsGeneralFunctions genObj = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        string strValues = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateGrid();
                PopulateAllDropDowns();
                Message.Show = false;
                txtTransferDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                PopulateTransferGrid();
            }
        }

        private void PopulateGrid()
        {

            if (Session["CompanyID"].ToString() == "3")
                strValues = "" + chr.ToString();
            else
                strValues = Session["CompanyID"].ToString() + chr.ToString();

            strValues += Session["BranchID"].ToString() + chr.ToString();
            strValues += Session["FinYrID"].ToString() + chr.ToString();

            strValues += "01 Apr " + GetFinYr().Split('-')[0].ToString() + " 00:00:00" + chr.ToString();
            strValues += "31 Mar " + GetFinYr().Split('-')[1].ToString() + " 23:59:59";

            genObj.ExecuteAnySP("sp_ProfitLoss", strValues);
            genObj.BindGridViewSP(gvBalanceSheet, "sp_GetPLBalance", strValues);

            ltrBalanceSheetHeading.Text = "01 Apr " + GetFinYr().Split('-')[0].ToString() + " To 31 Mar " + GetFinYr().Split('-')[1].ToString();
        }

        private void PopulateTransferGrid()
        {

            if (Session["CompanyID"].ToString() == "3")
                strValues = "" + chr.ToString();
            else
                strValues = Session["CompanyID"].ToString() + chr.ToString();

            strValues += Session["BranchID"].ToString() + chr.ToString();

            genObj.BindGridViewSP(gvCBVView, "usp_PLTransferGetAll", strValues);
        }

        protected void gvBalanceSheet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow GVR in gvBalanceSheet.Rows)
            {
                if (GVR.RowType == DataControlRowType.DataRow)
                {
                    Label lblamount1 = (Label)GVR.FindControl("lblamount1");
                    if (lblamount1.Text == "0.00")
                    {
                        lblamount1.Text = "";
                    }

                    Label lblamnt2 = (Label)GVR.FindControl("lblamnt2");
                    if (lblamnt2.Text == "0.00")
                    {
                        lblamnt2.Text = "";
                    }
                }
            }
        }

        private string GetFinYr()
        {
            string strValues = Session["FinYrID"].ToString();
            DataSet ds = genObj.ExecuteSelectSP("spSelect_MstFinancialYearForSelection", strValues);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["StartYear"].ToString() + "-" + ds.Tables[0].Rows[0]["EndYear"].ToString();
            }
            else { return ""; }

        }

        private void PopulateAllDropDowns()
        {
            strValues = Session["CompanyId"].ToString() + chr.ToString();
            strValues += Session["FinYrID"].ToString() + chr.ToString();
            strValues += Session["BranchID"].ToString() + chr.ToString();
            strValues += Session["DataFlow"].ToString();

            DataView dv = new DataView(genObj.GetDropDownColumnsBySP("spSelect_MstGeneralLedgerFull", strValues));
            dv.RowFilter = "LedgerType NOT IN ('BNK', 'CASH')";

            if (dv != null)
            {
                ddlLedg.DataSource = dv;
                ddlLedg.DataBind();
            }
            ListItem li = new ListItem("Select", "0");
            ddlLedg.Items.Insert(0, li);

            dv.RowFilter = "LedgerType = ('OTH') AND (ActType1 = 41 OR ActType2 = 42 OR ActType3 = 43)";
            if (dv != null)
            {
                ddlAdjustmentLedg.DataSource = dv;
                ddlAdjustmentLedg.DataBind();
            }
            ListItem ali = new ListItem("Select", "0");
            ddlAdjustmentLedg.Items.Insert(0, ali);
        }

        protected bool Validate()
        {
            if (ddlLedg.SelectedValue == "0")
                return false;
            if (ddlDRCR.SelectedValue == "0")
                return false;
            if (txtAmt.Text == "")
                return false;
            if (txtTransferDate.Text == "")
                return false;
            if (ddlAdjustmentLedg.SelectedValue == "0")
                return false;

            return true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validate())
                {
                    string rtMsg = "";
                    string strSPName = "";
                    string strValues = "";

                    strValues = DateTime.Now.ToString("dd MMM yyyy");
                    strValues += chr.ToString() + "";
                    DataSet ds_fn = genObj.ExecuteSelectSP("spSelect_GetFnYear", strValues);
                    strValues = "";

                    strSPName = "spInsert_PLTranfer";
                    strValues = Session["CompanyId"].ToString();
                    strValues += chr.ToString() + Session["FinYrID"].ToString();
                    strValues += chr.ToString() + Session["BranchID"].ToString();
                    strValues += chr.ToString() + txtAmt.Text;
                    strValues += chr.ToString() + ddlDRCR.SelectedItem.Text;
                    strValues += chr.ToString() + Session["DataFlow"].ToString();
                    strValues += chr.ToString() + txtTransferDate.Text;
                    strValues += chr.ToString() + ddlLedg.SelectedValue;
                    strValues += chr.ToString() + ddlAdjustmentLedg.SelectedValue;
                    strValues += chr.ToString() + "PLT";
                    strValues += chr.ToString() + txtNarration.Text.Trim();

                    rtMsg = genObj.ExecuteAnySPOutput(strSPName, strValues);

                    PopulateGrid();
                    PopulateTransferGrid();

                    Message.IsSuccess = true;
                    Message.Text = "Your request has been processed successfully!";
                }
            }
            catch (Exception ex)
            {
                Message.IsSuccess = false;
                Message.Text = ex.Message;
            }
            finally
            {
                Message.Show = true;
            }
        }

        protected void gvCBVView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int transferId = int.Parse(gvCBVView.DataKeys[e.RowIndex].Value.ToString());

            try
            {
                string strSPName = "";
                string strValues = "";

                strSPName = "usp_PLTransfer_Delete";
                strValues = transferId.ToString();

                genObj.ExecuteAnySPOutput(strSPName, strValues);

                PopulateGrid();
                PopulateTransferGrid();

                Message.IsSuccess = true;
                Message.Text = "Your request has been processed successfully!";
            }
            catch (Exception ex)
            {
                Message.IsSuccess = false;
                Message.Text = ex.Message;
            }
            finally
            {
                Message.Show = true;
            }
        }
    }
}