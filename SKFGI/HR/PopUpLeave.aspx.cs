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
using System.Text;

namespace CollegeERP.HR
{
    public partial class PopUpLeave : System.Web.UI.Page
    {
        public int EmployeeId
        {
            get { return Convert.ToInt32(ViewState["EmployeeId"]); }
            set { ViewState["EmployeeId"] = value; }
        }
        public int LeaveId
        {
            get { return Convert.ToInt32(ViewState["LeaveId"]); }
            set { ViewState["LeaveId"] = value; }
        }
        public string Email
        {
            get { return ViewState["Email"].ToString(); }
            set { ViewState["Email"] = value; }
        }

        public string ErrorText { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLeaveStatus();
                if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim().Length > 0)
                {
                    LeaveId = Convert.ToInt32(Request.QueryString["id"].Trim());
                    LoadLeaveDetails();
                    Message.Show = false;
                }
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
        }

        protected void LoadLeaveDetails()
        {
            BusinessLayer.HR.Leave ObjLeave = new BusinessLayer.HR.Leave();
            Entity.HR.Leave Leave = new Entity.HR.Leave();
            Leave = ObjLeave.GetAllById(LeaveId);
            if (Leave != null)
            {
                EmployeeId = Leave.EmployeeId;
                LoadEmpBasicInformation();
                LoadStockBalance();
                LoadLeaveType();
                ddlLeaveType.SelectedValue = Leave.LeaveTypeId.ToString();

                txtFrom.Text = Leave.StartDate.ToString("dd/MM/yyyy");
                txtTo.Text = Leave.EndDate.ToString("dd/MM/yyyy");

                if (Leave.LeaveTypeId == 1) // Means Half-pay Leave
                {
                    if (Leave.IsHalfDayApplied == true)
                    {
                        ddlLeaveFormat.SelectedValue = "Half";
                    }
                    else { ddlLeaveFormat.SelectedValue = "Full"; }
                    ddlLeaveFormat.Visible = true;
                }
                else { ddlLeaveFormat.Visible = false; }

                ddlStatus.SelectedValue = Leave.LeaveStatusId.ToString();
                txtPurpose.Text = Leave.Purpose;
                txtComment.Text = Leave.Comment;
                ChkIsClassAdjusted.Checked = Leave.isClassAdjusted;
                ChkIsExamDutyDuringLeave.Checked = Leave.isExamDutyDuringLeave;
                ChkIsAdjustment.Checked = Leave.IsAdjustment;
                txtCreatedOn.Text = Leave.CreatedOn.ToString("dd/MM/yyyy");

                //Hide Save button When Leave Status Is Other Than Pending
                if (Leave.LeaveStatusId == 2 || Leave.LeaveStatusId == 3 || Leave.LeaveStatusId == 4)
                {
                    btnSave.Visible = false;
                }

            }
        }

        protected void LoadEmpBasicInformation()
        {
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            Entity.Common.Employee Employee = new Entity.Common.Employee();
            Employee = ObjEmployee.GetAllById(EmployeeId);
            ltrEmpCode.Text = Employee.EmpCode;
            ltrEmpName.Text = Employee.FirstName + " " + Employee.MiddleName + " " + Employee.LastName;
            Email = Employee.ContactEmail1;

            BusinessLayer.Common.EmployeeOfficial ObjOfficial = new BusinessLayer.Common.EmployeeOfficial();
            Entity.Common.EmployeeOfficial Official = new Entity.Common.EmployeeOfficial();
            Official = ObjOfficial.GetAllByEmployeeId(EmployeeId);

            BusinessLayer.Common.Department ObjDepartment = new BusinessLayer.Common.Department();
            Entity.Common.Department Department = new Entity.Common.Department();
            Department = ObjDepartment.GetAllById(Official.EmployeeOfficial_DepartmentId);
            ltrDepartment.Text = Department.DepartmentName;

            BusinessLayer.Common.Designation ObjDesignation = new BusinessLayer.Common.Designation();
            Entity.Common.Designation Designation = new Entity.Common.Designation();
            Designation = ObjDesignation.GetAllById(Official.EmployeeOfficial_DesignationId);
            ltrDesignation.Text = Designation.DesignationName;
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

        protected void LoadLeaveType()
        {
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
            if (LeaveTypeId ==1)
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
                    Leave.LeaveId = LeaveId;
                    Leave.EmployeeId = EmployeeId;
                    Leave.LeaveTypeId = int.Parse(ddlLeaveType.SelectedValue.Trim());
                    string[] Start = txtFrom.Text.Trim().Split('/');
                    Leave.StartDate = Convert.ToDateTime(Start[1].Trim() + "/" + Start[0].Trim() + "/" + Start[2].Trim() + " 00:00:00");

                    string[] EndDate = txtTo.Text.Trim().Split('/');
                    Leave.EndDate = Convert.ToDateTime(EndDate[1].Trim() + "/" + EndDate[0].Trim() + "/" + EndDate[2].Trim() + " 00:00:00");

                    Leave.NoOfDays = GetNoOfDays();
                    Leave.LeaveStatusId = int.Parse(ddlStatus.SelectedValue.Trim());
                    Leave.Purpose = txtPurpose.Text.Trim();
                    Leave.Comment = txtComment.Text.Trim();
                    Leave.isClassAdjusted = ChkIsClassAdjusted.Checked;
                    Leave.isExamDutyDuringLeave = ChkIsExamDutyDuringLeave.Checked;
                    Leave.IsAdjustment = ChkIsAdjustment.Checked;
                    Leave.IsHalfDayApplied = IsHalfDayApplied();
                    Leave.Count = 0;
                    ObjLeave.Save(Leave);

                    //Hide Save button When Leave Status Is Other Than Pending
                    if (Leave.LeaveStatusId == 2 || Leave.LeaveStatusId == 3 || Leave.LeaveStatusId == 4)
                    {
                        btnSave.Visible = false;
                        LoadStockBalance();

                        //To Do
                        //SendMail();
                    }
                    Message.IsSuccess = true;
                    Message.Text = "Leave Modified Successfully";
                    Message.Show = true;
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Sorry! Employee do not have enough leave balance for " + ddlLeaveType.SelectedItem.Text + ".";
                }
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = ErrorText;
            }
            Message.Show = true;
        }
    
    }
}
