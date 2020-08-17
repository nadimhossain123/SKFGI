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

namespace CollegeERP.HR
{
    public partial class LeaveManagerChange : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.LEAVE_MANAGER_CHANGE))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadLeaveManager();
                LoadEmpManager(int.Parse(ddlEmployee.SelectedValue.ToString()), 0);
                Message.Show = false;
              
            }
        }

        protected void LoadEmpManager(int EmployeeId,int IntMode)
        {
            BusinessLayer.HR.LeaveManagerChange objLeaveManager = new BusinessLayer.HR.LeaveManagerChange();
            DataSet ds = new DataSet();
            ds = objLeaveManager.GetAllByEmployeeId(EmployeeId, IntMode);

            if (ds.Tables.Count > 0)
            {
                dgvLeave.DataSource = ds.Tables[0];
                dgvLeave.DataBind();
            }
        }

        protected void LoadLeaveManager()
        {
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            DataView dv = new DataView(ObjEmployee.GetAll("", ""));
            //dv.RowFilter = "EmployeeId <> " + EmployeeId;
            DataTable dt = dv.ToTable();

            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["EmployeeId"] = "0";
                dr["FullName"] = "--Select--";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                ddlLeaveManager.DataSource = dt;
                ddlLeaveManager.DataBind();

                ddlEmployee.DataSource = dt;
                ddlEmployee.DataBind();

                
            }
        }      
      

        protected void dgvLeave_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvLeave.PageIndex = e.NewPageIndex;
            LoadEmpManager(int.Parse(ddlEmployee.SelectedValue.ToString()), 0);
           // LoadRequestList();
        }

        protected void dgvLeave_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int EmployeeId = Convert.ToInt32(dgvLeave.DataKeys[e.Row.RowIndex].Value.ToString());
                //((ImageButton)e.Row.FindControl("btnEdit")).Attributes.Add("onclick", "javascript:openpopup('PopUpLeave.aspx?Id=" + LeaveId + "');");
               
            }
        }

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEmpManager(int.Parse(ddlEmployee.SelectedValue.ToString()), 0);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            BusinessLayer.HR.LeaveManagerChange objLeaveManager = new BusinessLayer.HR.LeaveManagerChange();
            string LeaveManagerUpdateXml = String.Empty;
            DataTable DT = new DataTable();
            DT.Columns.Add("EmployeeId1", typeof(int));
            DT.Columns.Add("LeaveManagerId1", typeof(int));
            //DT.Columns.Add("Amount", typeof(decimal));

            DataRow DR;

            foreach (GridViewRow GVR in dgvLeave.Rows)
            {
                CheckBox cbRowSelect = (CheckBox)GVR.FindControl("chkSelect");
                if (cbRowSelect.Checked == true)
                {
                    if (GVR.RowType == DataControlRowType.DataRow)
                    {
                        DR = DT.NewRow();
                        DR["EmployeeId1"] = Convert.ToInt32(dgvLeave.DataKeys[GVR.RowIndex].Value.ToString());
                        DR["LeaveManagerId1"] = Convert.ToUInt32(ddlLeaveManager.SelectedValue.ToString());
                        //DR["Amount"] = Convert.ToDecimal(txtAmount.Text.Trim());

                        DT.Rows.Add(DR);
                        DT.AcceptChanges();
                    }
                }
            }

            using (DataSet ds = new DataSet())
            {
                ds.Tables.Add(DT);
                LeaveManagerUpdateXml = ds.GetXml().Replace("Table1>", "Table>");
                //CreditBill.CreditBillXML = ds.GetXml().Replace("Table1>", "Table>");
            }

            int recCount = objLeaveManager.UpdateLeaveManager(LeaveManagerUpdateXml);

            if (recCount > 0)
            {
                Message.IsSuccess = true;
                Message.Text = "Updated Successfully";
                Message.Show = true;
                LoadEmpManager(int.Parse(ddlEmployee.SelectedValue.ToString()), 0);
            }


        }
    }
}
