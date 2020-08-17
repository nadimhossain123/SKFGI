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

namespace CollegeERP.Payroll
{
    public partial class EmployeeAdditionalHeadSetting : System.Web.UI.Page
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
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.EMPLOYEE_ADDITIONAL_HEAD_SETTING))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadDropdownList();
                LoadAdditionList();
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


            BusinessLayer.Payroll.EmployeeSalaryAdditionalHead objAdditionHead = new BusinessLayer.Payroll.EmployeeSalaryAdditionalHead();
            DT = objAdditionHead.GetAdditionalHead();
            if (DT != null)
            {
                ddlAdditionalHead.DataSource = DT;
                ddlAdditionalHead.DataBind();
            }
            ddlAdditionalHead.Items.Insert(0, li);

            int CurentYear = DateTime.Now.Year;
            ddlYear.Items.Add(new ListItem((CurentYear - 1).ToString(), (CurentYear - 1).ToString()));
            ddlYear.Items.Add(new ListItem(CurentYear.ToString(), CurentYear.ToString()));
            ddlYear.Items.Add(new ListItem((CurentYear + 1).ToString(), (CurentYear + 1).ToString()));
            ddlYear.Items.Insert(0, li);
        }

        protected void LoadAdditionList()
        {
            BusinessLayer.Payroll.EmployeeSalaryAdditionalHead objAdditionHead = new BusinessLayer.Payroll.EmployeeSalaryAdditionalHead();
            Entity.Payroll.EmployeeSalaryAdditionalHead AdditionHead = new Entity.Payroll.EmployeeSalaryAdditionalHead();
            AdditionHead.EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue.Trim());
            AdditionHead.SalaryAdditionalHeadId = Convert.ToInt32(ddlAdditionalHead.SelectedValue.Trim());
            AdditionHead.AdditionYear = Convert.ToInt32(ddlYear.SelectedValue.Trim());
            AdditionHead.AdditionMonth = Convert.ToInt32(ddlMonth.SelectedValue.Trim());
            DataTable DT = objAdditionHead.GetAll(AdditionHead);

            if (DT != null)
            {
                dgvAddition.DataSource = DT;
                dgvAddition.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadAdditionList();
        }

        protected void dgvAddition_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(dgvAddition.DataKeys[e.RowIndex].Value.ToString());
            BusinessLayer.Payroll.EmployeeSalaryAdditionalHead objAdditionHead = new BusinessLayer.Payroll.EmployeeSalaryAdditionalHead();
            objAdditionHead.Delete(id);
            LoadAdditionList();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Payroll.EmployeeSalaryAdditionalHead objAdditionHead = new BusinessLayer.Payroll.EmployeeSalaryAdditionalHead();
            Entity.Payroll.EmployeeSalaryAdditionalHead AdditionHead = new Entity.Payroll.EmployeeSalaryAdditionalHead();
            AdditionHead.EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue.Trim());
            AdditionHead.SalaryAdditionalHeadId = Convert.ToInt32(ddlAdditionalHead.SelectedValue.Trim());
            AdditionHead.AdditionYear = Convert.ToInt32(ddlYear.SelectedValue.Trim());
            AdditionHead.AdditionMonth = Convert.ToInt32(ddlMonth.SelectedValue.Trim());
            AdditionHead.AdditionAmount = Convert.ToDecimal(txtAmount.Text.Trim());
            objAdditionHead.Save(AdditionHead);
            ddlAdditionalHead.SelectedIndex = 0;
            LoadAdditionList();
        }

    }
}
