<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentMaster.aspx.cs" Inherits="CollegeERP.Common.DepartmentMaster" Title="Department Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../UserControl/Message.ascx" tagname="Message" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Department Details</title>
    <link href="../Styles/style.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/Control.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/blue.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/reset.css" rel="Stylesheet" type="text/css" />
</head>
<body style=" background-color:#fff;">
    <script type="text/javascript">
        function Validation()
        {
            if (document.getElementById('<%=txtDepartment.ClientID%>').value == '') {
            alert("Enter Department");
            return false;
            }
            else {return true;}
        }
 
        function RefreshParent()
        {
            window.close();
            opener.location.reload();
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
            <div style="width:580px;">
                     <table width="90%" align="center" style="padding:4px; background-color:#fff;">
                            <tr>
                                <td width="15%" align="left" class="label">Department<span class="req">*</span></td>
                                <td width="40%" align="left">
                                    <asp:TextBox ID="txtDepartment" runat="server" CssClass="textbox_required" Width="200px"></asp:TextBox>
                                </td>
                                <td width="60%" align="left">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" 
                                        OnClientClick="javascript:return Validation()" onclick="btnSave_Click" />&nbsp;
                                    <%--<asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Close" OnClientClick="RefreshParent();"     
                                    />--%>
                                </td>
                            </tr>
                        </table>
                        
                        <br />
                        <table width="100%" align="center">
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="dgvDepartment" runat="server" AutoGenerateColumns="false" 
                                        Width="100%" AllowPaging="false"
                                        DataKeyNames="DepartmentId" onrowdeleting="dgvDepartment_RowDeleting" 
                                        onrowediting="dgvDepartment_RowEditing">
                                    <Columns>
                                        <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                        <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
                                        </ItemTemplate>
                                         </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete_icon.gif" CommandName="Delete" OnClientClick="return confirm('Are You Sure?');" />
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                        
                                <HeaderStyle CssClass="HeaderStyle"  />
	                            <RowStyle CssClass="RowStyle" />
	                            <EmptyDataRowStyle CssClass="EditRowStyle" />
	                            <AlternatingRowStyle CssClass="AltRowStyle" />
	                            <PagerStyle CssClass="PagerStyle" />
                               </asp:GridView>
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
