<%@ Page Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true"
    CodeBehind="BTechRegistration.aspx.cs" Inherits="CollegeERP.Admission.BTechRegistration"
    Title="B.Tech Registration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript" src="../Scripts/ssjscript.js"></script>

    <script language="javascript" type="text/javascript">
        function Validation() {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlBatch.ClientID%>'), "Addmission In", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtNameOfApplicant.ClientID%>'), "Name Of Applicant", 1)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtDob.ClientID%>'), " Date of Birth", 22)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtFatherName.ClientID%>'), "Father Name", 1)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtPAddress.ClientID%>'), "Permanent Address", 1)) return false;
            return confirm('Are You Sure?');

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            B.Tech Registration</h5>
    </div>
    <div>
        <div style="width: 740px;">
            <uc3:Message ID="Message" runat="server" />
        </div>
        <div style="width: 740px;">
            <table width="100%" align="center" class="table">
                <tr>
                    <td align="left" width="20%">
                        <asp:Image ID="ImgPhoto" runat="server" Width="80px" Height="80px" />
                    </td>
                    <td align="left" colspan="3">
                        <asp:FileUpload ID="uploadImage" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Addmission In<span class="req">*</span>
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlBatch" runat="server" Width="192px" CssClass="dropdownList"
                            DataValueField="id" DataTextField="batch_name">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Enrollment No
                    </td>
                    <td>
                        <asp:TextBox ID="txtEnrollmentNo" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label">
                        Option
                    </td>
                    <td align="left">
                        <asp:RadioButton ID="rbWbjee" runat="server" Text="WBJEE" GroupName="enggRank" Checked="true" />&nbsp;&nbsp;
                        <asp:RadioButton ID="rbAieee" runat="server" Text="AIEEE" GroupName="enggRank" />&nbsp;&nbsp;
                        <asp:RadioButton ID="rbjelat" runat="server" Text="JELAT" GroupName="enggRank" />&nbsp;&nbsp;
                        <asp:RadioButton ID="rbDirect" runat="server" Text="DIRECT" GroupName="enggRank" />&nbsp;&nbsp;
                        <asp:RadioButton ID="rbMQ" runat="server" Text="MQ" GroupName="enggRank" />&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Lateral
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="ChkLateral" runat="server" />
                    </td>
                    <td align="left" width="20%" class="label">
                        Rank
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtRankid" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Hostel Facility
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="ChkHostelFacility" runat="server" />
                    </td>
                    <td align="left" width="20%" class="label">
                        TFW
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="ChkTFW" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Re-Admission
                    </td>
                    <td colspan="3">
                        <asp:CheckBox ID="ChkIsReAdmission" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <h6 align="left" style="color: #00356A;">
            1. 4-yr. B.Tech courses are offered in the following branches</h6>
        <div style="width: 740px;">
            <table width="100%" align="center" class="table">
                <tr>
                    <td align="left" colspan="2">
                        a. Applied Electronics and Instrumentation Engineering (AEIE)
                        <br />
                        <br />
                        b. Civil Engineering (CE)
                        <br />
                        <br />
                        c. Computer Science and Engineering (CSE) and
                        <br />
                        <br />
                        d. Electronic and Communication Engineering (ECE).
                        <br />
                        <br />
                        e. Electrical Engineering (EE)
                        <br />
                        <br />
                        f. Mechanical Engineering (ME)
                        <br />
                        <br />
                        Students who have cleared 10+2 with at least 45% marks in Physics, Chemistry and
                        Mathematics are eligible for admission through JEE. AIEEE
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Stream applied<span class="req">*</span>
                    </td>
                    <td align="left">
                        <%--<asp:CheckBox ID="chkEce" runat="server" Text="ECE " />&nbsp;&nbsp;
                                <asp:CheckBox ID="chkEe" runat="server" Text="EE" />&nbsp;&nbsp;
                                <asp:CheckBox ID="chkCse" runat="server" Text="CSE" />&nbsp;&nbsp;
                                <asp:CheckBox ID="chkAeie" runat="server" Text="AEIE" />&nbsp;&nbsp;
                                <asp:CheckBox ID="chkMe" runat="server" Text="ME" />&nbsp;&nbsp;
                                <asp:CheckBox ID="chkCe" runat="server" Text="CE" />&nbsp;&nbsp;--%>
                        <asp:CheckBoxList ID="chkStream" runat="server" RepeatDirection="Horizontal">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Percentage of marks (10+2)
                    </td>
                    <td align="left">
                        <table style="width: 80%">
                            <tr>
                                <td>
                                    Phy
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPhy" runat="server" CssClass="textbox" Width="50px" onKeyPress="return NumbersOnly(event)"
                                        onblur="isNumber(this)"></asp:TextBox>
                                </td>
                                <td>
                                    Chem
                                </td>
                                <td>
                                    <asp:TextBox ID="txtChem" runat="server" CssClass="textbox" Width="50px" onKeyPress="return NumbersOnly(event)"
                                        onblur="isNumber(this)"></asp:TextBox>
                                </td>
                                <td>
                                    Math
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMath" runat="server" CssClass="textbox" Width="50px" onKeyPress="return NumbersOnly(event)"
                                        onblur="isNumber(this)"></asp:TextBox>
                                </td>
                                <td>
                                    Eng
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEngg" runat="server" CssClass="textbox" Width="50px" onKeyPress="return NumbersOnly(event)"
                                        onblur="isNumber(this)"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <h6 align="left" style="color: #00356A;">
            2. Personal Details</h6>
        <div style="width: 740px;">
            <table width="100%" align="center" class="table">
                <tr>
                    <td align="left" width="20%" class="label">
                        Name of Applicant <span class="req">*</span>
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtNameOfApplicant" runat="server" CssClass="textbox_required" Width="80%"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label">
                        Date of Birth <span class="req">*</span>
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtDob" runat="server" CssClass="textbox_required" Width="80%"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                            PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtDob" OnClientDateSelectionChanged=""
                            Enabled="True">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Father's Name<span class="req">*</span>
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtFatherName" runat="server" CssClass="textbox_required" Width="80%"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label">
                        Occupation
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtFatherOccupation" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Mother's Name
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtMotherName" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label">
                        Occupation
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtMotherOccupation" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Guardian's Name
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtGuardiansName" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label">
                    </td>
                    <td align="left" width="30%">
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Permanent Address<span class="req">*</span>
                    </td>
                    <td align="left" width="30%" colspan="3">
                        <asp:TextBox ID="txtPAddress" runat="server" CssClass="textbox_required" Width="550px"
                            Height="40px" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Correspondence Address
                    </td>
                    <td align="left" width="30%" colspan="3">
                        <asp:TextBox ID="txtCAddress" runat="server" CssClass="textbox" Width="550px" Height="40px"
                            TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <h6 align="left" style="color: #00356A;">
            3. Contact Nos</h6>
        <div style="width: 740px;">
            <table width="100%" align="center" class="table">
                <tr>
                    <td align="left" width="20%" class="label" style="color: #00356A;">
                        Parents/Guardian
                    </td>
                    <td align="left" width="30%">
                    </td>
                    <td align="left" width="20%" class="label" style="color: #00356A;">
                        Student
                    </td>
                    <td align="left" width="30%">
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Residential
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtPResidential" runat="server" CssClass="textbox" Width="80%" onKeyPress="return NumbersOnly(event)"
                            onblur="isNumber(this)"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label">
                        Residential
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtSResidential" runat="server" CssClass="textbox" Width="80%" onKeyPress="return NumbersOnly(event)"
                            onblur="isNumber(this)"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Mobile
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtPMobile" runat="server" CssClass="textbox" Width="80%" onKeyPress="return NumbersOnly(event)"
                            onblur="isNumber(this)"> </asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label">
                        Mobile
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtSMobile" runat="server" CssClass="textbox" Width="80%" onKeyPress="return NumbersOnly(event)"
                            onblur="isNumber(this)"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        E-mail for
                        <br />
                        Correspondence
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Width="80%" onKeyPress="return EmailOnly(event)"
                            onblur="EmailOnly(this)"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label">
                    </td>
                    <td align="left" width="30%">
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        <%--Hostel Facility<br />
                        Required--%>Gender
                    </td>
                    <td align="left" width="30%" colspan="3">
                        
                        <asp:RadioButton ID="rbMale" runat="server" Text="M" GroupName="gender" Checked="true" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbFemale" runat="server" Text="F" GroupName="gender" />
                        &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span class="label">Marital Status </span>
                        <asp:RadioButton ID="rbSingle" runat="server" Text="Single" GroupName="marital" Checked="true" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbMarried" runat="server" Text="Married" GroupName="marital" />
                        <asp:RadioButton ID="rbHostalY" runat="server" Text="Yes" GroupName="hostel" Visible="false" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbHostalN" runat="server" Text="No" GroupName="hostel" Checked="true" Visible="false" />
                        &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span class="label"></span>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Mother Tongue
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtMotherTong" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label">
                        Nationality
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtNationality" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Religion
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtRealigion" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label">
                        Blood Group
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtBloodGroup" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label" colspan="4" style="color: #00356A;">
                        Language Known
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Bengali
                    </td>
                    <td align="left" width="30%">
                        <asp:CheckBox ID="chkBSpeak" runat="server" Text="Speak" />&nbsp;
                        <asp:CheckBox ID="chkBRead" runat="server" Text="Read" />&nbsp;
                        <asp:CheckBox ID="chkBWrite" runat="server" Text="Write" />
                    </td>
                    <td align="left" width="20%" class="label">
                        Hindi
                    </td>
                    <td align="left" width="30%">
                        <asp:CheckBox ID="chkHSpeak" runat="server" Text="Speak" />&nbsp;
                        <asp:CheckBox ID="chkHRead" runat="server" Text="Read" />&nbsp;
                        <asp:CheckBox ID="chkHWrite" runat="server" Text="Write" />
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        English
                    </td>
                    <td align="left" width="30%">
                        <asp:CheckBox ID="chkESpeak" runat="server" Text="Speak" />&nbsp;
                        <asp:CheckBox ID="chkERead" runat="server" Text="Read" />&nbsp;
                        <asp:CheckBox ID="chkEWrite" runat="server" Text="Write" />
                    </td>
                    <td align="left" width="20%" class="label">
                        Other
                    </td>
                    <td align="left" width="30%">
                        <asp:CheckBox ID="chkOSpeak" runat="server" Text="Speak" />&nbsp;
                        <asp:CheckBox ID="chkORead" runat="server" Text="Read" />&nbsp;
                        <asp:CheckBox ID="chkOWrite" runat="server" Text="Write" />
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Caste
                    </td>
                    <td align="left" width="30%">
                        <asp:RadioButton ID="rbGeneral" runat="server" Text="G" GroupName="cast" Checked="true" />
                        &nbsp
                        <asp:RadioButton ID="rbSc" runat="server" Text="SC" GroupName="cast" />
                        &nbsp
                        <asp:RadioButton ID="rbSt" runat="server" Text="ST" GroupName="cast" />
                        &nbsp
                        <asp:RadioButton ID="rbObc" runat="server" Text="OBC" GroupName="cast" />
                    </td>
                    <td align="left" width="20%" class="label">
                        Monthly Income
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtMonthlyIncome" runat="server" CssClass="textbox" Width="150px"
                            onKeyPress="return NumbersOnly(event)" onblur="isNumber(this)"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <h6 align="left" style="color: #00356A;">
            4. Two References</h6>
        <div style="width: 740px;">
            <table width="100%" align="center" class="table">
                <tr>
                    <td align="left" width="20%" class="label">
                        1. Name
                    </td>
                    <td>
                        <asp:TextBox ID="txtRefferenceName1" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Address
                    </td>
                    <td align="left" width="30%" colspan="2">
                        <asp:TextBox ID="txtRefferenceAddress1" runat="server" CssClass="textbox" Width="550px"
                            Height="40px" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Contact Number
                    </td>
                    <td>
                        <asp:TextBox ID="txtRefferenceContactNumber1" runat="server" CssClass="textbox" Width="150px"
                            onKeyPress="return NumbersOnly(event)" onblur="isNumber(this)"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        2. Name
                    </td>
                    <td>
                        <asp:TextBox ID="txtRefferenceName2" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Address
                    </td>
                    <td align="left" width="30%" colspan="2">
                        <asp:TextBox ID="txtRefferenceAddress2" runat="server" CssClass="textbox" Width="550px"
                            Height="40px" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Contact Number
                    </td>
                    <td>
                        <asp:TextBox ID="txtRefferenceContactNumber2" runat="server" CssClass="textbox" Width="150px"
                            onKeyPress="return NumbersOnly(event)" onblur="isNumber(this)"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <h6 align="left" style="color: #00356A;">
            5. Educational Background</h6>
        <div style="width: 740px;">
            <table class="table" width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <table id="ex" cellspacing="0" border="1" style="width: 100%; border-collapse: collapse;"
                                rules="all">
                                <tbody>
                                    <tr class="HeaderStyle">
                                        <th scope="col">
                                            Exam Passed
                                        </th>
                                        <th scope="col">
                                            Major Subject
                                        </th>
                                        <th scope="col">
                                            Board /
                                            <br />
                                            University
                                        </th>
                                        <th scope="col">
                                            Institution /<br />
                                            College
                                        </th>
                                        <th scope="col">
                                            Year of
                                            <br />
                                            Passing
                                        </th>
                                        <th scope="col">
                                            Division &
                                            <br />
                                            % Marks
                                        </th>
                                    </tr>
                                </tbody>
                                <tr class="RowStyle">
                                    <td class="label" style="color: #00356A;">
                                        Class X
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtXSubject" runat="server" CssClass="textbox" Style="width: 100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtXBoard" runat="server" CssClass="textbox" Style="width: 100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtXCollege" runat="server" CssClass="textbox" Style="width: 100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtXYearOfPassing" runat="server" CssClass="textbox" Style="width: 100px"
                                            onKeyPress="return NumbersOnly(event)" onblur="isNumber(this)"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtXMarks" runat="server" CssClass="textbox" Style="width: 100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="RowStyle">
                                    <td class="label" style="color: #00356A;">
                                        Class XII
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtXiiSubject" runat="server" CssClass="textbox" Style="width: 100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtXiiBoard" runat="server" CssClass="textbox" Style="width: 100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtXiiCollege" runat="server" CssClass="textbox" Style="width: 100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtXiiYearOfPassing" runat="server" CssClass="textbox" Style="width: 100px"
                                            onKeyPress="return NumbersOnly(event)" onblur="isNumber(this)"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtXiiMarks" runat="server" CssClass="textbox" Style="width: 100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="RowStyle">
                                    <td class="label" style="color: #00356A;">
                                        Diploma
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDSubject" runat="server" CssClass="textbox" Style="width: 100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDBoard" runat="server" CssClass="textbox" Style="width: 100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDCollege" runat="server" CssClass="textbox" Style="width: 100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDYearOfPassing" runat="server" CssClass="textbox" Style="width: 100px"
                                            onKeyPress="return NumbersOnly(event)" onblur="isNumber(this)"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDMarks" runat="server" CssClass="textbox" Style="width: 100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="RowStyle">
                                    <td class="label" style="color: #00356A;">
                                        Graduation
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGSubject" runat="server" CssClass="textbox" Style="width: 100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGBoard" runat="server" CssClass="textbox" Style="width: 100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGCollege" runat="server" CssClass="textbox" Style="width: 100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGYearOfPassing" runat="server" CssClass="textbox" Style="width: 100px"
                                            onKeyPress="return NumbersOnly(event)" onblur="isNumber(this)"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGMarks" runat="server" CssClass="textbox" Style="width: 100px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                    </tr>
                </tbody>
            </table>
            <div>
            </div>
        </div>
        <h6 align="left" style="color: #00356A;">
            6. List Of Document Required For Admission In B.Tech
        </h6>
        <div style="width: 740px;">
            <table width="100%" align="center" class="table">
                <tr>
                    <td align="left" style="color: #00356A; width: 5px">
                        i)
                    </td>
                    <td align="left" width="5px">
                        <asp:CheckBox ID="chkListDoc1" runat="server" />
                    </td>
                    <td class="label" align="left">
                        Ten copies of colour stamp size photograph (Including Front Page Copy).
                    </td>
                </tr>
                <tr>
                    <td align="left" style="color: #00356A; width: 5px">
                        ii)
                    </td>
                    <td align="left" width="5px">
                        <asp:CheckBox ID="chkListDoc2" runat="server" />
                    </td>
                    <td class="label" align="left">
                        Original Admit Card of Secondary Standard in support of Age Proof! Date of Birth
                        along with two Xerox copies.
                    </td>
                </tr>
                <tr>
                    <td align="left" style="color: #00356A; width: 5px">
                        iii)
                    </td>
                    <td align="left" width="5px">
                        <asp:CheckBox ID="chkListDoc3" runat="server" />
                    </td>
                    <td class="label" align="left">
                        Original Mark Sheet of 10+2 Examination passed along with two Xerox copies.
                    </td>
                </tr>
                <tr>
                    <td align="left" style="color: #00356A; width: 5px">
                        iv)
                    </td>
                    <td align="left" width="5px">
                        <asp:CheckBox ID="chkListDoc4" runat="server" />
                    </td>
                    <td class="label" align="left">
                        Original Mark Sheet of Graduation Examination along with two Xerox copies.
                    </td>
                </tr>
                <tr>
                    <td align="left" style="color: #00356A; width: 5px">
                        v)
                    </td>
                    <td align="left" width="5px">
                        <asp:CheckBox ID="chkListDoc5" runat="server" />
                    </td>
                    <td class="label" align="left">
                        Registration Number with Year is mandatory.
                    </td>
                </tr>
                <tr>
                    <td align="left" style="color: #00356A; width: 5px">
                        vi)
                    </td>
                    <td align="left" width="5px">
                        <asp:CheckBox ID="chkListDoc6" runat="server" />
                    </td>
                    <td class="label" align="left">
                        Original School Leaving Certificate / Character Certificate from the Head of the
                        Institution studied.
                    </td>
                </tr>
                <tr>
                    <td align="left" style="color: #00356A; width: 5px">
                        vii)
                    </td>
                    <td align="left" width="5px">
                        <asp:CheckBox ID="chkListDoc7" runat="server" />
                    </td>
                    <td class="label" align="left">
                        Original University Migration Certificate (if applicable).
                    </td>
                </tr>
                <tr>
                    <td align="left" style="color: #00356A; width: 5px">
                        viii)
                    </td>
                    <td align="left" width="5px">
                        <asp:CheckBox ID="chkListDoc8" runat="server" />
                    </td>
                    <td class="label" align="left">
                        Blood Group report by Pathological Centre. (May be submitted later but within seven
                        days from the date of commencement of the 1st session)
                    </td>
                </tr>
                <tr>
                    <td align="left" style="color: #00356A; width: 5px">
                        ix)
                    </td>
                    <td align="left" width="5px">
                        <asp:CheckBox ID="chkListDoc9" runat="server" />
                    </td>
                    <td class="label" align="left">
                        Caste Certificate in respect of SC 1ST candidates (if applicable).
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="color: #00356A;">
                        (The above documents are required to be produced during admission at the Campus).
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 740px;">
            <table width="100%" align="center" class="table">
                <tr>
                    <td align="right">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClientClick="return Validation();"
                            OnClick="btnSave_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
