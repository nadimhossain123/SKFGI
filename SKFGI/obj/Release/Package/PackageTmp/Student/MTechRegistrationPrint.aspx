<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MTechRegistrationPrint.aspx.cs"
    Inherits="CollegeERP.Student.MTechRegistrationPrint" %>

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
                        <h2>
                            APPLICATION FORM FOR ADMISSION IN M-TECH</h2>
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
                        Religion
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblRealigion" runat="server"></asp:Label>
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
                        Cast
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblcast" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        &nbsp;
                    </td>
                    <td align="left" width="30%">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 760px;">
            <h6 align="left" style="color: #00356A;">
                &nbsp;</h6>
        </div>
        <div style="width: 740px;">
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
            <div>
            </div>
            <table width="100%" align="center" class="table">
                <tr>
                    <td align="left" width="20%">
                        GATE Score (If any)
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblGateScore" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%" class="label" style="color: #00356A;">
                    </td>
                    <td align="left" width="30%">
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 760px;">
            <h6 align="left" style="color: #00356A;">
                4. Year of Experience</h6>
            <table width="100%" align="center" class="table">
                <tr>
                    <td align="left" width="20%">
                        Academic
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblAcademicYrExp" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="20%">
                        Industry
                    </td>
                    <td align="left" width="30%">
                        <asp:Label ID="lblIndustryYrExp" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 760px;">
            <h6 align="left" style="color: #00356A;">
                6. List Of Document Submitted For Admission In M.Tech</h6>
            <table width="100%" align="center" class="table">
                <tr>
                    <td align="left" style="color: #00356A; width: 5px">
                        i)
                    </td>
                    <td align="left" width="5px">
                        <asp:CheckBox ID="chkListDoc1" runat="server" onclick="return false;" />
                    </td>
                    <td align="left">
                        roof of Age (Admit Card of Secondary Standard).
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
                        Mark Sheet of Graduation.
                    </td>
                </tr>
                <tr>
                    <td align="left" style="color: #00356A; width: 5px">
                        iii)
                    </td>
                    <td align="left" width="5px">
                        <asp:CheckBox ID="chkListDoc3" runat="server" onclick="return false;"/>
                    </td>
                    <td align="left">
                        Gate Score Card (If any).
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
                        Proof of SC/ST (Caste Certificate).
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
                        Ten copies of Colour Photograph (Stamp Size).
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
                        No Objection Certificate from employer (If employed).&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" style="color: #00356A; width: 5px">
                        vii)
                    </td>
                    <td align="left" width="5px">
                        <asp:CheckBox ID="chkListDoc7" runat="server" onclick="return false;"/>
                    </td>
                    <td align="left">
                        Migration Certificate (If required).
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
                        Blood Group Report.
                    </td>
                </tr>
                <tr>
                    <td align="left" style="color: #00356A; width: 5px">
                        &nbsp;
                    </td>
                    <td align="left" width="5px">
                        &nbsp;
                    </td>
                    <td align="left">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="color: #00356A;">
                    (The above documents are required to be produced during admission at the Campus)</tr>
            </table>
        </div>
        <div style="width: 740px;">
            <h6 align="left" style="color: #00356A;">
                7. Declaration
            </h6>
            <table width="100%" align="center" class="table">
                <tr>
                    <td>
                        <p>
                            I solemnly declare that the entries made in this form are true to the best of my
                            knowledge and belief and I shall be held solely responsible for any discrepancy.
                            I shall scrupulously abide by the Rules and Regulations of the Institute and do
                            everything in my power to enhance the reputation and honour of the Institute. I
                            understand that student or teasing him / her or causing him / her any physical or
                            psychological harm, I will be liable to be expelled from the Institute summarily.
                            I also solemnly affirm that I will not participate in initiate any activity that
                            may lead to disruption / damage the reputation and goodwill of the Institute. I
                            also undertake to pay Institute dues / Hostel dues regularly & will attain regular
                            classes positively.
                        </p>
                        I declare that the information given above is true and correct.
                    </td>
                </tr>
                <tr>
                    <td>
                        ..............................................<br />
                        (Counter Signed by Parents / Guardian)
                    </td>
                    <td align="right">
                        ........................................<br />
                        (Signature of the Candidate)
                    </td>
                </tr>
            </table>
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
        <input type="button" value="Print" onclick="this.style.display='none'; window.print(); this.style.display='block';" />
    </center>
    </form>
</body>
</html>
