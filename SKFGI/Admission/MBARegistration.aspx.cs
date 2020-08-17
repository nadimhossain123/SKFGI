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
    public partial class MBARegistration : System.Web.UI.Page
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
            BusinessLayer.Student.BTechRegistration BtechReg = new BusinessLayer.Student.BTechRegistration();
            Entity.Student.BTechRegistration eBtechReg = new Entity.Student.BTechRegistration();
            eBtechReg.intMode = 3;
            eBtechReg.CourseId = 1;
            DataTable dt = new DataTable();
            dt = BtechReg.GetAllCommonSP(eBtechReg);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    dt.Rows.RemoveAt(0);
                    dt.AcceptChanges();
                    //----------------------------
                    chkStream.DataSource = dt;
                    chkStream.DataTextField = "stream_name";
                    chkStream.DataValueField = "StreamId";
                    chkStream.DataBind();

                }
            }
        }

        protected void ClearFields(ControlCollection pageControls)
        {
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
            rbCmat.Checked = true;
            rbHostalN.Checked = true;
            rbMale.Checked = true;
            rbSingle.Checked = true;
            rbGeneral.Checked = true;
            Message.Show = false;
        }

        private int saveData(int callMode)
        {
            BusinessLayer.Student.MBARegistration mbaReg = new BusinessLayer.Student.MBARegistration();
            Entity.Student.MBARegistration embaReg = new Entity.Student.MBARegistration();

            embaReg.intMode = callMode;
            embaReg.studentID = int.Parse(strStudentID);
            //To Do
            //For MBA Students CompanyId=1
            embaReg.intCompanyId = BusinessLayer.Common.Company.GetCompanyId("MBA");
            embaReg.login_id = "1"; // Hardcoded Login Id
            embaReg.CourseId = 1;
            embaReg.strapplicationNumber = "";
            embaReg.strBatch = ddlBatch.SelectedValue.Trim();
            embaReg.strRank = findSelectedRank();
            embaReg.strRankid = txtRankid.Text.Trim().Replace("'", "''");
            embaReg.strRegistrationNo = "";
            embaReg.strUniversityRollNo = "";
            embaReg.strMigrationInfo = "";
            embaReg.strIsHostelFacility = ChkHostelFacility.Checked;


            embaReg.strNameOfApplicant = txtNameOfApplicant.Text.Trim().Replace("'", "''");
            string[] Date = txtDob.Text.Trim().Split('/');
            embaReg.strDob = (Date[1].Trim() + "/" + Date[0].Trim() + "/" + Date[2].Trim() + " 00:00:00").ToString();
            embaReg.strFatherName = txtFatherName.Text.Trim().Replace("'", "''");
            embaReg.strFatherOccupation = txtFatherOccupation.Text.Trim().Replace("'", "''");
            embaReg.strMotherName = txtMotherName.Text.Trim().Replace("'", "''");
            embaReg.strMotherOccupation = txtMotherOccupation.Text.Trim().Replace("'", "''");
            embaReg.strGuardiansName = txtGuardiansName.Text.Trim().Replace("'", "''");
            embaReg.strPAddress = txtPAddress.Text.Trim().Replace("'", "''");
            embaReg.strCAddress = txtCAddress.Text.Trim().Replace("'", "''");


            embaReg.strPResidential = txtPResidential.Text.Trim().Replace("'", "''");
            embaReg.strSResidential = txtSResidential.Text.Trim().Replace("'", "''");
            embaReg.strPMobile = txtPMobile.Text.Trim().Replace("'", "''");
            embaReg.strSMobile = txtSMobile.Text.Trim().Replace("'", "''");
            embaReg.strEmail = txtEmail.Text.Trim().Replace("'", "''");
            embaReg.strHostalFacitity = findHostalSelected();
            embaReg.strGender = findGender();
            embaReg.strMarital = findMaritalStatus();
            embaReg.strMotherTong = txtMotherTong.Text.Trim().Replace("'", "''");
            embaReg.strNationality = txtNationality.Text.Trim().Replace("'", "''");
            embaReg.strRealigion = txtRealigion.Text.Trim().Replace("'", "''");
            embaReg.strBengali = findBLanguagePower();
            embaReg.strHindi = findHLanguagePower();
            embaReg.strEnglish = findELanguagePower();
            embaReg.strOther = findOLanguagePower();
            embaReg.strCast = findCast();
            embaReg.strMonthlyIncome = txtMonthlyIncome.Text.Trim().Replace("'", "''");
            embaReg.strRefferenceName1 = txtRefferenceName1.Text.Trim().Replace("'", "''");
            embaReg.strRefferenceAddress1 = txtRefferenceAddress1.Text.Trim().Replace("'", "''");
            embaReg.strRefferenceContactNumber1 = txtRefferenceContactNumber1.Text.Trim().Replace("'", "''");
            embaReg.strRefferenceName2 = txtRefferenceName2.Text.Trim().Replace("'", "''");
            embaReg.strRefferenceAddress2 = txtRefferenceAddress2.Text.Trim().Replace("'", "''");
            embaReg.strRefferenceContactNumber2 = txtRefferenceContactNumber2.Text.Trim().Replace("'", "''");

            embaReg.strXSubject = txtXSubject.Text.Trim().Replace("'", "''");
            embaReg.strXBoard = txtXBoard.Text.Trim().Replace("'", "''");
            embaReg.strXCollege = txtXCollege.Text.Trim().Replace("'", "''");
            embaReg.strXYearOfPassing = txtXYearOfPassing.Text.Trim().Replace("'", "''");
            embaReg.strXMarks = txtXMarks.Text.Trim().Replace("'", "''");

            embaReg.strXiiSubject = txtXiiSubject.Text.Trim().Replace("'", "''");
            embaReg.strXiiBoard = txtXiiBoard.Text.Trim().Replace("'", "''");
            embaReg.strXiiCollege = txtXiiCollege.Text.Trim().Replace("'", "''");
            embaReg.strXiiYearOfPassing = txtXiiYearOfPassing.Text.Trim().Replace("'", "''");
            embaReg.strXiiMarks = txtXiiMarks.Text.Trim().Replace("'", "''");

            embaReg.strGSubject = txtGSubject.Text.Trim().Replace("'", "''");
            embaReg.strGBoard = txtGBoard.Text.Trim().Replace("'", "''");
            embaReg.strGCollege = txtGCollege.Text.Trim().Replace("'", "''");
            embaReg.strGYearOfPassing = txtGYearOfPassing.Text.Trim().Replace("'", "''");
            embaReg.strGMarks = txtGMarks.Text.Trim().Replace("'", "''");

            embaReg.strDSubject = txtDSubject.Text.Trim().Replace("'", "''");
            embaReg.strDBoard = txtDBoard.Text.Trim().Replace("'", "''");
            embaReg.strDCollege = txtDCollege.Text.Trim().Replace("'", "''");
            embaReg.strDYearOfPassing = txtDYearOfPassing.Text.Trim().Replace("'", "''");
            embaReg.strDMarks = txtDMarks.Text.Trim().Replace("'", "''");


            embaReg.strNameofComapny1 = txt1NameOfCompany.Text.Trim().Replace("'", "''");
            embaReg.strResponsibility1 = txt1JobProfile.Text.Trim().Replace("'", "''");
            embaReg.strjoning1 = txt1DateOfJoining.Text.Trim().Replace("'", "''");
            embaReg.strLeaving1 = txt1DateOfLeave.Text.Trim().Replace("'", "''");
            embaReg.strRemarks1 = txt1Remark.Text.Trim().Replace("'", "''");


            embaReg.strNameofComapny2 = txt2NameOfCompany.Text.Trim().Replace("'", "''");
            embaReg.strResponsibility2 = txt2JobProfile.Text.Trim().Replace("'", "''");
            embaReg.strjoning2 = txt2DateOfJoining.Text.Trim().Replace("'", "''");
            embaReg.strLeaving2 = txt2JobProfile.Text.Trim().Replace("'", "''");
            embaReg.strRemarks2 = txt2Remark.Text.Trim().Replace("'", "''");

            embaReg.strNameofComapny3 = txt3NameOfCompany.Text.Trim().Replace("'", "''");
            embaReg.strResponsibility3 = txt3JobProfile.Text.Trim().Replace("'", "''");
            embaReg.strjoning3 = txt3DateOfJoining.Text.Trim().Replace("'", "''");
            embaReg.strLeaving3 = txt3JobProfile.Text.Trim().Replace("'", "''");
            embaReg.strRemarks3 = txt3Remark.Text.Trim().Replace("'", "''");

            embaReg.strtest1 = txtText1.Text.Trim().Replace("'", "''");
            embaReg.strExaminationDate1 = txtDateOfExam1.Text.Trim().Replace("'", "''");
            embaReg.strMarks1 = txtMarksObtain1.Text.Trim().Replace("'", "''");

            embaReg.strtest2 = txtText2.Text.Trim().Replace("'", "''");
            embaReg.strExaminationDate2 = txtDateOfExam2.Text.Trim().Replace("'", "''");
            embaReg.strMarks2 = txtMarksObtain3.Text.Trim().Replace("'", "''");

            embaReg.strtest3 = txtText3.Text.Trim().Replace("'", "''");
            embaReg.strExaminationDate2 = txtDateOfExam3.Text.Trim().Replace("'", "''");
            embaReg.strMarks3 = txtMarksObtain3.Text.Trim().Replace("'", "''");

            embaReg.strDocList1 = chkListDoc1.Checked == true ? "1" : "0";
            embaReg.strDocList2 = chkListDoc2.Checked == true ? "2" : "0";
            embaReg.strDocList3 = chkListDoc3.Checked == true ? "3" : "0";
            embaReg.strDocList4 = chkListDoc4.Checked == true ? "4" : "0";
            embaReg.strDocList5 = chkListDoc5.Checked == true ? "5" : "0";
            embaReg.strDocList6 = chkListDoc6.Checked == true ? "6" : "0";
            embaReg.strDocList7 = chkListDoc7.Checked == true ? "7" : "0";
            embaReg.strDocList8 = chkListDoc8.Checked == true ? "8" : "0";
            embaReg.strDocList9 = chkListDoc9.Checked == true ? "9" : "0";
            embaReg.BloodGroup = txtBloodGroup.Text.Trim();

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
            embaReg.strStreamApplied = checkedIItem;
            embaReg.strPhoto = Photo;

            int RowsAffected = mbaReg.Save(embaReg);
            if (RowsAffected > 0)
            {
                if (Photo != "")
                {
                    string ff = (Server.MapPath("..") + "\\Student\\StudentPhoto\\" + embaReg.studentID + Photo);
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
            if (rbCmat.Checked == true)
                strCheckItem = "1";
            else if (rbJeMat.Checked == true)
                strCheckItem = "2";
            else if (rbMat.Checked == true)
                strCheckItem = "3";
            return strCheckItem;
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

        private string findOLanguagePower()
        {
            string LanguagePower = "";
            if (chkOSpeak.Checked == true)
                LanguagePower += "1,";
            if (chkORead.Checked == true)
                LanguagePower += "2,";
            if (chkOWrite.Checked == true)
                LanguagePower += "3,";
            if (LanguagePower != "")
                LanguagePower = LanguagePower.Remove(LanguagePower.LastIndexOf(","), 1);
            return LanguagePower;
        }

        private string findELanguagePower()
        {
            string LanguagePower = "";
            if (chkESpeak.Checked == true)
                LanguagePower += "1,";
            if (chkERead.Checked == true)
                LanguagePower += "2,";
            if (chkEWrite.Checked == true)
                LanguagePower += "3,";
            if (LanguagePower != "")
                LanguagePower = LanguagePower.Remove(LanguagePower.LastIndexOf(","), 1);
            return LanguagePower;
        }

        private string findHLanguagePower()
        {
            string LanguagePower = "";
            if (chkHSpeak.Checked == true)
                LanguagePower += "1,";
            if (chkHRead.Checked == true)
                LanguagePower += "2,";
            if (chkHWrite.Checked == true)
                LanguagePower += "3,";
            if (LanguagePower != "")
                LanguagePower = LanguagePower.Remove(LanguagePower.LastIndexOf(","), 1);
            return LanguagePower;
        }

        private string findBLanguagePower()
        {
            string LanguagePower = "";
            if (chkBSpeak.Checked == true)
                LanguagePower += "1,";
            if (chkBRead.Checked == true)
                LanguagePower += "2,";
            if (chkBWrite.Checked == true)
                LanguagePower += "3,";
            if (LanguagePower != "")
                LanguagePower = LanguagePower.Remove(LanguagePower.LastIndexOf(","), 1);
            return LanguagePower;
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
            if (chkListDoc9.Checked)
            {
                ItemCount += 1;
            }
            return ItemCount;
        }
    }
}
