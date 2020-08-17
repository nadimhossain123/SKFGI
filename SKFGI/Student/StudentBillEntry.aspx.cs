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
    public partial class StudentBillEntry : System.Web.UI.Page
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
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_SINGLE_BILL_ENTRY))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                Message.Show = false;
                LoadSemFeesHead();
              
                LoadBatch();
                // LoadStream();
                LoadCourse();
                LoadStudent(0, 0, 0);
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
        private void blankgv()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("StudentName");
            DataRow dr;
            dr = dt.NewRow();
            dr["id"] = " ";
            dr["StudentName"] = "";
            dt.Rows.InsertAt(dr, 0);
        }

        protected void LoadStudent(int BatchId, int CourseId, int StreamId)
        {
            DataAccess.student.LibraryFine objDFine = new DataAccess.student.LibraryFine();
            BusinessLayer.Student.LibraryFine ObjFine = new BusinessLayer.Student.LibraryFine();
            DataView DV = new DataView(objDFine.GetApprovedStudentListWithDropOut());
            DV.RowFilter = "batch_id=" + int.Parse(ddlBatch.SelectedValue.Trim()) + "AND " + "company_id=" + Convert.ToInt32(Session["CompanyId"].ToString());

           

            if (DV != null)
            {
                //ddlStudent.DataSource = DV;
                //ddlStudent.DataBind();
                gvStudent.DataSource = DV;
                gvStudent.DataBind();
            }
            else
            {

                blankgv();
            }
            //ddlStudent.Items.Insert(0, li);
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            //if (ddlStudent.SelectedValue != "0" && ddlStudent.Text != string.Empty)
            //{
            string strValues = txtBillDate.Text;
                strValues += chr.ToString() + "";
                DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

                if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() == Session["FinYrID"].ToString().Trim())
                {
                    BusinessLayer.Student.StudentBillEntry objSingleBillB = new BusinessLayer.Student.StudentBillEntry();
                    Entity.Student.StudentBillEntry objSingleBillE = new Entity.Student.StudentBillEntry();
                    objSingleBillE.CompanyID_FK = Convert.ToInt32(Session["CompanyId"].ToString().Trim());
                    objSingleBillE.BranchID_FK = Convert.ToInt32(Session["BranchId"].ToString().Trim());
                    objSingleBillE.FinYearID_FK = Convert.ToInt32(Session["FinYrID"].ToString().Trim());
                    objSingleBillE.DataFlow = Convert.ToInt32(Session["DataFlow"].ToString().Trim());
                    //objSingleBillE.StudentId = Convert.ToInt32(ddlStudent.SelectedValue.Trim());
                    objSingleBillE.SemNo = Convert.ToInt32(ddlSemester.SelectedValue.Trim());
                    objSingleBillE.BillAmount = Convert.ToDecimal(txtTotalAmt.Text.Trim());
                    objSingleBillE.CreatedBy = Convert.ToInt32(Session["UserId"].ToString().Trim());
                    objSingleBillE.BillDate = Convert.ToDateTime(txtBillDate.Text.Trim() + " 00:00:00");

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
                        objSingleBillE.SingleBillXML = ds.GetXml().Replace("Table1>", "Table>");
                    }
                    //***********************Student
                    DataTable DTS = new DataTable();
                    DTS.Columns.Add("StudentId", typeof(int));                    
                    DataRow DRS;
                    foreach (GridViewRow GVR in gvStudent.Rows)
                    {
                        if (GVR.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox cbrowSelect = (CheckBox)GVR.FindControl("ChkSelect");
                            
                            if (cbrowSelect.Checked == true)
                            {
                                DRS = DTS.NewRow();
                                DRS["StudentId"] = Convert.ToInt32(gvStudent.DataKeys[GVR.RowIndex].Value.ToString());
                                
                                DTS.Rows.Add(DRS);
                                DTS.AcceptChanges();
                            }
                        }
                    }

                    using (DataSet dss = new DataSet())
                    {
                        dss.Tables.Add(DTS);
                        objSingleBillE.StudentIdXML = dss.GetXml().Replace("Table1>", "Table>");
                    }
                    //******************************
                    int rowAffected = 0;
                    rowAffected= objSingleBillB.Save(objSingleBillE);
                    if (rowAffected > 0)
                    {
                        Message.IsSuccess = true;
                        Message.Text = "Bill Generated Successfully";

                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Bill Can Not Be Generated";
                    }
                    //-------------------------Add On 17-09-2013
                    LoadSemFeesHead();
                    //LoadStudent();
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
            //}
            //else
            //{
            //    Message.IsSuccess = false;
            //    Message.Text = "Please Select Student";
            //}
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
            //LoadStudent(ddlBatch.SelectedValue.ToString(),0,0);
            BusinessLayer.Student.LibraryFine ObjFine = new BusinessLayer.Student.LibraryFine();
            DataView dv = new DataView(ObjFine.GetNoyDropoutList());
            dv.RowFilter = "batch_id=" + int.Parse(ddlBatch.SelectedValue.Trim()) + "AND " + "company_id=" + Convert.ToInt32(Session["CompanyId"].ToString());

            if (dv != null)
            {
                gvStudent.DataSource = dv;
                gvStudent.DataBind();
            }
            ddlStudent.Items.Insert(0, liS);
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStream();
        }

        protected void ddlStream_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadStudent();
            BusinessLayer.Student.LibraryFine ObjFine = new BusinessLayer.Student.LibraryFine();
            DataView dv = new DataView(ObjFine.GetApprovedStudentList());
            dv.RowFilter = "batch_id=" + int.Parse(ddlBatch.SelectedValue.Trim()) + "AND " + "company_id=" + Convert.ToInt32(Session["CompanyId"].ToString()) + "AND " + "CourseId=" + int.Parse(ddlCourse.SelectedValue.Trim()) + "AND " + "StreamId=" + int.Parse(ddlStream.SelectedValue.Trim()) + " AND " + "active=1";

            if (dv != null)
            {
                gvStudent.DataSource = dv;
                gvStudent.DataBind();
            }
            ddlStudent.Items.Insert(0, liS);
        }

        protected void ddlStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["State"] = 0;
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
