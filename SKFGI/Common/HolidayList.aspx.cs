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
using System.Data;

namespace CollegeERP.Common
{
    public partial class HolidayList : System.Web.UI.Page
    {
        public int HolidayListId
        {
            get { return Convert.ToInt32(ViewState["HolidayListId"]); }
            set { ViewState["HolidayListId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.HOLIDAY_CONFIG))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                LoadHolidayList();
                if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim().Length > 0)
                {
                    HolidayListId = Convert.ToInt32(Request.QueryString["id"].Trim());
                    LoadHolidayDetails();
                }
                else
                {
                    ClearControls();
                }
            }
        }

        protected void LoadHolidayList()
        {
            BusinessLayer.Common.HolidayList ObjHoliday = new BusinessLayer.Common.HolidayList();
            DataTable dt = ObjHoliday.GetAll();
            if (dt != null)
            {
                dgvHoliday.DataSource = dt;
                dgvHoliday.DataBind();
            }
        }

        protected void LoadHolidayDetails()
        {
            BusinessLayer.Common.HolidayList ObjHoliday = new BusinessLayer.Common.HolidayList();
            Entity.Common.HolidayList Holiday = new Entity.Common.HolidayList();
            Holiday = ObjHoliday.GetAllById(HolidayListId);
            if (Holiday != null)
            {
                txtName.Text = Holiday.HolidayName;
                txtRemarks.Text = Holiday.HolidayRemarks;
                txtHolidayDate.Text = Holiday.HolidayDate.ToString("dd/MM/yyyy");
                Message.Show = false;
                btnSave.Text = "Update";
            }
        }

        protected void ClearControls()
        {
            HolidayListId = 0;
            txtName.Text ="";
            txtRemarks.Text = "";
            txtHolidayDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            Message.Show = false;
            btnSave.Text = "Save";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.HolidayList ObjHoliday = new BusinessLayer.Common.HolidayList();
            Entity.Common.HolidayList Holiday = new Entity.Common.HolidayList();
            Holiday.HolidayListId = HolidayListId;
            Holiday.HolidayName = txtName.Text.Trim();
            Holiday.HolidayRemarks = txtRemarks.Text.Trim();
            string[] Date = txtHolidayDate.Text.Trim().Split('/');
            Holiday.HolidayDate = Convert.ToDateTime(Date[1].Trim() + "/" + Date[0].Trim() + "/" + Date[2].Trim() + " 00:00:00");

            Holiday.HolidayList_UserId = int.Parse(HttpContext.Current.User.Identity.Name);
            Holiday.HolidayList_ModUserId = int.Parse(HttpContext.Current.User.Identity.Name);
            int RowsAffected = ObjHoliday.Save(Holiday);
            if (RowsAffected != -1)
            {
                Message.IsSuccess = true;
                Message.Text = "Holiday Information Saved Successfully";
                ClearControls();
                LoadHolidayList();
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text ="Can't Save. Duplicate Holiday Date Is Not Allowed";
            }
            Message.Show = true;
        }

       
        protected void dgvHoliday_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvHoliday.PageIndex = e.NewPageIndex;
            LoadHolidayList();
        }

        protected void dgvHoliday_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int Id = Convert.ToInt32(dgvHoliday.DataKeys[e.NewEditIndex].Value);
            Response.Redirect("HolidayList.aspx?id=" + Id);
        }

        protected void dgvHoliday_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = Convert.ToInt32(dgvHoliday.DataKeys[e.RowIndex].Value);
            BusinessLayer.Common.HolidayList ObjHoliday = new BusinessLayer.Common.HolidayList();
            ObjHoliday.Delete(Id);
            LoadHolidayList();
        }
    }
}
