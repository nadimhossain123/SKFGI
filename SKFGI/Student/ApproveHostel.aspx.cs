using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CollegeERP.Student
{
    public partial class ApproveHostel : System.Web.UI.Page
    {
        public int id
        {
            get { return Convert.ToInt32(ViewState["id"]); }
            set { ViewState["id"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
                        
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.APPROVE_HOSTEL))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                Clearcontrols();
                LoadBatch();
                LoadCourse();
               // searchStudent();
            }
            
        }

        protected void Clearcontrols()
        {
            id = 0;
            Message.Show = false;
            
        }
        protected void LoadStudent()
        {
            BusinessLayer.Student.ApproveHostel objAH = new BusinessLayer.Student.ApproveHostel();
            DataSet ds = objAH.GetStudentDetails(0);
            DataView dv = new DataView(ds.Tables[0]);
            ltTotal.Text ="Total Hostel Alloted  :" + ds.Tables[1].Rows[0]["Total"].ToString();
            if (ddlBatch.SelectedIndex > 0 && ddlCourse.SelectedIndex==0 && ddlStream.SelectedIndex==0)
            {
                dv.RowFilter = "batch_id=" + int.Parse(ddlBatch.SelectedValue.Trim());
            }
            if (ddlBatch.SelectedIndex > 0 && ddlCourse.SelectedIndex > 0 )
            {
                dv.RowFilter = "batch_id=" + int.Parse(ddlBatch.SelectedValue.Trim()) + "And " + "CourseId=" + int.Parse(ddlCourse.SelectedValue);
            }
            if (ddlBatch.SelectedIndex > 0 && ddlCourse.SelectedIndex > 0 && ddlStream.SelectedIndex > 0)
            {
                dv.RowFilter = "batch_id=" + int.Parse(ddlBatch.SelectedValue.Trim())+ "And "  + "CourseId=" + int.Parse(ddlCourse.SelectedValue) + "And " + "StreamId=" + int.Parse(ddlStream.SelectedValue);
            }
            if (txtApplicantName.Text.Length > 0)
            {
                dv.RowFilter = "name like '" + this.txtApplicantName.Text + "%'";
            }

            if (ds.Tables.Count > 0)
            {
                dgvStudent.DataSource = dv;
                dgvStudent.DataBind();
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

        protected void LoadCourse()
        {
            BusinessLayer.Student.BTechRegistration ObjRegistration = new BusinessLayer.Student.BTechRegistration();
            Entity.Student.BTechRegistration Registration = new Entity.Student.BTechRegistration();
            Registration.intMode = 2;
            Registration.CourseId = 0; // Course Id is not required to fetch all courses
            DataTable dt = ObjRegistration.GetAllCommonSP(Registration);
            if (dt != null)
            {
                ddlCourse.DataSource = dt;
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

        //private void searchStudent()
        //{
        //    int CourseId = int.Parse(ddlCourse.SelectedValue.Trim());
        //    BusinessLayer.Student.SearchStudent searchS = new BusinessLayer.Student.SearchStudent();
        //    Entity.Student.SearchStudent esearchS = new Entity.Student.SearchStudent();

        //    esearchS.intMode = 1;
        //    esearchS.appliation_no = txtApplicationNo.Text.Trim();
        //    esearchS.name = txtApplicantName.Text.Trim();
        //    esearchS.CourseId = CourseId;
        //    esearchS.StreamId = int.Parse(ddlStream.SelectedValue.Trim());
        //    esearchS.batch_id = int.Parse(ddlBatch.SelectedValue.Trim());
        //    DataTable dt = new DataTable();
        //    dt = searchS.GetAllStudent(esearchS);
        //    if (dt != null)
        //    {
        //        dgvAllStudent.DataSource = dt;
        //        dgvAllStudent.DataBind();
                
        //    }
        //}     

       
        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStream();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //searchStudent();
            LoadStudent();
            Message.Show = false;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            BusinessLayer.Student.ApproveHostel objAH = new BusinessLayer.Student.ApproveHostel();
            string ApproveHostelListXML = String.Empty;
            DataTable DT = new DataTable();
            DT.Columns.Add("StudentId", typeof(int));
            DT.Columns.Add("IsHostelApproved", typeof(int));

            DataRow DR;

            foreach (GridViewRow GVR in dgvStudent.Rows)
            {
                CheckBox cbrowSelect = (CheckBox)GVR.FindControl("ChkSelect");
               
                    if (GVR.RowType == DataControlRowType.DataRow)
                    {

                        DR = DT.NewRow();
                        DR["StudentId"] = Convert.ToInt32(dgvStudent.DataKeys[GVR.RowIndex].Value.ToString());
                        if (cbrowSelect.Checked == true)
                        {
                            DR["IsHostelApproved"] = "1";
                        }
                        else
                        {
                            DR["IsHostelApproved"] = "0";
                        }
                        DT.Rows.Add(DR);
                        DT.AcceptChanges();

                }
            }

            using (DataSet ds = new DataSet())
            {
                ds.Tables.Add(DT);
                ApproveHostelListXML = ds.GetXml().Replace("Table1>", "Table>");
                //CreditBill.CreditBillXML = ds.GetXml().Replace("Table1>", "Table>");
            }

            int recCount = objAH.UpdateHostelApproveList(ApproveHostelListXML);

            if (recCount > 0)
            {
                Message.IsSuccess = true;
                Message.Text = "Updated Successfully";
                Message.Show = true;
                LoadStudent();
            }

        }
        protected void dgvStudent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvStudent.PageIndex = e.NewPageIndex;
            LoadStudent();
        }
       

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clearcontrols();
        }

        protected void btnAllotHostel_Click(object sender, EventArgs e)
        {
        //    Entity.Student.ApproveHostel objAhE = new Entity.Student.ApproveHostel();
        //     objAhE. StudentId=
        //public string Hosteldetail { get; set; }
        //public DateTime HostelDate { get; set; }
        //public int IsHostelApproved { get; set; }
        //public int IsHostelRelease { get; set; }

        }
        protected void dgvStudent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName=="Approve")
            {
                 int index = Convert.ToInt32(e.CommandArgument.ToString());
                
                 TextBox txtDate = (TextBox)dgvStudent.Rows[index].FindControl("txtDateFrom");
                 TextBox txtHostel = (TextBox)dgvStudent.Rows[index].FindControl("txtHostelRoomNo");
                 if (txtDate.Text.Trim() == "")
                 {
                     Message.IsSuccess = false;
                     Message.Text = "Please Enter date";
                     Message.Show = true;
                     LoadStudent();
                 }
                 else
                 {
                     Entity.Student.ApproveHostel objAHE = new Entity.Student.ApproveHostel();

                     BusinessLayer.Student.ApproveHostel objAHB = new BusinessLayer.Student.ApproveHostel();
                     objAHE.StudentId = int.Parse(dgvStudent.DataKeys[index].Value.ToString());
                     objAHE.Hosteldetail = txtHostel.Text;
                     objAHE.HostelDate = Convert.ToDateTime(txtDate.Text.Trim() + " 00:00:00");
                     objAHE.IsHostelApproved = 1;
                     objAHE.IsHostelRelease = 0;
                     int recCount = objAHB.UpdateHostelStudentApproveById(objAHE);
                     if (recCount > 0)
                     {
                         Message.IsSuccess = true;
                         Message.Text = "Updated Successfully";
                         Message.Show = true;
                         LoadStudent();
                     }
                 }
               // ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('" + objAHE.HostelDate + "');", true);

            }
            if (e.CommandName == "Release")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                TextBox txtDate = (TextBox)dgvStudent.Rows[index].FindControl("txtDateFrom");

                TextBox txtHostel = (TextBox)dgvStudent.Rows[index].FindControl("txtHostelRoomNo");
                Entity.Student.ApproveHostel objAHE = new Entity.Student.ApproveHostel();
                BusinessLayer.Student.ApproveHostel objAHB = new BusinessLayer.Student.ApproveHostel();
                objAHE.StudentId = int.Parse(dgvStudent.DataKeys[index].Value.ToString());
                objAHE.Hosteldetail = txtHostel.Text;
                objAHE.HostelDate = Convert.ToDateTime(txtDate.Text.Trim() + " 00:00:00");
                objAHE.IsHostelRelease = 1;
                int recCount = objAHB.UpdateHostelStudentReleaseById(objAHE);

                if (recCount > 0)
                {
                    Message.IsSuccess = true;
                    Message.Text = "Updated Successfully";
                    Message.Show = true;
                    LoadStudent();
                }
               //  ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('" + objAHE.Hosteldetail + "');", true);

            }
        }

       
        protected void btnReleaseHostel_Click(object sender, EventArgs e)
        { 

        }

        protected void dgvStudent_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        

      
         }
}