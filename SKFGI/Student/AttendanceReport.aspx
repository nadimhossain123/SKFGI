<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="AttendanceReport.aspx.cs" Inherits="CollegeERP.Student.AttendanceReport"
    Title="Attendance Report" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function Validation() {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlBatch.ClientID%>'), "Batch", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlCourse.ClientID%>'), "Course", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlStream.ClientID%>'), "Stream", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlSection.ClientID%>'), "Section", 0)) return false;
            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server" CombineScripts="True">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Student Attendance Report</h5>
    </div>
    <div style="width: 720px;">
        <asp:UpdatePanel ID="UP1" runat="server">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" width="20%" class="label">
                            Batch:<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlBatch" runat="server" CssClass="dropdownList" Width="140px"
                                DataValueField="id" DataTextField="batch_name" AutoPostBack="true" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="20%" class="label">
                            Course:<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="True" CssClass="dropdownList"
                                Width="140px" DataValueField="CourseId" DataTextField="CourseName" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Stream:<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlStream" runat="server" CssClass="dropdownList" Width="140px"
                                DataValueField="StreamId" DataTextField="stream_name" AutoPostBack="true" OnSelectedIndexChanged="ddlStream_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="20%" class="label">
                            Section:<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlSection" runat="server" CssClass="dropdownList" Width="140px"
                                DataValueField="id" DataTextField="section_name" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            From Date:<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox_required" Width="110px"
                                onkeydown="return false;"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtFromDate"
                                Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                        <td align="left" width="20%" class="label">
                            To Date:<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox_required" Width="110px"
                                onkeydown="return false;"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtToDate"
                                Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Student:
                        </td>
                        <td align="left" colspan="3">
                            <asp:ComboBox ID="ddlStudent" runat="server" CssClass="WindowsStyle" AutoPostBack="false"
                                AutoCompleteMode="SuggestAppend" Width="350px" DataValueField="id" DataTextField="StudentName">
                            </asp:ComboBox>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Subject Type:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlSubjectType" runat="server" CssClass="dropdownList" Width="150px">
                                <asp:ListItem Text="All" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Theoritical" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Practical" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;
                        </td>
                        <td align="left" width="20%" class="label">
                            Min Total Attendance(%):
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMinTotalAttendance" runat="server" CssClass="textbox_required"
                                Width="140px" MaxLength="3"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtMinAttendance_FilteredTextBoxExtender" ValidChars="0123456789"
                                runat="server" TargetControlID="txtMinTotalAttendance">
                            </asp:FilteredTextBoxExtender>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Print Only Marked:
                        </td>
                        <td align="left" colspan="3">
                            <asp:CheckBox ID="chkPrintMarked" runat="server" />
                            <asp:HiddenField ID="hdnPrintMode" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                        </td>
                    </tr>
                </table>
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center">
                    <asp:Button ID="btnSummaryReport" runat="server" CssClass="button" Text="Summary Report"
                        OnClientClick="return Validation();" OnClick="btnSummaryReport_Click" />
                    &nbsp;
                    <asp:Button ID="btnSubjectWiseReport" runat="server" CssClass="button" Text="Subject Wise Report"
                        OnClientClick="return Validation();" OnClick="btnSubjectWiseReport_Click" />
                </td>
            </tr>
         </div>
     
         <div style="width:800px; margin-left:auto; margin-right:auto;">
         
         <table width="100%" cellpadding="0" cellspacing="0">
            <tr><td><br /></td></tr>
            <tr>
                <td>
                    <asp:Panel ID="PNLReport" runat="server" Width="800px" Height="100%" ScrollBars="Auto">
                        <asp:GridView ID="dgvReport" runat="server" AutoGenerateColumns="true" Width="100%"
                            GridLines="None" AllowPaging="false" AllowSorting="true" CellPadding="0" CellSpacing="0" OnRowDataBound="dgvReport_RowDataBound"
                            OnSorting="dgvReport_Sorting">
                            <HeaderStyle CssClass="HeaderStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" OnClick="btnPrint_Click" />&nbsp;
                    <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download" OnClick="btnDownload_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
