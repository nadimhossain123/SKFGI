using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CollegeERP.Student
{
    public partial class printFeesVoucher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            populateLoad(Request.QueryString["id"].ToString());
            Page.ClientScript.RegisterStartupScript(GetType(), "javascript", "window.print()", true);
        }

        private void populateLoad(string id)
        {
            string str = "";
            BusinessLayer.Student.ApproveStudent Aps = new BusinessLayer.Student.ApproveStudent();
            Entity.Student.ApproveStudent eAps = new Entity.Student.ApproveStudent();

            eAps.intMode = 6;
            eAps.intStudentID = Convert.ToInt32(id);
            DataSet ds = new DataSet();
            ds = Aps.GetAllStudent(eAps);

            str += "<div align='center'>";
            str += "<h2>";
            str += "Supreme Knowledge Foundation Group of Institutions";
            str += "</h2>";
            str += "<h3>";
            str += "Student Fees Details</h3>";
            str += "<table cellpadding='5' cellspacing='0' style='width: 600px; color: #000000; font-family: Verdana, Arial, sans-serif;";
            str += "font-size: 12px; font-weight: normal;'>";

            if (ds.Tables.Count > 0)
            {
                str += HeaderPopulate(ds.Tables[0]);
                str += DetailsPopulate(ds.Tables[1], Convert.ToInt32(ds.Tables[0].Rows[0]["CourseId"].ToString()));
                str += sumPopulate(ds.Tables[2], Convert.ToInt32(ds.Tables[0].Rows[0]["CourseId"].ToString()));
            }

            str += "</table>";
            str += "<div height='30px'>&nbsp;</div>";
            str += "</div>";
            divPrint.InnerHtml = str;
        }

        private string sumPopulate(DataTable dt, int courseID)
        {
            string str = "";
            int headerCount = 0;
            int width = 0;
            if (courseID == 1 || courseID == 3)
            {
                headerCount = 4;
                width = 80;
            }
            else
            {
                headerCount = 8;
                width = 50;
            }

            str += "<tr>";
            str += "<th> Total Fees";
            str += "</th>";

            for (int i = 0; i < headerCount; i++)
            {
                str += "<th scope='col' style='width: " + width + "px'>";
                str += dt.Rows[0]["sem" + (i + 1).ToString()].ToString();
                str += "</th>";
            }

            str += "</tr>";
            return str;
        }

        private string DetailsPopulate(DataTable dt, int courseID)
        {
            int headerCount = 0;
            int width = 0;
            string str = "";
            str += "<table style='width: 600px;border:1px solid; font-family: Verdana, Arial, sans-serif;";
            str += "font-size: 12px; font-weight: normal;'  rules='all'>";
            ///----------------------------------
            str += "<tr>";
            str += "<th scope='col'>";
            str += " Fees";
            str += "</th>";
            if (courseID == 1 || courseID == 3)
            {
                headerCount = 4;
                width = 80;
            }
            else
            {
                headerCount = 8;
                width = 50;
            }
            for (int i = 0; i < headerCount; i++)
            {
                str += "<th scope='col' style='width: " + width + "px'>";
                str += GetSemName(i + 1);
                str += "</th>";
            }
            str += "</tr>";
            ///----------------------------------
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str += "<tr class='RowStyle'>";
                str += "<td>" + dt.Rows[i]["fees"].ToString();
                str += "</td>";
                for (int j = 0; j < headerCount; j++)
                {
                    str += "<td scope='col' style='width: " + width + "px'>";
                    if (dt.Rows[i]["sem" + (j + 1).ToString()].ToString() != "0")
                    {
                        str += dt.Rows[i]["sem" + (j + 1).ToString()].ToString();
                    }
                    else
                    {
                        str += ""; ;
                    }
                    str += "</td>";
                }
                str += "</tr>";
            }


            return str;
        }

        private string GetSemName(int SemNo)
        {
            string ReturnValue = "";
            switch (SemNo)
            {
                case 1: ReturnValue = "1st Sem"; break;
                case 2: ReturnValue = "2nd Sem"; break;
                case 3: ReturnValue = "3rd Sem"; break;
                case 4: ReturnValue = "4th Sem"; break;
                case 5: ReturnValue = "5th Sem"; break;
                case 6: ReturnValue = "6th Sem"; break;
                case 7: ReturnValue = "7th Sem"; break;
                case 8: ReturnValue = "8th Sem"; break;
            }
            return ReturnValue;
        }

        private string HeaderPopulate(DataTable dataTable)
        {
            string str = "";

            str += "<tr>";
            str += "<td style='width: 30%;' align='left'>";
            str += "Batch ";
            str += "</td>";
            str += "<td>" + dataTable.Rows[0]["batch_name"].ToString();
            str += "</td>";
            str += "</tr>";
            str += "<tr>";
            str += "<td style='width: 30%;' align='left'>";
            str += "Course ";
            str += "</td>";
            str += "<td>" + dataTable.Rows[0]["CourseName"].ToString();
            str += "</td>";
            str += "</tr>";
            str += "<tr>";
            str += "<td style='width: 30%;' align='left'>";
            str += "Stream ";
            str += "</td>";
            str += "<td>" + dataTable.Rows[0]["stream_name"].ToString();
            str += "</td>";
            str += "</tr>";
            str += "<tr>";
            str += "<td style='width: 30%;' align='left'>";
            str += "Student Name";
            str += "</td>";
            str += "<td>" + dataTable.Rows[0]["name"].ToString();
            str += "</td>";
            str += "</tr>";
            str += "<tr>";
            str += "<td style='width: 30%;' align='left'>";
            str += "Student Code";
            str += "</td>";
            str += "<td>" + dataTable.Rows[0]["student_code"].ToString();
            str += "</td>";
            str += "</tr>";
            str += "<tr>";
            str += "<td style='width: 30%;' align='left'>";
            str += "Fees Type";
            str += "</td>";
            str += "<td>" + dataTable.Rows[0]["fees_name"].ToString();
            str += "</td>";
            str += "</tr>";

            return str;
        }
    }
}