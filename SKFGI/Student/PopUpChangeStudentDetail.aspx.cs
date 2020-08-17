using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace CollegeERP.Common
{
    public partial class PopUpChangeStudentDetail : System.Web.UI.Page
    {
        public int Id
        {
            get { return Convert.ToInt32(ViewState["Id"]); }
            set { ViewState["Id"] = value; }
        }
        public int Course_id
        {
            get { return Convert.ToInt32(ViewState["Course_id"]); }
            set { ViewState["Course_id"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Message.Show = false;
               
                if (Request.QueryString["Id"] != null && Request.QueryString["Id"].Trim().Length > 0)
                {
                    Id = Convert.ToInt32(Request.QueryString["Id"].Trim());
                    Course_id = Convert.ToInt32(Request.QueryString["Course_id"].Trim());
                    //btnAddCategory.Attributes.Add("onclick", "javascript:return openpopup('CategoryMaster.aspx');");
                    //btnDepartmentAdd.Attributes.Add("onclick", "javascript:return openpopup('DepartmentMaster.aspx');");
                    //btnAddDesignation.Attributes.Add("onclick", "javascript:return openpopup('DesignationMaster.aspx');");
                    //btnAddBranch.Attributes.Add("onclick", "javascript:return openpopup('BranchMaster.aspx');");
                    // btnAddGrade.Attributes.Add("onclick", "javascript:return openpopup('GradeMaster.aspx');");
                   // LoadEmployeeDetails();
                    //LoadEmployeeOfficialDetails();
                    LoadStream();
                    LoadFeeStructure();
                    LoadStudentBatch();
                }
            }
          

        }
        protected void LoadStream()
        {
            BusinessLayer.Student.SearchStudent objSearchStudent = new BusinessLayer.Student.SearchStudent();

            DataTable dt = objSearchStudent.GetStudentStream(Course_id);
            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = "0";
                dr["stream_name"] = "---Select---";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                 ddlStream .DataSource = dt;
                 ddlStream.DataBind();
                 ddlStream.SelectedValue =Convert.ToString( BusinessLayer.Student.SearchStudent.getStreamId(Id));
            }
        }
        protected void LoadFeeStructure()
        {
            BusinessLayer.Student.SearchStudent objSearchStudent = new BusinessLayer.Student.SearchStudent();

            DataTable dt = objSearchStudent.GetStudentFees(Id);
            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = "0";
                dr["fees_name"] = "---Select---";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                ddlFeeStructure.DataSource = dt;
                ddlFeeStructure.DataBind();
                ddlFeeStructure.SelectedValue = Convert.ToString(BusinessLayer.Student.SearchStudent.getFeesId(Id));
                //ddlStream.SelectedValue = Convert.ToString(BusinessLayer.Student.SearchStudent.getStreamId(Id));
            }
        }
        protected void LoadStudentBatch()
        {
            BusinessLayer.Student.SearchStudent objSearchStudent = new BusinessLayer.Student.SearchStudent();

            DataTable dt = objSearchStudent.GetStudentBatch(Id);
            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = "0";
                dr["batch_name"] = "---Select---";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();
                ddlBatch.DataSource = dt;
                ddlBatch.DataBind();
                
                ddlBatch.SelectedValue = Convert.ToString(DataAccess.student.SearchStudent.getBatchId(Id));
                //ddlStream.SelectedValue = Convert.ToString(BusinessLayer.Student.SearchStudent.getStreamId(Id));
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (BusinessLayer.Student.SearchStudent.Update(Id, int.Parse(ddlStream.SelectedValue.Trim()), int.Parse( ddlBatch.SelectedValue.Trim())) > Convert.ToUInt32("0"))
            {
                Message.IsSuccess = true;
                Message.Text = "Student Information Updated Successfully";
            }
            else 
            {
                Message.IsSuccess = false;
                Message.Text = "Can't Update.";
            }
            Message.Show = true;

        }

        protected void btnSaveFees_Click(object sender, EventArgs e)
        {

            if (BusinessLayer.Student.SearchStudent.UpdateFeesStructure(Id, int.Parse(ddlFeeStructure.SelectedValue.Trim())) > Convert.ToUInt32("0"))
            {
                Message.IsSuccess = true;
                Message.Text = "Student Information Updated Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can't Update. Student Not Approved";
            }
            Message.Show = true;
        }
    }
}
