<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BulkSubjectAttendance.aspx.cs"
    Inherits="CollegeERP.Student.BulkSubjectAttendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Bulk Subject Master</title>
    <link href="../Styles/style.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/Control.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/blue.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/reset.css" rel="Stylesheet" type="text/css" />
</head>
<body style="background-color: #fff;">
    <script language="javascript" type="text/javascript" src="../Scripts/ssjscript.js"></script>
    <script type="text/javascript">
        function Validation() {
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
    <asp:toolkitscriptmanager id="ToolkitScriptManager1" runat="server">
    </asp:toolkitscriptmanager>
    <asp:updatepanel id="UP1" runat="server">
        <ContentTemplate>
            <br />
            <uc3:Message ID="Message" runat="server" />
            <br />
            <center>
                <div style="width: 750px;">
                    <table width="90%" align="center" style="padding: 4px; background-color: #fff;">
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
                                <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="button" OnClientClick="javascript:return Validation()"
                                    OnClick="btnSave_Click" />&nbsp;
                                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Close" OnClientClick="RefreshParent();" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="90%" align="center" class="table">
                        <tr>
                            <td align="center">
                                <asp:Panel ID="PNLGrid" runat="server" ScrollBars="Vertical" style="height:70vh" BackColor="#FFFFFF">
                                    <asp:GridView ID="dgvSubject" runat="server" AutoGenerateColumns="false" Width="100%"
                                        AllowPaging="false">
                                        <Columns>
                                            <asp:BoundField DataField="SubjectCode" HeaderText="Subject Code" />
                                            <asp:BoundField DataField="SubjectName" HeaderText="Subject Name" />
                                            <asp:BoundField DataField="SubjectType" HeaderText="Subject Type" />
                                            <asp:BoundField DataField="CourseName" HeaderText="Course" />
                                            <asp:BoundField DataField="stream_name" HeaderText="Stream" />
                                            <asp:BoundField DataField="SemNo" HeaderText="Sem" />
                                            <asp:BoundField DataField="Practical" HeaderText="Type" />
                                            <asp:BoundField DataField="MinAttend" HeaderText="Min Attendance" />
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
    </asp:updatepanel>
    </form>
</body>
</html>
