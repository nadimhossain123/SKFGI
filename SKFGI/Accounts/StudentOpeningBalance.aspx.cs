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
using BusinessLayer.Student;

namespace CollegeERP.Accounts
{
    public partial class StudentOpeningBalance : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        ListItem li = new ListItem("---SELECT STUDENT---", "0");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_OPENING_BALANCE))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                ClearControls();
            }
        }

        protected void LoadFeesHead()
        {
            BusinessLayer.Student.StreamGroup obj = new BusinessLayer.Student.StreamGroup();
            DataTable DT = obj.GetAllFeesHead();

            if (DT != null)
            {
                dgvFeesHead.DataSource = DT;
                dgvFeesHead.DataBind();
            }
        }

        protected void LoadStudent()
        {
            BusinessLayer.Student.LibraryFine ObjFine = new BusinessLayer.Student.LibraryFine();
            DataView DV = new DataView(ObjFine.GetApprovedStudentList());
            string filter = "company_id=" + Convert.ToInt32(Session["CompanyId"].ToString());

            if (ddlBatch.SelectedIndex > 0)
                filter += " and batch_id=" + ddlBatch.SelectedValue;

            if (ddlCourse.SelectedIndex > 0)
                filter += " and CourseId=" + ddlCourse.SelectedValue;

            if (ddlStream.SelectedIndex > 0)
                filter += " and StreamId=" + ddlStream.SelectedValue;

            DV.RowFilter = filter;

            if (DV != null)
            {
                ddlStudent.DataSource = DV;
                ddlStudent.DataBind();
            }
            ddlStudent.Items.Insert(0, li);
        }

        protected void ClearControls()
        {
            LoadBatch();
            LoadCourse();
            LoadStudent();
            LoadFeesHead();
            ddlSemester.SelectedIndex = 0;
            Message.Show = false;
            txtTotalAmt.Text = "0.00";
        }

        private void LoadBatch()
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

        private void LoadCourse()
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

        private void LoadStream()
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

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStream();
            LoadStudent();
        }

        protected void ddlStream_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStudent();
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStudent();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int count = 0;
            if (ddlStudent.SelectedValue == "0" || ddlStudent.Text == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Select a Student";
                Message.Show = true;
            }
            else
            {
                string strValues = DateTime.Now.ToString("dd MMM yyyy");
                strValues += chr.ToString() + "";
                DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

                //if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() == Session["FinYrID"].ToString().Trim())
                //{
                BusinessLayer.Accounts.StudentOpeningBal objOpeningBal = new BusinessLayer.Accounts.StudentOpeningBal();
                Entity.Accounts.StudentOpeningBal OpBal = new Entity.Accounts.StudentOpeningBal();

                OpBal.CompanyID_FK = Convert.ToInt32(Session["CompanyId"].ToString().Trim());
                OpBal.BranchID_FK = Convert.ToInt32(Session["BranchId"].ToString().Trim());
                OpBal.FinYearID_FK = Convert.ToInt32(Session["FinYrID"].ToString().Trim());
                OpBal.StudentId = Convert.ToInt32(ddlStudent.SelectedValue.Trim());
                OpBal.SemNo = Convert.ToInt32(ddlSemester.SelectedValue.Trim());
                OpBal.BillAmount = Convert.ToDecimal(txtTotalAmt.Text.Trim());
                OpBal.CreatedBy = Convert.ToInt32(Session["UserId"].ToString().Trim());

                DataTable DT = new DataTable();
                DT.Columns.Add("FeesHeadId", typeof(int));
                DT.Columns.Add("AmountDr", typeof(decimal));
                DT.Columns.Add("AmountCr", typeof(decimal));
                DataRow DR;

                foreach (GridViewRow GVR in dgvFeesHead.Rows)
                {
                    if (GVR.RowType == DataControlRowType.DataRow)
                    {
                        TextBox txtAmount = (TextBox)GVR.FindControl("txtAmount");
                        DropDownList ddlDrCr = (DropDownList)GVR.FindControl("ddlDrCr");
                        decimal Amount = (txtAmount.Text.Trim().Length > 0) ? Convert.ToDecimal(txtAmount.Text.Trim()) : 0;

                        if (Amount > 0)
                        {
                            count++;
                            DR = DT.NewRow();
                            DR["FeesHeadId"] = Convert.ToInt32(dgvFeesHead.DataKeys[GVR.RowIndex].Value.ToString());
                            DR["AmountDr"] = (ddlDrCr.SelectedValue == "DR") ? Amount : 0;
                            DR["AmountCr"] = (ddlDrCr.SelectedValue == "CR") ? Amount : 0;
                            DT.Rows.Add(DR);
                            DT.AcceptChanges();
                        }
                    }
                }

                using (DataSet ds = new DataSet())
                {
                    ds.Tables.Add(DT);
                    OpBal.OpeningBalXML = ds.GetXml().Replace("Table1>", "Table>");
                }

                if (ViewState["BillId"] != null && ViewState["BillId"].ToString().Length > 0)
                {
                    OpBal.BillId = int.Parse(ViewState["BillId"].ToString());
                    ViewState.Remove("BillId");
                }
                else
                    OpBal.BillId = 0;

                if (count > 0)
                {
                    int RecCount = objOpeningBal.SaveOpBal(OpBal);
                    if (RecCount > 0)
                    {
                        Message.IsSuccess = true;
                        Message.Text = "Opening Balance Created Successfully";
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Opening Balance Allready Created";
                    }
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Please Enter Amount for Atleast One Head";
                }
                //}
                //else
                //{
                //    Message.IsSuccess = false;
                //    Message.Text = "Please select a Date Between " + Session["SesFromDate"].ToString() + " & " + Session["SesToDate"].ToString();
                //}           

                Message.Show = true;

            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void dgvFeesHead_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((TextBox)e.Row.FindControl("txtAmount")).Attributes.Add("onkeypress", "javascript:moveEnter(" + (e.Row.RowIndex + 1) + ");");
                ((DropDownList)e.Row.FindControl("ddlDrCr")).Attributes.Add("onchange", "javascript:TotalAmount();");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadFeesHead();
            txtTotalAmt.Text = "0.00";

            BusinessLayer.Accounts.StudentOpeningBal objOpeningBal = new BusinessLayer.Accounts.StudentOpeningBal();
            Entity.Accounts.StudentOpeningBal OpBal = new Entity.Accounts.StudentOpeningBal();

            OpBal.StudentId = int.Parse(ddlStudent.SelectedValue);
            OpBal.SemNo = int.Parse(ddlSemester.SelectedValue);

            DataSet ds = new DataSet();
            ds = objOpeningBal.StudentOpeningBalance_GetById(OpBal);

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                decimal dramt = Convert.ToDecimal(ds.Tables[0].Rows[0]["AmountDr"].ToString());
                decimal cramt = Convert.ToDecimal(ds.Tables[0].Rows[0]["AmountCr"].ToString());
                txtTotalAmt.Text = (dramt > cramt) ? Convert.ToString(dramt) : Convert.ToString((cramt * -1));
            }

            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                foreach (GridViewRow gvr in dgvFeesHead.Rows)
                {
                    TextBox txtAmount = (TextBox)gvr.FindControl("txtAmount");
                    DropDownList ddlDrCr = (DropDownList)gvr.FindControl("ddlDrCr");
                    int FeesHeadId = int.Parse(dgvFeesHead.DataKeys[gvr.RowIndex].Values[0].ToString());
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        if (FeesHeadId == int.Parse(dr["FeesHeadId"].ToString()))
                        {
                            decimal AmtDr = Convert.ToDecimal(dr["AmountDr"].ToString());
                            decimal AmtCr = Convert.ToDecimal(dr["AmountCr"].ToString());
                            txtAmount.Text = (AmtDr > AmtCr) ? Convert.ToString(AmtDr) : Convert.ToString(AmtCr);
                            ddlDrCr.SelectedValue = (AmtDr > AmtCr) ? "DR" : "CR";
                        }
                    }
                }
                ViewState["BillId"] = ds.Tables[0].Rows[0]["BillId"].ToString();
                btnSave.Text = "Update";
            }
        }


    }
}
