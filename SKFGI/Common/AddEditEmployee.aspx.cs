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
    public partial class AddEditEmployee : System.Web.UI.Page
    {
        public int EmployeeId
        {
            get { return Convert.ToInt32(ViewState["EmployeeId"]); }
            set { ViewState["EmployeeId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.EMPLOYEE_SETTING))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadCompany();
                LoadEmployeeList();
                if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim().Length > 0)
                {
                    EmployeeId = Convert.ToInt32(Request.QueryString["id"].Trim());
                    LoadLeaveManager();//Claim Manager will also included in this method
                    LoadEmployeeDetails();
                }
                else
                {
                    ClearControls();
                }
            }
        }

        protected void LoadCompany()
        {
            BusinessLayer.Common.Company ObjCompany = new BusinessLayer.Common.Company();
            DataTable dt = ObjCompany.GetAll();
            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["CompanyId"] = "0";
                dr["CompanyName"] = "--Select Company--";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                ddlCompany.DataSource = dt;
                ddlCompany.DataBind();
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

                ddlClaimApprover.DataSource = dt;
                ddlClaimApprover.DataBind();
            }
        }

        protected void LoadEmployeeList()
        {
            string EmpCode = txtEmpCode.Text.Trim();
            string FName = txtFNameSearch.Text.Trim();
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            DataTable dt = ObjEmployee.GetAll(EmpCode, FName);
            if (dt != null)
            {
                dgvEmployee.DataSource = dt;
                dgvEmployee.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadEmployeeList();
        }

        protected void LoadEmployeeDetails()
        {
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            Entity.Common.Employee Employee = new Entity.Common.Employee();
            Employee = ObjEmployee.GetAllById(EmployeeId);
            if (Employee != null)
            {
                txtFName.Text = Employee.FirstName;
                txtMName.Text = Employee.MiddleName;
                txtLName.Text = Employee.LastName;
                txtEmpCode_Entry.Text = Employee.EmpCode;
                txtEmpCode_Entry.ReadOnly = true;
                txtDOB.Text = Employee.DateOfBirth.ToString("dd/MM/yyyy");
                ddlGender.SelectedValue = Employee.Gender;
                ddlMaritalStatus.SelectedValue = Employee.MaritalStatus;
                txtBloodGroup.Text = Employee.BloodGroup;
                ddlNationality.SelectedValue = Employee.Nationality;
                txtCast.Text = Employee.Cast;
                txtReligion.Text = Employee.Religion;

                txtCorrespondesAdd.Text = Employee.CorrespondanceAddress;
                txtCorrespondesCity.Text = Employee.CorrespondanceAddressCity;
                txtCorrespondesState.Text = Employee.CorrespondanceAddressState;
                txtCorrespondesPIN.Text = Employee.CorrespondanceAddressPin;

                txtPermanentAdd.Text = Employee.PermanentAddress;
                txtPermanentCity.Text = Employee.PermanentAddressCity;
                txtPermanentPIN.Text = Employee.PermanentAddressPin;
                txtPermanentState.Text = Employee.PermanentAddressState;

                txtCountry.Text = Employee.Country;
                txtContactNo1.Text = Employee.ContactNo1;
                txtContactNo2.Text = Employee.ContactNo2;
                txtEmail1.Text = Employee.ContactEmail1;
                txtEmail2.Text = Employee.ContactEmail2;
                txtPassportNo.Text = Employee.PassportNo;
                txtPlaceOfIssue.Text = Employee.PassportPlaceOfIssue;

                txtIssuedate.Text = (Employee.PassportIssueDate == null) ? "" : Convert.ToDateTime(Employee.PassportIssueDate).ToString("dd/MM/yyyy");
                txtExpiryDate.Text = (Employee.PassportExpiryDate == null) ? "" : Convert.ToDateTime(Employee.PassportExpiryDate).ToString("dd/MM/yyyy");

                ddlPaymode.SelectedValue = Employee.PayMode;
                
                txtBankName.Text = Employee.BankName;
                txtBranchAddress.Text = Employee.BankBranchAddress;
                txtACNo.Text = Employee.BankAcNo;
                txtIFSCode.Text = Employee.BankIFSCode;
                ChkIsActive.Checked = Employee.IsActive;
                ChkIsPermanent.Checked = Employee.IsPermanent;
                ddlCompany.SelectedValue = Employee.CompanyId.ToString();
                ddlLeaveManager.SelectedValue = Employee.LeaveManagerId.ToString();
                ddlClaimApprover.SelectedValue = Employee.ClaimApproverId.ToString();
                txtContractPeriod.Text = Employee.ContractPeriod.ToString();

                if (ChkIsPermanent.Checked)
                    txtContractPeriod.Enabled = false;
                else
                    txtContractPeriod.Enabled = true;

                btnSave.Text = "Update";
                Message.Show = false;
            }
        }

        protected void ClearControls()
        {
            EmployeeId = 0;
            txtFName.Text = "";
            txtMName.Text = "";
            txtLName.Text = "";
            txtEmpCode_Entry.Text = "";
            txtEmpCode_Entry.ReadOnly = false;
            txtDOB.Text = "";
            ddlGender.SelectedIndex = 0;
            ddlMaritalStatus.SelectedIndex = 0;
            txtBloodGroup.Text = "";
            ddlNationality.SelectedIndex = 0;
            txtCast.Text = "";
            txtReligion.Text = "";

            txtCorrespondesAdd.Text = "";
            txtCorrespondesCity.Text = "";
            txtCorrespondesState.Text = "";
            txtCorrespondesPIN.Text = "";

            txtPermanentAdd.Text = "";
            txtPermanentCity.Text = "";
            txtPermanentPIN.Text = "";
            txtPermanentState.Text = "";

            txtCountry.Text = "";
            txtContactNo1.Text = "";
            txtContactNo2.Text = "";
            txtEmail1.Text = "";
            txtEmail2.Text = "";
            txtPassportNo.Text = "";
            txtPlaceOfIssue.Text = "";

            txtIssuedate.Text = "";
            txtExpiryDate.Text = "";

            ddlPaymode.SelectedIndex = 0;
            txtBankName.Text = "State Bank Of India";
            txtBranchAddress.Text = "Mankundu";
            txtACNo.Text = "";
            txtIFSCode.Text = "SBI1002";
            ChkIsActive.Checked = true;
            ChkIsPermanent.Checked = true;
            txtContractPeriod.Enabled = false;

            ddlCompany.SelectedValue = "0";
            txtContractPeriod.Text = "";

            LoadLeaveManager();

            btnSave.Text = "Save";
            Message.Show = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            Entity.Common.Employee Employee = new Entity.Common.Employee();
            Employee.EmployeeId = EmployeeId;
            Employee.FirstName = txtFName.Text.Trim();
            Employee.MiddleName = txtMName.Text.Trim();
            Employee.LastName = txtLName.Text.Trim();
            Employee.EmpCode = txtEmpCode_Entry.Text.Trim();

            string[] DOB = txtDOB.Text.Trim().Split('/');
            Employee.DateOfBirth = Convert.ToDateTime(DOB[1].Trim() + "/" + DOB[0].Trim() + "/" + DOB[2].Trim() + " 00:00:00");

            Employee.Gender = ddlGender.SelectedValue.Trim();
            Employee.MaritalStatus = ddlMaritalStatus.SelectedValue.Trim();
            Employee.BloodGroup = txtBloodGroup.Text.Trim();
            Employee.Nationality = ddlNationality.SelectedValue.Trim();
            Employee.Cast = txtCast.Text.Trim();
            Employee.Religion = txtReligion.Text.Trim();

            Employee.CorrespondanceAddress = txtCorrespondesAdd.Text.Trim();
            Employee.CorrespondanceAddressCity = txtCorrespondesCity.Text.Trim();
            Employee.CorrespondanceAddressPin = txtCorrespondesPIN.Text.Trim();
            Employee.CorrespondanceAddressState = txtCorrespondesState.Text.Trim();

            Employee.PermanentAddress = txtPermanentAdd.Text.Trim();
            Employee.PermanentAddressCity = txtPermanentCity.Text.Trim();
            Employee.PermanentAddressPin = txtPermanentPIN.Text.Trim();
            Employee.PermanentAddressState = txtPermanentState.Text.Trim();

            Employee.Country = txtCountry.Text.Trim();
            Employee.ContactNo1 = txtContactNo1.Text.Trim();
            Employee.ContactNo2 = txtContactNo2.Text.Trim();
            Employee.ContactEmail1 = txtEmail1.Text.Trim();
            Employee.ContactEmail2 = txtEmail2.Text.Trim();

            Employee.PassportNo = txtPassportNo.Text.Trim();
            Employee.PassportPlaceOfIssue = txtPlaceOfIssue.Text.Trim();

            if (txtIssuedate.Text.Trim().Length == 0)
            {
                Employee.PassportIssueDate = null;
            }
            else
            {
                string[] DOI = txtIssuedate.Text.Trim().Split('/');
                Employee.PassportIssueDate = Convert.ToDateTime(DOI[1].Trim() + "/" + DOI[0].Trim() + "/" + DOI[2].Trim() + " 00:00:00");
            }

            if (txtExpiryDate.Text.Trim().Length == 0)
            {
                Employee.PassportExpiryDate = null;
            }
            else
            {
                string[] DOE = txtExpiryDate.Text.Trim().Split('/');
                Employee.PassportExpiryDate = Convert.ToDateTime(DOE[1].Trim() + "/" + DOE[0].Trim() + "/" + DOE[2].Trim() + " 00:00:00");
            }
            Employee.PayMode = ddlPaymode.SelectedValue.Trim();
            Employee.BankName = txtBankName.Text.Trim();
            Employee.BankBranchAddress = txtBranchAddress.Text.Trim();
            Employee.BankAcNo = txtACNo.Text.Trim();
            Employee.BankIFSCode = txtIFSCode.Text.Trim();
            Employee.IsActive = ChkIsActive.Checked;
            Employee.IsPermanent = ChkIsPermanent.Checked;
            Employee.CompanyId = int.Parse(ddlCompany.SelectedValue.Trim());
            Employee.LeaveManagerId = int.Parse(ddlLeaveManager.SelectedValue.Trim());
            Employee.ClaimApproverId = int.Parse(ddlClaimApprover.SelectedValue.Trim());
            Employee.ContractPeriod = (txtContractPeriod.Text.Trim().Length == 0) ? 0 : int.Parse(txtContractPeriod.Text.Trim());

            Employee.CreatedBy = int.Parse(HttpContext.Current.User.Identity.Name);
            Employee.ModifiedBy = int.Parse(HttpContext.Current.User.Identity.Name);

            string ff = "";
            if (uploadImage.PostedFile.FileName != null && uploadImage.PostedFile.ContentLength > 0)
            {
                string fn = uploadImage.FileName;
                string fileExt = System.IO.Path.GetExtension(fn);
                if (fileExt == ".jpg" || fileExt == ".bmp" || fileExt == ".JPG")
                {
                    string sm = Server.MapPath("");
                    Employee.Photo = fileExt;
                    int RowsAffected=ObjEmployee.Save(Employee);
                    if (RowsAffected != -1)
                    {
                        ff = (sm + "\\EmployeePhoto\\" + Employee.EmployeeId + fileExt);
                        uploadImage.PostedFile.SaveAs(ff);

                        ClearControls();
                        LoadEmployeeList();
                        Message.IsSuccess = true;
                        Message.Text = "Employee Saved Successfully";
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Duplicate Employee Code Is Not Allowed";
                    }


                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Please Select JPG or BMP File";

                }
            }
            else
            {
                Employee.Photo = "";
                int RowsAffected= ObjEmployee.Save(Employee);
                if (RowsAffected != -1)
                {
                    ClearControls();
                    LoadEmployeeList();
                    Message.IsSuccess = true;
                    Message.Text = "Employee Saved Successfully";
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Duplicate Employee Code Is Not Allowed";
                }
            }

            Message.Show = true;

        }

        protected void dgvEmployee_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int Id = Convert.ToInt32(dgvEmployee.DataKeys[e.NewEditIndex].Value);
            Response.Redirect("AddEditEmployee.aspx?id=" + Id);
        }

        protected void dgvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvEmployee.PageIndex = e.NewPageIndex;
            LoadEmployeeList();
        }

        protected void dgvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int EmpId = Convert.ToInt32(dgvEmployee.DataKeys[e.Row.RowIndex].Value.ToString());
                ((LinkButton)e.Row.FindControl("lnkOfficial")).Attributes.Add("onclick", "javascript:openpopup('PopupOfficialDetails.aspx?EmployeeId=" + EmpId + "');");
                ((LinkButton)e.Row.FindControl("lnkQualification")).Attributes.Add("onclick", "javascript:openpopup('PopupQualification.aspx?EmployeeId=" + EmpId + "');");
                ((LinkButton)e.Row.FindControl("lnkFamily")).Attributes.Add("onclick", "javascript:openpopup('PopupFamilyDetails.aspx?EmployeeId=" + EmpId + "');");
                ((LinkButton)e.Row.FindControl("lnkWork")).Attributes.Add("onclick", "javascript:openpopup('PopupWorkDetails.aspx?EmployeeId=" + EmpId + "');");
                ((LinkButton)e.Row.FindControl("lnkRole")).Attributes.Add("onclick", "javascript:openRolepopup('PopUpRole.aspx?EmployeeId=" + EmpId + "');");
                ((Image)e.Row.FindControl("ImgIDCard")).Attributes.Add("onclick", "javascript:openRolepopup('EmployeeIDCard.aspx?EmployeeId=" + EmpId + "');");
                ((LinkButton)e.Row.FindControl("lnkTerms")).Attributes.Add("onclick", "javascript:openpopup('EmployeeTermsAndCondition.aspx?EmployeeId=" + EmpId + "');");

                ((ImageButton)e.Row.FindControl("btnPrint")).Attributes.Add("onclick", "javascript:openpopup('AppointmentLetter.aspx?EmployeeId=" + EmpId + "');");
            }
        }

        protected void ChkIsPermanent_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkIsPermanent.Checked)
            {
                txtContractPeriod.Text = "";
                txtContractPeriod.Enabled = false;
            }
            else
            {
                txtContractPeriod.Enabled = true;
                txtContractPeriod.Focus();
            }
        }
    }
}
