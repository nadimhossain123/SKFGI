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
    public partial class DirectorClaimApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.IsInRole(Entity.Common.Utility.CLAIM_APPROVE_BY_DIRECTOR))
                {
                    Response.Redirect("../Unauthorized.aspx");
                }
                Message.Show = false;
                LoadRequestList();
            }
        }

        protected void LoadRequestList()
        {
            string FromDate = txtFromDate.Text.Trim();
            if (FromDate.Length != 0)
            {
                string[] ArrFrom = FromDate.Split('/');
                FromDate = ArrFrom[1].Trim() + "/" + ArrFrom[0].Trim() + "/" + ArrFrom[2].Trim() + " 00:00:00";
            }
            string ToDate = txtToDate.Text.Trim();
            if (ToDate.Length != 0)
            {
                string[] ArrTo = ToDate.Split('/');
                ToDate = ArrTo[1].Trim() + "/" + ArrTo[0].Trim() + "/" + ArrTo[2].Trim() + " 23:59:59";
            }

            BusinessLayer.HR.ExpenseClaim ObjClaim = new BusinessLayer.HR.ExpenseClaim();
            DataTable dt = ObjClaim.GetAllForDirectorApproval(FromDate, ToDate);
            if (dt != null)
            {
                dgvClaim.DataSource = dt;
                dgvClaim.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Message.Show = false;
            LoadRequestList();
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            Save(true);
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            Save(false);
        }

        protected void Save(bool IsApproved)
        {
            DataTable DTClaim = new DataTable();
            DTClaim.Columns.Add("ExpenseClaimId", typeof(int));
            DTClaim.AcceptChanges();
            DataRow DR;

            foreach (GridViewRow GVR in dgvClaim.Rows)
            {
                if (GVR.RowType == DataControlRowType.DataRow)
                {
                    if (((CheckBox)GVR.FindControl("ChkSelect")).Checked == true)
                    {
                        DR = DTClaim.NewRow();
                        DR["ExpenseClaimId"] = int.Parse(dgvClaim.DataKeys[GVR.RowIndex].Value.ToString());
                        DTClaim.Rows.Add(DR);
                        DTClaim.AcceptChanges();

                    }
                }
            }

            BusinessLayer.HR.ExpenseClaim ObjClaim=new BusinessLayer.HR.ExpenseClaim();
            ObjClaim.SaveDirectorApproval(DTClaim, IsApproved);
            Message.IsSuccess = true;
            Message.Text = "Claim Saved Successfully";
            Message.Show = true;
            LoadRequestList();
        }
    }
    }

