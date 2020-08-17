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

namespace CollegeERP.Common
{
    public partial class StreamMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.STREAM_MASTER))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }

                Message.Show = false;
                LoadCourse();
                LoadStreamList();
            }
        }

        protected void LoadCourse()
        {
            BusinessLayer.Student.BTechRegistration ObjRegistration = new BusinessLayer.Student.BTechRegistration();
            Entity.Student.BTechRegistration Registration = new Entity.Student.BTechRegistration();
            Registration.intMode = 2;
            Registration.CourseId = 0; // Course Id is not required to fetch all courses
            DataTable dt = ObjRegistration.GetAllCommonSP(Registration);
            DataView dv = new DataView(dt);
            dv.RowFilter = "CompanyId=" + Session["CompanyId"].ToString() + "or CompanyId = 0";
            if (dv != null)
            {
                ddlCourse.DataSource = dv;
                ddlCourse.DataBind();
            }
        }

        protected void LoadStreamList()
        {
            int CourseId = int.Parse(ddlCourse.SelectedValue.Trim());
            BusinessLayer.Student.StreamMaster ObjStream = new BusinessLayer.Student.StreamMaster();
            DataTable dt = ObjStream.GetAll(CourseId);
            DataView dv = new DataView(dt);
            dv.RowFilter = "CompanyId=" + Session["CompanyId"].ToString() + "or CompanyId = 0";
            if (dv != null)
            {
                dgvStream.DataSource = dv;
                dgvStream.DataBind();
            }
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStreamList();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Student.StreamMaster ObjStream = new BusinessLayer.Student.StreamMaster();
            Entity.Student.StreamMaster Stream = new Entity.Student.StreamMaster();
            Stream.CourseId = int.Parse(ddlCourse.SelectedValue.Trim());
            Stream.StreamName = txtStream.Text.Trim();
            Stream.Capacity = int.Parse(txtCapacity.Text.Trim());
            int RowsAffected = ObjStream.Save(Stream);
            if (RowsAffected != -1)
            {
                txtStream.Text = "";
                txtCapacity.Text = "";
                LoadStreamList();
                Message.IsSuccess = true;
                Message.Text = "Stream Added Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Stream Name Already Exists Under This Course";
            }
            Message.Show = true;
        }

        protected void dgvStream_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Capacity"))
            {
                int RowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                BusinessLayer.Student.StreamMaster ObjStream = new BusinessLayer.Student.StreamMaster();
                Entity.Student.StreamMaster Stream = new Entity.Student.StreamMaster();

                TextBox txtCap = (TextBox)dgvStream.Rows[RowIndex].FindControl("txtCap");
                Stream.StreamId = Convert.ToInt32(dgvStream.DataKeys[RowIndex].Value.ToString());
                Stream.Capacity = (txtCap.Text.Trim().Length == 0) ? 0 : int.Parse(txtCap.Text.Trim());
                ObjStream.ChangeCapacity(Stream);
                LoadStreamList();

                Message.IsSuccess = true;
                Message.Text = "Capacity Changed Successfully";
                Message.Show = true;

            }
        }

        protected void dgvStream_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((Button)e.Row.FindControl("btnChangeCapacity")).CommandArgument = e.Row.RowIndex.ToString();
            }
        }
    }
}
