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
using System.Text;

namespace CollegeERP.HR
{
    public partial class DirectLeaveEntry : System.Web.UI.Page
    {
        public int EmployeeId
        {
            get { return Convert.ToInt32(ViewState["EmployeeId"]); }
            set { ViewState["EmployeeId"] = value; }
        }

        public string ErrorText { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.DIRECT_LEAVE_ENTRY))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }

                EmployeeId = int.Parse(HttpContext.Current.User.Identity.Name);
                LoadStockBalance();
                LoadLeaveStatus();
                LoadLeaveType_Search();
                LoadLeaveHistory();
                LoadLeaveTypeFirstTime();
                ClearControl();
                BindEmployee();

            }
        }

        private void LoadLeaveTypeFirstTime()
        {
            //int employeeidd = Convert.ToInt32(ddlEmployee.SelectedValue);
            BusinessLayer.HR.LeaveType ObjType = new BusinessLayer.HR.LeaveType();
            DataView dv = new DataView(ObjType.GetAll());
            StringBuilder sb = new StringBuilder();
            //DataTable dt = ObjType.GetAll();

            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            Entity.Common.Employee Employee = new Entity.Common.Employee();
            Employee = ObjEmployee.GetAllById(EmployeeId);

            //Delete Matermity Leave for Male employees
            if (Employee.Gender == "Male")
                sb.Append("LeaveTypeId <> 7 and "); //Delete matermity Leave Type
            else
                sb.Append("LeaveTypeId = LeaveTypeId and ");

            sb.Append("LeaveTypeId <> 2 and "); //Delete Half pay Leave

            BusinessLayer.Common.EmployeeOfficial ObjOfficial = new BusinessLayer.Common.EmployeeOfficial();
            Entity.Common.EmployeeOfficial Official = new Entity.Common.EmployeeOfficial();
            Official = ObjOfficial.GetAllByEmployeeId(EmployeeId);

            //Delete earned Leave for those employees whose working day is 5
            if (Official.WorkingDays == 5)
                sb.Append("LeaveTypeId <> 4"); //Delete earned Leave
            else
                sb.Append("LeaveTypeId = LeaveTypeId");

            dv.RowFilter = sb.ToString();
            ddlLeaveType.DataSource = dv;
            ddlLeaveType.DataBind();

            ddlLeaveType.Items.Insert(0, new ListItem("Select", "0"));
        }

        protected void LoadStockBalance()
        {
            BusinessLayer.HR.Leave ObjLeave = new BusinessLayer.HR.Leave();
            DataTable dt = ObjLeave.GetStockBalance(EmployeeId);
            ViewState["Stock"] = dt;
            if (dt != null)
            {
                dgvStock.DataSource = dt;
                dgvStock.DataBind();
            }
        }

        protected void LoadLeaveStatus()
        {
            BusinessLayer.HR.Leave ObjLeave = new BusinessLayer.HR.Leave();
            DataTable dt = ObjLeave.GetLeaveStatus();
            if (dt != null)
            {
                ddlStatus.DataSource = dt;
                ddlStatus.DataBind();
            }
            ddlStatus.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void LoadLeaveHistory()
        {
            BusinessLayer.HR.Leave ObjLeave = new BusinessLayer.HR.Leave();
            int LeaveStatusId = int.Parse(ddlStatus.SelectedValue.Trim());
            int LeaveTypeId = int.Parse(ddlLeaveType_Search.SelectedValue.Trim());
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

            DataTable dt = ObjLeave.GetAll("", LeaveStatusId, EmployeeId, 0, FromDate, ToDate, LeaveTypeId); // Leave ManagerId =0 for selecting all leave requests of me regardless of the Leave Manager
            if (dt != null)
            {
                dgvLeave.DataSource = dt;
                dgvLeave.DataBind();
            }
        }

        protected void ClearControl()
        {
            Message.Show = false;
            txtCreatedOn.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ddlLeaveType.SelectedIndex = 0;
            txtFrom.Text = "";
            txtTo.Text = "";
            ddlLeaveFormat.Visible = false;
            ChkIsAdjustment.Checked = false;
            txtPurpose.Text = "";
            ChkIsClassAdjusted.Checked = false;
            ChkIsExamDutyDuringLeave.Checked = false;
        }

        protected void LoadLeaveType()
        {
            int employeeidd = Convert.ToInt32(ddlEmployee.SelectedValue);
            BusinessLayer.HR.LeaveType ObjType = new BusinessLayer.HR.LeaveType();
            DataView dv = new DataView(ObjType.GetAll());
            StringBuilder sb = new StringBuilder();
            //DataTable dt = ObjType.GetAll();

            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            Entity.Common.Employee Employee = new Entity.Common.Employee();
            Employee = ObjEmployee.GetAllById(employeeidd);

            //Delete Matermity Leave for Male employees
            if (Employee.Gender == "Male")
                sb.Append("LeaveTypeId <> 7 and "); //Delete matermity Leave Type
            else
                sb.Append("LeaveTypeId = LeaveTypeId and ");

            sb.Append("LeaveTypeId <> 2 and "); //Delete Half pay Leave

            BusinessLayer.Common.EmployeeOfficial ObjOfficial = new BusinessLayer.Common.EmployeeOfficial();
            Entity.Common.EmployeeOfficial Official = new Entity.Common.EmployeeOfficial();
            Official = ObjOfficial.GetAllByEmployeeId(employeeidd);

            //Delete earned Leave for those employees whose working day is 5
            if (Official.WorkingDays == 5)
                sb.Append("LeaveTypeId <> 4"); //Delete earned Leave
            else
                sb.Append("LeaveTypeId = LeaveTypeId");

            dv.RowFilter = sb.ToString();
            ddlLeaveType.DataSource = dv;
            ddlLeaveType.DataBind();

            ddlLeaveType.Items.Insert(0, new ListItem("Select", "0"));
        }

        protected void LoadLeaveType_Search()
        {
            BusinessLayer.HR.LeaveType ObjType = new BusinessLayer.HR.LeaveType();
            DataTable dt = ObjType.GetAll();
            if (dt != null)
            {
                ddlLeaveType_Search.DataSource = dt;
                ddlLeaveType_Search.DataBind();
            }
            ddlLeaveType_Search.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadLeaveHistory();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
        }
            
        protected bool Validate()
        {
            bool result = true;
            DateTime FromDate;
            DateTime Todate;

            string[] From = txtFrom.Text.Trim().Split('/');
            FromDate = Convert.ToDateTime(From[1].Trim() + "/" + From[0].Trim() + "/" + From[2].Trim() + " 00:00:00");

            string[] To = txtTo.Text.Trim().Split('/');
            Todate = Convert.ToDateTime(To[1].Trim() + "/" + To[0].Trim() + "/" + To[2].Trim() + " 00:00:00");

            //Check if leave is for adjustment then Todate Should Not belong from current month
            if (ChkIsAdjustment.Checked == true)
            {
                if (FromDate.Month >= DateTime.Now.Month)
                {
                    ErrorText = "For Adjustment, From Date Should Be Previous Month's Date";
                    result = false;
                }
                else if (Todate.Month >= DateTime.Now.Month)
                {
                    ErrorText = "For Adjustment, To Date Should Be Previous Month's Date";
                    result = false;
                }
                else if (FromDate > Todate)
                {
                    ErrorText = "Please Select a Valid Date Range";
                    result = false;
                }

            }
            //Validation when Leave Is not for Adjustment
            else
            {
                if (FromDate > Todate)
                {
                    ErrorText = "Please Select a Valid Date Range";
                    result = false;
                }
                else if (FromDate < DateTime.Now.AddDays(Convert.ToDouble(0 - BusinessLayer.HR.LeaveApplicationConfig.GetMaxDayLimit())))
                {
                    ErrorText = "Maximum Day Limit Exceeded For Previous Leave Application";
                    result = false;
                }
            }
            return result;
        }

        protected bool HasStockBalance()
        {
            DataView DVStock = new DataView((DataTable)ViewState["Stock"]);
            DVStock.RowFilter = "LeaveTypeId=" + ddlLeaveType.SelectedValue.Trim();

            if (DVStock.ToTable().Rows.Count > 0)
            {
                if (GetNoOfDays() > Convert.ToDecimal(DVStock.ToTable().Rows[0]["LeaveBalance"].ToString()))
                    return false;
                else
                    return true;
            }
            else
            {
                return true;
            }

        }

        protected void ddlLeaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int LeaveTypeId = int.Parse(ddlLeaveType.SelectedValue.Trim());
            //Disable ToDate and enable No of leave dropdown when selecting Half-Pay Leave
            if (LeaveTypeId == 1) //half-Pay Leave
            {
                ddlLeaveFormat.Visible = true;
            }
            else
            {
                ddlLeaveFormat.Visible = false;
            }

        }

        protected decimal GetNoOfDays()
        {
            decimal NoOfDays = 0;
            DateTime FromDate;
            DateTime Todate;
            int LeaveTypeId = int.Parse(ddlLeaveType.SelectedValue.Trim());

            string[] From = txtFrom.Text.Trim().Split('/');
            FromDate = Convert.ToDateTime(From[1].Trim() + "/" + From[0].Trim() + "/" + From[2].Trim() + " 00:00:00");

            string[] To = txtTo.Text.Trim().Split('/');
            Todate = Convert.ToDateTime(To[1].Trim() + "/" + To[0].Trim() + "/" + To[2].Trim() + " 00:00:00");


            if (LeaveTypeId == 1) //For Half-Pay Leave
            {
                if (ddlLeaveFormat.SelectedValue == "Half")
                {
                    NoOfDays = (decimal.Parse(Todate.Subtract(FromDate).TotalDays.ToString()) + 1) / 2;
                }
                else { NoOfDays = decimal.Parse(Todate.Subtract(FromDate).TotalDays.ToString()) + 1; }
            }
            else
            {
                NoOfDays = decimal.Parse(Todate.Subtract(FromDate).TotalDays.ToString()) + 1;
            }

            return NoOfDays;
        }

        private bool? IsHalfDayApplied()
        {
            int LeaveTypeId = int.Parse(ddlLeaveType.SelectedValue.Trim());
            if (LeaveTypeId == 1)
            {
                if (ddlLeaveFormat.SelectedValue == "Half")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return null;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                if (HasStockBalance())
                {
                    BusinessLayer.HR.Leave ObjLeave = new BusinessLayer.HR.Leave();
                    Entity.HR.Leave Leave = new Entity.HR.Leave();
                    Leave.LeaveId = 0;//Always Save. Employee Can Not Edit Their Leave
                    Leave.EmployeeId = EmployeeId;
                    Leave.LeaveTypeId = int.Parse(ddlLeaveType.SelectedValue.Trim());
                    string[] Start = txtFrom.Text.Trim().Split('/');
                    Leave.StartDate = Convert.ToDateTime(Start[1].Trim() + "/" + Start[0].Trim() + "/" + Start[2].Trim() + " 00:00:00");

                    string[] EndDate = txtTo.Text.Trim().Split('/');
                    Leave.EndDate = Convert.ToDateTime(EndDate[1].Trim() + "/" + EndDate[0].Trim() + "/" + EndDate[2].Trim() + " 00:00:00");

                    Leave.NoOfDays = GetNoOfDays();
                    Leave.LeaveStatusId = 1; // Always pending during first time save
                    Leave.Purpose = txtPurpose.Text.Trim();
                    Leave.Comment = "";
                    Leave.isClassAdjusted = ChkIsClassAdjusted.Checked;
                    Leave.isExamDutyDuringLeave = ChkIsExamDutyDuringLeave.Checked;
                    Leave.IsAdjustment = ChkIsAdjustment.Checked;
                    Leave.IsHalfDayApplied = IsHalfDayApplied();
                    Leave.IsDirectorApproved = true;
                    Leave.Count = 0;
                    int i = 0;
                    int RowsAffected = ObjLeave.SaveDirect(Leave);
                    i = Leave.Count;
                    if (i == 2)
                    {
                        ClearControl();
                        LoadLeaveHistory();
                        Message.IsSuccess = true;
                        Message.Text = "Leave successfully applied";
                    }
                    else if (i == -1)
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Error while Saving";
                    }
                    else if (i == 3)
                    {
                        Message.IsSuccess = false;
                        Message.Text = "You don't have the permission to apply for this leave";
                    }
                    else if (i == 1)
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Sorry! You Can Not Apply For Leave. You Don't Have Any Manager For Leave Approval.";
                    }
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Sorry! You do not have enough leave balance for " + ddlLeaveType.SelectedItem.Text + ".";
                }
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = ErrorText;
            }
            Message.Show = true;
        }
        
        protected void BindEmployee()
        {
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            DataView dv = new DataView(ObjEmployee.GetAll("", ""));
            //dv.RowFilter = "EmployeeId <> " + EmployeeId;
            DataTable dt = dv.ToTable();
            DataRow dr = dt.NewRow();
            dr["EmployeeId"] = "0";
            dr["FullName"] = "--Select--";
            dt.Rows.InsertAt(dr, 0);
            dt.AcceptChanges();
            if (dt != null)
            {                               
                ddlEmployee.DataSource = dt;
                ddlEmployee.DataBind();
            }
            ddlEmployee.SelectedValue = Convert.ToString(EmployeeId);
        }      

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmployeeId = int.Parse(ddlEmployee.SelectedValue);
            LoadStockBalance();
            LoadLeaveHistory();
            LoadLeaveType();
        }
    }
}
