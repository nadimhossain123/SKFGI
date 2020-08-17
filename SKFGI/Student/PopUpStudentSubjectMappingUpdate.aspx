<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopUpStudentSubjectMappingUpdate.aspx.cs"
    Inherits="CollegeERP.Student.PopUpStudentSubjectMappingUpdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Subject Mapping</title>
    <link href="../Styles/style.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/Control.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/blue.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/reset.css" rel="Stylesheet" type="text/css" />
</head>
<body style="background-color: #fff;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <br />
            <br />
            <br />
            <div style="width: 560px; margin-left: auto; margin-right: auto;">
                <asp:GridView ID="dgvElectiveSubject" runat="server" Width="100%" AutoGenerateColumns="false"
                    GridLines="None" AllowPaging="false" CellPadding="0" CellSpacing="0" DataKeyNames="Id"
                    OnRowDeleting="dgvElectiveSubject_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="SL" ItemStyle-Width="15px">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SubjectCode" HeaderText="Subject Code" />
                        <asp:BoundField DataField="SubjectName" HeaderText="Subject Name" />
                        <asp:TemplateField ItemStyle-Width="15px">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/delete_icon.gif"
                                    Width="15px" Height="15px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr class="RowStyle">
                                <td>
                                    No Subject Tagged
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="HeaderStyle" />
                    <RowStyle CssClass="RowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
