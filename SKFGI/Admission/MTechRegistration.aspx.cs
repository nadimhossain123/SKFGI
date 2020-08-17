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

namespace CollegeERP.Admission
{
    public partial class MTechRegistration : System.Web.UI.Page
    {
        string strStudentID = "0";
        public string Photo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                loadBatch();
                loadStream();
                ClearFields(Form.Controls);
            }
        }

        private void loadBatch()
        {
            BusinessLayer.Student.BTechRegistration BtechReg = new BusinessLayer.Student.BTechRegistration();
            Entity.Student.BTechRegistration eBtechReg = new Entity.Student.BTechRegistration();
            eBtechReg.intMode = 1;
            eBtechReg.CourseId = 0;
            DataTable dt = new DataTable();
            dt = BtechReg.GetAllCommonSP(eBtechReg);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ddlBatch.DataSource = dt;
                    ddlBatch.DataBind();
                }
            }
        }

        private void loadStream()
        {
            BusinessLayer.Student.BTechRegistration ObjRegistration = new BusinessLayer.Student.BTechRegistration();
            Entity.Student.BTechRegistration Registration = new Entity.Student.BTechRegistration();
            Registration.intMode = 3;
            Registration.CourseId = 3;
            DataTable dt = ObjRegistration.GetAllCommonSP(Registration);
            if (dt != null)
            {
                dt.Rows.RemoveAt(0);
                dt.AcceptChanges();

                chkStream.DataSource = dt;
                chkStream.DataBind();
            }
        }

        protected void ClearFields(ControlCollection pageControls)
        {
            strStudentID = "0";
            foreach (Control contl in pageControls)
            {
                string strCntName = (contl.GetType()).Name;
                switch (strCntName)
                {
                    case "TextBox":
                        TextBox tbSource = (TextBox)contl;
                        tbSource.Text = "";
                        break;
                    case "RadioButtonList":
                        RadioButtonList rblSource = (RadioButtonList)contl;
                        rblSource.SelectedIndex = -1;
                        break;
                    case "DropDownList":
                        DropDownList ddlSource = (DropDownList)contl;
                        //ddlSource.SelectedIndex = 0;
                        ddlSource.SelectedValue = "0";
                        break;
                    case "ListBox":
                        ListBox lbsource = (ListBox)contl;
                        lbsource.SelectedIndex = -1;
                        break;
                    case "RadioButton":
                        RadioButton rdb = (RadioButton)contl;
                        rdb.Checked = false;
                        break;
                    case "CheckBox":
                        CheckBox chk = (CheckBox)contl;
                        chk.Checked = false;
                        break;

                }
                ClearFields(contl.Controls);
            }

            ImgPhoto.ImageUrl = "../Student/StudentPhoto/Male.jpg";
            rbPgate.Checked = true;
            rbHostalN.Checked = true;
            rbMale.Checked = true;
            rbSingle.Checked = true;
            rbGeneral.Checked = true;
            for (int i = 0; i < chkStream.Items.Count; i++)
            {
                chkStream.Items[i].Selected = false;
            }
            btnSave.Text = "Save";
            Message.Show = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (checkStreamSelected() == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Can't Save. Please Select One Stream";
            }
            else if (checkStreamSelected() > 1)
            {
                Message.IsSuccess = false;
                Message.Text = "Can't Save. Please Select Only One Stream";
            }
            else if (checkDocumentsSelected() == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Can't Save. Please Select One Document Submitted";
            }
            else if (!IsValidPhoto())
            {
                Message.IsSuccess = false;
                Message.Text = "Please Select JPG or BMP File to Upload.";
            }
            else
            {
                if (strStudentID == "0")
                {
                    if (saveData(10) > 0)
                    {
                        Response.Redirect("Thanks.aspx");
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Can't Save. Duplicate Application No Is Not Allowed";
                    }

                }
            }
            Message.Show = true;
        }

        private int saveData(int callMode)
        {
            BusinessLayer.Student.MTechRegistration mtechReg = new BusinessLayer.Student.MTechRegistration();
            Entity.Student.MTechRegistration emtechReg = new Entity.Student.MTechRegistration();

            emtechReg.intMode = callMode;
            emtechReg.studentID = int.Parse(strStudentID);
            emtechReg.intCompanyId = BusinessLayer.Common.Company.GetCompanyId("Engineering");
            emtechReg.login_id = "1";
            emtechReg.CourseId = 3;
            emtechReg.strapplicationNumber = "";
            emtechReg.strBatch = ddlBatch.SelectedValue.Trim();
            emtechReg.strRank = findSelectedRank();
            emtechReg.strRankid = txtRankid.Text.Trim().Replace("'", "''");
            emtechReg.strRegistrationNo = "";
            emtechReg.strUniversityRollNo = "";
            emtechReg.strMigrationInfo = "";
            emtechReg.strIsHostelFacility = ChkHostelFacility.Checked;

            emtechReg.strNameOfApplicant = txtNameOfApplicant.Text.Trim().Replace("'", "''");
            string[] Date = txtDob.Text.Trim().Split('/');
            emtechReg.strDob = (Date[1].Trim() + "/" + Date[0].Trim() + "/" + Date[2].Trim() + " 00:00:00").ToString();
            emtechReg.strFatherName = txtFatherName.Text.Trim().Replace("'", "''");
            emtechReg.strFatherOccupation = txtFatherOccupation.Text.Trim().Replace("'", "''");
            emtechReg.strMotherName = txtMotherName.Text.Trim().Replace("'", "''");
            emtechReg.strMotherOccupation = txtMotherOccupation.Text.Trim().Replace("'", "''");
            emtechReg.strGuardiansName = txtGuardiansName.Text.Trim().Replace("'", "''");
            emtechReg.strPAddress = txtPAddress.Text.Trim().Replace("'", "''");
            emtechReg.strCAddress = txtCAddress.Text.Trim().Replace("'", "''");


            emtechReg.strPResidential = txtPResidential.Text.Trim().Replace("'", "''");
            emtechReg.strSResidential = txtSResidential.Text.Trim().Replace("'", "''");
            emtechReg.strPMobile = txtPMobile.Text.Trim().Replace("'", "''");
            emtechReg.strSMobile = txtSMobile.Text.Trim().Replace("'", "''");
            emtechReg.strEmail = txtEmail.Text.Trim().Replace("'", "''");
            emtechReg.strHostalFacitity = findHostalSelected();
            emtechReg.strGender = findGender();
            emtechReg.strMarital = findMaritalStatus();

            emtechReg.strNationality = txtNationality.Text.Trim().Replace("'", "''");
            emtechReg.strRealigion = txtRealigion.Text.Trim().Replace("'", "''");
            emtechReg.strCast = findCast();

            emtechReg.strGateScore = txtGateScore.Text.Trim();
            emtechReg.strAcademicYrExp = (txtAcademicYrExp.Text.Trim().Length > 0) ? txtAcademicYrExp.Text.Trim() : "0";
            emtechReg.strIndustryYrExp = (txtIndustryYrExp.Text.Trim().Length > 0) ? txtIndustryYrExp.Text.Trim() : "0";

            emtechReg.strXSubject = txtXSubject.Text.Trim().Replace("'", "''");
            emtechReg.strXBoard = txtXBoard.Text.Trim().Replace("'", "''");
            emtechReg.strXCollege = txtXCollege.Text.Trim().Replace("'", "''");
            emtechReg.strXYearOfPassing = txtXYearOfPassing.Text.Trim().Replace("'", "''");
            emtechReg.strXMarks = txtXMarks.Text.Trim().Replace("'", "''");

            emtechReg.strXiiSubject = txtXiiSubject.Text.Trim().Replace("'", "''");
            emtechReg.strXiiBoard = txtXiiBoard.Text.Trim().Replace("'", "''");
            emtechReg.strXiiCollege = txtXiiCollege.Text.Trim().Replace("'", "''");
            emtechReg.strXiiYearOfPassing = txtXiiYearOfPassing.Text.Trim().Replace("'", "''");
            emtechReg.strXiiMarks = txtXiiMarks.Text.Trim().Replace("'", "''");

            emtechReg.strGSubject = txtGSubject.Text.Trim().Replace("'", "''");
            emtechReg.strGBoard = txtGBoard.Text.Trim().Replace("'", "''");
            emtechReg.strGCollege = txtGCollege.Text.Trim().Replace("'", "''");
            emtechReg.strGYearOfPassing = txtGYearOfPassing.Text.Trim().Replace("'", "''");
            emtechReg.strGMarks = txtGMarks.Text.Trim().Replace("'", "''");

            emtechReg.strDSubject = txtDSubject.Text.Trim().Replace("'", "''");
            emtechReg.strDBoard = txtDBoard.Text.Trim().Replace("'", "''");
            emtechReg.strDCollege = txtDCollege.Text.Trim().Replace("'", "''");
            emtechReg.strDYearOfPassing = txtDYearOfPassing.Text.Trim().Replace("'", "''");
            emtechReg.strDMarks = txtDMarks.Text.Trim().Replace("'", "''");


            emtechReg.strDocList1 = chkListDoc1.Checked == true ? "1" : "0";
            emtechReg.strDocList2 = chkListDoc2.Checked == true ? "2" : "0";
            emtechReg.strDocList3 = chkListDoc3.Checked == true ? "3" : "0";
            emtechReg.strDocList4 = chkListDoc4.Checked == true ? "4" : "0";
            emtechReg.strDocList5 = chkListDoc5.Checked == true ? "5" : "0";
            emtechReg.strDocList6 = chkListDoc6.Checked == true ? "6" : "0";
            emtechReg.strDocList7 = chkListDoc7.Checked == true ? "7" : "0";
            emtechReg.strDocList8 = chkListDoc8.Checked == true ? "8" : "0";

            string checkedIItem = "";
            int checkedIItemCount = chkStream.Items.Count;
            for (int i = 0; i < checkedIItemCount; i++)
            {
                if (chkStream.Items[i].Selected)
                {
                    checkedIItem += chkStream.Items[i].Value.ToString() + ","; ;
                }
            }

            if (checkedIItem.Length > 0)
            {
                checkedIItem = checkedIItem.Remove(checkedIItem.LastIndexOf(","), 1);
            }
            emtechReg.strStreamApplied = checkedIItem;
            emtechReg.strPhoto = Photo;

            int RowsAffected = mtechReg.Save(emtechReg);
            if (RowsAffected > 0)
            {
                if (Photo != "")
                {
                    string ff = (Server.MapPath("..") + "\\Student\\StudentPhoto\\" + emtechReg.studentID + Photo);
                    uploadImage.PostedFile.SaveAs(ff);
                }
            }
            return RowsAffected;
        }

        public bool IsValidPhoto()
        {
            bool IsValid = true;
            if (uploadImage.PostedFile.FileName != null && uploadImage.PostedFile.ContentLength > 0)
            {
                string fn = uploadImage.FileName;
                string fileExt = System.IO.Path.GetExtension(fn);
                if (fileExt == ".jpg" || fileExt == ".bmp" || fileExt == ".JPG")
                {
                    string sm = Server.MapPath("");
                    Photo = fileExt;
                }
                else
                    IsValid = false;
            }
            else
            {
                Photo = "";
            }
            return IsValid;
        }

        private string findSelectedRank()
        {
            string strCheckItem = "0";
            if (rbPgate.Checked == true)
                strCheckItem = "1";
            else if (rbGate.Checked == true)
                strCheckItem = "2";
            else if (rbDirect.Checked == true)
                strCheckItem = "3";
            return strCheckItem;
        }

        private int checkStreamSelected()
        {
            int ItemCount = 0;
            for (int j = 0; j < chkStream.Items.Count; j++)
            {
                if (chkStream.Items[j].Selected == true)
                {
                    ItemCount += 1;
                }
            }
            return ItemCount;
        }

        private int checkDocumentsSelected()
        {
            int ItemCount = 0;
            if (chkListDoc1.Checked)
            {
                ItemCount += 1;
            }
            if (chkListDoc2.Checked)
            {
                ItemCount += 1;
            }
            if (chkListDoc3.Checked)
            {
                ItemCount += 1;
            }
            if (chkListDoc4.Checked)
            {
                ItemCount += 1;
            }
            if (chkListDoc5.Checked)
            {
                ItemCount += 1;
            }
            if (chkListDoc6.Checked)
            {
                ItemCount += 1;
            }
            if (chkListDoc7.Checked)
            {
                ItemCount += 1;
            }
            if (chkListDoc8.Checked)
            {
                ItemCount += 1;
            }
            return ItemCount;
        }

        private string findCast()
        {
            string strCheckItem = "0";
            if (rbGeneral.Checked == true)
                strCheckItem = "1";
            else if (rbSc.Checked == true)
                strCheckItem = "2";
            else if (rbSt.Checked == true)
                strCheckItem = "3";
            else if (rbObc.Checked == true)
                strCheckItem = "4";
            return strCheckItem;
        }

        private string findMaritalStatus()
        {
            string strCheckItem = "0";
            if (rbMarried.Checked == true)
                strCheckItem = "1";
            else
                strCheckItem = "2";
            return strCheckItem;
        }

        private string findHostalSelected()
        {
            string strCheckItem = "0";
            if (rbHostalY.Checked == true)
                strCheckItem = "1";
            else
                strCheckItem = "2";
            return strCheckItem;
        }

        private string findGender()
        {
            string strCheckItem = "0";
            if (rbMale.Checked == true)
                strCheckItem = "1";
            else
                strCheckItem = "2";
            return strCheckItem;
        }
    }
}
