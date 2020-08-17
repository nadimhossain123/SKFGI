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
    public partial class StatutorySalaryConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.STATUTORY_SALARY_CONFIG))
                {
                    Response.Redirect("Unauthorized.aspx");
                }
                LoadConfig();
            }
        }

        protected void LoadConfig()
        {
            BusinessLayer.Common.StatutorySalaryConfig ObjConfig = new BusinessLayer.Common.StatutorySalaryConfig();
            Entity.Common.StatutorySalaryConfig Config = new Entity.Common.StatutorySalaryConfig();
            Config = ObjConfig.GetAll();
            if (Config != null)
            {
                txtEffectiveDate.Text = Config.EffectiveDate.ToString("dd/MM/yyyy");
                txtEmployersPFCntrb.Text = Config.EmployersPFCntrb.ToString("#0.00");
                txtEmployeesPFCntrb.Text = Config.EmployeesPFCntrb.ToString("#0.00");
                txtEmployersESICntrb.Text = Config.EmployersESICntrb.ToString("#0.00");
                txtEmployeesESICntrb.Text = Config.EmployeesESICntrb.ToString("#0.00");
                txtESILimit.Text = Config.ESILimit.ToString("#0.00");
                txtEmployerPension.Text = Config.EmployersPension.ToString("#0.00");
                txtPFAdminCharges.Text = Config.PFAdminCharges.ToString("#0.00");
                txtEDLICharges.Text = Config.EDLICharges.ToString("#0.00");
                txtEDLIAdminCharges.Text = Config.EDLIAdminCharges.ToString("#0.00");
                Message.Show = false;
            }
        }

        protected void ClearControls()
        {
            txtEffectiveDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtEmployersPFCntrb.Text = "0.00";
            txtEmployeesPFCntrb.Text = "0.00";
            txtEmployersESICntrb.Text = "0.00";
            txtEmployeesESICntrb.Text = "0.00";
            txtESILimit.Text = "0.00";
            txtEmployerPension.Text = "0.00";
            txtPFAdminCharges.Text = "0.00";
            txtEDLICharges.Text = "0.00";
            txtEDLIAdminCharges.Text = "0.00";
            Message.Show = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.StatutorySalaryConfig ObjConfig = new BusinessLayer.Common.StatutorySalaryConfig();
            Entity.Common.StatutorySalaryConfig Config = new Entity.Common.StatutorySalaryConfig();

            string[] EfectiveDate = txtEffectiveDate.Text.Trim().Split('/');
            Config.EffectiveDate = Convert.ToDateTime(EfectiveDate[1].Trim() + "/" + EfectiveDate[0].Trim() + "/" + EfectiveDate[2].Trim() + " 00:00:00");
            Config.EmployersPFCntrb= decimal.Parse(txtEmployersPFCntrb.Text.Trim());
            Config.EmployeesPFCntrb = decimal.Parse(txtEmployeesPFCntrb.Text.Trim());
            Config.EmployersESICntrb = decimal.Parse(txtEmployersESICntrb.Text.Trim());
            Config.EmployeesESICntrb = decimal.Parse(txtEmployeesESICntrb.Text.Trim());
            Config.ESILimit = decimal.Parse(txtESILimit.Text.Trim());
            Config.EmployersPension = decimal.Parse(txtEmployerPension.Text.Trim());
            Config.PFAdminCharges = decimal.Parse(txtPFAdminCharges.Text.Trim());
            Config.EDLICharges = decimal.Parse(txtEDLICharges.Text.Trim());
            Config.EDLIAdminCharges = decimal.Parse(txtEDLIAdminCharges.Text.Trim());
            
            Config.StatutorySalaryConfigCUser_UserId = int.Parse(HttpContext.Current.User.Identity.Name);
            ObjConfig.Save(Config);
            LoadConfig();

            Message.IsSuccess = true;
            Message.Text="Statutory Salary Fixation Saved Successfully";
            Message.Show = true;
            
        }

        
    }
}
