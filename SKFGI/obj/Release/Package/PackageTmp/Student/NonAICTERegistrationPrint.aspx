<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NonAICTERegistrationPrint.aspx.cs" Inherits="SKFGI.Student.NonAICTERegistrationPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body {
            margin: 0;
            padding: 0;
            height: 100%;
            font-family: Lucida Grande, Verdana, Lucida Sans Regular, Lucida Sans Unicode, Arial, sans-serif;
            font-size: 11px;
        }

        h6 {
            font-size: 11px;
            color: #292929;
            font-weight: bold;
        }

        body span {
            text-transform: uppercase;
        }

        .style1 {
            width: 28%;
        }

        .style2 {
            width: 23%;
        }
    </style>
    <style type="text/css">
        .textbox {
            text-transform: uppercase;
        }
    </style>
    <link href="../Styles/Control.css" rel="Stylesheet" type="text/css" />
</head>
<body bgcolor="white">
    <form id="form1" runat="server">
        <center>
        <br />
        <br />
        <div style="width: 760px;">
            <table width="100%" align="center" class="table">
                <tr>
                    <td align="left" width="20%">
                        Applicant No:
                    </td>
                    <td align="left" colspan="3">
                        <asp:Label ID="lblapplicationNumber" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <img src="../Images/PrintNALogo.JPG" width="100%" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Literal ID="ltrBatchname" runat="server" Mode="PassThrough"></asp:Literal><br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="1" align="left">
                    
                         <asp:Label ID="Label1" runat="server" Font-Size="14px" Text="Name Of Applicant :-"></asp:Label>
                    </td>
                    <td colspan="2" align="left">
                        <asp:Label ID="lblApplicantName" runat="server" Font-Size="18px"></asp:Label>
                    </td>
                     <td align="center" rowspan="6">
                        <asp:Image ID="ImgPhoto" runat="server" Width="90px" Height="90px" />
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        Enrollment No
                    </td>
                    <td colspan="3" align="left">
                        <asp:Label ID="lblEnrollmentNo" runat="server"></asp:Label>
                    </td>
                   
                </tr>
                <tr>
                    <td align="left" width="20%" valign="bottom">
                        Option
                    </td>
                    <td align="left" valign="bottom">
                        <asp:Label ID="lblOption" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" valign="top"  style="padding-top:2px">
                        Rank
                    </td>
                    <td align="left" valign="top" style="padding-top:2px">
                        <asp:Label ID="lblRankid" runat="server"></asp:Label>
                    </td>
                </tr>
<%--                <tr>
                    <td align="left" width="20%" valign="top"  style="padding-top:2px">
                        T F W
                    </td>
                    <td align="left" valign="top" style="padding-top:2px">
                           <asp:RadioButtonList ID="DDLTFW" runat="server" 
                            RepeatDirection="Horizontal" Enabled="false">
                            <asp:ListItem Value="True">YES</asp:ListItem>
                            <asp:ListItem Value="False">NO</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>--%>
<%--                 <tr>
                    <td align="left" width="20%" valign="top"  style="padding-top:2px">
                        Lateral
                    </td>
                    <td align="left" valign="top" style="padding-top:2px">
                         <asp:CheckBox ID="chkLateral" runat="server" onclick="return false;" />
                         <asp:RadioButtonList ID="RDBLateral" runat="server" 
                            RepeatDirection="Horizontal" Enabled="false">
                            <asp:ListItem Value="True">YES</asp:ListItem>
                            <asp:ListItem Value="False">NO</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>--%>
            </table>
        </div>
        <div style="width: 760px;">
            <table width="100%" align="center" class="table">
                 <tr>
                    <td colspan="2">
                       &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style1">
                        Stream applied :-
                    </td>
                    <td align="left">
                        <asp:Label ID="lblStreamApplied" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
               
