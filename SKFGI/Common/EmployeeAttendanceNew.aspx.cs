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
using System.IO;


namespace CollegeERP.Common
{
    public partial class EmployeeAttendanceNew : System.Web.UI.Page
    {

        BusinessLayer.Payroll.MonthYearList MonthYearList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.IMPORT_EMPLOYEE_ATTENDANCE))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadYear();
                Message.Show = false;
            }
        }

        protected void LoadYear()
        {
            MonthYearList = new BusinessLayer.Payroll.MonthYearList();
            DataView dv = new DataView(MonthYearList.GetYear());
            dv.RowFilter = "YearNo <= " + DateTime.Now.Year;
            System.Data.DataTable dt = dv.ToTable();

            DataRow dr = dt.NewRow();
            dr["YearNo"] = 0;
            dr["YearName"] = "--Select Year--";
            dt.Rows.InsertAt(dr, 0);
            dt.AcceptChanges();

            ddlYear.DataSource = dt;
            ddlYear.DataBind();

            LoadMonth();
        }

        protected void LoadMonth()
        {
            MonthYearList = new BusinessLayer.Payroll.MonthYearList();
            int Year = int.Parse(ddlYear.SelectedValue.Trim());
            if (Year != 0)
            {
                DataView dv = new DataView(MonthYearList.GetMonth());
                if (Year == DateTime.Now.Year)
                {
                    dv.RowFilter = "MonthNo <= " + DateTime.Now.Month;
                }

                System.Data.DataTable dt = dv.ToTable();
                DataRow dr = dt.NewRow();
                dr["MonthNo"] = 0;
                dr["MonthName"] = "--Select Month--";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                ddlMonth.DataSource = dt;
                ddlMonth.DataBind();
            }
            else
            {

                ddlMonth.DataSource = MonthYearList.GetBlankMonthList();
                ddlMonth.DataBind();
            }
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMonth();
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            int Year = int.Parse(ddlYear.SelectedValue.Trim());
            int Month = int.Parse(ddlMonth.SelectedValue.Trim());
            getAttendance(Month, Year);
        }
        protected void getAttendance(int Month, int Year)
        {
           
            BusinessLayer.Common.EmployeeAttendance Obj = new BusinessLayer.Common.EmployeeAttendance();
            System.Data.DataTable dt = Obj.GetAllNew(Month, Year);

            if (dt != null)
            {
                dgvAttendance.DataSource = dt;
                dgvAttendance.DataBind();
                dgvAttendanceRpt.DataSource = dt;
                dgvAttendanceRpt.DataBind();
                Session["EmpAttendance"] = dt;
                btnDownload.Visible = true;
            }
            else
            {
                Session["EmpAttendance"] = "";
            }
            Message.Show = false;
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.EmployeeAttendance objEmpAtdB = new BusinessLayer.Common.EmployeeAttendance();
            int month=int.Parse(ddlMonth.SelectedValue);
            int Year = int.Parse(ddlYear.SelectedValue);
            int CreatedBy=int.Parse(HttpContext.Current.User.Identity.Name);
            objEmpAtdB.SaveNew(month, Year, CreatedBy);
            Message.IsSuccess = true;
            Message.Text = "Attendance Saved Successfully";
            Message.Show = true;                      
        }

        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {
           
        }

        protected void btnUpdateAbsent_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.EmployeeAttendance objEmpB = new BusinessLayer.Common.EmployeeAttendance();
            int Year = int.Parse(ddlYear.SelectedValue.Trim());
            int Month = int.Parse(ddlMonth.SelectedValue.Trim());
            int EmployeeId = Convert.ToInt16(hdnEmployeeId.Value);
            objEmpB.AbsentUpdate(Month, Year, EmployeeId, decimal.Parse(txtDays.Text.Trim()));
            getAttendance(Month, Year);
            txtDays.Text = string.Empty;
        }

        //protected void dgvAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
        //{

        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {

        //        ((TextBox)e.Row.FindControl("txtCL")).Attributes.Add("onkeydown", "javascript:moveEnter(" + (e.Row.RowIndex + 1) + ");");
        //        Session["State"] = 1;
        //    }
        //}
        protected void dgvAttendance_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "Change")
            //{
            //    int index = Convert.ToInt32(e.CommandArgument.ToString());

            //    int AttendanceId = int.Parse(dgvAttendance.DataKeys[index]["Id"].ToString());
            //    Entity.Common.EmployeeAttendance objEmpAtdE = new Entity.Common.EmployeeAttendance();
            //    objEmpAtdE.Id = AttendanceId;
            //    objEmpAtdE.Month = int.Parse(ddlMonth.SelectedValue);
            //    objEmpAtdE.Year = int.Parse(ddlYear.SelectedValue);
            //    objEmpAtdE.EmployeeId = int.Parse(dgvAttendance.DataKeys[index]["EmpId"].ToString());
            //    objEmpAtdE.EmpCode = dgvAttendance.Rows[index].Cells[1].ToString();
            //    //objEmpAtdE.TotalDays =int.Parse(dgvAttendance.Rows[index].Cells[3].ToString());
            //    TextBox txtPresent = (TextBox)dgvAttendance.Rows[index].FindControl("txtPresent");               
            //    objEmpAtdE.Present = decimal.Parse(txtPresent.Text.Trim());
            //    TextBox txtAbsent = (TextBox)dgvAttendance.Rows[index].FindControl("txtAbsent");
            //    objEmpAtdE.Absent = decimal.Parse(txtAbsent.Text.Trim());
            //    TextBox txtCL = (TextBox)dgvAttendance.Rows[index].FindControl("txtCL");
            //    objEmpAtdE.CL = decimal.Parse(txtCL.Text.Trim());
            //    TextBox txtEL = (TextBox)dgvAttendance.Rows[index].FindControl("txtEL");
            //    objEmpAtdE.EL = decimal.Parse(txtEL.Text.ToString());
            //    TextBox txtMedical = (TextBox)dgvAttendance.Rows[index].FindControl("txtMedical");
            //    objEmpAtdE.Medical = decimal.Parse(txtMedical.Text.Trim());
            //    TextBox txtSpecial = (TextBox)dgvAttendance.Rows[index].FindControl("txtSpecial");
            //    objEmpAtdE.SpecialLeave = decimal.Parse(txtSpecial.Text.Trim());
            //    TextBox txtHoliday = (TextBox)dgvAttendance.Rows[index].FindControl("txtHoliday");
            //    objEmpAtdE.Holiday = decimal.Parse(txtHoliday.Text.Trim());
            //    TextBox txtOffDay = (TextBox)dgvAttendance.Rows[index].FindControl("txtOffDay");
            //    objEmpAtdE.OffDay = decimal.Parse(txtOffDay.Text.Trim());
            //    TextBox txtTotalDays = (TextBox)dgvAttendance.Rows[index].FindControl("txtTotalPayDay");
            //    objEmpAtdE.TotalPayDay = decimal.Parse(txtTotalDays.Text.Trim());
            //    BusinessLayer.Common.EmployeeAttendance objEmpAtdB = new BusinessLayer.Common.EmployeeAttendance();

            //    int rowAffected = objEmpAtdB.UpdateAttendance(objEmpAtdE);
            //    if (rowAffected > 0)
            //    {
            //        Message.IsSuccess = true;
            //        Message.Text = "Attendance Updated Successfully";
            //        Message.Show = true;
            //        int Year = int.Parse(ddlYear.SelectedValue.Trim());
            //        int Month = int.Parse(ddlMonth.SelectedValue.Trim());
            //        getAttendance(Month, Year);
            //    }
            //}
        }

        protected void txtCL_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            //NamingContainer return the container that the control sits in
            TextBox other = (TextBox)row.FindControl("txtCL");
            this.ModalPopupExtender1.Show();
        }

        protected void lbtnAbsent_Click(object sender, EventArgs e)
        {
            LinkButton btnShow = sender as LinkButton;
            GridViewRow gvrow = (GridViewRow)btnShow.NamingContainer;
            int EmployeeId = int.Parse(dgvAttendance.DataKeys[gvrow.RowIndex]["EmpId"].ToString());
            pnlAbsent.Visible = true;
            hdnEmployeeId.Value =Convert.ToString( EmployeeId);
            this.ModalPopupExtender1.Show();
        }

        protected void lbtnCL_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.EmployeeAttendance objEmpAtnB = new BusinessLayer.Common.EmployeeAttendance();
            LinkButton btnShow = sender as LinkButton;
            GridViewRow gvrow = (GridViewRow)btnShow.NamingContainer;
            int EmployeeId = int.Parse(dgvAttendance.DataKeys[gvrow.RowIndex]["EmpId"].ToString());
            Session["EmpId"] = EmployeeId;
            int LeaveId = 1;
            int Year = int.Parse(ddlYear.SelectedValue.Trim());
            int Month = int.Parse(ddlMonth.SelectedValue.Trim());
            DataTable dt = objEmpAtnB.GetLeaveDetailById(Month, Year, EmployeeId, LeaveId);
            if (dt != null)
            {
                dgvLeaveDetail.DataSource = dt;
                dgvLeaveDetail.DataBind();
            }
            else
            {
                DataTable dtBl = null;
                dgvLeaveDetail.DataSource = dtBl;
                dgvLeaveDetail.DataBind();
            }
            pnlAbsent.Visible = false;
            this.ModalPopupExtender1.Show();
        }
        protected void lbtnEL_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.EmployeeAttendance objEmpAtnB = new BusinessLayer.Common.EmployeeAttendance();
            LinkButton btnShow = sender as LinkButton;
            GridViewRow gvrow = (GridViewRow)btnShow.NamingContainer;
            int EmployeeId = int.Parse(dgvAttendance.DataKeys[gvrow.RowIndex]["EmpId"].ToString());
            Session["EmpId"] = EmployeeId;
            int LeaveId = 4;
            int Year = int.Parse(ddlYear.SelectedValue.Trim());
            int Month = int.Parse(ddlMonth.SelectedValue.Trim());
            DataTable dt = objEmpAtnB.GetLeaveDetailById(Month, Year, EmployeeId, LeaveId);
            if (dt != null)
            {
                dgvLeaveDetail.DataSource = dt;
                dgvLeaveDetail.DataBind();
            }
            else
            {
                DataTable dtBl = null;
                dgvLeaveDetail.DataSource = dtBl;
                dgvLeaveDetail.DataBind();
            }
            pnlAbsent.Visible = false;
            this.ModalPopupExtender1.Show();
        }
        protected void lbtnMedical_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.EmployeeAttendance objEmpAtnB = new BusinessLayer.Common.EmployeeAttendance();
            LinkButton btnShow = sender as LinkButton;
            GridViewRow gvrow = (GridViewRow)btnShow.NamingContainer;
            int EmployeeId = int.Parse(dgvAttendance.DataKeys[gvrow.RowIndex]["EmpId"].ToString());
            Session["EmpId"] = EmployeeId;
            int LeaveId = 10;
            int Year = int.Parse(ddlYear.SelectedValue.Trim());
            int Month = int.Parse(ddlMonth.SelectedValue.Trim());
            DataTable dt = objEmpAtnB.GetLeaveDetailById(Month, Year, EmployeeId, LeaveId);
            if (dt != null)
            {
                dgvLeaveDetail.DataSource = dt;
                dgvLeaveDetail.DataBind();
            }
            else
            {
                DataTable dtBl = null;
                dgvLeaveDetail.DataSource = dtBl;
                dgvLeaveDetail.DataBind();
            }
            pnlAbsent.Visible = false;
            this.ModalPopupExtender1.Show();
        }
        protected void lbtnSpecial_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.EmployeeAttendance objEmpAtnB = new BusinessLayer.Common.EmployeeAttendance();
            LinkButton btnShow = sender as LinkButton;
            GridViewRow gvrow = (GridViewRow)btnShow.NamingContainer;
            int EmployeeId = int.Parse(dgvAttendance.DataKeys[gvrow.RowIndex]["EmpId"].ToString());
            Session["EmpId"] = EmployeeId;
            int LeaveId = 6;
            int Year = int.Parse(ddlYear.SelectedValue.Trim());
            int Month = int.Parse(ddlMonth.SelectedValue.Trim());
            DataTable dt = objEmpAtnB.GetLeaveDetailById(Month, Year, EmployeeId, LeaveId);
            if (dt != null)
            {
                dgvLeaveDetail.DataSource = dt;
                dgvLeaveDetail.DataBind();
            }
            else
            {
                DataTable dtBl = null;
                dgvLeaveDetail.DataSource = dtBl;
                dgvLeaveDetail.DataBind();
            }
            pnlAbsent.Visible = false;
            this.ModalPopupExtender1.Show();
        }
        protected void dgvLeaveDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Change")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                int month=int.Parse(ddlMonth.SelectedValue);
                int Year = int.Parse(ddlYear.SelectedValue);
                int LeaveId = int.Parse(dgvLeaveDetail.DataKeys[index]["leaveid"].ToString());
                TextBox txtFromDate = (TextBox)dgvLeaveDetail.Rows[index].FindControl("txtFromDate");
                DateTime FromDate = Convert.ToDateTime(txtFromDate.Text.Trim() + " 00:00:00");
                TextBox txtToDate = (TextBox)dgvLeaveDetail.Rows[index].FindControl("txtEndDate");
                DateTime ToDate = Convert.ToDateTime(txtToDate.Text.Trim() + " 00:00:00");
                int CreatedBy = int.Parse(HttpContext.Current.User.Identity.Name);
                
               
                BusinessLayer.Common.EmployeeAttendance objEmpAtdB = new BusinessLayer.Common.EmployeeAttendance();

                int rowAffected = objEmpAtdB.LeaveUpdate(LeaveId, FromDate, ToDate, month, Year,int.Parse(Session["EmpId"].ToString()),CreatedBy);
                if (rowAffected > 0)
                {
                    Message.IsSuccess = true;
                    Message.Text = "Attendance Updated Successfully";
                    Message.Show = true;
                  
                    getAttendance(int.Parse(ddlMonth.SelectedValue.Trim()), int.Parse(ddlYear.SelectedValue.Trim()));
                }
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string[] _header = new string[3];
            _header[0] = "Employee Attendance Report";
            _header[1] = "Month: " + ddlMonth.SelectedItem.Text;
            _header[2] = "Year: " + ddlYear.Text.ToString();
           
          


            string[] _footer = new string[0];
            string file = "EMPLOYEE_ATTENDANCE_REPORT";            
            BusinessLayer.Common.Excel.SaveExcel(_header, dgvAttendanceRpt , _footer, file);
           
        }

       
    }
}
