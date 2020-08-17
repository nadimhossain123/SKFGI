﻿<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="MTechRegistration.aspx.cs" Inherits="CollegeERP.Student.MTechRegistration"
    Title="MTech Registration" %>

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
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlstate.ClientID%>'), "State", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlDistrict.ClientID%>'), "District", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlCity.ClientID%>'), "City", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtpin.ClientID%>'), "Pin", 1)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlSchool.ClientID%>'), "School/College", 0)) return false;
            return confirm('Are You Sure?');

        }
    </script>
    <style type="text/css">
        .textbox
        {
            text-transform: uppercase;
        }
         .textbox_required{
     text-transform: uppercase;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            MTech Registration</h5>
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
                        Admission In<span class="req">*</span>
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlBatch" runat="server" Width="192px" CssClass="dropdownList"
                            DataTextField="batch_name" DataValueField="id">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Stream applied<span class="req">*</span>
                    </td>
                    <td align="left">
                        <asp:CheckBoxList ID="chkStream" runat="server" RepeatDirection="Horizontal" DataTextField="stream_name"
                            DataValueField="StreamId">
                        </asp:CheckBoxList>
                    </td>
                    <td align="left" width="20%" class="label">
                        Option
                    </td>
                    <td align="left">
                        <asp:RadioButton ID="rbPgate" runat="server" Text="PGATE" GroupName="mtechRank" Checked="true" />&nbsp;&nbsp;
                        <asp:RadioButton ID="rbGate" runat="server" Text="GATE" GroupName="mtechRank" />&nbsp;&nbsp;
                        <asp:RadioButton ID="rbDirect" runat="server" Text="DIRECT" GroupName="mtechRank" />&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Hostel Facility
                    </td>
                    <td align="left">
                        <%--<asp:CheckBox ID="ChkHostelFacility" runat="server" />--%>
                        <asp:RadioButtonList ID="RdbHostelFacility" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="True">YES</asp:ListItem>
                            <asp:ListItem Value="False">NO</asp:ListItem>
                        </asp:RadioButtonList>
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
                        Registration No
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtRegistrationNo" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label">
                        University RollNo
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtUniversityRollNo" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Migration Info
                    </td>
                    <td align="left" colspan="3">
                        <asp:TextBox ID="txtMigrationInfo" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <h6 align="left" style="color: #00356A;">
            1. Personal Details</h6>
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

                
                <%--Added by Biswajit--%>
                <tr>
                    <td align="left" width="20%" class="label">
                        Adhar No.
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtAdhar" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label">
                        Bank Account Name
                    </td>
                    <td align="left" width="30%">
                        
                        <asp:TextBox ID="txtBankAccountName" runat="server" Width="80%" CssClass="textbox"></asp:TextBox>
                    
                    </td>
                </tr>

                <tr>
                    <td align="left" width="20%" class="label">
                        Bank Account No. 
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtBankAcNo" runat="server" Width="80%" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label">
                        Bank IFSC Code
                    </td>
                    <td align="left" width="30%">
                        
                        <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>
                    </td>
                </tr>
                <%-- end --%>

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
                <%--  <tr>
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
                </tr>--%>
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
                        State <span class="req">*</span>
                    </td>
                    <td align="left" width="30%">
                        <asp:DropDownList ID="ddlstate" runat="server" CssClass="dropdownList" Width="140px"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td align="left" width="20%" class="label">
                        District <span class="req">*</span>
                    </td>
                    <td align="left" width="30%">
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="dropdownList" Width="140px"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        City <span class="req">*</span>
                    </td>
                    <td align="left" width="30%">
                        <asp:DropDownList ID="ddlCity" runat="server" CssClass="dropdownList" Width="140px">
                        </asp:DropDownList>
                    </td>
                    <td align="left" width="20%" class="label">
                        Pin<span class="req">*</span>
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtpin" runat="server" CssClass="textbox_required" Width="140px"
                            MaxLength="9"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="fft" runat="server" TargetControlID="txtpin" ValidChars="0123456789"
                            FilterMode="ValidChars">
                        </asp:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox ID="chkSameasAbove" runat="server" Text="Same as above" AutoPostBack="true"
                            OnCheckedChanged="chkSameasAbove_CheckedChanged" Style="float: left" />
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
                <tr>
                    <td align="left" width="20%" class="label">
                        State
                    </td>
                    <td align="left" width="30%">
                        <asp:DropDownList ID="ddlStatesearch" runat="server" AutoPostBack="True" CssClass="dropdownList"
                            OnSelectedIndexChanged="ddlStatesearch_SelectedIndexChanged" Width="140px">
                        </asp:DropDownList>
                    </td>
                    <td align="left" width="20%" class="label">
                        District&nbsp;
                    </td>
                    <td align="left" width="30%">
                        <asp:DropDownList ID="ddlDistrictSearch" runat="server" CssClass="dropdownList" Width="140px"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlDistrictSearch_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        City&nbsp;
                    </td>
                    <td align="left" width="30%">
                        <asp:DropDownList ID="ddlcitysearch" runat="server" CssClass="dropdownList" Width="140px">
                        </asp:DropDownList>
                    </td>
                    <td align="left" width="20%" class="label">
                        Pin
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtcPin" runat="server" CssClass="textbox" Width="140px" MaxLength="9"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtcPin"
                            ValidChars="0123456789" FilterMode="ValidChars">
                        </asp:FilteredTextBoxExtender>
                    </td>
                </tr>
            </table>
        </div>
        <h6 align="left" style="color: #00356A;">
            2. Contact Nos</h6>
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
                            onblur="EmailOnly(this)" Style="text-transform: lowercase"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label">
                    </td>
                    <td align="left" width="30%">
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Hostel Facility<br />
                        Required
                    </td>
                    <td align="left" width="30%" colspan="3">
                        <asp:RadioButton ID="rbHostalY" runat="server" Text="Yes" GroupName="hostel" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbHostalN" runat="server" Text="No" GroupName="hostel" Checked="true" />
                        &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span class="label">Gender</span>
                        <asp:RadioButton ID="rbMale" runat="server" Text="M" GroupName="gender" Checked="true" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbFemale" runat="server" Text="F" GroupName="gender" />
                        &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span class="label">Marital Status </span>
                        <asp:RadioButton ID="rbSingle" runat="server" Text="Single" GroupName="marital" Checked="true" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbMarried" runat="server" Text="Married" GroupName="marital" />
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">
                        Nationality
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtNationality" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label">
                        Religion
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtRealigion" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
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
                    </td>
                    <td align="left" width="30%">
                    </td>
                </tr>
            </table>
        </div>
        <h6 align="left" style="color: #00356A;">
            3. Educational Background</h6>
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
                                            Bord /
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
                                        BTech
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
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td align="left" width="137px" class="label">
                                        School/College<span class="req">*</span>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlSchool" runat="server" CssClass="dropdownList" Width="180px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div>
            </div>
            <table width="100%" align="center" class="table">
                <tr>
                    <td align="left" width="20%" class="label" style="color: #00356A;">
                        GATE Score (If any)
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtGateScore" runat="server" CssClass="textbox" Width="150px" onkeypress="return AmountOnly('txtGateScore',this);"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label" style="color: #00356A;">
                    </td>
                    <td align="left" width="30%">
                    </td>
                </tr>
            </table>
        </div>
        <h6 align="left" style="color: #00356A;">
            4. Year of Experience</h6>
        <div style="width: 740px;">
            <table width="100%" align="center" class="table">
                <tr>
                    <td align="left" width="20%" class="label" style="color: #00356A;">
                        Academic
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtAcademicYrExp" runat="server" CssClass="textbox" Width="150px"
                            onkeypress="return AmountOnly('txtAcademicYrExp',this);"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label" style="color: #00356A;">
                        Industry
                    </td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtIndustryYrExp" runat="server" CssClass="textbox" Width="150px"
                            onkeypress="return AmountOnly('txtIndustryYrExp',this);"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <h6 align="left" style="color: #00356A;">
            5. List Of Document Required For Admission In MTech
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
                        Proof of Age (Admit Card of Secondary Standard).
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
                        Mark Sheet of Graduation.
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
                        Gate Score Card (If any).
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
                        Proof of SC/ST (Caste Certificate).
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
                        Ten copies of Colour Photograph (Stamp Size).
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
                        No Objection Certificate from employer (If employed).
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
                        Migration Certificate (If required).
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
                        Blood Group Report.
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
                    <td align="left">
                        Admitted By :</td>
                    <td align="right">
                        <asp:TextBox ID="txtSubmittedBy" runat="server" CssClass="textbox" 
                            MaxLength="100" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClientClick="return Validation();"
                            OnClick="btnSave_Click" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" OnClick="btnCancel_Click" />
                        &nbsp;
                        <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" />
                    </td>
                </tr>
            </table>
        </div>
</asp:Content>
