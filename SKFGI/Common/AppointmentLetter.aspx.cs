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
using System.Text;
using iTextSharp;
using iTextSharp.text.api;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Collections.Generic;
using System.Net;

namespace CollegeERP.Common
{
    public partial class AppointmentLetter : System.Web.UI.Page
    {
        public int EmployeeId
        {
            get { return Convert.ToInt32(ViewState["EmployeeId"]); }
            set { ViewState["EmployeeId"] = value; }
        }
        public int i;

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
                if (Request.QueryString["EmployeeId"] != null && Request.QueryString["EmployeeId"].Trim().Length > 0)
                {
                    EmployeeId = Convert.ToInt32(Request.QueryString["EmployeeId"].Trim());
                    LoadAppointmentLetter();
                   
                }
            }
        }

        protected void LoadAppointmentLetter()
        {
            BusinessLayer.Common.AppointmentLetter ObjAppointment = new BusinessLayer.Common.AppointmentLetter();
            Entity.Common.AppointmentLetter Appointment = new Entity.Common.AppointmentLetter();
            Appointment.EmployeeId = EmployeeId;
            Appointment.IssuedBy = int.Parse(HttpContext.Current.User.Identity.Name);
            DataSet ds = ObjAppointment.GetAppointmentLetter(Appointment);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ltrRefNo.Text = "<b>Ref No: </b>" + ds.Tables[0].Rows[0]["RefNo"].ToString();
                ltrDate.Text = "<b>Date: </b>" + ds.Tables[0].Rows[0]["IssueDate"].ToString();
                ltrHeader.Text = HeaderContent(ds);
                dgvSalary.DataSource = GetSalaryHead(ds);
                dgvSalary.DataBind();

                ((Literal)dgvSalary.FooterRow.FindControl("ltrCTC")).Text = "<b>: Rs." + ds.Tables[0].Rows[0]["CTC"].ToString() + "/-</b>";

                ltrTerms.Text = TermsContent(ds);
                //DownloadFile("pdf", "pdf");
                DownloadFile("msword", "doc");
            }
            else
            {
                divMain.Visible = false;
                btnDownload.Visible = false;
                Response.Write("<center><h2>You Must Configure Employee Official Details and Salary Details Before Appointment Letter Generation</h2></center>");
            }

        }

        protected string HeaderContent(DataSet dsLetter)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("To,<br />");
            string gender = dsLetter.Tables[0].Rows[0]["Gender"].ToString().Trim();
            if (gender == "Male")
            {
                sb.Append("<b>Mr. " + dsLetter.Tables[0].Rows[0]["EmployeeName"].ToString().ToUpper() + "</b><br />");
            }
            else
            {
                sb.Append("<b>Ms. " + dsLetter.Tables[0].Rows[0]["EmployeeName"].ToString().ToUpper() + "</b><br />");
            }

            sb.Append(dsLetter.Tables[0].Rows[0]["CorrespondanceAddress"].ToString() + ",<br />");
            sb.Append(dsLetter.Tables[0].Rows[0]["CorrespondanceAddressCity"].ToString() + ",<br />");
            sb.Append(dsLetter.Tables[0].Rows[0]["CorrespondanceAddressState"].ToString() + ",<br />");
            sb.Append(dsLetter.Tables[0].Rows[0]["CorrespondanceAddressPin"].ToString() + "<br /><br />");


            sb.Append("Dear Sir/Madam,<br /><br />");

            string EmpType = dsLetter.Tables[0].Rows[0]["EmployeeType"].ToString();
            if (EmpType == "Contract")
            {
                sb.Append("With reference to your application, I, on behalf of the Trust, am glad to appoint you on contractual basis for ");
                sb.Append(dsLetter.Tables[0].Rows[0]["ContractPeriod"].ToString());
                sb.Append(" months effective from " + dsLetter.Tables[0].Rows[0]["DOJ"].ToString() + ", in the post of <b>'" + dsLetter.Tables[0].Rows[0]["DesignationName"].ToString() + "',</b> in ");
                sb.Append("<b>'Supreme Knowledge Foundation Group of Institutions'</b> at 1, Khan Road, PO: Mankundu, Hooghly, on the terms and conditions stipulated hereunder:");
            }
            else
            {
                sb.Append("With reference to your application for the post of <b>'" + dsLetter.Tables[0].Rows[0]["DesignationName"].ToString() + "'</b> in the department of " + dsLetter.Tables[0].Rows[0]["DepartmentName"].ToString());
                sb.Append(", I, on behalf of the Trust, am glad to appoint you for the above mentioned position in <b>'Supreme Knowledge Foundation Group of Institutions'</b> at 1, Khan Road, PO: Mankundu, Hooghly, on the terms and conditions stipulated hereunder:");
            }
            return sb.ToString();
        }

        protected DataTable GetSalaryHead(DataSet dsLetter)
        {
            DataTable dt = new DataTable();
            i = 1;
            string EmpType = dsLetter.Tables[0].Rows[0]["EmployeeType"].ToString();
            if (EmpType == "Contract")
            {
                dt.Columns.Add("SalaryHeadDetails");
                dt.Columns.Add("SalaryHeadPercent");
                dt.Columns.Add("SalaryHeadAmount");

                DataRow dr = dt.NewRow();
                dr["SalaryHeadDetails"] = "Consolidated Amount";
                dr["SalaryHeadAmount"] = ": Rs." + dsLetter.Tables[0].Rows[0]["BasicSalary"].ToString() + "/- only per month.";
                dt.Rows.Add(dr);
                dt.AcceptChanges();
            }
            else
            {
                dt = dsLetter.Tables[1];
                DataRow dr = dt.NewRow();
                dr["SalaryHeadDetails"] = "Basic Salary";
                dr["SalaryHeadAmount"] = ": Rs." + dsLetter.Tables[0].Rows[0]["BasicSalary"].ToString() + "/- (" + dsLetter.Tables[0].Rows[0]["PaybandDetails"].ToString() + ")";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();

                dr = dt.NewRow();
                dr["SalaryHeadDetails"] = "EPF";
                dr["SalaryHeadAmount"] = ": Rs." + dsLetter.Tables[0].Rows[0]["EmployerPF"].ToString() + "/-";
                dt.Rows.Add(dr);
                dt.AcceptChanges();
            }

            return dt;
        }


        protected string TermsContent(DataSet dsLetter)
        {
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < dsLetter.Tables[2].Rows.Count; j++)
            {
                i +=1;
                sb.Append(i.ToString() + ". " + dsLetter.Tables[2].Rows[j]["TermsName"].ToString() + "<br /><br />");
            }

            i += 1;
            sb.Append(i.ToString() + ". Your date of joining shall be treated from <b>" + dsLetter.Tables[0].Rows[0]["DOJ"].ToString() + ".</b><br />");
            sb.Append("   Please return the duplicate copy of the Appointment Letter duly signed in token of your accepting the aforesaid terms & conditions.");

            return sb.ToString();

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            

        }

        protected void DownloadFile(string contentType,string extension)
        {
            var document = new Document(PageSize.A4, 50, 50, 25, 25);

            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);

            document.Open();

            string contents = GetHTML();
            var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(contents), null);

            foreach (var htmlElement in parsedHtmlElements)
                document.Add(htmlElement as IElement);

            document.Close();

            Response.Clear();
            Response.ContentType = "application/" + contentType;
            Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}." + extension, "AppointmentLetter"));
            Response.BinaryWrite(output.ToArray());
            
        }

        protected string GetHTML()
        {
            StringBuilder sb=new StringBuilder();
            sb.Append(@"<table width='100%' align='center'>
                    <tr>
                    <td align='center'");

            sb.Append(@"<h4><u>APPOINTMENT-LETTER</u></h4>");
            sb.Append(@"</td>
                        </tr>
                    </table>
                <br />");


            sb.Append(@"<table width='100%' align='center'>
                    <tr>
                    <td align='left' width='50%'>");
            sb.Append(ltrRefNo.Text);
            sb.Append(@"</td>
                        <td align='right' width='50%'>");
            sb.Append(ltrDate.Text);
            sb.Append(@"</td>
                        </tr>
                    </table>
                <br />");

            sb.Append(ltrHeader.Text + "<br /><br />");


            //Grid Rendering
            StringWriter sw=new StringWriter();
            HtmlTextWriter htw=new HtmlTextWriter(sw);
            dgvSalary.RenderControl(htw);
            sb.Append(sw.ToString() + "<br />");

            sb.Append(ltrTerms.Text + "<br /><br />");

            sb.Append(@"Yours faithfully,<br />");
            sb.Append(@"<b>SUPREME KNOWLEDGE FOUNDATION</b><br /><br /><br />");
            sb.Append(@"<b>(Bijoy Guha Mallick)</b><br />");
            sb.Append(@"<b>Chairman</b>");


            return sb.ToString();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        
    }
}
