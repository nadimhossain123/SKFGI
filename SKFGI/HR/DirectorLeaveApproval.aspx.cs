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
using System.Net.Mail;

namespace CollegeERP.HR
{
    public partial class DirectorLeaveApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.LEAVE_APPROVE_BY_DIRECTOR))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                Message.Show = false;
                LoadRequestList();
            }
        }

        protected void LoadRequestList()
        {
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
            DataTable dt = ObjLeave.GetAllForDirectorApproval(FromDate, ToDate);
            if (dt != null) 
            {
                dgvLeave.DataSource = dt;
                dgvLeave.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Message.Show = false;
            LoadRequestList();
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
           Save(true);
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            Save(false);
        }

        protected void Save(bool IsApproved)
        {
            DataTable DTLeave = new DataTable();
            DTLeave.Columns.Add("LeaveId", typeof(int));
            DTLeave.AcceptChanges();
            DataRow DR;

            foreach (GridViewRow GVR in dgvLeave.Rows)
            {
                if (GVR.RowType == DataControlRowType.DataRow)
                {
                    if (((CheckBox)GVR.FindControl("ChkSelect")).Checked == true)
                    {
                        DR = DTLeave.NewRow();
                        DR["LeaveId"] = int.Parse(dgvLeave.DataKeys[GVR.RowIndex].Values["LeaveId"].ToString());
                        DTLeave.Rows.Add(DR);
                        DTLeave.AcceptChanges();

                    }
                }
            }

            BusinessLayer.HR.Leave ObjLeave = new BusinessLayer.HR.Leave();
            ObjLeave.SaveDirectorApproval(DTLeave, IsApproved);
            Message.IsSuccess = true;
            Message.Text = "Leave Approval Saved Successfully";
            Message.Show = true;
            LoadRequestList();
            
        }

     
        protected void dgvLeave_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal LeaveTaken = decimal.Parse(dgvLeave.DataKeys[e.Row.RowIndex].Values["LeaveTaken"].ToString());
                if (dgvLeave.DataKeys[e.Row.RowIndex].Values["LeaveBalance"].ToString() != "") //It may Be NULL for Study Leave and Spl Leave and etc
                {
                    decimal LeaveBalance = decimal.Parse(dgvLeave.DataKeys[e.Row.RowIndex].Values["LeaveBalance"].ToString());
                    string title = "Leave Balance=" + LeaveBalance.ToString() + "  Leave Taken=" + LeaveTaken.ToString();
                    if (LeaveTaken > LeaveBalance)
                        ((Literal)e.Row.FindControl("ltrWarning")).Text = "<img src='../Images/warning.gif' width='25px' height='25px' title='" + title + "' />";
                }
            }
        }
    }
}
