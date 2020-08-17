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
    public partial class RoleAccessLevel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.ROLE_ACCESS_LEVEL))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadRoles();
                LoadRoleAccessLevel();
            }
        }

        protected void LoadRoles()
        {
            BusinessLayer.Common.Role ObjRole = new BusinessLayer.Common.Role();
            DataTable dt = ObjRole.GetAll();
            if (dt != null)
            {
                ddlRole.DataSource = dt;
                ddlRole.DataBind();
            }
        }

        protected void LoadRoleAccessLevel()
        {
            UncheckAll();
            int RoleId = Convert.ToInt32(ddlRole.SelectedValue.Trim());
            BusinessLayer.Common.EmployeeRole ObjRole = new BusinessLayer.Common.EmployeeRole();
            DataTable dt = ObjRole.GetRoleAccessLevelByRoleId(RoleId);
            foreach (DataRow dr in dt.Rows)
            {
                if (ChkLstSettings.Items.FindByValue(dr["PermissionId"].ToString()) != null)
                {
                    ChkLstSettings.Items.FindByValue(dr["PermissionId"].ToString()).Selected = true;
                }
                else if (ChkLstPayroll.Items.FindByValue(dr["PermissionId"].ToString()) != null)
                {
                    ChkLstPayroll.Items.FindByValue(dr["PermissionId"].ToString()).Selected = true;
                }
                else if (ChkLstStudent.Items.FindByValue(dr["PermissionId"].ToString()) != null)
                {
                    ChkLstStudent.Items.FindByValue(dr["PermissionId"].ToString()).Selected = true;
                }
                else if (ChkLstITax.Items.FindByValue(dr["PermissionId"].ToString()) != null)
                {
                    ChkLstITax.Items.FindByValue(dr["PermissionId"].ToString()).Selected = true;
                }
                else if (ChkLstAccounts.Items.FindByValue(dr["PermissionId"].ToString()) != null)
                {
                    ChkLstAccounts.Items.FindByValue(dr["PermissionId"].ToString()).Selected = true;
                }
                else if (ChkLstPO.Items.FindByValue(dr["PermissionId"].ToString()) != null)
                {
                    ChkLstPO.Items.FindByValue(dr["PermissionId"].ToString()).Selected = true;
                }
                else if (ChkLstHR.Items.FindByValue(dr["PermissionId"].ToString()) != null)
                {
                    ChkLstHR.Items.FindByValue(dr["PermissionId"].ToString()).Selected = true;
                }
                else if (ChkLstAction.Items.FindByValue(dr["PermissionId"].ToString()) != null)
                {
                    ChkLstAction.Items.FindByValue(dr["PermissionId"].ToString()).Selected = true;
                }
            }
        }

        protected void UncheckAll()
        {
            foreach (ListItem lstItem in ChkLstSettings.Items)
            {
                lstItem.Selected = false;
            }
            foreach (ListItem lstItem in ChkLstPayroll.Items)
            {
                lstItem.Selected = false;
            }
            foreach (ListItem lstItem in ChkLstHR.Items)
            {
                lstItem.Selected = false;
            }
            foreach (ListItem lstItem in ChkLstStudent.Items)
            {
                lstItem.Selected = false;
            }
            foreach (ListItem lstItem in ChkLstITax.Items)
            {
                lstItem.Selected = false;
            }
            foreach (ListItem lstItem in ChkLstAccounts.Items)
            {
                lstItem.Selected = false;
            }
            foreach (ListItem lstItem in ChkLstPO.Items)
            {
                lstItem.Selected = false;
            }
            foreach (ListItem lstItem in ChkLstAction.Items)
            {
                lstItem.Selected = false;
            }

        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRoleAccessLevel();
        }

        protected void CheckListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(Request.Form["__EVENTTARGET"].Substring(Request.Form["__EVENTTARGET"].LastIndexOf('$') + 1));
            CheckBoxList cbl = (CheckBoxList)sender;
            int PermissionId = int.Parse(cbl.Items[index].Value);
            SaveRoleAccessLevel(PermissionId, cbl.Items[index].Selected);
        }

        protected void SaveRoleAccessLevel(int PermissionId, bool IsChecked)
        {
            BusinessLayer.Common.EmployeeRole ObjRole = new BusinessLayer.Common.EmployeeRole();
            int RoleId = Convert.ToInt32(ddlRole.SelectedValue.Trim());
            ObjRole.Save_RoleAccessLevel(RoleId, PermissionId, IsChecked);

        }
    }
}
