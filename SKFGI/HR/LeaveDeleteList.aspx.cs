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

namespace CollegeERP.HR
{
    public partial class LeaveDeleteList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.LEAVE_DELETE))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                //LoadLeaveStatus();
                LoadLeaveType();
                LoadRequestList();
                Message.Show = false;
                
            }
        }

        //protected void LoadLeaveStatus()
        //{
        //    BusinessLayer.HR.Leave ObjLeave = new BusinessLayer.HR.Leave();
        //    DataTable dt = ObjLeave.GetLeaveStatus();
        //    if (dt != null)
        //    {
        //        ddlStatus.DataSource = dt;
        //        ddlStatus.DataBind();
        //    }
        //    ddlStatus.Items.Insert(0, new ListItem("All", "0"));
        //}

        protected void LoadRequestList()
        {
            string FName = txtFName.Text.Trim();
            int LeaveStatusId = 2;//* (As only Approved Leave needed)int.Parse(ddlStatus.SelectedValue.Trim());
            int EmployeeId = int.Parse(HttpContext.Current.User.Identity.Name);
            int LeaveTypeId = int.Parse(ddlLeaveType.SelectedValue.Trim());

            string FromDate = txtFromDate.Text.Trim();
            if (FromDate.Length != 0)
            {
                string[] ArrFrom = FromDate.Split('/');
                FromDate = ArrFrom[1].Trim() + "/" + ArrFrom[0].Trim() + "/" + ArrFrom[2].Trim() + " 00:00:00";
            }
            string ToDate = txtToDate.Text.Trim();
            if (ToDate.Length != 0)
            {
                string[] ArrTo = ToDate.Split('/');
                ToDate = ArrTo[1].Trim() + "/" + ArrTo[0].Trim() + "/" + ArrTo[2].Trim() + " 23:59:59";
            }

            BusinessLayer.HR.Leave ObjLeave = new BusinessLayer.HR.Leave();
            DataTable dt = ObjLeave.GetLeaveDeleteList(FName,2, EmployeeId, FromDate, ToDate, LeaveTypeId); //EmployeeId= 0 to fetch all employees request under me
            if (dt != null) //LeaveManagerId=EmployeeId to fetch all leave request where leave manager is me
            {
                dgvLeave.DataSource = dt;
                dgvLeave.DataBind();
            }
        }

        protected void LoadLeaveType()
        {
            BusinessLayer.HR.LeaveType ObjType = new BusinessLayer.HR.LeaveType();
            DataTable dt = ObjType.GetAll();
            if (dt != null)
            {
                ddlLeaveType.DataSource = dt;
                ddlLeaveType.DataBind();
            }
            ddlLeaveType.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //if (txtFName.Text == "" && ddlLeaveType.SelectedValue == "0" && txtFromDate.Text == "" && txtToDate.Text == "")
            //{
                LoadRequestList();
                Message.Show = false;
            //}
            //else if (txtFName.Text.Trim().Length > 0)
            //{
            //    BusinessLayer.HR.Leave ObjLeave = new BusinessLayer.HR.Leave();
            //    DataTable dt = ObjLeave.GetLeaveDeleteList(FName, LeaveStatusId, 0, EmployeeId, FromDate, ToDate, LeaveTypeId); //EmployeeId= 0 to fetch all employees request under me
            //    DataView dv = dt.DefaultView;
            //    dv.RowFilter = "FullName Like '%" + txtFName.Text.Trim() + "'"; 
            //    if (dt != null) //LeaveManagerId=EmployeeId to fetch all leave request where leave manager is me
            //    {
            //        dgvLeave.DataSource = dv;
            //        dgvLeave.DataBind();
            //    }
            //}
        }

        protected void dgvLeave_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvLeave.PageIndex = e.NewPageIndex;
            LoadRequestList();
        }

        protected void dgvLeave_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int LeaveId = Convert.ToInt32(dgvLeave.DataKeys[e.Row.RowIndex].Value.ToString());
                //((ImageButton)e.Row.FindControl("btnEdit")).Attributes.Add("onclick", "javascript:openpopup('PopUpLeave.aspx?Id=" + LeaveId + "');");
               
            }
        }

        protected void dgvLeave_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int index = int.Parse(e.CommandArgument.ToString());
                int EmployeeId = int.Parse(dgvLeave.DataKeys[index]["EmployeeId"].ToString());
                int LeaveId = int.Parse(dgvLeave.DataKeys[index]["LeaveId"].ToString());
                int LeaveTypeId = int.Parse(dgvLeave.DataKeys[index]["LeaveTypeId"].ToString());
                Label lblDays =(Label) dgvLeave.Rows[index].FindControl("lblDays");
                decimal NoOfdays = decimal.Parse( lblDays.Text.ToString());
                BusinessLayer.HR.Leave leave = new BusinessLayer.HR.Leave();
                int rowAffected=leave.LeaveDelete( EmployeeId,LeaveId,LeaveTypeId,NoOfdays);
                if (rowAffected > 0)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('  ');", true);
                    Message.IsSuccess = true;
                    Message.Text = "Leave Deleted Successfully";
                    Message.Show = true;
                    LoadRequestList();

                }
            }
        }
    }
}
