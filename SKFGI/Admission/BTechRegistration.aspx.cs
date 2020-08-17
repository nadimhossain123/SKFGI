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
    public partial class BTechRegistration : System.Web.UI.Page
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
            eBtechReg.CourseId = 2;
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

        private string findSelectedRank()
        {
            string strCheckItem = "0";
            if (rbWbjee.Checked == true)
                strCheckItem = "1";
            else if (rbAieee.Checked == true)
                strCheckItem = "2";
            else if (rbjelat.Checked == true)
                strCheckItem = "3";
            else if (rbDirect.Checked == true)
                strCheckItem = "4";
            else if (rbMQ.Checked == true)
                strCheckItem = "5";
            return strCheckItem;
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
            rbWbjee.Checked = true;
            rbHostalN.Checked = true;
            rbMale.Checked = true;
            rbSingle.Checked = true;
            rbGeneral.Checked = true;
            Message.Show = false;
        }

        private int saveData(int callMode)
        {
            BusinessLayer.Student.BTechRegistration BtechReg = new BusinessLayer.Student.BTechRegistration();
            Entity.Student.BTechRegistration eBtechReg = new Entity.Student.BTechRegistration();
            eBtechReg.intMode = callMode;
            eBtechReg.studentID = int.Parse(strStudentID);
            //To Do
            //For BTech Students CompanyId=2
            eBtechReg.login_id = "1"; // Hardcoded Login Id
            eBtechReg.intCompanyId = BusinessLayer.Common.Company.GetCompanyId("Engineering");
            eBtechReg.CourseId = 2;
            eBtechReg.strapplicationNumber = "";
            eBtechReg.strBatch = ddlBatch.SelectedValue.Trim();
            eBtechReg.strEnrollmentNo = txtEnrollmentNo.Text.Trim().Replace("'", "''");
            eBtechReg.strRank = findSelectedRank();
            eBtechReg.strRankid = txtRankid.Text.Trim().Replace("'", "''");
            eBtechReg.strIsLateral = ChkLateral.Checked;
            eBtechReg.strRegistrationNo ="";
            eBtechReg.strUniversityRollNo = "";
            eBtechReg.strMigrationInfo = "";
            eBtechReg.strIsHostelFacility = ChkHostelFacility.Checked;
            eBtechReg.strTFW = ChkTFW.Checked;
            eBtechReg.strIsReAdmission = ChkIsReAdmission.Checked;

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
            eBtechReg.strStreamApplied = checkedIItem;
            //eBtechReg.strEce = chkEce.Checked == true ? "1" : "0";
            //eBtechReg.strEe = chkEe.Checked == true ? "2" : "0";
            //eBtechReg.strCse = chkCse.Checked == true ? "3" : "0";
            //eBtechReg.strAeie = chkAeie.Checked == true ? "4" : "0";
            //eBtechReg.strMe = chkMe.Checked == true ? "5" : "0";
            //eBtechReg.strCe = chkCe.Checked == true ? "6" : "0";


            eBtechReg.strPhy = txtPhy.Text.Trim().Length > 0 ? txtPhy.Text.Trim() : "0";
            eBtechReg.strChem = txtChem.Text.Trim().Length > 0 ? txtChem.Text.Trim() : "0";
            eBtechReg.strMath = txtMath.Text.Trim().Length > 0 ? txtMath.Text.Trim() : "0";
            eBtechReg.strEngg = txtEngg.Text.Trim().Length > 0 ? txtEngg.Text.Trim() : "0";


            eBtechReg.strNameOfApplicant = txtNameOfApplicant.Text.Trim().Replace("'", "''");
            string[] Date = txtDob.Text.Trim().Split('/');
            eBtechReg.strDob = (Date[1].Trim() + "/" + Date[0].Trim() + "/" + Date[2].Trim() + " 00:00:00").ToString();
            eBtechReg.strFatherName = txtFatherName.Text.Trim().Replace("'", "''");
            eBtechReg.strFatherOccupation = txtFatherOccupation.Text.Trim().Replace("'", "''");
            eBtechReg.strMotherName = txtMotherName.Text.Trim().Replace("'", "''");
            eBtechReg.strMotherOccupation = txtMotherOccupation.Text.Trim().Replace("'", "''");
            eBtechReg.strGuardiansName = txtGuardiansName.Text.Trim().Replace("'", "''");
            eBtechReg.strPAddress = txtPAddress.Text.Trim().Replace("'", "''");
            eBtechReg.strCAddress = txtCAddress.Text.Trim().Replace("'", "''");


            eBtechReg.strPResidential = txtPResidential.Text.Trim().Replace("'", "''");
            eBtechReg.strSResidential = txtSResidential.Text.Trim().Replace("'", "''");
            eBtechReg.strPMobile = txtPMobile.Text.Trim().Replace("'", "''");
            eBtechReg.strSMobile = txtSMobile.Text.Trim().Replace("'", "''");
            eBtechReg.strEmail = txtEmail.Text.Trim().Replace("'", "''");
            eBtechReg.strHostalFacitity = findHostalSelected();
            eBtechReg.strGender = findGender();
            eBtechReg.strMarital = findMaritalStatus();
            eBtechReg.strMotherTong = txtMotherTong.Text.Trim().Replace("'", "''");
            eBtechReg.strNationality = txtNationality.Text.Trim().Replace("'", "''");
            eBtechReg.strRealigion = txtRealigion.Text.Trim().Replace("'", "''");
            eBtechReg.strBengali = findBLanguagePower();
            eBtechReg.strHindi = findHLanguagePower();
            eBtechReg.strEnglish = findELanguagePower();
            eBtechReg.strOther = findOLanguagePower();
            eBtechReg.strCast = findCast();
            eBtechReg.strMonthlyIncome = txtMonthlyIncome.Text.Trim().Replace("'", "''");
            eBtechReg.strRefferenceName1 = txtRefferenceName1.Text.Trim().Replace("'", "''");
            eBtechReg.strRefferenceAddress1 = txtRefferenceAddress1.Text.Trim().Replace("'", "''");
            eBtechReg.strRefferenceContactNumber1 = txtRefferenceContactNumber1.Text.Trim().Replace("'", "''");
            eBtechReg.strRefferenceName2 = txtRefferenceName2.Text.Trim().Replace("'", "''");
            eBtechReg.strRefferenceAddress2 = txtRefferenceAddress2.Text.Trim().Replace("'", "''");
            eBtechReg.strRefferenceContactNumber2 = txtRefferenceContactNumber2.Text.Trim().Replace("'", "''");

            eBtechReg.strXSubject = txtXSubject.Text.Trim().Replace("'", "''");
            eBtechReg.strXBoard = txtXBoard.Text.Trim().Replace("'", "''");
            eBtechReg.strXCollege = txtXCollege.Text.Trim().Replace("'", "''");
            eBtechReg.strXYearOfPassing = txtXYearOfPassing.Text.Trim().Replace("'", "''");
            eBtechReg.strXMarks = txtXMarks.Text.Trim().Replace("'", "''");

            eBtechReg.strXiiSubject = txtXiiSubject.Text.Trim().Replace("'", "''");
            eBtechReg.strXiiBoard = txtXiiBoard.Text.Trim().Replace("'", "''");
            eBtechReg.strXiiCollege = txtXiiCollege.Text.Trim().Replace("'", "''");
            eBtechReg.strXiiYearOfPassing = txtXiiYearOfPassing.Text.Trim().Replace("'", "''");
            eBtechReg.strXiiMarks = txtXiiMarks.Text.Trim().Replace("'", "''");

            eBtechReg.strGSubject = txtGSubject.Text.Trim().Replace("'", "''");
            eBtechReg.strGBoard = txtGBoard.Text.Trim().Replace("'", "''");
            eBtechReg.strGCollege = txtGCollege.Text.Trim().Replace("'", "''");
            eBtechReg.strGYearOfPassing = txtGYearOfPassing.Text.Trim().Replace("'", "''");
            eBtechReg.strGMarks = txtGMarks.Text.Trim().Replace("'", "''");

            eBtechReg.strDSubject = txtDSubject.Text.Trim().Replace("'", "''");
            eBtechReg.strDBoard = txtDBoard.Text.Trim().Replace("'", "''");
            eBtechReg.strDCollege = txtDCollege.Text.Trim().Replace("'", "''");
            eBtechReg.strDYearOfPassing = txtDYearOfPassing.Text.Trim().Replace("'", "''");
            eBtechReg.strDMarks = txtDMarks.Text.Trim().Replace("'", "''");

            eBtechReg.strDocList1 = chkListDoc1.Checked == true ? "1" : "0";
            eBtechReg.strDocList2 = chkListDoc2.Checked == true ? "2" : "0";
            eBtechReg.strDocList3 = chkListDoc3.Checked == true ? "3" : "0";
            eBtechReg.strDocList4 = chkListDoc4.Checked == true ? "4" : "0";
            eBtechReg.strDocList5 = chkListDoc5.Checked == true ? "5" : "0";
            eBtechReg.strDocList6 = chkListDoc6.Checked == true ? "6" : "0";
            eBtechReg.strDocList7 = chkListDoc7.Checked == true ? "7" : "0";
            eBtechReg.strDocList8 = chkListDoc8.Checked == true ? "8" : "0";
            eBtechReg.strDocList9 = chkListDoc9.Checked == true ? "9" : "0";
            eBtechReg.BloodGroup = txtBloodGroup.Text.Trim();
            eBtechReg.strPhoto = Photo;

            int RowsAffected = BtechReg.Save(eBtechReg);
            if (RowsAffected > 0)
            {
                if (Photo != "")
                {
                    string ff = (Server.MapPath("..") + "\\Student\\StudentPhoto\\" + eBtechReg.studentID + Photo);
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
