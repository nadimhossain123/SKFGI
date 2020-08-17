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
using CollegeERP.Accounts;

namespace CollegeERP.Payroll
{
    public partial class SalaryHeadUpdation : System.Web.UI.Page
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
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.SALARY_HEAD_UPDATION))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadSalaryHead();
                Message.Show = false;
                LoadBlankGV();
               
            }
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
            int StudentHeadId =int.Parse( ddlSalaryHead.SelectedValue);
            BusinessLayer.Payroll.SalaryHead objSalary = new BusinessLayer.Payroll.SalaryHead();
            DataSet ds = new DataSet();
            ds = objSalary.GetAllBySalaryHeadId(StudentHeadId, 1);

            if (ds.Tables.Count > 0)
            {
                dgvEmployee.DataSource = ds.Tables[0];
                dgvEmployee.DataBind();
            }
            else
            {
                LoadBlankGV();

            }
        }
        protected void LoadBlankGV()
        {
            DataTable dt;
            dt = null;
            dgvEmployee.DataSource = dt;
            dgvEmployee.DataBind();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            
            BusinessLayer.Payroll.SalaryHead objSalary=new BusinessLayer.Payroll.SalaryHead();
            string SalaryHeadUpdateXml = String.Empty;
            DataTable DT = new DataTable();
            DT.Columns.Add("EmployeeId", typeof(int));
            DT.Columns.Add("SalaryHeadId", typeof(int));
            DT.Columns.Add("Amount", typeof( decimal));
          
            DataRow DR;

            foreach (GridViewRow GVR in dgvEmployee.Rows)
            {
                CheckBox cbrowSelect = (CheckBox)GVR.FindControl("ChkSelect");
                if (cbrowSelect.Checked == true)
                {
                    if (GVR.RowType == DataControlRowType.DataRow)
                    {

                        DR = DT.NewRow();
                        DR["EmployeeId"] = Convert.ToInt32(dgvEmployee.DataKeys[GVR.RowIndex].Value.ToString());
                        DR["SalaryHeadId"] = Convert.ToUInt32(ddlSalaryHead.SelectedValue.ToString());
                        DR["Amount"] = Convert.ToDecimal(txtAmount.Text.Trim());

                        DT.Rows.Add(DR);
                        DT.AcceptChanges();

                    }
                }
            }

            using (DataSet ds = new DataSet())
            {
                ds.Tables.Add(DT);
                SalaryHeadUpdateXml = ds.GetXml().Replace("Table1>", "Table>");
                //CreditBill.CreditBillXML = ds.GetXml().Replace("Table1>", "Table>");
            }

            int recCount = objSalary.UpdateSalaryHead(SalaryHeadUpdateXml);

            if (recCount > 0)
            {
                Message.IsSuccess = true;
                Message.Text = "Updated Successfully";
                Message.Show = true;
                LoadEmployeeList();
            }

        }

        protected void ddlSalaryHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEmployeeList();
        }

        protected void dgvEmployee_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Message.Show = false;
            LoadSalaryHead();
            txtAmount.Text = "";
            LoadBlankGV();
        }

      
    }
}
