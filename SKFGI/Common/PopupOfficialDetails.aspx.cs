using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace CollegeERP.Common
{
    public partial class PopupOfficialDetails : System.Web.UI.Page
    {
        public int EmployeeId
        {
            get { return Convert.ToInt32(ViewState["EmployeeId"]); }
            set { ViewState["EmployeeId"] = value; }
        }

        public int EmployeeOfficialId
        {
            get { return Convert.ToInt32(ViewState["EmployeeOfficialId"]); }
            set { ViewState["EmployeeOfficialId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategory();
                LoadDepartment();
                LoadDesignation();
                LoadBranch();
                LoadGrade();
                LoadPayBand();
                Message.Show = false;
                if (Request.QueryString["EmployeeId"] != null && Request.QueryString["EmployeeId"].Trim().Length > 0)
                {
                    EmployeeId = Convert.ToInt32(Request.QueryString["EmployeeId"].Trim());
                    //btnAddCategory.Attributes.Add("onclick", "javascript:return openpopup('CategoryMaster.aspx');");
                    //btnDepartmentAdd.Attributes.Add("onclick", "javascript:return openpopup('DepartmentMaster.aspx');");
                    //btnAddDesignation.Attributes.Add("onclick", "javascript:return openpopup('DesignationMaster.aspx');");
                    //btnAddBranch.Attributes.Add("onclick", "javascript:return openpopup('BranchMaster.aspx');");
                   // btnAddGrade.Attributes.Add("onclick", "javascript:return openpopup('GradeMaster.aspx');");
                    LoadEmployeeDetails();
                    LoadEmployeeOfficialDetails();
                }
            }
        }

        protected void LoadPayBand()
        {
            BusinessLayer.Common.PayBand ObjPayBand = new BusinessLayer.Common.PayBand();
            DataTable dt = ObjPayBand.GetAll();

            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["PayBandId"] = "0";
                dr["PayBandName"] = "--Select--";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                ddlPayBand.DataSource = dt;
                ddlPayBand.DataBind();
            }
            
        }
        protected void LoadEmployeeOfficialDetails()
        {
            BusinessLayer.Common.EmployeeOfficial ObjOfficial = new BusinessLayer.Common.EmployeeOfficial();
            Entity.Common.EmployeeOfficial Official = new Entity.Common.EmployeeOfficial();
            Official = ObjOfficial.GetAllByEmployeeId(EmployeeId);
            if (Official != null)
            {
                EmployeeOfficialId = Official.EmployeeOfficialId;
                ddlCategory.SelectedValue = Official.EmployeeOfficial_CategoryId.ToString();
                ddlDepartment.SelectedValue = Official.EmployeeOfficial_DepartmentId.ToString();
                ddlDesignation.SelectedValue = Official.EmployeeOfficial_DesignationId.ToString();
                ddlBranch.SelectedValue = Official.EmployeeOfficial_BranchId.ToString();
                ddlGrade.SelectedValue = Official.EmployeeOfficial_GradeId.ToString();
                //------------------
                //txtDOJ.Text = Convert.ToDateTime(txtDOJ.Text.Trim() + " 00:00:00") (Official.DOJ == DateTime.MinValue) ? DateTime.Now.ToString("dd/MM/yyyy") : Official.DOJ.ToString("dd/MM/yyyy");
                //Official.DOJ = Convert.ToDateTime(txtDOJ.Text.Trim() + " 00:00:00");
                //-----------------
                txtDOJ.Text =(Official.DOJ == DateTime.MinValue) ? DateTime.Now.ToString("dd/MM/yyyy") : Official.DOJ.ToString("dd/MM/yyyy");

                txtConfDt.Text = (Official.ConfDate == null) ? "" : Convert.ToDateTime(Official.ConfDate).ToString("dd/MM/yyyy");
                txtEffectiveDate.Text = (Official.EffectiveDate == null) ? "" : Convert.ToDateTime(Official.EffectiveDate).ToString("dd/MM/yyyy");
                ddlPTax.SelectedValue = Official.PTax;
                ddlEvaluationType.SelectedValue = Official.EvaluationType;
                txtLastEvaluationDate.Text = (Official.LastEvaluationDate == null) ? "" : Convert.ToDateTime(Official.LastEvaluationDate).ToString("dd/MM/yyyy");
                
                ddlPF.SelectedValue = Official.HasPF;
                txtPFEffDt.Text = (Official.PFEffectiveDate == null) ? "" : Convert.ToDateTime(Official.PFEffectiveDate).ToString("dd/MM/yyyy");
                txtPFEffDt.Enabled = (ddlPF.SelectedValue == "Y") ? true : false;
                txtPFNo.Text = Official.PFNo;
                txtPFNo.Enabled = (ddlPF.SelectedValue == "Y") ? true : false;

                ddlESI.SelectedValue = Official.HasESI;
                txtESIEffDt.Text = (Official.ESIEffectiveDate == null) ? "" : Convert.ToDateTime(Official.ESIEffectiveDate).ToString("dd/MM/yyyy");
                txtESIEffDt.Enabled = (ddlESI.SelectedValue == "Y") ? true : false;
                txtESINo.Text = Official.ESINo;
                txtESINo.Enabled = (ddlESI.SelectedValue == "Y") ? true : false;

                
                ddlTDS.SelectedValue = Official.HasTDS;
                txtTDSEffDt.Text = (Official.TDSEffectiveDate == null) ? "" : Convert.ToDateTime(Official.TDSEffectiveDate).ToString("dd/MM/yyyy");
                txtTDSEffDt.Enabled = (ddlTDS.SelectedValue == "Y") ? true : false;
                txtPAN.Text = Official.PANNo;
                txtPAN.Enabled = (ddlTDS.SelectedValue == "Y") ? true : false;
                
                ddlHealthCard.SelectedValue = Official.HasHealthCard;
                txtMediclaimNo.Text = Official.MediclaimNo;
                txtMediclaimNo.Enabled = (ddlHealthCard.SelectedValue == "Y") ? true : false;
                
                ddlPayBand.SelectedValue = Official.PayBandId.ToString();

                txtDateOfResign.Text = (Official.DOR == null) ? "" : Convert.ToDateTime(Official.DOR).ToString("dd/MM/yyyy");
                txtDateLeaving.Text = (Official.DOL == null) ? "" : Convert.ToDateTime(Official.DOL).ToString("dd/MM/yyyy");
                txtNoticePeriod.Text = Official.NoticePeriod.ToString();
                txtReasonLeving.Text = Official.ReasonForLeaving;
                ddlEmployeeType.SelectedValue = Official.EmployeeType;
                ddlWorkingDays.SelectedValue = Official.WorkingDays.ToString();
                txtFileNo.Text = Official.FileNo;
                txtUNANo.Text = Official.UNANo;
                txtAadhaar.Text = Official.Aadhaar;
            }
        }

        
        protected void LoadCategory()
        {
            BusinessLayer.Common.Category ObjCategory = new BusinessLayer.Common.Category();
            DataTable dt = ObjCategory.GetAll();
            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["CategoryId"] = "0";
                dr["CategoryName"] = "---Select---";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                ddlCategory.DataSource = dt;
                ddlCategory.DataBind();
            }
        }

        protected void LoadDepartment()
        {
            BusinessLayer.Common.Department ObjDepartment = new BusinessLayer.Common.Department();
            DataTable dt = ObjDepartment.GetAll();
            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["DepartmentId"] = "0";
                dr["DepartmentName"] = "---Select---";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                ddlDepartment.DataSource = dt;
                ddlDepartment.DataBind();
            }
        }

        protected void LoadDesignation()
        {
            BusinessLayer.Common.Designation ObjDesignation = new BusinessLayer.Common.Designation();
            DataTable dt = ObjDesignation.GetAll();
            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["DesignationId"] = "0";
                dr["DesignationName"] = "---Select---";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                ddlDesignation.DataSource = dt;
                ddlDesignation.DataBind();
            }
        }

        protected void LoadBranch()
        {
            BusinessLayer.Common.Branch ObjBranch = new BusinessLayer.Common.Branch();
            DataTable dt = ObjBranch.GetAll();
            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["BranchId"] = "0";
                dr["BranchName"] = "---Select---";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                ddlBranch.DataSource = dt;
                ddlBranch.DataBind();
            }
        }

        protected void LoadGrade()
        {
            BusinessLayer.Common.Grade ObjGrade = new BusinessLayer.Common.Grade();
            DataTable dt = ObjGrade.GetAll();
            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["GradeId"] = "0";
                dr["GradeName"] = "---Select---";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                ddlGrade.DataSource = dt;
                ddlGrade.DataBind();
            }
        }

        protected void LoadEmployeeDetails()
        {
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            Entity.Common.Employee Employee = new Entity.Common.Employee();
            Employee = ObjEmployee.GetAllById(EmployeeId);
            if (Employee != null)
            {
                divtitle.InnerHtml = "<h6>" + Employee.FirstName + " " + Employee.MiddleName + " " + Employee.LastName + " (" + Employee.EmpCode + ")</h6>";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                BusinessLayer.Common.EmployeeOfficial ObjOfficial = new BusinessLayer.Common.EmployeeOfficial();
                Entity.Common.EmployeeOfficial Official = new Entity.Common.EmployeeOfficial();
                Official.EmployeeOfficialId = EmployeeOfficialId;
                Official.EmployeeOfficial_EmployeeId = EmployeeId;
                Official.EmployeeOfficial_CategoryId = int.Parse(ddlCategory.SelectedValue.Trim());
                Official.EmployeeOfficial_DepartmentId = int.Parse(ddlDepartment.SelectedValue.Trim());
                Official.EmployeeOfficial_DesignationId = int.Parse(ddlDesignation.SelectedValue.Trim());
                Official.EmployeeOfficial_BranchId = int.Parse(ddlBranch.SelectedValue.Trim());
                Official.EmployeeOfficial_GradeId = int.Parse(ddlGrade.SelectedValue.Trim());

                string[] DOJ = txtDOJ.Text.Trim().Split('/');
                Official.DOJ = Convert.ToDateTime(DOJ[1].Trim() + "/" + DOJ[0].Trim() + "/" + DOJ[2].Trim() + " " + DateTime.Now.ToString("hh:mm:ss"));

                if (txtConfDt.Text.Trim().Length == 0)
                {
                    Official.ConfDate = null;
                }
                else
                {
                    string[] DOConf = txtConfDt.Text.Trim().Split('/');
                    Official.ConfDate = Convert.ToDateTime(DOConf[1].Trim() + "/" + DOConf[0].Trim() + "/" + DOConf[2].Trim() + " " + DateTime.Now.ToString("hh:mm:ss"));
                }


                if (txtEffectiveDate.Text.Trim().Length == 0)
                {
                    Official.EffectiveDate = null;
                }
                else
                {
                    string[] DOEfft = txtEffectiveDate.Text.Trim().Split('/');
                    Official.EffectiveDate = Convert.ToDateTime(DOEfft[1].Trim() + "/" + DOEfft[0].Trim() + "/" + DOEfft[2].Trim() + " " + DateTime.Now.ToString("hh:mm:ss"));
                }

                Official.PTax = ddlPTax.SelectedValue.Trim();
                Official.EvaluationType = ddlEvaluationType.SelectedValue.Trim();

                if (txtLastEvaluationDate.Text.Trim().Length == 0)
                {
                    Official.LastEvaluationDate = null;
                }
                else
                {
                    string[] DOLastEvaldate = txtLastEvaluationDate.Text.Trim().Split('/');
                    Official.LastEvaluationDate = Convert.ToDateTime(DOLastEvaldate[1].Trim() + "/" + DOLastEvaldate[0].Trim() + "/" + DOLastEvaldate[2].Trim() + " " + DateTime.Now.ToString("hh:mm:ss"));
                }

                Official.HasPF = ddlPF.SelectedValue.Trim();
                if (txtPFEffDt.Text.Trim().Length == 0)
                {
                    Official.PFEffectiveDate = null;
                }
                else
                {
                    string[] PFEfftDate = txtPFEffDt.Text.Trim().Split('/');
                    Official.PFEffectiveDate = Convert.ToDateTime(PFEfftDate[1].Trim() + "/" + PFEfftDate[0].Trim() + "/" + PFEfftDate[2].Trim() + " " + DateTime.Now.ToString("hh:mm:ss"));
                }

                Official.PFNo = txtPFNo.Text.Trim();
                Official.UNANo = txtUNANo.Text.Trim();
                Official.Aadhaar = txtAadhaar.Text.Trim();
                Official.HasESI = ddlESI.SelectedValue.Trim();
                if (txtESIEffDt.Text.Trim().Length == 0)
                {
                    Official.ESIEffectiveDate = null;
                }
                else
                {
                    string[] ESIEffDt = txtESIEffDt.Text.Trim().Split('/');
                    Official.ESIEffectiveDate = Convert.ToDateTime(ESIEffDt[1].Trim() + "/" + ESIEffDt[0].Trim() + "/" + ESIEffDt[2].Trim() + " " + DateTime.Now.ToString("hh:mm:ss"));
                }

                Official.ESINo = txtESINo.Text.Trim();
                Official.HasTDS = ddlTDS.SelectedValue.Trim();
                if (txtTDSEffDt.Text.Trim().Length == 0)
                {
                    Official.TDSEffectiveDate = null;
                }
                else
                {
                    string[] TDSEffDt = txtTDSEffDt.Text.Trim().Split('/');
                    Official.TDSEffectiveDate = Convert.ToDateTime(TDSEffDt[1].Trim() + "/" + TDSEffDt[0].Trim() + "/" + TDSEffDt[2].Trim() + " " + DateTime.Now.ToString("hh:mm:ss"));
                }

                Official.PANNo = txtPAN.Text.Trim();
                Official.HasHealthCard = ddlHealthCard.SelectedValue.Trim();
                Official.MediclaimNo = txtMediclaimNo.Text.Trim();

                if (txtDateOfResign.Text.Trim().Length == 0)
                {
                    Official.DOR = null;
                }
                else
                {
                    string[] DOR = txtDateOfResign.Text.Trim().Split('/');
                    Official.DOR = Convert.ToDateTime(DOR[1].Trim() + "/" + DOR[0].Trim() + "/" + DOR[2].Trim() + " " + DateTime.Now.ToString("hh:mm:ss"));
                }

                if (txtDateLeaving.Text.Trim().Length == 0)
                {
                    Official.DOL = null;
                }
                else
                {
                    string[] DOL = txtDateLeaving.Text.Trim().Split('/');
                    Official.DOL = Convert.ToDateTime(DOL[1].Trim() + "/" + DOL[0].Trim() + "/" + DOL[2].Trim() + " " + DateTime.Now.ToString("hh:mm:ss"));
                }

                Official.NoticePeriod = (txtNoticePeriod.Text.Trim().Length == 0) ? 0 : int.Parse(txtNoticePeriod.Text.Trim());
                Official.ReasonForLeaving = txtReasonLeving.Text.Trim();

                Official.CreatedBy = int.Parse(HttpContext.Current.User.Identity.Name);
                Official.ModifiedBy = int.Parse(HttpContext.Current.User.Identity.Name);
                Official.PayBandId = int.Parse(ddlPayBand.SelectedValue.Trim());
                Official.EmployeeType = ddlEmployeeType.SelectedValue.Trim();
                Official.WorkingDays = int.Parse(ddlWorkingDays.SelectedValue.Trim());
                Official.FileNo = txtFileNo.Text.Trim();

                ObjOfficial.Save(Official);
                EmployeeOfficialId = Official.EmployeeOfficialId;
                Page.ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: RefreshParent(); ", true);
            }
            else
            {
                Message.Show = true;
            }
           
        }

        protected bool Validate()
        {
            bool result = false;
            string ErrorMsg = "";

            if (ddlPF.SelectedValue == "Y" && txtPFEffDt.Text.Trim() == "")
            {
                result = false;
                ErrorMsg = "You Must Provide PF Effective Date When PF Is Activated";
            }
            else if (ddlESI.SelectedValue == "Y" && txtESIEffDt.Text.Trim() == "")
            {
                result = false;
                ErrorMsg = "You Must Provide ESI Effective Date When ESI Is Activated";
            }
            else if (ddlTDS.SelectedValue == "Y" && txtTDSEffDt.Text.Trim() == "")
            {
                result = false;
                ErrorMsg = "You Must Provide TDS Effective Date When TDS Is Activated";
            }
            else { result = true; }

            if (!result)
            {
                Message.IsSuccess = false;
                Message.Text = ErrorMsg;
            }
            return result;
        }

        protected void ddlPF_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPF.SelectedValue == "Y")
            {
                txtPFEffDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtPFNo.Text = "";
                txtPFEffDt.Enabled = true;
                txtPFNo.Enabled = true;
            }
            else
            {
                txtPFEffDt.Text = "";
                txtPFNo.Text = "";
                txtPFEffDt.Enabled = false;
                txtPFNo.Enabled = false;
            }
        }

        protected void ddlESI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlESI.SelectedValue == "Y")
            {
                txtESIEffDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtESINo.Text = "";
                txtESIEffDt.Enabled = true;
                txtESINo.Enabled = true;
            }
            else
            {
                txtESIEffDt.Text = "";
                txtESINo.Text = "";
                txtESIEffDt.Enabled = false;
                txtESINo.Enabled = false;
            }
        }

        protected void ddlTDS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTDS.SelectedValue == "Y")
            {
                txtTDSEffDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtPAN.Text = "";
                txtTDSEffDt.Enabled = true;
                txtPAN.Enabled = true;
            }
            else
            {
                txtTDSEffDt.Text = "";
                txtPAN.Text = "";
                txtTDSEffDt.Enabled = false;
                txtPAN.Enabled = false;
            }
        }

        protected void ddlHealthCard_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlHealthCard.SelectedValue == "Y")
            {
                txtMediclaimNo.Text = "";
                txtMediclaimNo.Enabled = true;
            }
            else
            {
                txtMediclaimNo.Text = "";
                txtMediclaimNo.Enabled = false;
            }
        }

    }
}
