<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopUpRole.aspx.cs" Inherits="CollegeERP.Common.PopUpRole" Title="Employee Role Mapping" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../UserControl/Message.ascx" tagname="Message" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Employee Role Mapping</title>
    <link href="../Styles/style.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/Control.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/blue.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/reset.css" rel="Stylesheet" type="text/css" />
</head>
<body style=" background-color:#fff;">
    <script language="javascript" type="text/javascript" src="../Scripts/ssjscript.js"></script>
    <script type="text/javascript">
        function RoleSaveValidation() {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtRole.ClientID%>'), "Role Description", 1)) return false;
            return true;

        }
        
        function RoleAddValidation(){
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlRole.ClientID%>'), "Role", 0)) return false;
             return true;
        }
    </script>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="Script1" runat="server">
        </asp:ScriptManager>
        
      <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
        <br />  
        <uc3:Message ID="Message" runat="server" />
        <br />
        <center>
            <div style="width:700px;">
                     <table width="90%" align="center" style="padding:4px; background-color:#fff;">
                            <tr>
                                <td width="15%" align="left" class="label">Role Name<span class="req">*</span></td>
                                <td width="40%" align="left">
                                    <asp:TextBox ID="txtRole" runat="server" CssClass="textbox_required" Width="180px"></asp:TextBox>
                                </td>
                                <td width="60%" align="left">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" 
                                        OnClientClick="javascript:return RoleSaveValidation()" 
                                        onclick="btnSave_Click" />&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Close" OnClientClick="window.close();"     
                                    />
                                </td>
                            </tr>
                     </table>
                     <br />
                        <table width="90%" align="center" class="table">
                            <tr>
                                <td align="center">
                                    <asp:Panel ID="PNLGrid" runat="server" ScrollBars="Vertical" Height="180px" BackColor="#FFFFFF">
                                     <asp:GridView ID="dgvRole" runat="server" AutoGenerateColumns="false" 
                                        Width="100%" AllowPaging="false"
                                        DataKeyNames="RoleId" onrowdeleting="dgvRole_RowDeleting" 
                                            onrowediting="dgvRole_RowEditing">
                                    <Columns>
                                        <asp:BoundField DataField="RoleDescription" HeaderText="Role Description" />
                                        <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
                                        </ItemTemplate>
                                         </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete_icon.gif" CommandName="Delete" />
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                        
                                <HeaderStyle CssClass="HeaderStyle"  />
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
            <br />
            <div style="width:700px;">
                     <table width="90%" align="center" style="padding:4px; background-color:#fff;">
                            <tr>
                                <td width="15%" align="left" class="label">Role<span class="req">*</span></td>
                                <td width="40%" align="left">
                                    <asp:DropDownList ID="ddlRole" runat="server" CssClass="dropdownList" Width="180px" DataValueField="RoleId" DataTextField="RoleDescription"></asp:DropDownList>
                                </td>
                                <td width="60%" align="left">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="button" 
                                        OnClientClick="javascript:return RoleAddValidation()" 
                                        onclick="btnAdd_Click" />
                                </td>
                            </tr>
                     </table>
                     <br />
                        <table width="90%" align="center" class="table">
                            <tr>
                                <td align="center">
                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="180px" BackColor="#FFFFFF">
                                     <asp:GridView ID="dgvMap" runat="server" AutoGenerateColumns="false" 
                                        Width="100%" AllowPaging="false"
                                        DataKeyNames="EmployeeRoleMappingId" onrowdeleting="dgvMap_RowDeleting">
                                    <Columns>
                                        <asp:BoundField DataField="RoleDescription" HeaderText="Role Description" />
                                        <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete_icon.gif" CommandName="Delete" />
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                        
                                <HeaderStyle CssClass="HeaderStyle"  />
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
            <br />
            
        </center>
        </ContentTemplate>
      </asp:UpdatePanel>               
    </form>
</body>
</html>
