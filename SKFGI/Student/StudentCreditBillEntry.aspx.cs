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
    public partial class StudentCreditBillEntry : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);
        ListItem li = new ListItem("---SELECT STUDENT---", "0");
        ListItem liS = new ListItem(" ", "0");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_CREDIT_BILL_ENTRY))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                Message.Show = false;
                LoadSemFeesHead();
                LoadStudent();
                LoadBatch();
                // LoadStream();
                LoadCourse();
            }
        }

        protected void LoadSemFeesHead()
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
            DataAccess.student.LibraryFine objDFine = new DataAccess.student.LibraryFine();
            BusinessLayer.Student.LibraryFine ObjFine = new BusinessLayer.Student.LibraryFine();
            DataView DV = new DataView(objDFine.GetApprovedStudentListWithDropOut());
            DV.RowFilter = "company_id=" + Convert.ToInt32(Session["CompanyId"].ToString());

            if (DV != null)
            {
                ddlStudent.DataSource = DV;
                ddlStudent.DataBind();
            }
            ddlStudent.Items.Insert(0, li);
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            if (ddlStudent.SelectedValue != "0" && ddlStudent.Text != string.Empty)
            {
                string strValues = txtBillDate.Text;
                strValues += chr.ToString() + "";
                DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

                if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() == Session["FinYrID"].ToString().Trim())
                {
                    BusinessLayer.Student.StudentCreditBillEntry objCreditBill = new BusinessLayer.Student.StudentCreditBillEntry();
                    Entity.Student.StudentCreditBillEntry CreditBill = new Entity.Student.StudentCreditBillEntry();
                    CreditBill.CompanyID_FK = Convert.ToInt32(Session["CompanyId"].ToString().Trim());
                    CreditBill.BranchID_FK = Convert.ToInt32(Session["BranchId"].ToString().Trim());
                    CreditBill.FinYearID_FK = Convert.ToInt32(Session["FinYrID"].ToString().Trim());
                    CreditBill.DataFlow = Convert.ToInt32(Session["DataFlow"].ToString().Trim());
                    CreditBill.StudentId = Convert.ToInt32(ddlStudent.SelectedValue.Trim());
                    CreditBill.SemNo = Convert.ToInt32(ddlSemester.SelectedValue.Trim());
                    CreditBill.BillAmount = Convert.ToDecimal(txtTotalAmt.Text.Trim());
                    CreditBill.CreatedBy = Convert.ToInt32(Session["UserId"].ToString().Trim());
                    CreditBill.BillDate = Convert.ToDateTime( txtBillDate.Text.Trim() + " 00:00:00");

                    DataTable DT = new DataTable();
                    DT.Columns.Add("FeesHeadId", typeof(int));
                    DT.Columns.Add("Amount", typeof(decimal));
                    DataRow DR;

                    foreach (GridViewRow GVR in dgvFeesHead.Rows)
                    {
                        if (GVR.RowType == DataControlRowType.DataRow)
                        {
                            TextBox txtAmount = (TextBox)GVR.FindControl("txtAmount");
                            decimal Amount = (txtAmount.Text.Trim().Length > 0) ? Convert.ToDecimal(txtAmount.Text.Trim()) : 0;
                            if (Amount > 0)
                            {
                                DR = DT.NewRow();
                                DR["FeesHeadId"] = Convert.ToInt32(dgvFeesHead.DataKeys[GVR.RowIndex].Value.ToString());
                                DR["Amount"] = Amount;
                                DT.Rows.Add(DR);
                                DT.AcceptChanges();
                            }
                        }
                    }

                    using (DataSet ds = new DataSet())
                    {
                        ds.Tables.Add(DT);
                        CreditBill.CreditBillXML = ds.GetXml().Replace("Table1>", "Table>");
                    }

                    objCreditBill.Save(CreditBill);
                    Message.IsSuccess = true;
                    Message.Text = "Bill Generated Successfully";
                    //-------------------------Add On 17-09-2013
                    LoadSemFeesHead();
                    LoadStudent();
                    LoadBatch();
                    // LoadStream();
                    LoadCourse();
                    txtBillDate.Text = string.Empty;
                    txtTotalAmt.Text = "0.00";
                    ddlSemester.SelectedValue = "0";
                    ddlStudent.SelectedValue = "0";
                    //-------------------------
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Please select a Date Between " + Session["SesFromDate"].ToString() + " & " + Session["SesToDate"].ToString();
                }
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Please Select Student";
            }
            Message.Show = true;
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
            DataView dv = new DataView(ObjRegistration.GetAllCommonSP(Registration));
            dv.RowFilter = "CompanyId=" + Session["CompanyId"].ToString() + " Or CompanyId=0";
            if (dv != null)
            {
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
            LoadStudent();
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
            LoadStudent();
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
    }
}
