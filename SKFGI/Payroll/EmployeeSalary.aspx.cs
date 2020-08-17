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
    public partial class EmployeeSalary : System.Web.UI.Page
    {
        public int EmployeeId
        {
            get { return Convert.ToInt32(ViewState["EmployeeId"]); }
            set { ViewState["EmployeeId"] = value; }
        }
        public int EmployeeSalaryId
        {
            get { return Convert.ToInt32(ViewState["EmployeeSalaryId"]); }
            set { ViewState["EmployeeSalaryId"] = value; }
        }
        ListItem li = new ListItem("Select", "0");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.EMPLOYEE_SALARY_SETTING))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadPTax();
                LoadSalaryHead();
                LoadEmployeeList();
                ClearControls();
                btnNewSalaryHead.Attributes.Add("onclick", "javascript:return openpopup('PopupSalaryHeadDetails.aspx');");
            }
        }

        protected void LoadPTax()
        {
            BusinessLayer.Common.PTax ObjPTax = new BusinessLayer.Common.PTax();
            DataTable dt = ObjPTax.GetAll();
            if (dt != null)
            {
                ddlPTax.DataSource = dt;
                ddlPTax.DataBind();
            }
            ddlPTax.Items.Insert(0, li);
        }

        protected void LoadSalaryHead()
        {
            BusinessLayer.Payroll.SalaryHead ObjHead = new BusinessLayer.Payroll.SalaryHead();
            DataTable dt = ObjHead.GetAll();
            if (dt != null)
            {
                ddlSalaryHead.DataSource = dt;
                ddlSalaryHead.DataBind();
            }
            ddlSalaryHead.Items.Insert(0, li);
        }

        protected void LoadEmployeeList()
        {
            string EmpCode = txtEmpCode.Text.Trim();
            string FName = txtFNameSearch.Text.Trim();
            BusinessLayer.Payroll.EmployeeSalary Objsalary = new BusinessLayer.Payroll.EmployeeSalary();
            DataView dv =new DataView(Objsalary.GetAll(EmpCode, FName));
            dv.RowFilter = "CompanyId=" + Session["CompanyId"].ToString();

            if (dv != null)
            {
                dgvEmployee.DataSource = dv;
                dgvEmployee.DataBind();
            }
        }

        protected void ClearControls()
        {
            EmployeeId = 0;
            EmployeeSalaryId = 0;
            txtEmployeeName.Text = "";
            
            HidHasPTax.Value = "N";

            txtBasic.Text = "";
            txtEmployeePFAmt.Text = "0";
            
            ddlPTax.SelectedValue = "0";
            txtEmployerPFAmt.Text = "0";
            txtPFPercent.Text = "0";
            txtEmployerPFPercent.Text = "0";

            ddlSalaryHead.SelectedValue = "0";
            txtHeadPercent.Text = "";
            txtHeadAmount.Text = "";
            txtHeadAmount.Enabled = false;
            txtHeadAmount.Enabled = false;
            
            ChkIsFixedPF.Checked = false;
            txtGradePay.Text = "";

            ViewState["DTHead"] = null;
            dgvSalaryHead.DataSource = (DataTable)ViewState["DTHead"];
            dgvSalaryHead.DataBind();

            Message.Show = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
            LoadEmployeeList();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadEmployeeList();
        }

        protected void dgvEmployee_RowEditing(object sender, GridViewEditEventArgs e)
        {
            BusinessLayer.Common.StatutorySalaryConfig ObjConfig = new BusinessLayer.Common.StatutorySalaryConfig();
            Entity.Common.StatutorySalaryConfig Config = new Entity.Common.StatutorySalaryConfig();
            Config = ObjConfig.GetAll();

            EmployeeId = int.Parse(dgvEmployee.DataKeys[e.NewEditIndex].Value.ToString());
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            Entity.Common.Employee Employee = new Entity.Common.Employee();
            Employee = ObjEmployee.GetAllById(EmployeeId);
            if (Employee != null)
                 txtEmployeeName.Text = Employee.FirstName + " " + Employee.MiddleName + " " + Employee.LastName;
            
            BusinessLayer.Common.EmployeeOfficial ObjOfficial = new BusinessLayer.Common.EmployeeOfficial();
            Entity.Common.EmployeeOfficial Official = new Entity.Common.EmployeeOfficial();
            Official = ObjOfficial.GetAllByEmployeeId(EmployeeId);
            if (Official != null)
            {
                HidHasPTax.Value = Official.PTax;
                ddlPTax.Enabled = (Official.PTax == "Y") ? true : false;
                if (Official.HasPF == "Y")
                {
                    txtPFPercent.Text = Config.EmployeesPFCntrb.ToString();
                    txtEmployerPFPercent.Text = (Config.EmployersPFCntrb + Config.EmployersPension).ToString();
                }
                else
                {
                    txtPFPercent.Text = "0";
                    txtEmployerPFPercent.Text = "0";
                }
            }

            BusinessLayer.Payroll.EmployeeSalary Objsalary = new BusinessLayer.Payroll.EmployeeSalary();
            Entity.Payroll.EmployeeSalary Salary = new Entity.Payroll.EmployeeSalary();
            Salary = Objsalary.GetAllById(EmployeeId);
            if (Salary != null)
            {
                EmployeeSalaryId = Salary.EmployeeSalaryId;
                txtBasic.Text = Salary.EmployeeSalaryBasicAmount.ToString();
                txtEmployeePFAmt.Text = Salary.EmployeeSalaryPFAmount.ToString();
                ddlPTax.SelectedValue = Salary.EmployeeSalary_PTaxId.ToString();
                txtEmployerPFAmt.Text = Salary.EmployeeSalaryEmployerPF.ToString();
                ChkIsFixedPF.Checked = Salary.IsFixedPF;
                txtGradePay.Text = Salary.EmployeeSalaryGradePay.ToString();

            }

            DataTable dt = Objsalary.GetAllSalaryHead(EmployeeId);
            ViewState["DTHead"] = dt;
            dgvSalaryHead.DataSource = dt;
            dgvSalaryHead.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["DTHead"];
            int SalaryHeadId = int.Parse(ddlSalaryHead.SelectedValue.Trim());
            DataView dv = new DataView(dt);
            dv.RowFilter = "EmployeeSalaryHead_SalaryHeadId=" + SalaryHeadId;

            if (dv.ToTable().Rows.Count == 0)
            {
                //HRA MAX RANGE VALIDATION
                BusinessLayer.Payroll.SalaryHead ObjHead = new BusinessLayer.Payroll.SalaryHead();
                Entity.Payroll.SalaryHead Head = new Entity.Payroll.SalaryHead();
                Head = ObjHead.GetAllById(SalaryHeadId);
                decimal? MaxRange = Head.MaxRange;
                decimal HeadAmount = Convert.ToDecimal(txtHeadAmount.Text.Trim());
                if (MaxRange != null && HeadAmount > MaxRange.Value)
                    HeadAmount = MaxRange.Value;
                //END OF CALCULATION

                DataRow dr = dt.NewRow();
                dr["SalaryHeadDetails"] = ddlSalaryHead.SelectedItem.Text; ;
                dr["EmployeeSalaryHead_SalaryHeadId"] = SalaryHeadId;
                dr["EmployeeSalaryHeadPercent"] = decimal.Parse(txtHeadPercent.Text.Trim());
                dr["EmployeeSalaryHeadAmount"] = HeadAmount;
                dt.Rows.Add(dr);
                dt.AcceptChanges();

                ViewState["DTHead"] = dt;
                dgvSalaryHead.DataSource = dt;
                dgvSalaryHead.DataBind();

            }
        }

        protected void dgvSalaryHead_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["DTHead"];
            dt.Rows[e.RowIndex].Delete();
            dt.AcceptChanges();
            ViewState["DTHead"] = dt;
            dgvSalaryHead.DataSource = dt;
            dgvSalaryHead.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Payroll.EmployeeSalary ObjSalary = new BusinessLayer.Payroll.EmployeeSalary();
            Entity.Payroll.EmployeeSalary Salary = new Entity.Payroll.EmployeeSalary();
            Salary.EmployeeSalaryId = EmployeeSalaryId;
            Salary.EmployeeSalary_EmpId = EmployeeId;
            Salary.EmployeeSalaryBasicAmount = decimal.Parse(txtBasic.Text.Trim());
            Salary.EmployeeSalaryPFAmount = decimal.Parse(txtEmployeePFAmt.Text.Trim());
            Salary.EmployeeSalary_PTaxId = int.Parse(ddlPTax.SelectedValue.Trim());
            Salary.EmployeeSalaryEmployerPF = decimal.Parse(txtEmployerPFAmt.Text.Trim());
            Salary.IsFixedPF = ChkIsFixedPF.Checked;
            Salary.EmployeeSalaryGradePay = decimal.Parse(txtGradePay.Text.Trim());
            Salary.ModifiedBy = int.Parse(HttpContext.Current.User.Identity.Name);

            DataTable dt = (DataTable)ViewState["DTHead"];
            ArrayList arr = new ArrayList();
            Entity.Payroll.EmployeeSalaryHeadDTO SalaryHeadDTO = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SalaryHeadDTO = new Entity.Payroll.EmployeeSalaryHeadDTO();
                    SalaryHeadDTO.EmployeeSalaryHead_SalaryHeadId =int.Parse(dt.Rows[i]["EmployeeSalaryHead_SalaryHeadId"].ToString());
                    SalaryHeadDTO.EmployeeSalaryHeadPercent = decimal.Parse(dt.Rows[i]["EmployeeSalaryHeadPercent"].ToString());
                    SalaryHeadDTO.EmployeeSalaryHeadAmount = decimal.Parse(dt.Rows[i]["EmployeeSalaryHeadAmount"].ToString());

                    arr.Add(SalaryHeadDTO);
                }
                Salary.EmployeeSalaryHeadDTO = (Entity.Payroll.EmployeeSalaryHeadDTO[])arr.ToArray(typeof(Entity.Payroll.EmployeeSalaryHeadDTO));

            }

            int RowsAffected = ObjSalary.Save(Salary);
            if (RowsAffected != -1)
            {
                ClearControls();
                LoadEmployeeList();
                Message.IsSuccess = true;
                Message.Text = "Salary Setting Saved Successfully";
            }

            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Some Problem Occurs";
            }
            Message.Show = true;
        }

        protected void ddlSalaryHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSalaryHead.SelectedIndex > 0)
            {
                BusinessLayer.Payroll.SalaryHead ObjHead = new BusinessLayer.Payroll.SalaryHead();
                Entity.Payroll.SalaryHead Head = new Entity.Payroll.SalaryHead();
                Head = ObjHead.GetAllById(Convert.ToInt32(ddlSalaryHead.SelectedValue));
                if (Head.IsFixed)
                {
                    txtHeadPercent.Text = "0";
                    txtHeadPercent.Enabled = false;
                    txtHeadAmount.Text = "";
                    txtHeadAmount.Enabled = true;
                }
                else
                {
                    txtHeadPercent.Text = "";
                    txtHeadPercent.Enabled = true;
                    txtHeadAmount.Text = "0";
                    txtHeadAmount.Enabled = false;
                }
            }
            else
            {
                txtHeadPercent.Enabled = false;
                txtHeadAmount.Enabled = false;
            }
        }
    }
}
