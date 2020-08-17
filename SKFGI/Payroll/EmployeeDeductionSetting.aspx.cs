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
using System.Globalization;

namespace CollegeERP.Payroll
{
    public partial class EmployeeDeductionSetting : System.Web.UI.Page
    {
        ListItem li = new ListItem("Select", "0");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.EMPLOYEE_DEDUCTION_SETTING))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadDropdownList();
                LoadDeductionList();
            }
        }

        protected void LoadDropdownList()
        {
            BusinessLayer.Payroll.EmployeeSalaryDeductionHead ObjDeductionHead = new BusinessLayer.Payroll.EmployeeSalaryDeductionHead();
            DataTable DT;
            DT = ObjDeductionHead.GetActiveEmployee(Convert.ToInt32(Session["CompanyId"].ToString()));
            if (DT != null)
            {
                ddlEmployee.DataSource = DT;
                ddlEmployee.DataBind();
            }
            ddlEmployee.Items.Insert(0, li);


            DT = ObjDeductionHead.GetDeductionHead();
            if (DT != null)
            {
                ddlDeductionHead.DataSource = DT;
                ddlDeductionHead.DataBind();
            }
            ddlDeductionHead.Items.Insert(0, li);

            int CurentYear = DateTime.Now.Year;
            ddlYear.Items.Add(new ListItem((CurentYear - 1).ToString(), (CurentYear - 1).ToString()));
            ddlYear.Items.Add(new ListItem(CurentYear.ToString(), CurentYear.ToString()));
            ddlYear.Items.Add(new ListItem((CurentYear + 1).ToString(), (CurentYear + 1).ToString()));
            ddlYear.Items.Insert(0, li);
        }

        protected void LoadDeductionList()
        {
            BusinessLayer.Payroll.EmployeeSalaryDeductionHead ObjDeductionHead = new BusinessLayer.Payroll.EmployeeSalaryDeductionHead();
            Entity.Payroll.EmployeeSalaryDeductionHead DeductionHead = new Entity.Payroll.EmployeeSalaryDeductionHead();
            DeductionHead.EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue.Trim());
            DeductionHead.SalaryDeductionHeadId = Convert.ToInt32(ddlDeductionHead.SelectedValue.Trim());
            DeductionHead.DeductionYear = Convert.ToInt32(ddlYear.SelectedValue.Trim());
            DeductionHead.DeductionMonth = Convert.ToInt32(ddlMonth.SelectedValue.Trim());
            DataTable DT = ObjDeductionHead.GetAll(DeductionHead);

            if (DT != null)
            {
                dgvDeduction.DataSource = DT;
                dgvDeduction.DataBind();
            }


        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadDeductionList();
        }

        protected void dgvDeduction_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(dgvDeduction.DataKeys[e.RowIndex].Value.ToString());
            BusinessLayer.Payroll.EmployeeSalaryDeductionHead ObjDeductionHead = new BusinessLayer.Payroll.EmployeeSalaryDeductionHead();
            ObjDeductionHead.Delete(id);
            LoadDeductionList();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Payroll.EmployeeSalaryDeductionHead ObjDeductionHead = new BusinessLayer.Payroll.EmployeeSalaryDeductionHead();
            Entity.Payroll.EmployeeSalaryDeductionHead DeductionHead = new Entity.Payroll.EmployeeSalaryDeductionHead();
            DeductionHead.EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue.Trim());
            DeductionHead.SalaryDeductionHeadId = Convert.ToInt32(ddlDeductionHead.SelectedValue.Trim());
            DeductionHead.DeductionYear = Convert.ToInt32(ddlYear.SelectedValue.Trim());
            DeductionHead.DeductionMonth = Convert.ToInt32(ddlMonth.SelectedValue.Trim());
            DeductionHead.DeductionAmount = Convert.ToDecimal(txtAmount.Text.Trim());
            ObjDeductionHead.Save(DeductionHead);
            ddlDeductionHead.SelectedIndex = 0;
            LoadDeductionList();    
        }
    }
}
