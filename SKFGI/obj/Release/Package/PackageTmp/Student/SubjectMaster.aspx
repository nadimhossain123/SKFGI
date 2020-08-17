<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubjectMaster.aspx.cs"
    Inherits="CollegeERP.Student.SubjectMaster" Title="Subject Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Subject Master</title>
    <link href="../Styles/style.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/Control.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/blue.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/reset.css" rel="Stylesheet" type="text/css" />
</head>
<body style="background-color: #fff;">
    <script language="javascript" type="text/javascript" src="../Scripts/ssjscript.js"></script>
    <script type="text/javascript">
        function Validation() {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtSubjectCode.ClientID%>'), "Subject Code", 1)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtSubjectName.ClientID%>'), "Subject Name", 1)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlCourse.ClientID%>'), "Course", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlStream.ClientID%>'), "Stream", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlSemester.ClientID%>'), "Semester", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlSubjectType.ClientID%>'), "Subject Type", 2)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtMinAttendance.ClientID%>'), "Min Attendence", 1)) return false;
            return true;
        }

        function RefreshParent() {
            window.close();
            opener.location.reload();
        }
    </script>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <br />
            <uc3:Message ID="Message" runat="server" />
            <br />
            <center>
                <div style="width: 750px;">
                    <table width="90%" align="center" style="padding: 4px; background-color: #fff;">
                        <tr>
                            <td align="left" width="20%" class="label">
                                Subject Code:<span class="req">*</span>
                            </td>
                            <td align="left" width="30%">
                                <asp:TextBox ID="txtSubjectCode" runat="server" CssClass="textbox_required" Width="140px"></asp:TextBox>
                            </td>
                            <td align="left" width="20%" class="label">
                                Subject Name:<span class="req">*</span>
                            </td>
                            <td align="left" width="30%">
                                <asp:TextBox ID="txtSubjectName" runat="server" CssClass="textbox_required" Width="140px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Parent Subject:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlParentSubject" runat="server" CssClass="dropdownList" Width="150px"
                                    DataValueField="SubjectId" DataTextField="SubjectNameWithCode">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                Course:<span class="req">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCourse" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                    Width="150px" DataValueField="CourseId" DataTextField="CourseName" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Stream:<span class="req">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStream" runat="server" CssClass="dropdownList" Width="150px"
                                    DataValueField="StreamId" DataTextField="stream_name">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                Semester:<span class="req">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSemester" runat="server" CssClass="dropdownList" Width="150px">
                                    <asp:ListItem Value="0" Text="---SELECT---"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="SEM-1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="SEM-2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="SEM-3"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="SEM-4"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="SEM-5"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="SEM-6"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="SEM-7"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="SEM-8"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Subject Type:<span class="req">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSubjectType" runat="server" CssClass="dropdownList" Width="150px">
                                    <asp:ListItem Text="--Select--" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Theoritical" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Practical" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                Min Attendance(%):<span class="req">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMinAttendance" runat="server" CssClass="textbox_required" Width="140px"
                                    MaxLength="3"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="txtMinAttendance_FilteredTextBoxExtender" ValidChars="0123456789"
                                    runat="server" TargetControlID="txtMinAttendance">
                                </asp:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="20%">
                            </td>
                            <td align="left" width="30%">
                            </td>
                            <td align="left" width="20%">
                            </td>
                            <td align="left" width="30%">
                                <asp:Button ID="btnSave" runat="server" Text="Add" CssClass="button" OnClientClick="javascript:return Validation()"
                                    OnClick="btnSave_Click" />&nbsp;
                                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Close" OnClientClick="RefreshParent();" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="90%" align="center" class="table">
                        <tr>
                            <td align="center">
                                <asp:Panel ID="PNLGrid" runat="server" ScrollBars="Vertical" Height="270px" BackColor="#FFFFFF">
                                    <asp:GridView ID="dgvSubject" runat="server" AutoGenerateColumns="false" Width="100%"
                                        AllowPaging="false" DataKeyNames="SubjectId" OnRowDeleting="dgvSubject_RowDeleting"
                                        OnRowEditing="dgvSubject_RowEditing">
                                        <Columns>
                                            <asp:BoundField DataField="SubjectCode" HeaderText="Subject Code" />
                                            <asp:BoundField DataField="SubjectName" HeaderText="Subject Name" />
                                            <asp:BoundField DataField="SubjectType" HeaderText="Subject Type" />
                                            <asp:BoundField DataField="CourseName" HeaderText="Course" />
                                            <asp:BoundField DataField="stream_name" HeaderText="Stream" />
                                            <asp:BoundField DataField="SemNo" HeaderText="Sem" />
                                            <asp:TemplateField ShowHeader="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete_icon.gif"
                                                        CommandName="Delete" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EditRowStyle" />
                                        <AlternatingRowStyle CssClass="AltRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
