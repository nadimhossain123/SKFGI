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
    public partial class Form16Generate : System.Web.UI.Page
    {
        public int PreviousFnYr
        {
            get { return Convert.ToInt32(ViewState["PreviousFnYr"]); }
            set { ViewState["PreviousFnYr"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }

            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.GENERATE_FORM16))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                Message.Show = false;
                GetPreviousFnYr();
                LoadEmployee();
                ltrFinancialYear.Text = PreviousFnYr.ToString() + " - " + (PreviousFnYr + 1).ToString();
            }

        }

        protected void LoadEmployee()
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

                ddlEmployee.DataSource = dt;
                ddlEmployee.DataBind();
            }
        }      

        protected void GetPreviousFnYr()
        {
            int CurrentFnYr;
            if (DateTime.Now.Month < 4)
            {
                CurrentFnYr = DateTime.Now.Year - 1;
            }
            else
            {
                CurrentFnYr = DateTime.Now.Year;
            }
            PreviousFnYr = CurrentFnYr - 1;
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            BusinessLayer.Payroll.Form16 ObjForm16 = new BusinessLayer.Payroll.Form16();
            Entity.Payroll.Form16 Form16 = new Entity.Payroll.Form16();
            Form16.FinYear = PreviousFnYr;
            int RowsAffected = ObjForm16.Save(Form16,int.Parse(ddlEmployee.SelectedValue.Trim()));

            if (RowsAffected != -1)
            {
                Message.IsSuccess = true;
                Message.Text = "Form16 Generated Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Generate. Form16 for this Financial Year is already Generated";
            }

            Message.Show = true;
        }


    }
}
