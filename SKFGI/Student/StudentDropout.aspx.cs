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

namespace CollegeERP.Student
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_DROPOUT))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                ClearControls();
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

                ddlStudent.DataSource = dt;
                ddlStudent.DataBind();

            }
        }
        protected void PopulateBatch()
        { 
              BusinessLayer.Student.StreamGroup stremGrp = new BusinessLayer.Student.StreamGroup();
            Entity.Student.StreamGroup estremGrp = new Entity.Student.StreamGroup();

            estremGrp.intCompanyId = Convert.ToInt32(Session["CompanyId"].ToString());
            estremGrp.intMode = 2;
           
            DataSet ds = new DataSet();
            ds = stremGrp.GetLoad(estremGrp);
            if (ds.Tables.Count > 0)
            {
                ddlBatch.DataSource = ds.Tables[0];
                ddlBatch.DataTextField = "batch_name";
                ddlBatch.DataValueField = "id";
                ddlBatch.DataBind();
                ddlBatch.SelectedValue = "0";
            }
        }
        protected void ClearControls()
        {
            
            //Message.Show = false;
            btnSave.Text = "Save";
            txtDropOutDate.Text = "";
            LoadActiveStudent();
            ddlStudent.SelectedIndex = 0;
            ddlStudent.Enabled = true;
           

            txtReason.Text = "";
            Message.Show=false;
            btnDownload.Visible = false;
            btnPrint.Visible = false;
            PopulateBatch();
           
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData(1) > 0)
            {
                ClearControls();
                Message.IsSuccess = true;
                Message.Text = "Student Information Saved Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can't Save. ";
            }
            Message.Show = true;
        }
        private int SaveData(int CallMode)
        {
            int RowsAffected = 0;

            if (ddlStudent.SelectedValue != "0" && ddlStudent.Text != string.Empty)
            {
                BusinessLayer.Student.StudentDropout objStu = new BusinessLayer.Student.StudentDropout();
                Entity.Student.StudentDropout objStuDropout = new Entity.Student.StudentDropout();
                objStuDropout.intMode = CallMode;
                objStuDropout.Student_Id = int.Parse(ddlStudent.SelectedValue.Trim());
                string[] Date = txtDropOutDate.Text.Trim().Split('/');
                objStuDropout.DODate =Convert.ToDateTime(txtDropOutDate.Text.Trim() + " 00:00:00");
                objStuDropout.Reason = txtReason.Text.Trim();
                objStuDropout.login_id=HttpContext.Current.User.Identity.Name;
                RowsAffected=objStu.Save(objStuDropout);

                
            }
            return RowsAffected;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void ddlStudent_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
           
            //DateTime Datefrom = Convert.ToDateTime(txtFromDate.Text.Trim() + " 00:00:00");              
            //DateTime DateTo = Convert.ToDateTime(txtTodate.Text.Trim() + " 00:00:00");
            BusinessLayer.Student.StudentDropout objStuDropOut = new BusinessLayer.Student.StudentDropout();
            int Batchid = int.Parse(ddlBatch.SelectedValue);
            DataTable dt = objStuDropOut.GetAll(Batchid);
           
            if (dt != null)
            {
                dgvReport.DataSource = dt;
                dgvReport.DataBind();

                btnDownload.Visible = (dt.Rows.Count > 0) ? true : false;
                btnPrint.Visible = (dt.Rows.Count > 0) ? true : false;

            }
            Message.Show = false;
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string[] _header = new string[2];
            _header[0] = "<b>Supreme Knowledge Foundation Group of Institutions</b>";
            _header[1] = "<p align='center'>Student Drop Out Report(" + ddlBatch.SelectedItem.Text + ")</p>";

            string[] _footer = new string[1];
            _footer[0] = " ";

            string file = "Student_DropOut_Report";
            dgvReport.Columns.RemoveAt(dgvReport.Columns.Count - 1);
            btnShow_Click(sender, e);
            dgvReport.DataBind();
            BusinessLayer.Common.Excel.SaveExcel(_header, dgvReport, _footer, file);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string Title = "<b>Supreme Knowledge Foundation Group of Institutions</b>";
            string[] _header = new string[3];
            _header[0] = "For Batch " + ddlBatch.SelectedItem.Text ;
            _header[1] = "Printed on " + DateTime.Now.ToString();
            _header[2] = "";

            string[] _footer = new string[1];
            _footer[0] = "";

            dgvReport.Columns[dgvReport.Columns.Count - 1].Visible = false;
            CollegeERP.Accounts.Print.ReportPrint(Title, _header, dgvReport, _footer);
            Response.Redirect("~/Accounts/RPTShowGrid.aspx");
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }


        protected void dgvReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("DeleteDropout"))
            {
                int Id = Convert.ToInt32(e.CommandArgument.ToString());
                BusinessLayer.Student.StudentDropout objStudentDropout = new BusinessLayer.Student.StudentDropout();
                objStudentDropout.Delete(Id);

                btnShow_Click(sender, e);
                Message.IsSuccess = true;
                Message.Text = "Student activated successfully";
                Message.Show = true;
            }
        }
    }
}