<%--                <tr>
                    <td align="left" class="style1">
                        Percentage of marks (10+2) :-
                    </td>
                    <td align="left">
                        <table style="width: 80%">
                            <tr>
                                <td>
                                    Phy-
                                </td>
                                <td>
                                    <asp:Label ID="lblPhy" runat="server"></asp:Label>
                                </td>
                                <td>
                                    Chem-
                                </td>
                                <td>
                                    <asp:Label ID="lblChem" runat="server"></asp:Label>
                                </td>
                                <td>
                                    Math-
                                </td>
                                <td>
                                    <asp:Label ID="lblMath" runat="server"></asp:Label>
                                </td>
                                
                                 <td>
                                    Bios-
                                </td>
                                <td>
                                    <asp:Label ID="lblBios" runat="server"></asp:Label>
                                </td>
                                <td>
                                    Eng-
                                </td>
                                <td>
                                    <asp:Label ID="lblEngg" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>--%>
                 <tr>
                    <td align="left" width="20%" class="label">
                        School/College<span class="req"></span>
                    </td>
                    <td align="left">
                       
                        <asp:Label ID="lblschool" runat="server" ></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 760px;">
            <h6 align="left" style="color: #00356A;">
             <asp:Label ID="Label2" runat="server" Text=" 1. Personal Details" ></asp:Label>
               </h6>
            <table width="100%" align="center" class="table">
                <tr>
                    <td align="left" class="style2">
                        Name of Student :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblNameOfApplicant" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        Date of Birth :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblDob" runat="server"></asp:Label>
                    </td>
                </tr>
                <%--Added By Biswajit--%>

                 <tr>
                    <td align="left" class="style2">
                        Adhar No. :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblAdhar" runat="server" ></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        Bank Account Name:-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblAccName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style2">
                        Bank Account No. :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblAccNo" runat="server" ></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        IFSC Code :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblIFSC" runat="server"></asp:Label>
                    </td>
                </tr>
                <%--end--%>
                <tr>
                    <td align="left" class="style2">
                        Father's Name :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblFatherName" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        Occupation :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblFatherOccupation" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style2">
                        Mother's Name :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblMotherName" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        Occupation :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblMotherOccupation" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style2">
                        Guardian's Name :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblGuardiansName" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                    </td>
                    <td align="left" width="30%">
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style2">
                        Permanent Address :-
                    </td>
                    <td align="left" width="30%" colspan="3">
                        <asp:Label ID="lblPAddress" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style2">
                        State :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblpstate" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        District :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblpdistrict" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style2">
                        City :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblpcity" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        Pin :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblppin" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style2">
                        Correspondence Address :-
                    </td>
                    <td align="left" width="30%" colspan="3">
                        <asp:Label ID="lblCAddress" runat="server"></asp:Label>
                    </td>
                </tr>
                  <tr>
                    <td align="left" class="style2">
                        State :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblcstate" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        District :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblcdistrict" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style2">
                        City :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblccity" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        Pin :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblcpin" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 760px;">
            <h6 align="left" style="color: #00356A;">
             <asp:Label ID="Label3" runat="server" Text="2. Contact Nos" ></asp:Label>
                </h6>
            <table width="100%" align="center" class="table">
                <tr>
                    <td align="left" width="20%" style="color: #00356A;">
                    <asp:Label ID="Label5" runat="server" Text="Parents/Guardian" Font-Underline="True"></asp:Label>
                       
                    </td>
                    <td align="left" width="30%">
                    </td>
                    <td align="left" width="20%" style="color: #00356A;">
                      <asp:Label ID="Label6" runat="server" Text="Student" Font-Underline="True"></asp:Label>

                        
                    </td>
                    <td align="left" width="30%">
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        Residential :-</td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblPResidential" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        Residential
                        :-</td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblSResidential" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        Mobile
                        :-</td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblPMobile" runat="server"> </asp:Label>
                    </td>
                    <td align="left" width="20%">
                        Mobile
                        :-</td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblSMobile" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        E-mail for
                        <br />
                        Correspondence :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblEmail" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                    </td>
                    <td align="left" width="30%">
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        Hostel Facility Required :-
                    </td>
                    <td align="left" width="30%" colspan="3">
                        <asp:Label ID="lblHostelFacility" runat="server"></asp:Label>
                        &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span>Gender :-</span>
                        <asp:Label ID="lblGender" runat="server"></asp:Label>
                        &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span>Marital Status :-</span>
                        <asp:Label ID="lblMaritalStatus" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        Mother Tongue :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblMotherTong" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        Nationality :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblNationality" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        Religion :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblRealigion" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        Blood Group :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblBloodGroup" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" colspan="4" style="color: #00356A;">
                        Language Known 
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        Bengali :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblBengali" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        Hindi :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblHindi" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        English :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblEnglish" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        Other :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblOther" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        Cast :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblcast" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        Monthly Income :-
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblMonthlyIncome" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 760px;">
            <h6 align="left" style="color: #00356A;">
               <asp:Label ID="Label4" runat="server" Text="3. Two References" ></asp:Label>

                </h6>
            <table width="100%" align="center" class="table">
                <tr>
                    <td align="left" width="20%">
                        1. Name :-
                    </td>
                    <td>
                        <asp:Label ID="lblRefferenceName1" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        &nbsp;&nbsp;&nbsp;&nbsp;Address :-
                    </td>
                    <td align="left" width="30%" colspan="2">
                        <asp:Label ID="lblRefferenceAddress1" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        &nbsp;&nbsp;&nbsp;&nbsp;Contact Number :-
                    </td>
                    <td>
                        <asp:Label ID="lblRefferenceContactNumber1" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        2. Name :-
                    </td>
                    <td>
                        <asp:Label ID="lblRefferenceName2" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                       &nbsp;&nbsp;&nbsp;&nbsp;Address :-
                    </td>
                    <td align="left" width="30%" colspan="2">
                        <asp:Label ID="lblRefferenceAddress2" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        &nbsp;&nbsp;&nbsp;&nbsp;Contact Number :-
                    </td>
                    <td>
                        <asp:Label ID="lblRefferenceContactNumber2" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <br />
        <div style="width: 760px;">
            <h6 align="left" style="color: #00356A;">
            4. Educational Background
               </h6>
            <table class="table" width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <table id="ex" cellspacing="0" border="1" style="width: 100%; border-collapse: collapse;"
                                rules="all">
                                <tbody>
                                    <tr >
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
                                            Total Marks
                                            <br />
                                            Obtained
                                        </th>
                                        <th scope="col">
                                            Division &
                                            <br />
                                            % Marks
                                        </th>
                                    </tr>
                                </tbody>
                                <tr>
                                    <td style="color: #00356A;">
                                        Class X
                                    </td>
                                    <td>
                                        <asp:Label ID="lblXSubject" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblXBoard" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblXCollege" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblXYearOfPassing" runat="server"></asp:Label>
                                    </td>
                                     <td>
                                        <asp:Label ID="lblXTotalMrkObtn" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblXMarks" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="color: #00356A;">
                                        Class XII
                                    </td>
                                    <td>
                                        <asp:Label ID="lblXiiSubject" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblXiiBoard" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblXiiCollege" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblXiiYearOfPassing" runat="server"></asp:Label>
                                    </td>
                                     <td>
                                        <asp:Label ID="lblXiiTotalMrkObtn" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblXiiMarks" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="color: #00356A;">
                                        Diploma
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDSubject" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDBoard" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDCollege" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDYearOfPassing" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDTotalMrkObtn" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDMarks" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="color: #00356A;">
                                        Graduation
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGSubject" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGBoard" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGCollege" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGYearOfPassing" runat="server"></asp:Label>
                                    </td>
                                     <td>
                                        <asp:Label ID="lblGTotalMrkObtn" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGMarks" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                    </tr>
                </tbody>
            </table>
            <div>
            </div>
        </div>
        <div style="width: 760px;">
            <h6 align="left" style="color: #00356A;">
                5. List Of Document Submitted For Admission In B.Tech
            </h6>
            <table width="100%" align="center" class="table">
                <tr>
                    <td align="left" style="color: #00356A; width: 5px">
                        i)
                    </td>
                    <td align="left" width="5px">
                        <asp:CheckBox ID="chkListDoc1" runat="server" onclick="return false;" />
                    </td>
                    <td align="left">
                        Ten copies of colour stamp size photograph (Including Front Page Copy).
                    </td>
                </tr>
                <tr>
                    <td align="left" style="color: #00356A; width: 5px">
                        ii)
                    </td>
                    <td align="left" width="5px">
                        <asp:CheckBox ID="chkListDoc2" runat="server" onclick="return false;" />
                    </td>
                    <td align="left">
                        Original Admit Card of Secondary Standard in support of Age Proof! Date of Birth
                        along with two Xerox copies.
                    </td>
                </tr>
                <tr>
                    <td align="left" style="color: #00356A; width: 5px">
                        iii)
                    </td>
                    <td align="left" width="5px">
                        <asp:CheckBox ID="chkListDoc3" runat="server" onclick="return false;" />
                    </td>
                    <td align="left">
                        Original Mark Sheet of 10+2 Examination passed along with two Xerox copies.
                    </td>
                </tr>
                <tr>
                    <td align="left" style="color: #00356A; width: 5px">
                        iv)
                    </td>
                    <td align="left" width="5px">
                        <asp:CheckBox ID="chkListDoc4" runat="server" onclick="return false;" />
                    </td>
                    <td align="left">
                        Original Mark Sheet of Graduation Examination along with two Xerox copies.
                    </td>
                </tr>
                <tr>
                    <td align="left" style="color: #00356A; width: 5px">
                        v)
                    </td>
                    <td align="left" width="5px">
                        <asp:CheckBox ID="chkListDoc5" runat="server" onclick="return false;" />
                    </td>
                    <td align="left">
                        Registration Number with Year is mandatory.
                    </td>
                </tr>
                <tr>
                    <td align="left" style="color: #00356A; width: 5px">
                        vi)
                    </td>
                    <td align="left" width="5px">
                        <asp:CheckBox ID="chkListDoc6" runat="server" onclick="return false;" />
                    </td>
                    <td align="left">
                        Original School Leaving Certificate / Character Certificate from the Head of the
                        Institution studied.
                    </td>
                </tr>
                <tr>
                    <td align="left" style="color: #00356A; width: 5px">
                        vii)
                    </td>
                    <td align="left" width="5px">
                        <asp:CheckBox ID="chkListDoc7" runat="server" onclick="return false;" />
                    </td>
                    <td align="left">
                        Original University Migration Certificate (if applicable).
                    </td>
                </tr>
                <tr>
                    <td align="left" style="color: #00356A; width: 5px">
                        viii)
                    </td>
                    <td align="left" width="5px">
                        <asp:CheckBox ID="chkListDoc8" runat="server" onclick="return false;" />
                    </td>
                    <td align="left">
                        Blood Group report by Pathological Centre. (May be submitted later but within seven
                        days from the date of commencement of the 1st session)
                    </td>
                </tr>
                <tr>
                    <td align="left" style="color: #00356A; width: 5px">
                        ix)
                    </td>
                    <td align="left" width="5px">
                        <asp:CheckBox ID="chkListDoc9" runat="server" onclick="return false;" />
                    </td>
                    <td align="left">
                        Caste Certificate in respect of SC 1ST candidates (if applicable).
                    </td>
                </tr>
                                <tr>
                    <td align="left" style="color: #00356A; width: 5px">
                        x)
                    </td>
                    <td align="left" width="5px">
                        <asp:CheckBox ID="ChkListDoc10" runat="server" onclick="return false;" />
                    </td>
                    <td align="left">
                        Valid Rank Card of CET (if applicable).
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
            <h6 align="left" style="color: #00356A;">
                6. Declaration
            </h6>
            <table width="100%" align="center" class="table">
                <tr>
                    <td>
                        A) Declaration By a Student:<br /><br />
                        
                        To
                        <br />
                        The  Director
                        <br />
                        <%--<h5>--%>
                            SUPREME KNOWLEDGE FOUNDATION GROUP OF INSTITUTIONS
                        <%--</h5>--%>
                        <br />
                        <br />
                        I declare that the above information is true and correct to the best of my knowledge.
                        I, intend to take admission into your College in course in the session 
                        <asp:Label ID="lblSession" runat="server" ></asp:Label> .
                        do hereby promise that:
                        <ol>
                            <li>I shall wear the Institute dress and follow the dress code strictly inside the Campus.</li>
                            <li align="justify">I will be regular in my classes throughout the year and I will maintain minimum
                                of 75% attendance in all subjects both theory and practical, as per the rules of
                                MAKAUT - NB.</li>
                            <li>I will appear in all the Terminal Tests and Examinations conducted by the Institute.</li>
                            <li align="justify">I will submit all the Assignments, Record Book, Home work etc. as per schedule prescribed
                                by the Institute for the purpose of continuous evaluation.</li>
                            <li align="justify">I shall make an earnest attempt to achieve academic improvement in all subjects
                                throughout the year.</li>
                            <li align="justify">I shall strictly follow the guide-lines/activities initiated by the Institute for
                                my training and placement support. </li>
                            <li align="justify">I will be fully committed to my studies and will maintain absolute discipline in
                                the Institute.</li>
                            <li align="justify">I will keep up the good name of the Institute in all my thoughts, behavior and action;
                                I shall abide by the rules and regulations of the Institute.</li>
                            <li align="justify">I will not cause any damage, deface any property of the Institute and Hostel such
                                as Benches, Walls, Switchboards, Furniture, Laboratory Equipment etc. or any public
                                property in the vicinity of the Institute.</li>
                            <li align="justify">I shall strictly abide by the rules and regulations as laid down by the Institute
                                and changes made by the Institute from time to time</li>
                            <li align="justify">I shall not indulge or take any role in ragging activities and any other kind of
                                misbehavior. </li>
                            <li align="justify">I shall implicitly accept the decision of the Director / Dean / Management as final
                                in all matter of discipline and others. </li>
                            <li>I confirm that:
                                <ol>
                                    <li>I have not taken admission in any other University / Institution after passing CET
                                        or equivalent examination. </li>
                                    <li>I joined ............................................................................................................................<br /><br />
                                        in the session.......................................under...................................................................University<br /><br />
                                        and Transfer / Migration Certificate is enclosed.</li>
                                </ol>
                            </li>
                        </ol>
                    </td>
                </tr>
            </table>
            <br />
            <table width="100%" align="center" class="table">
                <tr>
                    <td>
                        Place:...................
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Date:....................
                    </td>
                    <td align="right">
                      <p style="border-style: solid none none none; border-width: 1px; width:160px">  <asp:Label ID="Label7" runat="server" Text="Signature of the Candidate"></asp:Label></p>
                        
                    </td>
                </tr>
            </table>
            <br />
            
            <table width="100%" align="center" class="table">
                <tr>
                    <td>B) Declaration by the Parents / Guardian</td>
                </tr>
                <tr>
                    <td>
                        <ol>
                            <li align="justify">I hereby declare that I am aware of the financial obligation of my ward and I can
                                afford to pay all the cost and I undertake to pay the fees payable to the Institute
                                under the rules in force and which may be revised from time to time by the Institute.
                                I further confirm that I shall follow the payment norms for all the semesters as
                                stipulated by the Institute in the prospectus. I am also aware that the fees paid
                                to the Institute for admission will be forfeited in case of discontinuation of the
                                studies of my Son / Daughter / Ward for any reason whatsoever. </li>
                            <li align="justify">I have read and agreed to abide by the rules and conditions in respects of admission
                                of my Son / Daughter / Ward. I shall be responsible for his / her good conduct,
                                attendance and discipline during the period of his / her study in this Institute.
                                I assure that my Son / Daughter / Ward shall obey the rules of the institute if
                                he / she fails to do so and any action taken by you for such act(s), the decision
                                taken by the Institute will be accepted by me and I shall not have any objection,
                                claim or legal rights whatsoever for the refund of fees or for any other matters
                                if my Son / Daughter / Ward is either expelled by the Institute of or leaves the
                                Institute on' his / her own for any reason whatsoever. </li>
                            <li align="justify">That in the event my ward opts for hostel facility, provided by the Institute, the
                                Ward shall follow all norms as stipulated by the Institute. </li>
                        </ol>
                    </td>
                </tr>
            </table>
            <br />
            <table width="100%" align="center" class="table">
                <tr>
                    <td>
                        Place:...................
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Date:....................
                    </td>
                    <td align="right">
                    <p style="border-style: solid none none none; border-width: 1px; width:230px">Signature of the Parents / Guardian</p>
                    </td>
                </tr>
            </table>
        </div>
        <%--<div style="width: 740px;">
            <h6 align="left" style="color: #00356A;">
                8.LIST OF DOCUMENTS REQUIRED FOR ADMISSION IN B.TECH
            </h6>
            <table width="100%" align="center" class="table">
                <tr>
                    <td>
                        <h5>
                            A. At the Counselling Centre
                        </h5>
                        <ol>
                            <li>Original Rank Card of WBJEE / AIEEE / JELAT </li>
                            <li>Allotment slip issued by Central Selection Committee at the time of counselling.
                            </li>
                        </ol>
                        <br />
                        <h5>
                            B. At the Campus
                        </h5>
                        <ol>
                            <li>Ten copies of colour stamp size photograph (Including Front Page Copy). </li>
                            <li>Original Admit Card of Secondary Standard in support of Age Proof / Date of Birth
                                along with two Xerox copies</li>
                            <li>Original Mark Sheet of 10+2 Examination passed along with two Xerox copies</li>
                            <li>Original Mark Sheet of Graduation Examination along with two Xerox copies.</li>
                            <li>Registration Number with Year is mandatory</li>
                            <li>Original School Leaving Certificate / Character Certificate from the Head of the
                                Institution studied. </li>
                            <li>Original University Migration Certificate (if applicable). </li>
                            <li>Blood Group report by Pathological Centre. (May be submitted later but within seven
                                days from the date of commencement of the 1st session)</li>
                            <li>Caste Certificate in respect of SC / ST candidates (if applicable).<br />
                                (The above documents are required to be produced during admission at the Campus).
                            </li>
                        </ol>
                    </td>
                </tr>
            </table>
        </div>--%>
        <div style="width: 740px;">
