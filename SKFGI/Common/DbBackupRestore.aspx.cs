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
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.IO;
using System.Security.AccessControl;

namespace CollegeERP.Common
{
    public partial class DbBackupRestore : System.Web.UI.Page
    {
        public string ResponseText;
        string strBackupPath = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("../Login.aspx");

            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.DATABASE_BACKUP_RESTORE))
                    Response.Redirect("../Unauthorized.aspx");

                message.Show = false;
                LoadBackup();
            }
        }

        private void LoadBackup()
        {
            BusinessLayer.Common.DBBackup objDBBackup = new BusinessLayer.Common.DBBackup();
            dgvBackup.DataSource = objDBBackup.GetAll();
            dgvBackup.DataBind();
        }

        private bool BackupDatabase()
        {
            bool result;
            strBackupPath = System.Configuration.ConfigurationSettings.AppSettings["BackupPath"].Trim() + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".bak";
            string command = string.Format("backup database [{0}] to disk='{1}'", System.Configuration.ConfigurationSettings.AppSettings["DatabaseName"].Trim(), strBackupPath);
            System.Data.SqlClient.SqlConnection sqlcon = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString());

            try
            {
                sqlcon.Open();
                System.Data.SqlClient.SqlCommand sqlcmd = new System.Data.SqlClient.SqlCommand(command, sqlcon);
                sqlcmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                sqlcon.Close();
                sqlcon = null;
            }
            return result;
        }

        protected void btnBackup_Click(object sender, EventArgs e)
        {
            if (BackupDatabase())
            {
                BusinessLayer.Common.DBBackup objDBBackup = new BusinessLayer.Common.DBBackup();
                Entity.Common.DBBackup entity = new Entity.Common.DBBackup();
                entity.EmployeeId = Convert.ToInt32(Session["UserId"].ToString());
                entity.BackupPath = strBackupPath;
                objDBBackup.Save(entity);

                LoadBackup();

                message.IsSuccess = true;
                message.Text = "Database Backup operation completed successfully";
            }
            else
            {
                message.IsSuccess = false;
                message.Text = "Database Backup operation failed";
            }
            message.Show = true;
        }

        protected void dgvBackup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvBackup.PageIndex = e.NewPageIndex;
            LoadBackup();
        }
    }
}
