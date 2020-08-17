﻿using System;
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

namespace CollegeERP.Accounts
{
    public partial class ChangeFinYr : System.Web.UI.Page
    {
        clsGeneralFunctions genObj = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["SuperAdmin"] != null)
            {
                this.MasterPageFile = "../SuperAdmin.Master";
            }
            else
            {
                this.MasterPageFile = "../MasterAdmin.Master";
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ((Literal)Page.Master.FindControl("ltrTitle")).Text = GetEmployeeName() + GetCompanyType() + GetFinYr();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["UserId"] == null) && (Session["SuperAdmin"] == null))
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if ((!HttpContext.Current.User.IsInRole(Entity.Common.Utility.CHANGE_FINYR)) && (Session["SuperAdmin"] == null))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }

                genObj.BindDropDownColumnsBySP(ddlFinancialYear, "spSelect_MstFinancialYear", "");
                ddlFinancialYear.SelectedValue = Session["FinYrID"].ToString();
            }
        }

        private string GetEmployeeName()
        {
            int EmployeeId = Convert.ToInt32(Session["UserId"].ToString());
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            Entity.Common.Employee Employee = new Entity.Common.Employee();
            Employee = ObjEmployee.GetAllById(EmployeeId);
            if (Employee != null)
            {
                return Employee.FirstName + " " + Employee.MiddleName + " " + Employee.LastName;
            }
            else { return ""; }
        }

        private string GetCompanyType()
        {
            int CompanyId = Convert.ToInt32(Session["CompanyId"].ToString());
            BusinessLayer.Common.Company ObjCompany = new BusinessLayer.Common.Company();
            Entity.Common.Company Company = new Entity.Common.Company();
            Company = ObjCompany.GetAllById(CompanyId);
            if (Company != null)
            {
                return "/" + Company.CompanyType;
            }
            else { return ""; }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Session["FinYrID"] = ddlFinancialYear.SelectedValue.ToString();
            string strValues = Session["FinYrID"].ToString();
            DataSet ds = genObj.ExecuteSelectSP("spSelect_MstFinancialYearForSelection", strValues);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["SesFromDate"] = Convert.ToDateTime("01 Apr " + ds.Tables[0].Rows[0]["StartYear"].ToString()).ToString("dd MMM yyyy");
                Session["SesToDate"] = Convert.ToDateTime("31 Mar " + ds.Tables[0].Rows[0]["EndYear"].ToString()).ToString("dd MMM yyyy");
            }
            Response.Redirect("ChangeFinYr.aspx");           
             
        }

        private string GetFinYr()
        {
            string strValues = Session["FinYrID"].ToString();
            DataSet ds = genObj.ExecuteSelectSP("spSelect_MstFinancialYearForSelection", strValues);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return "/" + ds.Tables[0].Rows[0]["StartYear"].ToString() + "-" + ds.Tables[0].Rows[0]["EndYear"].ToString();
            }
            else { return ""; }
        }
    }
}
