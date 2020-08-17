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
using CollegeERP.Accounts;

namespace CollegeERP.Payroll
{
    public partial class EmployeeIncrementList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.EMPLOYEE_INCREMENT_LIST))
                {
                    Response.Redirect("../Unauthorized.aspx");
                    
                }
                ClearControls();
             }           
        }

        private void ClearControls()
        {
            txtDateFrom.Text = "";
            txtDateTo.Text = "";
            txtIncrementDate.Text = "";
            ChkSelect.Checked = false;
            dgvEmployee.DataSource = null;
            dgvEmployee.DataBind();
            Message.Show = false;
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            DateTime DateFrom = Convert.ToDateTime(txtDateFrom.Text.Trim() + " 00:00:00");
            DateTime DateTo = Convert.ToDateTime(txtDateTo.Text.Trim() + " 23:59:59");
          //*********************************************
            BusinessLayer.Payroll.EmployeeIncrement objEI = new BusinessLayer.Payroll.EmployeeIncrement();
            DataSet ds = new DataSet();
            ds = objEI.GetAllByDate(DateFrom, DateTo, 1);
            
            dgvEmployee.DataSource = ds.Tables[0];
            dgvEmployee.DataBind();
            ChkSelect.Checked = false;
            Message.Show = false;
        }
                
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            DateTime IncrDate = Convert.ToDateTime(txtIncrementDate.Text.Trim() + " 00:00:00");
            BusinessLayer.Payroll.EmployeeIncrement objEmpIncrement = new BusinessLayer.Payroll.EmployeeIncrement();
            string IncrementListXml = String.Empty;
        
            DataTable DT = new DataTable();
            DT.Columns.Add("EmployeeId", typeof(int));
            DataRow DR;

            foreach (GridViewRow GVR in dgvEmployee.Rows)
            {
                if (GVR.RowType == DataControlRowType.DataRow)
                {
                    CheckBox cbRowSelect = (CheckBox)GVR.FindControl("chkSelect");
                    if (cbRowSelect.Checked)
                    {
                        DR = DT.NewRow();
                        DR["EmployeeId"] = Convert.ToInt32(dgvEmployee.DataKeys[GVR.RowIndex].Value.ToString());
                        DT.Rows.Add(DR);
                        DT.AcceptChanges();
                    }
                }
            }

            using (DataSet ds = new DataSet())
            {
                ds.Tables.Add(DT);
                IncrementListXml = ds.GetXml().Replace("Table1>", "Table>");
            }

            int CreatedBy =int.Parse(HttpContext.Current.User.Identity.Name);
            int recCount = objEmpIncrement.UpdateEmployeeIncrement(IncrementListXml, IncrDate, CreatedBy);
            btnShow_Click(sender, e);

             Message.IsSuccess = true;
             Message.Text = "Saved Successfully";
             Message.Show = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

    }
}