<%--            <h6 align="Center" style="color: #00356A;">
                For Office Use Only
            </h6>
--%><%--            <table width="100%" align="center" 
                style="border-style: solid; border-width: 1px; text-align: center;" 
                 >
                <tr>
                    <td rowspan="2" height="100px" style="border-style: none solid solid none; border-width: 1px">
                        Verified Original Documents
                    </td>
                    <td height="30px" style="border-style: none solid solid none; border-width: 1px">
                        Date of Enrollment
                    </td>
                    <td height="30px" style="border-style: none solid solid none; border-width: 1px">
                        Enrollment No.
                    </td>
                    <td height="30px" style="border-style: none solid solid none; border-width: 1px">
                        Batch
                    </td>
                    <td height="30px" style="border-style: none solid solid none; border-width: 1px">
                        Payment Type
                    </td>
                    <td height="30px" style="border-style: none none solid  none; border-width: 1px">
                        Net Fees
                    </td>
                </tr>
                <tr>
                    <td height="70px" style="border-style: none solid solid none; border-width: 1px">
                    </td>
                    <td height="70px" style="border-style: none solid solid none; border-width: 1px">
                    </td>
                    <td height="70px" style="border-style: none solid solid none; border-width: 1px">
                    </td>
                    <td height="70px" style="border-style: none solid solid none; border-width: 1px">
                    </td>
                    <td height="70px" style="border-style: none none solid  none; border-width: 1px">
                    </td>
                </tr>
                <tr>
                    <td rowspan="2" height="100px" style="border-style: none solid none  none; border-width: 1px">
                        Verified Original Documents
                    </td>
                    <td height="100px" style="border-style: none solid none  none; border-width: 1px">
                        Date of Birth
                    </td>
                    <td height="100px" style="border-style: none solid none  none; border-width: 1px">
                        10+2 or 10+2+3 Mark sheet
                    </td>
                    <td height="100px" style="border-style: none solid none  none; border-width: 1px">
                        Stream
                    </td>
                    <td colspan="2" height="100px">
                        Transfer Migration
                    </td>
                </tr>
            </table>
--%>            <br />
            <br />
            <p style="text-align: left">
                Submitted by : <asp:Label ID="lblSubmittedBy" runat="server" ></asp:Label>
                <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;......................................<br />
                Verified by : <asp:Label ID="lblVarifiedBy" runat="server" ></asp:Label>
                <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;......................................<br />
                <br />
                Date : <asp:Label ID="lblAFVDate" runat="server" ></asp:Label>
                <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.......................................<br />
            </p>
        </div>
        <input type="button" value="Print" onclick="this.style.display = 'none'; window.print(); this.style.display = 'block';" />
    </center>
    </form>
</body>
</html>
