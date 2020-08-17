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
    public partial class ApproveStudent : System.Web.UI.Page
    {
        ListItem li = new ListItem("---SELECT---", "0");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            Message.Show = false;

            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.APPROVE_STUDENT))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                trCourse.Style["display"] = "none";
                trBooean.Style["display"] = "none";
                populateLoad();
                HidURL.Value = "#";
                LoadActiveStudent();
            }
        }


        private void populateLoad()
        {
            BusinessLayer.Student.ApproveStudent Aps = new BusinessLayer.Student.ApproveStudent();
            Entity.Student.ApproveStudent eAps = new Entity.Student.ApproveStudent();

            eAps.intMode = 1;

            DataSet ds = new DataSet();
            
            ds = Aps.PopulateLoadCombo(eAps);
            if (ds.Tables.Count > 0)
            {
                ddlBatch.DataSource = ds.Tables[0];
                ddlBatch.DataTextField = "batch_name";
                ddlBatch.DataValueField = "id";
                ddlBatch.DataBind();

                if (ddlBatch.Items.FindByText(DateTime.Now.Year.ToString()) != null)
                    ddlBatch.Items.FindByText(DateTime.Now.Year.ToString()).Selected = true;

                LoadHostelFees();

                DataView dv = new DataView(ds.Tables[1]);
                dv.RowFilter = "CompanyId=" + Session["CompanyId"].ToString() + "or CompanyId = 0";
                ddlCourse1.DataSource = dv;
                ddlCourse1.DataTextField = "CourseName";
                ddlCourse1.DataValueField = "CourseId";
                ddlCourse1.DataBind();
                //ddlCourse1.SelectedValue = "0";

                ddlStudent.DataSource = ds.Tables[2];
                ddlStudent.DataTextField = "name";
                ddlStudent.DataValueField = "id";
                ddlStudent.DataBind();
                ddlStudent.SelectedValue = "0";

                ddlFees.DataSource = ds.Tables[3];
                ddlFees.DataTextField = "fees_name";
                ddlFees.DataValueField = "id";
                ddlFees.DataBind();

                ddlCourse.DataSource = ds.Tables[1];
                ddlCourse.DataTextField = "CourseName";
                ddlCourse.DataValueField = "CourseId";
                ddlCourse.DataBind();
                ddlCourse.SelectedValue = "0";
            }
        }

        protected void ddlCourse1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BusinessLayer.Student.ApproveStudent Aps = new BusinessLayer.Student.ApproveStudent();
            Entity.Student.ApproveStudent eAps = new Entity.Student.ApproveStudent();

            eAps.intMode = 2;
            eAps.intBatchId = Convert.ToInt32(ddlBatch.SelectedValue.ToString());
            eAps.intCourseID = Convert.ToInt32(ddlCourse1.SelectedValue.ToString());

            DataSet ds = new DataSet();
            ds = Aps.GetAllStudent(eAps);
            if (ds.Tables.Count > 0)
            {
                ddlStudent.DataSource = ds.Tables[0];
                ddlStudent.DataTextField = "name";
                ddlStudent.DataValueField = "id";
                ddlStudent.DataBind();
                ddlStudent.SelectedValue = "0";

                ddlFees.DataSource = ds.Tables[1];
                ddlFees.DataTextField = "fees_name";
                ddlFees.DataValueField = "id";
                ddlFees.DataBind();
            }
            HidURL.Value = "#";
            LoadSeatBookingStatus();

        }

        protected void LoadSeatBookingStatus()
        {
            if (ddlBatch.SelectedValue != "0" && ddlCourse1.SelectedValue != "0")
            {
                ltrHeader.Text = ddlCourse1.SelectedItem.Text + " - " + ddlBatch.SelectedItem.Text;
                BusinessLayer.Student.ApproveStudent Aps = new BusinessLayer.Student.ApproveStudent();
                Entity.Student.ApproveStudent eAps = new Entity.Student.ApproveStudent();

                eAps.intMode = 8;
                eAps.intBatchId = Convert.ToInt32(ddlBatch.SelectedValue.ToString());
                eAps.intCourseID = Convert.ToInt32(ddlCourse1.SelectedValue.ToString());

                DataSet ds = new DataSet();
                ds = Aps.GetAllStudent(eAps);
                if (ds.Tables.Count > 0)
                {
                    dgvSeat.DataSource = ds.Tables[0];
                    dgvSeat.DataBind();
                }
            }
        }
        protected void LoadActiveStudent()
        {
            BusinessLayer.Student.LibraryFine ObjFine = new BusinessLayer.Student.LibraryFine();
            DataTable dt = ObjFine.GetApprovedStudentList();
            if (dt != null)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "company_id=" + Convert.ToInt32(Session["CompanyId"].ToString()) + " and active=1";

                dt = dv.ToTable();
                DataRow dr = dt.NewRow();
                dr["id"] = "0";
                dr["StudentName"] = "---Select Student---";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                ddlActiveStudent .DataSource = dt;
                ddlActiveStudent.DataBind();

            }
        }


        protected void ddlStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            trBooean.Style.Add("display", "");
            if (Convert.ToInt32(ddlStudent.SelectedValue) > 0)
            {

                tdStudentDetails.Style["display"] = "";
                if (Convert.ToInt32(ddlCourse1.SelectedValue) == 1)
                {
                    trCourse.Style["display"] = "";
                    //studentDetails11.Attributes.Add("onclick", "javascript:openpopup('MBARegistration.aspx','?id=" + ddlStudent.SelectedValue.ToString() + "',document.body.offsetHeight,document.body.offsetWidth);");
                    HidURL.Value = "MBARegistration.aspx?id=" + ddlStudent.SelectedValue.ToString() + "&type=view";
                }
                else if (Convert.ToInt32(ddlCourse1.SelectedValue) == 2)
                {
                    trCourse.Style["display"] = "";
                    //studentDetails11.Attributes.Add("onclick", "javascript:openpopup('BTechRegistration.aspx','?id=" + ddlStudent.SelectedValue.ToString() + "',document.body.offsetHeight',document.body.offsetWidth);");
                    HidURL.Value = "BTechRegistration.aspx?id=" + ddlStudent.SelectedValue.ToString() + "&type=view";
                }
                else if (Convert.ToInt32(ddlCourse1.SelectedValue) == 3)
                {
                    trCourse.Style["display"] = "";
                    //studentDetails11.Attributes.Add("onclick", "javascript:openpopup('BTechRegistration.aspx','?id=" + ddlStudent.SelectedValue.ToString() + "',document.body.offsetHeight',document.body.offsetWidth);");
                    HidURL.Value = "MTechRegistration.aspx?id=" + ddlStudent.SelectedValue.ToString() + "&type=view";
                }
                else if (Convert.ToInt32(ddlCourse1.SelectedValue) == 4)
                {
                    trCourse.Style["display"] = "";
                    //studentDetails11.Attributes.Add("onclick", "javascript:openpopup('BTechRegistration.aspx','?id=" + ddlStudent.SelectedValue.ToString() + "',document.body.offsetHeight',document.body.offsetWidth);");
                    HidURL.Value = "DiplomaRegistration.aspx?id=" + ddlStudent.SelectedValue.ToString() + "&type=view";
                }
                else if (Convert.ToInt32(ddlCourse1.SelectedValue) == 5)
                {
                    trCourse.Style["display"] = "";
                    //studentDetails11.Attributes.Add("onclick", "javascript:openpopup('BTechRegistration.aspx','?id=" + ddlStudent.SelectedValue.ToString() + "',document.body.offsetHeight',document.body.offsetWidth);");
                    HidURL.Value = "NonAICTERegistration.aspx?id=" + ddlStudent.SelectedValue.ToString() + "&type=view";
                }

            }
            else
            {
                tdStudentDetails.Style["display"] = "none";
                HidURL.Value = "#";
            }
            hidCourse_id.Value = ddlCourse1.SelectedValue;
            BusinessLayer.Student.ApproveStudent Aps = new BusinessLayer.Student.ApproveStudent();
            Entity.Student.ApproveStudent eAps = new Entity.Student.ApproveStudent();

            eAps.intMode = 3;
            eAps.intCourseID = Convert.ToInt32(ddlCourse1.SelectedValue.ToString());
            eAps.intStudentID = Convert.ToInt32(ddlStudent.SelectedValue.ToString());

            DataSet ds = new DataSet();
            ds = Aps.GetAllStudent(eAps);
            if (ds.Tables.Count > 0)
            {

                ds.Tables[0].Rows.RemoveAt(0);
                ds.Tables[0].AcceptChanges();
                chkStream.DataSource = ds.Tables[0];
                chkStream.DataBind();
                
                if (ds.Tables[1].Rows.Count > 0)
                {
                    ChkHostelFacility.Checked = Convert.ToBoolean(ds.Tables[1].Rows[0]["IsHostelFacility"].ToString());
                    ChkLateral.Checked = Convert.ToBoolean(ds.Tables[1].Rows[0]["IsLateral"].ToString());
                    ChkTFW.Checked = Convert.ToBoolean(ds.Tables[1].Rows[0]["TFW"].ToString());
                    selectStreamapplied(ds.Tables[1]);

                    ddlHostelFees.SelectedIndex=0;
                    if (ChkHostelFacility.Checked)
                        ddlHostelFees.Enabled = true;
                    else
                        ddlHostelFees.Enabled = false;
                }
            }


            if (Convert.ToInt32(ddlCourse1.SelectedValue) == 2 || Convert.ToInt32(ddlCourse1.SelectedValue)== 4)
            {
                ChkLateral.Visible = true;
                ChkTFW.Visible = true;
            }
            else
            {
                ChkLateral.Visible = false;
                ChkTFW.Visible = false;
            }
            
        }
        private void selectStreamapplied(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0 && chkStream.Items.Count > 0)
            {
                string[] appliedStream = dataTable.Rows[0]["Value"].ToString().Split(',');
                for (int j = 0; j < chkStream.Items.Count; j++)
                {
                    for (int i = 0; i < appliedStream.Length; i++)
                    {
                        if (chkStream.Items[j].Value == appliedStream[i].ToString())
                        {
                            chkStream.Items[j].Selected = true;
                        }
                    }
                }
            }
        }

        protected void btnStramSave_Click(object sender, EventArgs e)
        {
            clsGeneralFunctions gf = new clsGeneralFunctions();
            char chr = Convert.ToChar(130);
            string strValues = DateTime.Now.ToString("dd MMM yyyy");
            strValues += chr.ToString() + "";
            DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

            if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() == Session["FinYrID"].ToString().Trim())
            {
                if (save() > 0)
                {
                    Message.IsSuccess = true;
                    Message.Text = "Student Approved";
                    ddlCourse.SelectedValue = ddlCourse1.SelectedValue;
                    populateApprovedStudent();
                    ddlCourse1_SelectedIndexChanged(sender, e);
                    LoadSeatBookingStatus();
                }
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Please select a Date Between " + Session["SesFromDate"].ToString() + " & " + Session["SesToDate"].ToString();
            }
            Message.Show = true;
        }

        private int save()
        {
            BusinessLayer.Student.ApproveStudent Aps = new BusinessLayer.Student.ApproveStudent();
            Entity.Student.ApproveStudent eAps = new Entity.Student.ApproveStudent();

            eAps.intMode = 4;
            eAps.intStudentID = Convert.ToInt32(ddlStudent.SelectedValue.ToString());
            //if (Convert.ToInt32(ddlCourse1.SelectedValue) > 1)
            //{
            eAps.intStreamId = Convert.ToInt32(chkStream.SelectedValue.Trim());
            //}
            eAps.intFeesStructureID = Convert.ToInt32(ddlFees.SelectedValue.ToString());
            eAps.intupdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            eAps.IsHostelFacility = ChkHostelFacility.Checked;
            eAps.IsLateral = ChkLateral.Checked;
            eAps.TFW = ChkTFW.Checked;

            int CourseId = Convert.ToInt32(ddlCourse1.SelectedValue.Trim());
            if (CourseId == 1) // Means MBA
                eAps.CompanyID_FK = BusinessLayer.Common.Company.GetCompanyId("MBA");
            else if(CourseId == 4)
                eAps.CompanyID_FK=BusinessLayer.Common.Company.GetCompanyId("Diploma");
            else if(CourseId==2 || CourseId == 3)
                eAps.CompanyID_FK = BusinessLayer.Common.Company.GetCompanyId("Engineering");
            else if(CourseId==5)
                eAps.CompanyID_FK = BusinessLayer.Common.Company.GetCompanyId("NONAICTE");


            eAps.BranchID_FK = Convert.ToInt32(Session["BranchId"].ToString());
            eAps.FinYearID_FK = Convert.ToInt32(Session["FinYrID"].ToString());
            eAps.DataFlow = Convert.ToInt32(Session["DataFlow"].ToString());
            eAps.HostelFeesId = Convert.ToInt32(ddlHostelFees.SelectedValue.Trim());

            int RowsAffected = Aps.SaveDetails(eAps);
            return RowsAffected;
        }

        
        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadActiveStudent();
            populateApprovedStudent();
        }

        private void populateApprovedStudent()
        {
            BusinessLayer.Student.ApproveStudent Aps = new BusinessLayer.Student.ApproveStudent();
            Entity.Student.ApproveStudent eAps = new Entity.Student.ApproveStudent();

            eAps.intMode = 5;
            eAps.intCourseID = Convert.ToInt32(ddlCourse.SelectedValue.ToString());

            DataSet ds = new DataSet();
            ds = Aps.GetAllStudent(eAps);
            if (ds.Tables.Count > 0)
            {
                dgvAllStudent.DataSource = ds.Tables[0];
                dgvAllStudent.DataBind();
            }
        }

        protected void dgvAllStudent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //------------Add 06/08/2013--------------
                int IdP = Convert.ToInt32(dgvAllStudent.DataKeys[e.Row.RowIndex].Values["id"].ToString());
                int Course_id = Convert.ToInt32(dgvAllStudent.DataKeys[e.Row.RowIndex].Values["CourseId"].ToString());
                ((Image)e.Row.FindControl("ImgIDCard")).Attributes.Add("onclick", "javascript:openIDpopup('StudentIDCard.aspx?id=" + IdP + "');");
                //----------------------------------------

                int Id = Convert.ToInt32(dgvAllStudent.DataKeys[e.Row.RowIndex].Value.ToString());
                ((ImageButton)e.Row.FindControl("btnPrint")).Attributes.Add("onclick", "javascript:openpopup('printFeesVoucher.aspx?id=" + Id + "','','600','700');");

                if (Convert.ToInt32(ddlCourse.SelectedValue) == 2)
                    ((ImageButton)e.Row.FindControl("btnPrintForm")).Attributes.Add("onclick", "window.open('BTechRegistrationPrint.aspx?id=" + Id + "'); return false;");
                else if (Convert.ToInt32(ddlCourse.SelectedValue) == 1)
                    ((ImageButton)e.Row.FindControl("btnPrintForm")).Attributes.Add("onclick", "window.open('MBARegistrationPrint.aspx?id=" + Id + "'); return false;");
                else if (Convert.ToInt32(ddlCourse.SelectedValue) == 3)
                    ((ImageButton)e.Row.FindControl("btnPrintForm")).Attributes.Add("onclick", "window.open('MTechRegistrationPrint.aspx?id=" + Id + "'); return false;");
                else if (Convert.ToInt32(ddlCourse.SelectedValue) == 4)
                    ((ImageButton)e.Row.FindControl("btnPrintForm")).Attributes.Add("onclick", "window.open('DiplomaRegistrationPrint.aspx?id=" + Id + "'); return false;");
                else if (Convert.ToInt32(ddlCourse.SelectedValue) == 5)
                    ((ImageButton)e.Row.FindControl("btnPrintForm")).Attributes.Add("onclick", "window.open('NonAICTERegistrationPrint.aspx?id=" + Id + "'); return false;");
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            BusinessLayer.Student.ApproveStudent Aps = new BusinessLayer.Student.ApproveStudent();
            Entity.Student.ApproveStudent eAps = new Entity.Student.ApproveStudent();

            eAps.intMode = 7;
            eAps.intStudentID = Convert.ToInt32(ddlStudent.SelectedValue.ToString());
            eAps.intupdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            Aps.SaveDetails(eAps);

            Message.IsSuccess = true;
            Message.Text = "Student Rejected";
            Message.Show = true;
            populateApprovedStudent();
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadHostelFees();
            LoadSeatBookingStatus();
        }

        protected void dgvAllStudent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvAllStudent.PageIndex = e.NewPageIndex;
            populateApprovedStudent();
        }

        protected void LoadHostelFees()
        {
            int batch_id = Convert.ToInt32(ddlBatch.SelectedValue.Trim());
            BusinessLayer.Student.HostelFeesConfig objHostelFees = new BusinessLayer.Student.HostelFeesConfig();
            DataTable DT = objHostelFees.GetAll(batch_id);

            if (DT != null)
            {
                ddlHostelFees.DataSource = DT;
                ddlHostelFees.DataBind();
            }
            ddlHostelFees.Items.Insert(0, li);
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            if ( ddlActiveStudent.SelectedIndex != 0)
            {
            BusinessLayer.Student.ApproveStudent Aps = new BusinessLayer.Student.ApproveStudent();
            Entity.Student.ApproveStudent eAps = new Entity.Student.ApproveStudent();

            eAps.intMode = 9;
            eAps.intStudentID = int.Parse(ddlActiveStudent.SelectedValue.Trim());
            DataSet ds = new DataSet();
            ds = Aps.GetAllStudent(eAps);
            if (ds.Tables.Count > 0)
            {
               
                ddlCourse.SelectedValue = ds.Tables[0].Rows[0]["CourseId"].ToString();
              
                dgvAllStudent.DataSource = ds.Tables[0];
                dgvAllStudent.DataBind();
            }
            }
        }
    }
}