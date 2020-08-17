using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer.Accounts;

namespace CollegeERP.Student
{
    public partial class Fees : System.Web.UI.Page
    {
        int courseType = 0;
        int headerCount = 0;
        int width = 50;
        DataTable dt = new DataTable();
        int intHeaderID = 0;
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        ListItem li = new ListItem("Select", "0");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }

            courseType = 0;
            ////////////////////////////////////////////////////////////////////////////

            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.FEES))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                populateLoad();
                if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim().Length > 0)
                {
                    populateFeesDetails(Convert.ToInt32(Request.QueryString["id"].ToString()));
                    intHeaderID = Convert.ToInt32(Request.QueryString["id"].ToString());
                }

            }
            else
            {
                if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim().Length > 0)
                {
                    intHeaderID = Convert.ToInt32(Request.QueryString["id"].ToString());
                }
            }
            courseType = Convert.ToInt32(hidCourse_id.Value.ToString());
            populatefees();
            Message.Show = false;
        }

        private void populateFeesDetails(int headerID)
        {
            BusinessLayer.Student.StreamGroup stremGrp = new BusinessLayer.Student.StreamGroup();
            Entity.Student.StreamGroup estremGrp = new Entity.Student.StreamGroup();

            estremGrp.intMode = 11;
            estremGrp.feesID = headerID;
            DataTable dt = new DataTable();
            dt = stremGrp.FeesBasedOnID(estremGrp);
            if (dt.Rows.Count > 0)
            {
                ddlBatch.SelectedValue = dt.Rows[0]["batchID"].ToString();
                ddlCourse1.SelectedValue = dt.Rows[0]["CourseId"].ToString();

                estremGrp.intCompanyId = Convert.ToInt32(Session["CompanyId"].ToString());
                estremGrp.intMode = 3;
                estremGrp.courseID = Convert.ToInt32(ddlCourse1.SelectedValue.ToString());

                DataSet ds = new DataSet();
                ds = stremGrp.GetLoad(estremGrp);
                if (ds.Tables.Count > 0)
                {
                    ddlStream1.DataSource = ds.Tables[0];
                    ddlStream1.DataTextField = "stream_name";
                    ddlStream1.DataValueField = "StreamId";
                    ddlStream1.DataBind();
                    ddlStream1.SelectedValue = "0";
                }
                ddlStream1.SelectedValue = dt.Rows[0]["streamID"].ToString();

                txtFeesName.Text = dt.Rows[0]["fees_name"].ToString();
                hidCourse_id.Value = dt.Rows[0]["CourseId"].ToString();
            }
        }
        protected void ClearFields(ControlCollection pageControls)
        {
            foreach (Control contl in pageControls)
            {
                string strCntName = (contl.GetType()).Name;
                switch (strCntName)
                {
                    case "TextBox":
                        TextBox tbSource = (TextBox)contl;
                        tbSource.Text = "";
                        break;
                    case "RadioButtonList":
                        RadioButtonList rblSource = (RadioButtonList)contl;
                        rblSource.SelectedIndex = -1;
                        break;
                    case "DropDownList":
                        DropDownList ddlSource = (DropDownList)contl;
                        //ddlSource.SelectedIndex = 0;
                        ddlSource.SelectedValue = "0";
                        break;
                    case "ListBox":
                        ListBox lbsource = (ListBox)contl;
                        lbsource.SelectedIndex = -1;
                        break;
                    case "RadioButton":
                        RadioButton rdb = (RadioButton)contl;
                        rdb.Checked = false;
                        break;
                    case "CheckBox":
                        CheckBox chk = (CheckBox)contl;
                        chk.Checked = false;
                        break;

                }
                ClearFields(contl.Controls);
            }
            Message.Show = false;
        }
        private void populateLoad()
        {
            BusinessLayer.Student.StreamGroup stremGrp = new BusinessLayer.Student.StreamGroup();
            Entity.Student.StreamGroup estremGrp = new Entity.Student.StreamGroup();

            estremGrp.intCompanyId = Convert.ToInt32(Session["CompanyId"].ToString());
            estremGrp.intMode = 2;
            estremGrp.stream_type_id = courseType;
            
            DataSet ds = new DataSet();
            ds = stremGrp.GetLoad(estremGrp);
            if (ds.Tables.Count > 0)
            {
                ddlBatch.DataSource = ds.Tables[0];
                ddlBatch.DataTextField = "batch_name";
                ddlBatch.DataValueField = "id";
                ddlBatch.DataBind();
                ddlBatch.SelectedValue = "0";

                DataView dvCourse = new DataView(ds.Tables[1]);
                dvCourse.RowFilter = "CompanyId=" + Session["CompanyId"].ToString() + "or CompanyId = 0";
                ddlCourse1.DataSource = dvCourse;
                ddlCourse1.DataTextField = "CourseName";
                ddlCourse1.DataValueField = "CourseId";
                ddlCourse1.DataBind();
                ddlCourse1.SelectedValue = "0";

                ddlStream1.DataSource = ds.Tables[2];
                ddlStream1.DataTextField = "stream_name";
                ddlStream1.DataValueField = "StreamId";
                ddlStream1.DataBind();
                ddlStream1.SelectedValue = "0";
            }


            //Load Customer Type Ledger For Tagging
            string strParams = Session["CompanyId"].ToString() + chr.ToString();
            strParams += Session["FinYrID"].ToString() + chr.ToString();
            strParams += Session["BranchId"].ToString() + chr.ToString();
            strParams += Session["DataFlow"].ToString();

            DataView dv = new DataView(gf.GetDropDownColumnsBySP("spSelect_MstGeneralLedgerFull", strParams));
            dv.RowFilter = "LedgerType IN ('CUST')";
            if (dv != null)
            {
                ddlAssestLedger.DataSource = dv;
                ddlAssestLedger.DataBind();

                ddlIncomeLedger.DataSource = dv;
                ddlIncomeLedger.DataBind();
            }

            ddlAssestLedger.Items.Insert(0, li);
            ddlIncomeLedger.Items.Insert(0, li);

        }

        private void populatefees()
        {
            //--------------------------------------------------------
            panelfeesDetails1.Controls.Add(new LiteralControl("<table style='width:100%;border-collapse:collapse;'  rules='all'>"));
            panelfeesDetails1.Controls.Add(new LiteralControl("<tr class='HeaderStyle'>"));
            panelfeesDetails1.Controls.Add(new LiteralControl("<th scope='col'>"));
            panelfeesDetails1.Controls.Add(new LiteralControl(" Fees"));
            panelfeesDetails1.Controls.Add(new LiteralControl("</th>"));

            if (courseType == 1 || courseType == 3)
            {
                headerCount = 4;
                width = 80;
            }
            else
            {
                headerCount = 8;
                width = 50;
            }
            ////////CREATE HEADER .////////////////////////////
            createHeader(headerCount, width);
            panelfeesDetails1.Controls.Add(new LiteralControl("</tr>"));
            ////////END HEADER .////////////////////////////
            int intItemCount = 0;
            string strFeesName = "";

            BusinessLayer.Student.StreamGroup stremGrp = new BusinessLayer.Student.StreamGroup();
            Entity.Student.StreamGroup estremGrp = new Entity.Student.StreamGroup();

            //estremGrp.co = courseType;
            estremGrp.intMode = 1;
            estremGrp.stream_type_id = Convert.ToInt32(ddlStream1.SelectedValue.ToString());
            estremGrp.batch_ID = Convert.ToInt32(ddlBatch.SelectedValue.ToString());
            estremGrp.courseID = Convert.ToInt32(ddlCourse1.SelectedValue.ToString());
            estremGrp.FeesName = txtFeesName.Text.Trim();
            //estremGrp.stream_type_i

            dt = new DataTable();
            dt = stremGrp.GetFeesHead(estremGrp);

            if (dt.Rows.Count > 0)
            {
                intItemCount = dt.Rows.Count;
                for (int i = 0; i < intItemCount; i++)
                {
                    strFeesName = dt.Rows[i]["fees"].ToString();
                    int item = Convert.ToInt32(dt.Rows[i]["id"].ToString());
                    panelfeesDetails1.Controls.Add(new LiteralControl("<tr class='RowStyle'>"));
                    createBody(headerCount, width, item, strFeesName, dt, i);
                    panelfeesDetails1.Controls.Add(new LiteralControl("</tr>"));
                }
            }
            panelfeesDetails1.Controls.Add(new LiteralControl("</table>"));
        }

        private void createBody(int headerCount, int width, int item, string strFeesName, DataTable dt, int Pevi)
        {
            panelfeesDetails1.Controls.Add(new LiteralControl("<td>"));
            panelfeesDetails1.Controls.Add(new LiteralControl(strFeesName));
            panelfeesDetails1.Controls.Add(new LiteralControl("</td>"));
            for (int i = 0; i < headerCount; i++)
            {
                panelfeesDetails1.Controls.Add(new LiteralControl("<td scope='col' style='width: " + width + "px'>"));
                TextBox text = new TextBox();
                text.ID = "txtFees_" + item.ToString() + "_" + i.ToString();
                text.Width = width;
                text.Text = dt.Rows[Pevi]["sem" + (i + 1).ToString()].ToString();
                text.Attributes.Add("onKeyPress", "return NumbersOnly(event)");
                text.Attributes.Add("onblur", "return isNumber(event)");
                panelfeesDetails1.Controls.Add(text);
                panelfeesDetails1.Controls.Add(new LiteralControl("</td>"));
            }

        }

        private void createHeader(int headerCount, int width)
        {
            for (int i = 0; i < headerCount; i++)
            {
                panelfeesDetails1.Controls.Add(new LiteralControl("<th scope='col' style='width: " + width + "px'>"));
                panelfeesDetails1.Controls.Add(new LiteralControl(GetSemName(i + 1)));
                panelfeesDetails1.Controls.Add(new LiteralControl("</th>"));
            }
        }

        private string GetSemName(int SemNo)
        {
            string ReturnValue = "";
            switch (SemNo)
            {
                case 1: ReturnValue = "1st Sem"; break;
                case 2: ReturnValue = "2nd Sem"; break;
                case 3: ReturnValue = "3rd Sem"; break;
                case 4: ReturnValue = "4th Sem"; break;
                case 5: ReturnValue = "5th Sem"; break;
                case 6: ReturnValue = "6th Sem"; break;
                case 7: ReturnValue = "7th Sem"; break;
                case 8: ReturnValue = "8th Sem"; break;
            }
            return ReturnValue;
        }

        protected void btnStramSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Student.StreamGroup stremGrp = new BusinessLayer.Student.StreamGroup();
            Entity.Student.StreamGroup estremGrp = new Entity.Student.StreamGroup();

            courseType = Convert.ToInt32(hidCourse_id.Value.ToString());

            //--------------------------------------------------------
            if (intHeaderID == 0)
            {
                estremGrp.intMode = 4;
            }
            else
            {
                estremGrp.intMode = 12;
            }
            estremGrp.stream_type_id = Convert.ToInt32(ddlStream1.SelectedValue.ToString());
            estremGrp.batch_ID = Convert.ToInt32(ddlBatch.SelectedValue.ToString());
            estremGrp.courseID = Convert.ToInt32(ddlCourse1.SelectedValue.ToString());
            estremGrp.FeesName = txtFeesName.Text.Trim();
            estremGrp.feesID = intHeaderID;


            int IntLastInsertedID = stremGrp.SaveHeader(estremGrp);
            //-------------------------------------------------------
            if (IntLastInsertedID > 0)
            {
                stremGrp = new BusinessLayer.Student.StreamGroup();
                estremGrp = new Entity.Student.StreamGroup();
                estremGrp.intMode = 1;
                estremGrp.stream_type_id = Convert.ToInt32(ddlStream1.SelectedValue.ToString());
                estremGrp.batch_ID = Convert.ToInt32(ddlBatch.SelectedValue.ToString());
                estremGrp.courseID = Convert.ToInt32(ddlCourse1.SelectedValue.ToString());


                dt = new DataTable();
                dt = stremGrp.GetFeesHead(estremGrp);

                if (dt.Rows.Count > 0)
                {
                    if (courseType == 1 || courseType == 3)
                    {
                        headerCount = 4;
                    }
                    else
                    {
                        headerCount = 8;
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < headerCount; j++)
                        {
                            string tdId = "txtFees_" + dt.Rows[i]["id"].ToString() + "_" + j.ToString();
                            TextBox tx = (TextBox)panelfeesDetails1.FindControl(tdId);
                            if (tx.Text.Length == 0)
                            {
                                tx.Text = "0";
                            }
                            stremGrp = new BusinessLayer.Student.StreamGroup();
                            estremGrp = new Entity.Student.StreamGroup();
                            estremGrp.intMode = 5;
                            estremGrp.feesID = Convert.ToInt32(dt.Rows[i]["id"].ToString());
                            estremGrp.feesHeaderID = IntLastInsertedID;
                            estremGrp.column_name = j + 1;
                            estremGrp.column_value = Convert.ToInt32(tx.Text.ToString().Trim());
                            int IntID1 = stremGrp.SaveDetails(estremGrp);

                        }
                    }
                }

                Message.IsSuccess = true;
                Message.Text = "Fees Information Saved Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can't Save. Duplicate Fees Information Is Not Allowed. This Fees Head Already Exists For This Batch-Course-Stream.";
            }
            Message.Show = true;
            //int aa = panelfeesDetails1.Controls.Count;
        }
        protected void ddlCourse1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BusinessLayer.Student.StreamGroup stremGrp = new BusinessLayer.Student.StreamGroup();
            Entity.Student.StreamGroup estremGrp = new Entity.Student.StreamGroup();

            courseType = Convert.ToInt32(ddlCourse1.SelectedValue.ToString());
            estremGrp.intCompanyId = Convert.ToInt32(Session["CompanyId"].ToString());
            estremGrp.intMode = 3;
            estremGrp.courseID = Convert.ToInt32(ddlCourse1.SelectedValue.ToString());

            DataSet ds = new DataSet();
            ds = stremGrp.GetLoad(estremGrp);
            if (ds.Tables.Count > 0)
            {
                ddlStream1.DataSource = ds.Tables[0];
                ddlStream1.DataTextField = "stream_name";
                ddlStream1.DataValueField = "StreamId";
                ddlStream1.DataBind();
                ddlStream1.SelectedValue = "0";
            }

            hidCourse_id.Value = ddlCourse1.SelectedValue.ToString();
            panelfeesDetails1.Controls.Clear();
            populatefees();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields(Form.Controls);
            Response.Redirect("Fees.aspx");
            //panelfeesDetails1.Controls.Clear();
        }

        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    hidCourse_id.Value = courseType.ToString();
        //    panelfeesDetails1.Controls.Clear();
        //    populatefees();
        //}

        protected void ddlStream1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelfeesDetails1.Controls.Clear();
            populatefees();
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelfeesDetails1.Controls.Clear();
            populatefees();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Student.StreamGroup stremGrp = new BusinessLayer.Student.StreamGroup();
            Entity.Student.StreamGroup estremGrp = new Entity.Student.StreamGroup();
            estremGrp.intMode = 6;
            estremGrp.strBatchName = txtBatchName.Text.Trim().ToString();

            string[] Date = txtStartDate.Text.Trim().Split('/');
            estremGrp.strStartDate = (Date[1].Trim() + "/" + Date[0].Trim() + "/" + Date[2].Trim() + " 00:00:00").ToString();
            Date = txtEndDate.Text.Trim().Split('/');
            estremGrp.strEndDate = (Date[1].Trim() + "/" + Date[0].Trim() + "/" + Date[2].Trim() + " 00:00:00").ToString();
            int RowsAffected = stremGrp.SaveBatch(estremGrp);
            if (RowsAffected != -1)
            {
                populateLoad();
                txtBatchName.Text = "";
                txtEndDate.Text = "";
                txtStartDate.Text = "";
                Message.Show = false;
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate Batch is Not Allowed.";
                Message.Show = true;
            }
        }

        protected void btnFeesHeadSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Student.StreamGroup stremGrp = new BusinessLayer.Student.StreamGroup();
            Entity.Student.StreamGroup estremGrp = new Entity.Student.StreamGroup();
            estremGrp.intMode = 9;
            estremGrp.feesID = 0; // Saving
            estremGrp.Fees = txtFeesHead.Text.Trim();
            estremGrp.FeesHeadType = rbtnListHeadType.SelectedValue.Trim();
            estremGrp.AssestLedgerID_FK = Convert.ToInt32(ddlAssestLedger.SelectedValue.Trim());
            estremGrp.IncomeLedgerID_FK = Convert.ToInt32(ddlIncomeLedger.SelectedValue.Trim());
            estremGrp.IsRefundable = ChkIsRefundable.Checked;
            estremGrp.IsOneTimeApplicable = ChkIsOneTimeApplicable.Checked;
            int RowsAffected = stremGrp.SaveFeesHead(estremGrp);
            //populateLoad();
            if (RowsAffected != -1)
            {
                panelfeesDetails1.Controls.Clear();
                populatefees();
                txtFeesHead.Text = "";
                rbtnListHeadType.SelectedValue = "SEM";
                ddlAssestLedger.SelectedIndex = 0;
                ddlIncomeLedger.SelectedIndex = 0;
                ChkIsRefundable.Checked = false;
                ChkIsOneTimeApplicable.Checked = false;
                Message.Show = false;
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate Fees Head is Not Allowed.";
                Message.Show = true;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            panelfeesDetails1.Controls.Clear();
            populatefees();
        }
    }
}