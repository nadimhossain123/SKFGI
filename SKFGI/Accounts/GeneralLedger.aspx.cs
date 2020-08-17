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
    public partial class GeneralLedger : System.Web.UI.Page
    {
        public string strFilter;
        clsGeneralFunctions genObj = new clsGeneralFunctions();
        clsConnection objConn = new clsConnection();
        char chr = Convert.ToChar(130);
        ListItem li = new ListItem("Select", "0");

        public int LedgerID
        {
            get { return Convert.ToInt32(ViewState["LedgerID"]); }
            set { ViewState["LedgerID"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.GENERAL_LEDGER))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadDropdown();
                ResetControls();
                PopulateGrid(null);
            }
        }

        protected void ResetControls()
        {
            LedgerID = 0;

            ddlLedgerType.SelectedValue = "0";
            ddlGroup.SelectedValue = "0";
            ddlSubGroup.Items.Clear();
            ddlSubGroup.Items.Insert(0, li);

            ddlOpeningBalanceType.SelectedValue = "DR";
            txtLedgerName.Text = "";
            txtOpeningBalance.Text = "0.00";
            txtOpBalDate.Text = DateTime.Now.ToString("dd MMM yyyy");


            txtBuildingNo.Text = "";
            txtBName.Text = "";
            txtFlatNo.Text = "";
            txtFloor.Text = "";
            txtStreet.Text = "";
            ddlCountry.SelectedValue = "0";
            txtPinCode.Text = "";
            txtMobileNo.Text = "";
            txtResidentialContactNo.Text = "";
            txtOfficeContactNo.Text = "";
            txtPANNo.Text = "";
            txtVATNo.Text = "";
            txtOther.Text = "";
            Message.Show = false;
            btnSave.Text = "Save";
            btnSave.Enabled = true;
        }

        protected void LoadDropdown()
        {
            char chr = Convert.ToChar(130);
            string strValues = "";
            // A/c group population
            strValues = "" + chr.ToString() + "Main Group";
            genObj.BindAjaxDropDownColumnsBySP(ddlGroup, "spSelect_MstAccountsGroup", strValues);
            ddlGroup.Items.Insert(0, li);

            BusinessLayer.Common.Country ObjCountry = new BusinessLayer.Common.Country();
            DataTable dt = ObjCountry.GetAll();
            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["CountryId"] = "0";
                dr["CountryName"] = "--Select Country--";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                ddlCountry.DataSource = dt;
                ddlCountry.DataBind();
            }
            LoadState();

        }

        protected void LoadState()
        {
            int CountryId = int.Parse(ddlCountry.SelectedValue.Trim());
            BusinessLayer.Common.State ObjState = new BusinessLayer.Common.State();
            DataTable dt = ObjState.GetAll(CountryId);
            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["StateId"] = "0";
                dr["StateNameWithCode"] = "--Select State--";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                ddlState.DataSource = dt;
                ddlState.DataBind();
            }

            LoadCity();
        }

        protected void LoadCity()
        {
            int StateId = int.Parse(ddlState.SelectedValue.Trim());
            BusinessLayer.Common.City ObjCity = new BusinessLayer.Common.City();
            DataTable dt = ObjCity.GetAll(StateId);
            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["CityId"] = "0";
                dr["CityName"] = "--Select City--";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                ddlCity.DataSource = dt;
                ddlCity.DataBind();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadState();
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCity();
        }

        protected void PopulatePage()
        {
            string strChkAct;

            string strValues = "";
            strValues = Session["CompanyId"].ToString();
            strValues += chr.ToString() + Session["FinYrID"].ToString();
            strValues += chr.ToString() + Session["BranchID"].ToString();
            strValues += chr.ToString() + LedgerID.ToString();
            strValues += chr.ToString() + "" + chr.ToString() + Session["DataFlow"].ToString();
            DataSet ds = genObj.ExecuteSelectSP("spSelect_MstGeneralLedger", strValues);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Session["FinYrID"].ToString() != ds.Tables[0].Rows[0]["FinYearID_FK"].ToString())
                {
                    btnSave.Enabled = false;
                    Message.IsSuccess = false;
                    Message.Text = "Sorry! This Ledger is not created within current financial year. Update is not allowed.";
                    Message.Show = true;
                }
                else
                    Message.Show = false;

                txtLedgerName.Text = ds.Tables[0].Rows[0]["LedgerName"].ToString();
                ddlLedgerType.SelectedValue = ds.Tables[0].Rows[0]["LedgerType"].ToString();
                ddlGroup.SelectedValue = ds.Tables[0].Rows[0]["GroupID_FK"].ToString();
                LoadSubGroup();

                if (ds.Tables[0].Rows[0]["SubGroupID_FK"].ToString() == "" || ds.Tables[0].Rows[0]["SubGroupID_FK"].ToString() == "0")
                    ddlSubGroup.SelectedValue = "0";
                else
                    ddlSubGroup.SelectedValue = ds.Tables[0].Rows[0]["SubGroupID_FK"].ToString();

                txtOpeningBalance.Text = ds.Tables[1].Rows[0]["OpeningBalance"].ToString();
                ddlOpeningBalanceType.SelectedValue = ds.Tables[1].Rows[0]["OpeningBalanceType"].ToString();

                strChkAct = ds.Tables[0].Rows[0]["CostCenterApplied"].ToString();
                if (strChkAct == "True")
                    chkCostCenter.Checked = true;
                else
                    chkCostCenter.Checked = false;
                strChkAct = ds.Tables[0].Rows[0]["Active"].ToString();
                if (strChkAct == "True")
                    chkActive.Checked = true;
                else
                    chkActive.Checked = false;
                txtOpBalDate.Text = (ds.Tables[0].Rows[0]["OpeningDate"] == DBNull.Value ? "" : ((DateTime)ds.Tables[0].Rows[0]["OpeningDate"]).ToString("dd MMM yyyy"));

                txtBuildingNo.Text = (ds.Tables[0].Rows[0]["BuildingNo"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["BuildingNo"]).ToString();
                txtBName.Text = (ds.Tables[0].Rows[0]["BuildingName"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["BuildingName"]).ToString(); //ds.Tables[0].Rows[0]["BuildingName"].ToString();
                txtFlatNo.Text = (ds.Tables[0].Rows[0]["FlatNo"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["FlatNo"]).ToString(); //ds.Tables[0].Rows[0]["FlatNo"].ToString();
                txtFloor.Text = (ds.Tables[0].Rows[0]["Floor"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["Floor"]).ToString(); //ds.Tables[0].Rows[0]["Floor"].ToString();
                txtStreet.Text = (ds.Tables[0].Rows[0]["Street"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["Street"]).ToString(); //ds.Tables[0].Rows[0]["Street"].ToString();

                ddlCountry.ClearSelection();
                ddlCountry.SelectedValue = (ds.Tables[0].Rows[0]["CountryID_FK"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["CountryID_FK"]).ToString(); //ds.Tables[0].Rows[0]["CountryID_FK"].ToString();
                LoadState();
                ddlState.SelectedValue = (ds.Tables[0].Rows[0]["StateID_FK"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["StateID_FK"]).ToString(); //ds.Tables[0].Rows[0]["StateID_FK"].ToString();
                LoadCity();
                ddlCity.SelectedValue = (ds.Tables[0].Rows[0]["CityID_FK"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["CityID_FK"]).ToString(); //ds.Tables[0].Rows[0]["CityID_FK"].ToString();

                txtPinCode.Text = (ds.Tables[0].Rows[0]["PinCode"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["PinCode"]).ToString(); //ds.Tables[0].Rows[0]["PinCode"].ToString();
                txtMobileNo.Text = (ds.Tables[0].Rows[0]["MobileNo"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MobileNo"]).ToString(); //ds.Tables[0].Rows[0]["MobileNo"].ToString();
                txtResidentialContactNo.Text = (ds.Tables[0].Rows[0]["HomeContactNo"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["HomeContactNo"]).ToString(); //ds.Tables[0].Rows[0]["HomeContactNo"].ToString();
                txtOfficeContactNo.Text = (ds.Tables[0].Rows[0]["HomeContactNo"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["OfficeContactNo"]).ToString(); //ds.Tables[0].Rows[0]["OfficeContactNo"].ToString();
                txtPANNo.Text = (ds.Tables[0].Rows[0]["PANNo"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["PANNo"]).ToString(); //ds.Tables[0].Rows[0]["PANNo"].ToString();
                txtVATNo.Text = (ds.Tables[0].Rows[0]["VATNo"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["VATNo"]).ToString(); //ds.Tables[0].Rows[0]["VATNo"].ToString();
                txtOther.Text = (ds.Tables[0].Rows[0]["Other"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["Other"]).ToString(); //ds.Tables[0].Rows[0]["Other"].ToString();
                btnSave.Text = "Update";
            }


        }

        protected void PopulateGrid(string strFilter)
        {
            char chr = Convert.ToChar(130);
            if (strFilter == "null" || strFilter == null)
                strFilter = "";
            string strValues = "";
            strValues = Session["CompanyId"].ToString();
            strValues += chr.ToString() + Session["FinYrID"].ToString();
            strValues += chr.ToString() + Session["BranchID"].ToString();
            strValues += chr.ToString() + "" + chr.ToString() + "" + chr.ToString() + Session["DataFlow"].ToString();
            genObj.BindGridViewSP(gdGenLedger, "spSelect_MstGeneralLedger", strValues, strFilter);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                char chr = Convert.ToChar(130);
                string strSPName = "";
                string strValues = "";

                if (LedgerID == 0)
                    strSPName = "spInsert_MstGeneralLedger";
                else
                {
                    strValues = LedgerID.ToString();
                    strSPName = "spUpdate_MstGeneralLedger";
                }
                // Value for BankAccount Table
                if (strValues == "")
                    strValues = Session["CompanyId"].ToString();
                else
                    strValues += chr.ToString() + Session["CompanyId"].ToString();

                strValues += chr.ToString() + Session["FinYrID"].ToString();
                strValues += chr.ToString() + Session["BranchID"].ToString();
                strValues += chr.ToString() + txtLedgerName.Text.Trim().ToString();
                strValues += chr.ToString() + ddlGroup.SelectedValue.Trim().ToString();
                strValues += chr.ToString() + ddlSubGroup.SelectedValue.Trim().ToString();
                if (chkCostCenter.Checked)
                    strValues += chr.ToString() + "True";
                else
                    strValues += chr.ToString() + "False";
                strValues += chr.ToString() + ddlLedgerType.SelectedValue.Trim().ToString();
                if (chkActive.Checked)
                    strValues += chr.ToString() + "True";
                else
                    strValues += chr.ToString() + "False";
                strValues += chr.ToString() + txtOpeningBalance.Text.Trim().ToString();
                strValues += chr.ToString() + ddlOpeningBalanceType.SelectedValue.Trim();
                strValues += chr.ToString() + Session["UserId"];
                strValues += chr.ToString() + Session["DataFlow"].ToString();
                strValues += chr.ToString() + txtOpBalDate.Text.Trim().ToString();

                strValues += chr.ToString() + txtBuildingNo.Text.Trim().ToString();
                strValues += chr.ToString() + txtBName.Text.Trim().ToString();
                strValues += chr.ToString() + txtFlatNo.Text.Trim().ToString();
                strValues += chr.ToString() + txtFloor.Text.Trim().ToString();
                strValues += chr.ToString() + txtStreet.Text.Trim().ToString();
                strValues += chr.ToString() + ddlCity.SelectedValue.Trim().ToString();
                strValues += chr.ToString() + ddlState.SelectedValue.Trim().ToString();
                strValues += chr.ToString() + ddlCountry.SelectedValue.Trim().ToString();
                strValues += chr.ToString() + txtOther.Text.Trim().ToString();
                strValues += chr.ToString() + txtPinCode.Text.Trim().ToString();
                strValues += chr.ToString() + txtMobileNo.Text.Trim().ToString();
                strValues += chr.ToString() + txtResidentialContactNo.Text.Trim().ToString();
                strValues += chr.ToString() + txtOfficeContactNo.Text.Trim().ToString();
                strValues += chr.ToString() + txtPANNo.Text.Trim().ToString();
                strValues += chr.ToString() + txtVATNo.Text.Trim().ToString();

                string rtMsg = genObj.ExecuteAnySPOutput(strSPName, strValues);
                if (rtMsg == "True")
                {
                    Message.IsSuccess = true;
                    Message.Text = "Your request has been processed successfully!";
                    ResetControls();
                    PopulateGrid(null);
                }
                else if (rtMsg == "Duplicate")
                {
                    Message.IsSuccess = false;
                    Message.Text = "This General Ledger already exist!";
                }

            }

            Message.Show = true;
        }


        protected bool Validate()
        {
            bool result = true;
            string ErrorText = "";
            if (ddlGroup.SelectedValue == "0" || ddlGroup.Text == string.Empty)
            {
                result = false;
                ErrorText = "Please Select Group";
            }

            if (!result)
            {
                Message.IsSuccess = false;
                Message.Text = ErrorText;
            }
            return result;
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubGroup();
        }

        protected void LoadSubGroup()
        {
            char chr = Convert.ToChar(130);
            string strValues = "";

            strValues = "" + chr.ToString() + "Sub Group";
            DataView dv = new DataView(genObj.GetDropDownColumnsBySP("spSelect_MstAccountsGroup", strValues));
            dv.RowFilter = "GroupID_FK=" + ddlGroup.SelectedValue.ToString();

            ddlSubGroup.DataSource = dv;
            ddlSubGroup.DataBind();

            ddlSubGroup.Items.Insert(0, li);
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            strFilter = "";
            if (txtLedgerNameVw.Text != "")
            {
                strFilter = "LedgerName like '" + txtLedgerNameVw.Text.Trim() + "%'";
            }
            gdGenLedger.PageIndex = 0;
            PopulateGrid(strFilter);
        }

        protected void gdGenLedger_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdGenLedger.PageIndex = e.NewPageIndex;
            PopulateGrid(strFilter);
        }

        protected void gdGenLedger_RowEditing(object sender, GridViewEditEventArgs e)
        {
            LedgerID = Convert.ToInt32(gdGenLedger.DataKeys[e.NewEditIndex].Value);
            PopulatePage();
        }

    }
}
