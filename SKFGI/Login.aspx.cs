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
using BusinessLayer.Accounts;

namespace CollegeERP
{
    public partial class Login : System.Web.UI.Page
    {
        clsGeneralFunctions genObj = new clsGeneralFunctions();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["UserId"] = null;
                Session["CompanyId"] = null;
                Session["BranchID"] = null;
                Session["FinYrID"] = null;
                Session["DataFlow"] = null;
                txtUserName.Focus();
                rbtnLoginAs.SelectedValue = "Employee";
            }
        }

        protected void btnLogIn_Click(object sender, ImageClickEventArgs e)
        {
            if (rbtnLoginAs.SelectedValue == "Employee")
            {
                EmployeeLogin();
            }
            else if(rbtnLoginAs.SelectedValue=="Student")
            {
                StudentLogin();
            }
            else if (rbtnLoginAs.SelectedValue == "SuperAdmin")
            {
                SuperAdmin();
            }
        }

        protected void SuperAdmin()
        {
            string UserName = System.Configuration.ConfigurationSettings.AppSettings["SuperAdminUserName"].ToString();
            string Password = System.Configuration.ConfigurationSettings.AppSettings["SuperAdminPassword"].ToString();
            if ((txtUserName.Text == UserName) && (txtPassword.Text == Password))
            {
                Session["SuperAdmin"] = "SuperAdmin";
                Session["CompanyID"] = "2";
                Session["UserId"] = "0";
                Session["FinYrID"] = GetCurrentFinYrID();
                Session["BranchID"] = 1;
                Session["DataFlow"] = 1;
                string strValues = Session["FinYrID"].ToString();
                DataSet ds = genObj.ExecuteSelectSP("spSelect_MstFinancialYearForSelection", strValues);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Session["SesFromDate"] = Convert.ToDateTime("01 Apr " + ds.Tables[0].Rows[0]["StartYear"].ToString()).ToString("dd MMM yyyy");
                    Session["SesToDate"] = Convert.ToDateTime("31 Mar " + ds.Tables[0].Rows[0]["EndYear"].ToString()).ToString("dd MMM yyyy");
                }
                Response.Redirect("Default.aspx?SuperAdmin=" + "SuperAdmin");
            }
        }

        protected void EmployeeLogin()
        {
            string u = txtUserName.Text.Trim();
            string p = txtPassword.Text.Trim();
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            Entity.Common.Employee Employee = new Entity.Common.Employee();
            Employee = ObjEmployee.AuthenticateUser(u);

            if (Employee != null)
            {
                if (Employee.Password == p)
                {
                    int UserId = Employee.EmployeeId;
                    int CompanyId = Employee.CompanyId;
                    FormsAuthenticationTicket Authticket = new FormsAuthenticationTicket(
                                                               1,
                                                               UserId.ToString(),
                                                               DateTime.Now,
                                                               DateTime.Now.AddMinutes(180),
                                                               false,
                                                               Employee.Roles,
                                                               FormsAuthentication.FormsCookiePath);
                    string hash = FormsAuthentication.Encrypt(Authticket);
                    HttpCookie Authcookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                    if (Authticket.IsPersistent)
                        Authcookie.Expires = Authticket.Expiration;
                    Response.Cookies.Add(Authcookie);
                    Session["UserId"] = UserId;
                    Session["CompanyId"] = CompanyId;
                    //Session["BranchID"] = GetBranchId(UserId);
                    Session["BranchID"] = 1;
                    Session["DataFlow"] = 1;
                    Session["FinYrID"] = GetCurrentFinYrID();
                    Session["EmpRole"] = Employee.Roles;
                    Session.Timeout = 180;

                    string strValues = Session["FinYrID"].ToString();
                    DataSet ds = genObj.ExecuteSelectSP("spSelect_MstFinancialYearForSelection", strValues);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Session["SesFromDate"] = Convert.ToDateTime("01 Apr " + ds.Tables[0].Rows[0]["StartYear"].ToString()).ToString("dd MMM yyyy");
                        Session["SesToDate"] = Convert.ToDateTime("31 Mar " + ds.Tables[0].Rows[0]["EndYear"].ToString()).ToString("dd MMM yyyy");
                    }
                    Response.Redirect("Default.aspx");
                }
            }
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtUserName.Focus();
        }

        protected int GetBranchId(int EmpId)
        {
            BusinessLayer.Common.EmployeeOfficial ObjOfficial = new BusinessLayer.Common.EmployeeOfficial();
            Entity.Common.EmployeeOfficial Official = new Entity.Common.EmployeeOfficial();
            Official = ObjOfficial.GetAllByEmployeeId(EmpId);
            return Official.EmployeeOfficial_BranchId;
        }

        protected void StudentLogin()
        {
            string u = txtUserName.Text.Trim();
            string p = txtPassword.Text.Trim();

            if (u.Trim().ToLower() == "student" && p.Trim().ToLower() == "skfgi001")
            {
                FormsAuthenticationTicket Authticket = new FormsAuthenticationTicket(
                                                           1,
                                                           "1",
                                                           DateTime.Now,
                                                           DateTime.Now.AddMinutes(180),
                                                           false,
                                                           "Student",
                                                           FormsAuthentication.FormsCookiePath);
                string hash = FormsAuthentication.Encrypt(Authticket);
                HttpCookie Authcookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                if (Authticket.IsPersistent)
                    Authcookie.Expires = Authticket.Expiration;
                Response.Cookies.Add(Authcookie);
                Session["UserId"] = "1";
                Session["CompanyId"] = "1";
                Session.Timeout = 180;
                Response.Redirect(@"Student\DiplomaRegistration.aspx");
            }

            txtUserName.Text = "";
            txtPassword.Text = "";
            txtUserName.Focus();
        }

        protected int GetCurrentFinYrID()
        {
            int CurrentFnYr = 0;
            if (DateTime.Now.Month < 4)
            {
                CurrentFnYr = DateTime.Now.Year - 1;
            }
            else
            {
                CurrentFnYr = DateTime.Now.Year;
            }
            DataView dv = new DataView(genObj.GetDropDownColumnsBySP("spSelect_MstFinancialYear", ""));
            dv.RowFilter = "StartYear=" + CurrentFnYr;
            return int.Parse(dv.ToTable().Rows[0]["FinYearID"].ToString());
        }
    }
}
