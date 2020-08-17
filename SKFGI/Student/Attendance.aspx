<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="Attendance.aspx.cs" Inherits="CollegeERP.Student.Attendance" Title="Student Attendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function Validation() {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlBatch.ClientID%>'), "Batch", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlCourse.ClientID%>'), "Course", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlStream.ClientID%>'), "Stream", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlSection.ClientID%>'), "Section", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlSubject.ClientID%>'), "Subject", 0)) return false;
            return true;
        }

        function CheckAll(checkbox) {
            var gv = document.getElementById('<%=dgvStudent.ClientID%>');
            var arr = gv.getElementsByTagName("input");
            for (var i = 0; i < arr.length; i++) {
                if (arr[i].type == 'checkbox') {
                    if (checkbox.checked == true) {
                        arr[i].checked = true;
                        arr[i].parentNode.parentNode.className = 'GreenRowStyle';
                    }
                    else {
                        arr[i].checked = false;
                        arr[i].parentNode.parentNode.className = 'RedRowStyle';
                    }
                }
            }
            AttendanceCount();
        }

        function ChangeCSS(Obj) {
            var row = Obj.parentNode.parentNode;
            if (Obj.checked)
                row.className = 'GreenRowStyle';
            else
                row.className = 'RedRowStyle';

            AttendanceCount();
        }

        function AttendanceCount() {
            var gv = document.getElementById('<%=dgvStudent.ClientID%>');
            var arr = gv.getElementsByTagName("input");
            var TotalPresent = 0;
            var TotalAbsent = 0;

            for (var i = 0; i < arr.length; i++) {
                if (arr[i].type == 'checkbox') {
                    if (arr[i].checked == true) {
                        TotalPresent = parseInt(TotalPresent) + 1;
                    }
                }
            }

            TotalAbsent = parseInt(arr.length) - parseInt(TotalPresent);
            document.getElementById('<%=lblTotalPresent.ClientID%>').innerHTML = TotalPresent;
            document.getElementById('<%=lblTotalAbsent.ClientID%>').innerHTML = TotalAbsent;
        }
    </script>

    <style type="text/css">
        .GreenRowStyle td
        {
            background-color: #4CB848;
            color: #fff;
            vertical-align: middle;
            border: solid 1px #D3D3D3;
            padding: 3px 7px 3px 7px;
            height: 15px;
        }
        .RedRowStyle td
        {
            background-color: #E7433C;
            color: #fff;
            vertical-align: middle;
            border: solid 1px #D3D3D3;
            padding: 3px 7px 3px 7px;
            height: 15px;
        }
        .text
        {
            font-family: Calibri;
            font-size: 14px;
            padding-bottom: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Student Attendance</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <div style="width: 720px;">
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="20%" class="label">
                            Batch:<span class="req">*</span>
                        </td>
                        <td width="30%">
                            <asp:DropDownList ID="ddlBatch" runat="server" CssClass="dropdownList" Width="140px"
                                DataValueField="id" DataTextField="batch_name">
                            </asp:DropDownList>
                        </td>
                        <td width="20%" class="label">
                            Course:<span class="req">*</span>
                        </td>
                        <td width="30%">
                            <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" CssClass="dropdownList"
                                Width="140px" DataValueField="CourseId" DataTextField="CourseName" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Stream:<span class="req">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStream" runat="server" CssClass="dropdownList" Width="140px"
                                DataValueField="StreamId" DataTextField="stream_name" AutoPostBack="true" OnSelectedIndexChanged="ddlStream_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="label">
                            Section:<span class="req">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSection" runat="server" CssClass="dropdownList" Width="140px"
                                DataValueField="id" DataTextField="section_name">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Subject:<span class="req">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSubject" runat="server" CssClass="dropdownList" Width="140px"
                                DataValueField="SubjectId" DataTextField="SubjectNameWithCode" AutoPostBack="true" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="label">
                            Sub-Subject:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSubSubject" runat="server" CssClass="dropdownList" Width="140px"
                                DataValueField="SubjectId" DataTextField="SubjectNameWithCode">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Date:<span class="req">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAttendanceDate" runat="server" CssClass="textbox_required" Width="110px"
                                onkeydown="return false;"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtAttendanceDate"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                        <td class="label">
                            Period:<span class="req">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="dropdownList" Width="140px"
                                DataValueField="Period" DataTextField="Period">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btnShow" runat="server" CssClass="button" Text="Search" OnClientClick="return Validation();"
                                OnClick="btnShow_Click" />
                            &nbsp;
                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClientClick="return confirm('Are You Sure?');"
                                OnClick="btnSave_Click" />
                            &nbsp;
                            <asp:Button ID="btnDelete" runat="server" CssClass="button" Text="Delete" 
                                OnClientClick="return Validation();" onclick="btnDelete_Click"
                                 />
                            &nbsp;
                            <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" OnClick="btnReset_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:CheckBox ID="ChkSelect" runat="server" Text="All" onclick="javascript:CheckAll(this);" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="dgvStudent" runat="server" AutoGenerateColumns="false" Width="100%"
                                AllowPaging="false" AllowSorting="true" DataKeyNames="id" GridLines="None" CellPadding="0"
                                CellSpacing="0" OnSorting="dgvStudent_Sorting" OnRowDataBound="dgvStudent_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Present">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkSelect" runat="server" Checked='<%#Bind("IsPresent") %>' onclick="ChangeCSS(this);" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="UniversityRollNo" HeaderText="Roll No" SortExpression="UniversityRollNo" />
                                    <asp:BoundField DataField="name" HeaderText="Student Name" SortExpression="name" />
                                    <asp:BoundField DataField="student_code" HeaderText="Student Code" SortExpression="student_code" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr align="left" class="HeaderStyle">
                                            <th scope="col">
                                                No Record Found
                                            </th>
                                        </tr>
                                        <tr class="RowStyle">
                                            <td>
                                                No Record Found
                                            </td>
                                    </table>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="14%" class="text">
                            Total Present:
                        </td>
                        <td class="text" align="left">
                            <asp:Label ID="lblTotalPresent" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="14%" class="text">
                            Total Absent:
                        </td>
                        <td class="text" align="left">
                            <asp:Label ID="lblTotalAbsent" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
