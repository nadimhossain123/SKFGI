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

namespace CollegeERP.Student
{
    public partial class LibraryFine : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);

        public int LibraryFineId
        {
            get { return Convert.ToInt32(ViewState["LibraryFineId"]); }
            set { ViewState["LibraryFineId"] = value; }
        }
        ListItem li = new ListItem("---SELECT---", "0");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.LIBRARY_FINE))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadFeesHead();
                LoadActiveStudent();
                LoadSemNo();
                ClearControls();
                LoadFineList();
            }
        }

        protected void LoadFeesHead()
        {
            BusinessLayer.Student.StreamGroup ObjFees = new BusinessLayer.Student.StreamGroup();
            DataTable DT = ObjFees.GetOtherFeesHead();
            if (DT != null)
            {
                ddlFeesHead.DataSource = DT;
                ddlFeesHead.DataBind();
            }
            ddlFeesHead.Items.Insert(0, li);
        }

        protected void LoadActiveStudent()
        {
            BusinessLayer.Student.LibraryFine ObjFine = new BusinessLayer.Student.LibraryFine();
            DataView DV = new DataView(ObjFine.GetApprovedStudentList());
            DV.RowFilter = "company_id=" + Convert.ToInt32(Session["CompanyId"].ToString()) + " and active=1";

            if (DV != null)
            {
                ddlStudent.DataSource = DV;
                ddlStudent.DataBind();
            }
            ddlStudent.Items.Insert(0, li);
        }

        protected void ClearControls()
        {
            LibraryFineId = 0;
            Message.Show = false;
            btnSave.Text = "Save";

            ddlStudent.SelectedIndex = 0;
            ddlStudent.Enabled = true;
            ddlFeesHead.SelectedIndex = 0;
            ddlFeesHead.Enabled = true;

            ddlSemNo.SelectedIndex = 0;
            ddlSemNo.Enabled = true;

            txtReason.Text = "";
            txtAmount.Text = "";
            txtAmount.Enabled = true;
            txtFrom.Text = DateTime.Now.AddDays(-7).ToString("dd MMM yyyy");
            txtTo.Text = DateTime.Now.ToString("dd MMM yyyy");

            btnPrint.Attributes.Add("onclick", "javascript:alert('Sorry! No data to print'); return false;");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void LoadFineList()
        {
            string VoucherNo = txtVoucherNo.Text.Trim();
            DateTime From = Convert.ToDateTime(txtFrom.Text.Trim() + " 00:00:00");
            DateTime To = Convert.ToDateTime(txtTo.Text.Trim() + " 23:59:59");

            BusinessLayer.Student.LibraryFine ObjFine = new BusinessLayer.Student.LibraryFine();
            DataTable dt = ObjFine.GetAll(VoucherNo, From, To);
            if (dt != null)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "company_id=" + Convert.ToInt32(Session["CompanyId"].ToString());

                dgvFine.DataSource = dv.ToTable();
                dgvFine.DataBind();
            }
        }

        protected void btnSearchList_Click(object sender, EventArgs e)
        {
            LoadFineList();
        }

        protected void dgvFine_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnDelete = ((ImageButton)e.Row.FindControl("btnDelete"));
                int FineId = Convert.ToInt32(dgvFine.DataKeys[e.Row.RowIndex].Value.ToString());
                ((ImageButton)e.Row.FindControl("btnPrint")).Attributes.Add("onclick", "javascript:openpopup('PopUpLibraryFineVoucher.aspx?Id=" + FineId + "');");

                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.LIBRARY_FINE_DELETE_BUTTON))
                {
                    btnDelete.Visible = false;
                }
                else
                {
                    btnDelete.Visible = true;
                }
            }
        }

        protected void dgvFine_RowEditing(object sender, GridViewEditEventArgs e)
        {
            LibraryFineId = int.Parse(dgvFine.DataKeys[e.NewEditIndex].Value.ToString());
            LoadFineDetails();
        }

        protected void LoadFineDetails()
        {
            BusinessLayer.Student.LibraryFine ObjFine = new BusinessLayer.Student.LibraryFine();
            Entity.Student.LibraryFine Fine = new Entity.Student.LibraryFine();
            Fine = ObjFine.GetAllById(LibraryFineId);
            if (Fine != null)
            {
                ddlStudent.SelectedValue = Fine.StudentId.ToString();
                ddlStudent.Enabled = false;
                LoadSemNo();

                txtReason.Text = Fine.ReasonForFine;
                txtAmount.Text = Fine.FineAmount.ToString("#0.00");
                ddlSemNo.SelectedValue = Fine.SemNo.ToString();

                ddlSemNo.Enabled = false;
                txtAmount.Enabled = false;
                ddlFeesHead.Enabled = false;

                Message.Show = false;
                btnSave.Text = "Update";
                btnPrint.Attributes.Add("onclick", "javascript:openpopup('PopUpLibraryFineVoucher.aspx?Id=" + LibraryFineId + "');");

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ddlStudent.SelectedValue != "0" && ddlStudent.Text != string.Empty)
            {
                string strValues = DateTime.Now.ToString("dd MMM yyyy");
                strValues += chr.ToString() + "";
                DataSet ds_fn = gf.ExecuteSelectSP("spSelect_GetFnYear", strValues);

                if (ds_fn.Tables[0].Rows[0]["FinYearID"].ToString() == Session["FinYrID"].ToString().Trim())
                {
                    BusinessLayer.Student.LibraryFine ObjFine = new BusinessLayer.Student.LibraryFine();
                    Entity.Student.LibraryFine Fine = new Entity.Student.LibraryFine();
                    Fine.LibraryFineId = LibraryFineId;
                    Fine.StudentId = int.Parse(ddlStudent.SelectedValue.Trim());
                    Fine.ReasonForFine = txtReason.Text.Trim();
                    Fine.FineAmount = decimal.Parse(txtAmount.Text.Trim());

                    Fine.CreatedBy = int.Parse(HttpContext.Current.User.Identity.Name);
                    Fine.ModifiedBy = int.Parse(HttpContext.Current.User.Identity.Name);
                    Fine.FeesHeadId = int.Parse(ddlFeesHead.SelectedValue.Trim());
                    Fine.SemNo = Convert.ToInt32(ddlSemNo.SelectedValue.Trim());

                    Fine.CompanyID_FK = Convert.ToInt32(Session["CompanyId"].ToString());
                    Fine.BranchID_FK = Convert.ToInt32(Session["BranchId"].ToString());
                    Fine.FinYearID_FK = Convert.ToInt32(Session["FinYrID"].ToString());
                    Fine.DataFlow = Convert.ToInt32(Session["DataFlow"].ToString());

                    ObjFine.Save(Fine);
                    ClearControls();
                    LoadFineList();

                    btnPrint.Attributes.Add("onclick", "javascript:openpopup('PopUpLibraryFineVoucher.aspx?Id=" + Fine.LibraryFineId + "');");
                    Fine = ObjFine.GetAllById(Fine.LibraryFineId);
                    Message.IsSuccess = true;
                    Message.Text = "Voucher No " + Fine.VoucherNo + " Is Generated / Modified";
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Please select a Date Between " + Session["SesFromDate"].ToString() + " & " + Session["SesToDate"].ToString();
                }
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Please Select Student";
            }
            Message.Show = true;

        }

        protected void ddlStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSemNo();
        }

        protected void LoadSemNo()
        {
            ddlSemNo.Items.Clear();
            int CourseId = BusinessLayer.Student.SearchStudent.GetStudentCourseId(Convert.ToInt32(ddlStudent.SelectedValue));

            ListItem lst;
            int LastSemNo = 0;
            if (CourseId == 1 || CourseId == 3) //means MBA or MTech
            {
                LastSemNo = 4;
            }
            else if (CourseId == 2) // means BTech
            {
                LastSemNo = 8;
            }
            else if (CourseId == 4) //Diploma
            {
                LastSemNo = 6;
            }

            for (int i = 1; i <= LastSemNo; i++)
            {
                lst = new ListItem("Sem-" + i.ToString(), i.ToString());
                ddlSemNo.Items.Add(lst);
            }

            lst = new ListItem("--Select--", "0");
            ddlSemNo.Items.Insert(0, lst);
            ddlSemNo.SelectedValue = "0";
        }

        protected void dgvFine_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void dgvFine_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = Convert.ToInt32(dgvFine.DataKeys[e.RowIndex].Value.ToString());
            BusinessLayer.Student.LibraryFine ObjLibraryFine = new BusinessLayer.Student.LibraryFine();
            ObjLibraryFine.LibraryFineDelete(Id);
            LoadFineList();
        }
    }
}
