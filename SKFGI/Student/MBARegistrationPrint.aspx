<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MBARegistrationPrint.aspx.cs"
    Inherits="CollegeERP.Student.MBARegistrationPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
            height: 100%;
            font-family: Lucida Grande, Verdana, Lucida Sans Regular, Lucida Sans Unicode, Arial, sans-serif;
            font-size: 11px;
        }
        body span {text-transform:uppercase;}
        h6
        {
            font-size: 11px;
            color: #292929;
            font-weight: bold;
        }
        .style2
        {
            width: 23%;
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
                        <img src="../Images/logoBTech.JPG" width="100%" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Literal ID="ltrBatchname" runat="server" Mode="PassThrough"></asp:Literal><br />
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        Enrollment No
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblEnrollmentNo" runat="server"></asp:Label>
                    </td>
                    <td align="left" rowspan="3">
                        <asp:Image ID="ImgPhoto" runat="server" Width="90px" Height="90px" />
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        Option
                    </td>
                    <td align="left">
                        <asp:Label ID="lblOption" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        Rank
                    </td>
                    <td align="left">
                        <asp:Label ID="lblRankid" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 760px;">
            <table width="100%" align="center" class="table">
                <tr>
                    <td align="left" width="20%">
                        Stream applied
                    </td>
                    <td align="left">
                        <asp:Label ID="lblStreamApplied" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 760px;">
            <h6 align="left" style="color: #00356A;">
                2. Personal Details</h6>
            <table width="100%" align="center" class="table">
                <tr>
                    <td align="left" width="20%">
                        Name of Applicant
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblNameOfApplicant" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        Date of Birth
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
                    <td align="left" width="20%">
                        Father's Name
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblFatherName" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        Occupation
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblFatherOccupation" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        Mother's Name
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblMotherName" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        Occupation
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblMotherOccupation" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        Guardian's Name
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
                3. Contact Nos</h6>
            <table width="100%" align="center" class="table">
                <tr>
                    <td align="left" width="20%" style="color: #00356A;">
                        Parents/Guardian
                    </td>
                    <td align="left" width="30%">
                    </td>
                    <td align="left" width="20%" style="color: #00356A;">
                        Student
                    </td>
                    <td align="left" width="30%">
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        Residential
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblPResidential" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        Residential
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblSResidential" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        Mobile
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblPMobile" runat="server"> </asp:Label>
                    </td>
                    <td align="left" width="20%">
                        Mobile
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblSMobile" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        E-mail for
                        <br />
                        Correspondence
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
                        Hostel Facility<br />
                        Required
                    </td>
                    <td align="left" width="30%" colspan="3">
                        <asp:Label ID="lblHostelFacility" runat="server"></asp:Label>
                        &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span>Gender</span>
                        <asp:Label ID="lblGender" runat="server"></asp:Label>
                        &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span>Marital Status </span>
                        <asp:Label ID="lblMaritalStatus" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        Mother Tongue
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblMotherTong" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        Nationality
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblNationality" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        Religion
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblRealigion" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        Blood Group
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
                        Bengali
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblBengali" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        Hindi
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblHindi" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        English
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblEnglish" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        Other
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblOther" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        Cast
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblcast" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        Monthly Income
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblMonthlyIncome" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 760px;">
            <h6 align="left" style="color: #00356A;">
                4. Two References</h6>
            <table width="100%" align="center" class="table">
                <tr>
                    <td align="left" width="20%">
                        1. Name
                    </td>
                    <td>
                        <asp:Label ID="lblRefferenceName1" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        Address
                    </td>
                    <td align="left" width="30%" colspan="2">
                        <asp:Label ID="lblRefferenceAddress1" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        Contact Number
                    </td>
                    <td>
                        <asp:Label ID="lblRefferenceContactNumber1" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        2. Name
                    </td>
                    <td>
                        <asp:Label ID="lblRefferenceName2" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        Address
                    </td>
                    <td align="left" width="30%" colspan="2">
                        <asp:Label ID="lblRefferenceAddress2" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%">
                        Contact Number
                    </td>
                    <td>
                        <asp:Label ID="lblRefferenceContactNumber2" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 760px;">
            <h6 align="left" style="color: #00356A;">
                5. Educational Background</h6>
            <table class="table" width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <table id="ex" cellspacing="0" border="1" style="width: 100%; border-collapse: collapse;"
                                rules="all">
                                <tbody>
                                    <tr>
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
                                        <asp:Label ID="lblGMarks" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                    </tr>
                    <tr>
                    <td>
                    <table>
                    <tr>
                    <td align="left" width="137px"  class="label">
                        School/College<span class="req"></span>
                    </td>
                    <td align="left">
                       
                        <asp:Label ID="lblschool" runat="server" ></asp:Label>
                    </td>
                    </tr>
                    </table>
                    </td>
                </tr>
                </tbody>
            </table>
            <h6 align="left" style="color: #00356A;">
                5. Exprerience Details(If Any)</h6>
            <div style="width: 760px;">
                <table class="table" width="100%" align="center">
                    <tbody>
                        <tr>
                            <td align="center">
                                <table id="Table1" cellspacing="0" border="1" style="width: 100%; border-collapse: collapse;"
                                    rules="all">
                                    <tbody>
                                        <tr >
                                            <th scope="col">
                                                Sl.<br />
                                                No.
                                            </th>
                                            <th scope="col">
                                                Name of Comapny
                                            </th>
                                            <th scope="col">
                                                Jpb Profile /
                                                <br />
                                                Responsibility
                                            </th>
                                            <th scope="col">
                                                Date of
                                                <br />
                                                joning
                                            </th>
                                            <th scope="col">
                                                Date of
                                                <br />
                                                Leaving
                                            </th>
                                            <th scope="col">
                                                Remarks
                                            </th>
                                        </tr>
                                    </tbody>
                                    <tr>
                                        <td style="color: #00356A; width: 5">
                                            1
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl1NameOfCompany" runat="server" Style="width: 100px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl1JobProfile" runat="server" Style="width: 90px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl1DateOfJoining" runat="server" Style="width: 90px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl1DateOfLeave" runat="server" Style="width: 90px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl1Remark" runat="server" Style="width: 180px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="color: #00356A;">
                                            2
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl2NameOfCompany" runat="server" Style="width: 100px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl2JobProfile" runat="server" Style="width: 90px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl2DateOfJoining" runat="server" Style="width: 90px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl2DateOfLeave" runat="server" Style="width: 90px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl2Remark" runat="server" Style="width: 180px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="color: #00356A;">
                                            3
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl3NameOfCompany" runat="server" Style="width: 100px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl3JobProfile" runat="server" Style="width: 90px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl3DateOfJoining" runat="server" Style="width: 90px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl3DateOfLeave" runat="server" Style="width: 90px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl3Remark" runat="server" Style="width: 180px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
            </div>
            <h6 align="left" style="color: #00356A;">
                6. Details Of Qualifying Examinations
            </h6>
            <div style="width: 760px;">
                <table class="table" width="100%" align="center">
                    <tbody>
                        <tr>
                            <td align="center">
                                <table id="Table2" cellspacing="0" border="1" style="width: 100%; border-collapse: collapse;"
                                    rules="all">
                                    <tbody>
                                        <tr>
                                            <th scope="col">
                                                Test
                                            </th>
                                            <th scope="col">
                                                Date Of Examination
                                            </th>
                                            <th scope="col">
                                                Marks Obtained
                                            </th>
                                        </tr>
                                    </tbody>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblTest1" runat="server" Style="width: 200px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDateOfExam1" runat="server" Style="width: 200px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMarksObtain1" runat="server" Style="width: 200px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblTest2" runat="server" Style="width: 200px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDateOfExam2" runat="server" Style="width: 200px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMarksObtain2" runat="server" Style="width: 200px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblTest3" runat="server" Style="width: 200px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDateOfExam3" runat="server" Style="width: 200px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMarksObtain3" runat="server" Style="width: 200px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                        </tr>
                    </tbody>
                </table>
                <h6 align="left" style="color: #00356A;">
                    7. List Of Document Submitted For Admission In MBA
                </h6>
                <div style="width: 760px;">
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
                            <td colspan="3" style="color: #00356A;">
                                (The above documents are required to be produced during admission at the Campus).
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div style="width: 740px;">
                <h6 align="left" style="color: #00356A;">
                    7. Declaration
                </h6>
                <table width="100%" align="center" class="table">
                    <tr>
                        <td>
                            A.Declaration By a Student:<br />
                            To
                            <br />
                            The Campus Director<br />
                            <h4>
                                SUPREME KNOWLEDGE FOUNDATION GROUP OF INSTITUTIONS
                            </h4>
                            <br />
                            I declare that the above information is true and correct to the best of my knowledge.
                            I, intend to take admission into your College in course in the session 20 - 20 .
                            do hereby promise that:
                            <ol>
                                <li>I shall wear the Institute dress and follow the dress code strictly inside the Campus.</li>
                                <li>I will be regular in my classes throughout the year and I will maintain minimum
                                    of 75% attendance in all subjects both theory and practical, as per the rules of
                                    WBUT.</li>
                                <li>I will appear in all the Terminal Tests and Examinations conducted by the Institute.</li>
                                <li>I will submit all the Assignments, Record Book, Home work etc. as per schedule prescribed
                                    by the Institute for the purpose of continuous evaluation.</li>
                                <li>I shall make an earnest attempt to achieve academic improvement in all subjects
                                    throughout the year.</li>
                                <li>I shall strictly follow the guide-lines/activities initiated by the Institute for
                                    my training and placement support. </li>
                                <li>I will be fully committed to my studies and will maintain absolute discipline in
                                    the Institute.</li>
                                <li>I will keep up the good name of the Institute in all my thoughts, behavior and action;
                                    I shall abide by the rules and regulations of the Institute.</li>
                                <li>I will not cause any damage, deface any property of the Institute and Hostel such
                                    as Benches, Walls, Switchboards, Furniture, Laboratory Equipment etc. or any public
                                    property in the vicinity of the Institute.</li>
                                <li>I shall strictly abide by the rules and regulations as laid down by the Institute
                                    and changes made by the Institute from time to time</li>
                                <li>I shall not indulge or take any role in ragging activities and any other kind of
                                    misbehavior. </li>
                                <li>I shall implicitly accept the decision of the Director / Dean / Management as final
                                    in all matter of discipline and others. </li>
                                <li>I confirm that:
                                    <ol>
                                        <li>I have not taken admission in any other University / Institution after passing MAT/JEMAT
                                            or equivalent examination. </li>
                                        <li>I joined ............................................................................................................................<br />
                                            in the session.......................................under...................................................................University<br />
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
                            Signature of the Candidate
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                            B.Declaration by the Parents / Guardian:
                            <br />
                            <p>
                                i) I hereby declare that I am aware of the financial obligation of my ward and I
                                can afford to pay all the cost and I undertake to pay the fees payable to the Institute
                                under the rules in force and which may be revised from time to time by the Institute.
                                I further confirm that I shall follow the payment norms for all the semesters as
                                stipulated by the Institute in the prospectus. lam also aware that the fees paid
                                to the Institute for admission will be forfeited in case of discontinuation of the
                                studies of my Son / Daughter / ward for any reason whatsoever.
                            </p>
                            <p>
                                ii) I have read and agreed to abide by the rules and conditions in respects of admission
                                of my Son / Daughter / Ward. I shall be responsible for his / her good conduct,
                                attendance and discipline during the period of his / her study in this Institute.
                                I assure that my Son / Daughter / ward shall obey the rules of the institute if
                                he / she fails to do so and any action taken by you for such act(s), the decision
                                taken by the Institute will be accepted by me and I shall not have any objection,
                                claim or legal rights whatsoever for the refund of fees or for any other matters
                                if my Son / Daughter / ward is either expelled by the Institute of or leaves the
                                Institute on his / her own for any reason whatsoever.
                            </p>
                            <p>
                                iii) That in the event my ward opts for hostel facility, provided by the Institute,
                                the Ward shall follow all norms as stipulated by the Institute.
                            </p>
                        </td>
                    </tr>
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
                            Signature of the Parents/Guardian
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 740px;">
                <h6 align="left" style="color: #00356A;">
                    8.LIST OF DOCUMENTS REQUIRED FOR ADMISSION IN MBA
                </h6>
                <table width="100%" align="center" class="table">
                    <tr>
                        <td>
                            <h5>
                                A. At the Campus
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
            </div>
            <div style="width: 740px;">
                <h6 align="center" style="color: #00356A;">
                    Examination Report(for Office Use Only)
                </h6>
                <table width="100%" align="center" style="text-align: center;">
                    <tr>
                        <td align="left">
                            <b>Group Discussion </b>
                        </td>
                        <td align="left" style="padding-left: 110px">
                            <b>Interview</b>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Date......................................................
                        </td>
                        <td align="right">
                            Date.........................................................
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            ConductBy................................................
                        </td>
                        <td align="Right">
                            ConductBy................................................
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Remarks...................................................
                        </td>
                        <td align="Right">
                            Remarks...................................................
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Remarks...................................................
                        </td>
                        <td align="Right">
                            Remarks...................................................
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            ............................................................
                        </td>
                        <td align="Right">
                            ...............................................................
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            ..............................................................
                        </td>
                        <td align="Right">
                            ................................................................
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 740px;">
                <h6 align="center" style="color: #00356A;">
                    For Office Use Only
                </h6>
                <table width="100%" align="center" style="text-align: center;" border="1px">
                    <tr>
                        <td rowspan="2" height="100px">
                            Verified Original Documents
                        </td>
                        <td height="30px">
                            Date of Enrollment
                        </td>
                        <td height="30px">
                            Enrollment No.
                        </td>
                        <td height="30px">
                            Batch
                        </td>
                        <td height="30px">
                            Payment Type
                        </td>
                        <td height="30px">
                            Net Fees
                        </td>
                    </tr>
                    <tr>
                        <td height="70px">
                        </td>
                        <td height="70px">
                        </td>
                        <td height="70px">
                        </td>
                        <td height="70px">
                        </td>
                        <td height="70px">
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2" height="100px">
                            Verified Original Documents
                        </td>
                        <td height="100px">
                            Date of Birth
                        </td>
                        <td height="100px">
                            10+2 or 10+2+3 Mark sheet
                        </td>
                        <td height="100px">
                            Stream
                        </td>
                        <td colspan="2" height="100px">
                            Transfer Migration
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <p style="text-align: left">
                    Submitted by : <asp:Label ID="lblSubmittedBy" runat="server" ></asp:Label>
                <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;......................................<br />
                    Verified by...............<br />
                    <br />
                    Date......................<br />
                </p>
            </div>
            <input type="button" value="Print" onclick="this.style.display='none'; window.print(); this.style.display='block';" />
    </center>
    </form>
</body>
</html>
