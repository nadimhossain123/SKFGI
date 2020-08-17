<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="SemFeesGeneration.aspx.cs" Inherits="CollegeERP.Student.SemFeesGeneration"
    Title="Semester Fees Generation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation() {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlCourse.ClientID%>'), "Course", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlStream.ClientID%>'), "Stream", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlBatch.ClientID%>'), "Batch", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlSemNo.ClientID%>'), "Semester No", 0)) return false;
            return confirm('Are You Sure');

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Sem Fees Generation</h5>
    </div>
    <uc3:Message ID="Message" runat="server" />
    <br />
    <div style="width: 720px;">
        <table width="100%" align="center" class="table">
            <tr>
                <td align="left" width="20%" class="label">
                    Course<span class="req">*</span>
                </td>
                <td align="left" width="30%">
                    <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" CssClass="dropdownList"
                        Width="140px" DataValueField="CourseId" DataTextField="CourseName" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td align="left" width="20%" class="label">
                    Batch<span class="req">*</span>
                </td>
                <td align="left" width="30%">
                    <asp:DropDownList ID="ddlBatch" runat="server" CssClass="dropdownList" Width="140px"
                        DataValueField="id" DataTextField="batch_name">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Stream<span class="req">*</span>
                </td>
                <td align="left" width="30%">
                    <asp:DropDownList ID="ddlStream" runat="server" CssClass="dropdownList" Width="140px"
                        DataValueField="StreamId" DataTextField="stream_name">
                    </asp:DropDownList>
                </td>
                <td align="left" width="20%" class="label">
                    Semester No <span class="req">*</span>
                </td>
                <td align="left" width="30%">
                    <asp:DropDownList ID="ddlSemNo" runat="server" CssClass="dropdownList" Width="140px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Bill Date<span class="req">*</span>
                </td>
                <td align="left" colspan="3">
                    <asp:TextBox ID="txtBillDate" runat="server" CssClass="textbox_required" Width="140px" onkeydown="return false;"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                       PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtBillDate"
                       OnClientDateSelectionChanged="" Enabled="True">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Generate Fees" OnClientClick="return Validation()"
                        OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
</asp:Content>
