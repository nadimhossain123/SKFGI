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
    public partial class EmployeeIDCard : System.Web.UI.Page
    {
        public int EmployeeId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["EmployeeId"] != null && Request.QueryString["EmployeeId"].Trim().Length > 0)
                {
                    EmployeeId = Convert.ToInt32(Request.QueryString["EmployeeId"].Trim());
                    LoadIDCardDetails();
                    Page.ClientScript.RegisterStartupScript(GetType(), "javascript", "window.print()", true);
                }
            }
        }

        protected void LoadIDCardDetails()
        {
            BusinessLayer.Common.Employee ObjEmployee = new BusinessLayer.Common.Employee();
            Entity.Common.Employee Employee = new Entity.Common.Employee();
            Employee = ObjEmployee.GetAllById(EmployeeId);
            if (Employee != null)
            {
                ImgID.ImageUrl = Employee.Photo;
                ltrEmployeeName.Text = "Name: <b><span style='color:#3D3777;'>" + Employee.FirstName.ToUpper() + " " + Employee.MiddleName.ToUpper() + " " + Employee.LastName.ToUpper() + "</span></b>";
                ltrEmpCode.Text = "Emp. Code <span style='color:#3D4051;'>" + Employee.EmpCode + "</span>";
                ltrBloodGroup.Text = "Blood Group: <span style='color:#3D4051;'>" + Employee.BloodGroup + "</span>";
                ltrAddress.Text = "<b>Correspondence Address:</b><br /> <span style='color:#27B3DE;'>" + Employee.CorrespondanceAddress.ToUpper() + "</span>";
                ltrDOB.Text = "<b>Date of Birth :</b> <span style='color:#27B3DE;'>" + Employee.DateOfBirth.ToString("dd.MM.yyyy") + "</span>";
                ltrPhoneNo.Text = "<b>Telephone No :</b> <span style='color:#27B3DE;'>" + Employee.ContactNo1 + "</span>";

            }

            BusinessLayer.Common.EmployeeOfficial ObjOfficial = new BusinessLayer.Common.EmployeeOfficial();
            Entity.Common.EmployeeOfficial Official = new Entity.Common.EmployeeOfficial();
            Official = ObjOfficial.GetAllByEmployeeId(EmployeeId);
            if (Official != null)
            {
                ltrDOJ.Text = "<b>Date of Joining :</b> <span style='color:#27B3DE;'>" + Official.DOJ.ToString("dd.MM.yyyy") + "</span>";

                if (Official.EmployeeOfficial_DesignationId > 0)
                {
                    BusinessLayer.Common.Designation ObjDesignation = new BusinessLayer.Common.Designation();
                    Entity.Common.Designation Designation = new Entity.Common.Designation();
                    Designation = ObjDesignation.GetAllById(Official.EmployeeOfficial_DesignationId);
                    ltrDesignation.Text = "Designation : <span style='color:#3D4051;'>" + Designation.DesignationName.ToUpper() + "</span>";
                }
                if (Official.EmployeeOfficial_DepartmentId > 0)
                {
                    BusinessLayer.Common.Department ObjDepartment = new BusinessLayer.Common.Department();
                    Entity.Common.Department Department = new Entity.Common.Department();
                    Department = ObjDepartment.GetAllById(Official.EmployeeOfficial_DepartmentId);
                    ltrDepartment.Text = "Dept. : <span style='color:#3D4051;'>" + Department.DepartmentName.ToUpper() + "</span>";
                }
            }
        }
    }
}
