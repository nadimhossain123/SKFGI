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

namespace CollegeERP.Student
{
    public partial class HostelFeesGeneration : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        ListItem li = new ListItem("---SELECT---", "0");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.HOSTEL_BILL_GENERATION))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadBatch();
                ClearControl();
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
        protected void LoadHostelFeesHead()
        {
            BusinessLayer.Student.StreamGroup objStreamGroup = new BusinessLayer.Student.StreamGroup();
            DataTable DT = objStreamGroup.GetAllHostelFeesHead();

            if (DT != null)
            {
                dgvFeesHead.DataSource = DT;
                dgvFeesHead.DataBind();
            }
        }

        protected void ClearControl()
        {
            Message.Show = false;
            dgvStudent.DataSource = null;
            dgvStudent.DataBind();

            ddlBatch.SelectedIndex = 0;
            ddlBatch.Enabled = true;
            LoadYear();
            ddlYear.SelectedIndex = 0;
            ddlYear.Enabled = true;
            ddlQuarter.SelectedIndex = 0;
            ddlQuarter.Enabled = true;
            LoadHostelFeesHead();
        }

        protected void LoadYear()
        {
            ddlYear.Items.Clear();
            ListItem liItem;

            if (ddlBatch.SelectedIndex > 0)
            {
                int Year = Convert.ToInt32(ddlBatch.SelectedItem.Text.Trim());
                for (int i = 0; i < 5; i++)
                {
                    liItem = new ListItem((Year + i).ToString(), (Year + i).ToString());
                    ddlYear.Items.Add(liItem);
                }
            }

            ddlYear.Items.Insert(0, li);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadYear();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BusinessLayer.Student.HostelFeesGeneration objHostelFees = new BusinessLayer.Student.HostelFeesGeneration();
            Entity.Student.HostelFeesGeneration Fees = new Entity.Student.HostelFeesGeneration();
            Fees.batch_id = Convert.ToInt32(ddlBatch.SelectedValue.Trim());
            Fees.CompanyID_FK = Convert.ToInt32(Session["CompanyId"].ToString().Trim());
            Fees.Year = Convert.ToInt32(ddlYear.SelectedValue.Trim());
            Fees.Quarter = Convert.ToInt32(ddlQuarter.SelectedValue.Trim());

            DataTable DT = objHostelFees.GetAllStudent(Fees);
            if (DT != null)
            {
                dgvStudent.DataSource = DT;
                dgvStudent.DataBind();
            }

            ddlBatch.Enabled = false;
            ddlYear.Enabled = false;
            ddlQuarter.Enabled = false;
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            string strValues = txtBillDate.Text;
            strValues += chr.ToString() + "";
            DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

            if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() == Session["FinYrID"].ToString().Trim())
            {
                BusinessLayer.Student.HostelFeesGeneration objHostelFees = new BusinessLayer.Student.HostelFeesGeneration();
                Entity.Student.HostelFeesGeneration Fees = new Entity.Student.HostelFeesGeneration();
                Fees.CompanyID_FK = Convert.ToInt32(Session["CompanyId"].ToString().Trim());
                Fees.BranchID_FK = Convert.ToInt32(Session["BranchId"].ToString().Trim());
                Fees.FinYearID_FK = Convert.ToInt32(Session["FinYrID"].ToString().Trim());
                Fees.DataFlow = Convert.ToInt32(Session["DataFlow"].ToString().Trim());
                Fees.Year = Convert.ToInt32(ddlYear.SelectedValue.Trim());
                Fees.Quarter = Convert.ToInt32(ddlQuarter.SelectedValue.Trim());
                Fees.CreatedBy = Convert.ToInt32(Session["UserId"].ToString().Trim());
                Fees.BillDate = Convert.ToDateTime( txtBillDate.Text.Trim() + " 00:00:00");

                DataTable DT = new DataTable();
                DT.Columns.Add("StudentId", typeof(int));
                DataRow DR;

                foreach (GridViewRow GVR in dgvStudent.Rows)
                {
                    if (GVR.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox Chk = (CheckBox)GVR.FindControl("ChkSelect");
                        if (Chk.Checked)
                        {
                            DR = DT.NewRow();
                            DR["StudentId"] = Convert.ToInt32(dgvStudent.DataKeys[GVR.RowIndex].Value.ToString());
                            DT.Rows.Add(DR);
                            DT.AcceptChanges();
                        }
                    }
                }

                using (DataSet ds = new DataSet())
                {
                    ds.Tables.Add(DT);
                    Fees.StudentIdXML = ds.GetXml().Replace("Table1>", "Table>");
                }

                //*************Hostel Fees Id*******************
                DataTable DTFee = new DataTable();
                DTFee.Columns.Add("fees_header_id", typeof(int));
                DTFee.Columns.Add("amount", typeof(decimal));
                DataRow DRFee;

                foreach (GridViewRow GVR in dgvFeesHead.Rows)
                {
                    if (GVR.RowType == DataControlRowType.DataRow)
                    {
                       
                            DRFee = DTFee.NewRow();
                            DRFee["fees_header_id"] = Convert.ToInt32(dgvFeesHead.DataKeys[GVR.RowIndex].Value.ToString());

                            TextBox txtAmount = (TextBox)GVR.FindControl("txtAmount");
                            //if (String.IsNullOrEmpty(txtAmount.Text))
                            //{
                            if (Convert.ToInt16(txtAmount.Text) > 0)
                            {
                                DRFee["amount"] = (txtAmount.Text.Trim().Length > 0) ? Convert.ToDecimal(txtAmount.Text.Trim()) : 0;
                                DTFee.Rows.Add(DRFee);
                                DTFee.AcceptChanges();
                            }
                            //}
                            //else
                            //{
                            //    Message.IsSuccess = false;
                            //    Message.Text = "Please Enter Amount";
                            //    Message.Show = true;
                                
                            //}
                    }
                }
                using (DataSet dsFee = new DataSet())
                {
                    dsFee.Tables.Add(DTFee);
                    Fees.feesdetailsxml = dsFee.GetXml().Replace("Table1>", "Table>");
                }
                //*******************End************************

                objHostelFees.GenerateFees(Fees);
                btnSearch_Click(sender, e);

                Message.IsSuccess = true;
                Message.Text = "Hostel Fees Generated Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Please select a Date Between " + Session["SesFromDate"].ToString() + " & " + Session["SesToDate"].ToString();
            }
            Message.Show = true;
        }

        protected void dgvFeesHead_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                ((TextBox)e.Row.FindControl("txtAmount")).Attributes.Add("onkeydown", "javascript:moveEnter(" + (e.Row.RowIndex + 1) + ");");
            }
        }

    }
}
